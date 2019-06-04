using SSS.Domain.Seedwork.Events;
using System;

namespace SSS.Domain.Seedwork.Model
{
    public class EventStore : Event
    {
        public EventStore(string MsgType, string data, string user)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Data = data;
            this.User = user;
            this.MsgType = MsgType;
        }
        public string Id { get; private set; }

        public string Data { get; private set; }

        public string User { get; private set; }
    }
}
