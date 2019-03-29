using SSS.Domain.Seedwork.Repository;

namespace SSS.Domain.CQRS.Student.Repository
{
    public interface IStudentRepository : IRepository<Core.Student.Student>
    {
        SSS.Domain.Core.Student.Student GetByName(string name);
    }
}