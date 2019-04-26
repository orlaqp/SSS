﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SSS.Application.OkexSdk.Futures
{
    public class PositionResult<T>
    {
        public bool result { get; set; }
        public List<T> holding { get; set; }
        /// <summary>
        /// 账户类型：全仓 crossed, 逐仓 fixed
        /// </summary>
        public string margin_mode { get; set; }
    }
}