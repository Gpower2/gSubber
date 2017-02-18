using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace gSubber
{
    public enum BorderStyle
    {
        WithoutOpaqueBox = 1,
        WithOpaqueBox = 3
    }

    public enum ScreenAlignment
    {
        UpperLeft = 7,
        UpperCenter = 8,
        UpperRight = 9,
        MiddleLeft = 4,
        MiddleCenter = 5,
        MiddleRight = 6,
        LowerLeft = 1,
        LowerCenter = 2,
        LowerRight = 3
    }

    public enum FontEncoding
    {
        ANSI = 0,
        Default = 1,
        Symbol = 2,
        Mac = 77,
        Shit_JIS = 128,
        Hangeul = 129,
        Johab = 130,
        GB2312 = 134,
        Chinese_BIG5 = 136,
        Greek = 161,
        Turkish = 162,
        Vietnamese = 163,
        Hebrew = 177,
        Arabic = 178,
        Baltic = 186,
        Russian = 204,
        Thai = 222,
        East_European = 238,
        OEM = 255
    }

    public class SubFileStyleItem
    {
        public String Name { get; set; }

        public String Fontname { get; set; }

        public float Fontsize { get; set; }

        public FontEncoding FontEncoding { get; set; }


        public bool Bold { get; set; }

        public bool Italic { get; set; }

        public bool Underline { get; set; }

        public bool StrikeOut { get; set; }

        public float Spacing { get; set; }


        public Color PrimaryColor { get; set; }

        public Color SecondaryColor { get; set; }

        public Color OutlineColor { get; set; }

        public Color BackColor { get; set; }


        public double ScaleX { get; set; }

        public double ScaleY { get; set; }

        public double RotationAngle { get; set; }


        public BorderStyle BorderStyle { get; set; }

        public double OutlineSize { get; set; }

        public double ShadowSize { get; set; }

        public ScreenAlignment ScreenAlignment { get; set; }


        public double MarginLeft { get; set; }

        public double MarginRight { get; set; }

        public double MarginVertical { get; set; }

        public override string ToString()
        {
            return Name ?? "";
        }
    }
}
