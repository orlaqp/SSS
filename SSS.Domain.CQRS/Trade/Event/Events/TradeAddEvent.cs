using System;

namespace SSS.Domain.CQRS.Trade.Event.Events
{
    public class TradeAddEvent : Seedwork.Events.Event
    {
        public Guid id { set; get; }

        public string coin { set; get; } 

        public TradeAddEvent(Domain.Trade.Trade trade)
        {
            this.id = trade.Id;
            this.coin = trade.Coin;
        }
    }
}
