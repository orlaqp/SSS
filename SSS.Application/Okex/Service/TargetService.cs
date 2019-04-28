using Microsoft.Extensions.Logging;
using SSS.Application.OkexSdk.Core;
using SSS.Application.OkexSdk.Sdk;
using SSS.Domain.Okex.Target;
using SSS.Infrastructure.Repository.Okex;
using System;
using System.Linq;

namespace SSS.Application.Okex.Service
{
    public class TargetService : ITargetService
    {
        private readonly ILogger _logger;
        private readonly OkexTarget _okextarget;
        private readonly IMacdRepository _macd;
        private readonly IMaRepository _ma;
        private readonly IEmaRepository _ema;
        private readonly IKdjRepository _kdj;

        public TargetService(ILogger<TargetService> logger, OkexTarget okextarget, IMacdRepository macd, IMaRepository ma, IEmaRepository ema, IKdjRepository kdj)
        {
            _logger = logger;
            _okextarget = okextarget;
            _macd = macd;
            _ma = ma;
            _ema = ema;
            _kdj = kdj;
        }

        public void CreateTarget(DateTime time, string instrument, KLineTime timetype)
        {
            DateTime starttime = _okextarget.StartTime(time, timetype);

            starttime = starttime.AddSeconds((int)timetype);

            if (starttime > DateTime.Now)
                return;

            var kdata = _okextarget.GetKLineData(instrument, (int)timetype, starttime.ToString());

            //macd
            var yesyday_time = starttime.AddSeconds(-(int)timetype);

            Macd yesday_macd = _macd.GetAll(x => x.instrument.Equals(instrument) && x.timetype == (int)timetype && x.ktime == yesyday_time).FirstOrDefault();
            //Macd yesday_macd = new Macd() { dea = -23.241647, dif = -16.816302, ema12 = 5294.6, ema26 = 5311.5, macd = 12.850689 };

            Ema ema12 = _okextarget.GetEMA(instrument, (int)timetype, 12, kdata[0].close, yesday_macd.ema12, kdata[0].time);
            Ema ema26 = _okextarget.GetEMA(instrument, (int)timetype, 26, kdata[0].close, yesday_macd.ema26, kdata[0].time);
            Macd macd = _okextarget.GetMACD(instrument, kdata[0].close, (int)timetype, ema12.now_ema, ema26.now_ema, kdata[0].time, yesday_macd);

            _ema.Add(ema12);
            _ema.Add(ema26);
            _ema.SaveChanges();

            _macd.Add(macd);
            _macd.SaveChanges();

            //kdj
            Kdj yesday_kdj = _kdj.GetAll(x => x.instrument.Equals(instrument) && x.timetype == (int)timetype && x.ktime == yesyday_time).FirstOrDefault();
            //Kdj yesday_kdj = new Kdj() { k = 70.314793, d = 60.539841, j = 89.864696 };
            Kdj kdj = _okextarget.GetKdj(kdata, instrument, (int)timetype, yesday_kdj);

            _kdj.Add(kdj);
            _kdj.SaveChanges();

            //ma 
            Ma ma_price = _okextarget.GetMaPrice(kdata, instrument, (int)timetype);
            Ma ma_volume = _okextarget.GetMaVolume(kdata, instrument, (int)timetype);

            _ma.Add(ma_price);
            _ma.Add(ma_volume);
            _ma.SaveChanges();
        }
    }
}
