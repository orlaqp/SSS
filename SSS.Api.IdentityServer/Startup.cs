using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace SSS.Api.IdentityServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //为令牌签名创建了一个临时密钥,并添加各类资源
            services.AddIdentityServer().AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(ClientConfig.GetIdentityResources())
                .AddInMemoryApiResources(ClientConfig.GetApis())
                .AddInMemoryClients(ClientConfig.GetClients())
                .AddTestUsers(ClientConfig.GetUsers());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/plain; charset=utf-8";
                await context.Response.WriteAsync("This is IdentityServer," +
                                                  "访问 http://localhost:456/.well-known/openid-configuration 查看IdentityServer的各种元数据信息。");
            });
        }
    }
}
