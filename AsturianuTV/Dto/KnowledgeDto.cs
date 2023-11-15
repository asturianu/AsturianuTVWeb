using AsturianuTV.Infrastructure.Data.Models.BaseKnowledges;
using System.Collections.Generic;

namespace AsturianuTV.Dto
{
    public class KnowledgeDto
    {
        public List<Character> AgilityHeroes { get; set; }
        public List<Character> StrenghtHeroes { get; set; }
        public List<Character> IntHeroes { get; set; }
        public List<Character> UniversalHeroes { get; set; }
        public List<Item> DefaultItems { get; set; }
        public List<Item> NeutralItems { get; set; }
    }
}
