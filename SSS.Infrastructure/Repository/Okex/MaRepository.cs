using SSS.Infrastructure.Seedwork.DbContext;
using SSS.Infrastructure.Seedwork.Repository;

namespace SSS.Infrastructure.Repository.Okex
{
    /// <summary>
    /// EmaRepository
    /// </summary>
    public class MaRepository : Repository<SSS.Domain.Okex.Target.Ma>, IMaRepository
    {
        public MaRepository(DbcontextBase context) : base(context)
        {
        }
    }

}
