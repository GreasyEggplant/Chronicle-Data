namespace com.greasyeggplant.chronicle.data.csvData
{
    public class CsvCard
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
        public int? Reward0Value0 { get; set; }
        public int? Reward0Value1 { get; set; }
        public int Reward0Type { get; set; }
        public int? Reward1Value0 { get; set; }
        public int? Reward1Value1 { get; set; }
        public int Reward1Type { get; set; }
        public int? Reward2Value0 { get; set; }
        public int? Reward2Value1 { get; set; }
        public int Reward2Type { get; set; }
        public int Rarity { get; set; }
        public int DescId { get; set; }
        public int? EffectDesc { get; set; }
        public int Source { get; set; }
        public string Artist { get; set; }
        public int HitSplat { get; set; }
    }
}
