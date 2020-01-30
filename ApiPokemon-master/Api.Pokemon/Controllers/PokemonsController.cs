using System;
using System.Threading.Tasks;
using Api.Pokemon.Business;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Api.Pokemon.RedisHelper;
using Api.Pokemon.Data.Interfaces;

namespace Api.Pokemon.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class PokemonsController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;

        private readonly IRedisRepository _redisRepository;

        public PokemonsController(IPokemonRepository pokemonRepository, IRedisRepository redisRepository)
        {
            _pokemonRepository = pokemonRepository;
            _redisRepository = redisRepository;
        }

        // GET: Получение всех покемонов
        [HttpGet]
        public async Task<Data.PokemonResponce> GetInfoAboutPokemons()
        {
            var responce = new Data.PokemonResponce();
            var casheProcess = new ConnectionRedis(_redisRepository);
            var cache = casheProcess.GetResultFromCashe(DateTime.Today);

            if(cache.Result != null)
            {
                responce.Pokemons = cache.Result;
                return responce;
            }
            var apiProcess = new PokemonBusiness(_pokemonRepository);
            responce.Pokemons = await apiProcess.GetAllInfo();

            if(responce != null)
            {
                await casheProcess.SaveResponceToCashe(responce.Pokemons);
                return responce;
            }

            return null;
        }

        // PUT: Добавить информацию о покемоне
        /*   {
            "Type": "Electric",
            "Generation": 5,
            "Name": "Pickachu",
            "KindPokemon": "mouse",
            "Statistic": {
                "HP": 40,
                "Damage": 35,
                "Def": 40,
                "Velocity": 45,
                "CommonStatistic": 200
            }
    }*/
        [HttpPut]
        public async Task<HttpStatusCode> AddPokemon([FromBody]Data.Pokemon request)
        {
             var apiProcess = new PokemonBusiness(_pokemonRepository);
             var resp = await apiProcess.Add(request);

             return HttpStatusCode.OK;
        }


        // POST: Найти покемона по критерию
            /*  {
                    "Сriterion" : "Generation",
                    "Value" : "3"
                 }
             }*/
        [HttpPost]
        public async Task<Data.PokemonResponce> SearchPokemon([FromBody]Data.SearchPokemon request)
        {
            var responce = new Data.PokemonResponce();

            var apiProcess = new PokemonBusiness(_pokemonRepository);
            responce.Pokemons = await apiProcess.SearchOnCreateria(request.Сriterion, request.Value);
            return responce;
        }
    }
}