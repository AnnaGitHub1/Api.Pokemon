using Api.Pokemon.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Pokemon.Data.Interfaces
{
    public interface IPokemonRepository
    {
        Task<List<PokemonDate>> GetAllCollection();

        Task<bool> MakeRecord(PokemonDate pokemon);

        Task<List<PokemonDate>> FindPokemon(string whereFind, string whatFind);
    }
}
