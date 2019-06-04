using System;

namespace SSS.Infrastructure.Repository.EventStore
{
    public interface IEventStoreRepository : IDisposable
    {
        void Add(SSS.Domain.Seedwork.Model.EventStore @event);
    }
}
