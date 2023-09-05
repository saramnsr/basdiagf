using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BL;
using BasCommon_BO;
using BasCommon_DAL;
using System.Data;
using Newtonsoft.Json.Linq;
namespace BasCommon_BL
{
    public static class PresenceMgt
    {

        public enum TierTemps
        {
            matin = 1,
            apresmidi = 2,
            journee = 4,
            Current = 8

        }

        private static List<JourFerie> _JourFerie;
        public static List<JourFerie> JourFerie
        {
            get
            {
                if (_JourFerie == null)
                    _JourFerie = getJoursF();

                return _JourFerie;
            }

        }

        private static List<JourFerie> getJourFeries()
        {
            _JourFerie = getJoursF();
            return _JourFerie;
        }

        public static List<JourFerie> getJoursF() {

            List<JourFerie> liste = new List<JourFerie>();
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray("/getAllJourFerie");

            foreach (JObject obj in jArray)
                liste.Add(Builders.BuildPresence.BuildJourFerieFromJObject(obj));
            return liste;
        }

        public static List<JourFerie> getJoursFOld()
        {
            List<JourFerie> lst = new List<JourFerie>();
            DataTable dt = DAC.getJourFeries();

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildPresence.BuildJourFerie(r));
            }

            return lst;
        }

        public static void DelAffectation(Utilisateur user, DateTime startdate, DateTime enddate)
        {
            DAC.DelAffectation(user, startdate, enddate);
        }

        public static bool IsFauteuilAffected(Utilisateur user, DateTime startdate, DateTime enddate)
        {
            return DAC.IsFauteuilAffected(user,  startdate,  enddate);
        }

        public static void RemplacerJoursFerie(List<JourFerie> jfs)
        {
            DAC.DeleteJoursFerie();

            foreach (JourFerie jf in jfs)
            {
                DAC.AddJourFerie(jf);
            }

            _JourFerie = jfs;

        }

        public static List<JourFerie> GetCalculatedJoursFerie(int Year)
        {

            List<JourFerie> lstjf = new List<JourFerie>();

            JourFerie jf = new JourFerie();
            jf.Libelle = "1° Janvier";
            jf.Dte = new DateTime(Year, 1, 1);
            lstjf.Add(jf);

            jf = new JourFerie();
            jf.Libelle = "1° Mai";
            jf.Dte = new DateTime(Year, 5, 1);
            lstjf.Add(jf);

            jf = new JourFerie();
            jf.Libelle = "8 Mai";
            jf.Dte = new DateTime(Year, 5, 8);
            lstjf.Add(jf);

            jf = new JourFerie();
            jf.Libelle = "14 Juillet";
            jf.Dte = new DateTime(Year, 7, 14);
            lstjf.Add(jf);

            jf = new JourFerie();
            jf.Libelle = "15 Aout";
            jf.Dte = new DateTime(Year, 8, 15);
            lstjf.Add(jf);

            jf = new JourFerie();
            jf.Libelle = "1 Novembre";
            jf.Dte = new DateTime(Year, 11, 1);
            lstjf.Add(jf);

            jf = new JourFerie();
            jf.Libelle = "11 Novembre";
            jf.Dte = new DateTime(Year, 11, 11);
            lstjf.Add(jf);

            jf = new JourFerie();
            jf.Libelle = "Noël";
            jf.Dte = new DateTime(Year, 12, 25);
            lstjf.Add(jf);




            // Calcul du jour de pâques (algorithme de Oudin (1940))
            //Calcul du nombre d'or - 1
            int intGoldNumber = (int)(Year % 19);
            // Année divisé par cent
            int intAnneeDiv100 = (int)(Year / 100);
            // intEpacte est = 23 - Epacte (modulo 30)
            int intEpacte = (int)((intAnneeDiv100 - intAnneeDiv100 / 4 - (8 * intAnneeDiv100 + 13) / 25 + (
            19 * intGoldNumber) + 15) % 30);
            //Le nombre de jours à partir du 21 mars pour atteindre la pleine lune Pascale
            int intDaysEquinoxeToMoonFull = (int)(intEpacte - (intEpacte / 28) * (1 - (intEpacte / 28) * (29 / (intEpacte + 1)) * ((21 - intGoldNumber) / 11)));
            //Jour de la semaine pour la pleine lune Pascale (0=dimanche)
            int intWeekDayMoonFull = (int)((Year + Year / 4 + intDaysEquinoxeToMoonFull +
                  2 - intAnneeDiv100 + intAnneeDiv100 / 4) % 7);
            // Nombre de jours du 21 mars jusqu'au dimanche de ou 
            // avant la pleine lune Pascale (un nombre entre -6 et 28)
            int intDaysEquinoxeBeforeFullMoon = intDaysEquinoxeToMoonFull - intWeekDayMoonFull;
            // mois de pâques
            int intMonthPaques = (int)(3 + (intDaysEquinoxeBeforeFullMoon + 40) / 44);
            // jour de pâques
            int intDayPaques = (int)(intDaysEquinoxeBeforeFullMoon + 28 - 31 * (intMonthPaques / 4));
            // lundi de pâques
            DateTime dtMondayPaques = DateTime.MinValue;
            try
            {
                dtMondayPaques = new DateTime(Year, intMonthPaques, intDayPaques + 1);

                jf = new JourFerie();
                jf.Libelle = "Pacques";
                jf.Dte = new DateTime(Year, intMonthPaques, intDayPaques + 1);
                lstjf.Add(jf);

                jf = new JourFerie();
                jf.Libelle = "Ascension";
                jf.Dte = dtMondayPaques.AddDays(38);
                lstjf.Add(jf);

                jf = new JourFerie();
                jf.Libelle = "Pentecote";
                jf.Dte = dtMondayPaques.AddDays(49);
                lstjf.Add(jf);
            }
            catch (System.Exception) { }

            return lstjf;
        }  
        
        private static void FormatedDirectFound(List<AffectedUtilisateurs> m_AffectedUsers, DateTime dateStart, DateTime dateEnd, Fauteuil f, out string sh, out string sb)
        {
            sh = "";
            sb = "";

            try
            {

                foreach (AffectedUtilisateurs ut in DirectFound(m_AffectedUsers, dateStart, dateEnd, f))
                {
                    

                        if (ut.type == Utilisateur.typeUtilisateur.Praticien)
                        {
                            sh += sh == "" ? ut.LastNameShort : "\n" + ut.LastNameShort;
                        }
                        else
                        {
                            sb += sb == "" ? ut.NameShort : "\n" + ut.NameShort;
                        }                    
                }
            }
            catch (System.Exception) { }
        }
        
        public static string CheckIfCanBeAffected(AffectationFauteuil af)
        {


            DateTime returnedDate;
            UserStatus stat;

            bool isAffectable = !UtilisateursMgt.IsOnHoliday(af.user, af.DteFrom, af.DteTo, out returnedDate);
            if (!isAffectable) return "En congé jusqu'au " + returnedDate.ToString();

            isAffectable = !UtilisateursMgt.IsOutOfOffice(af.user, af.DteFrom, af.DteTo, out stat);
            if (!isAffectable)
            {
                if ((stat != null) && (stat.status.IsAnAbsence))
                    return "Ne travail pas !" + stat.status.Libelle;
                else
                    return "Ne travail pas";
            }

            isAffectable = !(UtilisateursMgt.IsOnWorkTime(af.user, af.DteFrom, af.DteTo, true) == 0);
            if (!isAffectable) return "Hors horaire de travail ";


            if (isAffectable)
            {
                return "";
            }
            else
                return "Ne travail pas";



        }
        
            
        public static string CheckIfCanBeAffected(Utilisateur ut, DateTime dateStart, DateTime dateEnd, Fauteuil f)
        {


            DateTime returnedDate;
            UserStatus stat;

            bool isAffectable = !UtilisateursMgt.IsOnHoliday(ut, dateStart, dateEnd, out returnedDate);
            if (!isAffectable) return "En congé jusqu'au " + returnedDate.ToString();

            isAffectable = !UtilisateursMgt.IsOutOfOffice(ut, dateStart, dateEnd, out stat);
            if (!isAffectable)
            {
                if ((stat != null) && (stat.status.IsAnAbsence))
                    return "Ne travail pas !" + stat.status.Libelle;
                else
                    return "Ne travail pas";
            }

            isAffectable = !(UtilisateursMgt.IsOnWorkTime(ut, dateStart, dateEnd, true) == 0);
            if (!isAffectable) return "Hors horaire de travail ";


            if (isAffectable)
            {
                return "";
            }
            else
                return "Ne travail pas";



        }
        

        public static List<AffectedUtilisateurs> DirectFound(List<AffectedUtilisateurs> m_AffectedUsers, DateTime dateStart, DateTime dateEnd, Fauteuil f)
        {
            
            List<AffectedUtilisateurs> lst = new List<AffectedUtilisateurs>();

            try
            {

                foreach (AffectedUtilisateurs ut in m_AffectedUsers)
                {
                    DateTime returnedDate;
                    UserStatus stat;

                    bool isAffectable = !UtilisateursMgt.IsOnHoliday(ut, dateStart, dateEnd, out returnedDate);
                    isAffectable &= !UtilisateursMgt.IsOutOfOffice(ut, dateStart, dateEnd, out stat);
                    isAffectable &= !(UtilisateursMgt.IsOnWorkTime(ut, dateStart, dateEnd, true) == 0);


                    if ((stat!=null) && (!stat.status.IsAnAbsence)) isAffectable = true;

                    if ((dateStart.Date == ut.date.Date) && (f.Id == ut.fauteuil.Id) && isAffectable)
                    {

                        lst.Add(ut);
                    }
                }
            }
            catch (System.Exception) { }

            return lst;
        }
                
        public static void getAffectationFaut(List<AffectedUtilisateurs> m_AffectedUsers, DateTime dateStart, DateTime dateEnd, Fauteuil f, out string sh, out string sb)
        {
            sh = "";
            sb = "";

            FormatedDirectFound(m_AffectedUsers, dateStart, dateEnd, f, out sh, out sb);

            
            
        }
                
        public static void getAffectationFaut(List<BasCommon_BO.AffectationFauteuil> affectation, DateTime dte, TierTemps ttps, BasCommon_BO.Fauteuil f, out string sh, out string sb)
        {
            int idxh = 0;
            int idxb = 0;
            string[] h = new string[2];
            string[] b = new string[2];



            foreach (BasCommon_BO.AffectationFauteuil af in affectation)
            {
                if ((af.fauteuil == f) &&
                    ((((ttps | TierTemps.journee) == TierTemps.journee) && (dte.Date == af.DteFrom.Date)) ||
                    ((((ttps | TierTemps.Current) == TierTemps.Current) && (af.DteFrom < DateTime.Now) && (af.DteTo > DateTime.Now)) ||
                    (((ttps | TierTemps.matin) == TierTemps.matin) && (af.DteFrom.Hour < 12) && (dte.Date == af.DteFrom.Date)) ||
                    (((ttps | TierTemps.apresmidi) == TierTemps.apresmidi) && (af.DteTo.Hour > 14) && (dte.Date == af.DteFrom.Date))
                    )))
                {
                    if (af.user.type == BasCommon_BO.Utilisateur.typeUtilisateur.Praticien)
                    {
                        if (idxh > 1)
                        {
                        }
                        else
                        {
                            h[idxh] = af.user.LastNameShort;
                            idxh++;
                        }
                    }
                    else
                    {
                        if (idxb > 1)
                        {
                            if (idxh < 2)
                            {
                                h[idxh] = af.user.LastNameShort;
                                idxh++;
                            }
                        }
                        else
                        {
                            b[idxb] = af.user.LastNameShort;
                            idxb++;
                        }
                    }
                }
            }
            sb = b[0] + "\n" + b[1];
            sh = h[0] + "\n" + h[1];


        }

        public static List<AffectationFauteuil> getAffectationFauteuils(DateTime dteFrom, DateTime dteTo, Utilisateur user)
        {
            DataTable dt = DAC.getAffectationFauteuil(dteFrom, dteTo, user);

            List<AffectationFauteuil> lst = new List<AffectationFauteuil>();
            foreach (DataRow r in dt.Rows)
            {
                AffectationFauteuil u = Builders.BuildPresence.BuildAffectationFauteuil(r);
                if (u != null) lst.Add(u);
            }
            return lst;

        }



        public static List<AffectationFauteuil> getAffectationFauteuils()
        {

            JArray json = DAC.getMethodeJsonArray("/Affectations");
            List<AffectationFauteuil> lst = new List<AffectationFauteuil>();
            foreach (JObject j in json)
            {
                AffectationFauteuil u = Builders.BuildPresence.BuildAffectationFauteuilJ(j);
                if (u != null) lst.Add(u);
            }
            return lst;
        }
       
        public static List<AffectationFauteuil> getAffectationFauteuilsOLD()
        {
            DataTable dt = DAC.getAffectationFauteuil();

            List<AffectationFauteuil> lst = new List<AffectationFauteuil>();
            foreach (DataRow r in dt.Rows)
            {
                AffectationFauteuil u = Builders.BuildPresence.BuildAffectationFauteuil(r);
                if (u != null) lst.Add(u);
            }
            return lst;

        }

        public static void Delete(AffectationFauteuil af)
        {
            DAC.DelAffectation(af);
        }

        public static List<AffectationFauteuil> getAffectationFauteuils(DateTime dte)
        {
            DataTable dt = DAC.getAffectationFauteuil(dte);

            List<AffectationFauteuil> lst = new List<AffectationFauteuil>();
            foreach (DataRow r in dt.Rows)
            {
                AffectationFauteuil u = Builders.BuildPresence.BuildAffectationFauteuil(r);
                if (u != null) lst.Add(u);
            }
            return lst;

        }
        public static List<AffectationFauteuil> getAffectationFauteuils(DateTime dteFrom, DateTime dteTo)
        {

            JArray json = DAC.getMethodeJsonArray("/AffectationBetween/" + dteFrom.ToString("yyyy-MM-dd HH:mm:ss") + "&" + dteTo.ToString("yyyy-MM-dd HH:mm:ss"));
            List<AffectationFauteuil> lst = new List<AffectationFauteuil>();
            foreach (JObject j in json)
            {
                AffectationFauteuil u = Builders.BuildPresence.BuildAffectationFauteuilJ(j);
                if (u != null) lst.Add(u);
            }
            return lst;

        }
        public static List<AffectationFauteuil> getAffectationFauteuilsOLD(DateTime dteFrom, DateTime dteTo)
        {
            DataTable dt = DAC.getAffectationFauteuil(dteFrom, dteTo);

            List<AffectationFauteuil> lst = new List<AffectationFauteuil>();
            foreach (DataRow r in dt.Rows)
            {
                AffectationFauteuil u = Builders.BuildPresence.BuildAffectationFauteuil(r);
                if (u != null) lst.Add(u);
            }
            return lst;

        }
        //public static List<AffectationFauteuil> getAffectationFauteuilById(int id, DateTime dteFrom, DateTime dteTo)
        //{
        //    DataTable dt = DAC.getAffectationFauteuilById(id,dteFrom, dteTo);

        //    List<AffectationFauteuil> lst = new List<AffectationFauteuil>();
        //    foreach (DataRow r in dt.Rows)
        //    {
        //        AffectationFauteuil u = Builders.BuildPresence.BuildAffectationFauteuil(r);
        //        if (u != null) lst.Add(u);
        //    }
        //    return lst;

        //}
        public static List<AffectationFauteuil> getAffectationFauteuilById(int id, DateTime dteFrom, DateTime dteTo)
        {
             List<AffectationFauteuil> lst = new List<AffectationFauteuil>();
            foreach(AffectationFauteuil af in UtilisateursMgt.AffectationFauteuilCached)
            {
                if (dteFrom < af.DteFrom && dteTo > af.DteTo && af.fauteuil.Id == id)
                    lst.Add(af);

            }
            //DataTable dt = DAC.getAffectationFauteuilById(id, dteFrom, dteTo);

           
            //foreach (DataRow r in dt.Rows)
            //{
            //    AffectationFauteuil u = Builders.BuildPresence.BuildAffectationFauteuil(r);
            //    if (u != null) lst.Add(u);
            //}
            return lst;

        }
        public static void InsertAffectationFauteuils(AffectationFauteuil af)
        {
            if (af.Id == -1)
                DAC.InsertAffectationFauteuils(af);

        }

        public static void UpdateAffectationFauteuils(AffectationFauteuil af)
        {
            if (af.Id != -1)
                DAC.UpdateAffectationFauteuils(af);

        }

    }
}
