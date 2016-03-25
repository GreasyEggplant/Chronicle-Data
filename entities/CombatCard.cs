namespace com.greasyeggplant.chronicle.data.entities
{
    public class CombatCard : Card
    {
        public override CardType Type { get { return CardType.Combat; } }
        public int Attack { get; set; }
        public int Health { get; set; }
    }
}
