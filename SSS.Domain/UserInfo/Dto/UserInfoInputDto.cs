using System;

namespace SSS.Domain.UserInfo.Dto
{
    public class UserInfoInputDto
    {
        public Guid id { get; set; } 

        public int pageindex { set; get; }

        public int pagesize { set; get; }

        public string order_by { set; get; }
    }
}
