using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunriseSunsetCalculator
{
    /// <summary>
    /// Manage common math functions
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// Get the date of the year for a <see cref="DateTime"/>
        /// </summary>
        /// <param name="date"><see cref="DateTime"/> to get the day of the year for</param>
        /// <returns>Day of the Year</returns>
        public static double GetDayOfYear(DateTime date)
        {
            return date.DayOfYear;
        }

        /// <summary>
        /// Get the UTC Offset in total hours for a <see cref="DateTime"/>
        /// </summary>
        /// <param name="date"><see cref="DateTime"/> to find the UTC offset in hours</param>
        /// <returns>UTC Offset in hours</returns>
        public static double GetUTCOffset(DateTime date)
        {
            return TimeZoneInfo.Utc.GetUtcOffset(date).TotalHours;
        }

        /// <summary>
        /// Get the Arc Cosine from the radians
        /// </summary>
        /// <param name="radians">Radians parameter</param>
        /// <returns>Arc Cosine</returns>
        public static double GetArcCosineFor(double radians)
        {
            var arcCosine = Math.Acos(radians);
            return SetScale(arcCosine);
        }

        /// <summary>
        /// Convert Radians to Degrees
        /// </summary>
        /// <param name="radians">Radians parameter</param>
        /// <returns>Degrees from Radians</returns>
        public static double ConvertRadiansToDegrees(double radians)
        {
            return SetScale(radians * (180d / Math.PI));
        }

        /// <summary>
        /// Convert Degrees to Radians
        /// </summary>
        /// <param name="degrees">Degrees parameter</param>
        /// <returns>Radians from Degrees</returns>
        public static double ConvertDegreesToRadians(double degrees)
        {
            return SetScale(degrees * (Math.PI / 180d));
        }

        /// <summary>
        /// Round the number to 4 decimal places
        /// </summary>
        /// <param name="number">Number to round</param>
        /// <returns>Rounded number</returns>
        public static double SetScale(double number)
        {
            return Math.Round(number, 4, MidpointRounding.ToEven);
        }
    }
}
