using System;

namespace SSS.Domain.CQRS.Trade.Event.Events
{
    public class TradeUpdateEvent : Seedwork.Events.Event
    {
        public Guid id { set; get; }

        public string coin { set; get; }

        public string trade_no { set; get; }

        public int trade_status { set; get; }

        public double size { set; get; }

        public double price { set; get; }

        public string side { set; get; }

        public DateTime time { set; get; }

        public TradeUpdateEvent(Domain.Trade.Trade trade)
        {
            this.id = trade.Id;
            this.coin = trade.Coin;
            this.trade_no = trade.Last_Trade_No;
            this.trade_status =(int) trade.Last_Trade_Status;
            this.side = trade.Side;
            this.size = trade.Size;
            this.price = (double)trade.Last_Price;
            this.time = (DateTime)trade.Last_Time;
        }
    }
}
