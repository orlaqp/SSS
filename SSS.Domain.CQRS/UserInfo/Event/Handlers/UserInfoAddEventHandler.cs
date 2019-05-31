using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SSS.Api.Seedwork;
using SSS.Domain.CQRS.UserInfo.Event.Events;
using System.Threading;
using System.Threading.Tasks;

namespace SSS.Domain.CQRS.UserInfo.Event.Handlers
{
    [SSS.Domain.Seedwork.Attribute.DIService(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped, typeof(INotificationHandler<UserInfoAddEvent>))]
    public class UserInfoAddEventHandler : INotificationHandler<UserInfoAddEvent>
    {
        private static ILogger _logger;

        public UserInfoAddEventHandler()
        {
            _logger = (ILogger)HttpContextService.Current.RequestServices.GetService(typeof(ILogger<ApiBaseController>));
        }

        public Task Handle(UserInfoAddEvent noticen, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UserInfoAddEventHandler {JsonConvert.SerializeObject(noticen)}");
            return Task.CompletedTask;
        }
    }
}
