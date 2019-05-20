using SSS.Infrastructure.Seedwork.DbContext;
using SSS.Infrastructure.Seedwork.Repository;

namespace SSS.Infrastructure.Repository.Trade
{
    public class TradeRepository : Repository<SSS.Domain.Trade.Trade>, ITradeRepository
    {
        public TradeRepository(DbcontextBase context) : base(context)
        {
        }
    }
}