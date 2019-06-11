﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SSS.Api.Seedwork.ServiceCollection;
using SSS.Infrastructure.Seedwork.Cache.Memcached;
using SSS.Infrastructure.Seedwork.Cache.Redis;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

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
            services.AutoRegisterServicesFromAssembly("SSS.Domain.Seedwork");

            // Application
            services.AutoRegisterServicesFromAssembly("SSS.Application.Seedwork");
            services.AutoRegisterServicesFromAssembly("SSS.Application");

            // Infra - Data 
            services.AutoRegisterServicesFromAssembly("SSS.Infrastructure.Seedwork");
            services.AutoRegisterServicesFromAssembly("SSS.Infrastructure");

            // Domain - Commands Events 
            services.AutoRegisterServicesFromAssembly("SSS.Domain.CQRS");
        }

        /// <summary>
        /// AutoMapper
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoMapperSupport(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            Type[] types = Assembly.Load("SSS.Application").GetTypes().Where(t => t.BaseType != null && t.BaseType.Name.Equals("Profile")).ToArray();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(types);
            });

            services.AddAutoMapper();
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
