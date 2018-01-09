using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber.Core.SubtitleFile
{
    public class SubFilePropertyItem
    {
        public String Name { get; set; }

        public String Value { get; set; }

        public override string ToString()
        {
            return String.Format("{0}: {1}", Name ?? "", Value ?? "");
        }
    }
}
