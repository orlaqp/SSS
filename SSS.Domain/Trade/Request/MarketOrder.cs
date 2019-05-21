using Newtonsoft.Json;

namespace SSS.Domain.Trade.Request
{
    public class MarketOrder
    {
        /// <summary>
        /// market 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// buy or sell
        /// </summary>
        public string side { get; set; }
        /// <summary>
        /// 币对名称
        /// </summary>
        public string instrument_id { get; set; }
        /// <summary>
        /// 买入或卖出的数量
        /// </summary>
        [JsonProperty(PropertyName = "size", NullValueHandling = NullValueHandling.Ignore)]
        public string size { get; set; }
        /// <summary>
        /// 下单类型(当前为币币杠杆交易，请求值为2)
        /// </summary>
        public int margin_trading { set; get; }

        [JsonProperty(PropertyName = "notional", NullValueHandling = NullValueHandling.Ignore)]
        public string notional { get; set; }
    }
}
