using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Pokemon.Models
{
    public class PokemonDate
    {
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип покемона
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Поколение
        /// </summary>
        public int? Generation { get; set; }

        /// <summary>
        /// Вид покемона
        /// </summary>
        public string KindPokemon { get; set; }

        /// <summary>
        /// Статистика
        /// </summary>
        public PokemonStatistic PokemonStatistic { get; set; }
    }

    public class PokemonDateRedis
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип покемона
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Поколение
        /// </summary>
        public int? Generation { get; set; }

        /// <summary>
        /// Вид покемона
        /// </summary>
        public string KindPokemon { get; set; }

        /// <summary>
        /// Статистика
        /// </summary>
        public PokemonStatistic PokemonStatistic { get; set; }
    }
}
