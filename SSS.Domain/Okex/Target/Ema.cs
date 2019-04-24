using SSS.Domain.Seedwork.Model;
using System;

namespace SSS.Domain.Okex.Target
{
    public class Ema : Entity
    {
        public string instrument { set; get; }
        public DateTime createtime { set; get; }

        public double ema { set; get; }
        public int time { set; get; }
    }
}
