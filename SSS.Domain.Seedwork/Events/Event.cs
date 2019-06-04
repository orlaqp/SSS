using MediatR;
using System;

namespace SSS.Domain.Seedwork.Events
{
    public abstract class Event : Message, INotification
    {
        public DateTime CreateTime { get; private set; }

        protected Event()
        {
            CreateTime = DateTime.Now;
        }
    }
}
