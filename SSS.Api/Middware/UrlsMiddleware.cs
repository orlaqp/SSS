using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace SSS.Api.Middware
{
    public class UrlsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public UrlsMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<UrlsMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.Equals("/code"))
            {
                context.Request.Path = "/api/v1/code/index";
            }
            await _next.Invoke(context);
        }
    }
}
