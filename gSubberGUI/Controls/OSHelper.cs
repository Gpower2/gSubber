using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gSubberGUI.Controls
{
    public static class OSHelper
    {
        /// <summary>
        /// Returns if the running Platform is Linux Or MacOSX
        /// </summary>
        public static Boolean IsOnLinuxOrMac
        {
            get
            {
                PlatformID myPlatform = Environment.OSVersion.Platform;
                // 128 is Mono 1.x specific value for Linux systems, so it's there to provide compatibility
                return (myPlatform == PlatformID.Unix) || (myPlatform == PlatformID.MacOSX) || ((Int32)myPlatform == 128);
            }
        }

        /// <summary>
        /// Returns if the running Platform is Linux
        /// </summary>
        public static Boolean IsOnLinux
        {
            get
            {
                PlatformID myPlatform = Environment.OSVersion.Platform;
                // 128 is Mono 1.x specific value for Linux systems, so it's there to provide compatibility
                return (myPlatform == PlatformID.Unix) || ((Int32)myPlatform == 128);
            }
        }

        /// <summary>
        /// Returns if the running Platform is MacOSX
        /// </summary>
        public static Boolean IsOnMac
        {
            get
            {
                PlatformID myPlatform = Environment.OSVersion.Platform;
                return ((myPlatform == PlatformID.MacOSX));
            }
        }

        /// <summary>
        /// Returns if the running Platform is Windows
        /// </summary>
        public static Boolean IsOnWindows
        {
            get
            {
                PlatformID myPlatform = Environment.OSVersion.Platform;
                // 128 is Mono 1.x specific value for Linux systems, so it's there to provide compatibility
                return !((myPlatform == PlatformID.Unix) || (myPlatform == PlatformID.MacOSX) || ((Int32)myPlatform == 128));
            }
        }
    }
}
