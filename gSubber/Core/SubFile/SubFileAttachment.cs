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

        public String Name { get; set; }

        public String Data { get; set; }

    }
}
