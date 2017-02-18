using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber
{
    public class SubFileSubtitleItem
    {
        public bool IsComment { get; set; }

        public SubFileStyleItem Style { get; set; }

        public String ActorName { get; set; }

        public String Effect { get; set; }


        public int Zindex { get; set; }

        public double MarginLeft { get; set; }

        public double MarginRight { get; set; }

        public double MarginVertical { get; set; }


        public Time StartTime { get; set; }

        public Time EndTime { get; set; }

        public String Text { get; set; }

        public override string ToString()
        {
            return String.Format("{0} - {1} [{2}] : {3}", 
                StartTime == null ? new Time() : StartTime, 
                EndTime == null ? new Time() : EndTime, 
                Style == null ? "" : Style.ToString(),
                Text ?? "");
        }
    }
}
