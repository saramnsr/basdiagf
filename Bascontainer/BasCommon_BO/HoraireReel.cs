using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{

    public class HoraireReelTimeComparer : IComparer<HoraireReel>
    {

        public int Compare(HoraireReel x, HoraireReel y)
        {
            return x.starttime.TimeOfDay.CompareTo(y.starttime.TimeOfDay);
        }
    }


    public class HoraireReel : HoraireTr
    {

       

        private int _id = -1;
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private int _WeekNum;
        public int WeekNum
        {
            get
            {
                return _WeekNum;
            }
            set
            {
                _WeekNum = value;
            }
        }

        private int _Year;
        public int Year
        {
            get
            {
                return _Year;
            }
            set
            {
                _Year = value;
            }
        }



    }
}
