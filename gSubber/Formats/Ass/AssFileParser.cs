using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace gSubber.Formats.Ass
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
            SubFileParserResults results = new gSubber.SubFileParserResults();
            results.SubFile = new SubFile();

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
                        SubFileInfoItem inf = new gSubber.SubFileInfoItem();
                        if (dataLine.StartsWith(";"))
                        {
                            inf.IsComment = true;
                            inf.Value = dataLine.Substring(1);
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
                                    results.AddWarning(new gSubber.SubFileParserMessage() { Line = i, LineData = line, Message = "No info value found..." });
                                    inf.Value = "";
                                }
                            }
                            else
                            {
                                results.AddWarning(new gSubber.SubFileParserMessage() { Line = i, LineData = line, Message = "Malformed info line..." });
                                inf.Value = dataLine.Trim();
                            }
                            results.SubFile.Info.Add(inf);
                            continue;
                        }
                    case ParsingState.AegisubProperties:
                        SubFilePropertyItem prop = new gSubber.SubFilePropertyItem();
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
                                results.AddWarning(new gSubber.SubFileParserMessage() { Line = i, LineData = line, Message = "No property value found..." });
                                prop.Value = "";
                            }
                        }
                        else
                        {
                            results.AddWarning(new gSubber.SubFileParserMessage() { Line = i, LineData = line, Message = "Malformed property line..." });
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

                            style.PrimaryColor = ColorHelper.FromASS(styleElements[3]);
                            style.SecondaryColor = ColorHelper.FromASS(styleElements[4]);
                            style.OutlineColor = ColorHelper.FromASS(styleElements[5]);
                            style.BackColor = ColorHelper.FromASS(styleElements[6]);

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
                            string dataSub = ""; 
                            if (lowerCaseLine.StartsWith("dialogue:"))
                            {
                                dataSub= dataLine.Substring(9).Trim();
                                sub.IsComment = false;
                            }
                            else
                            {
                                dataSub = dataLine.Substring(8).Trim();
                                sub.IsComment = true;
                            }
                            string[] subElements = dataSub.Split(new string[] { "," }, StringSplitOptions.None);
                            //Format: Layer, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, Text
                            //Comment: 0,0:04:59.88,0:05:06.20,Opening,,0000,0000,0000,,{start}
                            //Dialogue: 0,0:01:30.23,0:01:31.14,Default,,0000,0000,0000,,Careful!
                            sub.Zindex = Int32.Parse(subElements[0]);
                            sub.StartTime = TimeHelper.FromAssTime(subElements[1]);
                            sub.EndTime = TimeHelper.FromAssTime(subElements[2]);
                            sub.Style = results.SubFile.Styles.Where(s => s.Name == subElements[3]).FirstOrDefault();
                            sub.ActorName = subElements[4];
                            sub.MarginLeft = double.Parse(subElements[5], NumberStyles.Any, CultureInfo.InvariantCulture);
                            sub.MarginRight = double.Parse(subElements[6], NumberStyles.Any, CultureInfo.InvariantCulture);
                            sub.MarginVertical = double.Parse(subElements[7], NumberStyles.Any, CultureInfo.InvariantCulture);
                            sub.Effect = subElements[8];
                            sub.Text = subElements[9];

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

        public void Save(SubFile argSubFile, string argFilename)
        {
            throw new NotImplementedException();
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
