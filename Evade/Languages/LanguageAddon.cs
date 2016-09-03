using System.Collections.Generic;

namespace Evade.Languages
{
    public abstract class LanguageAddon
    {
        public Dictionary<Config.ConfigValue, string> LangDictionary { get; set; }
    }
}