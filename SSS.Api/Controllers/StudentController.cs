using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSS.Api.Seedwork;
using SSS.Application.Student;
using SSS.Domain.Core.Student;
using System;

namespace SSS.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class StudentController : ApiBaseController
    {
        private readonly IStudentService _student;

        public StudentController(IStudentService student)
        {
            _student = student;
        }

        [HttpGet("getbyname")]
        [AllowAnonymous]  //匿名访问
        public IActionResult GetByName([FromQuery]StudentInputDto student)
        {
            //Convert.ToInt32("abc");
            var result = _student.GetByName(student);
            return Response(result);
        }

        [HttpGet("getlist")]
        public IActionResult GetList([FromQuery]StudentInputDto student)
        {
            var result = _student.GetListStudent(student);
            if (result != null)
                return Ok(new { success = true, data = result, code = 200 });
            return Ok(new { success = false, data = "", code = 400, message = "数据为空" });
        }
    }
}
