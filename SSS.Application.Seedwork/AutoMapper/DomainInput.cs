using AutoMapper;
using SSS.Domain.CQRS.Student.Command.Commands;
using SSS.Domain.CQRS.Trade.Command.Commands;
using SSS.Domain.Student.Dto;
using SSS.Domain.Trade.Dto;

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

            CreateMap<TradeInputDto, TradeAddCommand>()
              .ConstructUsing(input => new TradeAddCommand(input));

            CreateMap<TradeInputDto, TradeUpdateCommand>()
            .ConstructUsing(input => new TradeUpdateCommand(input));


            CreateMap<TradeInputDto, TradeNullCommand>()
           .ConstructUsing(input => new TradeNullCommand(input));
        }
    }
}
