﻿using Microsoft.Extensions.DependencyInjection;
using System;

namespace SSS.Infrastructure.Util.Attribute
{
    /// <summary>
    /// 自动注入特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class AutowiredAttribute : System.Attribute, IServiceProviderFactory<IServiceProvider>
    {
        IServiceProvider IServiceProviderFactory<IServiceProvider>.CreateBuilder(IServiceCollection services) => throw new NotImplementedException();
        IServiceProvider IServiceProviderFactory<IServiceProvider>.CreateServiceProvider(IServiceProvider containerBuilder) => containerBuilder;
    }
}
