using SSS.Domain.Seedwork.Model;
using System;

namespace SSS.Domain.UserInfo
{
    public class UserInfo : Entity
    {
        public UserInfo(Guid id)
        {
            this.Id = id;
        }
    }
}
