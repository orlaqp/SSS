using Microsoft.EntityFrameworkCore;
using SSS.Infrastructure.Seedwork.DbContext;
using SSS.Infrastructure.Seedwork.Repository;
using System.Linq;

namespace SSS.Infrastructure.Repository.Student
{
    public class StudentRepository : Repository<SSS.Domain.Student.Student>, IStudentRepository
    {
        public SSS.Domain.Student.Student GetByName(string name)
        {
            return DbSet.AsNoTracking().FirstOrDefault(x => x.Name.Contains(name));
        }

        public StudentRepository(DbcontextBase context) : base(context)
        {
        }
    }
}