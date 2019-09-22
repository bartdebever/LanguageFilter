using System;
using System.Collections.Generic;
using System.Text;

namespace Bartdebever.LanguageFilter.Api
{
    public class FilterSettings
    {
        /// <summary>
        /// The list of banned words to be removed.
        /// </summary>
        public IEnumerable<string> BannedWords { get; set; }

        /// <summary>
        /// Defines if the mutations should be executed.
        /// </summary>
        public bool UseMutations { get; set; }

        /// <summary>
        /// A set of character to replace another character with.
        /// </summary>
        public Dictionary<char, char> MutationSet { get; set; }

        /// <summary>
        /// Defines the character to replace words with.
        /// </summary>
        public char ReplacementCharacter { get; set; }
    }
}
