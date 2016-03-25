using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.greasyeggplant.chronicle.data.entities
{
    public interface Localizer
    {
        string GetLocalization(Language language, int descId);
    }
}
