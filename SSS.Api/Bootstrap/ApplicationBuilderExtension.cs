﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SSS.Api.Seedwork;
using SSS.Infrastructure.Seedwork.Cache.Redis;
using System; 

namespace SSS.Api.Bootstrap
{
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// 注入HttpContext
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 注入RedisCache
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRedisCache(this IApplicationBuilder app, Action<RedisOptions> options = null)
        {
            RedisOptions config = null;
            options(config);
            if (config == null)
                config = new RedisOptions() { host = "localhost", port = 6379 };
            
            return app;
        }

        /// <summary>
        /// 注入Memcached
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMemCached(this IApplicationBuilder app, Action<MemCachedOptions> options = null)
        {
            MemCachedOptions config = null;
            options(config);
            if (config == null)
                config = new MemCachedOptions() { host = "localhost", port = 11211 };

            return app;
        }
    }
}
