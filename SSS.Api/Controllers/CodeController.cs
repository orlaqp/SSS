using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SSS.Api.Seedwork;
using System.IO;
using System.Net;

namespace SSS.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CodeController : ApiBaseController
    {
        private IHostingEnvironment _env;

        public CodeController(IHostingEnvironment env)
        {
            _env = env;
        }

        [HttpGet("index")]
        public ContentResult Index()
        {
            string html = ""; 
            string filepath = _env.ContentRootPath+ "\\codegenerator.html";

            using (StreamReader sr=new StreamReader(filepath))
            {
                html = sr.ReadToEnd();
            }

            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = html
            };
        }
    }
}