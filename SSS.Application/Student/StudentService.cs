using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using SSS.Domain.Core.Student;
using SSS.Domain.Repository.Student;

namespace SSS.Application.Student
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;

        private readonly IStudentRepository _studentrepository;
        public StudentService(IMapper mapper, IStudentRepository studentrepository)
        {
            _mapper = mapper;
            _studentrepository = studentrepository;
        }
        public StudentOutputDto AddStudent(StudentInputDto student) => throw new System.NotImplementedException();
        public bool DeleteStudent(StudentInputDto student) => throw new System.NotImplementedException();

        public List<StudentOutputDto> GetListStudent(StudentInputDto student)
        {
            var list = _studentrepository.GetAll().ProjectTo<StudentOutputDto>(_mapper.ConfigurationProvider);
            return null;
        }
        public bool UpdateStudent(StudentInputDto student) => throw new System.NotImplementedException();
    }
}