using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.Data;
using System.Windows.Forms;
using BasCommon_DAL;
using System.Globalization;
using Newtonsoft.Json.Linq;


namespace BasCommon_BL
{
    public static class MgmtHoraire
    {
        public static List<HoraireReel> GetHorairesPointeuse(Utilisateur p_utilisateur)
        {
            List<HoraireReel> lst = new List<HoraireReel>();
            DataTable dt = DAC.getHorairesPointeuse(p_utilisateur);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildHoraire.BuildHoraireReelFromPointeuse(r));
            }

            return lst;
        }

        public static DateTime GetStartDayWeek(int ww, int yyyy,int d)
        {         

                DateTime start = new DateTime(yyyy, 1, 1);
                int delta = DayOfWeek.Monday - start.DayOfWeek;
                delta = delta > 0 ? delta - 7 : delta;
                DateTime firstdayweek = start.AddDays(delta);

                if ((delta > -4) && (delta<1)) ww -= 1;

                return start.AddDays(7*ww+d + delta);
            
        }
       
        public static void GetHorairesReel(Utilisateur p_utilisateur) 
        {
            List<HoraireReel> liste = new List<HoraireReel>();
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray("/HorraireReel/"+p_utilisateur.Id);

            foreach (JObject obj in jArray) {
                HoraireReel hr = Builders.BuildHoraire.buildHorraireReelFromJObject(obj);
                liste.Add(hr);
            }
            p_utilisateur.horairesreels = liste;
        }

        public static void GetHorairesReelOld(Utilisateur p_utilisateur)
        {
            List<HoraireReel> lst = new List<HoraireReel>();
            DataTable dt = DAC.getHorairesReel(p_utilisateur);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildHoraire.BuildHoraireReel(r));
            }

            p_utilisateur.horairesreels = lst;
        }

        public static List<HoraireReel> GetHorairesReel(Utilisateur p_utilisateur, int WeekNum)
        {
            List <HoraireReel>liste = new List<HoraireReel>();
            string method = "/getHorraireReelByUserIdAndWeekNumber/"+p_utilisateur.Id+"&"+WeekNum;
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(method);

            foreach (JObject obj in jArray)
                liste.Add(Builders.BuildHoraire.buildHorraireReelFromJObject(obj));


            return liste;       
        }

        public static List<HoraireReel> GetHorairesReelOld(Utilisateur p_utilisateur,int WeekNum)
        {
            List<HoraireReel> lst = new List<HoraireReel>();
            DataTable dt = DAC.getHorairesReel(p_utilisateur, WeekNum);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildHoraire.BuildHoraireReel(r));
            }

            return lst;
        }


        public static void GetHorairesDeTravail(Utilisateur p_utilisateur)         
        {
            List<HorairesDeTravail> liste = new List<HorairesDeTravail>();
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray("/GetHorairesDeTravail/"+p_utilisateur.Id);

            foreach (JObject obj in jArray)
                liste.Add(Builders.BuildHoraire.BuildHoraires(obj));

            p_utilisateur.horairesDeTravail = liste; 
        
        }

        public static void GetHorairesDeTravailOld(Utilisateur p_utilisateur)
        {
            // old Method
            List<HorairesDeTravail> lst = new List<HorairesDeTravail>();
            DataTable dt = DAC.getHorairesDeTravail(p_utilisateur);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildHoraire.BuildHoraires(r));
            }
            p_utilisateur.horairesDeTravail = lst;
        }

        public static void SaveHorairesDeTravail(Utilisateur p_utilisateur)
        {
            DAC.SaveHorairesDeTravail(p_utilisateur);
        }
      
        public static void AddHorairesReel(HoraireReel hr)
        {
            DAC.AddHorairesReel(hr);
        }
        public static void DeleteHorairesReelPlanning(int dayWeek, int weekNum, int year, int id)
        {
            DAC.DeleteHorairePlanning(dayWeek,weekNum,year,id);
        }
        public static void UpdateHorairesReel(HoraireReel hr)
        {
            DAC.UpdateHorairesReel(hr);
        }

        public static void DeleteHoraire(HoraireReel hr)
        {
            DAC.DeleteHoraire(hr);
        }

        public static void DeleteHorairesReel(Utilisateur user,DateTime dteStart,DateTime dteEnd)
        {
            

            int StartYear = dteStart.Year;
            int Startweeknum = UtilisateursMgt.GetYearWeek(dteStart);
            int Startdaynum = (int)dteStart.DayOfWeek;
            DateTime dteStartTime = dteStart;
            int EndYear = dteEnd.Year;
             int Endweeknum = UtilisateursMgt.GetYearWeek(dteEnd);
            int Enddaynum = (int)dteEnd.DayOfWeek;
            DateTime dteEndTime = dteEnd;

            DAC.DeleteHorairesReel(StartYear,Startweeknum,Startdaynum,dteStartTime,
                                    EndYear,Endweeknum,Enddaynum,dteEndTime,user);


            
                for (int i = user.horairesreels.Count-1; i >= 0; i--)
                {
                    HoraireReel hr = user.horairesreels[i];
                    if ((hr.Year >= StartYear && hr.WeekNum >= Startweeknum && hr.starttime.TimeOfDay >= dteStartTime.TimeOfDay && hr.week_day >= Startdaynum) &&
                        (hr.Year <= EndYear && hr.WeekNum <= Endweeknum && hr.endtime.TimeOfDay <= dteEndTime.TimeOfDay && hr.week_day <= Enddaynum))
                    {
                        user.horairesreels.Remove(hr);
                    }
                }

        }

        

        public static List<Holiday> GetHolidays(Utilisateur p_utilisateur)
        {
            List<Holiday> lst = new List<Holiday>();
            DataTable dt = DAC.getHolidays(p_utilisateur);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildHoraire.BuildHoliday(r));
            }
            p_utilisateur.Holidays = lst;


            return lst;
        }

        public static void UpdateHolidays(Holiday holiday)
        {
            DAC.updateHoliday(holiday);
        }

        public static void AddHolidays(Utilisateur p_utilisateur, Holiday holiday)
        {
            DAC.AddHoliday(p_utilisateur, holiday);
        }

        public static void DeleteHoliday(Utilisateur ut, Holiday holiday)
        {
            ut.Holidays.Remove(holiday);
            DAC.DelHoliday(holiday);
        }
    }
}
