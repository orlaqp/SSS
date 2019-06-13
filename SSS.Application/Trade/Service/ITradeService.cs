using SSS.Domain.Seedwork.Model;
using SSS.Domain.Trade.Dto;
using System.Collections.Generic;

namespace SSS.Application.Trade.Service
{
    public interface ITradeService
    {
        void OperateTrade(TradeInputDto input);
        Pages<List<TradeOutputDto>> GetListTrade(TradeInputDto input);
    }
}