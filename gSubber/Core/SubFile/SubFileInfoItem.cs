using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber.Core.SubtitleFile
{
    public class SubFileInfoItem
    {
        public bool IsComment { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Name ?? ""}: {Value ?? ""}";
        }
    }
}
