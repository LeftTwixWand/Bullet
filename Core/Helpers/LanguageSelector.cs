using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class LanguageSelector
    {
        public static Dictionary<LanguageEnum, string> Languages = new Dictionary<LanguageEnum, string>()
        {
            { LanguageEnum.Russian, "ru" },
            { LanguageEnum.English, "en" },
        };
    }

    public enum LanguageEnum
    {
        Russian,
        English
    }
}
