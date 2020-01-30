using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Pokemon.Models;

namespace Api.Pokemon.Data.Interfaces
{
    public interface IRedisRepository
    {
        Task SaveDataToRedis(List<PokemonDateRedis> dataToCash);

        Task<List<PokemonDateRedis>> GetDataFromRedis(DateTime today);
    }
}
