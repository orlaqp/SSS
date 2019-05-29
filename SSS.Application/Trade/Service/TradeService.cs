﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SSS.Domain.CQRS.Trade.Command.Commands;
using SSS.Domain.Seedwork.Attribute;
using SSS.Domain.Seedwork.Bus;
using SSS.Domain.Seedwork.Model;
using SSS.Domain.Trade;
using SSS.Domain.Trade.Dto;
using SSS.Domain.Trade.Request;
using SSS.Domain.Trade.Response;
using SSS.Infrastructure.Repository.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SSS.Application.Trade
{
    [DIService(ServiceLifetime.Scoped, typeof(ITradeService))]
    public class TradeService : ITradeService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly ILogger _logger;

        private readonly ITradeRepository _traderepository;
        public TradeService(ILogger<TradeService> logger, IMapper mapper, IMediatorHandler bus, ITradeRepository traderepository)
        {
            _mapper = mapper;
            _bus = bus;
            _traderepository = traderepository;
            _logger = logger;
        }

        /// <summary>
        /// 下单接口
        /// </summary>
        /// <param name="input"></param>
        public void OperateTrade(TradeInputDto input)
        {
            var kdata = GetKDataLine(input.coin, input.ktime, DateTime.Now);
            var ma_5 = GetMaPrice(input.coin, input.ktime, 5, kdata);
            var ma_10 = GetMaPrice(input.coin, input.ktime, 10, kdata);
            double curruent_price = kdata[0].close; //当前收盘价

            //【1.判断趋势】
            if (ma_5 > ma_10) //做多 
                input.side = "buy";
            else //做空
                input.side = "sell";

            //【2.查看是否持有单子】
            var order_list = _traderepository.GetAll(x => x.First_Trade_Status == 1 && x.Coin.Equals(input.coin)).ProjectTo<TradeOutputDto>(_mapper.ConfigurationProvider).ToList();

            //平单
            if (order_list != null && order_list.Count > 0)
            {
                //【3.如果有相同单子】判断并止盈止损位
                if (ProfitOrLossPoint(order_list, input, curruent_price))
                    return;

                //【4.查看持有单子,是否满足平单要求】
                //查看是否【持有且需要】平多平空订单，检查是否能够盈利且平单 
                if (StopTrade(order_list, input, curruent_price))
                    return;
            }
            //【5.开单】新建订单
            else
            {
                input.first_price = curruent_price;
                if (CreateTrade(input))
                    return;
            }

            input.id = Guid.NewGuid();
            input.last_time = DateTime.Now;
            input.last_price = curruent_price;
            input.first_trade_no = order_list[0].first_trade_no;
            var null_cmd = _mapper.Map<TradeNullCommand>(input);
            _bus.SendCommand(null_cmd);
            _logger.LogInformation($"无符合的交易规则  input:{JsonConvert.SerializeObject(input)}");
        }

        /// <summary>
        /// 止盈止损判断,如果满足止盈止损条件直接处理，否则继续执行
        /// </summary>
        /// <param name="input"></param>
        /// <param name="curruent_price">当前价格</param>
        private bool ProfitOrLossPoint(List<TradeOutputDto> order_list, TradeInputDto input, double curruent_price)
        {
            var current_order = order_list.Where(x => x.side.Equals(input.side)).FirstOrDefault();
            if (current_order == null)
            {
                _logger.LogInformation($"止盈止损判断 没有相同方向单子，继续下单");
                return false;
            }

            _logger.LogInformation($"止盈止损判断 已有订单：{JsonConvert.SerializeObject(current_order)}");
            if (current_order.side.Equals("buy"))
            {
                //做多 盈利超过10%止盈
                if ((curruent_price - current_order.first_price) * 100 > 10)
                {
                    input.side = "sell";
                    input.last_price = curruent_price;
                    input.last_time = DateTime.Now;
                    _logger.LogInformation($"止盈止损判断 做多止盈 {JsonConvert.SerializeObject(input)}");
                    UpdateTrade(input);
                    return true;
                }
                //做多 亏损超过10%止损
                else if ((current_order.first_price - curruent_price) * 100 > 10)
                {
                    input.side = "sell";
                    input.last_price = curruent_price;
                    input.last_time = DateTime.Now;
                    _logger.LogInformation($"止盈止损判断 做多止损 {JsonConvert.SerializeObject(input)}");
                    UpdateTrade(input);
                    return true;
                }
            }
            else if (current_order.side.Equals("sell"))
            {
                //做空 盈利超过10%止盈
                if ((current_order.first_price - curruent_price) * 100 > 10)
                {
                    input.side = "buy";
                    input.last_price = curruent_price;
                    input.last_time = DateTime.Now;
                    _logger.LogInformation($"止盈止损判断 做空止盈 {JsonConvert.SerializeObject(input)}");
                    UpdateTrade(input);
                    return true;
                }
                //做空 亏损超过10%止损
                else if ((curruent_price - current_order.first_price) * 100 > 10)
                {
                    input.side = "buy";
                    input.last_price = curruent_price;
                    input.last_time = DateTime.Now;
                    _logger.LogInformation($"止盈止损判断 做空止损 {JsonConvert.SerializeObject(input)}");
                    UpdateTrade(input);
                    return true;
                }
            }
            _logger.LogInformation($"止盈止损判断 相同方向单子，没有且没有达到止盈止损需求 {JsonConvert.SerializeObject(current_order)}");
            return false;
        }

        /// <summary>
        /// 检查订单是否满足平单要求,如果满足则继续执行，否则跳出
        /// </summary>
        /// <param name="input"></param> 
        /// <param name="curruent_price">当前价格</param>
        public bool StopTrade(List<TradeOutputDto> order_list, TradeInputDto input, double curruent_price)
        {
            var current_order = order_list.Where(x => !x.side.Equals(input.side)).FirstOrDefault();
            if (current_order == null)
            {
                _logger.LogInformation($"检查订单是否满足平单要求，没有单子");
                return false;
            }

            //【1 平空】
            //如果是平空
            if (input.side == "buy" && current_order.side.Equals("sell"))
            {
                //如果是平空，市场价不能高于订单价
                if (curruent_price > current_order.first_price)
                {
                    _logger.LogInformation($"检查订单是否满足平单要求，平空失败，价格太高 {JsonConvert.SerializeObject(current_order)}");
                    return false;
                }
                else
                {
                    //平单
                    if (current_order.side.Equals("buy"))
                        input.side = "sell";
                    else if (current_order.side.Equals("sell"))
                        input.side = "buy";
                    input.last_time = DateTime.Now;
                    input.last_price = curruent_price;
                    UpdateTrade(input);
                    return true;
                }
            }
            //【2 平多】
            //如果是平多
            else if (input.side == "sell" && current_order.side.Equals("buy"))
            {
                //如果是平多，市场价不能高于订单价
                if (curruent_price < current_order.first_price)
                {
                    _logger.LogInformation($"检查订单是否满足平单要求，平多失败，价格太低 {JsonConvert.SerializeObject(current_order)}");
                    return false;
                }
                else
                {
                    //平单
                    if (current_order.side.Equals("buy"))
                        input.side = "sell";
                    else if (current_order.side.Equals("sell"))
                        input.side = "buy";
                    input.last_time = DateTime.Now;
                    input.last_price = curruent_price;
                    UpdateTrade(input);
                    return true;
                }
            }
            _logger.LogInformation($"检查订单是否满足平单要求，异常单子,非buy、sell {JsonConvert.SerializeObject(current_order)}");
            return false;
        }

        /// <summary>
        /// 开单
        /// </summary>
        /// <param name="input"></param>
        private bool CreateTrade(TradeInputDto input)
        {
            //【1 开单,获取订单号】
            string order_id = MarketOrder(input.coin, input.first_price, input.size, input.side);
            if (order_id == null)
            {
                _logger.LogError("开单，下单失败");
                return false;
            }
            input.first_trade_no = order_id;

            //【2 根据订单号查询具体订单详情】
            var orderinfo = GetOrderInfo(input.coin, order_id);
            if (orderinfo == null)
            {
                _logger.LogError("开单，查询订单详情失败");
                return false;
            }

            input.id = Guid.NewGuid();
            input.first_trade_status = Convert.ToInt32(orderinfo.state);
            input.first_price = Convert.ToDouble(orderinfo.filled_notional);
            input.side = orderinfo.side;
            input.size = Convert.ToDouble(orderinfo.filled_size);
            input.first_time = Convert.ToDateTime(orderinfo.timestamp);

            //【3 增加一条订单】
            var add_cmd = _mapper.Map<TradeAddCommand>(input);
            _bus.SendCommand(add_cmd);
            _logger.LogInformation($"开单  input:{JsonConvert.SerializeObject(input)}");
            return true;
        }

        /// <summary>
        /// 平单
        /// </summary>
        /// <param name="input"></param>
        private void UpdateTrade(TradeInputDto input)
        {
            //【1 平单,获取订单号】
            string order_id = MarketOrder(input.coin, input.last_price, input.size, input.side);
            if (order_id == null)
            {
                _logger.LogError("UpdateTrade，平单失败");
                return;
            }
            input.last_trade_no = order_id;

            //【2 根据订单号查询具体订单详情】
            var orderinfo = GetOrderInfo(input.coin, order_id);
            if (orderinfo == null)
            {
                _logger.LogError("UpdateTrade，平单，查询订单详情失败");
                return;
            }
            input.last_trade_status = Convert.ToInt32(orderinfo.state);
            input.last_price = Convert.ToDouble(orderinfo.notional);
            input.last_time = Convert.ToDateTime(orderinfo.timestamp);

            //【3 更新一条订单】
            var update_cmd = _mapper.Map<TradeUpdateCommand>(input);
            _bus.SendCommand(update_cmd);
            _logger.LogInformation($"平单 input:{JsonConvert.SerializeObject(input)}");

        }

        public Pages<List<TradeOutputDto>> GetListTrade(TradeInputDto input)
        {
            List<TradeOutputDto> list = null;
            int count = 0;

            list = _traderepository.GetPage(input.pageindex, input.pagesize, ref count).ProjectTo<TradeOutputDto>(_mapper.ConfigurationProvider).ToList();
            _logger.LogInformation($"GetListTrade Result {JsonConvert.SerializeObject(list)}");

            return new Pages<List<TradeOutputDto>>(list, count);
        }

        /// <summary>
        /// 市价下单
        /// </summary>
        /// <param name="coin"></param>
        /// <param name="price"></param>
        /// <param name="size"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        private string MarketOrder(string coin, double price, double size, string side)
        {
            MarketOrder order = new MarketOrder();
            order.side = side;
            order.instrument_id = coin;
            order.type = "market";
            order.margin_trading = 2;

            if (order.side.Equals("buy"))
                order.notional = (price * size).ToString();
            else
                order.size = size.ToString();

            var postdata = JsonConvert.SerializeObject(order);

            using (var client = new HttpClient(new HttpInterceptor("2b90783f-0e71-4a84-a767-d932b062b1fe", "260E06424BACF0AE22E6E0B8B657499E", "123456", postdata)))
            {
                var res = client.PostAsync("https://www.okex.me/api/margin/v3/orders", new StringContent(postdata, Encoding.UTF8, "application/json")).Result;

                var contentStr = res.Content.ReadAsStringAsync().Result;
                var result = JObject.Parse(contentStr);
                _logger.LogInformation($"MarketOrder Result {contentStr}");
                if (contentStr.Contains("order_id"))
                {
                    return result["order_id"].ToString();
                    //{ "client_oid":"","error_code":"","error_message":"","order_id":"2857929499812864","result":true}
                }
                else
                {
                    return null;
                    //{"code":33017,"message":"Greater than the maximum available balance"}
                }
            }
        }

        /// <summary>
        /// 获取订单信息
        /// </summary> 
        public OrderInfoResponse GetOrderInfo(string coin, string order_id)
        {
            var url = $"https://www.okex.me/api/margin/v3/orders/{order_id}";
            using (var client = new HttpClient(new HttpInterceptor("2b90783f-0e71-4a84-a767-d932b062b1fe", "260E06424BACF0AE22E6E0B8B657499E", "123456", null)))
            {
                var queryParams = new Dictionary<string, string>();
                queryParams.Add("instrument_id", coin);
                var encodedContent = new FormUrlEncodedContent(queryParams);
                var paramsStr = encodedContent.ReadAsStringAsync().Result;
                var res = client.GetAsync($"{url}?{paramsStr}").Result;

                var result = res.Content.ReadAsStringAsync().Result;
                _logger.LogInformation($"GetOrderInfo {result}");
                if (result.Contains("error"))
                    return null;
                return JsonConvert.DeserializeObject<OrderInfoResponse>(result);
            }
        }

        /// <summary>
        /// 获取均价线值
        /// </summary>
        private double GetMaPrice(string coin, int ktime, int size, List<KData> kdata = null)
        {
            var result = kdata.Skip(0).Take(size) ?? GetKDataLine(coin, ktime, DateTime.Now).Skip(0).Take(size);
            return result.Sum(x => x.close) / size;
        }

        private List<KData> GetKDataLine(string coin, int ktime, DateTime time)
        {
            string url = $"https://www.okex.me/api/spot/v3/instruments/{coin}/candles?granularity={ktime * 60}&start={time}";
            WebClient client = new WebClient();
            var result = client.DownloadString(url);
            if (string.IsNullOrWhiteSpace(result))
                return null;
            JArray array = JArray.Parse(result);

            List<KData> list = new List<KData>();

            for (int i = 0; i < array.Count; i++)
            {
                KData data = new KData();
                data.time = Convert.ToDateTime(array[i][0]).AddHours(8);
                data.open = Convert.ToDouble(array[i][1]);
                data.high = Convert.ToDouble(array[i][2]);
                data.low = Convert.ToDouble(array[i][3]);
                data.close = Convert.ToDouble(array[i][4]);
                data.volume = Convert.ToDouble(array[i][5]);
                list.Add(data);
            }

            _logger.LogInformation($"KDataLine Result {JsonConvert.SerializeObject(list)}");

            return list;
        }
    }
}