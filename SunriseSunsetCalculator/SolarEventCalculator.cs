using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunriseSunsetCalculator
{
    /// <summary>
    /// Parent class of the Sunrise and Sunset calculator classes
    /// </summary>
    public class SolarEventCalculator
    {
        /// <summary>
        /// Get the <see cref="Location"/> used by <see cref="SolarEventCalculator"/>
        /// </summary>
        public Location location
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the <see cref="TimeZoneInfo"/> used by <see cref="SolarEventCalculator"/>
        /// </summary>
        public TimeZoneInfo timeZone
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructs a new <see cref="SolarEventCalculator"/> using the given parameters
        /// </summary>
        /// <param name="location"><see cref="Location"/> of the place where the solar event should be calculated from</param>
        /// <param name="timeZone"><see cref="TimeZoneInfo"/> of the location parameter</param>
        public SolarEventCalculator(Location location)
        {
            this.location = location;
            this.timeZone = TimeZoneInfo.Local;
        }

        /// <summary>
        /// Computes the sunset time for the given zenith at the given date
        /// </summary>
        /// <param name="solarZenith"><see cref="Zenith"/> enum corresponding to the type of sunset to compute</param>
        /// <param name="date"><see cref="DateTime"/> object representing the date to compute the sunset for</param>
        /// <returns>The sunset time, in HH:MM format (24-hour clock), 00:00 if the sun does not set on the given date</returns>
        public string ComputeSunriseTime(Zenith solarZenith, DateTime date)
        {
            return GetLocalTimeAsString(ComputerSolarEventTime(solarZenith, date, true));
        }

        /// <summary>
        /// Computes the sunrise time for the given zenith at the given date
        /// </summary>
        /// <param name="solarZenith"><see cref="Zenith"/> enum corresponding to the type of sunrise to compute</param>
        /// <param name="date"><see cref="DateTime"/> object representing the date to compute the sunrise for</param>
        /// <returns>The sunset time as a <see cref="DateTime?"/></returns>
        public DateTime? ComputeSunriseCalendar(Zenith solarZenith, DateTime date)
        {
            return GetLocalTimeAsDateTime(ComputerSolarEventTime(solarZenith, date, true), date);
        }

        /// <summary>
        /// Computes the sunset time for the given zenith at the given date
        /// </summary>
        /// <param name="solarZenith"><see cref="Zenith"/> enum corresponding to the type of sunset to compute</param>
        /// <param name="date"><see cref="DateTime"/> object representing the date to compute the sunset for</param>
        /// <returns>The sunset time, in HH:MM format (24-hour clock), 00:00 if the sun does not set on the given date</returns>
        public string ComputeSunsetTime(Zenith solarZenith, DateTime date)
        {
            return GetLocalTimeAsString(ComputerSolarEventTime(solarZenith, date, false));
        }

        /// <summary>
        /// Computes the sunset time for the given zenith at the given date
        /// </summary>
        /// <param name="solarZenith"><see cref="Zenith"/> enum corresponding to the type of sunset to compute</param>
        /// <param name="date"><see cref="DateTime"/> object representing the date to compute the sunset for</param>
        /// <returns>The sunset time as a <see cref="DateTime?"/></returns>
        public DateTime? ComputeSunsetCalendar(Zenith solarZenith, DateTime date)
        {
            return GetLocalTimeAsDateTime(ComputerSolarEventTime(solarZenith, date, false), date);
        }

        private double ComputerSolarEventTime(Zenith solarZenith, DateTime date, bool isSunrise)
        {
            date = TimeZoneInfo.ConvertTime(date, this.timeZone);
            double longitudeHour = GetLongitudeHour(date, isSunrise);

            double meanAnomaly = GetMeanAnomaly(longitudeHour);
            double sunTrueLong = GetSunTrueLongitude(meanAnomaly);
            double cosineSunLocalHour = GetCosineSunLocalHour(sunTrueLong, solarZenith);
            if ((cosineSunLocalHour < -1d) || (cosineSunLocalHour > 1d))
                throw new ArgumentOutOfRangeException("Sun Local Hour out of range");

            double sunLocalHour = GetSunLocalHour(cosineSunLocalHour, isSunrise);
            double localMeanTime = GetLocalMeanTime(sunTrueLong, longitudeHour, sunLocalHour);
            double localTime = GetLocalTime(localMeanTime, date);
            return localTime;
        }

        private double GetBaseLongitudeHour()
        {
            return Utility.SetScale(this.location.longitude / 15d);
        }

        private double GetLongitudeHour(DateTime date, bool isSunrise)
        {
            int offset = 18;
            if (isSunrise)
                offset = 6;

            double dividend = offset - GetBaseLongitudeHour();
            double addend = Utility.SetScale(dividend / 24d);
            double longHour = Utility.GetDayOfYear(date) + addend;
            return Utility.SetScale(longHour);
        }

        private double GetMeanAnomaly(double longitudeHour)
        {
            double meanAnomaly = Utility.SetScale(0.9856d * longitudeHour) - 3.289d;
            return Utility.SetScale(meanAnomaly);
        }

        private double GetSunTrueLongitude(double meanAnomaly)
        {
            double sinMeanAnomaly = Math.Sin(Utility.ConvertDegreesToRadians(meanAnomaly));
            double sinDoubleMeanAnomaly = Math.Sin(Utility.SetScale(Utility.ConvertDegreesToRadians(meanAnomaly) * 2d));

            double firstPart = meanAnomaly + Utility.SetScale(sinMeanAnomaly * 1.916d);
            double secondPart = Utility.SetScale(sinMeanAnomaly * 0.020d) + 282.634d;
            double trueLongitude = firstPart + secondPart;

            if (trueLongitude > 360)
                trueLongitude = trueLongitude - 360d;

            return Utility.SetScale(trueLongitude);
        }

        private double GetRightAscension(double sunTrueLong)
        {
            double tanL = Math.Tan(Utility.ConvertDegreesToRadians(sunTrueLong));

            double innerParens = Utility.SetScale(Utility.ConvertRadiansToDegrees(tanL) * 0.91764d);
            double rightAscension = Math.Atan(Utility.ConvertDegreesToRadians(innerParens));
            rightAscension = Utility.SetScale(Utility.ConvertRadiansToDegrees(rightAscension));

            if (rightAscension < 0d)
                rightAscension = rightAscension + 360d;
            else if (rightAscension > 360d)
                rightAscension = rightAscension - 360d;

            double ninety = 90d;
            double longitudeQuadrant = Math.Floor(sunTrueLong / ninety);
            longitudeQuadrant = longitudeQuadrant * ninety;

            double rightAscensionQuadrant = Math.Floor(rightAscension / ninety);
            rightAscensionQuadrant = rightAscensionQuadrant * ninety;

            double augend = longitudeQuadrant - rightAscensionQuadrant;
            return Utility.SetScale((rightAscensionQuadrant + augend) / 15d);
        }

        private double GetCosineSunLocalHour(double sunTrueLong, Zenith zenith)
        {
            double sinSunDeclination = GetSinOfSunDeclination(sunTrueLong);
            double cosineSunDeclination = GetCosineOfSunDeclination(sinSunDeclination);

            double zenithInRads = Utility.ConvertDegreesToRadians(zenith.degrees);
            double cosineZenith = Math.Cos(zenithInRads);
            double sinLatitude = Math.Sin(Utility.ConvertDegreesToRadians(this.location.latitude));
            double cosLatitude = Math.Cos(Utility.ConvertDegreesToRadians(this.location.latitude));

            double sinDeclinationTimesSinLat = sinSunDeclination * sinLatitude;
            double dividend = cosineZenith - sinDeclinationTimesSinLat;
            double divisor = cosineSunDeclination * cosLatitude;

            return Utility.SetScale(dividend / divisor);
        }

        private double GetSinOfSunDeclination(double sunTrueLong)
        {
            double sinTrueLongitude = Math.Sin(Utility.ConvertDegreesToRadians(sunTrueLong));
            double sinOfDeclination = sinTrueLongitude * 0.39782d;
            return Utility.SetScale(sinOfDeclination);
        }

        private double GetCosineOfSunDeclination(double sinSunDeclination)
        {
            double arcSinOfSinDeclination = Math.Asin(sinSunDeclination);
            double cosDeclination = Math.Cos(arcSinOfSinDeclination);
            return Utility.SetScale(cosDeclination);
        }

        private double GetSunLocalHour(double cosineSunLocalHour, bool isSunrise)
        {
            double arcCosineOfCosineHourAngle = Utility.GetArcCosineFor(cosineSunLocalHour);
            double localHour = Utility.ConvertRadiansToDegrees(arcCosineOfCosineHourAngle);
            if (isSunrise)
                localHour = 360d - localHour;

            return Utility.SetScale(localHour / 15d);
        }

        private double GetLocalMeanTime(double sunTrueLong, double longitudeHour, double sunLocalHour)
        {
            double rightAscension = GetRightAscension(sunTrueLong);
            double innerParens = longitudeHour * 0.06571d;
            double localMeanTime = sunLocalHour + rightAscension - innerParens;
            localMeanTime = localMeanTime - 6.622d;

            if (localMeanTime < 0d)
                localMeanTime = localMeanTime + 24d;
            else if (localMeanTime > 24d)
                localMeanTime = localMeanTime - 24d;

            return Utility.SetScale(localMeanTime);
        }

        private double GetLocalTime(double localMeanTime, DateTime date)
        {
            double utcTime = localMeanTime - GetBaseLongitudeHour();
            double utcOffset = Utility.GetUTCOffset(date);
            double utcOffsetTime = utcTime + utcOffset;
            return AdjustForDST(utcOffsetTime, date);
        }

        private double AdjustForDST(double localMeanTime, DateTime date)
        {
            double localTime = localMeanTime;
            if (timeZone.IsDaylightSavingTime(date))
                localTime = localTime + 1d;
            if (localTime > 24d)
                localTime = localTime - 24d;

            return localTime;
        }

        /// <summary>
        /// Returns the local sunrise/set time in the form HH:MM
        /// </summary>
        /// <param name="localTimeParam"><see cref="double?"/> representation of the local sunrise/set time</param>
        /// <returns><see cref="string"/> representation of the local sunrise/set time in the HH:MM format</returns>
        private string GetLocalTimeAsString(double? localTimeParam)
        {
            if (localTimeParam == null)
                return "99:99";

            double localTime = localTimeParam.Value;
            if (localTime.CompareTo(0) == -1d)
                localTime = localTime + 24d;
            string[] timeComponents = localTime.ToString().Split('.');
            int hour = 0;
            if (!int.TryParse(timeComponents[0], out hour))
                throw new ArgumentException("Time component could not be converted");
            if (hour < 0 || hour > 24)
                throw new ArgumentOutOfRangeException("Hour is out of range (" + hour + ")");

            double minutes = 0d;
            if (!double.TryParse("0." + timeComponents[1], out minutes))
                throw new ArgumentException("Time component could not be converted");
            minutes = Math.Round(minutes * 60d, 0, MidpointRounding.ToEven);
            if (minutes > 60d)
                throw new ArgumentException("Minute is out of range (" + minutes + ")");
            if ((int)minutes == 60)
            {
                minutes = 0d;
                hour += 1;
            }
            if (hour == 24)
                hour = 0;

            return string.Format("{0}:{1}", hour.ToString("N2"), minutes.ToString("N2"));
        }

        /// <summary>
        /// Returns the local sunrise/set time in the form HH:MM
        /// </summary>
        /// <param name="localTimeParam"><see cref="double?"/> representation of the local sunrise/set time</param>
        /// <param name="date"><see cref="DateTime"/> representation of the local time as a calendar or null for none</param>
        /// <returns><see cref="DateTime"/> representation of the local time as a calendar, or null for none</returns>
        protected DateTime? GetLocalTimeAsDateTime(double? localTimeParam, DateTime date)
        {
            if(localTimeParam == null)
                return null;

            DateTime resultTime = date;

            double localTime = localTimeParam.Value;
            if(localTime.CompareTo(0) == -1)
            {
                localTime = localTime + 24.0d;
                resultTime.Add(new TimeSpan(-24, 0, 0));
            }
            string[] timeComponents = localTime.ToString().Split('.');
            int hour = 0;
            if (!int.TryParse(timeComponents[0], out hour))
                throw new ArgumentException("Time component could not be converted");
            if (hour < 0 || hour > 24)
                throw new ArgumentOutOfRangeException("Hour is out of range (" + hour + ")");

            double minutes = 0d;
            if (!double.TryParse("0." + timeComponents[1], out minutes))
                throw new ArgumentException("Time component could not be converted");
            minutes = Math.Round(minutes * 60d, 0, MidpointRounding.ToEven);
            if (minutes > 60d)
                throw new ArgumentException("Minute is out of range (" + minutes + ")");
            if((int)minutes == 60)
            {
                minutes = 0d;
                hour += 1;
            }
            if (hour == 24)
                hour = 0;

            var finalTime = new DateTime(resultTime.Year, resultTime.Month, resultTime.Day, hour, (int)minutes, 0, 0, DateTimeKind.Local);

            return finalTime;
        }
    }
}
