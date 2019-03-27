using System.Collections.Generic;
using SSS.Domain.Core.Student;

namespace SSS.Application.Student
{
    public interface IStudentService
    {
        StudentOutputDto AddStudent(StudentInputDto student);

        bool UpdateStudent(StudentInputDto student);

        List<StudentOutputDto> GetListStudent(StudentInputDto student);

        bool DeleteStudent(StudentInputDto student);

        StudentOutputDto GetByName(StudentInputDto student);
    }
}