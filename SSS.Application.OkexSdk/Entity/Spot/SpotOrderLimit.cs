using System;
using System.Collections.Generic;
using System.Text;

namespace SSS.Application.OkexSdk.Spot
{
    public class SpotOrderLimit : SpotOrder
    {
        /// <summary>
        /// 价格
        /// </summary>
        public string price { get; set; }
    }
}
