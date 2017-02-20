using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber
{
    public static class StringHelper
    {
        public static Int32 NumberOfOccurences(string argSource, string argSearch)
        {
            // Check for empty source or search string
            if (String.IsNullOrWhiteSpace(argSource) || String.IsNullOrWhiteSpace(argSearch))
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
                        break;
                    }
                }
                if (foundOccurence)
                {
                    occurences++;
                }
                currentSourceIndex += (i + 1);
                if (currentSourceIndex > sourceLength - 1)
                {
                    break;
                }
            }
            return occurences;
        }

    }
}
