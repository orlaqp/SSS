using System.Collections.Generic;
using System.Linq;
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
        public StudentOutputDto GetByName(StudentInputDto student)
        {
            return _mapper.Map<StudentOutputDto>(_studentrepository.GetByName(student.name));
        }
        public List<StudentOutputDto> GetListStudent(StudentInputDto student)
        {
            return _studentrepository.GetAll().ProjectTo<StudentOutputDto>(_mapper.ConfigurationProvider).ToList();
        }
        public bool UpdateStudent(StudentInputDto student) => throw new System.NotImplementedException();
    }
}