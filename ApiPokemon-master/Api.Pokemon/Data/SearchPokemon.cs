using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Api.Pokemon.Data
{
    public class SearchPokemon
    {
        [DataMember, XmlAttribute]
        public string Сriterion { get; set; }

        [DataMember, XmlAttribute]
        public string Value { get; set; }
    }
}
