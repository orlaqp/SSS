using System;

namespace SSS.Domain.CQRS.Trade.Event.Events
{
    public class TradeAddEvent : Seedwork.Events.Event
    {
        public Guid id { set; get; }

        public string coin { set; get; }

        public string trade_no { set; get; }

        public int trade_status { set; get; }

        public double size { set; get; }

        public double price { set; get; }

        public int side { set; get; }

        public TradeAddEvent(Domain.Trade.Trade trade)
        {
            this.id = trade.Id;
            this.coin = trade.Coin;
            this.trade_no = trade.Trade_No;
            this.trade_status = trade.Trade_Status;
            this.side = trade.Side;
            this.size = trade.Size;
            this.price = trade.Price;
        }
    }
}
