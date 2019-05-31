using System;

namespace SSS.Domain.CQRS.Trade.Event.Events
{
    public class TradeAddEvent : Seedwork.Events.Event
    {
        public string id { set; get; }

        public string coin { set; get; }

        public string trade_no { set; get; }

        public int trade_status { set; get; }

        public double size { set; get; }

        public double price { set; get; }

        public string side { set; get; }

        public DateTime time { set; get; }

        public int ktime { set; get; }

        public TradeAddEvent(Domain.Trade.Trade trade)
        {
            this.id = trade.Id;
            this.ktime = trade.KTime;
            this.coin = trade.Coin;
            this.trade_no = trade.First_Trade_No;
            this.trade_status = trade.First_Trade_Status;
            this.side = trade.Side;
            this.size = trade.Size;
            this.price = trade.First_Price;
            this.time = trade.First_Time;
        }
    }
}
