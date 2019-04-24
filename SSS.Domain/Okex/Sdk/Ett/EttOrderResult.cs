﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SSS.Domain.Okex.Sdk.Ett
{
    public class EttOrderResult
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public long order_id { get; set; }
        /// <summary>
        /// 由您设置的订单ID来识别您的订单
        /// </summary>
        public string client_oid { get; set; }
        /// <summary>
        /// 下单结果。若是下单失败，将给出错误码提示
        /// </summary>
        public bool result { get; set; }
    }
}
