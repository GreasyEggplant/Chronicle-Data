using com.greasyeggplant.chronicle.data.csvData;

namespace com.greasyeggplant.chronicle.data.entities
{
    public class CardFactory
    {
        public Localizer Localizer { get; set; }

        public Card CreateCard(CsvCard csvCard)
        {
            Card c = null;
            switch ((CardType)csvCard.Type)
            {
                case CardType.Combat:
                    c = new CombatCard()
                    {
                        Attack = csvCard.Attack.Value,
                        Health = csvCard.Health.Value
                    };
                    break;
                case CardType.Support:
                    c = new SupportCard()
                    {
                        GoldCost = csvCard.GoldCost.Value
                    };
                    break;
                case CardType.None:
                default:
                    return null;
            }
            MapValues(c, csvCard);

            return c;
        }

        private Card MapValues(Card c, CsvCard csvCard)
        {
            //TODO: Use some kind of mapping framework
            c.Id = csvCard.Id;
            c.NameId = csvCard.NameId;
            c.Name = csvCard.Name;
            c.Archetype = (LegendType)csvCard.Archetype;
            c.Family = csvCard.Family;
            c.Image = csvCard.Image;
            c.Reward0 = new Reward
            {
                Value0 = csvCard.Reward0Value0,
                Value1 = csvCard.Reward0Value1,
                Type = csvCard.Reward0Type
            };
            c.Reward1 = new Reward
            {
                Value0 = csvCard.Reward1Value0,
                Value1 = csvCard.Reward1Value1,
                Type = csvCard.Reward1Type
            };
            c.Reward2 = new Reward
            {
                Value0 = csvCard.Reward2Value0,
                Value1 = csvCard.Reward2Value1,
                Type = csvCard.Reward2Type
            };
            c.Rarity = csvCard.Rarity;
            c.Source = csvCard.Source;
            c.Artist = csvCard.Artist;
            c.HitSplat = csvCard.HitSplat;

            c.Description = GetLocalization(Language.English, csvCard.DescId);
            c.EffectDescription = GetLocalization(Language.English, csvCard.EffectDesc);

            return c;
        }

        private string GetLocalization(Language language, int? descId)
        {
            if (!descId.HasValue)
            {
                return null;
            }
            return Localizer.GetLocalization(language, descId.Value);
        }
    }
}
