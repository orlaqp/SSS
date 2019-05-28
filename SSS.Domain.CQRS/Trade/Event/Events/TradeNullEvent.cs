namespace SSS.Domain.CQRS.Trade.Event.Events
{
    public class TradeNullEvent : Seedwork.Events.Event
    {
        public double price { set; get; }

        public TradeNullEvent(Domain.Trade.Trade trade)
        {
            this.price = (double)trade.Last_Price;
        }
    }
}
