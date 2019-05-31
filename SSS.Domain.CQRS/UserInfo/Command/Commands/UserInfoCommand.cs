using System;

namespace SSS.Domain.CQRS.UserInfo.Command.Commands
{
    public abstract class UserInfoCommand : SSS.Domain.Seedwork.Command.Command
    {
        public string id { set; get; }

        public string phone { set; get; }

        public int uid { set; get; }

        public string password { set; get; }

        public int firstid { set; get; }
         
        public int secondid { set; get; }
         
        public int thirdid { set; get; }
    }
}
