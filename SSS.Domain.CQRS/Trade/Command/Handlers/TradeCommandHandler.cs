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
                request.size, request.price,
                request.side, request.trade_status, 
                request.trade_no);
            trade.CreateTime=DateTime.Now;
            trade.IsDelete = 0;
            _traderepository.Add(trade);

            if (Commit())
            {
                _logger.LogInformation("TradeAddCommand Success");
                Bus.RaiseEvent(new TradeAddEvent(trade));
            }
            return Task.FromResult(true);
        }
    }
}
