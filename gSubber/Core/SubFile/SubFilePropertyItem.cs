using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber.Core.SubtitleFile
{
    public class SubFilePropertyItem
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Name ?? ""}: {Value ?? ""}";
        }
    }
}
