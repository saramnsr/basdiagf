using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class HoraireTr
    {
       
        private DateTime _endtime;
        public DateTime endtime
        {
            get
            {
                return _endtime;
            }
            set
            {
                _endtime = value;
            }
        }

        private DateTime _starttime;
        public DateTime starttime
        {
            get
            {
                return _starttime;
            }
            set
            {
                _starttime = value;
            }
        }

        private int _week_day; //1 : lundi 7 : dimanche
        public int week_day
        {
            get
            {
                return _week_day;
            }
            set
            {
                _week_day = value;
            }
        }

        private int _id_utilisateur;
        public int id_utilisateur
        {
            get
            {
                return _id_utilisateur;
            }
            set
            {
                _id_utilisateur = value;
            }
        }
    }
}
