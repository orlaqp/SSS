using Microsoft.Extensions.Logging;
using SSS.Application.Okex.Target;
using SSS.Application.OkexSdk.Sdk;

namespace SSS.Application.Okex.Trading
{
    public class TradingService : ITradingService
    {
        private readonly ILogger _logger;
        private readonly OkexTarget _okextarget;

        public TradingService(ILogger<TradingService> logger, OkexTarget okextarget)
        {
            _logger = logger;
            _okextarget = okextarget;
        }

        public void AddOrder()
        {
            _okextarget.GetKLineData("BTC-USDT", (int)KLineTime.一分钟);
        }
    }
}
