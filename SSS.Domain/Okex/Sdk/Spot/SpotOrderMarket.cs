﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SSS.Domain.Okex.Sdk.Spot
{
    public class SpotOrderMarket : SpotOrder
    {
        /// <summary>
        /// 买入金额，市价买入是必填notional
        /// </summary>
        public string notional { get; set; }
    }
}
