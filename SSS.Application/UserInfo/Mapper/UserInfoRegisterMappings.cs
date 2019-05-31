using AutoMapper;
using SSS.Application.UserInfo.AutoMapper;
using SSS.Domain.Seedwork.Attribute;

namespace SSS.Application.UserInfo.Mapper
{
    [RegisterMapper]
    public class UserInfoRegisterMappings
    {
        public static void RegisterMappings()
        {
            new MapperConfiguration(cfg =>
             {
                 cfg.AddProfile(new UserInfoMapper());
             });
        }
    }
}
