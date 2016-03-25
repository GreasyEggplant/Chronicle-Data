using com.greasyeggplant.chronicle.data.csvData;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace com.greasyeggplant.chronicle.data.entities
{
    public class Repository
    {
        /*
        private static string GetFileName(string[] args)
        {
            string localdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //return "<localdata>/Jagex Ltd/Chronicle - RuneScape Legends/Chronicle-log-2016-03-24-22-32-48.log";
        }
        */
        public static readonly string GameFolder = "C:/Program Files (x86)/Jagex/Chronicle";
        public static readonly string DataFolder = "Game/Chronicle_Data/Config/CSV";
        
        public static readonly string CardsFileName = "cards.csv";
        public static readonly string LocalizationFileName = "loc.csv";

        private static readonly CsvConfiguration ReaderConfiguration = new CsvConfiguration()
        {
            Delimiter = "\t",
            IsHeaderCaseSensitive = false,
            WillThrowOnMissingField = false
        };

        //TODO: Prevent access to data if not intialized
        public bool IsInitialized { get; private set; }
        public List<Card> Cards { get; private set; }

        private List<CsvCard> CardData { get; set; }
        private List<CsvDescription> Localizations { get; set; }

        public Repository()
        {
            IsInitialized = false;
        }

        public void Initialize()
        {
            //TODO: Error Handling
            ReadCsvs();

            MapData();

            IsInitialized = true;
        }

        private void ReadCsvs()
        {
            CardData = GetList<CsvCard>(CardsFileName);
            Localizations = GetList<CsvDescription>(LocalizationFileName);
        }

        private void MapData()
        {
            Cards = new List<Card>();
            foreach(CsvCard csvCard in CardData)
            {
                Card c = Map(csvCard);
                c.Description = GetLocalization("EN", csvCard.DescId);
                c.EffectDescription = GetLocalization("EN", csvCard.EffectDesc);
                Cards.Add(c);
            }
        }
        
        private string GetLocalization(string language, int? descId)
        {
            if(!descId.HasValue)
            {
                return null;
            }
            CsvDescription localization = Localizations.FirstOrDefault(l => l.Id == descId.Value);
            if(localization == null)
            {
                //TODO: Raise Warning
                return null;
            }
            //TODO: Multiple Languages
            switch (language)
            {
                default:
                    return localization.En;
            }
        }

        private Card Map(CsvCard csvCard)
        {
            //TODO: Use some kind of mapping framework
            return new Card()
            {
                Id = csvCard.Id,
                NameId = csvCard.NameId,
                Name = csvCard.Name,
                Archetype = csvCard.Archetype,
                Type = csvCard.Type,
                Family = csvCard.Family,
                Image = csvCard.Image,
                Attack = csvCard.Attack,
                Health = csvCard.Health,
                GoldCost = csvCard.GoldCost,
                Reward0 = new Reward
                {
                    Value0 = csvCard.Reward0Value0,
                    Value1 = csvCard.Reward0Value1,
                    Type = csvCard.Reward0Type
                },
                Reward1 = new Reward
                {
                    Value0 = csvCard.Reward1Value0,
                    Value1 = csvCard.Reward1Value1,
                    Type = csvCard.Reward1Type
                },
                Reward2 = new Reward
                {
                    Value0 = csvCard.Reward2Value0,
                    Value1 = csvCard.Reward2Value1,
                    Type = csvCard.Reward2Type
                },
                Rarity = csvCard.Rarity,
                Source = csvCard.Source,
                Artist = csvCard.Artist,
                HitSplat = csvCard.HitSplat
            };
        }

        private List<T> GetList<T>(string filename)
        {
            string filepath = Path.Combine(GameFolder, DataFolder, filename);
            using (FileStream fileStream = File.OpenRead(filepath))
            using (StreamReader streamReader = new StreamReader(fileStream))
            using (CsvReader reader = new CsvReader(streamReader, ReaderConfiguration))
            {
                return reader.GetRecords<T>().ToList();
            }
        }
    }
}
