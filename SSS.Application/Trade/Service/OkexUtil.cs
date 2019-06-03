using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSS.Application.Trade.Service
{
    public class HttpInterceptor : DelegatingHandler
    {
        private string _apiKey;
        private string _passPhrase;
        private string _secret;
        private string _bodyStr;
        public HttpInterceptor(string apiKey, string secret, string passPhrase, string bodyStr)
        {
            this._apiKey = apiKey;
            this._passPhrase = passPhrase;
            this._secret = secret;
            this._bodyStr = bodyStr;
            InnerHandler = new HttpClientHandler();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var method = request.Method.Method;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("OK-ACCESS-KEY", this._apiKey);

            var now = DateTime.Now;
            var timeStamp = TimeZoneInfo.ConvertTimeToUtc(now).ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            var requestUrl = request.RequestUri.PathAndQuery;
            string sign = "";
            if (!String.IsNullOrEmpty(this._bodyStr))
            {
                sign = Encryptor.HmacSHA256($"{timeStamp}{method}{requestUrl}{this._bodyStr}", this._secret);
            }
            else
            {
                sign = Encryptor.HmacSHA256($"{timeStamp}{method}{requestUrl}", this._secret);
            }

            request.Headers.Add("OK-ACCESS-SIGN", sign);
            request.Headers.Add("OK-ACCESS-TIMESTAMP", timeStamp.ToString());
            request.Headers.Add("OK-ACCESS-PASSPHRASE", this._passPhrase);

            return base.SendAsync(request, cancellationToken);
        }
    }

    static class Encryptor
    {
        public static string HmacSHA256(string infoStr, string secret)
        {
            byte[] sha256Data = Encoding.UTF8.GetBytes(infoStr);
            byte[] secretData = Encoding.UTF8.GetBytes(secret);
            using (var hmacsha256 = new HMACSHA256(secretData))
            {
                byte[] buffer = hmacsha256.ComputeHash(sha256Data);
                return Convert.ToBase64String(buffer);
            }
        }

        public static string MakeSign(string apiKey, string secret, string phrase)
        {
            var timeStamp = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            var sign = Encryptor.HmacSHA256($"{timeStamp}GET/users/self/verify", secret);
            var info = new
            {
                op = "login",
                args = new List<string>()
                {
                    apiKey,phrase,timeStamp.ToString(),sign
                }
            };
            return JsonConvert.SerializeObject(info);
        }
    }
}
