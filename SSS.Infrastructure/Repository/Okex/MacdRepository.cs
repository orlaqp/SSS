using SSS.Infrastructure.Seedwork.DbContext;
using SSS.Infrastructure.Seedwork.Repository;

namespace SSS.Infrastructure.Repository.Okex
{
    /// <summary>
    /// MacdRepository
    /// </summary>
    public class MacdRepository : Repository<SSS.Domain.Okex.Target.Macd>, IMacdRepository
    {
        public MacdRepository(DbcontextBase context) : base(context)
        {
        }
    }

}
