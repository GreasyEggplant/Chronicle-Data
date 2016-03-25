using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.greasyeggplant.chronicle.data.entities
{
    public class Card
    {
        public int Id { get; set; }
        public int NameId { get; set; }
        public string Name { get; set; }
        public int Archetype { get; set; }
        public int Type { get; set; }
        public int? Family { get; set; }
        public string Image { get; set; }
        public int? Attack { get; set; }
        public int? Health { get; set; }
        public int? GoldCost { get; set; }
        public Reward Reward0 { get; set; }
        public Reward Reward1 { get; set; }
        public Reward Reward2 { get; set; }
        public int Rarity { get; set; }
        public string Description { get; set; }
        public string EffectDescription { get; set; }
        public int Source { get; set; }
        public string Artist { get; set; }
        public int HitSplat { get; set; }
    }
}
