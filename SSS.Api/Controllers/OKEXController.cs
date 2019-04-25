using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SSS.Api.Seedwork;
using SSS.Application.Okex.Trading;

namespace SSS.Api.Controllers
{ 
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class OkexController : ApiBaseController
    {
        private readonly ITradingService _tradingservice;
        private readonly ILogger _logger;

        public OkexController(ITradingService tradingservice,ILogger<OkexController> logger)
        {
            _tradingservice = tradingservice;
            _logger = logger;
        }

        [HttpGet("okex")]
        public IActionResult Index()
        {
            _tradingservice.AddOrder();
            return Response("okex");
        }

    }
}