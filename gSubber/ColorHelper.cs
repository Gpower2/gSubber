using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;

namespace gSubber
{
    public static class ColorHelper
    {
        public static Color FromASS(String argAssColor)
        {
            //&H00693212
            if (String.IsNullOrWhiteSpace(argAssColor))
            {
                throw new Exception("Empty ASS color!");
            }
            argAssColor = argAssColor.Trim();
            if (argAssColor.Length != 10 && argAssColor.Length != 8)
            {
                throw new Exception("The ASS color is malformed!");
            }
            string assColorToParse = argAssColor;
            if(argAssColor.Length == 10)
            {
                assColorToParse = argAssColor.Substring(2);
            }
            byte dummyByte, red, green, blue, alpha;
            if (byte.TryParse(assColorToParse.Substring(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out dummyByte))
            {
                red = dummyByte;
            }
            else
            {
                throw new Exception("The ASS color is malformed! (red)");
            }
            if (byte.TryParse(assColorToParse.Substring(2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out dummyByte))
            {
                green = dummyByte;
            }
            else
            {
                throw new Exception("The ASS color is malformed! (green)");
            }
            if (byte.TryParse(assColorToParse.Substring(4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out dummyByte))
            {
                blue = dummyByte;
            }
            else
            {
                throw new Exception("The ASS color is malformed! (blue)");
            }
            if (byte.TryParse(assColorToParse.Substring(6, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out dummyByte))
            {
                alpha = dummyByte;
            }
            else
            {
                throw new Exception("The ASS color is malformed! (Alpha)");
            }

            return Color.FromArgb(alpha, red, green, blue);
        }
    }
}
