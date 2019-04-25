using System;
using Microsoft.Extensions.Logging;
using SSS.Application.Okex.Target;
using SSS.Application.OkexSdk.Sdk;
using SSS.Domain.Okex.Target;
using SSS.Infrastructure.Repository.Okex;

namespace SSS.Application.Okex.Trading
{
    public class TradingService : ITradingService
    {
        private readonly ILogger _logger;
        private readonly OkexTarget _okextarget;
        private readonly IMacdRepository _macd;
        private readonly IMaRepository _ma;
        private readonly IEmaRepository _ema;
        private readonly IKdjRepository _kdj;

        public TradingService(ILogger<TradingService> logger, OkexTarget okextarget, IMacdRepository macd, IMaRepository ma, IEmaRepository ema, IKdjRepository kdj)
        {
            _logger = logger;
            _okextarget = okextarget;
            _macd = macd;
            _ma = ma;
            _ema = ema;
            _kdj = kdj;
        }

        public void AddOrder()
        {
            string instrument = "BTC-USDT";
            string starttime = "2019-04-25 15:20:10";

            var kdata = _okextarget.GetKLineData(instrument, (int)KLineTime.一分钟, starttime);

            double yesday_ema12_3day = 5383;
            double yesday_ema26_3day = 5380.9;

            Ema yesday_ema12 = _okextarget.GetEMA(instrument, (int)KLineTime.一分钟, 12, kdata[1].close, yesday_ema12_3day, kdata[1].time);
            Ema yesday_ema26 = _okextarget.GetEMA(instrument, (int)KLineTime.一分钟, 26, kdata[1].close, yesday_ema26_3day, kdata[1].time);
            double yesday_dea = 0.52227;

            Macd macd = _okextarget.GetMACD(instrument, kdata[0].close, (int)KLineTime.一分钟, yesday_ema12.now_ema, yesday_ema26.now_ema, yesday_dea, kdata[0].time);

            _ema.Add(yesday_ema12);
            _ma.SaveChanges();

            _ema.Add(yesday_ema26);
            _ema.SaveChanges();

            _macd.Add(macd);
            _macd.SaveChanges();
        }
    }
}
