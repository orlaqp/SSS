using AutoMapper;
using SSS.Domain.Student;
using SSS.Domain.Student.Dto;

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
