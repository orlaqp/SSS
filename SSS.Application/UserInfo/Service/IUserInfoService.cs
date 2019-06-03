using SSS.Domain.UserInfo.Dto;

namespace SSS.Application.UserInfo.Service
{
    public interface IUserInfoService
    {
        void AddUserInfo(UserInfoInputDto input);
    }
}