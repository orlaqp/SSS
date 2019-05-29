using AutoMapper;
using SSS.Application.UserInfo.AutoMapper;

namespace SSS.Application.UserInfo.Mapper
{
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
