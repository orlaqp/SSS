using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SSS.Api.Seedwork;
using System.Collections.Generic;
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
            string filepath = _env.ContentRootPath + "\\codegenerator.html";

            using (StreamReader sr = new StreamReader(filepath))
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

        [HttpPost("createcode")]
        public IActionResult CreateCode()
        {
            var class_name = HttpContext.Request.Form["class_name"];
            var fields = HttpContext.Request.Form["fields"];
            var list = JsonConvert.DeserializeObject<List<Field>>(fields);
            return Response(null);
        }
    }

    public class Field
    {
        public string field_name { set; get; }
        public string field_type { set; get; }
    }
}