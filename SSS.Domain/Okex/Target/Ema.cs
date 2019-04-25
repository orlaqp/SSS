using SSS.Domain.Seedwork.Model;
using System;

namespace SSS.Domain.Okex.Target
{
    public class Ema : Entity
    {
        public string instrument { set; get; }
        public DateTime createtime { set; get; }
        public DateTime ktime { set; get; }
        public double yesday_ema { set; get; }
        public double now_ema { set; get; }
        public int timetype { set; get; }
        /// <summary>
        /// 默认参数为12、26
        /// </summary>
        public int parameter { set; get; }
    }
}
