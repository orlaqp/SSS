using AutoMapper;
using SSS.Application.Seedwork.AutoMapper;
using SSS.Domain.Seedwork.Attribute;

namespace SSS.Application.Student.Mapper
{
    [RegisterMapper]
    public class TradeRegisterMappings
    {
        public static void RegisterMappings()
        {
            new MapperConfiguration(cfg =>
             {
                 cfg.AddProfile(new TradeMapper());
             });
        }
    }
}
