using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Api.Pokemon.Data.Repository
{
    public class RedisContext
    {
        private readonly IDatabase _database = null;

        public RedisContext(IOptions<RedisSettings> settings)
        {
            var client =  ConnectionMultiplexer.Connect(settings.Value.ConnectionString);
            if(client != null)
            {
                _database = client.GetDatabase();
            }
        }

        public IDatabase RedisDb
        {
            get
            {
                return _database;
            }
        }
    }
}
