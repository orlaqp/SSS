using SSS.Domain.Seedwork.Model;

namespace SSS.Domain.Student
{
    public class Student : Entity
    {
        public Student(string id, string name, int age)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
        }
        public string Name { set; get; }

        public int Age { set; get; }

    }
}
