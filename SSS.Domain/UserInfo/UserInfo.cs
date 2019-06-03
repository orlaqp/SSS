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
        /// ����
        /// </summary>
        public string PassWord { set; get; }

        /// <summary>
        /// ͷ��
        /// </summary>
        public string Img { set; get; }

        /// <summary>
        /// ��ά��
        /// </summary>
        public string QrCode { set; get; }

        /// <summary>
        /// �ֻ���
        /// </summary>
        public string Phone { set; get; }

        /// <summary>
        /// ������
        /// </summary>
        public string Uid { set; get; }

        /// <summary>
        /// Ӷ��
        /// </summary>
        public decimal Commission { set; get; }

        /// <summary>
        /// �ۼ�����
        /// </summary>
        public decimal Earning { set; get; }

        /// <summary>
        /// һ��ID
        /// </summary>
        public string FirstId { set; get; }

        /// <summary>
        /// ����ID
        /// </summary>
        public string SecondId { set; get; }

        /// <summary>
        /// ����ID
        /// </summary>
        public string ThirdId { set; get; }

    }
}
