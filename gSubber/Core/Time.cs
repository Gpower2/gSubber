using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubber.Core
{
    public class Time : IEquatable<Time>, IComparable<Time>, IComparable
    {
        public enum TimeFormat
        {
            WithSeconds,
            WithMilliseconds,
            WithMicroseconds,
            WithNanoseconds
        }

        private int _Hours;

        public int Hours
        {
            get { return _Hours; }
            set { _Hours = value; }
        }

        public double TotalHours {
            get
            {
                return Convert.ToDouble(_Hours) +
                    Convert.ToDouble(_Minutes) / 60.0 +
                    Convert.ToDouble(_Seconds) / (60.0 * 60.0) +
                    Convert.ToDouble(_Milliseconds) / (60.0 * 60.0 * 1000.0) +
                    Convert.ToDouble(_Microseconds) / (60.0 * 60.0 * 1000.0 * 1000.0) +
                    Convert.ToDouble(_Nanoseconds) / (60.0 * 60.0 * 1000.0 * 1000.0 * 1000.0);
            }
        }

        private int _Minutes;

        public int Minutes
        {
            get { return _Minutes; }
            set
            {
                // Check if bigger than 1 hour
                if (value > 60)
                {
                    // Assign the true minutes to the value
                    _Minutes = value - ((value / 60) * 60);
                    // Add the full hours to the existing hours
                    Hours += ((value / 60) * 60);
                }
                else
                {
                    _Minutes = value;
                }
            }
        }

        public double TotalMinutes
        {
            get {
                return Convert.ToDouble(_Hours * 60.0) +
                    Convert.ToDouble(_Minutes) +
                    Convert.ToDouble(_Seconds) / (60.0) +
                    Convert.ToDouble(_Milliseconds) / (60.0 * 1000.0) +
                    Convert.ToDouble(_Microseconds) / (60.0 * 1000.0 * 1000.0) +
                    Convert.ToDouble(_Nanoseconds) / (60.0 * 1000.0 * 1000.0 * 1000.0);
            }
        }

        private int _Seconds;

        public int Seconds
        {
            get { return _Seconds; }
            set
            {
                // Check if bigger than 1 minute
                if (value > 60)
                {
                    // Assign the true seconds to the value
                    _Seconds = value - ((value / 60) * 60);
                    // Add the full minutes to the existing minutes
                    Minutes += ((value / 60) * 60);
                }
                else
                {
                    _Seconds = value;
                }
            }
        }

        public double TotalSeconds {
            get
            {
                return Convert.ToDouble(_Hours * 60.0 * 60.0) +
                    Convert.ToDouble(_Minutes * 60.0) +
                    Convert.ToDouble(_Seconds) +
                    Convert.ToDouble(_Milliseconds) / (1000.0) +
                    Convert.ToDouble(_Microseconds) / (1000.0 * 1000.0) +
                    Convert.ToDouble(_Nanoseconds) / (1000.0 * 1000.0 * 1000.0);
            }
        }

        private int _Milliseconds;

        public int Milliseconds
        {
            get { return _Milliseconds; }
            set
            {
                // Check if bigger than 1 second
                if (value > 1000)
                {
                    // Assign the true milliseconds to the value
                    _Milliseconds = value - ((value / 1000) * 1000);
                    // Add the full seconds to the existing seconds
                    Seconds += ((value / 1000) * 1000);
                }
                else
                {
                    _Milliseconds = value;
                }
            }
        }

        public double TotalMilliseconds
        {
            get
            {
                return Convert.ToDouble(_Hours * 60.0 * 60.0 * 1000.0) +
                    Convert.ToDouble(_Minutes * 60.0 * 1000.0) +
                    Convert.ToDouble(_Seconds * 1000.0) +
                    Convert.ToDouble(_Milliseconds) +
                    Convert.ToDouble(_Microseconds) / (1000.0) +
                    Convert.ToDouble(_Nanoseconds) / (1000.0 * 1000.0);
            }
        }

        private int _Microseconds;

        public int Microseconds
        {
            get { return _Microseconds; }
            set
            {
                // Check if bigger than 1 millisecond
                if (value > 1000)
                {
                    // Assign the true microseconds to the value
                    _Microseconds = value - ((value / 1000) * 1000);
                    // Add the full milliseconds to the existing milliseconds
                    Milliseconds += ((value / 1000) * 1000);
                }
                else
                {
                    _Microseconds = value;
                }
            }
        }

        public double TotalMicroseconds
        {
            get
            {
                return Convert.ToDouble(_Hours * 60.0 * 60.0 * 1000.0 * 1000.0) +
                    Convert.ToDouble(_Minutes * 60.0 * 1000.0 * 1000.0) +
                    Convert.ToDouble(_Seconds * 1000.0 * 1000.0) +
                    Convert.ToDouble(_Milliseconds * 1000.0) +
                    Convert.ToDouble(_Microseconds) +
                    Convert.ToDouble(_Nanoseconds) / (1000.0);
            }
        }

        private int _Nanoseconds;

        public int Nanoseconds
        {
            get { return _Nanoseconds; }
            set
            {
                // Check if bigger than 1 microsecond
                if (value > 1000)
                {
                    // Assign the true nanoseconds to the value
                    _Nanoseconds = value - ((value / 1000) * 1000);
                    // Add the full microseconds to the existing microseconds
                    Microseconds += ((value / 1000) * 1000);
                }
                else
                {
                    _Nanoseconds = value;
                }
            }
        }

        public double TotalNanoseconds
        {
            get
            {
                return Convert.ToDouble(_Hours * 60.0 * 60.0 * 1000.0 * 1000.0 * 1000.0) +
                    Convert.ToDouble(_Minutes * 60.0 * 1000.0 * 1000.0 * 1000.0) +
                    Convert.ToDouble(_Seconds * 1000.0 * 1000.0 * 1000.0) +
                    Convert.ToDouble(_Milliseconds * 1000.0 * 1000.0) +
                    Convert.ToDouble(_Microseconds * 1000.0) +
                    Convert.ToDouble(_Nanoseconds);
            }
        }

        public static Time Now
        {
            get
            {
                DateTime now = DateTime.Now;
                return new Time(now.Hour, now.Minute, now.Second, now.Millisecond);
            }
        }

        public Time() { }

        public Time(int argHours, int argMinutes)
        {
            Hours = argHours;
            Minutes = argMinutes;
        }

        public Time(int argHours, int argMinutes, int argSeconds)
        {
            Hours = argHours;
            Minutes = argMinutes;
            Seconds = argSeconds;
        }

        public Time(int argHours, int argMinutes, int argSeconds, int argMilliseconds)
        {
            Hours = argHours;
            Minutes = argMinutes;
            Seconds = argSeconds;
            Milliseconds = argMilliseconds;
        }

        public Time(int argHours, int argMinutes, int argSeconds, int argMilliseconds, int argMicroseconds)
        {
            Hours = argHours;
            Minutes = argMinutes;
            Seconds = argSeconds;
            Milliseconds = argMilliseconds;
            Microseconds = argMicroseconds;
        }

        public Time(int argHours, int argMinutes, int argSeconds, int argMilliseconds, int argMicroseconds, int argNanoseconds)
        {
            Hours = argHours;
            Minutes = argMinutes;
            Seconds = argSeconds;
            Milliseconds = argMilliseconds;
            Microseconds = argMicroseconds;
            Nanoseconds = argNanoseconds;
        }

        public static Time operator +(Time argTime1, Time argTime2)
        {
            return new Time(
                argTime1.Hours + argTime2.Hours,
                argTime1.Minutes + argTime2.Minutes,
                argTime1.Seconds + argTime2.Seconds,
                argTime1.Milliseconds + argTime2.Milliseconds,
                argTime1.Microseconds + argTime2.Microseconds,
                argTime1.Nanoseconds + argTime2.Nanoseconds);
        }

        public static Time operator -(Time argTime1, Time argTime2)
        {
            return new Time(
                argTime1.Hours - argTime2.Hours,
                argTime1.Minutes - argTime2.Minutes,
                argTime1.Seconds - argTime2.Seconds,
                argTime1.Milliseconds - argTime2.Milliseconds,
                argTime1.Microseconds - argTime2.Microseconds,
                argTime1.Nanoseconds - argTime2.Nanoseconds);
        }

        public static bool operator >(Time argTime1, Time argTime2)
        {
            return argTime1?.TotalNanoseconds > argTime2?.TotalNanoseconds;
        }

        public static bool operator >=(Time argTime1, Time argTime2)
        {
            return argTime1?.TotalNanoseconds >= argTime2?.TotalNanoseconds;
        }

        public static bool operator <(Time argTime1, Time argTime2)
        {
            return argTime1?.TotalNanoseconds < argTime2?.TotalNanoseconds;
        }

        public static bool operator <=(Time argTime1, Time argTime2)
        {
            return argTime1?.TotalNanoseconds <= argTime2?.TotalNanoseconds;
        }

        public void Clear()
        {
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
            Milliseconds = 0;
            Nanoseconds = 0;
        }

        public override string ToString()
        {
            return string.Format("{0:#00}:{1:00}:{2:00}.{3:000}", Hours, Minutes, Seconds, Milliseconds);
        }

        public string ToString(TimeFormat argTimeFormat)
        {
            switch (argTimeFormat)
            {
                case TimeFormat.WithSeconds:
                    return string.Format("{0:#00}:{1:00}:{2:00}", Hours, Minutes, Seconds);
                case TimeFormat.WithMilliseconds:
                    return string.Format("{0:#00}:{1:00}:{2:00}.{3:000}", Hours, Minutes, Seconds, Milliseconds);
                case TimeFormat.WithMicroseconds:
                    return string.Format("{0:#00}:{1:00}:{2:00}.{3:000}{4:000}", Hours, Minutes, Seconds, Milliseconds, Microseconds);
                case TimeFormat.WithNanoseconds:
                    return string.Format("{0:#00}:{1:00}:{2:00}.{3:000}{4:000}{5:000}", Hours, Minutes, Seconds, Milliseconds, Microseconds, Nanoseconds);
                default:
                    return string.Format("{0:#00}:{1:00}:{2:00}.{3:000}", Hours, Minutes, Seconds, Milliseconds);
            } 
        }

        public override bool Equals(object argTime)
        {
            if (!(argTime is Time)) 
                return false;
            
            return Equals((Time)argTime);
        }

        public bool Equals(Time other)
        {
            return (other != null) && (TotalNanoseconds == other.TotalNanoseconds);
        }

        public override int GetHashCode()
        {
            return TotalNanoseconds.GetHashCode(); 
        }

        public int CompareTo(Time other)
        {
            return TotalNanoseconds.CompareTo(other.TotalNanoseconds);
        }

        public int CompareTo(object obj)
        {
            return TotalNanoseconds.CompareTo((obj as Time).TotalNanoseconds);
        }
    }
}
