using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SSS.Api.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSS.Api.Bootstrap
{
    public static class AppSetup
    {
        public static IApplicationBuilder UseHttpContext(this IApplicationBuilder app)
        {
            //获取HtppContext实例
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            //获取IHostingEnvironment实例
            var hostingEnvironment = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();
            //注入实例
            HttpContextService.Configure(httpContextAccessor, hostingEnvironment);
            return app;
        }
    }
}
