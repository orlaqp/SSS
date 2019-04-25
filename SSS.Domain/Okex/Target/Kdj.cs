using SSS.Domain.Seedwork.Model;
using System;

namespace SSS.Domain.Okex.Target
{
    public class Kdj : Entity
    {
        public string instrument { set; get; }
        public int timetype { set; get; }
        public DateTime createtime { set; get; }
        public DateTime ktime { set; get; }

        public double k { set; get; }
        public double d { set; get; }
        public double j { set; get; }

        public double yesday_d { set; get; }
        public double yesday_k { set; get; }
    }
}
