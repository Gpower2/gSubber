using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber.Core.SubtitleFile
{
    public class SubFile : ISubFile
    {
        public IList<SubFileInfoItem> Info { get; } = new List<SubFileInfoItem>();

        public IList<SubFilePropertyItem> Properties { get; } = new List<SubFilePropertyItem>();

        public IList<SubFileStyleItem> Styles { get; } = new List<SubFileStyleItem>();

        public IList<SubFileSubtitleItem> Subtitles { get; } = new List<SubFileSubtitleItem>();

        public IList<SubFileAttachment> Attachments { get; } = new List<SubFileAttachment>();

        public string Filename { get; private set; }

        public Encoding FileEncoding { get; private set; }

        public SubFile(string argFilename, Encoding argFileEncoding)
        {
            Filename = argFilename;
            FileEncoding = argFileEncoding;
        }
    }
}
