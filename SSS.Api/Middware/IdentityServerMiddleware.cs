using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace SSS.Api.Middware
{
    public class IdentityServerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public IdentityServerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<IdentityServerMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            await _next.Invoke(context);
        }
    }
}
