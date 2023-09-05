using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;


namespace BasCommon_BL.Builders
{
    public static class BuildHoraire
    {
        public static HoraireReel BuildHoraireReelFromPointeuse(DataRow r)
        {

            string Nom = Convert.ToString(r["Nom"]);
            string Prenom = Convert.ToString(r["PRENOM"]);
            DateTime Date = Convert.ToDateTime(r["DATE_PT"]);
            TimeSpan Entree = ((TimeSpan)r["ENTREE"]);
            TimeSpan Sortie = ((TimeSpan)r["SORTIE"]);

            HoraireReel hr = new HoraireReel();
            hr.id_utilisateur = UtilisateursMgt.getUtilisateur(Nom).Id;
            hr.Year = Date.Year;
            hr.WeekNum = UtilisateursMgt.GetYearWeek(Date);
            hr.starttime = DateTime.MinValue.Add(Entree);
            hr.endtime = DateTime.MinValue.Add(Sortie);
            hr.week_day = (int)Date.DayOfWeek;

            return hr;
        }

       

        

        public static Holiday BuildHoliday(DataRow r)
        {
            Holiday Hd = new Holiday();

            Hd.Id = Convert.ToInt32(r["ID"]);
            Hd.personne = UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["ID_PERSONNE"]));

            Hd.startdate = Convert.ToDateTime(r["startdate"]);
            Hd.enddate = Convert.ToDateTime(r["enddate"]);

            Hd.holiday_name = Convert.ToString(r["holiday_name"]).Trim();



            return Hd;
        }

        public static HoraireReel BuildHoraireReel(DataRow r)
        {
            
            HoraireReel hr = new HoraireReel();

            hr.id_utilisateur = Convert.ToInt32(r["id_utilisateur"]);
            hr.id = Convert.ToInt32(r["id"]);
            hr.Year = Convert.ToInt32(r["annee"]);
            hr.WeekNum = Convert.ToInt32(r["weeknum"]);
            hr.starttime = DateTime.MinValue.AddMinutes((((TimeSpan)r["starttime"]).TotalMinutes));
            hr.endtime = DateTime.MinValue.AddMinutes((((TimeSpan)r["endtime"]).TotalMinutes));
            hr.week_day = Convert.ToInt32(r["daynum"]);

            return hr;
        }

        

        public static HoraireReel buildHorraireReelFromJObject(JObject obj) {
            
            HoraireReel hr = new HoraireReel();

            hr.id_utilisateur = Convert.ToInt32(obj["idUtilisateur"]);
            hr.id = Convert.ToInt32(obj["id"]);
            hr.Year = Convert.ToInt32(obj["annee"]);
            hr.WeekNum = Convert.ToInt32(obj["weekNum"]);
            hr.week_day = Convert.ToInt32(obj["dayNum"]);
            hr.starttime = DateTime.MinValue.AddMinutes((((TimeSpan)obj["startTime"]).TotalMinutes));
            hr.endtime = DateTime.MinValue.AddMinutes((((TimeSpan)obj["endTime"]).TotalMinutes));
            
            //if (obj["startTime"] != null && obj["startTime"].ToString() != "")
            //{
            //    DateTime date1 = (DateTime)Convert.ToDateTime(obj["startTime"]);
            //    hr.starttime = DateTime.MinValue.AddMinutes(date1.TimeOfDay.TotalMinutes);

            //}

            //if (obj["endTime"] != null && obj["endTime"].ToString() != "")
            //{
            //    DateTime date2 = (DateTime)Convert.ToDateTime(obj["endTime"]);
            //    hr.endtime = DateTime.MinValue.AddMinutes(date2.TimeOfDay.TotalMinutes);
            //}
            

            return hr;
        }

        public static HorairesDeTravail BuildHoraires(JObject obj)
        {
            // valide
            HorairesDeTravail Ht = new HorairesDeTravail();
            Ht.id_utilisateur = Convert.ToInt32(obj["idUser"]);
            Ht.week_day = Convert.ToInt32(obj["weekDay"]);
            Ht.starttime = DateTime.MinValue.Add((TimeSpan)obj["startTime"]);
            Ht.endtime = DateTime.MinValue.Add((TimeSpan)obj["endTime"]);
            if (!(obj["dayNum"] == null))
            {
                Ht.period.MonthPeriodicityDay = obj["dayNum"] == null ? 0 : (DayOfWeek)Convert.ToInt16(obj["dayNum"]);
                Ht.period.MonthPeriodicityNum = obj["periodicity"] == null? 0 : Convert.ToInt16(obj["periodicity"]);

                Ht.period.tpeperiod = HoraireTrPeriodicity.TypePeriodicity.JourParMois;
            }

            if ((obj["firstDate"] != null) && obj["firstDate"].ToString() != "")
            {
                Ht.period.FirstDate = obj["firstDate"] == null ? DateTime.MinValue : Convert.ToDateTime(obj["firstDate"]);
                Ht.period.MonthPeriodicityNum = obj["periodicity"] == null ? 0 : Convert.ToInt16(obj["periodicity"]);

                Ht.period.tpeperiod = HoraireTrPeriodicity.TypePeriodicity.XSemainesSur;
            }
            return Ht;

        }

        public static HorairesDeTravail BuildHoraires(DataRow r)
        {
            HorairesDeTravail Ht = new HorairesDeTravail();
            Ht.id_utilisateur = Convert.ToInt32(r["id_utilisateur"]);
            Ht.week_day = Convert.ToInt32(r["week_day"]);

            Ht.starttime = DateTime.MinValue.Add((TimeSpan)r["starttime"]);
            Ht.endtime = DateTime.MinValue.Add((TimeSpan)r["endtime"]);

            if (!(r["daynum"] is DBNull))
            {
                Ht.period.MonthPeriodicityDay = r["daynum"] is DBNull ? 0 : (DayOfWeek)Convert.ToInt16(r["daynum"]);
                Ht.period.MonthPeriodicityNum = r["PERIODICITY"] is DBNull ? 0 : Convert.ToInt16(r["PERIODICITY"]);

                Ht.period.tpeperiod = HoraireTrPeriodicity.TypePeriodicity.JourParMois;
            }

            if (!(r["FirstDate"] is DBNull))
            {
                Ht.period.FirstDate = r["FirstDate"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(r["FirstDate"]);
                Ht.period.MonthPeriodicityNum = r["PERIODICITY"] is DBNull ? 0 : Convert.ToInt16(r["PERIODICITY"]);

                Ht.period.tpeperiod = HoraireTrPeriodicity.TypePeriodicity.XSemainesSur;
            }

            return Ht;
        }

        
    }
}
