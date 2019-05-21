﻿using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SSS.Api.Seedwork;
using SSS.Domain.CQRS.Trade.Event.Events;
using System.Threading;
using System.Threading.Tasks;

namespace SSS.Domain.CQRS.Trade.Event.Handlers
{
    public class TradeUpdateEventHandler : INotificationHandler<TradeUpdateEvent>
    {
        private static ILogger _logger;

        public TradeUpdateEventHandler()
        {
            _logger = (ILogger)HttpContextService.Current.RequestServices.GetService(typeof(ILogger<ApiBaseController>));
        }

        public Task Handle(TradeUpdateEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"TradeUpdateEventHandler {JsonConvert.SerializeObject(notification)}");
            return Task.CompletedTask;
        }
    }
}
