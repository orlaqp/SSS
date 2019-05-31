using System;

namespace SSS.Domain.Seedwork.Model
{
    public abstract class InputDtoBase
    {
        public Guid id { get; set; }

        public int pageindex { set; get; }

        public int pagesize { set; get; }

        public string order_by { set; get; }
    }

    public abstract class OutputDtoBase
    {
        public Guid id { get; set; }
    }
}
