using System;
using System.Collections.Generic;
using System.Text;

namespace BasCommon_BO
{

    public class HoraireTrPeriodicity
    {

        public enum TypePeriodicity
        {
            JourParMois,
            XSemainesSur,
            XParMois
        }

        public override string ToString()
        {
            if (tpeperiod == TypePeriodicity.JourParMois)
                return MonthPeriodicityNum.ToString() + "° " + MonthPeriodicityDay.ToString() + " du mois";
            else
                if (tpeperiod == TypePeriodicity.XSemainesSur)
                {
                    return "1 semaine sur " + MonthPeriodicityNum.ToString();
                }
                else
                    if (tpeperiod == TypePeriodicity.XSemainesSur)
                    {
                        return MonthPeriodicityNum.ToString() + " par mois ";
                    }
                    else
                        return "";

        }


        public TypePeriodicity tpeperiod;

        public DayOfWeek MonthPeriodicityDay;
        public int MonthPeriodicityNum;
        public DateTime FirstDate;

    }

    public class HoraireTrAppointment : Appointment
    {

        public override string ToString()
        {
            string str = StartDate.DayOfWeek.ToString() + ":" + StartDate.ToString("HH:mm") + " to " + EndDate.ToString("HH:mm");
            if (period != null) str += "("+period.ToString()+")";
            return str;
        }


        private int m_Id;
        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        private basePatient m_patient;
        public basePatient patient { 
            get
            {
                return m_patient;
            }
            set
            {
                m_patient=value;
                Title = m_patient.ToString();
            }
        }

        public HoraireTrPeriodicity period = new HoraireTrPeriodicity();

      
    }
}
