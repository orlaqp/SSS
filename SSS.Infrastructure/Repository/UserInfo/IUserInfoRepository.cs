using SSS.Domain.Seedwork.Repository;

namespace SSS.Infrastructure.Repository.UserInfo
{
    public interface IUserInfoRepository : IRepository<SSS.Domain.UserInfo.UserInfo>
    {
        Domain.UserInfo.UserInfo GetUserInfoByPhone(string phone);
        Domain.UserInfo.UserInfo GetUserInfoByUid(string uid);
    }
}