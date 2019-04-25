using System;
using SSS.Domain.Seedwork.Model;

namespace SSS.Domain.Okex.Target
{
    public class Ma : Entity
    {
        public string instrument { set; get; }
        public DateTime createtime { set; get; }
        public DateTime ktime { set; get; }
        public double now_ma { set; get; }
        public int timetype { set; get; }

        /// <summary>
        /// 1代表均量线 2代表均价线
        /// </summary>
        public int type { set; get; }

        /// <summary>
        /// 默认参数为5、10
        /// </summary>
        public int parameter { set; get; }
    }
}
