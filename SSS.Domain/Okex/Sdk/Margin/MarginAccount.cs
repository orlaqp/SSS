using System;
using System.Collections.Generic;
using System.Text;

namespace SSS.Domain.Okex.Sdk.Margin
{
    public class MarginAccount
    {
        /// <summary>
        /// 余额
        /// </summary>
        public string balance { get; set; }
        /// <summary>
        /// 冻结(不可用)
        /// </summary>
        public string hold { get; set; }
        /// <summary>
        /// 可用于交易或资金划转的数量
        /// </summary>
        public string available { get; set; }
        /// <summary>
        /// 风险率
        /// </summary>
        public string risk_rate { get; set; }
        /// <summary>
        /// 爆仓价
        /// </summary>
        public string liquidation_price { get; set; }
        /// <summary>
        /// 已借币 （已借未还的部分）
        /// </summary>
        public string borrowed { get; set; }
        /// <summary>
        /// 利息 （未还的利息）
        /// </summary>
        public string lending_fee { get; set; }
    }



    public class Currency1
    {
        /// <summary>
        /// 
        /// </summary>
        public string available { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string balance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string borrowed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string can_withdraw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string frozen { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hold { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string holds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lending_fee { get; set; }
    }

    public class Currency2
    {
        /// <summary>
        /// 
        /// </summary>
        public string available { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string balance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string borrowed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string can_withdraw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string frozen { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hold { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string holds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lending_fee { get; set; }
    }

    public class CurrencyRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public Currency1 currency1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Currency2 currency2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string instrument_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string liquidation_price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string product_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string risk_rate { get; set; }
    }
}
