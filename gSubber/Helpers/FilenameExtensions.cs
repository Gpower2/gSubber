using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace gSubber.Helpers
{
    public static class FilenameExtensions
    {
        /// <summary>
        /// Returns the encoding from a text file
        /// </summary>
        /// <param name="argFilename">The text file's full filename</param>
        /// <returns>The file's encoding (If it's not UTF-8 then it's ALWAYS ASCII)</returns>
        public static Encoding GetEncoding(this string argFilename)
        {
            // Read the first 4 bytes to check for a valid BOM
            var bom = new byte[4];
            using (var file = new FileStream(argFilename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                file.Read(bom, 0, 4);
            }

            // Analyze the first 4 byte to check for BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76)
            {
                return Encoding.UTF7;
            }
            else if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf)
            {
                return Encoding.UTF8;
            }
            else if (bom[0] == 0xff && bom[1] == 0xfe)
            {
                return Encoding.Unicode; //UTF-16LE
            }
            else if (bom[0] == 0xfe && bom[1] == 0xff)
            {
                return Encoding.BigEndianUnicode; //UTF-16BE
            }
            else if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff)
            {
                return Encoding.UTF32;
            }

            // No BOM was found, so we check for UTF-8 characters (multibyte)

            // Read the file contents into a byte array, since ReadByte() from FileStream takes too long
            byte[] fileContents = File.ReadAllBytes(argFilename);

            for (Int64 i = 0; i < fileContents.LongLength; i++)
            {
                Byte firstByte = fileContents[i];
                // Check for 2 byte characters
                if ((firstByte >> 5 << 5) == 192 && i + 1 < fileContents.LongLength - 1) // StartsWith("110") BIN: 1100 0000 => DEC: 192
                {
                    Byte secondByte = fileContents[i + 1];
                    if ((secondByte >> 6 << 6) == 128) // StartsWith("10") BIN: 1000 0000 => DEC: 128
                    {
                        return Encoding.UTF8;
                    }
                }
                // Check for 3 byte characters
                else if ((firstByte >> 4 << 4) == 224 && i + 1 < fileContents.LongLength - 1) // StartsWith("1110") BIN: 1110 0000 => DEC: 224
                {
                    Byte secondByte = fileContents[i + 1];
                    if ((secondByte >> 6 << 6) == 128 && i + 2 < fileContents.LongLength - 1) // StartsWith("10") BIN: 1000 0000 => DEC: 128
                    {
                        Byte thirdByte = fileContents[i + 2];
                        if ((thirdByte >> 6 << 6) == 128) // StartsWith("10") BIN: 1000 0000 => DEC: 128
                        {
                            return Encoding.UTF8;
                        }
                    }
                }
                // Check for 4 byte characters
                else if ((firstByte >> 3 << 3) == 240 && i + 1 < fileContents.LongLength - 1) // StartsWith("11110") BIN: 1111 0000 => DEC: 240
                {
                    Byte secondByte = fileContents[i + 1];
                    if ((secondByte >> 6 << 6) == 128 && i + 2 < fileContents.LongLength - 1) // StartsWith("10") BIN: 1000 0000 => DEC: 128
                    {
                        Byte thirdByte = fileContents[i + 2];
                        if ((thirdByte >> 6 << 6) == 128 && i + 3 < fileContents.LongLength - 1) // StartsWith("10") BIN: 1000 0000 => DEC: 128
                        {
                            Byte fourthByte = fileContents[i + 3];
                            if ((fourthByte >> 6 << 6) == 128) // StartsWith("10") BIN: 1000 0000 => DEC: 128
                            {
                                return Encoding.UTF8;
                            }
                        }
                    }
                }
            }

            return Encoding.ASCII;
        }

    }
}
