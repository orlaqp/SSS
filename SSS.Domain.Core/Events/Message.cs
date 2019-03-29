using MediatR;
using System;

namespace SSS.Domain.Seedwork.Events
{
    public class Message : IRequest<bool>
    {
        public string MsgType { set; get; }
        public Guid AggregateId { set; get; }
        protected Message()
        {
            MsgType = GetType().Name;
        }
    }
}
