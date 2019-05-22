namespace SSS.Domain.Trade.Response
{
    public class OrderInfoResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public string client_oid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string created_at { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string filled_notional { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string filled_size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string funds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string instrument_id { get; set; }
        /// <summary>
        /// 	买入金额，市价买入时返回
        /// </summary>
        public string notional { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string order_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string price_avg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string product_id { get; set; }
        /// <summary>
        /// 	buy or sell
        /// </summary>
        public string side { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// ("-2":失败,"-1":撤单成功,"0":等待成交 ,"1":部分成交, "2":完全成交,"3":下单中,"4":撤单中,）
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
    }
}
