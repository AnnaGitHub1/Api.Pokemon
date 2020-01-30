using Api.Pokemon.Data.Exceptions;
using Api.Pokemon.Data.Interfaces;
using Api.Pokemon.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Pokemon.Business
{
    public class PokemonBusiness
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonBusiness(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        public async Task<List<Api.Pokemon.Data.Pokemon>> GetAllInfo()
        {
            var pokemon = new List<Api.Pokemon.Data.Pokemon>();
            var resp = await _pokemonRepository.GetAllCollection();
            if (resp.Any())
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

        public async Task<bool> Add(Data.Pokemon pokemonToAdd)
        {
            var pokemonToDb = new PokemonDate()
            {
                Name = pokemonToAdd.Name,
                Type = pokemonToAdd.Type,
                Generation = pokemonToAdd.Generation,
                KindPokemon = pokemonToAdd.KindPokemon,
                PokemonStatistic = new Models.PokemonStatistic
                {
                    HP = pokemonToAdd.Statistic.HP,
                    Def = pokemonToAdd.Statistic.Def,
                    Damage = pokemonToAdd.Statistic.Damage,
                    Velocity = pokemonToAdd.Statistic.Velosity,
                    CommonStatistic = pokemonToAdd.Statistic.CommonStatistic
                }
            };
            var add = await _pokemonRepository.MakeRecord(pokemonToDb);
            if (add)
            {
                return true;
            }

            throw new AddingException();
        }

        public async Task<List<Api.Pokemon.Data.Pokemon>> SearchOnCreateria(string where, string what)
        {
            List<PokemonDate> pokemon = await _pokemonRepository.FindPokemon(where, what);

            return pokemon.Select(r => new Api.Pokemon.Data.Pokemon
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

    }

}
