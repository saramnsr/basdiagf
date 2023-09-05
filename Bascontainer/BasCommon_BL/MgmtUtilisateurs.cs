using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using BasCommon_BO;
using BasCommon_DAL;
using System.Diagnostics;
using System.Data;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;



namespace BasCommon_BL
{




    public static class UtilisateursMgt
    {

        private static string _RegistryKey = "Software\\BASE\\BASEPractice";
        private static string _RegistryKeyPref = _RegistryKey + "\\Preferences";
        private static string _CurrentUserRegistryKey = _RegistryKeyPref + "\\CurrentUser";
        private static string _CurrentCabRegistryKey = _RegistryKeyPref + "\\CurrentCab";
        

        public static event EventHandler OnCurrentUtilisateurChange;

        private static DateTime _ValidityDate = DateTime.MinValue;
        private static AccessObject _Currentutilisateur;
        public static AccessObject CurrentUtilisateur
        {
            get
            {

                GetCurrentUserOnRegistry();
                if ((DateTime.Now > _ValidityDate) && (_Currentutilisateur != null))
                {
                    _Currentutilisateur = null;
                    if (OnCurrentUtilisateurChange != null)
                        OnCurrentUtilisateurChange(null, new EventArgs());
                }
                else
                    _ValidityDate = DateTime.Now.Date.AddHours(23).AddMinutes(58);
                return _Currentutilisateur;
            }
            set
            {
                if (value != null)
                {
                    _ValidityDate = DateTime.Now.Date.AddHours(23).AddMinutes(58);
                    _Currentutilisateur = value;
                    SetCurrentUserOnRegistry();
                }
                else
                {
                    _ValidityDate = DateTime.Now.Date.AddHours(-1);
                    _Currentutilisateur = null;
                    SetCurrentUserOnRegistry();
                }


                if (OnCurrentUtilisateurChange != null)
                    OnCurrentUtilisateurChange(null, new EventArgs());
            }
        }


        public enum StatusPointage
        {
            AbsenceNormal,
            AbsenceANormal,
            PresenceNormal,
            PresenceHeureSupp,
            Inconnue
        }

        private static List<AffectationFauteuil> _AffectationFauteuilCached;
        public static List<AffectationFauteuil> AffectationFauteuilCached
        {
            get
            {
                if (_AffectationFauteuilCached == null) _AffectationFauteuilCached = PresenceMgt.getAffectationFauteuils();
                return _AffectationFauteuilCached;
            }
        }

        private static List<AffectationFauteuil> _AffectationFauteuilOfTheDayCached;
        private static List<AffectationFauteuil> AffectationFauteuilOfTheDayCached
        {
            get
            {
                if (_AffectationFauteuilOfTheDayCached == null) _AffectationFauteuilOfTheDayCached = PresenceMgt.getAffectationFauteuils(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
                return _AffectationFauteuilOfTheDayCached;
            }
        }


        private static string _prefix ;
        public static string prefix
        {
            get
            {
                GetCurrentCabOnRegistry();
                return _prefix;
            }
            set
            {
                _prefix = value;
            }


        }
        private static List<Utilisateur> _utilisateurs = null;
        public static List<Utilisateur> utilisateurs
        {
            get
            {
                if (_utilisateurs == null)
                    _utilisateurs = getUtilisateurs();

                return _utilisateurs;
            }


        }

        public static List<Utilisateur> Assistantes
        {
            get
            {
                return utilisateurs.FindAll(x=> x.Fonction != "Praticien");            
            }
            #region Old commented by wael
            /*
            get
            {
                if (utilisateurs == null || utilisateurs.Count == 0)
                    _utilisateurs = getUtilisateurs();
                return utilisateurs.FindAll(x => x.Fonction != "Praticien");

                /* foreach (Utilisateur u in UtilisateursMgt.utilisateurs)
                     if (u.Actif)
                     {
                         if (u.Fonction != "Praticien")
                             lst.Add(u);
                     }

                 return lst;*/
              //  return null;
       //     }
           
            #endregion

        }


        public static void ClearCurrentUser()
        {

            ClearCurrentUserOnRegistry();
            _Currentutilisateur = null;
            if (OnCurrentUtilisateurChange != null)
                OnCurrentUtilisateurChange(null, new EventArgs());

        }

        public static void GetCurrentUserOnRegistry()
        {

            RegistryKey key = Registry.CurrentUser.OpenSubKey(_CurrentUserRegistryKey);

            // If the return value is null, the key doesn't exist
            if (key == null) return;

            string objValidityDate = (string)key.GetValue("ValidityDate");
            string objValidityUser = (string)key.GetValue("ValidityUser");

            key.Close();

            DateTime ValidityDate;
            int IdUser;

            if (DateTime.TryParse(objValidityDate, out ValidityDate) && int.TryParse(objValidityUser, out IdUser))
            {
                if (ValidityDate > DateTime.Now)
                {
                    _Currentutilisateur = BasCommon_BL.AccessMgmt.getAccessObject(IdUser);
                    _ValidityDate = ValidityDate;
                }
            }
        }
        public static int GetCurrentCabOnRegistry()
        {

            RegistryKey key = Registry.CurrentUser.OpenSubKey(_CurrentCabRegistryKey);

            // If the return value is null, the key doesn't exist
            if (key == null) return -1;

            string objValidityDate = (string)key.GetValue("ValidityDate");
            string objValidityUser = (string)key.GetValue("ValidityCab");

            key.Close();

            DateTime ValidityDate;
            int IdCab = 1;

            if (DateTime.TryParse(objValidityDate, out ValidityDate) && int.TryParse(objValidityUser, out IdCab))
            {
                if (ValidityDate > DateTime.Now)
                {
                    
                    _ValidityDate = ValidityDate;
                }
            }
            return IdCab;
        }

        public static void ClearCurrentUserOnRegistry()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(_CurrentUserRegistryKey, true);

                // If the return value is null, the key doesn't exist
                if (key == null)
                {
                    // The key doesn't exist; create it / open it
                    key = Registry.CurrentUser.CreateSubKey(_CurrentUserRegistryKey);
                }

                key.SetValue("ValidityDate", DateTime.Now.ToString());
                key.SetValue("ValidityUser", "-1");

                key.Close();
            }
            catch (System.Exception)
            { }
        }

        public static void SetCurrentUserOnRegistry()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(_CurrentUserRegistryKey, true);

                // If the return value is null, the key doesn't exist
                if (key == null)
                {
                    // The key doesn't exist; create it / open it
                    key = Registry.CurrentUser.CreateSubKey(_CurrentUserRegistryKey);
                }

                key.SetValue("ValidityDate", _ValidityDate.ToString());
                key.SetValue("ValidityUser", _Currentutilisateur.Utilisateur.Id.ToString());

                key.Close();
            }
            catch (System.Exception)
            { }
        }


        public static void SetCurrentUCabOnRegistry(string value)
        {
            try
            {
                string _CurrentCabRegistryKey = "Software\\BASE\\BASEPractice\\Preferences\\CurrentCab";
                RegistryKey key = Registry.CurrentUser.OpenSubKey(_CurrentCabRegistryKey, true);

                // If the return value is null, the key doesn't exist
                if (key == null)
                {
                    // The key doesn't exist; create it / open it
                    key = Registry.CurrentUser.CreateSubKey(_CurrentCabRegistryKey);
                }

                key.SetValue("ValidityDate", DateTime.Now.Date.AddHours(23).AddMinutes(58).ToString());
                key.SetValue("ValidityCab", value);

                key.Close();
            }
            catch (System.Exception)
            { }
        }
        public static List<Utilisateur> Praticiens
        {
            get
            {
                return utilisateurs.FindAll(x => x.Fonction == "Praticien" || x.type == Utilisateur.typeUtilisateur.Praticien);
            }
            #region Old commented by wael
            /*
            get
            {
                if (utilisateurs == null || utilisateurs.Count == 0)
                    _utilisateurs = getUtilisateurs();
                return utilisateurs.FindAll(x => x.Fonction == "Praticien" || x.type == Utilisateur.typeUtilisateur.Praticien);
                /*  foreach (Utilisateur u in UtilisateursMgt.utilisateurs)
                      if (u.Actif)
                      {
                          if ((u.Fonction == "Praticien") || (u.type == Utilisateur.typeUtilisateur.Praticien))
                              lst.Add(u);
                      }

                  return lst;*/
                //return null;
           // }
            #endregion
        }


        public static void DeleteHorairesReelWeek(Utilisateur ut, int WeekNum, int Annee)
        {
            //foreach (HoraireReel hr in ut.horairesreels)
            for (int i = ut.horairesreels.Count - 1; i >= 0; i--)
            {
                HoraireReel hr = ut.horairesreels[i];
                if ((hr.Year == Annee) && (hr.WeekNum == WeekNum))
                {
                    ut.horairesreels.Remove(hr);
                    MgmtHoraire.DeleteHoraire(hr);
                }
            }
        }

        public static void CopyHoraireDeTravailToReel(Utilisateur ut, int WeekNum, int Annee)
        {
            if (ut.horairesDeTravail == null)
                UtilisateursMgt.FillHoraireDeTravail(ut);
            DeleteHorairesReelWeek(ut, WeekNum, Annee);

            foreach (HorairesDeTravail ht in ut.horairesDeTravail)
            {
                HoraireReel hr = new HoraireReel();
                hr.endtime = ht.endtime;
                hr.id_utilisateur = ut.Id;
                hr.starttime = ht.starttime;
                hr.week_day = ht.week_day;
                hr.WeekNum = WeekNum;
                hr.Year = Annee;
                ut.horairesreels.Add(hr);
                MgmtHoraire.AddHorairesReel(hr);
            }
        }



        public static void SynchroHoraire(Utilisateur CurrentUtilisateur)
        {
            List<HoraireReel> lstFinal = new List<HoraireReel>();
            List<HoraireReel> lstPointeuse = MgmtHoraire.GetHorairesPointeuse(CurrentUtilisateur);
            int currentweek = 0;


            DateTime currentDte = new DateTime(DateTime.Now.Year, 1, 1);

            while (currentDte <= new DateTime(DateTime.Now.Year, 12, 31))
            {
                currentweek = GetYearWeek(currentDte);
                bool IsReelWeek = false;

                DateTime returneddte;
                if (!IsOnHoliday(CurrentUtilisateur, currentDte.AddMinutes(5), currentDte.AddHours(23), out returneddte))
                {
                    UserStatus state;
                    if (!IsOutOfOffice(CurrentUtilisateur, currentDte.AddMinutes(5), currentDte.AddHours(23), out state))
                    {





                        bool done = false;

                        foreach (HoraireReel hr in lstPointeuse)
                            if (UtilisateursMgt.GetDateFromHoraireReel(hr).Date == currentDte.Date)
                            {
                                lstFinal.Add(hr);
                                done = true;
                            }
                        if (done)
                        {
                            currentDte = currentDte.AddDays(1);
                            continue;
                        }

                        foreach (HoraireReel hr in CurrentUtilisateur.horairesreels)
                            if (hr.WeekNum == currentweek)
                                IsReelWeek = true;

                        if (IsReelWeek)
                        {
                            foreach (HoraireReel hr in CurrentUtilisateur.horairesreels)
                                if (UtilisateursMgt.GetDateFromHoraireReel(hr).Date == currentDte.Date)
                                {
                                    lstFinal.Add(hr);
                                    done = true;
                                }
                            if (done)
                            {
                                currentDte = currentDte.AddDays(1);
                                continue;
                            }
                        }
                        else
                        {
                            if (CurrentUtilisateur.horairesDeTravail == null)
                                UtilisateursMgt.FillHoraireDeTravail(CurrentUtilisateur);
                            foreach (HorairesDeTravail ht in CurrentUtilisateur.horairesDeTravail)
                                if (ht.week_day == (int)currentDte.Date.DayOfWeek)
                                {
                                    HoraireReel hr = new HoraireReel();
                                    hr.endtime = ht.endtime;
                                    hr.id_utilisateur = CurrentUtilisateur.Id;
                                    hr.starttime = ht.starttime;
                                    hr.week_day = ht.week_day;
                                    hr.WeekNum = GetYearWeek(currentDte);
                                    hr.Year = currentDte.Year;

                                    #region TypePeriodicity.JourParMois
                                    if (ht.period.tpeperiod == HoraireTrPeriodicity.TypePeriodicity.JourParMois)
                                    {
                                        int nbdays = 0;
                                        if (ht.period.MonthPeriodicityDay == currentDte.DayOfWeek)
                                            for (int i = 0; i < currentDte.Day; i++)
                                                if (currentDte.AddDays(i).DayOfWeek == ht.period.MonthPeriodicityDay) nbdays++;

                                        if (nbdays != ht.period.MonthPeriodicityNum)
                                        {
                                            hr = null;
                                        }
                                    }
                                    #endregion

                                    #region TypePeriodicity.XSemainesSur
                                    if (ht.period.tpeperiod == HoraireTrPeriodicity.TypePeriodicity.XSemainesSur)
                                    {
                                        int weekRef = GetYearWeek(ht.period.FirstDate);
                                        int weekAct = GetYearWeek(currentDte);
                                        if (((weekAct - weekRef) % ht.period.MonthPeriodicityNum) != 0)
                                        {
                                            hr = null;
                                        }
                                    }
                                    #endregion



                                    if (hr != null) lstFinal.Add(hr);
                                }
                        }
                    }
                }
                currentDte = currentDte.AddDays(1);



            }
            CurrentUtilisateur.horairesDePointeuse.Clear();
            CurrentUtilisateur.horairesDePointeuse = lstFinal;

            CurrentUtilisateur.SynchroPointeuse = true;
        }

        public static string getprivate(Utilisateur _utilisateur, string key)
        {
            byte[] array = DAC.getPrivate(_utilisateur.Id);
            if (array != null)
                return Cryptage.Decrypt(array, key);
            else
                return "";
        }


        public static List<PointageAsRange> GetPointagesAsRange(List<Pointage> lstpointages, DateTime dtestart, DateTime dteEnd)
        {
            lstpointages.Sort();
            Pointage dteIn = null;
            Pointage dteOut = null;
            List<PointageAsRange> lstresult = new List<PointageAsRange>();

            foreach (Pointage p in lstpointages)
            {
                if (p.DateTimePointage < dtestart) continue;
                if (p.DateTimePointage > dteEnd) continue;

                if ((p.sens == Pointage.SensPointage.Entree) && (dteIn == null))
                {
                    dteIn = p;
                    continue;
                }

                if ((p.sens == Pointage.SensPointage.Sortie) && (dteOut == null) && (dteIn != null))
                {
                    dteOut = p;

                    if (dteIn.DateTimePointage.Date == dteOut.DateTimePointage.Date)
                    {
                        PointageAsRange par = new PointageAsRange();
                        par.Entre = dteIn;
                        par.sortie = dteOut;
                        par.dateEntree = dteIn.DateTimePointage;
                        par.dateSortie = dteOut.DateTimePointage;
                        lstresult.Add(par);
                        dteIn = null;
                        dteOut = null;
                    }
                    else
                    {
                        PointageAsRange par = new PointageAsRange();
                        par.Entre = dteIn;
                        par.sortie = null;
                        par.dateEntree = dteIn.DateTimePointage;
                        par.dateSortie = dteIn.DateTimePointage.Date.AddHours(18);
                        lstresult.Add(par);

                        par = new PointageAsRange();
                        par.Entre = null;
                        par.sortie = dteOut;
                        par.dateEntree = dteOut.DateTimePointage.Date.AddHours(8);
                        par.dateSortie = dteOut.DateTimePointage;
                        lstresult.Add(par);

                        dteIn = null;
                        dteOut = null;
                    }
                }


            }

            if ((dteIn != null) && (dteOut == null))
            {
                PointageAsRange par = new PointageAsRange();
                par.dateEntree = dteIn.DateTimePointage;
                par.dateSortie = dteEnd;
                lstresult.Add(par);
                dteIn = null;
                dteOut = null;
            }
            return lstresult;

        }

        public static StatusPointage GetStatusPointage(Utilisateur u, DateTime dte)
        {

            if (u == null) return StatusPointage.Inconnue;
            int weekstrt = GetYearWeek(dte);

            if (u.horairesreels == null)
                u.horairesreels = MgmtHoraire.GetHorairesReel(u, weekstrt);


            bool mustWork = false;
            bool Working = false;
            foreach (HoraireReel hr in u.horairesreels)
            {
                if (weekstrt != hr.WeekNum) continue;
                if ((hr.starttime.TimeOfDay < dte.TimeOfDay) &&
                    (hr.endtime.TimeOfDay > dte.TimeOfDay) &&
                    (((DayOfWeek)hr.week_day) == dte.DayOfWeek) &&
                           dte.Year == hr.Year)
                {
                    mustWork = true;
                    break;
                }
            }

            if (!mustWork)
            {

                if (u.status == null) UtilisateursMgt.GetUtilisateursStatus(u);
                foreach (UserStatus us in u.status)
                {
                    if ((us.dateStart < dte) &&
                    (us.dateEnd > dte) &&
                    (!us.status.IsAnAbsence))
                    {
                        mustWork = true;
                        break;
                    }
                }
            }









            if (u.pointageDuJour == null)
                u.pointageDuJour = BasCommon_BL.UtilisateursMgt.getPointagesDuJour(u,dte);
            u.pointageDuJour.Sort();

            Pointage LatestPt = null;

            foreach (Pointage p in u.pointageDuJour)
                if (p.DateTimePointage < dte)
                    LatestPt = p;


            if ((LatestPt != null) && (LatestPt.sens == Pointage.SensPointage.Entree))
                Working = true;


            if (!mustWork && !Working) return StatusPointage.AbsenceNormal;
            if (mustWork && !Working) return StatusPointage.AbsenceANormal;
            if (mustWork && Working) return StatusPointage.PresenceNormal;
            if (!mustWork && Working) return StatusPointage.PresenceHeureSupp;

            return StatusPointage.Inconnue;

        }


        public static byte[] getprivate(Utilisateur _utilisateur)
        {
            return DAC.getPrivate(_utilisateur.Id);
        }

        public static void setprivate(Utilisateur _utilisateur, string text, string key)
        {
            byte[] array = Cryptage.Encrypt(text, key);
            DAC.setPrivate(array, _utilisateur.Id);
        }

        public static void ForceRefreshUserList()
        {
            _utilisateurs.Clear();
            getUtilisateurs();
            FillRHUtilisateur();
        }

        private static List<Utilisateur> getUtilisateurs()
        {

            JArray json = DAC.getMethodeJsonArray("/Utilisateurs");

            List<Utilisateur> lst = new List<Utilisateur>();

            foreach (JObject r in json)
                lst.Add(Builders.BuildUtilisateur.BuildJ(r));
            return lst;
        }
        private static List<Utilisateur> getUtilisateursOLD()
        {

            DataTable dt = DAC.getUtilisateurs();

            List<Utilisateur> lst = new List<Utilisateur>();

            foreach (DataRow r in dt.Rows)
                lst.Add(Builders.BuildUtilisateur.Build(r));


            return lst;
        }

        public static void FillRHUtilisateur()
        {
            foreach (Utilisateur u in _utilisateurs)
            {
                FillHoraireDeTravail(u);

                FillHoraireReel(u);

                FillHolidays(u);

                FillStatus(u);

                FillNbJoursDeConges(u);
            }

        }

        public static void FillNbJoursDeConges(Utilisateur u)
        {
            u.NbJoursDeCongés = DAC.GetNbJoursDeConges(u);
        }
        public static void FillStatus(Utilisateur u)
        {

            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray("/AllUserStatusByIdUser/"+u.Id);
            List<UserStatus> liste = new List<UserStatus>();

            foreach (JObject obj in jArray) 
            {
                liste.Add(Builders.BuildStatus.BuildUserStatus(obj, u));
            }
            u.status = liste;

        }

        public static void FillStatusOld(Utilisateur u)
        {

            //DataTable dt = DAC.getUtilisateursStatus(u);
            //List<UserStatus> lst = new List<UserStatus>();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    lst.Add(Builders.BuildStatus.BuildUserStatus(dr, u));
            //}
            //u.status = lst;
        }

        public static void FillHolidays(Utilisateur u)
        {
            List<Holiday> lst = new List<Holiday>();
            DataTable dt = DAC.getHolidays(u);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildHoraire.BuildHoliday(r));
            }
            u.Holidays = lst;
        }
        public static void FillHoraireReel(Utilisateur u) {

            List<HoraireReel> liste = new List<HoraireReel>();
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray("/HorraireReel/"+u.Id);

            foreach (JObject obj in jArray) {
                liste.Add(Builders.BuildHoraire.buildHorraireReelFromJObject(obj));            
            }
            u.horairesreels = liste;
        }

        public static void FillHoraireReelOld(Utilisateur u)
        {
            List<HoraireReel> lst = new List<HoraireReel>();
            DataTable dt = DAC.getHorairesReel(u);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildHoraire.BuildHoraireReel(r));
            }

            u.horairesreels = lst;
        }
        public static void FillHoraireDeTravail(Utilisateur u) {
            BasCommon_BL.MgmtHoraire.GetHorairesDeTravail(u);
        }


        public static void FillHoraireDeTravailOld(Utilisateur u)
        {
            // Old Method
            List<HorairesDeTravail> lst = new List<HorairesDeTravail>();
            DataTable dt = DAC.getHorairesDeTravail(u);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildHoraire.BuildHoraires(r));
            }
            u.horairesDeTravail = lst;

        }

        public static void DeleteUtilisateur(Utilisateur u)
        {
            DAC.DelUtilisateur(u);
        }

        public static Utilisateur getUtilisateur(int Id)
        {
            if (utilisateurs == null || utilisateurs.Count == 0)
                _utilisateurs = getUtilisateurs();           
            Utilisateur u = _utilisateurs.Find(ut => ut.Id == Id);            
            return u;
    
        }

        public static Utilisateur getUtilisateur(string nom)
        {
            if (utilisateurs == null || utilisateurs.Count == 0)
                _utilisateurs = getUtilisateurs();
            return utilisateurs.Find(w => w.Nom.ToUpper() == nom.ToUpper());
            /*     foreach (Utilisateur ut in utilisateurs)
                     if (ut.Nom.ToUpper() == nom.ToUpper()) return ut;
                 */
          //  return null;
        }





        public static void AddPointage(Utilisateur user, Pointage.SensPointage sens)
        {
            Pointage p = new Pointage();
            p.DateTimePointage = DateTime.Now;
            p.user = user;
            p.sens = sens;
            DAC.AddPointage(p);
            if (user.pointageDuJour != null) user.pointageDuJour.Add(p);
            if (user.pointages != null) user.pointages.Add(p);


        }






        public static TimeSpan GetCumulHeuresPointage(List<Pointage> lst)
        {
            DateTime? dteref = null;
            foreach (Pointage p in lst)
            {
                if (dteref == null)
                    dteref = p.DateTimePointage.Date;
                else
                    if (dteref.Value.Date != p.DateTimePointage.Date)
                        throw new SystemException("tous les pointages doivent être de la meme journée");

            }
            if (dteref == null) return new TimeSpan();

            TimeSpan cumul = new TimeSpan();
            lst.Sort();
            DateTime? LastDateEntre = null;

            foreach (Pointage p in lst)
            {
                if (p.sens == Pointage.SensPointage.Entree)
                {
                    LastDateEntre = p.DateTimePointage;
                }
                if (p.sens == Pointage.SensPointage.Sortie)
                {
                    if (LastDateEntre != null)
                        cumul += (p.DateTimePointage - LastDateEntre.Value);

                    LastDateEntre = null;
                }

            }

            if ((LastDateEntre != null) && (dteref.Value.Date == DateTime.Now.Date))
                cumul += (DateTime.Now - LastDateEntre.Value);

            if ((LastDateEntre != null) && (dteref.Value.Date < DateTime.Now.Date))
                cumul += (LastDateEntre.Value.Date.AddDays(1) - LastDateEntre.Value);


            return cumul;
        }

        public static List<Pointage> getPointagesDuJour(Utilisateur user,DateTime dte) 
        {
            String d1 = dte.Date.ToString("yyyy-MM-dd HH:mm:ss");
            String d2 = dte.Date.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");

            string methodPath = "/pointageDuJourByIdUserAndDates/";
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(methodPath+user.Id+"&"+d1+"&"+d2);
            List<Pointage> liste = new List<Pointage>();

            foreach (JObject obj in jArray) 
                liste.Add(Builders.BuildPointage.BuildJson(obj));         
            
            return liste;
                    
        }

        public static List<Pointage> getPointagesDuJourOld(Utilisateur user)
        {
            DataTable dt = DAC.GetPointages(user, DateTime.Now.Date);

            List<Pointage> lst = new List<Pointage>();

            foreach (DataRow dr in dt.Rows)
            {
                Pointage p = Builders.BuildPointage.Build(dr);
                lst.Add(p);
            }


            return lst;
        }

        public static List<Pointage> getPointages(Utilisateur user, DateTime dteStart, DateTime dteEnd) 
        {
            String d1 = dteStart.Date.ToString("yyyy-MM-dd HH:mm:ss");
            String d2 = dteEnd.Date.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");

            string methodPath = "/pointageDuJourByIdUserAndDates/";
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(methodPath+user.Id+"&"+d1+"&"+d2);
            List<Pointage> liste = new List<Pointage>();

            foreach (JObject obj in jArray)
                liste.Add(Builders.BuildPointage.BuildJson(obj));

            return liste; 
        }

        public static List<Pointage> getPointagesOld(Utilisateur user, DateTime dteStart, DateTime dteEnd)
        {
            DataTable dt = DAC.GetPointages(user, dteStart, dteEnd);

            List<Pointage> lst = new List<Pointage>();

            foreach (DataRow dr in dt.Rows)
            {
                Pointage p = Builders.BuildPointage.Build(dr);
                lst.Add(p);
            }

            return lst;
        }
        public static List<Pointage> getPointages(Utilisateur user)
        {
            // valide
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray("/PointageByUserId/" + user.Id);
            List<Pointage> liste = new List<Pointage>();
            foreach (JObject obj in jArray)
            {
                Pointage p = Builders.BuildPointage.BuildJson(obj);
                liste.Add(p);
            }
            return liste;
        }

        public static List<Pointage> getPointagesOld(Utilisateur user)
        {
          
            DataTable dt = DAC.GetPointages(user);
            List<Pointage> lst = new List<Pointage>();

            foreach (DataRow dr in dt.Rows)
            {
                Pointage p = Builders.BuildPointage.Build(dr);
                lst.Add(p);
            }
            return lst;    
        }

        #region Gestion des absences

        public static bool UserIsPresent(Utilisateur ut)
        {
            string Comment = "";
            UserStatus us;
            return UserIsPresent(ut, DateTime.Now, DateTime.Now.AddMinutes(5), out Comment, out us);
        }

        public static bool UserIsPresent(Utilisateur ut, DateTime dte)
        {
            string Comment = "";
            UserStatus us;
            return UserIsPresent(ut, dte, dte.AddMinutes(5), out Comment, out us);
        }

        public static bool UserIsPresent(Utilisateur ut, DateTime dteStart, DateTime dteEnd, out UserStatus status)
        {
            string Comment = "";
            return UserIsPresent(ut, dteStart, dteEnd, out Comment, out status);
        }

        public static bool UserIsPresent(Utilisateur ut, out string Comment, out UserStatus status)
        {
            return UserIsPresent(ut, DateTime.Now, out Comment, out status);
        }

        public static bool UserIsPresent(Utilisateur ut, DateTime dte, out string Comment, out UserStatus status)
        {
            return UserIsPresent(ut, dte.Date, dte.Date.AddHours(23), out Comment, out status);
        }

        public static bool UserIsPresent(Utilisateur ut, DateTime dteStart, DateTime dteEnd, out string Comment, out UserStatus status)
        {
            bool m_IsPresent = false;

            DateTime returnedDate;
            string msg = "";
            Comment = "";


            m_IsPresent = !IsOutOfOffice(ut, dteStart, dteEnd, out status);

            if ((status == null) && (!m_IsPresent)) Comment = "Absent";
#if TRACE
            Debug.Write("IsOutOfOffice :" + ut.Id + " " + ut.Nom + " " + ut.Prenom + " m_IsPresent IsOutOfOffice : " + m_IsPresent);
#endif

            if (m_IsPresent)
            {
                if ((status != null) && (status.status.IsAnAbsence))
                {
                    if ((status.dateStart.Date == dteStart.Date) && (status.dateStart.Hour >= 12))
                    {
                        Comment = "AM";
                        dteEnd = status.dateStart.AddMinutes(-5);
                    }
                    else
                        if ((status.dateEnd.Date == dteStart.Date) && (status.dateEnd.Hour <= 12))
                        {
                            Comment = "PM";
                            dteStart = status.dateEnd.AddMinutes(5);
                        }
                        else
                        {
                            m_IsPresent = false;
                            Comment = status.ShortDisplay;
                            return false;
                        }
                }

                if (!IsNotFerie(dteStart, out msg))
                {
                    Comment = msg;
                    return false;
                }


            }
            else
            {

                if (status != null)
                {
                    Comment = status.ShortDisplay;
                    if (status.dateStart.Date != status.dateEnd.Date)
                        Comment += "jusque " + status.dateEnd.Date.ToShortDateString();
                }
            }

            return m_IsPresent;
        }


        public static DateTime GetLastSynchroGoogle(Utilisateur u)
        {
            return DAC.GetLastSynchroGoogle(u);
        }

        public static void Update(Utilisateur ut)
        {
            DAC.UpdateUtilisateur(ut);
        }


        public static void UpdateNextSynchroGoogle(Utilisateur ut, DateTime dte)
        {
            if (dte < DateTime.Now) return;

            DAC.UpdateSynchroGoogle(ut, dte);
        }

        public static void Add(Utilisateur ut)
        {
            DAC.AddUtilisateur(ut);
            _utilisateurs.Add(ut);
        }

        public static void Remove(Utilisateur ut)
        {
            DAC.DelUtilisateur(ut);
            _utilisateurs.Remove(ut);
        }

        public static void AddStatus(Utilisateur ut, UserStatus st)
        {

            if (!DAC.IsExistStatus(ut, st))
            {
                ut.status.Add(st);
                DAC.AddStatus(ut, st);
            }
        }

        public static void UpdateStatus(UserStatus st)
        {
            DAC.UpdateUtilisateursStatus(st);
        }


        public static void SetNbJoursDeConges(Utilisateur ut, int NbJours)
        {
            ut.NbJoursDeCongés = NbJours;
            DAC.SetNbJoursDeConges(ut, NbJours);
        }

        public static int GetNbJoursDeConges(Utilisateur ut)
        {

            DateTime StartDateOfCurrentYear;
            DateTime EndDateOfCurrentYear;
            int y = DateTime.Now.Year;
            if (DateTime.Now >= new DateTime(y, 5, 1))
            {
                StartDateOfCurrentYear = new DateTime(y, 5, 1); //1 Mai N-1
                EndDateOfCurrentYear = new DateTime(y + 1, 4, 30); //30 Avril N-1
            }
            else
            {
                StartDateOfCurrentYear = new DateTime(y - 1, 5, 1); //1 Mai N
                EndDateOfCurrentYear = new DateTime(y, 4, 30); //30 Avril N
            }


            return GetNbJoursDeConges(ut, StartDateOfCurrentYear, EndDateOfCurrentYear);
        }


        public static int GetNbJoursDeConges(Utilisateur ut, List<DateTime> lstdte)
        {
            int NbJours = 0;
            string libferie;

            if (ut.Holidays == null)
                ut.Holidays = MgmtHoraire.GetHolidays(ut);

            foreach (Holiday h in ut.Holidays)
            {
                DateTime dte = h.startdate.Date;
                while (dte < h.enddate.AddDays(1).Date)
                {
                    UserStatus stat;
                    if (IsNotFerie(dte.Date, out libferie) && IsAWorkDay(ut, dte.Date) &&
                        (!IsOutOfOffice(ut, dte.Date.AddHours(6), dte.Date.AddHours(23), out stat)) &&
                        lstdte.Contains(dte.Date))
                        NbJours++;
                    dte = dte.AddDays(1);
                }
            }

            return NbJours;
        }


        public static int GetNbJoursDeConges(Utilisateur ut, DateTime dteStart, DateTime dteEnd)
        {
            int NbJours = 0;
            string libferie;

            if (ut.Holidays == null)
                ut.Holidays = MgmtHoraire.GetHolidays(ut);

            foreach (Holiday h in ut.Holidays)
            {
                DateTime dte = h.startdate.Date;
                while (dte < h.enddate.AddDays(1).Date)
                {
                    UserStatus stat;
                    if (IsNotFerie(dte.Date, out libferie) && IsAWorkDay(ut, dte.Date) &&
                        (!IsOutOfOffice(ut, dte.Date.AddHours(6), dte.Date.AddHours(23), out stat)) &&
                        (dteStart <= dte) && (dteEnd >= dte))
                        NbJours++;
                    dte = dte.AddDays(1);
                }
            }

            return NbJours;
        }



        public static void DeleteHoliday(Utilisateur ut, Holiday holiday)
        {
            ut.Holidays.Remove(holiday);
            DAC.DelHoliday(holiday);

        }

        public static void DeleteUtilisateursStatus(UserStatus stat)
        {
            stat.utilisateur.status.Remove(stat);
            DAC.DelUtilisateursStatus(stat);

        }


        public static void UpdateHolidays(Holiday holiday)
        {
            DAC.updateHoliday(holiday);
        }

        public static void AddHolidays(Utilisateur ut, Holiday holiday)
        {
            ut.Holidays.Add(holiday);
            DAC.AddHoliday(ut, holiday);
        }

        public static void GetCurrentPeriode(out DateTime dtestart, out DateTime dteEnd)
        {
            if (DateTime.Now > new DateTime(DateTime.Now.Year, 5, 1))
            {
                dtestart = new DateTime(DateTime.Now.Year, 5, 1); //1 Mai N-1
                dteEnd = new DateTime(DateTime.Now.Year + 1, 4, 30); //30 Avril N-1
            }
            else
            {
                dtestart = new DateTime(DateTime.Now.Year - 1, 5, 1); //1 Mai N
                dteEnd = new DateTime(DateTime.Now.Year, 4, 30); //30 Avril N
            }
        }



        public static float GetNbJoursWithCode(Utilisateur ut, string code)
        {
            DateTime StartDateOfCurrentYear;
            DateTime EndDateOfCurrentYear;
            GetCurrentPeriode(out  StartDateOfCurrentYear, out EndDateOfCurrentYear);

            return GetNbJoursAbsentWithCode(ut, code, StartDateOfCurrentYear, EndDateOfCurrentYear);
        }


        public static float GetNbJoursPasseWithCode(Utilisateur ut, string code)
        {
            DateTime StartDateOfCurrentYear;
            DateTime EndDateOfCurrentYear;
            GetCurrentPeriode(out  StartDateOfCurrentYear, out EndDateOfCurrentYear);

            return GetNbJoursAbsentWithCode(ut, code, StartDateOfCurrentYear, (EndDateOfCurrentYear < DateTime.Now) ? EndDateOfCurrentYear : DateTime.Now);
        }

        public static float GetNbJoursAVenirWithCode(Utilisateur ut, string code)
        {
            DateTime StartDateOfCurrentYear;
            DateTime EndDateOfCurrentYear;
            GetCurrentPeriode(out  StartDateOfCurrentYear, out EndDateOfCurrentYear);

            return GetNbJoursAbsentWithCode(ut, code, (StartDateOfCurrentYear > DateTime.Now) ? StartDateOfCurrentYear : DateTime.Now, EndDateOfCurrentYear);
        }

        public static float GetNbJoursAbsentWithCode(Utilisateur ut, string code, DateTime startDate, DateTime EndDate)
        {
            float nbabsent = 0;

            if (ut.status == null)
                ut.status = UtilisateursMgt.GetUtilisateursStatus(ut);

            foreach (UserStatus us in ut.status)
            {
                if ((code != "") && (code != us.status.code)) continue;

                DateTime dteStart = startDate > us.dateStart ? startDate : us.dateStart;
                if (dteStart > EndDate) continue;

                DateTime dteend = EndDate < us.dateEnd ? EndDate : us.dateEnd;
                if (dteend < startDate) continue;


                if ((dteStart.TimeOfDay.Ticks == 0) && (dteend.TimeOfDay.Ticks == 0))
                    nbabsent += (float)(dteend.Date.AddDays(1) - dteStart.Date).TotalDays;
                else
                {
                    DateTime dtstr = dteStart;

                    while (dtstr < us.dateEnd)
                    {
                        DateTime endoftheday = us.dateEnd < dtstr.Date.AddHours(24) ? us.dateEnd : dtstr.Date.AddHours(24);

                        double nbhourinday = (endoftheday - dtstr).TotalHours;
                        nbabsent += nbhourinday > 4 ? 1 : 0.5f;

                        dtstr = endoftheday;
                    }
                }

                /*

                DateTime dtStr = dteStart.Hour > 12 ? dteStart.Date.AddHours(14) : dteStart.Date.AddHours(8);
                DateTime dtEnd = dteend.Hour > 12 ? dteend.Date.AddHours(18) : dteStart.Date.AddHours(12);

                while (dtStr < dtEnd)
                {
                    nbabsent += .5f;
                    switch ((int)dtStr.Hour)
                    {
                        case 8: dtStr = dtStr.AddHours(6); break;
                        case 14: dtStr = dtStr.AddHours(18); break;
                    }
                }

                */
            }

            return nbabsent;
        }


        public static double GetNbJoursDeCongeAPrendre(Utilisateur ut)
        {

            if (ut.DateEmbauche == null) return -1;

            DateTime StartDateOfCurrentYear;
            DateTime EndDateOfCurrentYear;
            if (DateTime.Now < new DateTime(DateTime.Now.Year, 6, 1))
            {
                StartDateOfCurrentYear = new DateTime(DateTime.Now.Year, 6, 1); //1 Juin N-1
                EndDateOfCurrentYear = new DateTime(DateTime.Now.Year + 1, 5, 31); //31 Mai N-1
            }
            else
            {
                StartDateOfCurrentYear = new DateTime(DateTime.Now.Year - 1, 6, 1); //1 Juin N
                EndDateOfCurrentYear = new DateTime(DateTime.Now.Year, 5, 31); //31 Mai N
            }

            DateTime StartDte = ut.DateEmbauche.Value < StartDateOfCurrentYear ? StartDateOfCurrentYear : ut.DateEmbauche.Value;

            StartDte = new DateTime(StartDte.Year, StartDte.Month, StartDte.Day);



            int NbMois = 0;
            int OldMonth = StartDte.Month;
            //int nbjoursOuvrable = 0;
            while (StartDte <= EndDateOfCurrentYear.AddDays(1))
            {

                StartDte = StartDte.AddDays(1);
                if (StartDte.Month != OldMonth)
                {
                    OldMonth = StartDte.Month;
                    NbMois++;
                }
                // if ((StartDte.DayOfWeek != DayOfWeek.Sunday)) nbjoursOuvrable++;
            }
            /*
            int NbSemaines = nbjoursOuvrable / 6;
            int NombreDeMois = NbSemaines / 4;
            */
            return Math.Round(NbMois * 2.5);

            // return Math.Round((DateTime.Now.Month-StartDte.Month) * 2.5);
        }

        public static double GetNbJoursDeCongeAcquis(Utilisateur ut)
        {

            if (ut.DateEmbauche == null) return -1;

            DateTime StartDateOfCurrentYear;
            if (DateTime.Now >= new DateTime(DateTime.Now.Year, 6, 1))
                StartDateOfCurrentYear = new DateTime(DateTime.Now.Year, 6, 1); //1 Juin N-1
            else
                StartDateOfCurrentYear = new DateTime(DateTime.Now.Year - 1, 6, 1); //1 Juin N


            DateTime StartDte = ut.DateEmbauche.Value < StartDateOfCurrentYear ? StartDateOfCurrentYear : ut.DateEmbauche.Value;

            StartDte = new DateTime(StartDte.Year, StartDte.Month, StartDte.Day);

            int NbMois = 0;
            int OldMonth = StartDte.Month;

            while (StartDte < DateTime.Now)
            {
                if (StartDte.Month != OldMonth)
                {
                    OldMonth = StartDte.Month;
                    NbMois++;
                }
                StartDte = StartDte.AddDays(1);
                // if ((StartDte.DayOfWeek != DayOfWeek.Sunday)) nbjoursOuvrable++;
            }
            /*
            int NbSemaines = nbjoursOuvrable / 6;
            int NombreDeMois = NbSemaines / 4;
            */
            return Math.Round(NbMois * 2.5);

            // return Math.Round((DateTime.Now.Month-StartDte.Month) * 2.5);
        }


        public static List<UserStatus> FindUserStatus(Utilisateur u, DateTime dte)
        {
            List<UserStatus> lst = new List<UserStatus>();

            if (u.status == null)
                u.status = UtilisateursMgt.GetUtilisateursStatus(u);

            foreach (UserStatus us in u.status)
            {
                if (((us.dateStart > dte.Date) && (us.dateStart < dte.Date.AddDays(1))) ||
                    ((us.dateEnd > dte.Date) && (us.dateEnd < dte.Date.AddDays(1))) ||
                    ((us.dateStart < dte.Date) && (us.dateEnd > dte.Date.AddDays(1))))
                    lst.Add(us);
            }

            return lst;
        }


        public static bool IsOutOfOffice(Utilisateur ut, DateTime dteStart, DateTime dteEnd, out UserStatus status)
        {
            bool m_IsOutOfOffice = false;
            status = null;

            if (ut.status == null)
                ut.status = UtilisateursMgt.GetUtilisateursStatus(ut);



            if (!ut.Actif)
            {
                return true;
            }

            int yearstr = (dteStart.Year);
            int yearend = (dteEnd.Year);
            int weekstrt = GetYearWeek(dteStart);
            int weekend = GetYearWeek(dteEnd);
            int daystr = ((int)dteStart.DayOfWeek);
            int dayend = ((int)dteEnd.DayOfWeek);

            m_IsOutOfOffice = true;
            if(ut.horairesreels == null)
            UtilisateursMgt.FillHoraireReel(ut);
            foreach (HoraireReel h in ut.horairesreels)
            {
                if (((h.Year >= yearstr) && (h.Year <= yearend)) &&
                    ((h.WeekNum >= weekstrt) && (h.WeekNum <= weekend)) &&
                    ((h.week_day >= daystr) && (h.week_day <= dayend)) &&
                    (((h.endtime.TimeOfDay > dteStart.TimeOfDay) && (h.endtime.TimeOfDay < dteEnd.TimeOfDay)) ||
                     ((h.starttime.TimeOfDay >= dteStart.TimeOfDay) && (h.starttime.TimeOfDay <= dteEnd.TimeOfDay)) ||
                     ((h.starttime.TimeOfDay <= dteStart.TimeOfDay) && (h.endtime.TimeOfDay >= dteEnd.TimeOfDay))))
                {
                    m_IsOutOfOffice = false;
                    break;
                }



            }

            foreach (UserStatus h in ut.status)
            {
                //Si la personne est presente une fraction du temp testÃ©, alors elle est presente
                if (((h.dateStart <= dteStart) &&
                    (dteEnd <= h.dateEnd)))
                {
                    m_IsOutOfOffice = h.status.IsAnAbsence;
                }


                if (((h.dateStart < dteStart) &&
                    (h.dateEnd > dteStart)) ||
                    ((h.dateStart < dteEnd) &&
                    (h.dateEnd > dteEnd)) ||
                    ((h.dateStart >= dteStart) &&
                    (h.dateEnd <= dteEnd)))
                {
                    status = h;
                }
            }
            return m_IsOutOfOffice;

        }

        public static int GetYearWeek(DateTime time)
        {
            System.Globalization.Calendar cal = System.Globalization.CultureInfo.InvariantCulture.Calendar;
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = cal.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return cal.GetWeekOfYear(time, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        }

        public static int GetMaxWeek(int year)
        {
            DateTime dte = new DateTime(year, 12, 31);
            int nb = GetYearWeek(dte);
            while (nb == 1) { nb = GetYearWeek(dte); dte = dte.AddDays(-1); };

            return nb;

        }

        private static bool IsAWorkDay(Utilisateur ut, DateTime dte)
        {
            bool ispresent = false;
            bool IsInRealWeek = false;

            DateTime dteStart = dte.Date;
            DateTime dteEnd = dte.Date.AddHours(23);

            if (ut.horairesDeTravail == null)
                UtilisateursMgt.FillHoraireDeTravail(ut);

            if (ut.horairesreels == null)
                UtilisateursMgt.FillHoraireReel(ut);

            if (ut.horairesDeTravail.Count == 0) return false;
            if (ut.horairesreels.Count == 0) return false;

            int weekstrt = GetYearWeek(dteStart);


            foreach (HoraireReel h in ut.horairesreels)
            {
                if (weekstrt == h.WeekNum)
                {
                    if ((((h.starttime.TimeOfDay <= dteStart.TimeOfDay) &&
                       (h.endtime.TimeOfDay >= dteStart.TimeOfDay)) ||
                       ((h.starttime.TimeOfDay <= dteEnd.TimeOfDay) &&
                       (h.endtime.TimeOfDay >= dteEnd.TimeOfDay)) ||
                       ((h.starttime.TimeOfDay >= dteStart.TimeOfDay) &&
                       (h.endtime.TimeOfDay <= dteEnd.TimeOfDay))) &&
                       (((DayOfWeek)h.week_day) == dteStart.DayOfWeek) &&
                       dteStart.Year == h.Year)
                    {

                        ispresent = true;
                    }
                    IsInRealWeek = true;
                }

            }

            if (!IsInRealWeek)
            {

                foreach (HorairesDeTravail h in ut.horairesDeTravail)
                {
                    if (h.week_day == (int)dte.DayOfWeek) ispresent = true;

                    if (ispresent)
                    {

                        #region TypePeriodicity.JourParMois
                        if (h.period.tpeperiod == HoraireTrPeriodicity.TypePeriodicity.JourParMois)
                        {
                            int nbdays = 0;
                            if (h.period.MonthPeriodicityDay == dte.DayOfWeek)
                                for (int i = 0; i < dte.Day; i++)
                                    if (dte.AddDays(i).DayOfWeek == h.period.MonthPeriodicityDay) nbdays++;

                            if (nbdays != h.period.MonthPeriodicityNum)
                            {
                                ispresent = false;
                            }
                        }
                        #endregion

                        #region TypePeriodicity.XSemainesSur
                        if (h.period.tpeperiod == HoraireTrPeriodicity.TypePeriodicity.XSemainesSur)
                        {
                            int weekRef = GetYearWeek(h.period.FirstDate);
                            int weekAct = GetYearWeek(dte);
                            if (((weekAct - weekRef) % h.period.MonthPeriodicityNum) != 0)
                            {
                                ispresent = false;
                            }
                        }
                        #endregion

                        return ispresent;
                    }
                }
            }
            return ispresent;


        }


        /// <summary>
        /// RÃ©cupere le nombre d'heures travaillÃ©s entre dteStart et dteEnd.
        /// dteStart et dteEnd doivent etre le mÃªme jour
        /// </summary>
        /// <param name="ut"></param>
        /// <param name="dteStart"></param>
        /// <param name="dteEnd"></param>
        /// <returns></returns>
        public static double NbHeuresPointees(Utilisateur ut, DateTime dteStart, DateTime dteEnd)
        {
            if (!ut.SynchroPointeuse) return -1;
            if (ut.horairesDeTravail == null)
                UtilisateursMgt.FillHoraireDeTravail(ut);
            if (ut.horairesDeTravail.Count == 0) return 0;


            if (dteStart.Date != dteEnd.Date) throw new ArgumentException("dteStart doit etre le mÃªme jour que dteEnd");
            long minutespresences = 0;
            int weekstrt = GetYearWeek(dteStart);

            #region Absence car hors horaire de travail

            foreach (HoraireReel h in ut.horairesDePointeuse)
            {
                if (weekstrt == h.WeekNum)
                {
                    if ((((h.starttime.TimeOfDay <= dteStart.TimeOfDay) &&
                       (h.endtime.TimeOfDay >= dteStart.TimeOfDay)) ||
                       ((h.starttime.TimeOfDay <= dteEnd.TimeOfDay) &&
                       (h.endtime.TimeOfDay >= dteEnd.TimeOfDay)) ||
                       ((h.starttime.TimeOfDay >= dteStart.TimeOfDay) &&
                       (h.endtime.TimeOfDay <= dteEnd.TimeOfDay))) &&
                       (((DayOfWeek)h.week_day) == dteStart.DayOfWeek) &&
                       dteStart.Year == h.Year)
                    {

                        if (h.endtime > h.starttime) minutespresences += (long)(h.endtime - h.starttime).TotalMinutes;
                    }
                }

            }



            #endregion

            return minutespresences;
        }


        /// <summary>
        /// RÃ©cupere le nombre d'heures travaillÃ©s entre dteStart et dteEnd.
        /// dteStart et dteEnd doivent etre le mÃªme jour
        /// </summary>
        /// <param name="ut"></param>
        /// <param name="dteStart"></param>
        /// <param name="dteEnd"></param>
        /// <returns></returns>
        public static void FindWorkPeriod(Utilisateur ut, DateTime dte, out DateTime? start, out DateTime? end)
        {
            start = null;
            end = null;

            if (ut.DateFinContrat < dte) return;
            if (ut.horairesDeTravail == null)
                UtilisateursMgt.FillHoraireDeTravail(ut);
            if (ut.horairesreels == null)
                UtilisateursMgt.FillHoraireReel(ut);
            if (ut.horairesDeTravail.Count == 0) return;
            string ferielib = "";
            if (!IsNotFerie(dte, out ferielib)) return;
            int weekstrt = GetYearWeek(dte);
            bool IsInRealWeek = false;
            foreach (HoraireReel h in ut.horairesreels)
                if (h.WeekNum == weekstrt) IsInRealWeek = true;

            #region Absence car hors horaire de travail
#if TRACE
            Debug.Write("Id " + ut.Id + " Nom : " + ut.Nom + "Prenom : " + ut.Prenom);
#endif

            if (IsInRealWeek)
            {
                foreach (HoraireReel h in ut.horairesreels)
                {
                    if (weekstrt == h.WeekNum)
                    {
                        if ((((h.starttime.TimeOfDay <= dte.TimeOfDay) &&
                           (h.endtime.TimeOfDay >= dte.TimeOfDay))) &&
                           (((DayOfWeek)h.week_day) == dte.DayOfWeek) &&
                           dte.Year == h.Year)
                        {

                            start = dte.Date.Add(h.starttime.TimeOfDay);
                            end = dte.Date.Add(h.endtime.TimeOfDay);

                        }
                    }

                }

            }
            else
            {
                foreach (HorairesDeTravail h in ut.horairesDeTravail)
                {
                    if ((((h.starttime.TimeOfDay <= dte.TimeOfDay) &&
                        (h.endtime.TimeOfDay >= dte.TimeOfDay))) &&
                        (((DayOfWeek)h.week_day) == dte.DayOfWeek))
                    {


                        start = dte.Date.Add(h.starttime.TimeOfDay);
                        end = dte.Date.Add(h.endtime.TimeOfDay);

                        #region TypePeriodicity.JourParMois
                        if (h.period.tpeperiod == HoraireTrPeriodicity.TypePeriodicity.JourParMois)
                        {
                            int nbdays = 0;
                            if (h.period.MonthPeriodicityDay == dte.DayOfWeek)
                                for (int i = 0; i < dte.Day; i++)
                                    if (dte.AddDays(i).DayOfWeek == h.period.MonthPeriodicityDay) nbdays++;

                            if (nbdays != h.period.MonthPeriodicityNum)
                            {
                                start = null;
                                end = null;
                            }
                        }
                        #endregion

                        #region TypePeriodicity.XSemainesSur
                        if (h.period.tpeperiod == HoraireTrPeriodicity.TypePeriodicity.XSemainesSur)
                        {
                            int weekRef = GetYearWeek(h.period.FirstDate);
                            int weekAct = GetYearWeek(dte);
                            if (((weekAct - weekRef) % h.period.MonthPeriodicityNum) != 0)
                            {
                                start = null;
                                end = null;
                            }
                        }
                        #endregion

                    }
                }
            }

            #endregion
        }


        /// <summary>
        /// RÃ©cupere le nombre d'heures travaillÃ©s entre dteStart et dteEnd.
        /// dteStart et dteEnd doivent etre le mÃªme jour
        /// </summary>
        /// <param name="ut"></param>
        /// <param name="dteStart"></param>
        /// <param name="dteEnd"></param>
        /// <returns></returns>
        public static double IsOnWorkTime(Utilisateur ut, DateTime dteStart, DateTime dteEnd, bool NeedReel)
        {

            if (dteStart.Date != dteEnd.Date) throw new ArgumentException("dteStart doit etre le mÃªme jour que dteEnd");

            if (ut.DateFinContrat < dteStart) return 0;

            if (ut.horairesDeTravail == null)
                UtilisateursMgt.FillHoraireDeTravail(ut);
            if (ut.horairesDeTravail.Count == 0) return 0;


            if (ut.horairesreels == null)
                UtilisateursMgt.FillHoraireReel(ut);

            string ferielib = "";
            if (!IsNotFerie(dteStart, out ferielib)) return 0;
            long minutespresences = 0;
            int weekstrt = GetYearWeek(dteStart);
            bool IsInRealWeek = false;

            if (ut.horairesreels == null)
                UtilisateursMgt.FillHoraireReel(ut);

            foreach (HoraireReel h in ut.horairesreels)
                if (h.WeekNum == weekstrt) IsInRealWeek = true;

            #region Absence car hors horaire de travail
#if TRACE
            Debug.Write("Id " + ut.Id + " Nom : " + ut.Nom + "Prenom : " + ut.Prenom + " NeedReel " + NeedReel);
#endif

            if (NeedReel)
            {
                foreach (HoraireReel h in ut.horairesreels)
                {
                    if (weekstrt == h.WeekNum)
                    {
                        if ((((h.starttime.TimeOfDay <= dteStart.TimeOfDay) &&
                           (h.endtime.TimeOfDay >= dteStart.TimeOfDay)) ||
                           ((h.starttime.TimeOfDay <= dteEnd.TimeOfDay) &&
                           (h.endtime.TimeOfDay >= dteEnd.TimeOfDay)) ||
                           ((h.starttime.TimeOfDay >= dteStart.TimeOfDay) &&
                           (h.endtime.TimeOfDay <= dteEnd.TimeOfDay))) &&
                           (((DayOfWeek)h.week_day) == dteStart.DayOfWeek) &&
                           dteStart.Year == h.Year)
                        {
                            if (h.endtime > h.starttime) minutespresences += (long)(h.endtime - h.starttime).TotalMinutes;
#if TRACE
                            Debug.Write("Id " + ut.Id + " Nom : " + ut.Nom + "Prenom : " + ut.Prenom + " minutespresences1 :" + minutespresences);
#endif
                        }
                    }

                }
            }

            if ((!IsInRealWeek) && (!NeedReel))
            {
                foreach (HorairesDeTravail h in ut.horairesDeTravail)
                {
                    if ((((h.starttime.TimeOfDay <= dteStart.TimeOfDay) &&
                        (h.endtime.TimeOfDay >= dteStart.TimeOfDay)) ||
                        ((h.starttime.TimeOfDay <= dteEnd.TimeOfDay) &&
                        (h.endtime.TimeOfDay >= dteEnd.TimeOfDay)) ||
                        ((h.starttime.TimeOfDay >= dteStart.TimeOfDay) &&
                        (h.endtime.TimeOfDay <= dteEnd.TimeOfDay))) &&
                        (((DayOfWeek)h.week_day) == dteStart.DayOfWeek))
                    {


                        if (h.endtime > h.starttime) minutespresences += (long)(h.endtime - h.starttime).TotalMinutes;

                        #region TypePeriodicity.JourParMois
                        if (h.period.tpeperiod == HoraireTrPeriodicity.TypePeriodicity.JourParMois)
                        {
                            int nbdays = 0;
                            if (h.period.MonthPeriodicityDay == dteStart.DayOfWeek)
                                for (int i = 0; i < dteStart.Day; i++)
                                    if (dteStart.AddDays(i).DayOfWeek == h.period.MonthPeriodicityDay) nbdays++;

                            if (nbdays != h.period.MonthPeriodicityNum)
                            {
                                minutespresences = 0;
                            }
                        }
                        #endregion

                        #region TypePeriodicity.XSemainesSur
                        if (h.period.tpeperiod == HoraireTrPeriodicity.TypePeriodicity.XSemainesSur)
                        {
                            int weekRef = GetYearWeek(h.period.FirstDate);
                            int weekAct = GetYearWeek(dteStart);
                            if (((weekAct - weekRef) % h.period.MonthPeriodicityNum) != 0)
                            {
                                minutespresences = 0;
                            }
                        }
                        #endregion

                    }
                }
            }

            #endregion
#if TRACE
            Debug.Write("Id " + ut.Id + " Nom : " + ut.Nom + "Prenom : " + ut.Prenom + " minutespresences2 " + minutespresences);
#endif

            return minutespresences;
        }


        /// <summary>
        /// RÃ©cupere les horaires REELS de travail de la journÃ©e
        /// </summary>
        /// <param name="ut"></param>
        /// <param name="dteStart"></param>
        /// <param name="dteEnd"></param>
        /// <returns></returns>
        public static List<DateTime> WorkingReelHoursOfTheDays(Utilisateur ut, DateTime dte)
        {
            List<DateTime> lst = new List<DateTime>();

            if (ut.DateFinContrat < dte) return lst;

            if (ut.horairesDeTravail == null)
                UtilisateursMgt.FillHoraireDeTravail(ut);
                UtilisateursMgt.FillHoraireReel(ut);

            if (ut.horairesDeTravail.Count == 0) return lst;

            string ferielib = "";
            if (!IsNotFerie(dte, out ferielib)) return lst;
            int weekstrt = GetYearWeek(dte);

            ut.horairesreels.Sort(new HoraireReelTimeComparer());

         /*   ut.horairesreels.ForEach(w =>
            {
                if (w.WeekNum == weekstrt && w.week_day == (int)dte.DayOfWeek && dte.Year == w.Year)
                {
                    lst.Add(w.starttime);
                    lst.Add(w.endtime);
                }
            });*/
           foreach (HoraireReel h in ut.horairesreels)
            {
               if (weekstrt == h.WeekNum)
               {
                    if ((h.week_day == (int)dte.DayOfWeek) &&
                       dte.Year == h.Year)
                    {
                        lst.Add(h.starttime);
                        lst.Add(h.endtime);
                    }
                }

            }

            lst = lst.OrderBy(w => w.TimeOfDay).ToList();

            return lst;
        }
        public static List<HoraireReel> WorkingReelHoursOfTheDaysRee(Utilisateur ut, DateTime dte)
        {
            List<HoraireReel> lst = new List<HoraireReel>();

            if (ut.DateFinContrat < dte) return lst;

            if (ut.horairesDeTravail == null)
                UtilisateursMgt.FillHoraireDeTravail(ut);

            if (ut.horairesreels == null)
                UtilisateursMgt.FillHoraireReel(ut);


            if (ut.horairesDeTravail.Count == 0) return lst;

            string ferielib = "";
            if (!IsNotFerie(dte, out ferielib)) return lst;
            int weekstrt = GetYearWeek(dte);

            ut.horairesreels.Sort(new HoraireReelTimeComparer());

            foreach (HoraireReel h in ut.horairesreels)
            {
                if (weekstrt == h.WeekNum)
                {
                    if ((h.week_day == (int)dte.DayOfWeek) &&
                       dte.Year == h.Year)
                    {
                        lst.Add(h);

                    }
                }

            }



            return lst;
        }

        public static double GetNbJoursOutOfOffice(Utilisateur CurrentUtilisateur, List<DateTime> lstdtes, Status st)
        {
            double total = 0;
            string libferie;
            if (CurrentUtilisateur.status == null)
                CurrentUtilisateur.status = UtilisateursMgt.GetUtilisateursStatus(CurrentUtilisateur);

            foreach (UserStatus us in CurrentUtilisateur.status)
            {
                if (us.status.Id == st.Id)
                {
                    foreach (DateTime dte in lstdtes)
                    {
                        if ((dte.Date.AddHours(10) >= us.dateStart) && (dte.Date.AddHours(10) <= us.dateEnd))
                        {
                            if (IsNotFerie(dte.Date, out libferie) && IsAWorkDay(CurrentUtilisateur, dte.Date))
                                total += 0.5;
                        }

                        if ((dte.Date.AddHours(15) >= us.dateStart) && (dte.Date.AddHours(15) <= us.dateEnd))
                        {
                            if (IsNotFerie(dte.Date, out libferie) && IsAWorkDay(CurrentUtilisateur, dte.Date))
                                total += 0.5;
                        }
                    }
                }
            }

            return total;
        }

        public static long GetNbJoursOutOfOfficeEnMinutes(Utilisateur CurrentUtilisateur, DateTime startdte, DateTime enddte, Status st)
        {
            long total = 0;

            if (CurrentUtilisateur.status == null)
                CurrentUtilisateur.status = UtilisateursMgt.GetUtilisateursStatus(CurrentUtilisateur);

            foreach (UserStatus us in CurrentUtilisateur.status)
            {
                if (us.status.Id == st.Id)
                {
                    DateTime dteS = us.dateStart > startdte ? us.dateStart : startdte;
                    DateTime dteE = us.dateEnd < enddte ? us.dateEnd : enddte;

                    if (dteE > dteS)
                        total += (int)(dteE - dteS).TotalMinutes;
                }
            }

            return total;
        }


        public static TimeSpan GetRecup(Utilisateur ut, List<DateTime> lstdte)
        {
            TimeSpan TotalOfTotal = GetNbHeureTravaille(ut, lstdte).Subtract(GetNbHeureTravailleTheorique(ut, lstdte));

            return TotalOfTotal;
        }
        /*
        public static TimeSpan GetRecup(Utilisateur ut, DateTime startdte, DateTime enddte)
        {
            long Totalminutes = 0;
            DateTime dte = startdte.Date;
            

            while (dte <= enddte.Date)
            {
                bool CanCalculate = false;
                int weekstrt = GetYearWeek(dte);
                
                if (ut.SynchroPointeuse)
                {
                    foreach (HoraireReel h in ut.horairesDePointeuse)
                        if (h.WeekNum == weekstrt) CanCalculate = true;
                }
                else
                {
                    foreach (HoraireReel h in ut.horairesreels)
                        if (h.WeekNum == weekstrt) CanCalculate = true;
                }

                if (CanCalculate)
                {

                    DateTime returneddte;
                    if (!IsOnHoliday(ut, dte.Date.AddMinutes(1), dte.Date.AddHours(23), out returneddte))
                    {

                        if (ut.SynchroPointeuse)
                        {
                            foreach (HoraireReel h in ut.horairesDePointeuse)
                            {
                                if (weekstrt == h.WeekNum)
                                {
                                    if ((h.starttime.TimeOfDay >= dte.Date.TimeOfDay) &&
                                        (h.endtime.TimeOfDay <= dte.Date.AddHours(23).AddMinutes(59).TimeOfDay) &&
                                        (h.week_day == (int)dte.DayOfWeek) &&
                                        (dte.Year == h.Year))
                                    {
                                        if (h.endtime > h.starttime) Totalminutes += (long)(h.endtime - h.starttime).TotalMinutes;
                                    }
                                }

                            }
                        }
                        else
                        {
                            foreach (HoraireReel h in ut.horairesreels)
                            {
                                if (weekstrt == h.WeekNum)
                                {
                                    if ((h.starttime.TimeOfDay >= dte.Date.TimeOfDay) &&
                                        (h.endtime.TimeOfDay <= dte.Date.AddHours(23).AddMinutes(59).TimeOfDay) &&
                                        (h.week_day == (int)dte.DayOfWeek) &&
                                        (dte.Year == h.Year))
                                    {
                                        if (h.endtime > h.starttime) Totalminutes += (long)(h.endtime - h.starttime).TotalMinutes;
                                    }
                                }

                            }
                        }



                        foreach (HorairesDeTravail h in ut.horairesDeTravail)
                        {

                            if ((h.starttime.TimeOfDay >= dte.Date.TimeOfDay) &&
                                    (h.endtime.TimeOfDay <= dte.Date.AddHours(23).AddMinutes(59).TimeOfDay) &&
                                    (((DayOfWeek)h.week_day) == dte.DayOfWeek))
                            {
                                if (h.endtime > h.starttime) Totalminutes -= (long)(h.endtime - h.starttime).TotalMinutes;
                            }

                        }
                    }
                }
                dte = dte.Date.AddDays(1);
            }
            TimeSpan ret = DateTime.Now.AddMinutes(Totalminutes) - DateTime.Now;
            return ret;
        }
        */


        public static TimeSpan GetNbHeureTravaillePointe(Utilisateur ut, List<DateTime> dates)
        {
            long Totalminutes = 0;
            if (ut == null) return new TimeSpan();
            foreach (DateTime dte in dates)
            {

                Totalminutes += (long)NbHeuresPointees(ut, dte.Date.AddMinutes(5), dte.AddHours(23));

            }




            return new TimeSpan(((int)Totalminutes / 60), ((int)Totalminutes % 60), 0);

        }


        public static bool IsOnHoliday(BasCommon_BO.Utilisateur ut, DateTime dteStart, DateTime dteEnd, out DateTime returndate)
        {
            bool m_IsOnHoliday = false;
            returndate = DateTime.MinValue;
            //if (ut.Holidays == null)
            //    ut.Holidays = MgmtHoraire.GetHolidays(ut);
            if (ut.status == null)
                ut.status = GetUtilisateursStatus(ut);
            foreach (UserStatus h in ut.status)
            {
                if (h.status.Libelle.Trim() == "Congé payé")
                {
                    DateTime sd = h.dateStart.Date;
                    DateTime ed = h.dateEnd.Date.AddHours(23).AddMinutes(59);

                    if (((sd <= dteStart) && (ed >= dteStart)) ||
                        ((sd <= dteEnd) &&
                        (ed >= dteEnd)) ||
                        ((sd >= dteStart) &&
                        (ed <= dteEnd)))
                    {
                        m_IsOnHoliday = true;
                        returndate = h.dateEnd.Date.AddDays(1);
                    }
                }
            }

            /*     foreach (Holiday h in ut.Holidays)
                 {
                     DateTime sd = h.startdate.Date;
                     DateTime ed = h.enddate.Date.AddHours(23).AddMinutes(59);

                     if (((sd <= dteStart) &&
                         (ed >= dteStart)) ||
                         ((sd <= dteEnd) &&
                         (ed >= dteEnd)) ||
                         ((sd >= dteStart) &&
                         (ed <= dteEnd)))
                     {
                         m_IsOnHoliday = true;
                         returndate = h.enddate.Date.AddDays(1);
                     }
                 }*/
            return m_IsOnHoliday;


        }



        public static Holiday IsOnHoliday(BasCommon_BO.Utilisateur ut, DateTime dte)
        {
            if (ut.Holidays == null)
                ut.Holidays = MgmtHoraire.GetHolidays(ut);



            foreach (Holiday h in ut.Holidays)
            {
                if ((dte > h.startdate.Date) && (dte <= h.enddate.Date.AddDays(1)))
                    return h;
            }
            return null;


        }


        public static TimeSpan GetNbHeureTravailleTheorique(Utilisateur ut, List<DateTime> dates)
        {
            long Totalminutes = 0;
            if (ut == null) return new TimeSpan();
            foreach (DateTime dte in dates)
            {
                DateTime returneddte;
                if (!IsOnHoliday(ut, dte.Date.AddMinutes(5), dte.AddHours(23), out returneddte))
                {
                    UserStatus state;
                    if (!IsOutOfOffice(ut, dte.Date.AddMinutes(5), dte.AddHours(23), out state))
                    {
                        Totalminutes += (long)IsOnWorkTime(ut, dte.Date.AddMinutes(5), dte.AddHours(23), false);
                    }
                }

            }




            return new TimeSpan(((int)Totalminutes / 60), ((int)Totalminutes % 60), 0);

        }

        public static TimeSpan GetNbHeureTravaille(Utilisateur ut, List<DateTime> dates)
        {
            long Totalminutes = 0;
            if (ut == null) return new TimeSpan();
            foreach (DateTime dte in dates)
            {
                DateTime returneddte;
                if (!IsOnHoliday(ut, dte.Date.AddMinutes(5), dte.AddHours(23), out returneddte))
                {
                    UserStatus state;
                    if (!IsOutOfOffice(ut, dte.Date.AddMinutes(5), dte.AddHours(23), out state))
                    {
                        Totalminutes += (long)IsOnWorkTime(ut, dte.Date.AddMinutes(5), dte.AddHours(23), true);
                    }
                }

            }




            return new TimeSpan(((int)Totalminutes / 60), ((int)Totalminutes % 60), 0);

        }


        public static DateTime GetDateFromHoraireReel(HoraireReel hr)
        {
            /*
            DateTime dte = new DateTime(hr.Year, 1, 1);
            int CurrentWeek = 1;

            while (CurrentWeek != hr.WeekNum)
            {
                dte = dte.AddDays(1);
                CurrentWeek = GetYearWeek(dte);
            }
            dte = dte.AddDays(hr.week_day-1);
            return dte;
             * */

            return MgmtHoraire.GetStartDayWeek(hr.WeekNum, hr.Year, (int)hr.week_day - 1);
        }

        public static bool IsNotFerie(DateTime dtDate, out string libelle)
        {



            foreach (JourFerie jf in PresenceMgt.JourFerie)
            {
                if (dtDate.Date == jf.Dte.Date)
                {
                    libelle = jf.Libelle;
                    return false;
                }
            }
            libelle = (dtDate.DayOfWeek == DayOfWeek.Sunday) ? "Dimanche" : "";
            return (dtDate.DayOfWeek != DayOfWeek.Sunday);
        }

        /*
        public static void GetNbHeuresTravailleParAn(Utilisateur user, bool IncludeHolidays, bool IncludeJourFerie,out double heuresFait, out double heuresAFaire)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, 1, 1);
            heuresFait = 0;
            heuresAFaire = 0;
            double TotalFerie = 0;
            double TotalHolidays = 0;
            string msg;

            while (startDate.Year == DateTime.Now.Year)
            {
                UserStatus status;
                if (!Calendar.BL.UtilisateursMgt.IsOutOfOffice(user, startDate.Date, startDate.Date.AddHours(23).AddMinutes(59), out status))
                {
                    if (IncludeJourFerie && !IsNotFerie(startDate,out msg))
                        TotalFerie += Calendar.BL.UtilisateursMgt.IsOnWorkTime(user, startDate.Date, startDate.Date.AddHours(23).AddMinutes(59));
                    else
                    {
                        DateTime returndte;
                        if (IncludeHolidays && (IsOnHoliday(user, startDate.Date, startDate.Date.AddHours(23).AddMinutes(59), out returndte)))
                            TotalHolidays += Calendar.BL.UtilisateursMgt.IsOnWorkTime(user, startDate.Date, startDate.Date.AddHours(23).AddMinutes(59));
                        else
                        {
                            if (startDate < DateTime.Now)
                            {
                                heuresFait += Calendar.BL.UtilisateursMgt.IsOnWorkTime(user, startDate.Date, startDate.Date.AddHours(23).AddMinutes(59));
                            }
                            else
                            {
                                heuresAFaire += Calendar.BL.UtilisateursMgt.IsOnWorkTime(user, startDate.Date, startDate.Date.AddHours(23).AddMinutes(59));
                            }
                        }
                    }
                }

                startDate = startDate.AddDays(1);
            }

            



        }

        */
        public static double GetNbHeuresTravailleParSemaine(Utilisateur user)
        {
            if (user.horairesDeTravail == null)
                UtilisateursMgt.FillHoraireDeTravail(user);

            double count = 0;
            foreach (HorairesDeTravail app in user.horairesDeTravail)
            {
                double nbHour = (app.endtime - app.starttime).TotalHours;

                if (app.period.tpeperiod == HoraireTrPeriodicity.TypePeriodicity.JourParMois)
                    if (app.period.MonthPeriodicityNum > 0)
                        nbHour /= 4.33f;
                     //   nbHour /= 4.53f;


                if (app.period.tpeperiod == HoraireTrPeriodicity.TypePeriodicity.XSemainesSur)
                    nbHour /= app.period.MonthPeriodicityNum;

                count += nbHour;

            }



            return count;

            /*
            int NbSemaine = GetYearWeek(new DateTime(DateTime.Now.Year,12,31));
            DateTime startDate = new DateTime(DateTime.Now.Year, 1, 1);
            double Total = 0;
            int week = 0;
            if (startDate.DayOfWeek == DayOfWeek.Monday || startDate.DayOfWeek == DayOfWeek.Tuesday || startDate.DayOfWeek == DayOfWeek.Wednesday || startDate.DayOfWeek == DayOfWeek.Thursday)
                week++;

            while (startDate.DayOfWeek != DayOfWeek.Monday)
                startDate = startDate.AddDays(1);

            while (week < NbSemaine)
            {
                if (startDate.DayOfWeek == DayOfWeek.Sunday) week++;

                Total += Calendar.BL.UtilisateursMgt.IsOnWorkTime(user, startDate.Date, startDate.Date.AddHours(23).AddMinutes(59), false);
                

                startDate = startDate.AddDays(1);
            }


            return (Total/60) / NbSemaine;
            */
        }

        #endregion


        public static List<Utilisateur> getUtilisateurInFauteuil(Fauteuil f, DateTime dte)
        {

            List<Utilisateur> res = new List<Utilisateur>();
            if (f == null) return res;


            foreach (AffectationFauteuil af in AffectationFauteuilOfTheDayCached)
                if ((af.fauteuil == f) && (af.DteFrom < dte) && (af.DteTo > dte) && (af.user != null))
                    res.Add(af.user);


            return res;


        }

        public static List<UserStatus> GetUtilisateursStatus(Utilisateur p_utilisateur) {

            List<UserStatus> liste = new List<UserStatus>();
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray("/AllUserStatusByIdUser/" + p_utilisateur.Id);

            foreach (JObject obj in jArray)
                
                liste.Add(Builders.BuildUtilisateur.BuildUserStatus(obj,p_utilisateur));

            p_utilisateur.status = liste;

            return liste;            
        }


        public static List<UserStatus> GetUtilisateursStatusOld(Utilisateur p_utilisateur)
        {
            List<UserStatus> lst = new List<UserStatus>();
            DataTable dt = DAC.getUtilisateursStatus(p_utilisateur);

            foreach (DataRow r in dt.Rows)
            {
                //lst.Add(Builders.BuildUtilisateur.BuildUserStatus(r, p_utilisateur));
            }

            p_utilisateur.status = lst;
            return lst;
        }

        public static List<AffectedUtilisateurs> getAffectedUser(DateTime Dte)
        {
            List<AffectedUtilisateurs> lst = new List<AffectedUtilisateurs>();
            DataTable dt = DAC.getAffectedUser(Dte);
            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildUtilisateur.BuildAffectedUtilisateur(r, Dte));
            }
            return lst;
        }




    }
}
