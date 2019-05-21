using MediatR;
using Microsoft.Extensions.Logging;
using SSS.Domain.CQRS.Trade.Command.Commands;
using SSS.Domain.CQRS.Trade.Event.Events;
using SSS.Domain.Seedwork.Bus;
using SSS.Domain.Seedwork.Command;
using SSS.Domain.Seedwork.Notifications;
using SSS.Domain.Seedwork.UnitOfWork;
using SSS.Infrastructure.Repository.Trade;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SSS.Domain.CQRS.Trade.Command.Handlers
{
    /// <summary>
    /// StudentCommandHandler
    /// </summary>
    public class TradeCommandHandler : CommandHandler,
         IRequestHandler<TradeAddCommand, bool>
    {

        private readonly ITradeRepository _traderepository;
        private readonly IMediatorHandler Bus;
        private readonly ILogger _logger;

        public TradeCommandHandler(ITradeRepository traderepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications,
                                      ILogger<TradeCommandHandler> logger
                                      ) : base(uow, bus, notifications)
        {
            _logger = logger;
            _traderepository = traderepository;
            Bus = bus;
        }

        public Task<bool> Handle(TradeAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }
            var trade = new SSS.Domain.Trade.Trade(
                request.id, request.coin,
                request.size, request.first_price, 0,
                request.side, request.first_trade_status, 0,
                request.first_trade_no, "");
            trade.CreateTime = DateTime.Now;
            trade.IsDelete = 0;
            _traderepository.Add(trade);

            if (Commit())
            {
                _logger.LogInformation("TradeAddCommand Success");
                Bus.RaiseEvent(new TradeAddEvent(trade));
            }
            return Task.FromResult(true);
        }

        public Task<bool> Handle(TradeUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            var trade = _traderepository.GetByTradeNo(request.first_trade_no);
            if (trade == null)
            {
                _logger.LogInformation("TradeUpdateCommand Error Trade Is Null");
                return Task.FromResult(false);
            } 

            trade.Last_Price = request.last_price;
            trade.Last_Trade_No = request.last_trade_no;
            trade.Last_Trade_Status = request.last_trade_status;
            _traderepository.Update(trade);

            if (Commit())
            {
                _logger.LogInformation("TradeUpdateCommand Success");
                Bus.RaiseEvent(new TradeAddEvent(trade));
            }
            return Task.FromResult(true);
        }
    }
}
