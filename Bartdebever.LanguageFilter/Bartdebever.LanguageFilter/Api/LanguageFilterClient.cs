using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Bartdebever.LanguageFilter.Api
{
    public class LanguageFilterClient : IFilter
    {
        private readonly FilterSettings _filterSettings;

        /// <summary>
        /// Initialize a new instance of the <see cref="LanguageFilterClient"/> class.
        /// </summary>
        /// <param name="filterSettings">The settings for the filter to use.</param>
        public LanguageFilterClient(FilterSettings filterSettings = null)
        {
            _filterSettings = filterSettings ?? new FilterSettings();
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="LanguageFilterClient"/> class.
        /// </summary>
        /// <param name="filterSettingsFunc">
        /// The function to execute to gain the settings object.
        /// </param>
        public LanguageFilterClient(Func<FilterSettings> filterSettingsFunc)
        {
            _filterSettings = filterSettingsFunc.Invoke();
        }

        /// <inheritdoc />
        public IFilter AddWords(IEnumerable<string> bannedWords)
        {
            _filterSettings.BannedWords = bannedWords;
            return this;
        }

        /// <inheritdoc />
        public IFilter UseMutations(Dictionary<char, char> mutationSet = null)
        {
            _filterSettings.MutationSet = mutationSet;
            _filterSettings.UseMutations = true;

            return this;
        }

        /// <inheritdoc />
        public bool ContainsBannedWords(string text)
        {
            var bannedWords = CreateRegexExpression();

            return Regex.Match(text, bannedWords, RegexOptions.IgnoreCase).Success;
        }

        /// <inheritdoc />
        public string CensorText(string text, char replacement = '*')
        {
            var bannedWords = CreateRegexExpression();

            var regex = new Regex(bannedWords, RegexOptions.IgnoreCase);
            return regex.Replace(text, replacement.ToString());
        }

        private string CreateRegexExpression()
        {
            // Structure of the Regex expression is (word1|word2|word3)
            var bannedWordsStringBuilder = new StringBuilder("(");
            for (var i = 0; i < _filterSettings.BannedWords.Count(); i++)
            {
                var bannedWord = _filterSettings.BannedWords.ElementAt(i);

                // Add the words to the RegEx expression.
                bannedWordsStringBuilder.Append(bannedWord);

                // Loop over the mutation sets from where the value is at least include (might make single words searching lighter)
                foreach (var mutationSet in _filterSettings.MutationSet.Where(keyValuePair => bannedWord.Contains(keyValuePair.Key)))
                {
                    // The string builder is expecting a | character so it's fine to add it before adding the words.
                    bannedWordsStringBuilder.Append("|" + bannedWord.Replace(mutationSet.Key, mutationSet.Value));
                }

                // If this is the last words in the list, skip the | character.
                if (i == _filterSettings.BannedWords.Count() - 1)
                {
                    continue;
                }

                // Its not the last words, add the | character.
                bannedWordsStringBuilder.Append("|");
            }

            // Close down the brackets.
            bannedWordsStringBuilder.Append(")");

            return bannedWordsStringBuilder.ToString();
        }
    }
}
