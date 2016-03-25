namespace com.greasyeggplant.chronicle.data.entities
{
    public abstract class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LegendType Archetype { get; set; }
        public abstract CardType Type { get; }
        public Family Family { get; set; }
        public string Image { get; set; }
        public Reward Reward0 { get; set; }
        public Reward Reward1 { get; set; }
        public Reward Reward2 { get; set; }
        public Rarity Rarity { get; set; }
        public string Description { get; set; }
        public string EffectDescription { get; set; }
        public int Source { get; set; }
        public string Artist { get; set; }
        public int HitSplat { get; set; }
    }
}
