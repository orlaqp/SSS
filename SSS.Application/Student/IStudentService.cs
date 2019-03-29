using System.Collections.Generic;
using SSS.Domain.Student.Dto;

namespace SSS.Application.Student
{
    public interface IStudentService
    {
        StudentOutputDto AddStudent(StudentInputDto student);

        void UpdateStudent(StudentInputDto student);

        List<StudentOutputDto> GetListStudent(StudentInputDto student);

        bool DeleteStudent(StudentInputDto student);

        StudentOutputDto GetByName(StudentInputDto student);
    }
}