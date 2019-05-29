using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SSS.Domain.CQRS.UserInfo.Command.Commands;
using SSS.Domain.CQRS.UserInfo.Event.Events;
using SSS.Domain.Seedwork.Attribute;
using SSS.Domain.Seedwork.Bus;
using SSS.Domain.Seedwork.Command;
using SSS.Domain.Seedwork.Notifications;
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
                                      INotificationHandler<DomainNotification> notifications,
                                      ILogger<UserInfoCommandHandler> logger
                                      ) : base(uow, bus, notifications)
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
            var model = new SSS.Domain.UserInfo.UserInfo(request.id);
            model.CreateTime = DateTime.Now;
            model.IsDelete = 0;

            _repository.Add(model);
            if (Commit())
            {
                _logger.LogInformation("UserInfoAddCommand Success");
                Bus.RaiseEvent(new UserInfoAddEvent(model));
            }
            return Task.FromResult(true);
        }
    }
}
