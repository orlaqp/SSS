using SSS.Application.OkexSdk.Sdk;
using System;

namespace SSS.Application.Okex.Service
{
    public interface ITargetService
    {
        void CreateTarget(string instrument, KLineTime timetype);
    }
}
