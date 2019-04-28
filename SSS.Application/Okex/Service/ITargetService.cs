using SSS.Application.OkexSdk.Sdk;
using System;

namespace SSS.Application.Okex.Service
{
    public interface ITargetService
    {
        void CreateTarget(DateTime time,string instrument, KLineTime timetype);
    }
}
