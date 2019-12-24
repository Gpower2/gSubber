using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber.Core.SubtitleFile
{
    public class SubFileSubtitleItem
    {
        public static String NEW_LINE_CHARACTER = "¶";

        public Int64 LineNumber { get; set; }

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

        public String DisplayText
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(Text))
                {
                    return Text.Replace("\r\n", "\n").Replace("\n", String.Format(" {0} ", NEW_LINE_CHARACTER));
                }
                else
                {
                    return "";
                }
            }
        }

        public string[] TextLines
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(Text))
                {
                    return Text.Replace("\r\n", "\n").Trim().Split(new string[] { "\n" }, StringSplitOptions.None);
                }
                else
                {
                    return new string[] { "" };
                }
            }
        }

        public double Duration
        {
            get
            {
                if(StartTime != null && EndTime != null)
                {
                    return (EndTime - StartTime).TotalSeconds;
                }
                else
                {
                    return 0.0;
                }
            }
        }

        public double CharactersPerSecond
        {
            get
            {
                if (Duration > 0.0 && !String.IsNullOrWhiteSpace(Text))
                {
                    return Convert.ToDouble(Text.Trim().Length) / Duration;
                }
                else
                {
                    return 0.0;
                }
            }
        }

        public SubFileSubtitleItem ShallowClone()
        {
            return (SubFileSubtitleItem)this.MemberwiseClone();
        }

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
