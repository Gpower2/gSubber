using System.Collections.Generic;
using System.Text;

namespace gSubber.Core.SubtitleFile
{
    public interface ISubFile
    {
        IList<SubFileInfoItem> Info { get; }
        IList<SubFilePropertyItem> Properties { get; }
        IList<SubFileStyleItem> Styles { get; }
        IList<SubFileSubtitleItem> Subtitles { get; }
        IList<SubFileAttachment> Attachments { get; }
        
        string Filename { get; }
        Encoding FileEncoding { get; }
    }
}