using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SSS.Infrastructure.Seedwork.DataBase.MongoDB;
using SSS.Infrastructure.Seedwork.DbContext;
using SSS.Infrastructure.Seedwork.Repository;

namespace SSS.Infrastructure.Repository.Student
{
    /// <summary>
    /// RDMS
    /// </summary>
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

    /// <summary>
    /// NoSql
    /// </summary>
    public class MongoStudentRepository : MongoDBRepository<SSS.Domain.Student.Student>, IStudentRepository
    {
        public MongoStudentRepository(IOptions<MongoOptions> options,
            ILogger<MongoDBRepository<Domain.Student.Student>> logger) : base(options, logger)
        {
        }

        public Domain.Student.Student GetByName(string name)
        {
            return _collection.Find(x => x.Name == name).ToList().FirstOrDefault();
        }
    }
}