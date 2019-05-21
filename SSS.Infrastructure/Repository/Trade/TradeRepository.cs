using Microsoft.EntityFrameworkCore;
using SSS.Domain.Trade;
using SSS.Infrastructure.Seedwork.DbContext;
using SSS.Infrastructure.Seedwork.Repository;
using System.Linq;

namespace SSS.Infrastructure.Repository.Trade
{
    public class TradeRepository : Repository<SSS.Domain.Trade.Trade>, ITradeRepository
    {
        public TradeRepository(DbcontextBase context) : base(context)
        {
        }

        public Domain.Trade.Trade GetByTradeNo(string tradeno)
        { 
            return DbSet.AsNoTracking().FirstOrDefault(x => x.First_Trade_No.Contains(tradeno));
        }
    }
}