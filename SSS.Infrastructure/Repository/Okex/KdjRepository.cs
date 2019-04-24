using SSS.Infrastructure.Seedwork.DbContext;
using SSS.Infrastructure.Seedwork.Repository;

namespace SSS.Infrastructure.Repository.Okex
{
    /// <summary>
    /// KdjRepository
    /// </summary>
    public class KdjRepository : Repository<SSS.Domain.Okex.Target.Kdj>, IKdjRepository
    {
        public KdjRepository(DbcontextBase context) : base(context)
        {
        }
    }

}
