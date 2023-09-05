using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Ville
    {
        public override string ToString()
        {
            return _CodePostal + " : " + _ville;
        }

        private int _Id;
        [PropertyCanBeSerialized]
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        private string _CodePostal;
        [PropertyCanBeSerialized]
        public string CodePostal
        {
            get
            {
                return _CodePostal;
            }
            set
            {
                _CodePostal = value;
            }
        }

        private string _ville;
        [PropertyCanBeSerialized]
        public string NomVille
        {
            get
            {
                return _ville;
            }
            set
            {
                _ville = value.ToUpper();
            }
        }

       
        public double? Eloignement
        {
            get
            {
                if (Latitude == null || Longitude == null) return 0;
                return Calculate(Latitude.Value, Longitude.Value, 43.133333f, 5.75f);
            }
           
        }

        public static double Calculate(double sLatitude, double sLongitude, double eLatitude,
                               double eLongitude)
        {
            double e = (3.1415926538 * sLatitude / 180);
            double f = (3.1415926538 * sLongitude / 180);
            double g = (3.1415926538 * eLatitude / 180);
            double h = (3.1415926538 * eLongitude / 180);
            double i = (Math.Cos(e) * Math.Cos(g) * Math.Cos(f) * Math.Cos(h) + Math.Cos(e) * Math.Sin(f) * Math.Cos(g) * Math.Sin(h) + Math.Sin(e) * Math.Sin(g));
            double j = (Math.Acos(i));
            double k = (6371 * j);

            return k;
        }

        private double? _latitude;
        [PropertyCanBeSerialized]
        public double? Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                _latitude = value;
            }
        }

        private double? _longitude;
        [PropertyCanBeSerialized]
        public double? Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                _longitude = value;
            }
        }

    }
}
