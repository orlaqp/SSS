using AutoMapper;

namespace SSS.Application.Seedwork.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainInput());
                cfg.AddProfile(new DomainOutput());
            });
        }
    }
}
