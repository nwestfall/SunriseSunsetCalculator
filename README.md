# SunriseSunsetCalculator
[![Build Status](https://travis-ci.org/nwestfall/SunriseSunsetCalculator.svg?branch=master)](https://travis-ci.org/nwestfall/SunriseSunsetCalculator)

A port of https://github.com/mikereedell/sunrisesunsetlib-java for .NET PCL written in C#

## Dependencies
None

## Installation
Available on [nuget](https://www.nuget.org/packages/com.fistbumpstudios.sunrisesunsetcalculator/)
```
Install-Package com.fistbumpstudios.sunrisesunsetcalculator
```

## Usage
Create an instance with a location (currently only supports local timezone due to PCL restriction of TimeZoneInfo)
```
  Location location = new Location(42.6525790, -73.7562320)
  SunriseSunsetCalculator.SunriseSunsetCalculator calculator = new SunriseSunsetCalculator.SunriseSunsetCalculator(location);
```
Start calling methods
```
  string astronomicalSunrise = calc.GetAstronomicalSunriseForDate(DateTime.Now);
  DateTime? astronomicalSunriseDate = calc.GetAstronomicalSunriseDateTimeForDate(DateTime.Now);
  string astronomicalSunset = calc.GetAstronomicalSunsetForDate(DateTime.Now);
  DateTime? astronomicalSunsetDate = calc.GetAstronomicalSunsetDateTimeForDate(DateTime.Now);
  string nauticalSunrise = calc.GetNauticalSunriseForDate(DateTime.Now);
  DateTime? nauticalSunriseDate = calc.GetNauticalSunriseDateTimeForDate(DateTime.Now);
  string nauticalSunset = calc.GetNauticalSunsetForDate(DateTime.Now);
  DateTime? nauticalSunsetDate = calc.GetNauticalSunsetDateTimeForDate(DateTime.Now);
  string civilSunrise = calc.GetCivilSunriseForDate(DateTime.Now);
  DateTime> civilSunriseDat = calc.GetCivilSunriseDateTimeForDate(DateTime.Now);
  string civilSunset = calc.GetCivilSunsetForDate(DateTime.Now);
  DateTime> civilSunsetDate = calc.GetCivilSunsetDateTimeForDate(DateTime.Now);
  string officialSunrise = calc.GetOfficialSunriseForDate(DateTime.Now);
  DateTime? officialSunriseDate = calc.GetOfficialSunriseDateTimeForDate(DateTime.Now);
  string officalSunset = calc.GetOfficialSunsetForDate(DateTime.Now);
  DateTime? officialSunsetDate = calc.GetOfficialSunsetDateTimeForDate(DateTime.Now);
  DateTime? sunrise = calc.GetSunrise(42.6525790, -73.7562320, DateTime.Now, 90);
  DateTime? sunset = calc.GetSunset(43.6525790, -73.7562320, DateTime.Now, 90);
```
## Author
Nathan Westfall

## License
Apache License, Version 2.0 http://www.apache.org/licenses/LICENSE-2.0
