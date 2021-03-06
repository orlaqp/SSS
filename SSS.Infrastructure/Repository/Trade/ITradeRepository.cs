﻿using SSS.Domain.Seedwork.Repository;

namespace SSS.Infrastructure.Repository.Trade
{
    public interface ITradeRepository : IRepository<SSS.Domain.Trade.Trade>
    {
        SSS.Domain.Trade.Trade GetByTradeNo(string tradeno);
    }
}