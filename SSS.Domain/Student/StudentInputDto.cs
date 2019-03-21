using System;
using System.Collections.Generic;
using System.Text;

namespace SSS.Domain.Core.Student
{
    public class StudentInputDto
    {
        public string name { set; get; }

        public int pageindex { set; get; }

        public int pagesize { set; get; }

        public string order_by { set; get; }
    }
}
