using SSS.Domain.Seedwork.Model;
using System;

namespace SSS.Domain.Okex.Trade
{
    public class Order : Entity
    {
        /// <summary>
        /// 交易对
        /// </summary>
        public string instrument { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// 委托时间
        /// </summary>
        public DateTime createtime { get; set; } 
        /// <summary>
        /// 订单价格
        /// </summary>
        public decimal price { get; set; }
        /// <summary>
        /// 订单状态(-1.撤单成功；0:等待成交 1:部分成交 2:已完成）
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 订单类型(1:开多 2:开空 3:开多 4:平空)
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 杠杆倍数 value:3x/5x BTC ETH LTC ETC EOS BCH默认5x  其他3x
        /// </summary>
        public int leverage { get; set; }
    }
}
