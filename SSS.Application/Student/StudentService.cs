using System.Collections.Generic;
using SSS.Domain.Core.Student;

namespace SSS.Application.Student
{
    public class StudentService : IStudentService
    {
        public StudentOutputDto AddStudent(StudentInputDto student) => throw new System.NotImplementedException();
        public bool DeleteStudent(StudentInputDto student) => throw new System.NotImplementedException();
        public List<StudentOutputDto> GetListStudent(StudentInputDto student) => throw new System.NotImplementedException();
        public bool UpdateStudent(StudentInputDto student) => throw new System.NotImplementedException();
    }
}