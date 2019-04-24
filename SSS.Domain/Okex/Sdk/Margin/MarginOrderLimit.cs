using System;
using System.Collections.Generic;
using System.Text;

namespace SSS.Domain.Okex.Sdk.Margin
{
    public class MarginOrderLimit : MarginOrder
    {
        /// <summary>
        /// 价格
        /// </summary>
        public string price { get; set; }
    }
}
