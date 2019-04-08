using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;

namespace SSS.Infrastructure.Seedwork.Cache.Redis
{
    public class RedisCache
    {
        private readonly ConnectionMultiplexer _redis;

        private readonly ILogger _logger;

        private readonly IDatabase _db;

        public RedisCache(IOptions<RedisOptions> options, ILogger<RedisCache> logger)
        {
            _logger = logger;

            try
            {
                if (!string.IsNullOrWhiteSpace(options.Value.host))
                    _redis = ConnectionMultiplexer.Connect(new ConfigurationOptions { EndPoints = { { options.Value.host, options.Value.port } } });

                else
                    _redis = ConnectionMultiplexer.Connect(new ConfigurationOptions { EndPoints = { { "localhost", 6379 } } });

                if (_redis.IsConnected)
                    _db = _redis.GetDatabase();
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(ex.HResult), ex, ex.Message);
            }
        }

        public void Set(string key, string value)
        {
            _db.StringSet(key, value);
        }
    }
}
