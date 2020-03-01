using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace gSubber.Utilities
{
    public class TextFinder
    {
        public enum SearchMode
        {
            Normal,
            Extended,
            RegularExpression
        }

        private static char[] _WordSeparators = new char[] { '.', ',', ' ', '?', '"', '\'', '\r', '\n', '\t', ':', ';', '<', '>', '/', '\\', '[', ']', '{', '}', '(', ')', '-', '_', '+', '=', '!', '@', '#', '$', '%', '^', '&', '*', '`', '~', '|' };

        private static string _RegExWordSeparators = "[., ?\"'\\r\\n\\t:;<>/\\\\\\[\\]{}\\(\\)-_+=!@#$%^&*`~|]";

        public bool Find(string textToFind, bool argNext, bool matchCase, bool matchWholeWord, bool wrapAround, SearchMode searchmode, 
            int startRowIndex, int startTextIndex, IList<string> rowList, Action<int> selectRowAction, Action<int, int> selectTextAction)
        {
            if (string.IsNullOrWhiteSpace(textToFind))
            {
                throw new ArgumentNullException(nameof(textToFind));                
            }

            switch (searchmode)
            {
                case SearchMode.Normal:
                    if (!matchCase)
                    {
                        textToFind = textToFind.ToLower();
                    }
                    break;
                case SearchMode.Extended:
                    textToFind = textToFind.Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t");
                    if (!matchCase)
                    {
                        textToFind = textToFind.ToLower();
                    }
                    break;
                case SearchMode.RegularExpression:
                default:
                    break;
            }

            // If we have RegularExpression search mode, create the Regular Expression
            Regex regex = null;
            if (searchmode == SearchMode.RegularExpression)
            {
                RegexOptions regexOptions = RegexOptions.Compiled;
                string regexTextToFind = textToFind;

                // Check if we have match case
                if (!matchCase)
                {
                    regexOptions |= RegexOptions.IgnoreCase;
                }

                // Check if we have match whole word
                if (matchWholeWord)
                {
                    regexTextToFind = $@"(?<={_RegExWordSeparators}|^){textToFind}(?={_RegExWordSeparators}|$)";
                }

                regex = new Regex(regexTextToFind, regexOptions);
            }

            // If no startRowIndex, start from the first one
            if (startRowIndex < 0)
            {
                startRowIndex = 0;
                startTextIndex = 0;
            }

            // Set the current row index
            int currentRowIndex = startRowIndex;

            // Set a flag to know if the find operation succeeded
            bool textWasFound = false;

            // Set a flag to know if we have wrapped around
            bool wrappedAround = false;

            int rowCount = rowList.Count();

            // Start searching the rows according to the search direction
            // argNext == true => Search down
            // argNext == false => Search up
            while (
                (argNext && currentRowIndex < rowCount)
                || (!argNext && currentRowIndex >= 0)
                )
            {
                // Check if we wrapped around and surpassed the start row
                if (wrappedAround)
                {
                    if (
                        (argNext && currentRowIndex >= startRowIndex)
                        || (!argNext && currentRowIndex <= startRowIndex)
                        )
                    {
                        // We wrapped around and surpassed tha starting row!
                        // Break the search loop!
                        break;
                    }
                }

                // Get the subtitle item to search
                string subText = rowList[currentRowIndex];

                // Check if we have regular expression or normal/extended search mode
                if (searchmode == SearchMode.RegularExpression)
                {
                    Match match;
                    if (
                        // Check if we are in the start row and then search from the startTextIndex
                        currentRowIndex == startRowIndex && (match = regex.Match(subText, startTextIndex)).Success
                        || currentRowIndex != startRowIndex && (match = regex.Match(subText)).Success
                        )
                    {
                        // Found match! Select the row
                        selectRowAction(currentRowIndex);

                        // Select the text
                        selectTextAction(match.Index, match.Length);

                        // Set the flag that the find operation was successfull
                        textWasFound = true;

                        // Exit the search loop
                        break;
                    }
                }
                else
                {
                    int startIndex;
                    if (!matchCase)
                    {
                        subText = subText.ToLower();
                    }
                    if (
                        // Check if we are in the start row and then search from the startTextIndex
                        currentRowIndex == startRowIndex &&
                            (
                                // Check if we have match case and if not convert text to lower case to search
                                (startIndex = subText.IndexOf(textToFind, startTextIndex)) > -1
                            )
                        // Check if we are in the start row and then search from the startTextIndex
                        || currentRowIndex != startRowIndex &&
                            (
                                // Check if we have match case and if not convert text to lower case to search
                                (startIndex = subText.IndexOf(textToFind)) > -1
                            )
                        )
                    {
                        if (
                            // Check if we have match whole word
                            !matchWholeWord
                            ||
                            (
                                // Check if the previous and next characters are in the word separators array
                                (
                                    startIndex == 0
                                    || (startIndex > 0 && _WordSeparators.Contains(subText[startIndex - 1]))
                                )
                                &&
                                (
                                    startIndex + textToFind.Length == subText.Length
                                    || (startIndex + textToFind.Length < subText.Length && _WordSeparators.Contains(subText[startIndex + textToFind.Length]))
                                )
                            )
                        )
                        {
                            // Found match! Select the row
                            selectRowAction(currentRowIndex);

                            // Select the text
                            selectTextAction(startIndex, textToFind.Length);

                            // Set the flag that the find operation was successfull
                            textWasFound = true;

                            // Exit the search loop
                            break;
                        }
                    }
                }

                // Go the next row according to search direction
                if (argNext)
                {
                    currentRowIndex++;
                    // We check if we have wrap around AND we haven't already wrapped around
                    if (wrapAround && !wrappedAround)
                    {
                        if (currentRowIndex >= rowCount)
                        {
                            // We wrap around and start from the first row
                            currentRowIndex = 0;
                            // We set the flag to know that we wrapped around
                            wrappedAround = true;
                        }
                    }
                }
                else
                {
                    currentRowIndex--;
                    // We check if we have wrap around AND we haven't already wrapped around
                    if (wrapAround && !wrappedAround)
                    {
                        if (currentRowIndex < 0)
                        {
                            // We wrap around and start from the last row
                            currentRowIndex = rowCount - 1;
                            // We set the flag to know that we wrapped around
                            wrappedAround = true;
                        }
                    }
                }
            }

            return textWasFound;            
        }
    }
}
