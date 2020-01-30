using System.Collections.Generic;
namespace Api.Pokemon.Data
{
    public class PokemonResponce
    {
        public List<Pokemon> Pokemons { get; set; }
    }

    public class Pokemon
    { 
        /// <summary>
        /// Тип покемона
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Поколение
        /// </summary>
        public int? Generation { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Вид покемона
        /// </summary>
        public string KindPokemon { get; set; }

        /// <summary>
        /// Статистика
        /// </summary>
        public PokemonStatistic Statistic { get; set; }
    }

    public struct PokemonStatistic
    {
        /// <summary>
        /// Очки здоровья
        /// </summary>
        public int? HP { get; set; }

        /// <summary>
        /// Урон
        /// </summary>
        public int? Damage { get; set; }

        /// <summary>
        /// Защита
        /// </summary>
        public int? Def { get; set; }

        /// <summary>
        /// Скорость
        /// </summary>
        public int? Velosity { get; set; }

        /// <summary>
        /// Общие очки 
        /// </summary>
        public int? CommonStatistic { get; set; }
    }
}
