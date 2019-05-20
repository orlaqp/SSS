using System;

namespace SSS.Domain.Trade.Dto
{
    public class TradeOutputDto
    {
        public Guid id { get; set; }

        public string coin { set; get; }

        public double size { set; get; }

        public double price { set; get; }

        public double side { set; get; }

        public int trade_status { set; get; }

        public string trade_no { set; get; }
    }
}
