﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace SSS.Api.Seedwork
{
    public static class HttpContextService
    {
        private static IHttpContextAccessor _accessor;
        public static HttpContext Current
        {
            get => _accessor.HttpContext;
        }

        private static IHostingEnvironment _hostingEnvironment;

        public static void Configure(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public static void Configure(IHttpContextAccessor accessor, IHostingEnvironment hostingEnvironment)
        {
            _accessor = accessor;
            _hostingEnvironment = hostingEnvironment;
        }

        public static string ContentRootPath(this HttpContext context)
        {
            return _hostingEnvironment.ContentRootPath;
        }
        public static string WebRootPath(this HttpContext context)
        {
            return _hostingEnvironment.WebRootPath;
        }
    }
}
