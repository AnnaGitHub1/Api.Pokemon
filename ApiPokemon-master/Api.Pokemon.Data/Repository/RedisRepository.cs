using Api.Pokemon.Data.Interfaces;
using Api.Pokemon.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Pokemon.Data.Repository
{
    public class RedisRepository : IRedisRepository
    {
        private readonly RedisContext _context = null;

        public RedisRepository(IOptions<RedisSettings> settings)
        {
            _context = new RedisContext(settings);
        }

        public async Task SaveDataToRedis(List<PokemonDateRedis> dataToCash)
        {
            var serialize = JsonConvert.SerializeObject(dataToCash);
            await _context.RedisDb.StringSetAsync($"PokemonKey {DateTime.Today}", serialize);
        }

        public async Task<List<PokemonDateRedis>> GetDataFromRedis(DateTime today)
        {
            var serialize = await _context.RedisDb.StringGetAsync($"PokemonKey {today}");
            if (serialize.IsNull)
            {
                return null;
            }
            var pokemons = JsonConvert.DeserializeObject<List<PokemonDateRedis>>(serialize);
            return pokemons;
        }
    }
}
