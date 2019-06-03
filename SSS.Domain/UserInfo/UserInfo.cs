using SSS.Domain.Seedwork.Model;

namespace SSS.Domain.UserInfo
{
    public class UserInfo : Entity
    {
        public UserInfo(string id, string uid, string phone, string PassWord, string FirstId)
        {
            this.Id = id;
            this.PassWord = PassWord;
            this.Uid = uid;
            this.Phone = phone;
            this.FirstId = FirstId;
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { set; get; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Img { set; get; }

        /// <summary>
        /// 二维码
        /// </summary>
        public string QrCode { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { set; get; }

        /// <summary>
        /// 邀请码
        /// </summary>
        public string Uid { set; get; }

        /// <summary>
        /// 佣金
        /// </summary>
        public decimal Commission { set; get; }

        /// <summary>
        /// 累计收益
        /// </summary>
        public decimal Earning { set; get; }

        /// <summary>
        /// 一级ID
        /// </summary>
        public string FirstId { set; get; }

        /// <summary>
        /// 二级ID
        /// </summary>
        public string SecondId { set; get; }

        /// <summary>
        /// 三级ID
        /// </summary>
        public string ThirdId { set; get; }

    }
}
