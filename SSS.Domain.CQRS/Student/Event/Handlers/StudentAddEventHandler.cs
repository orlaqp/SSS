using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SSS.Api.Seedwork;
using SSS.Domain.CQRS.Student.Event.Events;

namespace SSS.Domain.CQRS.Student.Event.Handlers
{
    public class StudentAddEventHandler : INotificationHandler<StudentAddEvent>
    {
        private static ILogger _logger;

        public StudentAddEventHandler()
        {
            _logger = (ILogger)HttpContextService.Current.RequestServices.GetService(typeof(ILogger<ApiBaseController>));
        }

        public Task Handle(StudentAddEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"StudentAddEventHandler {JsonConvert.SerializeObject(notification)}");
            return Task.CompletedTask;
        }
    }
}
