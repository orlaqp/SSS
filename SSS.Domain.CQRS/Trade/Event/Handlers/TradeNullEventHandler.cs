using MediatR;
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
    public class TradeNullEventHandler : INotificationHandler<TradeNullEvent>
    {
        private static ILogger _logger;

        public TradeNullEventHandler()
        {
            _logger = (ILogger)HttpContextService.Current.RequestServices.GetService(typeof(ILogger<ApiBaseController>));
        }

        public Task Handle(TradeNullEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"TradeNullEventHandler {JsonConvert.SerializeObject(notification)}");
            return Task.CompletedTask;
        }
    }
}
