using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SSS.Api.Seedwork;
using SSS.Domain.CQRS.Student.Event.Events; 

namespace SSS.Domain.CQRS.Student.Event.Handlers
{
    public class StudentEventHandler : INotificationHandler<StudentUpdateEvent>
    {
        private static ILogger _logger;

        public StudentEventHandler()
        {
            _logger = (ILogger)HttpContextService.Current.RequestServices.GetService(typeof(ILogger<ApiBaseController>));
        }

        public Task Handle(StudentUpdateEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("This is StudentEventHandler");
            return Task.CompletedTask;
        }
    }
}
