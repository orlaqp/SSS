using SSS.Infrastructure.Seedwork.DbContext;
using SSS.Infrastructure.Seedwork.Repository;

namespace SSS.Domain.Repository.Student
{
    public class StudentRepository : Repository<Core.Student.Student>, IStudentRepository
    {
        public Core.Student.Student GetByName(string name) => throw new System.NotImplementedException();

        public StudentRepository(DbcontextBase context) : base(context)
        {
        }
    }
}