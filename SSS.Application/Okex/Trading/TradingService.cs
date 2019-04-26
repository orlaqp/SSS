using System;
using Microsoft.Extensions.Logging;
using SSS.Application.OkexSdk.Core;
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
            string starttime = "2019-04-26 15:45:10";
            int ktime = (int)KLineTime.一分钟;

            var kdata = _okextarget.GetKLineData(instrument, ktime, starttime);


            //macd

            double yesday_ema12_3day = 5289.3;
            double yesday_ema26_3day = 5292.2;

            Ema yesday_ema12 = _okextarget.GetEMA(instrument, ktime, 12, kdata[1].close, yesday_ema12_3day, kdata[1].time);
            Ema yesday_ema26 = _okextarget.GetEMA(instrument, ktime, 26, kdata[1].close, yesday_ema26_3day, kdata[1].time);
            double yesday_dea = -2.466058;

            Macd macd = _okextarget.GetMACD(instrument, kdata[0].close, ktime, yesday_ema12.now_ema, yesday_ema26.now_ema, yesday_dea, kdata[0].time);

            _ema.Add(yesday_ema12);
            _ema.Add(yesday_ema26);
            _ema.SaveChanges();

            _macd.Add(macd);
            _macd.SaveChanges();



            //kdj
            double yesday_k = 50.401117;
            double yesday_d = 40.708779;
            Kdj kdj = _okextarget.GetKdj(kdata,instrument, ktime, yesday_k, yesday_d);
            _kdj.Add(kdj);
            _kdj.SaveChanges();


            //ma 
            Ma ma_price = _okextarget.GetMaPrice(kdata,instrument, ktime);
            Ma ma_volume= _okextarget.GetMaVolume(kdata,instrument, ktime);
            _ma.Add(ma_price);
            _ma.Add(ma_volume);
            _ma.SaveChanges();
        }
    }
}
