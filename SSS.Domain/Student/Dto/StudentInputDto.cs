using System;

namespace SSS.Domain.Student.Dto
{
    public class StudentInputDto
    {
        public Guid id { get; set; }

        public string name { set; get; }
        public int age { set; get; }

        public int pageindex { set; get; }

        public int pagesize { set; get; }

        public string order_by { set; get; } 
    }
}
