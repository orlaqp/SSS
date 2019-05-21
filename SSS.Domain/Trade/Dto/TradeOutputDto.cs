using System;

namespace SSS.Domain.Trade.Dto
{
    public class TradeOutputDto
    {
        public Guid id { get; set; }

        public string coin { set; get; }

        public double size { set; get; }

        public double last_price { set; get; }
        public double first_price { set; get; }

        public string side { set; get; }

        public int first_trade_status { set; get; }

        public int last_trade_status { set; get; }

        public string first_trade_no { set; get; }

        public string last_trade_no { set; get; }
    }
}
