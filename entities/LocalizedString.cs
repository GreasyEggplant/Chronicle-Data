using System.Collections.Generic;

namespace com.greasyeggplant.chronicle.data.entities
{
    public class LocalizedString
    {
        public int Id { get; set; }
        public Dictionary<Language, string> Text { get; set; }

        public string GetText(Language language) { return Text[language]; }

        public override string ToString()
        {
            //TODO: Support better localization
            return GetText(Language.English);
        }
    }
}
