using SSS.Domain.Seedwork.Events;
using System.Threading.Tasks;

namespace SSS.Domain.Seedwork.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command.Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
