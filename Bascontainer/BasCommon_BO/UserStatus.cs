using System;
using System.Collections.Generic;
using System.Text;

namespace BasCommon_BO
{
    public class UserStatus
    {

       

        public string ShortDisplay
        {
            get
            {
                if (dateEnd.Hour < 13)
                {
                    //Absent AM uniquement
                    return  status.Libelle + " AM";

                }
                if (dateStart.Hour >= 13)
                {
                    //Absent PM uniquement
                    return status.Libelle + " PM";

                }

                return status.Libelle;
            }
        }

        public override string ToString()
        {
            if (dateStart.Date == dateEnd.Date)
            {
                if (status.IsAnAbsence)
                    return "Absent le " + dateStart.ToString("dddd dd MMMM yyyy") + " (" + status.Libelle + ")";
                else
                    return "Présent le " + dateStart.ToString("dddd dd MMMM yyyy") + " (" + status.Libelle + ")";
            }
            else
            {
                if (status.IsAnAbsence)
                    return "Absent du " + dateStart.ToString("dddd dd MMMM yyyy") + " au "+dateEnd.ToString("dddd dd MMMM yyyy") +" (" + status.Libelle + ")";
                else
                    return "Présent du " + dateStart.ToString("dddd dd MMMM yyyy") + " au " + dateEnd.ToString("dddd dd MMMM yyyy") + " (" + status.Libelle + ")";

            }
        }
        public int Id;
        [PropertyCanBeSerialized]
        public Status status{get;set;}
        [PropertyCanBeSerialized]
        public Utilisateur utilisateur { get; set; }
        [PropertyCanBeSerialized]
        public DateTime dateStart { get; set; }
        [PropertyCanBeSerialized]
        public DateTime dateEnd { get; set; }
    }
}
