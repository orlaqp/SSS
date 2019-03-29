using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSS.Api.Seedwork;
using SSS.Application.Student;
using SSS.Domain.Student.Dto;

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
            var result = _student.GetByName(student);
            return Response(result);
        }

        [HttpGet("getlist")]
        public IActionResult GetList([FromQuery]StudentInputDto student)
        {
            var result = _student.GetListStudent(student);
            return Response(result);
        }

        [HttpPost("updateage")]
        [AllowAnonymous]  //匿名访问
        public IActionResult UpdateAge([FromBody]StudentInputDto student)
        {
            _student.UpdateStudent(student);
            return Response(student);
        }
    }
}
