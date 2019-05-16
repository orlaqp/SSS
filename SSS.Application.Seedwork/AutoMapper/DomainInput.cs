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
               .ConstructUsing(input => new StudentUpdateCommand(input));

            CreateMap<StudentInputDto, StudentAddCommand>()
                .ConstructUsing(input => new StudentAddCommand(input));
        }
    }
}
