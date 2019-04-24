using System;
using System.Collections.Generic;
using System.Text;

namespace SSS.Domain.Okex.Sdk.Spot
{
    public class SpotOrderLimit : SpotOrder
    {
        /// <summary>
        /// 价格
        /// </summary>
        public string price { get; set; }
    }
}
