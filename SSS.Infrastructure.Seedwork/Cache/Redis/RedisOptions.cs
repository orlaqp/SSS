using Microsoft.Extensions.Options;

namespace SSS.Infrastructure.Seedwork.Cache.Redis
{
    public class RedisOptions : IOptions<RedisOptions>
    {
        public RedisOptions Value => this;

        public string host { set; get; }

        public int port { set; get; }
    }
}
