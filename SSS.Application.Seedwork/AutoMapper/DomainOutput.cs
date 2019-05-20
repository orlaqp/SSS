using AutoMapper;
using SSS.Domain.Student;
using SSS.Domain.Student.Dto;
using SSS.Domain.Trade;
using SSS.Domain.Trade.Dto;

namespace SSS.Application.Seedwork.AutoMapper
{
    public class DomainOutput : Profile
    {
        public DomainOutput()
        {
            CreateMap<Student, StudentOutputDto>();

            CreateMap<Trade, TradeOutputDto>();
        }
    }
}
