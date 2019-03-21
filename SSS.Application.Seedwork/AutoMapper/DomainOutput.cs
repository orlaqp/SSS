using AutoMapper;
using SSS.Domain.Core.Student;

namespace SSS.Application.Seedwork.AutoMapper
{
    public class DomainOutput : Profile
    {
        public DomainOutput()
        {
            CreateMap<Student, StudentOutputDto>();
        }
    }
}
