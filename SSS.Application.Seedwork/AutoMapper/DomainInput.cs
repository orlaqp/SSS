using AutoMapper;
using SSS.Domain.CQRS.Student.Command.Commands; 
using SSS.Domain.Student.Dto;

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
