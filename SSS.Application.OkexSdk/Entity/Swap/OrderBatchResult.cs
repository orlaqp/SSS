﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SSS.Application.OkexSdk.Swap
{
    public class OrderBatchResult
    {
        /// <summary>
        /// 调用接口返回结果
        /// </summary>
        public bool result { get; set; }
        /// <summary>
        /// 订单信息
        /// </summary>
        public List<OrderBatchResultDetail> order_info { get; set; }
    }
}