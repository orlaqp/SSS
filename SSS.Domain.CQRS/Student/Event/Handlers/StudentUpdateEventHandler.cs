using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json; 
using SSS.Domain.CQRS.Student.Event.Events;
using SSS.Domain.Seedwork.Attribute;
using System.Threading;
using System.Threading.Tasks;

namespace SSS.Domain.CQRS.Student.Event.Handlers
{
    [DIService(ServiceLifetime.Scoped, typeof(INotificationHandler<StudentUpdateEvent>))]
    public class StudentUpdateEventHandler : INotificationHandler<StudentUpdateEvent>
    {
        private static ILogger _logger;

        public StudentUpdateEventHandler()
        {
            _logger = (ILogger)SSS.Infrastructure.Util.HttpContextService.Current.RequestServices.GetService(typeof(ILogger<StudentUpdateEventHandler>));
        }

        public Task Handle(StudentUpdateEvent noticen, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"StudentUpdateEventHandler {JsonConvert.SerializeObject(noticen)}");
            return Task.CompletedTask;
        }
    }
}
