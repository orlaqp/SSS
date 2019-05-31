using MediatR;
using Microsoft.Extensions.Logging;
using SSS.Domain.Seedwork.Bus;
using SSS.Domain.Seedwork.Notice;
using SSS.Domain.Seedwork.UnitOfWork;
using System;

namespace SSS.Domain.Seedwork.Command
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _bus;
        private readonly ErrorNoticeHandler _Notice;
        private readonly ILogger _logger;

        public CommandHandler(IUnitOfWork uow, ILogger<CommandHandler> logger, IMediatorHandler bus, INotificationHandler<ErrorNotice> Notice)
        {
            _uow = uow;
            _Notice = (ErrorNoticeHandler)Notice;
            _bus = bus;
            _logger = logger;
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                _bus.RaiseEvent(new ErrorNotice(message.MsgType, error.ErrorMessage));
            }
        }

        public bool Commit()
        {
            if (_Notice.HasNotice())
                return false;

            try
            {
                if (_uow.Commit())
                    return true;
            }
            catch (Exception e)
            {
                _bus.RaiseEvent(new ErrorNotice("Commit", $"事务提交异常，{e.InnerException}"));
                _logger.LogError("事务提交异常", e.InnerException);
                return false;
            }

            _bus.RaiseEvent(new ErrorNotice("Commit", "事务提交失败"));
            return false;
        }
    }
}
