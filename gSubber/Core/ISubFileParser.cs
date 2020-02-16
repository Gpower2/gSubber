using gSubber.Core.SubtitleFile;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace gSubber.Core
{
    public interface ISubFileParser
    {
        SubFileParserResults Load(string argFilename, Encoding argFileEncoding);

        void Save(SubFile argSubFile);

        void SaveAs(SubFile argSubFile, string argFilename, Encoding argFileEncoding);

        Time GetTimeFromFormatString(string argTimeFormatString);

        string ConvertTimeToFormatString(Time argTime);

        Color GetColorFromFormatString(string argColorFormatString);

        string ConvertColorToFormatString(Color argColor);
    }
}
