﻿using SSS.Domain.CQRS.Student.Command.Commands;
using SSS.Domain.Student.Dto;

namespace SSS.Application.Seedwork.Profile
{
    public class StudentProfile : AutoMapper.Profile
    {
        public StudentProfile()
        {
            CreateMap<SSS.Domain.Student.Student, StudentOutputDto>();

            CreateMap<StudentInputDto, StudentUpdateCommand>()
               .ConstructUsing(input => new StudentUpdateCommand(input));

            CreateMap<StudentInputDto, StudentAddCommand>()
                .ConstructUsing(input => new StudentAddCommand(input));
        }
    }
}
