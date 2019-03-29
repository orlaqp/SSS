using System;

namespace SSS.Domain.Seedwork.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}