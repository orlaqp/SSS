using SSS.Domain.Seedwork.Events;
using System;

namespace SSS.Domain.CQRS.Student.Event.Events
{
    public class StudentUpdateEvent : Seedwork.Events.Event
    {
        public Guid id { set; get; }

        public string name { set; get; }

        public int age { set; get; }

        public StudentUpdateEvent(Guid id, string name, int age)
        {
            this.id = id;
            this.name = name;
            this.age = age;
        }
    }
}
