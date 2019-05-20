using System;
using System.Collections.Generic;
using System.Text;

namespace SSS.Domain.Trade
{
    public class MarginResponse
    {
        public string client_oid { set; get; }

        public string error_code { set; get; }

        public string error_message { set; get; }

        public string order_id { set; get; }

        public string result { set; get; }

    }
}
