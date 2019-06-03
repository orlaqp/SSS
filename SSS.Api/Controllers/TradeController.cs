using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSS.Api.Seedwork;
using SSS.Application.Trade.Service;
using SSS.Domain.Trade.Dto;

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
        private readonly ITradeService _trade;

        public TradeController(ITradeService trade)
        {
            _trade = trade;
        }

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

        /// <summary>
        /// AddTrade
        /// </summary>
        /// <param name="trade">TradeInputDto</param>
        /// <returns></returns> 
        [HttpPost("addtrade")]
        [AllowAnonymous]  //匿名访问
        public IActionResult AddTrade([FromBody]TradeInputDto trade)
        {
            _trade.OperateTrade(trade);
            return Response(trade);
        }
    }
}