using SSS.Domain.Seedwork.Model;
using System;

namespace SSS.Domain.Okex.Target
{
    public class Macd : Entity
    {
        public string instrument { set; get; }
        public DateTime time { set; get; }
        public DateTime createtime { set; get; }

        public double ema12 { set; get; }
        public double ema26 { set; get; }

        public double dif { set; get; }
        public double dea { set; get; }
        public double macd { set; get; }
    }
}
