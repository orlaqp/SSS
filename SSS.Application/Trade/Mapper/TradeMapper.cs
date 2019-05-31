using AutoMapper;
using SSS.Domain.CQRS.Trade.Command.Commands;
using SSS.Domain.Trade.Dto;

namespace SSS.Application.Seedwork.AutoMapper
{
    public class TradeMapper : Profile
    {
        public TradeMapper()
        {
            CreateMap<Domain.Trade.Trade, TradeOutputDto>();

            CreateMap<TradeInputDto, TradeAddCommand>()
              .ConstructUsing(input => new TradeAddCommand(input));

            CreateMap<TradeInputDto, TradeUpdateCommand>()
            .ConstructUsing(input => new TradeUpdateCommand(input));

            CreateMap<TradeInputDto, TradeNullCommand>()
            .ConstructUsing(input => new TradeNullCommand(input));
        }
    }
}
