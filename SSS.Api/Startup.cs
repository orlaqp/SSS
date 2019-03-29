using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 
using SSS.Api.Bootstrap;
using MediatR;
using SSS.Api.Seedwork;

namespace SSS.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                //全局Action Exception Result过滤器
                options.Filters.Add<MvcFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //AutoMapper映射
            services.AddAutoMapperSetup();

            //MediatR
            services.AddMediatR(typeof(Startup));

            //集中注入
            services.AddService();

            //Session
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Session缓存
            app.UseSession();

            //http上下文
            app.UseHttpContext();

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
