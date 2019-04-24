﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SSS.Domain.Okex.Sdk.Spot
{
    public class SpotLedgerDetail
    {
        /// <summary>
        /// 交易的ID
        /// </summary>
        public long order_id { get; set; }
        /// <summary>
        /// 币对
        /// </summary>
        public string instrument_id { get; set; }
    }
}
