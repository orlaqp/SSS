using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSS.Api.Seedwork;
using SSS.Application.UserInfo.Service;
using SSS.Domain.UserInfo.Dto;

namespace SSS.Api.Controllers
{
    /// <summary>
    /// UserInfoController
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserInfoController : ApiBaseController
    {
        private readonly IUserInfoService _service;

        /// <summary>
        /// UserInfoController
        /// </summary>
        /// <param name="UserInfo">IUserInfoService</param>
        public UserInfoController(IUserInfoService service)
        {
            _service = service;
        }

        /// <summary>
        /// AddUserInfo
        /// </summary>
        /// <param name="input">UserInfoInputDto</param>
        /// <returns></returns> 
        [HttpPost("add")]
        [AllowAnonymous]  //匿名访问
        public IActionResult AddUserInfo([FromBody]UserInfoInputDto input)
        {
            RecurringJob.AddOrUpdate(() => _service.AddUserInfo(input), Cron.MinuteInterval(1));
            return Response(input);
        }

        /// <summary>
        /// ListUserInfo
        /// </summary>
        /// <param name="input">UserInfoInputDto</param>
        /// <returns></returns> 
        [HttpGet("list")]
        [AllowAnonymous]  //匿名访问
        public IActionResult ListUser([FromQuery]UserInfoInputDto input)
        {
            object result = _service.GetListUser(input);
            return Response(result);
        }

        /// <summary>
        /// getbyphone
        /// </summary>
        /// <param name="input">UserInfoInputDto</param>
        /// <returns></returns> 
        [HttpGet("getbyphone")]
        [AllowAnonymous]  //匿名访问
        public IActionResult GetByPhone([FromQuery]UserInfoInputDto input)
        {
            object result = _service.GetByPhone(input);
            return Response(result);
        }
    }
}
