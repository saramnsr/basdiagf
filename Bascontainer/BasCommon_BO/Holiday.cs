using System;
using System.Collections.Generic;
using System.Text;
using BasCommon_BO;

namespace BasCommon_BO
{
    public class Holiday : IComparable
    {

        private int _Id;
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


        public int nbJours
        {
            get
            {
                return (int)(enddate.Date - startdate.Date).TotalDays;
            }
            
        }

        public override string ToString()
        {
            return holiday_name+" du "+startdate.ToString("dd/MM/yyyy")+" au "+enddate.ToString("dd/MM/yyyy");
        }

        public Utilisateur personne;
        public DateTime startdate;
        public DateTime enddate;
        public string holiday_name;


        int IComparable.CompareTo(object obj)
        {
            if (obj is Holiday)
                return (int)(this.startdate - ((Holiday)obj).startdate).TotalDays;
            else
                if (obj is DateTime)
                    return (int)(this.startdate - (DateTime)obj).TotalDays;
                else
                    return 0;

            
 
        }
    }
}
