using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SSS.Domain.CQRS.UserInfo.Command.Commands;
using SSS.Domain.CQRS.UserInfo.Event.Events;
using SSS.Domain.Seedwork.Attribute;
using SSS.Domain.Seedwork.Bus;
using SSS.Domain.Seedwork.Command;
using SSS.Domain.Seedwork.Notice;
using SSS.Domain.Seedwork.UnitOfWork;
using SSS.Infrastructure.Repository.UserInfo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SSS.Domain.CQRS.UserInfo.Command.Handlers
{
    [DIService(ServiceLifetime.Scoped,
       typeof(IRequestHandler<UserInfoAddCommand, bool>))]
    /// <summary>
    /// UserInfoCommandHandler
    /// </summary>
    public class UserInfoCommandHandler : CommandHandler,
         IRequestHandler<UserInfoAddCommand, bool>
    {
        private readonly IUserInfoRepository _repository;
        private readonly IMediatorHandler Bus;
        private readonly ILogger _logger;

        public UserInfoCommandHandler(IUserInfoRepository repository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<ErrorNotice> @event,
                                      ILogger<UserInfoCommandHandler> logger
                                      ) : base(uow, logger, bus, @event)
        {
            _logger = logger;
            _repository = repository;
            Bus = bus;
        }
        public Task<bool> Handle(UserInfoAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }

            if (_repository.GetUserInfoByPhone(request.phone) != null)
            {
                Bus.RaiseEvent(new ErrorNotice(request.MsgType, "手机号已存在！"));
                return Task.FromResult(false);
            }

            var model = new SSS.Domain.UserInfo.UserInfo(request.id, request.uid, request.phone, request.password, request.firstid);
            model.CreateTime = DateTime.Now;
            model.IsDelete = 0;
            model.Earning = 0;
            model.Commission = 0;
            model.FirstId = request.firstid;
            model.Uid = 1234;

            _repository.Add(model);
            if (!Commit())
                return Task.FromResult(false);

            _logger.LogInformation("UserInfoAddCommand Success");
            Bus.RaiseEvent(new UserInfoAddEvent(model));
            return Task.FromResult(true);
        }
    }
}
