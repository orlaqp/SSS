using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SSS.Domain.CQRS.Trade.Command.Commands;
using SSS.Domain.Seedwork.Bus;
using SSS.Domain.Seedwork.Model;
using SSS.Domain.Trade;
using SSS.Domain.Trade.Dto;
using SSS.Domain.Trade.Request;
using SSS.Infrastructure.Repository.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SSS.Application.Trade
{
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

        public void AddTrade(TradeInputDto input)
        {
            int ktime = 15; //15分钟线
            var kdata = GetKDataLine("BTC-USDT", ktime, DateTime.Now);
            var ma_5 = GetMaPrice("BTC-USDT", ktime, 5, kdata);
            var ma_10 = GetMaPrice("BTC-USDT", ktime, 10, kdata);

            input.id = Guid.NewGuid();
            input.price = 1;// kdata[0].close;
            input.coin = "BTC-USDT";
            input.size = 1; //0.01
            input.trade_status = 1;

            //做多
            if (ma_5 > ma_10)
                input.side = "buy";
            else
                input.side = "buy";
            string order_id = MarketOrder(input.coin, input.price, input.size, input.side, "limit");
            if (!string.IsNullOrEmpty(order_id))
                input.trade_no = order_id;

            var cmd = _mapper.Map<TradeAddCommand>(input);
            _bus.SendCommand(cmd);

            _logger.LogInformation($"AddTrade ma_5:{ma_5}  ma_10:{ma_10}");
        }

        public Pages<List<TradeOutputDto>> GetListTrade(TradeInputDto input)
        {
            List<TradeOutputDto> list = null;
            int count = 0;

            list = _traderepository.GetPage(input.pageindex, input.pagesize, ref count).ProjectTo<TradeOutputDto>(_mapper.ConfigurationProvider).ToList();
            _logger.LogInformation($"GetListTrade Result {JsonConvert.SerializeObject(list)}");

            return new Pages<List<TradeOutputDto>>(list, count);
        }

        private string MarketOrder(string coin, double price, double size, string side, string type)
        {
            LimitOrder order = new LimitOrder();
            order.side = side;
            order.instrument_id = coin;
            order.type = type;
            order.margin_trading = 2;
            order.size = size.ToString();
            order.price = price.ToString();

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
                    return "";
                    //{"code":33017,"message":"Greater than the maximum available balance"}
                }
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