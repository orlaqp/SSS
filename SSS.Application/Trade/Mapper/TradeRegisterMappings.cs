using AutoMapper;
using SSS.Application.Seedwork.AutoMapper;

namespace SSS.Application.Student.Mapper
{
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
