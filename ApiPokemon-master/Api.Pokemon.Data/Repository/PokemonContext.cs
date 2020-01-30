using Api.Pokemon.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api.Pokemon.Data
{
    public class PokemonContext
    {
        private readonly IMongoDatabase _database = null;

        public PokemonContext(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<PokemonDate> Pokemons
        {
            get
            {
                return _database.GetCollection<PokemonDate>("Pokemons");
            }
        }
    }
}
