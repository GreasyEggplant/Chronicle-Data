namespace com.greasyeggplant.chronicle.data.entities
{
    public class SupportCard : Card
    {
        public override CardType Type { get { return CardType.Support; } }
        public int GoldCost { get; set; }
    }
}
