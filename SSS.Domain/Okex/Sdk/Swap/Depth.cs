using System;
using System.Collections.Generic;
using System.Text;

namespace SSS.Domain.Okex.Sdk.Swap
{
    public class Depth
    {
        public List<List<double>> asks { get; set; }
        public List<List<double>> bids { get; set; }
        public DateTime timestamp { get; set; }
    }
}
