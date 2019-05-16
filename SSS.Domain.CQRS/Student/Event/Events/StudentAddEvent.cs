﻿using System;

namespace SSS.Domain.CQRS.Student.Event.Events
{
    public class StudentAddEvent : Seedwork.Events.Event
    {
        public Guid id { set; get; }

        public string name { set; get; }

        public int age { set; get; }

        public StudentAddEvent(Domain.Student.Student student)
        {
            this.id = student.Id;
            this.name = student.Name;
            this.age = student.Age;
        }
    }
}
