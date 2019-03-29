using MediatR;
using Microsoft.Extensions.Logging;
using SSS.Domain.CQRS.Student.Commands; 
using SSS.Domain.Seedwork.Bus;
using SSS.Domain.Seedwork.CommandHandlers;
using SSS.Domain.Seedwork.Notifications;
using SSS.Domain.Seedwork.UnitOfWork;
using SSS.Infrastructure.Student.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace SSS.Domain.CQRS.Student.CommandHandlers
{
    /// <summary>
    /// 
    /// </summary>
    public class StudentCommandHandler : CommandHandler,
         IRequestHandler<StudentUpdateCommand, bool>
    {

        private readonly IStudentRepository _studentrepository;
        private readonly IMediatorHandler Bus;
        private readonly ILogger _logger;

        public StudentCommandHandler(IStudentRepository studentrepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications,
                                      ILogger<StudentCommandHandler> logger
                                      ) : base(uow, bus, notifications)
        {
            _logger = logger;
            _studentrepository = studentrepository;
            Bus = bus;
        }
        public Task<bool> Handle(StudentUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return Task.FromResult(false);
            }
            var student = new SSS.Domain.Student.Student(request.id, request.name, request.age);
            _studentrepository.Update(student);
            if (Commit())
            {
                _logger.LogInformation("StudentUpdateCommand Success");
            }
            return Task.FromResult(true);
        }
    }
}
