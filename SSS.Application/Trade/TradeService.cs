using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SSS.Domain.CQRS.Trade.Command.Commands;
using SSS.Domain.Seedwork.Bus;
using SSS.Domain.Seedwork.Model;
using SSS.Domain.Trade;
using SSS.Domain.Trade.Dto;
using SSS.Infrastructure.Repository.Trade;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            //input.trade_no = "123";
            input.size = 1;
            input.trade_status = 1;

            //做多
            if (ma_5 > ma_10)
                input.side = "buy";
            else
                input.side = "buy";
            MarketOrder(input.coin, input.price, input.size, input.side, "limit");

            var cmd = _mapper.Map<TradeAddCommand>(input);
            _bus.SendCommand(cmd);

            _logger.LogInformation($"AddTrade ma_5:{ma_5}  ma_10:{ma_10}");
        }

        public Pages<List<TradeOutputDto>> GetListTrade(TradeInputDto input)
        {
            List<TradeOutputDto> list = null;
            int count = 0;
            return new Pages<List<TradeOutputDto>>(list, count);
        }

        private void MarketOrder(string coin, double price, double size, string side, string type)
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
                //{"code":33017,"message":"Greater than the maximum available balance"}
                //{"client_oid":"","error_code":"","error_message":"","order_id":"2857929499812864","result":true}
                var result = JObject.Parse(contentStr);
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

            return list;
        }
    }

    public class HttpInterceptor : DelegatingHandler
    {
        private string _apiKey;
        private string _passPhrase;
        private string _secret;
        private string _bodyStr;
        public HttpInterceptor(string apiKey, string secret, string passPhrase, string bodyStr)
        {
            this._apiKey = apiKey;
            this._passPhrase = passPhrase;
            this._secret = secret;
            this._bodyStr = bodyStr;
            InnerHandler = new HttpClientHandler();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var method = request.Method.Method;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("OK-ACCESS-KEY", this._apiKey);

            var now = DateTime.Now;
            var timeStamp = TimeZoneInfo.ConvertTimeToUtc(now).ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            var requestUrl = request.RequestUri.PathAndQuery;
            string sign = "";
            if (!String.IsNullOrEmpty(this._bodyStr))
            {
                sign = Encryptor.HmacSHA256($"{timeStamp}{method}{requestUrl}{this._bodyStr}", this._secret);
            }
            else
            {
                sign = Encryptor.HmacSHA256($"{timeStamp}{method}{requestUrl}", this._secret);
            }

            request.Headers.Add("OK-ACCESS-SIGN", sign);
            request.Headers.Add("OK-ACCESS-TIMESTAMP", timeStamp.ToString());
            request.Headers.Add("OK-ACCESS-PASSPHRASE", this._passPhrase);

            return base.SendAsync(request, cancellationToken);
        }
    }

    static class Encryptor
    {
        public static string HmacSHA256(string infoStr, string secret)
        {
            byte[] sha256Data = Encoding.UTF8.GetBytes(infoStr);
            byte[] secretData = Encoding.UTF8.GetBytes(secret);
            using (var hmacsha256 = new HMACSHA256(secretData))
            {
                byte[] buffer = hmacsha256.ComputeHash(sha256Data);
                return Convert.ToBase64String(buffer);
            }
        }

        public static string MakeSign(string apiKey, string secret, string phrase)
        {
            var timeStamp = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            var sign = Encryptor.HmacSHA256($"{timeStamp}GET/users/self/verify", secret);
            var info = new
            {
                op = "login",
                args = new List<string>()
                {
                    apiKey,phrase,timeStamp.ToString(),sign
                }
            };
            return JsonConvert.SerializeObject(info);
        }
    }
}