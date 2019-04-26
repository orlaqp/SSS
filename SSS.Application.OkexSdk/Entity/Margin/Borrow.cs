using System;
using System.Collections.Generic;
using System.Text;

namespace SSS.Application.OkexSdk.Margin
{
    public class Borrow
    {
        /// <summary>
        /// 杠杆币对名称
        /// </summary>
        public string instrument_id { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string currency { get; set; }
        /// <summary>
        /// 借币数量
        /// </summary>
        public string amount { get; set; }
    }
}
