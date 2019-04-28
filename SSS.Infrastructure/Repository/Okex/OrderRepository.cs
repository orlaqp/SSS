using SSS.Infrastructure.Seedwork.DbContext;
using SSS.Infrastructure.Seedwork.Repository;

namespace SSS.Infrastructure.Repository.Okex
{
    /// <summary>
    /// EmaRepository
    /// </summary>
    public class OrderRepository : Repository<SSS.Domain.Okex.Trade.Order>, IOrderRepository
    {
        public OrderRepository(DbcontextBase context) : base(context)
        {
        }
    }
}
