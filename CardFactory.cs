using com.greasyeggplant.chronicle.data.csvData;
using com.greasyeggplant.chronicle.data.entities.enums;

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
            c.Archetype = (LegendType)csvCard.Archetype;
            c.Family = (Family) csvCard.Family.GetValueOrDefault(-1);
            c.Image = csvCard.Image;
            c.Rarity = (Rarity)csvCard.Rarity;
            c.Source = (SourceType)csvCard.Source;
            c.Artist = csvCard.Artist;
            c.HitSplat = csvCard.HitSplat;

            c.Name = GetLocalization(csvCard.NameId);
            c.Description = GetLocalization(csvCard.DescId);
            c.EffectDescription = GetLocalization(csvCard.EffectDesc);

            c.Reward0 = CreateReward(csvCard.Reward0Type, csvCard.Reward0Value0, csvCard.Reward0Value1);
            c.Reward1 = CreateReward(csvCard.Reward1Type, csvCard.Reward1Value0, csvCard.Reward1Value1);
            c.Reward2 = CreateReward(csvCard.Reward2Type, csvCard.Reward2Value0, csvCard.Reward2Value1);

            return c;
        }

        private Reward CreateReward(int type, int? value0, int? value1)
        {
            RewardType rewardType = (RewardType)type;
            switch (rewardType)
            {
                case RewardType.Armor:
                case RewardType.Health:
                case RewardType.Gold:
                case RewardType.Attack:
                    return new Reward { Type = rewardType, Value0 = value0.Value + 1 };
                case RewardType.Weapon:
                    return new Reward { Type = rewardType, Value0 = value0.Value + 1, Value1 = value1.Value + 1 };
                case RewardType.None:
                default:
                    return null;
            }
        }

        private LocalizedString GetLocalization(int? descId)
        {
            if (!descId.HasValue)
            {
                return null;
            }
            return Localizer.GetLocalization(descId.Value);
        }
    }
}
