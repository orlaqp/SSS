using Hangfire;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SSS.Application.Okex.Service;
using SSS.Application.OkexSdk.Sdk;

namespace SSS.Api.Job
{
    public class TargetJob : BackgroundService
    {
        private readonly ITargetService _target;

        private readonly ILogger _logger;

        public TargetJob(ILogger<TargetJob> logger, ITargetService target)
        {
            _target = target;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                RecurringJob.AddOrUpdate("十五分钟线",
                    () => _target.CreateTarget("BTC-USDT", KLineTime.十五分钟),
                    Cron.Minutely);

                //RecurringJob.AddOrUpdate("第0分钟",
                //    () => _target.CreateTarget("BTC-USDT", KLineTime.十五分钟),
                //    Cron.Hourly(0));

                //RecurringJob.AddOrUpdate("第十五分钟",
                //    () => _target.CreateTarget("BTC-USDT", KLineTime.十五分钟),
                //    Cron.Hourly(15));

                //RecurringJob.AddOrUpdate("第三十分钟",
                //    () => _target.CreateTarget("BTC-USDT", KLineTime.十五分钟),
                //    Cron.Hourly(30));

                //RecurringJob.AddOrUpdate("第四十五分钟",
                //    () => _target.CreateTarget("BTC-USDT", KLineTime.十五分钟),
                //    Cron.Hourly(45));

                //RecurringJob.AddOrUpdate("一小时",
                //    () => _target.CreateTarget("BTC-USDT", KLineTime.一小时),
                //    Cron.Hourly(0));

                //RecurringJob.AddOrUpdate("一天",
                //    () => _target.CreateTarget("BTC-USDT", KLineTime.一天),
                //    Cron.Daily(0));

            }
            catch (Exception e)
            {
                _logger.LogError(e, "TargetJob异常");
            }

            return Task.CompletedTask;
        }
    }
}
