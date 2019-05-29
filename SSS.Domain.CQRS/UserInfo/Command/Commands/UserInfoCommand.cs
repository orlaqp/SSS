using System;

namespace SSS.Domain.CQRS.UserInfo.Command.Commands
{
    public abstract class UserInfoCommand : SSS.Domain.Seedwork.Command.Command
    {
        public Guid id { set; get; }
    }
}
