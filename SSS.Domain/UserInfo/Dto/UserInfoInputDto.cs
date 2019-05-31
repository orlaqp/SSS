using SSS.Domain.Seedwork.Model;

namespace SSS.Domain.UserInfo.Dto
{
    public class UserInfoInputDto : InputDtoBase
    {
        public string phone { set; get; }

        public int uid { set; get; }

        public string password { set; get; }

        public int firstid { set; get; }
    }
}
