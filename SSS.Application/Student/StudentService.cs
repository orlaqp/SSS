using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions; 
using SSS.Domain.CQRS.Student.Commands; 
using SSS.Domain.Seedwork.Bus;
using SSS.Domain.Student.Dto;
using SSS.Infrastructure.Student.Repository;

namespace SSS.Application.Student
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;

        private readonly IStudentRepository _studentrepository;
        public StudentService(IMapper mapper, IMediatorHandler bus, IStudentRepository studentrepository)
        {
            _mapper = mapper;
            _bus = bus;
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
        public void UpdateStudent(StudentInputDto student)
        {
            var cmd = _mapper.Map<StudentUpdateCommand>(student);
            _bus.SendCommand(cmd); 
        }
    }
}