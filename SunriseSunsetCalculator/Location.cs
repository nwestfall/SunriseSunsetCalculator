using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunriseSunsetCalculator
{
    /// <summary>
    /// Simple class to store latitude/longitude information
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Get the latitude
        /// </summary>
        public double latitude
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the longitude
        /// </summary>
        public double longitude
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Location"/> with the given parameters
        /// </summary>
        /// <param name="latitude">The latitude, in degrees, of this location, North latitude is positive, south negative</param>
        /// <param name="longitude">The longitude, in degrees, of this location.  East longitude is positive, west negative</param>
        public Location(string latitude, string longitude)
        {
            this.SetLocation(latitude, longitude);
        }

        /// <summary>
        /// Creates a new instance of <see cref="Location"/> with the given parameters
        /// </summary>
        /// <param name="latitude">The latitude, in degrees, of this location, North latitude is positive, south negative</param>
        /// <param name="longitude">The longitude, in degrees, of this location.  East longitude is positive, west negative</param>
        public Location(double latitude, double longitude)
        {
            this.SetLocation(latitude, longitude);
        }

        /// <summary>
        /// Sets the coordinates of the <see cref="Location"/> object
        /// </summary>
        /// <param name="latitude">The latitude, in degrees, of this location, North latitude is positive, south negative</param>
        /// <param name="longitude">The longitude, in degrees, of this location.  East longitude is positive, west negative</param>
        public void SetLocation(string latitude, string longitude)
        {
            double lat, lng;
            if (double.TryParse(latitude, out lat))
                this.latitude = lat;
            else
                throw new ArgumentException("Latitude is not a valid 'double' entry");
            if (double.TryParse(longitude, out lng))
                this.longitude = lng;
            else
                throw new ArgumentException("Longitude is not a valid 'double' entry");
        }

        /// <summary>
        /// Sets the coordinates of the <see cref="Location"/> object
        /// </summary>
        /// <param name="latitude">The latitude, in degrees, of this location, North latitude is positive, south negative</param>
        /// <param name="longitude">The longitude, in degrees, of this location.  East longitude is positive, west negative</param>
        public void SetLocation(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }
    }
}
