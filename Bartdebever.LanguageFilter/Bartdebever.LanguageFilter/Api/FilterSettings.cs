using System;
using System.Collections.Generic;
using System.Text;

namespace Bartdebever.LanguageFilter.Api
{
    public class FilterSettings
    {
        public IEnumerable<string> BannedWords { get; set; }

        public bool UseReplacementSet { get; set; }

        public Dictionary<char, char> MutationSet { get; set; }

        public char ReplacementCharacter { get; set; }
    }
}
