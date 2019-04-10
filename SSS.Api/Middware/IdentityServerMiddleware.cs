using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using IdentityModel.Client;

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
            if (!context.User.Identity.IsAuthenticated) // string.IsNullOrWhiteSpace(context.Request.Headers["Authorization"])
            {
                var client = new HttpClient();

                //发现 IdentityServer 各个终结点（EndPoint）的客户端库
                var disco = client.GetDiscoveryDocumentAsync("http://localhost:456").Result;

                //从 IdentityServer 元数据获取到的Token终结点请求令牌
                var tokenResponse = client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "client1",
                    ClientSecret = "secret",
                    Scope = "api_test1"
                }).Result;

                context.Request.Headers.Add("Authorization", "Bearer " + tokenResponse.AccessToken);
            }
            else
                foreach (var item in context.User.Claims)
                    _logger.LogInformation("Claims :【" + item.Issuer + "】" + "【" + item.Type + "】" + "  【" + item.Value + "】");

            await _next.Invoke(context);
        }
    }
}
