using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSS.Api.Seedwork;
using SSS.Application.Trade.Service;
using SSS.Domain.Trade.Dto;
using SSS.Infrastructure.Util.Attribute;
using System;

namespace SSS.Api.Controllers
{
    /// <summary>
    /// TradeController
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TradeController : ApiBaseController
    {
        [Autowired]
        private readonly ITradeService _trade; 

        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="trade">TradeInputDto</param>
        /// <returns></returns> 
        [HttpGet("getlist")]
        [AllowAnonymous]  //匿名访问
        public IActionResult GetList([FromQuery]TradeInputDto trade)
        {
            var result = _trade.GetListTrade(trade);
            return Response(result);
        } 
    }
}