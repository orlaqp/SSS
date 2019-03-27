using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SSS.Application.Student;
using SSS.Domain.Repository.Student;
using SSS.Domain.Seedwork.UnitOfWork;
using SSS.Infrastructure.Seedwork.DbContext;
using SSS.Infrastructure.Seedwork.UnitOfWork; 

namespace SSS.Api.Bootstrap
{
    public static class IocSetup
    {
        public static void AddIoc(this IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Application
            services.AddScoped<IStudentService, StudentService>(); 

            // Infra - Data
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbcontextBase>();
        }
    }
}
