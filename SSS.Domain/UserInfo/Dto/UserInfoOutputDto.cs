using SSS.Domain.Seedwork.Model;

namespace SSS.Domain.UserInfo.Dto
{
    public class UserInfoOutputDto : OutputDtoBase
    {
        public string img { set; get; }

        public string qrcode { set; get; }

        public string phone { set; get; }

        public string uid { set; get; }

        public decimal commission { set; get; }

        public decimal earning { set; get; }
    }
}
