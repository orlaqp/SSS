using System;
using System.Collections.Generic;
using System.Text;

namespace SSS.Domain.Okex.Sdk.Spot
{
    /// <summary>
    /// k线数据
    /// </summary>
    public class SpotCandle
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime time { get; set; }
        /// <summary>
        /// 最低价格
        /// </summary>
        public double low { get; set; }
        /// <summary>
        /// 最高价格
        /// </summary>
        public double high { get; set; }
        /// <summary>
        /// 开盘价格
        /// </summary>
        public double open { get; set; }
        /// <summary>
        /// 收盘价格
        /// </summary>
        public double close { get; set; }
        /// <summary>
        /// 交易量
        /// </summary>
        public double volume { get; set; }
        /// <summary>
        /// 交易对
        /// </summary>
        public string instrument { set; get; }
    }
}
