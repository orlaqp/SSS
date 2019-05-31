﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SSS.Api.Seedwork;
using SSS.Domain.CQRS.Trade.Event.Events;
using SSS.Domain.Seedwork.Attribute;
using System.Threading;
using System.Threading.Tasks;

namespace SSS.Domain.CQRS.Trade.Event.Handlers
{
    [DIService(ServiceLifetime.Scoped, typeof(INotificationHandler<TradeAddEvent>))]
    public class TradeUpdateEventHandler : INotificationHandler<TradeUpdateEvent>
    {
        private static ILogger _logger;

        public TradeUpdateEventHandler()
        {
            _logger = (ILogger)HttpContextService.Current.RequestServices.GetService(typeof(ILogger<ApiBaseController>));
        }

        public Task Handle(TradeUpdateEvent noticen, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"TradeUpdateEventHandler {JsonConvert.SerializeObject(noticen)}");
            return Task.CompletedTask;
        }
    }
}
