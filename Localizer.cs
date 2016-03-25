namespace com.greasyeggplant.chronicle.data.entities
{
    public interface Localizer
    {
        string GetLocalization(Language language, int descId);
    }
}
