using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SSS.Application.Student;
using SSS.Domain.CQRS.Student.CommandHandlers;
using SSS.Domain.CQRS.Student.Commands; 
using SSS.Domain.Seedwork.Bus;
using SSS.Domain.Seedwork.Notifications;
using SSS.Domain.Seedwork.UnitOfWork;
using SSS.Infrastructure.Seedwork.DbContext;
using SSS.Infrastructure.Seedwork.UnitOfWork;
using SSS.Infrastructure.Student.Repository;

namespace SSS.Api.Bootstrap
{
    public static class ServiceSetup
    {
        public static void AddService(this IServiceCollection services)
        { 

            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<IStudentService, StudentService>();

            // Infra - Data
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbcontextBase>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Domain - Commands 
            services.AddScoped<IRequestHandler<StudentUpdateCommand, bool>, StudentCommandHandler>();
        }
    }
}
