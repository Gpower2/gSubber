using gSubber.Core;
using gSubber.Core.SubtitleFile;
using gSubber.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace gSubber.Formats
{
    public class SrtFileParser : ISubFileParser
    {
        private enum ParsingState
        {
            None,
            SubtitleNumber,
            SubtitleTime,
            SubtitleText,
        }

        public SubFileParserResults Load(string argFilename, Encoding argFileEncoding)
        {
            if (String.IsNullOrWhiteSpace(argFilename))
            {
                throw new Exception("No filename was provided!");
            }
            if (argFileEncoding == null)
            {
                throw new Exception("No file encoding was provided!");
            }
            if (!File.Exists(argFilename))
            {
                throw new FileNotFoundException();
            }

            // Read all the contents of the file
            String fileContents = File.ReadAllText(argFilename, argFileEncoding);

            // Get the lines of the file
            // To avoid confusion with Line endings, replace \r\n with plain \n and split with that
            fileContents = fileContents.Replace("\r\n", "\n");
            String[] fileLines = fileContents.Split(new String[] { "\n" }, StringSplitOptions.None);

            // Create a new SubFile object
            SubFileParserResults results = new SubFileParserResults();
            results.SubFile = new SubFile();

            // Set the parsing state
            ParsingState pState = ParsingState.None;

            // Define a new subtitle
            SubFileSubtitleItem sub = null;

            // Parse the file lines
            for (Int64 i = 0; i < fileLines.LongLength; i++)
            {
                // Get the trimmed line data
                String line = fileLines[i] == null ? "" : fileLines[i].Trim();

                // Check the line data
                Int64 dummyInt64;
                if (String.IsNullOrWhiteSpace(line)) 
                {
                    // if we have an empty line
                    pState = ParsingState.None;
                }
                else if (Int64.TryParse(line, out dummyInt64))
                {
                    // If we have a number
                    if(pState == ParsingState.None)
                    {
                        // This is the expected behaviour, since the subtitle numbers should follow empty lines
                        if(sub != null)
                        {
                            if(String.IsNullOrWhiteSpace(sub.Text))
                            {
                                sub.Text = "";
                            }
                            else
                            {
                                sub.Text = sub.Text.Trim();
                            }
                            results.SubFile.Subtitles.Add(sub);
                        }
                        sub = new SubFileSubtitleItem();
                        // Save the LineNumber
                        sub.LineNumber = dummyInt64;
                        pState = ParsingState.SubtitleNumber;
                    }
                    else if(pState == ParsingState.SubtitleTime || pState == ParsingState.SubtitleText)
                    {
                        // this means that the number is part of text, since it follows subtitle time or text
                        sub.Text += line.Trim() + "\r\n";
                        pState = ParsingState.SubtitleText;
                    }
                    else if (pState == ParsingState.SubtitleNumber)
                    {
                        // if we have a number after a subtitle number, we ignore it
                        continue;
                    }
                    continue;
                }
                else if (HasTimeData(line))
                {
                    // if we have time data
                    if(pState == ParsingState.SubtitleText || pState == ParsingState.None)
                    {
                        // that means that there was no subtitle number!
                        if (sub != null)
                        {
                            if (String.IsNullOrWhiteSpace(sub.Text))
                            {
                                sub.Text = "";
                            }
                            else
                            {
                                sub.Text = sub.Text.Trim();
                            }
                            results.SubFile.Subtitles.Add(sub);
                        }
                        sub = new SubFileSubtitleItem();
                    }
                    String separator = "";
                    if (line.Contains("-->"))
                    {
                        separator = "-->";
                    }
                    else if (line.Contains("->"))
                    {
                        separator = "->";
                    }
                    String[] timeElements = line.Split(new string[] { separator }, StringSplitOptions.None);
                    sub.StartTime = GetTimeFromFormatString(timeElements[0]);
                    sub.EndTime = GetTimeFromFormatString(timeElements[1]);
                    pState = ParsingState.SubtitleTime;
                }
                else
                {
                    // if we have subtitle data
                    if(pState == ParsingState.SubtitleTime || pState == ParsingState.SubtitleText)
                    {
                        // this is the expected behaviour
                        if (String.IsNullOrWhiteSpace(sub.Text))
                        {
                            sub.Text = "";
                        }
                        sub.Text += line.Trim() + "\r\n";
                        pState = ParsingState.SubtitleText;
                    }
                    else if (pState == ParsingState.None)
                    {
                        if (sub != null)
                        {
                            // if we have a sub then add it to the text
                            if (String.IsNullOrWhiteSpace(sub.Text))
                            {
                                sub.Text = "";
                            }
                            sub.Text += line.Trim() + "\r\n";
                            pState = ParsingState.SubtitleText;
                        }
                        else
                        {
                            // don't know what to do here, so ignore it...
                            continue;
                        }
                    }
                    else if (pState == ParsingState.SubtitleNumber)
                    {
                        // don't know what to do here, so ignore it...
                        continue;
                    }
                }
            }
            // Add the last subtitle
            if(sub != null)
            {
                if (String.IsNullOrWhiteSpace(sub.Text))
                {
                    sub.Text = "";
                }
                else
                {
                    sub.Text = sub.Text.Trim();
                }
                results.SubFile.Subtitles.Add(sub);
            }

            return results;

            //throw new NotImplementedException();
        }

        public void Save(SubFile argSubFile, string argFilename, Encoding argFileEncoding)
        {
            if (String.IsNullOrWhiteSpace(argFilename))
            {
                throw new Exception("No filename was provided!");
            }
            if(argSubFile == null)
            {
                throw new Exception("No Sub File was provided!");
            }

            // Create a new String builder to write filecontents
            StringBuilder contents = new StringBuilder();

            // Define a subtitle item
            SubFileSubtitleItem sub = null;
            for (int i = 0; i < argSubFile.Subtitles.Count; i++)
            {
                // Get the current Subtitle
                sub = argSubFile.Subtitles[i];
                // Write subtitle number
                contents.AppendFormat("{0}\r\n", sub.LineNumber);
                // Write start and end time
                contents.AppendFormat("{0} --> {1}\r\n",
                    ConvertTimeToFormatString(sub.StartTime),
                    ConvertTimeToFormatString(sub.EndTime)
                );
                // Write subtitle text
                contents.AppendFormat("{0}\r\n", sub.Text.Trim());
                // Leave an empty line
                contents.AppendFormat("\r\n");
            }
            // Remove last empty line
            if (contents.Length > 2)
            {
                contents.Length -= 2;
            }
            // Write the file
            using (StreamWriter sw = new StreamWriter(argFilename, false, argFileEncoding))
            {
                sw.Write(contents.ToString());
            }

            //throw new NotImplementedException();
        }

        private bool HasTimeData(String argline)
        {
            if (String.IsNullOrWhiteSpace(argline))
            {
                return false;
            }
            if (!argline.Contains(":"))
            {
                return false;
            }
            if(!argline.Contains("-->") && !argline.Contains("->"))
            {
                return false;
            }
            if (!argline.Contains(",") && !argline.Contains("."))
            {
                return false;
            }
            String splitTimes = "-->";
            if (!argline.Contains("-->"))
            {
                splitTimes = "->";
            }
            string[] timeElements = argline.Split(new string[] { splitTimes }, StringSplitOptions.None);
            if (timeElements.Length != 2)
            {
                return false;
            }
            if(StringHelper.NumberOfOccurences(timeElements[0], ":") != 2)
            {
                return false;
            }
            if(!timeElements[0].Contains(",") && !timeElements[0].Contains("."))
            {
                return false;
            }
            string[] startTimeElements = timeElements[0].Split(new String[] { ":" }, StringSplitOptions.None);
            if (startTimeElements.Length != 3)
            {
                return false;
            }
            Int32 dummyInt32;
            if(!Int32.TryParse(startTimeElements[0], out dummyInt32))
            {
                return false;
            }
            if (!Int32.TryParse(startTimeElements[1], out dummyInt32))
            {
                return false;
            }
            String splitElements = ",";
            if (startTimeElements[2].Contains("."))
            {
                splitElements = ".";
            }
            string[] startTimeMinutes = startTimeElements[2].Split(new string[] { splitElements }, StringSplitOptions.None);
            if(startTimeMinutes.Length != 2)
            {
                return false;
            }
            if (!Int32.TryParse(startTimeMinutes[0], out dummyInt32))
            {
                return false;
            }
            if (!Int32.TryParse(startTimeMinutes[1], out dummyInt32))
            {
                return false;
            }

            if (StringHelper.NumberOfOccurences(timeElements[1], ":") != 2)
            {
                return false;
            }
            if (!timeElements[1].Contains(",") && !timeElements[0].Contains("."))
            {
                return false;
            }
            string[] endTimeElements = timeElements[1].Split(new String[] { ":" }, StringSplitOptions.None);
            if (endTimeElements.Length != 3)
            {
                return false;
            }
            if (!Int32.TryParse(endTimeElements[0], out dummyInt32))
            {
                return false;
            }
            if (!Int32.TryParse(endTimeElements[1], out dummyInt32))
            {
                return false;
            }
            splitElements = ",";
            if (endTimeElements[2].Contains("."))
            {
                splitElements = ".";
            }
            string[] endTimeMinutes = endTimeElements[2].Split(new string[] { splitElements }, StringSplitOptions.None);
            if (endTimeMinutes.Length != 2)
            {
                return false;
            }
            if (!Int32.TryParse(endTimeMinutes[0], out dummyInt32))
            {
                return false;
            }
            if (!Int32.TryParse(endTimeMinutes[1], out dummyInt32))
            {
                return false;
            }

            return true;
        }

        public Time GetTimeFromFormatString(string argTime)
        {
            if (String.IsNullOrWhiteSpace(argTime))
            {
                throw new Exception("Empty SRT time!");
            }
            argTime = argTime.Trim();
            //00:00:01,742
            string[] timeElements = argTime.Split(new string[] { ":" }, StringSplitOptions.None);
            if (timeElements.Length != 3)
            {
                throw new Exception("Malformed SRT time!");
            }
            Int32 hours, minutes, seconds, milliseconds, dummyInt;
            if (Int32.TryParse(timeElements[0], NumberStyles.Any, CultureInfo.InvariantCulture, out dummyInt))
            {
                hours = dummyInt;
            }
            else
            {
                throw new Exception("The SRT time is malformed! (hours)");
            }
            if (Int32.TryParse(timeElements[1], NumberStyles.Any, CultureInfo.InvariantCulture, out dummyInt))
            {
                minutes = dummyInt;
            }
            else
            {
                throw new Exception("The SRT time is malformed! (minutes)");
            }
            string secondsSeparator = ",";
            if (timeElements[2].Contains("."))
            {
                secondsSeparator = ".";
            }

            string[] secondsElements = timeElements[2].Split(new string[] { secondsSeparator }, StringSplitOptions.None);
            if (secondsElements.Length != 2)
            {
                throw new Exception("Malformed SRT time!");
            }
            if (Int32.TryParse(secondsElements[0], NumberStyles.Any, CultureInfo.InvariantCulture, out dummyInt))
            {
                seconds = dummyInt;
            }
            else
            {
                throw new Exception("The SRT time is malformed! (seconds)");
            }
            if (Int32.TryParse(secondsElements[1], NumberStyles.Any, CultureInfo.InvariantCulture, out dummyInt))
            {
                milliseconds = dummyInt;
            }
            else
            {
                throw new Exception("The SRT time is malformed! (milliseconds)");
            }

            return new Time(hours, minutes, seconds, milliseconds);
        }

        public string ConvertTimeToFormatString(Time argTime)
        {
            return String.Format("{0}:{1}:{2},{3}",
                argTime.Hours.ToString("00"),
                argTime.Minutes.ToString("00"),
                argTime.Seconds.ToString("00"),
                argTime.Milliseconds.ToString("000")
            );
        }
    }
}
