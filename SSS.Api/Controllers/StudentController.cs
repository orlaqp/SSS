using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSS.Api.Seedwork;
using SSS.Application.Student;
using SSS.Domain.Student.Dto;
using SSS.Infrastructure.Seedwork.Cache.Redis;

namespace SSS.Api.Controllers
{
    /// <summary>
    /// StudentController
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class StudentController : ApiBaseController
    {
        private readonly IStudentService _student;

        private readonly RedisCache _redis;

        /// <summary>
        /// StudentController
        /// </summary>
        /// <param name="student">IStudentService</param>
        public StudentController(IStudentService student, RedisCache redis)
        {
            _student = student;
            _redis = redis;
        }

        /// <summary>
        /// GetByName
        /// </summary>
        /// <param name="student">StudentInputDto</param>
        /// <returns></returns> 
        [HttpGet("getbyname")]
        [AllowAnonymous]  //匿名访问
        public IActionResult GetByName([FromQuery]StudentInputDto student)
        {
            _redis.Set("abc","123");
            var result = _student.GetByName(student);
            return Response(result);
        }

        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="student">StudentInputDto</param>
        /// <returns></returns> 
        [HttpGet("getlist")]
        [AllowAnonymous]  //匿名访问
        public IActionResult GetList([FromQuery]StudentInputDto student)
        {
            var result = _student.GetListStudent(student);
            return Response(result);
        }

        /// <summary>
        /// UpdateAge
        /// </summary>
        /// <param name="student">StudentInputDto</param>
        /// <returns></returns> 
        [HttpPost("updateage")]
        [AllowAnonymous]  //匿名访问
        public IActionResult UpdateAge([FromBody]StudentInputDto student)
        {
            _student.UpdateStudent(student);
            return Response(student);
        }
    }
}
