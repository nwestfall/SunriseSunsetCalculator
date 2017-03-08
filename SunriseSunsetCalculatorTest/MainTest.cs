using System;
using NUnit.Framework;

using SunriseSunsetCalculator;

namespace SunriseSunsetCalculatorTest
{
    [TestFixture]
    public class MainTest
    {
        //Albany NY location
        double lat = 42.6525790d;
        double lng = -73.7562320d;
        string latStr = "42.6525790";
        string lngStr = "-73.7562320";

        [Test]
        public void TestZenithDegrees()
        {
            Assert.AreEqual(Zenith.ASTRONOMICAL.degrees, 108);
            Assert.AreEqual(Zenith.NAUTICAL.degrees, 102);
            Assert.AreEqual(Zenith.CIVIL.degrees, 96);
            Assert.AreEqual(Zenith.OFFICIAL.degrees, 90.8333);
        }
        
        [Test]
        public void TestLocation()
        {
            Location loc = new Location(lat, lng);

            Assert.AreEqual(loc.latitude, lat);
            Assert.AreEqual(loc.longitude, lng);

            Location locStr = new Location(latStr, lngStr);

            Assert.AreEqual(loc.latitude, lat);
            Assert.AreEqual(loc.longitude, lng);
        }

        [Test]
        public void TestOfficalSunriseSunset()
        {
            SunriseSunsetCalculator.SunriseSunsetCalculator calc = new SunriseSunsetCalculator.SunriseSunsetCalculator(new Location(lat, lng));
            var sunriseToday = calc.GetOfficialSunriseDateTimeForDate(DateTime.Now);
            var sunsetToday = calc.GetOfficalSunsetDateTimeForDate(DateTime.Now);

            Console.WriteLine("Sunrise: " + sunriseToday);
            Console.WriteLine("Sunset: " + sunsetToday);
        }

        [Test]
        public void TestAstronomicalSunriseSunet()
        {
            SunriseSunsetCalculator.SunriseSunsetCalculator calc = new SunriseSunsetCalculator.SunriseSunsetCalculator(new Location(lat, lng));
            var sunriseToday = calc.GetAstronomicalSunriseDateTimeForDate(DateTime.Now);
            var sunsetToday = calc.GetAstronomicalSunsetDateTimeForDate(DateTime.Now);

            Console.WriteLine("Sunrise: " + sunriseToday);
            Console.WriteLine("Sunset: " + sunsetToday);
        }

        [Test]
        public void TestNauticalSunriseSunet()
        {
            SunriseSunsetCalculator.SunriseSunsetCalculator calc = new SunriseSunsetCalculator.SunriseSunsetCalculator(new Location(lat, lng));
            var sunriseToday = calc.GetNauticalSunriseDateTimeForDate(DateTime.Now);
            var sunsetToday = calc.GetNauticalSunsetDateTimeForDate(DateTime.Now);

            Console.WriteLine("Sunrise: " + sunriseToday);
            Console.WriteLine("Sunset: " + sunsetToday);
        }

        [Test]
        public void TestCivilSunriseSunset()
        {
            SunriseSunsetCalculator.SunriseSunsetCalculator calc = new SunriseSunsetCalculator.SunriseSunsetCalculator(new Location(lat, lng));
            var sunriseToday = calc.GetCivilSunriseDateTimeForDate(DateTime.Now);
            var sunsetToday = calc.GetCivilSunsetDateTimeForDate(DateTime.Now);

            Console.WriteLine("Sunrise: " + sunriseToday);
            Console.WriteLine("Sunset: " + sunsetToday);
        }
    }
}
