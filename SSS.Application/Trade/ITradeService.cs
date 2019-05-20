﻿using System.Collections.Generic;
using SSS.Domain.Seedwork.Model; 
using SSS.Domain.Trade.Dto;

namespace SSS.Application.Trade
{
    public interface ITradeService
    {
        void AddTrade(TradeInputDto input); 
        Pages<List<TradeOutputDto>> GetListTrade(TradeInputDto input); 
    }
}