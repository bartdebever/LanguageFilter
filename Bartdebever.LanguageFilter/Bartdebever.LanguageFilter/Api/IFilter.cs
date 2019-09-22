using System;
using System.Collections.Generic;
using System.Text;

namespace Bartdebever.LanguageFilter.Api
{
    public interface IFilter
    {
        /// <summary>
        /// Adds a list of banned words.
        /// </summary>
        /// <param name="bannedWords">The list of banned words.</param>
        /// <returns>The filter to continue with.</returns>
        IFilter AddWords(IEnumerable<string> bannedWords);

        /// <summary>
        /// Defines the replacement characters to use for the mutations.
        /// </summary>
        /// <param name="mutationSet">The set of replacement characters.</param>
        /// <returns>The filter to continue with.</returns>
        IFilter UseMutations(Dictionary<char, char> mutationSet = null);

        /// <summary>
        /// Checks if a piece of text contains one of the banned words.
        /// </summary>
        /// <param name="text">The text to check.</param>
        /// <returns>If the <paramref name="text"/> contains banned words.</returns>
        bool ContainsBannedWords(string text);

        /// <summary>
        /// Censors the provided <paramref name="text"/> using the <paramref name="replacement"/> character.
        /// </summary>
        /// <param name="text">The text to censor.</param>
        /// <param name="replacement">The character to replace banned text with.</param>
        /// <returns>The censored text.</returns>
        string CensorText(string text, char replacement = '*');
    }
}
