using System;
using System.Collections.Generic;
using System.Text;

namespace Bartdebever.LanguageFilter.Api
{
    public interface IFilter
    {
        IFilter AddWords(IEnumerable<string> bannedWords);

        IFilter UseReplacements(Dictionary<char, char> replacementSet = null);

        bool ContainsBannedWords(string text);

        string CensorText(string text, char replacement = '*');
    }
}
