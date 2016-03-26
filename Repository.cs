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
        public static readonly string GameFolder = "Jagex/Chronicle";
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
        
        public LocalizedString GetLocalization(int descId)
        {
            CsvDescription localization = Localizations.FirstOrDefault(l => l.Id == descId);
            if(localization == null)
            {
                //TODO: Raise Warning
                return null;
            }
            return new LocalizedString
            {
                Id = descId,
                Text = new Dictionary<Language, string>
                {
                    { Language.English, localization.En },
                    { Language.French, localization.Fr },
                    { Language.German, localization.De },
                    { Language.Spanish, localization.Es },
                }
            };
        }

        private List<T> GetList<T>(string filename)
        {
            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), GameFolder, DataFolder, filename);
            using (FileStream fileStream = File.OpenRead(filepath))
            using (StreamReader streamReader = new StreamReader(fileStream))
            using (CsvReader reader = new CsvReader(streamReader, ReaderConfiguration))
            {
                return reader.GetRecords<T>().ToList();
            }
        }
    }
}
