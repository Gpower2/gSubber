using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber
{
    public interface ISubFileParser
    {
        SubFileParserResults Load(String argFilename, Encoding argFileEncoding);

        void Save(SubFile argSubFile, String argFilename);


    }
}
