using AutoMapper;
using SSS.Domain.CQRS.UserInfo.Command.Commands;
using SSS.Domain.UserInfo.Dto;

namespace SSS.Application.UserInfo.AutoMapper
{
    public class UserInfoMapper : Profile
    {
        public UserInfoMapper()
        {
            CreateMap<SSS.Domain.UserInfo.UserInfo, UserInfoOutputDto>();

            CreateMap<UserInfoInputDto, UserInfoAddCommand>()
                .ConstructUsing(input => new UserInfoAddCommand(input));
        }
    }
}
