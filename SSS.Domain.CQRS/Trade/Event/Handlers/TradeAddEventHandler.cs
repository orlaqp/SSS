using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SSS.Domain.CQRS.Trade.Event.Events;
using SSS.Domain.Seedwork.Attribute;
using SSS.Infrastructure.Util;
using SSS.Infrastructure.Util.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SSS.Domain.CQRS.Trade.Event.Handlers
{
    [DIService(ServiceLifetime.Scoped, typeof(INotificationHandler<TradeAddEvent>))]
    public class TradeAddEventHandler : INotificationHandler<TradeAddEvent>
    {
        private static ILogger _logger;

        public TradeAddEventHandler()
        {
            _logger = (ILogger)HttpContextService.Current.RequestServices.GetService(typeof(ILogger<TradeAddEventHandler>));
        }

        public Task Handle(TradeAddEvent @event, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"TradeAddEventHandler {@event.ToJson()}");
            return Task.CompletedTask;
        }
    }
}
