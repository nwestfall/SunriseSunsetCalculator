using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SunriseSunsetCalculator;

namespace SunriseSunsetCalculatorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Latitude: ");
            var latStr = Console.ReadLine();

            if (string.IsNullOrEmpty(latStr))
                latStr = "42.6525790";

            Console.Write("Enter Longitude: ");
            var lngStr = Console.ReadLine();

            if (string.IsNullOrEmpty(lngStr))
                lngStr = "-73.7562320";

            Console.WriteLine("Calculating...");
            Location loc = new Location(latStr, lngStr);
            SunriseSunsetCalculator.SunriseSunsetCalculator calc = new SunriseSunsetCalculator.SunriseSunsetCalculator(loc);

            Console.WriteLine("Astronomical Sunrise: " + calc.GetAstronomicalSunriseDateTimeForDate(DateTime.Now));
            Console.WriteLine("Astronomical Sunset: " + calc.GetAstronomicalSunsetDateTimeForDate(DateTime.Now));
            Console.WriteLine("Nautical Sunrise: " + calc.GetNauticalSunriseDateTimeForDate(DateTime.Now));
            Console.WriteLine("Nautical Sunset: " + calc.GetNauticalSunsetDateTimeForDate(DateTime.Now));
            Console.WriteLine("Civil Sunrise: " + calc.GetCivilSunriseDateTimeForDate(DateTime.Now));
            Console.WriteLine("Civil Sunset: " + calc.GetCivilSunsetDateTimeForDate(DateTime.Now));
            Console.WriteLine("Official Sunrise: " + calc.GetOfficialSunriseDateTimeForDate(DateTime.Now));
            Console.WriteLine("Official Sunset: " + calc.GetOfficalSunsetDateTimeForDate(DateTime.Now));

            Console.ReadKey(true);
        }
    }
}
