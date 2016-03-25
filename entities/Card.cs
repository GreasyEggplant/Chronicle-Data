using com.greasyeggplant.chronicle.data.entities.enums;

namespace com.greasyeggplant.chronicle.data.entities
{
    public abstract class Card
    {
        public abstract CardType Type { get; }
        public int Id { get; set; }
        public LocalizedString Name { get; set; }
        public LegendType Archetype { get; set; }
        public Family Family { get; set; }
        public string Image { get; set; }
        public Reward Reward0 { get; set; }
        public Reward Reward1 { get; set; }
        public Reward Reward2 { get; set; }
        public Rarity Rarity { get; set; }
        public LocalizedString Description { get; set; }
        public LocalizedString EffectDescription { get; set; }
        public SourceType Source { get; set; }
        public string Artist { get; set; }
        public int HitSplat { get; set; }
    }
}
