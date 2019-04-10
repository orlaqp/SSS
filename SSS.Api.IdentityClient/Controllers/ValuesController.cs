using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace SSS.Api.IdentityClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //客户端授权模式登陆
        [HttpGet("client")]
        public string Client()
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

            //
            client.SetBearerToken(tokenResponse.AccessToken);
            var respone = client.GetAsync("http://localhost:1234/api/v1/student/getlist").Result;

            return respone.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// 密码授权模式登陆
        /// </summary>
        /// <returns></returns>
        [HttpGet("password")]
        public string Password()
        {
            var client = new HttpClient();

            //发现 IdentityServer 各个终结点（EndPoint）的客户端库
            var disco = client.GetDiscoveryDocumentAsync("http://localhost:456").Result;

            //从 IdentityServer 元数据获取到的Token终结点请求令牌
            var tokenResponse = client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client2",
                ClientSecret = "secret",
                Scope = "api_test1",

                UserName = "wbs",
                Password = "123456"
            }).Result;

            //设置Token
            client.SetBearerToken(tokenResponse.AccessToken);
            var respone = client.GetAsync("http://localhost:1234/api/v1/student/getlist").Result;

            return respone.Content.ReadAsStringAsync().Result;
        }
    }
}
