using SSS.Domain.UserInfo.Dto;

namespace SSS.Application.UserInfo
{
    public interface IUserInfoService
    {
        void AddUserInfo(UserInfoInputDto input);
    }
}