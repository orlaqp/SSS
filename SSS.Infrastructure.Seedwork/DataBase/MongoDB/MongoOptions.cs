using Microsoft.Extensions.Options;

namespace SSS.Infrastructure.Seedwork.DataBase.MongoDB
{
    public class MongoOptions : IOptions<MongoOptions>
    {
        public MongoOptions Value => this;

        public string host { set; get; }

        public int port { set; get; }
    }
}
