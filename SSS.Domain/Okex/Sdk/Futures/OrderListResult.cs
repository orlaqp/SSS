using System;
using System.Collections.Generic;
using System.Text;

namespace SSS.Domain.Okex.Sdk.Futures
{
    public class OrderListResult
    {
        public bool result { get; set; }
        public List<Order> order_info { get; set; }
    }
}
