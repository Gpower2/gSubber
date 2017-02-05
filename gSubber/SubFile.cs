using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber
{
    public class SubFile
    {
        public List<SubFileInfoItem> Info { get; set; }

        public List<SubFilePropertyItem> Properties { get; set; }

        public List<SubFileStyleItem> Styles { get; set; }

        public List<SubFileSubtitleItem> Subtitles { get; set; }

        public List<SubFileAttachment> Attachments { get; set; }

    }
}
