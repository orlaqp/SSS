using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SSS.Api.Seedwork;
using SSS.Domain.CQRS.Student.Event.Events;
using System.Threading;
using System.Threading.Tasks;

namespace SSS.Domain.CQRS.Student.Event.Handlers
{
    [SSS.Domain.Seedwork.Attribute.DIService(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped, typeof(INotificationHandler<StudentAddEvent>))]
    public class StudentAddEventHandler : INotificationHandler<StudentAddEvent>
    {
        private static ILogger _logger;

        public StudentAddEventHandler()
        {
            _logger = (ILogger)HttpContextService.Current.RequestServices.GetService(typeof(ILogger<ApiBaseController>));
        }

        public Task Handle(StudentAddEvent noticen, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"StudentAddEventHandler {JsonConvert.SerializeObject(noticen)}");
            return Task.CompletedTask;
        }
    }
}
