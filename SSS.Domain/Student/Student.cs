using SSS.Domain.Seedwork.Model;
using System;

namespace SSS.Domain.Student
{
    public class Student : Entity
    {
        public Student(Guid id, string name, int age)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
        }
        public string Name { set; get; }

        public int Age { set; get; }

    }
}
