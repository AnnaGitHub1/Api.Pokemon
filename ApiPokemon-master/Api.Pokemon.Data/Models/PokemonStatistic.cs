using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Api.Pokemon.Models
{
     public class PokemonStatistic
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
        public int? Velocity { get; set; }

        /// <summary>
        /// Общие очки 
        /// </summary>
        public int? CommonStatistic { get; set; }
    }
}
