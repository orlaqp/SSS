using SSS.Domain.Seedwork.Commands;
using System;

namespace SSS.Domain.CQRS.Student.Commands
{
    public abstract class StudentCommand : Command
    {
        public Guid id { set; get; }
        public string name { get; set; }
        public int age { set; get; }
    }
}
