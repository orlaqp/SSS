using SSS.Domain.Seedwork.Repository;

namespace SSS.Domain.Repository.Student
{
    public interface IStudentRepository : IRepository<Core.Student.Student>
    {
        SSS.Domain.Core.Student.Student GetByName(string name);
    }
}