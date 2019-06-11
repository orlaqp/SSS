using Hangfire.Server;
using SSS.Domain.Seedwork.Model;
using SSS.Domain.Trade.Dto;
using SSS.Infrastructure.Util.Hangfire;
using System.Collections.Generic;

namespace SSS.Application.Trade.Service
{
    public interface ITradeService  
    {
        void OperateTrade(PerformContext context);
        Pages<List<TradeOutputDto>> GetListTrade(TradeInputDto input);
    }
}