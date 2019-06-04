using SSS.Domain.Seedwork.Events;

namespace SSS.Domain.Seedwork.EventStore
{
    public interface IEventStore
    {
        void Save<T>(T @event) where T : Event;
    }
}
