using SSS.Domain.Seedwork.Repository;

namespace SSS.Infrastructure.Student.Repository
{
    public interface IStudentRepository : IRepository<SSS.Domain.Student.Student>
    {
        SSS.Domain.Student.Student GetByName(string name);
    }
}