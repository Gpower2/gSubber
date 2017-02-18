using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber
{
    public class SubFileInfoItem
    {
        public bool IsComment { get; set; }

        public String Name { get; set; }

        public String Value { get; set; }

        public override string ToString()
        {
            return String.Format("{0}: {1}", Name ?? "", Value ?? "");
        }
    }
}
