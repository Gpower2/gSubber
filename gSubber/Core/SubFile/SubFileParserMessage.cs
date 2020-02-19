using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber.Core.SubtitleFile
{
    public class SubFileParserMessage
    {
        public int Line { get; set; }

        public string LineData { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return $"Line {Line}: {Message ?? ""}";
        }
    }
}
