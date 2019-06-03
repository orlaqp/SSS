using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SSS.Domain.Seedwork.Attribute;
using SSS.Infrastructure.Seedwork.Cache.Memcached;
using SSS.Infrastructure.Seedwork.Cache.Redis;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
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

        #region 自动注册服务

        /// <summary>
        /// 自动注册服务
        /// </summary>
        /// <param name="services">注册服务的集合（向其中注册）</param>
        /// <param name="ImplementationType">要注册的类型</param>
        public static void AutoRegisterService(this IServiceCollection services, Type ImplementationType)
        {
            //获取类型的 UseDIAttribute 属性 对应的对象
            DIServiceAttribute attr = ImplementationType.GetCustomAttribute(typeof(DIServiceAttribute)) as DIServiceAttribute;
            ////获取类实现的所有接口
            //Type[] types = ImplementationType.GetInterfaces();
            List<Type> types = attr.GetTargetTypes();
            var lifetime = attr.Lifetime;
            //遍历类实现的每一个接口
            foreach (var t in types)
            {
                //将类注册为接口的实现-----但是存在一个问题，就是担心 如果一个类实现了IDisposible接口 担心这个类变成了这个接口的实现
                ServiceDescriptor serviceDescriptor = new ServiceDescriptor(t, ImplementationType, lifetime);
                services.Add(serviceDescriptor);
            }

        }
        #endregion

        #region 将程序集中的所有符合条件的类型全部注册到 IServiceCollection中 
        /// <summary>
        /// 将程序集中的所有符合条件的类型全部注册到 IServiceCollection 中
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="AassemblyName">程序集名字</param>
        public static void AutoRegisterServicesFromAssembly(this IServiceCollection services,
            string AassemblyName)
        {
            //根据程序集的名字 获取程序集中所有的类型
            Type[] types = Assembly.Load(AassemblyName).GetTypes();

            //过滤上述程序集 首先按照传进来的条件进行过滤 接着要求Type必须是类，而且不能是抽象类
            IEnumerable<Type> list = types.Where(t => t.IsClass && !t.IsAbstract);
            foreach (var item in list)
            {
                IEnumerable<Attribute> attrs = item.GetCustomAttributes();
                //遍历类的所有特性
                foreach (var attr in attrs)
                {
                    //如果在其特性中发现特性是 UseDIAttribute 特性 就将这个类注册到DI容器中去
                    //并跳出当前的循环 开始对下一个类进行循环
                    if (attr is DIServiceAttribute)
                    {
                        services.AutoRegisterService(item);
                        break;
                    }
                }
            }
        }
        #endregion 
    }
}
