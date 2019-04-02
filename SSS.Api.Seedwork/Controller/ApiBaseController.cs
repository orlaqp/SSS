using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SSS.Domain.Seedwork.Bus;
using SSS.Domain.Seedwork.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace SSS.Api.Seedwork
{
    public abstract class ApiBaseController : ControllerBase
    {
        private static ILogger _logger;
        private static DomainNotificationHandler _notifications;
        private static IMediatorHandler _mediator;

        protected IEnumerable<DomainNotification> Notifications
        {
            get => _notifications.GetNotifications();
        }

        protected bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }

        protected IActionResult Response(object data, bool status = true, string message = "", int code = 200)
        {
            _logger = (ILogger)HttpContextService.Current.RequestServices.GetService(typeof(ILogger<ApiBaseController>));
            _notifications = (DomainNotificationHandler)HttpContextService.Current.RequestServices.GetService(typeof(INotificationHandler<DomainNotification>));
            _mediator = (IMediatorHandler)HttpContextService.Current.RequestServices.GetService(typeof(IMediatorHandler));

            if (IsValidOperation())
            {
                if (data == null)
                    return NotFound(new { status = false, data = "", message = "数据为空", code = 204 });
                return Ok(new { status, data, message, code });
            }
            else
                return BadRequest(new { status = false, data = "", message = Notifications.Select(n => n.Value), code = 400 });
            //return Content("无效");
        }
    }
}
