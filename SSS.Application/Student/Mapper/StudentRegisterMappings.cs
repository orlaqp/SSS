using AutoMapper;
using SSS.Application.Seedwork.AutoMapper;

namespace SSS.Application.Student.Mapper
{
    public class StudentRegisterMappings
    {
        public static void RegisterMappings()
        {
            new MapperConfiguration(cfg =>
             {
                 cfg.AddProfile(new StudentMapper());
             });
        }
    }
}
