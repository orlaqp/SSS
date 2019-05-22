using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SSS.Application.Student;
using SSS.Domain.CQRS.Student.Command.Handlers;
using SSS.Domain.CQRS.Student.Command.Commands;
using SSS.Domain.CQRS.Student.Event.Handlers;
using SSS.Domain.CQRS.Student.Event.Events;
using SSS.Domain.Seedwork.Bus;
using SSS.Domain.Seedwork.Notifications;
using SSS.Domain.Seedwork.UnitOfWork;
using SSS.Infrastructure.Seedwork.DbContext;
using SSS.Infrastructure.Seedwork.UnitOfWork;
using SSS.Infrastructure.Repository.Student;
using SSS.Infrastructure.Seedwork.Cache.Redis;
using Microsoft.Extensions.Configuration;
using System;
using SSS.Application.Seedwork.AutoMapper;
using SSS.Infrastructure.Seedwork.Cache.Memcached;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using SSS.Application.Trade;
using SSS.Infrastructure.Repository.Trade;
using SSS.Domain.CQRS.Trade.Event.Events;
using SSS.Domain.CQRS.Trade.Event.Handlers;
using SSS.Domain.CQRS.Trade.Command.Handlers;
using SSS.Domain.CQRS.Trade.Command.Commands;

namespace SSS.Api.Bootstrap
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Service Base
        /// </summary>
        /// <param name="services"></param>
        public static void AddService(this IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITradeService, TradeService>();

            // Infra - Data
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ITradeRepository, TradeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbcontextBase>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<StudentUpdateEvent>, StudentUpdateEventHandler>();
            services.AddScoped<INotificationHandler<StudentAddEvent>, StudentAddEventHandler>(); 
            services.AddScoped<INotificationHandler<TradeAddEvent>, TradeAddEventHandler>();

            // Domain - Commands 
            services.AddScoped<IRequestHandler<StudentUpdateCommand, bool>, StudentCommandHandler>();
            services.AddScoped<IRequestHandler<StudentAddCommand, bool>, StudentCommandHandler>(); 
            services.AddScoped<IRequestHandler<TradeAddCommand, bool>, TradeCommandHandler>();
            services.AddScoped<IRequestHandler<TradeUpdateCommand, bool>, TradeCommandHandler>();
            services.AddScoped<IRequestHandler<TradeNullCommand, bool>, TradeCommandHandler>();
        }

        /// <summary>
        /// AutoMapper
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoMapperSupport(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper();

            AutoMapperConfig.RegisterMappings();
        }

        /// <summary>
        /// Swagger
        /// </summary>
        /// <param name="services"></param> 
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "SSS Project V1",
                    Description = "SSS API Swagger docs",
                    Contact = new Contact { Name = "wbs", Email = "512742341@qq.com", Url = "https://github.com/wangboshun" },
                    License = new License { Name = "MIT", Url = "https://github.com/wangboshun/SSS" }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            }); 
        }

        //ApiVersion
        public static void AddApiVersion(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = false;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }

        #region Redis

        /// <summary>
        /// 配置Redis链接
        /// </summary>
        /// <param name="services"></param>
        /// <param name="section"></param>
        public static void AddRedisCache(this IServiceCollection services, IConfigurationSection section)
        {
            services.Configure<RedisOptions>(section);
            services.AddTransient<RedisCache>();
        }

        /// <summary>
        /// 配置Redis链接
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        public static void AddRedisCache(this IServiceCollection services, Action<RedisOptions> options)
        {
            services.Configure<RedisOptions>(options);
            services.AddTransient<RedisCache>();
        }

        /// <summary>
        /// 默认Redis链接
        /// </summary>
        /// <param name="services"></param>
        public static void AddRedisCache(this IServiceCollection services)
        {
            services.AddTransient<RedisCache>();
        }

        #endregion

        #region Memcached

        /// <summary>
        /// 配置Memcached链接
        /// </summary>
        /// <param name="services"></param>
        /// <param name="section"></param>
        public static void AddMemCached(this IServiceCollection services, IConfigurationSection section)
        {
            services.Configure<MemCachedOptions>(section);
            services.AddTransient<MemCached>();
        }

        /// <summary>
        /// 默认Memcached链接
        /// </summary>
        /// <param name="services"></param>
        public static void AddMemcached(this IServiceCollection services)
        {
            services.AddTransient<MemCached>();
        }

        /// <summary>
        /// 配置Memcached链接
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        public static void AddMemcached(this IServiceCollection services, Action<MemCachedOptions> options)
        {
            services.Configure<MemCachedOptions>(options);
            services.AddTransient<RedisCache>();
        }

        #endregion 
    }
}
