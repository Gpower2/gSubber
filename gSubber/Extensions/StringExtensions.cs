using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber.Extensions
{
    public static class StringExtensions
    {
        public static Int32 NumberOfOccurences(string argSource, string argSearch)
        {
            // Check for empty source or search string
            if (String.IsNullOrEmpty(argSource) || String.IsNullOrEmpty(argSearch))
            {
                return 0;
            }
            // Check if search string is longer than the source string
            if (argSearch.Length > argSource.Length)
            {
                return 0;
            }

            Int32 occurences = 0, currentSourceIndex = 0, sourceLength = argSource.Length, searchLength = argSearch.Length, i = 0;
            bool foundOccurence = true;
            while (true)
            {
                foundOccurence = true;
                for (i = 0; i < searchLength; i++)
                {
                    if (argSource[currentSourceIndex + i] != argSearch[i])
                    {
                        foundOccurence = false;
                        i++;
                        break;
                    }
                }
                if (foundOccurence)
                {
                    occurences++;
                }
                currentSourceIndex += i;
                if (currentSourceIndex > sourceLength - 1)
                {
                    break;
                }
            }
            return occurences;
        }
    }
}
