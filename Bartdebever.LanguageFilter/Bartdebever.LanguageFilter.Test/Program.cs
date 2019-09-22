using System;
using System.Collections.Generic;

namespace Bartdebever.LanguageFilter.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var filter = new Api.LanguageFilter();
            filter.AddWords(new List<string>()
            {
                "darn",
                "gosh",
                "yikes"
            });
            filter.UseReplacements(new Dictionary<char, char>()
            {
                {'i', '1'}
            });

            var badText = "y1kes oh gosh darnit";
            var containsBannedWords = filter.ContainsBannedWords(badText);
            Console.WriteLine($"Banned words found: {containsBannedWords}");

            if (containsBannedWords)
            {
                Console.WriteLine(filter.CensorText(badText));
            }

            Console.ReadLine();
        }
    }
}
