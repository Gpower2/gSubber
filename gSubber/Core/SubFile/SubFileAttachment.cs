using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber.Core.SubtitleFile
{
    public class SubFileAttachment
    {
        public enum AttachmentType
        {
            Font,
            Graphic
        }

        public AttachmentType DataType { get; set; }

        public string Name { get; set; }

        public string Data { get; set; }
    }
}
