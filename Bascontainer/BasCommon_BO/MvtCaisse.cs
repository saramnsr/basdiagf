using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class MvtCaisse
    {

        private double _Mvt;
        public double Mvt
        {
            get
            {
                return _Mvt;
            }
            set
            {
                _Mvt = value;
            }
        }

        private DateTime _Date;
        public DateTime Date
        {
            get
            {
                return _Date;
            }
            set
            {
                _Date = value;
            }
        }

    }
}
