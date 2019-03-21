using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SSS.Application.Seedwork.AutoMapper;

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
    }
}