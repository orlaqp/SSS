using System;
using System.Collections.Generic;
using System.Text;

namespace SSS.Domain.Okex.Sdk.Margin
{
    public class RepaymentResult
    {
        /// <summary>
        /// 还币记录ID
        /// </summary>
        public long repayment_id { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public bool result { get; set; }
    }
}
