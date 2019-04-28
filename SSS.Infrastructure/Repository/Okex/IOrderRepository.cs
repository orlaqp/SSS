using SSS.Domain.Seedwork.Repository;

namespace SSS.Infrastructure.Repository.Okex
{
    public interface IOrderRepository : IRepository<SSS.Domain.Okex.Trade.Order>
    {
    }
}