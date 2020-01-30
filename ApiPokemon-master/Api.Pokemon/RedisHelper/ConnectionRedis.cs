using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Pokemon.Data.Interfaces;
using System.Linq;
using Api.Pokemon.Models;

namespace Api.Pokemon.RedisHelper
{
    public class ConnectionRedis
    {
        private readonly IRedisRepository _redisRepository;

        public ConnectionRedis(IRedisRepository redisRepository)
        {
            _redisRepository = redisRepository;
        }

        public async Task<List<Api.Pokemon.Data.Pokemon>> GetResultFromCashe(DateTime today)
        {
            var resp = await _redisRepository.GetDataFromRedis(today);
            if (resp != null)
            {
                return resp.Select(r => new Api.Pokemon.Data.Pokemon
                {
                    Name = r.Name,
                    Type = r.Type,
                    Generation = r.Generation,
                    KindPokemon = r.KindPokemon,
                    Statistic = new Data.PokemonStatistic
                    {
                        HP = r.PokemonStatistic.HP,
                        Def = r.PokemonStatistic.Def,
                        Damage = r.PokemonStatistic.Damage,
                        Velosity = r.PokemonStatistic.Velocity,
                        CommonStatistic = r.PokemonStatistic.CommonStatistic
                    }
                }).ToList();
            }
            return null;
        }

        public async Task SaveResponceToCashe(List<Data.Pokemon> pokemonToAdd)
        {
            var pokemonToDb = new List<PokemonDateRedis>();
            foreach (var el in pokemonToAdd)
            {
                 var element = new PokemonDateRedis()
                {
                    Name = el.Name,
                    Type = el.Type,
                    Generation = el.Generation,
                    KindPokemon = el.KindPokemon,
                    PokemonStatistic = new Models.PokemonStatistic
                    {
                        HP = el.Statistic.HP,
                        Def = el.Statistic.Def,
                        Damage = el.Statistic.Damage,
                        Velocity = el.Statistic.Velosity,
                        CommonStatistic = el.Statistic.CommonStatistic
                    }
                };
                pokemonToDb.Add(element);
            }

            await _redisRepository.SaveDataToRedis(pokemonToDb);
        }
    }
}
