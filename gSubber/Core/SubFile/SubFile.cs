using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber.Core.SubtitleFile
{
    public class SubFile
    {
        private List<SubFileInfoItem> _Info = new List<SubFileInfoItem>();
        public List<SubFileInfoItem> Info { get { return _Info; } }

        private List<SubFilePropertyItem> _Properties = new List<SubFilePropertyItem>();
        public List<SubFilePropertyItem> Properties { get { return _Properties; } }

        private List<SubFileStyleItem> _Styles = new List<SubFileStyleItem>();
        public List<SubFileStyleItem> Styles { get { return _Styles; } }

        private List<SubFileSubtitleItem> _Subtitles = new List<SubFileSubtitleItem>();
        public List<SubFileSubtitleItem> Subtitles { get { return _Subtitles; } }

        private List<SubFileAttachment> _Attachments = new List<SubFileAttachment>();
        public List<SubFileAttachment> Attachments { get { return _Attachments; } }

        public SubFile() { }
    }
}
