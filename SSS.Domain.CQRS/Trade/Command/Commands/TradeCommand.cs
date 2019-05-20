using System;

namespace SSS.Domain.CQRS.Trade.Command.Commands
{
    public abstract class TradeCommand : SSS.Domain.Seedwork.Command.Command
    {
        public Guid id { set; get; }

        public string coin { get; set; }

        public double size { set; get; }

        public double price { set; get; }

        public int side { set; get; }

        public string trade_no { set; get; }

        public int trade_status { set; get; }
    }
}
