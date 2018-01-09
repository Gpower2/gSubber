using gSubber.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace gSubber.Helpers
{
    public static class TimeHelper
    {
        public static Time FromAssTime(String argTime)
        {
            if (String.IsNullOrWhiteSpace(argTime))
            {
                throw new Exception("Empty ASS time!");
            }
            argTime = argTime.Trim();
            //0:04:59.88
            string[] timeElements = argTime.Split(new string[] { ":" }, StringSplitOptions.None);
            if(timeElements.Length != 3)
            {
                throw new Exception("Malformed ASS time!");
            }
            Int32 hours, minutes, seconds, milliseconds, dummyInt;
            if (Int32.TryParse(timeElements[0], NumberStyles.Any, CultureInfo.InvariantCulture, out dummyInt))
            {
                hours = dummyInt;
            }
            else
            {
                throw new Exception("The ASS time is malformed! (hours)");
            }
            if (Int32.TryParse(timeElements[1], NumberStyles.Any, CultureInfo.InvariantCulture, out dummyInt))
            {
                minutes = dummyInt;
            }
            else
            {
                throw new Exception("The ASS time is malformed! (minutes)");
            }
            string[] secondsElements = timeElements[2].Split(new string[] { "." }, StringSplitOptions.None);
            if (secondsElements.Length != 2)
            {
                throw new Exception("Malformed ASS time!");
            }
            if (Int32.TryParse(secondsElements[0], NumberStyles.Any, CultureInfo.InvariantCulture, out dummyInt))
            {
                seconds = dummyInt;
            }
            else
            {
                throw new Exception("The ASS time is malformed! (seconds)");
            }
            if (Int32.TryParse(secondsElements[1], NumberStyles.Any, CultureInfo.InvariantCulture, out dummyInt))
            {
                // Ass format specifies deciseconds instead of milliseconds
                milliseconds = dummyInt * 10;
            }
            else
            {
                throw new Exception("The ASS time is malformed! (milliseconds)");
            }

            return new Time(hours, minutes, seconds, milliseconds);
        }

        public static Time FromSrtTime(String argTime)
        {
            if (String.IsNullOrWhiteSpace(argTime))
            {
                throw new Exception("Empty SRT time!");
            }
            argTime = argTime.Trim();
            //00:00:01,742
            string[] timeElements = argTime.Split(new string[] { ":" }, StringSplitOptions.None);
            if (timeElements.Length != 3)
            {
                throw new Exception("Malformed SRT time!");
            }
            Int32 hours, minutes, seconds, milliseconds, dummyInt;
            if (Int32.TryParse(timeElements[0], NumberStyles.Any, CultureInfo.InvariantCulture, out dummyInt))
            {
                hours = dummyInt;
            }
            else
            {
                throw new Exception("The SRT time is malformed! (hours)");
            }
            if (Int32.TryParse(timeElements[1], NumberStyles.Any, CultureInfo.InvariantCulture, out dummyInt))
            {
                minutes = dummyInt;
            }
            else
            {
                throw new Exception("The SRT time is malformed! (minutes)");
            }
            string secondsSeparator = ",";
            if (timeElements[2].Contains("."))
            {
                secondsSeparator = ".";
            }

            string[] secondsElements = timeElements[2].Split(new string[] { secondsSeparator }, StringSplitOptions.None);
            if (secondsElements.Length != 2)
            {
                throw new Exception("Malformed SRT time!");
            }
            if (Int32.TryParse(secondsElements[0], NumberStyles.Any, CultureInfo.InvariantCulture, out dummyInt))
            {
                seconds = dummyInt;
            }
            else
            {
                throw new Exception("The SRT time is malformed! (seconds)");
            }
            if (Int32.TryParse(secondsElements[1], NumberStyles.Any, CultureInfo.InvariantCulture, out dummyInt))
            {
                milliseconds = dummyInt;
            }
            else
            {
                throw new Exception("The SRT time is malformed! (milliseconds)");
            }

            return new Time(hours, minutes, seconds, milliseconds);
        }

    }
}
