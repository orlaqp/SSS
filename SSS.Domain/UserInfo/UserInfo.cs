using SSS.Domain.Seedwork.Model;

namespace SSS.Domain.UserInfo
{
    public class UserInfo : Entity
    {
        public UserInfo(string id, int uid, string phone, string PassWord, int? FirstId)
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
        public int Uid { set; get; }

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
        public int? FirstId { set; get; }

        /// <summary>
        /// ����ID
        /// </summary>
        public int? SecondId { set; get; }

        /// <summary>
        /// ����ID
        /// </summary>
        public int? ThirdId { set; get; }

    }
}
