using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SSS.Domain.Seedwork.Attribute;
using SSS.Infrastructure.Seedwork.DbContext;
using SSS.Infrastructure.Seedwork.Repository;
using System.Linq;

namespace SSS.Infrastructure.Repository.UserInfo
{
    [DIService(ServiceLifetime.Scoped, typeof(IUserInfoRepository))]
    public class UserInfoRepository : Repository<SSS.Domain.UserInfo.UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(DbcontextBase context) : base(context)
        {
        }

        public Domain.UserInfo.UserInfo GetUserInfoByPhone(string phone)
        {
            return DbSet.AsNoTracking().FirstOrDefault(x => x.Phone.Equals(phone));
        }
    }
}