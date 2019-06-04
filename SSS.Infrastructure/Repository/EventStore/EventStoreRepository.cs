using Microsoft.Extensions.DependencyInjection;
using SSS.Domain.Seedwork.Attribute;
using SSS.Infrastructure.Seedwork.DbContext;

namespace SSS.Infrastructure.Repository.EventStore
{
    [DIService(ServiceLifetime.Scoped, typeof(IEventStoreRepository))]
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly EventStoreContext _context;

        public EventStoreRepository(EventStoreContext context)
        {
            _context = context;
        }

        public void Add(SSS.Domain.Seedwork.Model.EventStore @event)
        {
            _context.eventstore.Add(@event);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
