using MongoDB.Driver;
using System.Collections.Generic;
using Api.Pokemon.Models;
using System.Threading.Tasks;
using MongoDB.Bson;
using Api.Pokemon.Data.Interfaces;
using Microsoft.Extensions.Options;

namespace Api.Pokemon.Data
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly PokemonContext _context = null;

        public PokemonRepository(IOptions<MongoSettings> settings)
        {
            _context = new PokemonContext(settings);
        }

        /// <summary>
        /// Вывести всю информацию из коллекции
        /// </summary>
        /// <returns></returns>
        public async Task<List<PokemonDate>> GetAllCollection()
        {
            var filter = new BsonDocument();
            var pokemons = await this._context.Pokemons.Find(filter).ToListAsync();
            return pokemons;
        }

        /// <summary>
        /// Сделать запись в коллекцию
        /// </summary>
        /// <param name="pokemon"></param>
        /// <returns></returns>
        public async Task<bool> MakeRecord(PokemonDate pokemon)
        {
            var pokemonInDb = await this._context.Pokemons.Find(c => c.Name == pokemon.Name).ToListAsync();

            if (pokemonInDb.Count == 0)
            {

                var pokemonToDb = new PokemonDate
                {
                    Name = pokemon.Name,
                    Type = pokemon.Type,
                    Generation = pokemon.Generation,
                    KindPokemon = pokemon.KindPokemon,
                    PokemonStatistic = new PokemonStatistic
                    {
                        HP = pokemon.PokemonStatistic.HP,
                        Damage = pokemon.PokemonStatistic.Damage,
                        Def = pokemon.PokemonStatistic.Def,
                        Velocity = pokemon.PokemonStatistic.Velocity,
                        CommonStatistic = pokemon.PokemonStatistic.CommonStatistic
                    }
                };

                await this._context.Pokemons.InsertOneAsync(pokemonToDb);

                if (pokemonToDb.Id != null)
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// Поиск элемента
        /// </summary>
        /// <param name="whereFind"></param>
        /// <param name="whatFind"></param>
        /// <returns></returns>
        public async Task<List<PokemonDate>> FindPokemon(string whereFind, string whatFind)
        {
            var filter = Builders<PokemonDate>.Filter.Eq(whereFind, whatFind);
            var pokemon = await this._context.Pokemons.Find(filter).ToListAsync();
            return pokemon;
        }
    }
}
