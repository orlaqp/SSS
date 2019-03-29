using Microsoft.EntityFrameworkCore;
using SSS.Infrastructure.Seedwork.DbContext;
using SSS.Infrastructure.Seedwork.Repository;
using System.Linq;

namespace SSS.Domain.CQRS.Student.Repository
{
    public class StudentRepository : Repository<Core.Student.Student>, IStudentRepository
    {
        public Core.Student.Student GetByName(string name)
        {
            return DbSet.AsNoTracking().FirstOrDefault(x => x.Name.Contains(name));
        }

        public StudentRepository(DbcontextBase context) : base(context)
        {
        }
    }
}