using SSS.Infrastructure.Seedwork.DbContext;
using SSS.Infrastructure.Seedwork.Repository;

namespace SSS.Infrastructure.Repository.Okex
{
    /// <summary>
    /// EmaRepository
    /// </summary>
    public class EmaRepository : Repository<SSS.Domain.Okex.Target.Ema>,IEmaRepository
    {
        public EmaRepository(DbcontextBase context) : base(context)
        {
        }
    }

}
