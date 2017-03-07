using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunriseSunsetCalculator
{
    /// <summary>
    /// Public class for getting the various types of sunrise/sunset
    /// </summary>
    public class SunriseSunsetCalculator
    {
        /// <summary>
        /// Get the <see cref="Location"/>
        /// </summary>
        public Location location
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the <see cref="SolarEventCalculator"/>
        /// </summary>
        public SolarEventCalculator calculator
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructs a new <see cref="SunriseSunsetCalculator"/> with the given <see cref="Location"/>
        /// </summary>
        /// <param name="location"><see cref="Location"/> object containing the latitude/longitude of the location to computer for sunrise/sunset for</param>
        public SunriseSunsetCalculator(Location location)
        {
            this.location = location;
            this.calculator = new SolarEventCalculator(location);
        }

        /// <summary>
        /// Returns the astronomical (108deg) sunrise for the given date
        /// </summary>
        /// <param name="date"><see cref="DateTime"/> object containing the date to computer the astronomical sunrise for</param>
        /// <returns>The astronomical sunrise time in HH:MM (24-hour clock) form</returns>
        public string GetAstronomicalSunriseForDate(DateTime date)
        {
            return calculator.ComputeSunriseTime(Zenith.ASTRONOMICAL, date);
        }

        /// <summary>
        /// Returns the astronomical (108deg) sunrise for the given date
        /// </summary>
        /// <param name="date"><see cref="DateTime"/> object containing the date to computer the astronomical sunrise for</param>
        /// <returns>The astronomical sunrise time as a <see cref="DateTime"/></returns>
        public DateTime? GetAstronomicalSunriseDateTimeForDate(DateTime date)
        {
            return calculator.ComputeSunriseCalendar(Zenith.ASTRONOMICAL, date);
        }

        public string GetAstronomicalSunsetForDate(DateTime date)
        {
            return calculator.ComputeSunsetTime(Zenith.ASTRONOMICAL, date);
        }

        public DateTime? GetAstronomicalSunsetDateTimeForDate(DateTime date)
        {
            return calculator.ComputeSunsetCalendar(Zenith.ASTRONOMICAL, date);
        }

        public string GetNauticalSunriseForDate(DateTime date)
        {
            return calculator.ComputeSunriseTime(Zenith.NAUTICAL, date);
        }

        public DateTime? GetNauticalSunriseDateTimeForDate(DateTime date)
        {
            return calculator.ComputeSunriseCalendar(Zenith.NAUTICAL, date);
        }

        public string GetNauticalSunsetForDate(DateTime date)
        {
            return calculator.ComputeSunsetTime(Zenith.NAUTICAL, date);
        }

        public DateTime? GetNauticalSunsetDateTimeForDate(DateTime date)
        {
            return calculator.ComputeSunsetCalendar(Zenith.NAUTICAL, date);
        }

        public string GetCivilSunriseForDate(DateTime date)
        {
            return calculator.ComputeSunriseTime(Zenith.CIVIL, date);
        }

        public DateTime? GetCivilSunriseDateTimeForDate(DateTime date)
        {
            return calculator.ComputeSunriseCalendar(Zenith.CIVIL, date);
        }

        public string GetCivilSunsetForDate(DateTime date)
        {
            return calculator.ComputeSunsetTime(Zenith.CIVIL, date);
        }

        public DateTime? GetCivilSunsetDateTimeForDate(DateTime date)
        {
            return calculator.ComputeSunsetCalendar(Zenith.CIVIL, date);
        }

        public string GetOfficalSunriseForDate(DateTime date)
        {
            return calculator.ComputeSunriseTime(Zenith.OFFICIAL, date);
        }

        public DateTime? GetOfficialSunriseDateTimeForDate(DateTime date)
        {
            return calculator.ComputeSunriseCalendar(Zenith.OFFICIAL, date);
        }

        public string GetOfficialSunsetForDate(DateTime date)
        {
            return calculator.ComputeSunsetTime(Zenith.OFFICIAL, date);
        }

        public DateTime? GetOfficalSunsetDateTimeForDate(DateTime date)
        {
            return calculator.ComputeSunsetCalendar(Zenith.OFFICIAL, date);
        }

        public static DateTime? GetSunrise(double latitude, double longitude, DateTime date, double degrees)
        {
            SolarEventCalculator solarEventCalculator = new SolarEventCalculator(new Location(latitude, longitude));
            return solarEventCalculator.ComputeSunriseCalendar(new Zenith(90 - degrees), date);
        }

        public static DateTime? GetSunset(double latitude, double longitude, DateTime date, double degrees)
        {
            SolarEventCalculator solarEventCalculator = new SolarEventCalculator(new Location(latitude, longitude));
            return solarEventCalculator.ComputeSunsetCalendar(new Zenith(90 - degrees), date);
        }
    }
}
