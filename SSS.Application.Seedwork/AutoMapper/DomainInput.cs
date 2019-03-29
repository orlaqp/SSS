using AutoMapper;
using SSS.Domain.Core.Student;
using SSS.Domain.CQRS.Student.Commands;

namespace SSS.Application.Seedwork.AutoMapper
{
    public class DomainInput : Profile
    {
        public DomainInput()
        {
            CreateMap<StudentInputDto, StudentUpdateCommand>()
               .ConstructUsing(c => new StudentUpdateCommand(c.id, c.name, c.age));
        }
    }
}
