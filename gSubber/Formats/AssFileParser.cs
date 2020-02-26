using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using gSubber.Extensions;
using gSubber.Core;
using gSubber.Core.SubtitleFile;
using System.Drawing;

namespace gSubber.Formats
{
    public class AssFileParser : ISubFileParser
    {
        private enum ParsingState
        {
            None,
            ScriptInfo,
            AegisubProperties,
            Styles,
            Events,
            Fonts,
            Graphics
        }

        public ISubFileParserResults Load(string argFilename, Encoding argFileEncoding)
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
            results.SubFile = new SubFile(argFilename, argFileEncoding);

            // Set the parsing state
            ParsingState pState = ParsingState.None;

            // Parse the file lines
            for (int i = 0; i < fileLines.Length; i++)
            {
                String line = fileLines[i];
                // Ignore empty lines
                if (String.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                // Check if we have a section start
                String dataLine = line.Trim();
                String lowerCaseLine = line.ToLower().Trim();
                if (lowerCaseLine.StartsWith("[script info]"))
                {
                    pState = ParsingState.ScriptInfo;
                    continue;
                }
                else if (lowerCaseLine.StartsWith("[aegisub project garbage]"))
                {
                    pState = ParsingState.AegisubProperties;
                    continue;
                }
                else if (lowerCaseLine.StartsWith("[v4+ styles]"))
                {
                    pState = ParsingState.Styles;
                    continue;
                }
                else if (lowerCaseLine.StartsWith("[events]"))
                {
                    pState = ParsingState.Events;
                    continue;
                }
                else if (lowerCaseLine.StartsWith("[fonts]"))
                {
                    pState = ParsingState.Fonts;
                    continue;
                }
                else if (lowerCaseLine.StartsWith("[graphics]"))
                {
                    pState = ParsingState.Graphics;
                    continue;
                }

                // Parse the line depending on the parsing state
                switch (pState)
                {
                    case ParsingState.None:
                        break;
                    case ParsingState.ScriptInfo:
                        SubFileInfoItem inf = new SubFileInfoItem();
                        if (dataLine.StartsWith(";"))
                        {
                            inf.IsComment = true;
                            if (dataLine.Length > 2)
                            {
                                inf.Value = dataLine.Substring(2);
                            }
                            else
                            {
                                inf.Value = "";
                            }
                            results.SubFile.Info.Add(inf);
                            continue;
                        }
                        else
                        {
                            inf.IsComment = false;
                            if (dataLine.Contains(":"))
                            {
                                int idx = dataLine.IndexOf(":");
                                inf.Name = dataLine.Substring(0, idx).Trim();
                                if (idx < dataLine.Length - 1)
                                {
                                    inf.Value = dataLine.Substring(idx + 1).Trim();
                                }
                                else
                                {
                                    results.AddWarning(new SubFileParserMessage() { Line = i, LineData = line, Message = "No info value found..." });
                                    inf.Value = "";
                                }
                            }
                            else
                            {
                                results.AddWarning(new SubFileParserMessage() { Line = i, LineData = line, Message = "Malformed info line..." });
                                inf.Value = dataLine.Trim();
                            }
                            results.SubFile.Info.Add(inf);
                            continue;
                        }
                    case ParsingState.AegisubProperties:
                        SubFilePropertyItem prop = new SubFilePropertyItem();
                        if (dataLine.Contains(":"))
                        {
                            int idx = dataLine.IndexOf(":");
                            prop.Name = dataLine.Substring(0, idx).Trim();
                            if (idx < dataLine.Length - 1)
                            {
                                prop.Value = dataLine.Substring(idx + 1).Trim();
                            }
                            else
                            {
                                results.AddWarning(new SubFileParserMessage() { Line = i, LineData = line, Message = "No property value found..." });
                                prop.Value = "";
                            }
                        }
                        else
                        {
                            results.AddWarning(new SubFileParserMessage() { Line = i, LineData = line, Message = "Malformed property line..." });
                            prop.Value = dataLine.Trim();
                        }
                        results.SubFile.Properties.Add(prop);
                        continue;
                    case ParsingState.Styles:
                        if (lowerCaseLine.StartsWith("format:"))
                        {
                            // Ignore Format: line
                            continue;
                        }
                        else if (lowerCaseLine.StartsWith("style:"))
                        {
                            // Parse the style line
                            string dataStyle = dataLine.Substring(6).Trim();
                            string[] styleElements = dataStyle.Split(new string[] { "," }, StringSplitOptions.None);
                            //Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding
                            //Opening,Ruzicka TypeK,75,&H00693212,&H000000FF,&H00FFFFFF,&H00000000,-1,0,0,0,100,100,0,0,1,3,0,5,15,15,45,1
                            SubFileStyleItem style = new SubFileStyleItem();
                            style.Name = styleElements[0];
                            style.Fontname = styleElements[1];
                            style.Fontsize = float.Parse(styleElements[2], NumberStyles.Any, CultureInfo.InvariantCulture);

                            style.PrimaryColor = GetColorFromFormatString(styleElements[3]);
                            style.SecondaryColor = GetColorFromFormatString(styleElements[4]);
                            style.OutlineColor = GetColorFromFormatString(styleElements[5]);
                            style.BackColor = GetColorFromFormatString(styleElements[6]);

                            style.Bold = (Int16.Parse(styleElements[7]) == 0 ? false : true);
                            style.Italic = (Int16.Parse(styleElements[8]) == 0 ? false : true);
                            style.Underline = (Int16.Parse(styleElements[9]) == 0 ? false : true);
                            style.StrikeOut = (Int16.Parse(styleElements[10]) == 0 ? false : true);

                            style.ScaleX = double.Parse(styleElements[11], NumberStyles.Any, CultureInfo.InvariantCulture);
                            style.ScaleY = double.Parse(styleElements[12], NumberStyles.Any, CultureInfo.InvariantCulture);

                            style.Spacing = float.Parse(styleElements[13], NumberStyles.Any, CultureInfo.InvariantCulture);
                            style.RotationAngle = double.Parse(styleElements[14], NumberStyles.Any, CultureInfo.InvariantCulture);

                            style.BorderStyle = (BorderStyle)Enum.Parse(typeof(BorderStyle), styleElements[15]);

                            style.OutlineSize = double.Parse(styleElements[16], NumberStyles.Any, CultureInfo.InvariantCulture);
                            style.ShadowSize = double.Parse(styleElements[17], NumberStyles.Any, CultureInfo.InvariantCulture);
                            style.ScreenAlignment = (ScreenAlignment)Enum.Parse(typeof(ScreenAlignment), styleElements[18]);

                            style.MarginLeft = double.Parse(styleElements[19], NumberStyles.Any, CultureInfo.InvariantCulture);
                            style.MarginRight = double.Parse(styleElements[20], NumberStyles.Any, CultureInfo.InvariantCulture);
                            style.MarginVertical = double.Parse(styleElements[21], NumberStyles.Any, CultureInfo.InvariantCulture);

                            style.FontEncoding = (FontEncoding)Enum.Parse(typeof(FontEncoding), styleElements[22]);

                            results.SubFile.Styles.Add(style);
                        }
                        break;
                    case ParsingState.Events:
                        if (lowerCaseLine.StartsWith("format:"))
                        {
                            // Ignore Format: line
                            continue;
                        }
                        else if (lowerCaseLine.StartsWith("dialogue:") || lowerCaseLine.StartsWith("comment:"))
                        {
                            // Parse the event line
                            SubFileSubtitleItem sub = new SubFileSubtitleItem();
                            Int32 startData = 0; 
                            if (lowerCaseLine.StartsWith("dialogue:"))
                            {
                                sub.IsComment = false;
                                startData = 9;
                            }
                            else
                            {
                                sub.IsComment = true;
                                startData = 8;
                            }

                            List<Int32> commas = new List<int>();
                            Int32 startComma = startData;
                            for (int t = 0; t < 9; t++)
                            {
                                commas.Add(dataLine.IndexOf(",", startComma, StringComparison.InvariantCulture));
                                startComma = commas[t] + 1;
                            }

                            //Format: Layer, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text
                            //Comment: 0,0:04:59.88,0:05:06.20,Opening,,0000,0000,0000,,{start}
                            //Dialogue: 0,0:01:30.23,0:01:31.14,Default,,0000,0000,0000,,Careful!
                            sub.Zindex = Int32.Parse(dataLine.Substring(startData + 1, commas[0] - startData - 1));
                            sub.StartTime = GetTimeFromFormatString(dataLine.Substring(commas[0] + 1, commas[1] - commas[0] - 1));
                            sub.EndTime = GetTimeFromFormatString(dataLine.Substring(commas[1] + 1, commas[2] - commas[1] - 1));
                            sub.Style = results.SubFile.Styles.Where(s => s.Name == dataLine.Substring(commas[2] + 1, commas[3] - commas[2] - 1)).FirstOrDefault();
                            sub.ActorName = dataLine.Substring(commas[3] + 1, commas[4] - commas[3] - 1);
                            sub.MarginLeft = double.Parse(dataLine.Substring(commas[4] + 1, commas[5] - commas[4] - 1), NumberStyles.Any, CultureInfo.InvariantCulture);
                            sub.MarginRight = double.Parse(dataLine.Substring(commas[5] + 1, commas[6] - commas[5] - 1), NumberStyles.Any, CultureInfo.InvariantCulture);
                            sub.MarginVertical = double.Parse(dataLine.Substring(commas[6] + 1, commas[7] - commas[6] - 1), NumberStyles.Any, CultureInfo.InvariantCulture);
                            sub.Effect = dataLine.Substring(commas[7] + 1, commas[8] - commas[7] - 1);

                            sub.Text = dataLine.Substring(commas[8] + 1);

                            results.SubFile.Subtitles.Add(sub);
                        }
                        break;
                    case ParsingState.Fonts:
                        break;
                    case ParsingState.Graphics:
                        break;
                    default:
                        break;
                }
            }

            return results;
        }

        public void Save(ISubFile argSubFile)
        {
            SaveAs(argSubFile, argSubFile.Filename, argSubFile.FileEncoding);
        }

        public void SaveAs(ISubFile argSubFile, string argFilename, Encoding argFileEncoding)
        {
            if (String.IsNullOrWhiteSpace(argFilename))
            {
                throw new Exception("No filename was provided!");
            }
            if (argSubFile == null)
            {
                throw new Exception("No Sub File was provided!");
            }

            // Create a new String builder to write filecontents
            StringBuilder contents = new StringBuilder();

            // First write the [Script Info] section
            contents.AppendLine("[Script Info]");
            foreach (var info in argSubFile.Info)
            {
                if (info.IsComment)
                {
                    contents.AppendFormat("; {0}", info.Value).AppendLine();
                }
                else
                {
                    contents.AppendFormat("{0}: {1}", info.Name, info.Value).AppendLine();
                }
            }

            // Leave an empty line
            contents.AppendLine();

            // Check for properties
            if(argSubFile.Properties != null && argSubFile.Properties.Any())
            {
                // First write the [Aegisub Project Garbage] section
                contents.AppendLine("[Aegisub Project Garbage]");

                foreach (var prop in argSubFile.Properties)
                {
                    contents.AppendFormat("{0}: {1}", prop.Name, prop.Value).AppendLine();
                }
            }

            // Leave an empty line
            contents.AppendLine();

            // Write the V4+ Styles
            // First write the [V4+ Styles] section
            contents.AppendLine("[V4+ Styles]");
            // Write the format line (It's constant)
            contents.AppendLine("Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding");
            foreach (var style in argSubFile.Styles)
            {
                //Style: Kanji,Calibri,40,&H00FFFFFF,&H000000FF,&H00000000,&HC0000000,-1,0,0,0,100,95,0,0,1,3,0,2,40,40,5,161
                contents.AppendFormat("Style: {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22}",
                    style.Name.Trim(),
                     style.Fontname.Trim(),
                     style.Fontsize.ToString(CultureInfo.InvariantCulture),
                     ConvertColorToFormatString(style.PrimaryColor),
                     ConvertColorToFormatString(style.SecondaryColor),
                     ConvertColorToFormatString(style.OutlineColor),
                     ConvertColorToFormatString(style.BackColor),
                     style.Bold ? "-1" : "0",
                     style.Italic ? "-1" : "0",
                     style.Underline ? "-1" : "0",
                     style.StrikeOut ? "-1" : "0",
                     style.ScaleX.ToString(CultureInfo.InvariantCulture),
                     style.ScaleY.ToString(CultureInfo.InvariantCulture),
                     style.Spacing.ToString(CultureInfo.InvariantCulture),
                     style.RotationAngle.ToString(CultureInfo.InvariantCulture),
                     (int)style.BorderStyle,
                     style.OutlineSize.ToString(CultureInfo.InvariantCulture),
                     style.ShadowSize.ToString(CultureInfo.InvariantCulture),
                     (int)style.ScreenAlignment,
                     style.MarginLeft.ToString(CultureInfo.InvariantCulture),
                     style.MarginRight.ToString(CultureInfo.InvariantCulture),
                     style.MarginVertical.ToString(CultureInfo.InvariantCulture),
                     (int)style.FontEncoding
                    ).AppendLine();
            }

            // Leave an empty line
            contents.AppendLine();

            // Write the Events
            // First write the [Events] section
            contents.AppendLine("[Events]");
            // Write the format line (It's constant)
            contents.AppendLine("Format: Layer, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text");
            foreach (var sub in argSubFile.Subtitles)
            {
                if (sub.IsComment)
                {
                    contents.AppendFormat("Comment: ");
                }
                else
                {
                    contents.AppendFormat("Dialogue: ");
                }
                //0,0:00:13.36,0:00:15.78,Romaji,,0,0,0,,koufun suzzo! uchuu e go!
                contents.AppendFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                     sub.Zindex.ToString(CultureInfo.InvariantCulture),
                     ConvertTimeToFormatString(sub.StartTime),
                     ConvertTimeToFormatString(sub.EndTime),
                     sub.Style,
                     sub.ActorName,
                     sub.MarginLeft.ToString(CultureInfo.InvariantCulture),
                     sub.MarginRight.ToString(CultureInfo.InvariantCulture),
                     sub.MarginVertical.ToString(CultureInfo.InvariantCulture),
                     sub.Effect,
                     sub.Text.Replace("\r\n", "\n").Replace("\n", "\\N")
                ).AppendLine();
            }

            // Write the file
            using (StreamWriter sw = new StreamWriter(argFilename, false, argFileEncoding))
            {
                sw.Write(contents.ToString());
            }

            //throw new NotImplementedException();
        }

        public Time GetTimeFromFormatString(string argTimeFormatString)
        {
            if (String.IsNullOrWhiteSpace(argTimeFormatString))
            {
                throw new Exception("Empty ASS time!");
            }
            argTimeFormatString = argTimeFormatString.Trim();
            //0:04:59.88
            string[] timeElements = argTimeFormatString.Split(new string[] { ":" }, StringSplitOptions.None);
            if (timeElements.Length != 3)
            {
                throw new Exception("Malformed ASS time!");
            }
            Int32 hours, minutes, seconds, milliseconds, dummyInt;
            if (Int32.TryParse(timeElements[0], NumberStyles.Any, CultureInfo.InvariantCulture, out dummyInt))
            {
                hours = dummyInt;
            }
            else
            {
                throw new Exception("The ASS time is malformed! (hours)");
            }
            if (Int32.TryParse(timeElements[1], NumberStyles.Any, CultureInfo.InvariantCulture, out dummyInt))
            {
                minutes = dummyInt;
            }
            else
            {
                throw new Exception("The ASS time is malformed! (minutes)");
            }
            string[] secondsElements = timeElements[2].Split(new string[] { "." }, StringSplitOptions.None);
            if (secondsElements.Length != 2)
            {
                throw new Exception("Malformed ASS time!");
            }
            if (Int32.TryParse(secondsElements[0], NumberStyles.Any, CultureInfo.InvariantCulture, out dummyInt))
            {
                seconds = dummyInt;
            }
            else
            {
                throw new Exception("The ASS time is malformed! (seconds)");
            }
            if (Int32.TryParse(secondsElements[1], NumberStyles.Any, CultureInfo.InvariantCulture, out dummyInt))
            {
                // Ass format specifies deciseconds instead of milliseconds
                milliseconds = dummyInt * 10;
            }
            else
            {
                throw new Exception("The ASS time is malformed! (milliseconds)");
            }

            return new Time(hours, minutes, seconds, milliseconds);
        }

        public string ConvertTimeToFormatString(Time argTime)
        {
            if (argTime == null)
            {
                throw new Exception("Time is null!");
            }
            //0:00:13.36
            return String.Format("{0:0}:{1:00}:{2:00}.{3:00}",
                argTime.Hours
                , argTime.Minutes
                , argTime.Seconds
                , ((double)argTime.Milliseconds) / 10.0);
        }

        public Color GetColorFromFormatString(string argColorFormatString)
        {
            //&H00693212
            if (String.IsNullOrWhiteSpace(argColorFormatString))
            {
                throw new Exception("Empty ASS color!");
            }
            argColorFormatString = argColorFormatString.Trim();
            if (argColorFormatString.Length != 10 && argColorFormatString.Length != 8)
            {
                throw new Exception("The ASS color is malformed!");
            }
            string assColorToParse = argColorFormatString;
            if (argColorFormatString.Length == 10)
            {
                assColorToParse = argColorFormatString.Substring(2);
            }
            byte dummyByte, red, green, blue, alpha;
            if (byte.TryParse(assColorToParse.Substring(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out dummyByte))
            {
                red = dummyByte;
            }
            else
            {
                throw new Exception("The ASS color is malformed! (red)");
            }
            if (byte.TryParse(assColorToParse.Substring(2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out dummyByte))
            {
                green = dummyByte;
            }
            else
            {
                throw new Exception("The ASS color is malformed! (green)");
            }
            if (byte.TryParse(assColorToParse.Substring(4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out dummyByte))
            {
                blue = dummyByte;
            }
            else
            {
                throw new Exception("The ASS color is malformed! (blue)");
            }
            if (byte.TryParse(assColorToParse.Substring(6, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out dummyByte))
            {
                alpha = dummyByte;
            }
            else
            {
                throw new Exception("The ASS color is malformed! (Alpha)");
            }

            return Color.FromArgb(alpha, red, green, blue);
        }

        public string ConvertColorToFormatString(Color argColor)
        {
            if (argColor == null)
            {
                throw new Exception("Color is null!");
            }
            //&H00693212
            //&H 00 69 32 12
            return String.Format("&H{0}{1}{2}{3}",
                argColor.R.ToString("X2")
                , argColor.G.ToString("X2")
                , argColor.B.ToString("X2")
                , argColor.A.ToString("X2")
            );
        }

        //private Byte[] UUDecode(String argFourChars)
        //{
        //    if(argFourChars.Length != 4)
        //    {
        //        throw new Exception("No 4 chars were provided!");
        //    }
        //    char[] chars = argFourChars.ToArray();

        //    // Get the ASCII character code
        //    Int32 asciiCode_01 = (Int32)chars[0];
        //    // Remove the 33 offset
        //    asciiCode_01 -= 33;
        //    // Get the ASCII character code
        //    Int32 asciiCode_02 = (Int32)chars[1];
        //    // Remove the 33 offset
        //    asciiCode_02 -= 33;
        //    // Get the ASCII character code
        //    Int32 asciiCode_03 = (Int32)chars[2];
        //    // Remove the 33 offset
        //    asciiCode_03 -= 33;
        //    // Get the ASCII character code
        //    Int32 asciiCode_04 = (Int32)chars[3];
        //    // Remove the 33 offset
        //    asciiCode_04 -= 33;


        //}


    }
}
