using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Bartdebever.LanguageFilter.Api
{
    public class LanguageFilter : IFilter
    {
        private readonly FilterSettings _filterSettings;

        public LanguageFilter(FilterSettings filterSettings = null)
        {
            _filterSettings = filterSettings ?? new FilterSettings();
        }

        public LanguageFilter(Func<FilterSettings> filterSettingsFunc)
        {
            _filterSettings = filterSettingsFunc.Invoke();
        }

        public IFilter AddWords(IEnumerable<string> bannedWords)
        {
            _filterSettings.BannedWords = bannedWords;
            return this;
        }

        public IFilter UseReplacements(Dictionary<char, char> replacementSet = null)
        {
            _filterSettings.MutationSet = replacementSet;
            _filterSettings.UseReplacementSet = true;

            return this;
        }

        public bool ContainsBannedWords(string text)
        {
            var bannedWords = CreateRegexExpression();

            return Regex.Match(text, bannedWords, RegexOptions.IgnoreCase).Success;
        }

        public string CensorText(string text, char replacement = '*')
        {
            var bannedWords = CreateRegexExpression();

            var regex = new Regex(bannedWords, RegexOptions.IgnoreCase);
            return regex.Replace(text, replacement.ToString());
        }

        private string CreateRegexExpression()
        {
            var bannedWordsStringBuilder = new StringBuilder(@"(");
            for (var i = 0; i < _filterSettings.BannedWords.Count(); i++)
            {
                var bannedWord = _filterSettings.BannedWords.ElementAt(i);
                bannedWordsStringBuilder.Append(bannedWord);

                foreach (var mutationSet in _filterSettings.MutationSet.Where(keyValuePair => bannedWord.Contains(keyValuePair.Key)))
                {
                    bannedWordsStringBuilder.Append("|" + bannedWord.Replace(mutationSet.Key, mutationSet.Value));
                }

                if (i == _filterSettings.BannedWords.Count() - 1)
                {
                    continue;
                }

                bannedWordsStringBuilder.Append("|");
            }

            bannedWordsStringBuilder.Append(@")");

            return bannedWordsStringBuilder.ToString();
        }
    }
}
