using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace SSS.Api.Seedwork
{
    public abstract class ApiBaseController : ControllerBase
    {
        private static ILogger _logger;

        public new IActionResult Response(object data, bool status = true, string message = "", int code = 200)
        {
            _logger = (ILogger)HttpContextService.Current.RequestServices.GetService(typeof(ILogger<ApiBaseController>));

            if (status)
                return Ok(new { status, data, message, code });
            if (status && data == null)
                return NotFound(new { status = false, data, message = "数据为空", code = 204 });
            if (!status)
                return BadRequest(new { status = false, data, message, code = 404 });
            return Content("无效");
        } 
    }
}
