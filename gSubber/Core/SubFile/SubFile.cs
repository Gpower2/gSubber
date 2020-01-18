using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber.Core.SubtitleFile
{
    public class SubFile
    {
        public List<SubFileInfoItem> Info { get; } = new List<SubFileInfoItem>();

        public List<SubFilePropertyItem> Properties { get; } = new List<SubFilePropertyItem>();

        public List<SubFileStyleItem> Styles { get; } = new List<SubFileStyleItem>();

        public List<SubFileSubtitleItem> Subtitles { get; } = new List<SubFileSubtitleItem>();

        public List<SubFileAttachment> Attachments { get; } = new List<SubFileAttachment>();

        public string Filename { get; private set; }

        public Encoding FileEncoding { get; private set; }

        public SubFile(string argFilename, Encoding argFileEncoding)
        {
            Filename = argFilename;
            FileEncoding = argFileEncoding;
        }
    }
}
