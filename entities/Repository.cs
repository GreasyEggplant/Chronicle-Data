using com.greasyeggplant.chronicle.data.csvData;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace com.greasyeggplant.chronicle.data.entities
{
    public class Repository : Localizer
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

        private CardFactory CardFactory { get; set; }
        private List<CsvCard> CardData { get; set; }
        private List<CsvDescription> Localizations { get; set; }

        public Repository()
        {
            CardFactory = new CardFactory();
            CardFactory.Localizer = this;

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
                Card c = CardFactory.CreateCard(csvCard);
                if (c != null)
                {
                    Cards.Add(c);
                }
            }
        }
        
        public string GetLocalization(Language language, int descId)
        {
            CsvDescription localization = Localizations.FirstOrDefault(l => l.Id == descId);
            if(localization == null)
            {
                //TODO: Raise Warning
                return null;
            }
            switch (language)
            {
                case Language.French:
                    return localization.Fr;
                case Language.German:
                    return localization.De;
                case Language.Spanish:
                    return localization.Es;
                case Language.English:
                default:
                    return localization.En;
            }
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
