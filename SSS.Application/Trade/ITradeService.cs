using System.Collections.Generic;
using SSS.Domain.Seedwork.Model; 
using SSS.Domain.Trade.Dto;

namespace SSS.Application.Trade
{
    public interface ITradeService
    {
        void OperateTrade(TradeInputDto input); 
        Pages<List<TradeOutputDto>> GetListTrade(TradeInputDto input); 
    }
}