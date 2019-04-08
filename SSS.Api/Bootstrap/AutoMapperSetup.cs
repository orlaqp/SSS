using System;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SSS.Application.Seedwork.AutoMapper;
using SSS.Infrastructure.Seedwork.Cache.Redis;

namespace SSS.Api.Bootstrap
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper();

            AutoMapperConfig.RegisterMappings();
        }

        public static void AddRedisCache(this IServiceCollection services, IConfigurationSection section)
        {
            services.Configure<RedisOptions>(section);
            services.AddTransient<RedisCache>();
        }

        public static void AddRedisCache(this IServiceCollection services)
        { 
            services.AddTransient<RedisCache>();
        }
    }
}