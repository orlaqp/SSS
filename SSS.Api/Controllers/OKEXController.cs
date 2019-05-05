using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SSS.Api.Seedwork;

namespace SSS.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class OkexController : ApiBaseController
    {
        private readonly ILogger _logger;

        public OkexController(ILogger<OkexController> logger)
        {
            _logger = logger;
        }

        [HttpGet("okex")]
        public IActionResult Index()
        {
            return Response("okex");
        }

    }
}