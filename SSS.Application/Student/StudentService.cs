using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using SSS.Domain.CQRS.Student.Command.Commands;
using SSS.Domain.Seedwork.Bus;
using SSS.Domain.Student.Dto;
using SSS.Infrastructure.Repository.Student;
using System.Collections.Generic;
using System.Linq;

namespace SSS.Application.Student
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;

        private readonly IStudentRepository _studentrepository;

        private readonly IStudentRepository _mongodbstudentrepository;

        private readonly Func<string, IStudentRepository> _serviceaccessor;

        public StudentService(IMapper mapper, IMediatorHandler bus, Func<string, IStudentRepository> serviceAccessor)
        {
            _mapper = mapper;
            _bus = bus;
            _serviceaccessor = serviceAccessor;
            _mongodbstudentrepository = _serviceaccessor("_mongodbstudentrepository");
            _studentrepository = _serviceaccessor("_studentrepository");
        }
        public StudentOutputDto AddStudent(StudentInputDto student) => throw new System.NotImplementedException();
        public bool DeleteStudent(StudentInputDto student) => throw new System.NotImplementedException();
        public StudentOutputDto GetByName(StudentInputDto student)
        {
            return _mapper.Map<StudentOutputDto>(_studentrepository.GetByName(student.name));
        }
        public List<StudentOutputDto> GetListStudent(StudentInputDto student)
        {
            var list1 = _mongodbstudentrepository.GetAll();

            var list = _studentrepository.GetAll(x => x.Age == 12);
            int count = 0;
            var page = _studentrepository.GetPage(1, 10, ref count);
            return _studentrepository.GetAll().ProjectTo<StudentOutputDto>(_mapper.ConfigurationProvider).ToList();
        }
        public void UpdateStudent(StudentInputDto student)
        {
            var cmd = _mapper.Map<StudentUpdateCommand>(student);
            _bus.SendCommand(cmd);
        }
    }
}