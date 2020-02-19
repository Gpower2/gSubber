using gSubber.Formats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace gSubber.Core
{
    public class SubFileParserFactory
    {
        public static ISubFileParser GetSubFileParser(string argSubTitleFilename)
        {
            string inputFileExtension = Path.GetExtension(argSubTitleFilename);
            if (inputFileExtension.Length > 1)
            {
                inputFileExtension = inputFileExtension.Substring(1);
            }
            inputFileExtension = inputFileExtension.ToLower().Trim();

            ISubFileParser parser = null;
            if (inputFileExtension == "srt")
            {
                parser = new SrtFileParser();
            }
            else if (inputFileExtension == "ass")
            {
                parser = new AssFileParser();
            }
            if (parser == null)
            {
                throw new Exception($"Could not file parser for format {inputFileExtension}!");
            }

            return parser;
        }
    }
}
