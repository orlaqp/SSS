using Microsoft.AspNetCore.Mvc;
using SSS.Application.Student;
using SSS.Domain.Core.Student;

namespace SSS.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _student;

        public StudentController(IStudentService student)
        {
            _student = student;
        }

        [HttpPost("getbyname")]
        public StudentOutputDto GetByName([FromBody]StudentInputDto student)
        {
            return _student.GetByName(student);
        }
    }
}
