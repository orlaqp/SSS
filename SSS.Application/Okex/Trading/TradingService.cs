using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using SSS.Application.OkexSdk.Core;
using SSS.Application.OkexSdk.Sdk;
using SSS.Domain.Okex.Target;
using SSS.Infrastructure.Repository.Okex;
using SSS.Utils.Seedwork.Datetime;

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

            int timetype = (int)KLineTime.十五分钟;

            DateTime time = new DateTime(2019, 04, 27, 13, 50, 10);
             
            for (int i = 0; i < 500; i++)
            {
                DateTime starttime = _okextarget.StartTime(time, KLineTime.十五分钟);

                starttime = starttime.AddSeconds(timetype * i);

                if (starttime > DateTime.Now)
                    return;

                var kdata = _okextarget.GetKLineData(instrument, timetype, starttime.ToString());

                //macd
                var yesyday_time =starttime.AddSeconds(-timetype);

                Macd yesday_macd = _macd.GetAll(x => x.instrument.Equals(instrument) && x.timetype == timetype && x.ktime == yesyday_time).FirstOrDefault();
                //Macd yesday_macd = new Macd() { dea = -23.241647, dif = -16.816302, ema12 = 5294.6, ema26 = 5311.5, macd = 12.850689 };

                Ema ema12 = _okextarget.GetEMA(instrument, timetype, 12, kdata[0].close, yesday_macd.ema12, kdata[0].time);
                Ema ema26 = _okextarget.GetEMA(instrument, timetype, 26, kdata[0].close, yesday_macd.ema26, kdata[0].time);
                Macd macd = _okextarget.GetMACD(instrument, kdata[0].close, timetype, ema12.now_ema, ema26.now_ema, kdata[0].time, yesday_macd);

                _ema.Add(ema12);
                _ema.Add(ema26);
                _ema.SaveChanges();

                _macd.Add(macd);
                _macd.SaveChanges();

                //kdj
                Kdj yesday_kdj = _kdj.GetAll(x => x.instrument.Equals(instrument) && x.timetype == timetype && x.ktime == yesyday_time).FirstOrDefault();
                //Kdj yesday_kdj = new Kdj() { k = 70.314793, d = 60.539841, j = 89.864696 };
                Kdj kdj = _okextarget.GetKdj(kdata, instrument, timetype, yesday_kdj);

                _kdj.Add(kdj);
                _kdj.SaveChanges();

                //ma 
                Ma ma_price = _okextarget.GetMaPrice(kdata, instrument, timetype);
                Ma ma_volume = _okextarget.GetMaVolume(kdata, instrument, timetype);

                _ma.Add(ma_price);
                _ma.Add(ma_volume);
                _ma.SaveChanges();
            }
        }
    }
}
