using System;
using System.Collections.Generic;
using System.Text;

namespace SSS.Domain.Okex.Sdk.Spot
{
    public class SpotBook
    {
        public List<List<string>> bids { get; set; }
        public List<List<string>> asks { get; set; }
    }
}
