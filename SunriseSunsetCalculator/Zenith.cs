using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunriseSunsetCalculator
{
    /// <summary>
    /// Defines the solar declination used in computer the sunrise/sunet
    /// </summary>
    public class Zenith
    {
        /// <summary>
        /// Astronomical sunrise/set is when the sun is 18 degrees below the horizon
        /// </summary>
        public static readonly Zenith ASTRONOMICAL = new Zenith(108);

        /// <summary>
        /// Nautical sunrise/set is when the sun is 12 degrees below the horizon
        /// </summary>
        public static readonly Zenith NAUTICAL = new Zenith(102);

        /// <summary>
        /// Civil sunrise/set (dawn/dusk) is when the sun is 6 degress below the horizon
        /// </summary>
        public static readonly Zenith CIVIL = new Zenith(96);

        /// <summary>
        /// Official sunrise/sset is when the sun is 50' below the horizon
        /// </summary>
        public static readonly Zenith OFFICIAL = new Zenith(90.8333);

        public double degrees
        {
            get;
            private set;
        }

        public Zenith(double degrees)
        {
            this.degrees = degrees;
        }
    }
}
