using SSS.Domain.Seedwork.Events;
using System;

namespace SSS.Domain.Seedwork.Notice
{
    public class ErrorNotice : Event
    {
        public string NoticeId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }

        public ErrorNotice(string key, string value)
        {
            NoticeId = Guid.NewGuid().ToString();
            Version = 1;
            Key = key;
            Value = value;
        }
    }
}
