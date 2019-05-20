using SSS.Domain.Seedwork.Model;
using System;

namespace SSS.Domain.Trade
{
    public class Trade : Entity
    {
        public Trade(Guid id, string coin, double size, double price, int side,int Trade_Status, string Trade_No)
        {
            this.Id = id;
            this.Trade_Status = Trade_Status;
            this.Coin = coin;
            this.Size = size;
            this.Price = price;
            this.Side = side;
            this.Trade_No = Trade_No;
        }
        public string Coin { set; get; }

        public double Size { set; get; }

        public double Price { set; get; }

        public int Side { set; get; }

        public string Trade_No { set; get; }

        public int Trade_Status { set; get; }
    }
}
