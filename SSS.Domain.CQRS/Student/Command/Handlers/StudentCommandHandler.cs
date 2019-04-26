﻿using MediatR;
using Microsoft.Extensions.Logging; 
using SSS.Domain.Seedwork.Bus; 
using SSS.Domain.Seedwork.Notifications;
using SSS.Domain.Seedwork.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;
using SSS.Domain.CQRS.Student.Command.Commands;
using SSS.Domain.CQRS.Student.Event.Events;
using SSS.Domain.Seedwork.Command;
using SSS.Infrastructure.Repository.Student;

namespace SSS.Domain.CQRS.Student.Command.Handlers
{
    /// <summary>
    /// StudentCommandHandler
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
                Bus.RaiseEvent(new StudentUpdateEvent(student.Id, student.Name, student.Age));
            }
            return Task.FromResult(true);
        }
    }
}