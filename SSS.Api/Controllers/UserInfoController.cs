using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSS.Api.Seedwork;
using SSS.Application.UserInfo;
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
        [HttpPost("addtemplate")]
        [AllowAnonymous]  //匿名访问
        public IActionResult GetList([FromBody]UserInfoInputDto input)
        {
            _service.AddUserInfo(input);
            return Response(input);
        }
    }
}
