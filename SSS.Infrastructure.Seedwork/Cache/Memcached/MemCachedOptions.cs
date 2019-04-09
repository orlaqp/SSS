﻿using Microsoft.Extensions.Options;

namespace SSS.Infrastructure.Seedwork.Cache.Redis
{
    public class MemCachedOptions : IOptions<MemCachedOptions>
    {
        public MemCachedOptions Value => this;

        public string host { set; get; }

        public int port { set; get; }
    }
}
