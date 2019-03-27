using SSS.Domain.Seedwork.UnitOfWork;
using SSS.Infrastructure.Seedwork.DbContext;

namespace SSS.Infrastructure.Seedwork.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbcontextBase _context;

        public UnitOfWork(DbcontextBase context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}