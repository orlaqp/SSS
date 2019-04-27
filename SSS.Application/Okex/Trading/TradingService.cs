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
            string starttime = "2019-04-26 15:50:10";
            int timetype = (int)KLineTime.一分钟;

            var kdata = _okextarget.GetKLineData(instrument, timetype, starttime);

            //macd
            var yesyday_time = Convert.ToDateTime(Convert.ToDateTime(starttime).AddSeconds(-timetype).GetDateTimeFormats('g')[0].ToString());

            Macd yesday_macd = _macd.GetAll(x => x.instrument.Equals(instrument) && x.timetype == timetype && x.ktime == yesyday_time).FirstOrDefault();

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
