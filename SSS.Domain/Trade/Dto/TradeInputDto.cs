using System;

namespace SSS.Domain.Trade.Dto
{
    public class TradeInputDto
    {
        public Guid id { get; set; }

        public string coin { set; get; } 

        public double size { set; get; } 

        public double price { set; get; }

        public string side { set; get; }

        public int trade_status { set; get; }

        public string trade_no { set; get; }

        public int pageindex { set; get; }

        public int pagesize { set; get; }

        public string order_by { set; get; } 
    }
}
