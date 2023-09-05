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



namespace BasCommon_BL
{
    public class UtilisateursMgt
    {
        private static List<Utilisateur> _utilisateurs;
        public static List<Utilisateur> utilisateurs
        {
            get
            {
                if (_utilisateurs != null)
                    return _utilisateurs;
                else
                    return getUtilisateurs();
            }
            
        }

        public static List<Utilisateur> Assistantes
        {
            get
            {
                List<Utilisateur> lst = new List<Utilisateur>();

                foreach (Utilisateur u in UtilisateursMgt.utilisateurs)
                    if (u.Actif)
                    {
                        if (u.Fonction != "Praticien")
                            lst.Add(u);
                    }

                return lst;
            }

        }

        public static List<Utilisateur> Praticiens
        {
            get
            {
                List<Utilisateur> lst = new List<Utilisateur>();

                foreach (Utilisateur u in UtilisateursMgt.utilisateurs)
                    if (u.Actif)
                    {
                        if (u.Fonction == "Praticien")
                            lst.Add(u);
                    }

                return lst;
            }

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
                }
            }
        }

        public static void CopyHoraireDeTravailToReel(Utilisateur ut,int WeekNum, int Annee)
        {

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
        }

        private static List<Utilisateur> getUtilisateurs()
        {
            _utilisateurs = UtilisateursMgt.utilisateurs;


            foreach (Utilisateur u in _utilisateurs)
                DAC.getHorairesDeTravail(u);

            foreach (Utilisateur u in _utilisateurs)
                DAC.getHorairesReel(u);

            foreach (Utilisateur u in _utilisateurs)
                DAC.getHolidays(u);

            foreach (Utilisateur u in _utilisateurs)
                DAC.getUtilisateursStatus(u);

            foreach (Utilisateur u in _utilisateurs)
                DAC.GetNbJoursDeConges(u);

            return _utilisateurs;
        }


        public static void DeleteUtilisateur(Utilisateur u)
        {
            DAC.DelUtilisateur(u);
        }
        
        public static Utilisateur getUtilisateur(int Id)
        {
            foreach (Utilisateur ut in utilisateurs)
                if (ut.Id == Id) return ut;

            return null;
        }

        public static Utilisateur getUtilisateur(string nom)
        {
            foreach (Utilisateur ut in utilisateurs)
                if (ut.Nom.ToUpper() == nom.ToUpper()) return ut;

            return null;
        }

        #region Gestion des absences

        public static bool UserIsPresent(Utilisateur ut)
        {
            string Comment = "";
            UserStatus us;
            return UserIsPresent(ut, DateTime.Now, DateTime.Now.AddMinutes(5), out Comment, out us);
        }

        public static bool UserIsPresent(Utilisateur ut,DateTime dte)
        {
            string Comment = "";
            UserStatus us;
            return UserIsPresent(ut, dte, dte.AddMinutes(5), out Comment, out us);
        }

        public static bool UserIsPresent(Utilisateur ut, DateTime dteStart, DateTime dteEnd, out UserStatus status)
        {
            string Comment = "";
            return UserIsPresent(ut, dteStart, dteEnd, out Comment,out status);
        }

        public static bool UserIsPresent(Utilisateur ut, out string Comment, out UserStatus status)
        {
            return UserIsPresent(ut, DateTime.Now, out Comment, out status);
        }

        public static bool UserIsPresent(Utilisateur ut,DateTime dte, out string Comment, out UserStatus status)
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

                m_IsPresent = !IsOnHoliday(ut, dteStart, dteEnd, out returnedDate);
#if TRACE
                Debug.Write("IsOnHoliday :" + ut.Id + " " + ut.Nom + " " + ut.Prenom + " m_IsPresent IsOnHoliday : " + m_IsPresent);
#endif
                if (m_IsPresent)
                {
                    m_IsPresent = (IsOnWorkTime(ut, dteStart, dteEnd, true) != 0);
#if TRACE
                    Debug.Write("IsOnWorkTime :" + ut.Id + " " + ut.Nom + " " + ut.Prenom + " m_IsPresent IsOnWorkTime : " + m_IsPresent);
#endif
#if TRACE
                    if (m_IsPresent)
                        Debug.Write("Présent ");
                    else
                        Debug.Write("Absent ");
#endif


                    if (m_IsPresent)
                    {

                        return true;
                    }
                    else
                    {
                        
                        if ((status != null) && (status.status.IsAnAbsence))
                            Comment = status.status .Libelle;
                        else
                            Comment = "Absent";
                    }
                }
                else
                {
                    int it = 0;
                    while (!IsAWorkDay(ut, returnedDate) && (it < 10))
                    {
                        returnedDate = returnedDate.AddDays(1);
                        it++;
                    }
                    Comment = "En congé. retour le " + returnedDate.ToString("dd/MM/yyyy");
                }
            }
            else
            {

                if (status!=null) 
                    Comment = status.ShortDisplay;
            }
                
            return m_IsPresent;
        }
                
        public static void Update(Utilisateur ut)
        {
            DAC.UpdateUtilisateur(ut);
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

        public static void AddStatus(Utilisateur ut,UserStatus st)
        {
            ut.status.Add(st);
            if (!DAC.IsExistStatus(ut,st))
                DAC.AddStatus(ut, st);
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


        public static int GetNbJoursDeConges(Utilisateur ut,DateTime dteStart,DateTime dteEnd)
        {
            int NbJours = 0;
            string libferie;
            foreach (Holiday h in ut.Holidays)
            {
                DateTime dte = h.startdate.Date;
                while (dte < h.enddate.AddDays(1).Date)
                {
                    UserStatus stat;
                    if ( IsNotFerie(dte.Date,out libferie) && IsAWorkDay(ut, dte.Date) &&
                        (!IsOutOfOffice(ut, dte.Date.AddHours(6), dte.Date.AddHours(23), out stat)) &&
                        (dteStart<=dte) && (dteEnd>=dte))
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


        public static void UpdateHolidays( Holiday holiday)
        {
            DAC.updateHoliday(holiday);
        }

        public static void AddHolidays(Utilisateur ut,Holiday holiday)
        {
            ut.Holidays.Add(holiday);
            DAC.AddHoliday(ut, holiday);
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
                EndDateOfCurrentYear = new DateTime(DateTime.Now.Year , 5, 31); //31 Mai N
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
            return Math.Round(NbMois * 2.08);
            
           // return Math.Round((DateTime.Now.Month-StartDte.Month) * 2.08);
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
            return Math.Round(NbMois * 2.08);

            // return Math.Round((DateTime.Now.Month-StartDte.Month) * 2.08);
        }

        public static int GetNbJoursDeCongesPris(Utilisateur ut)
        {

            DateTime StartDateOfCurrentYear;
            DateTime EndDateOfCurrentYear;
            if (DateTime.Now > new DateTime(DateTime.Now.Year, 5, 1))
            {
                StartDateOfCurrentYear = new DateTime(DateTime.Now.Year, 5, 1); //1 Mai N-1
                EndDateOfCurrentYear = new DateTime(DateTime.Now.Year + 1, 4, 30); //30 Avril N-1
            }
            else
            {
                StartDateOfCurrentYear = new DateTime(DateTime.Now.Year -1, 5, 1); //1 Mai N
                EndDateOfCurrentYear = new DateTime(DateTime.Now.Year, 4, 30); //30 Avril N
            }



            int NbJours = 0;
            foreach (Holiday h in ut.Holidays)
            {
                DateTime dte = h.startdate.Date;
                string libferie;
                while (dte <= h.enddate.Date)
                {

                    UserStatus stat;
                    if (IsNotFerie(dte.Date, out libferie) && IsAWorkDay(ut, dte.Date) &&
                        (!IsOutOfOffice(ut, dte.Date.AddHours(6), dte.Date.AddHours(23), out stat)) &&
                        (StartDateOfCurrentYear < dte.Date) &&
                        (EndDateOfCurrentYear > dte.Date) &&
                        (DateTime.Now.Date > dte.Date))
                        NbJours++;
                    dte = dte.AddDays(1);
                }
            }

            return NbJours;

        }

        public static bool IsOutOfOffice(Utilisateur ut, DateTime dteStart, DateTime dteEnd, out UserStatus status)
        {
            bool m_IsOutOfOffice = false;
            status = null;
            if (ut.status == null) return false;

            if (!ut.Actif)
            {
                return true;
            }

            foreach (UserStatus h in ut.status)
            {
                //Si la personne estpresente une fraction du temp testé, alors elle est presente
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

        private static bool IsAWorkDay(Utilisateur ut, DateTime dte)
        {
            bool ispresent = false;
            bool IsInRealWeek = false;

            DateTime dteStart = dte.Date;
            DateTime dteEnd = dte.Date.AddHours(23);

            if (ut.horairesDeTravail == null) return false;

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
        /// Récupere le nombre d'heures travaillés entre dteStart et dteEnd.
        /// dteStart et dteEnd doivent etre le même jour
        /// </summary>
        /// <param name="ut"></param>
        /// <param name="dteStart"></param>
        /// <param name="dteEnd"></param>
        /// <returns></returns>
        public static double NbHeuresPointees(Utilisateur ut, DateTime dteStart, DateTime dteEnd)
        {
            if (!ut.SynchroPointeuse) return -1;
            if (ut.horairesDeTravail == null) return 0;
            if (dteStart.Date != dteEnd.Date) throw new ArgumentException("dteStart doit etre le même jour que dteEnd");
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
        /// Récupere le nombre d'heures travaillés entre dteStart et dteEnd.
        /// dteStart et dteEnd doivent etre le même jour
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
            if (ut.horairesDeTravail == null) return;
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
        /// Récupere le nombre d'heures travaillés entre dteStart et dteEnd.
        /// dteStart et dteEnd doivent etre le même jour
        /// </summary>
        /// <param name="ut"></param>
        /// <param name="dteStart"></param>
        /// <param name="dteEnd"></param>
        /// <returns></returns>
        public static double IsOnWorkTime(Utilisateur ut, DateTime dteStart, DateTime dteEnd, bool NeedReel)
        {

            if (dteStart.Date != dteEnd.Date) throw new ArgumentException("dteStart doit etre le même jour que dteEnd");

            if (ut.DateFinContrat < dteStart) return 0;
            if (ut.horairesDeTravail == null) return 0;
            string ferielib = "";
            if (!IsNotFerie(dteStart,out ferielib)) return 0;
            long minutespresences = 0;
            int weekstrt = GetYearWeek(dteStart);
            bool IsInRealWeek = false;
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

            if ((!IsInRealWeek)&&(!NeedReel))
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
        /// Récupere les horaires REELS de travail de la journée
        /// </summary>
        /// <param name="ut"></param>
        /// <param name="dteStart"></param>
        /// <param name="dteEnd"></param>
        /// <returns></returns>
        public static List<DateTime> WorkingReelHoursOfTheDays(Utilisateur ut, DateTime dte)
        {
            List<DateTime> lst = new List<DateTime>();

            if (ut.DateFinContrat < dte) return lst;
            if (ut.horairesDeTravail == null) return lst;
            string ferielib = "";
            if (!IsNotFerie(dte, out ferielib)) return lst;
            int weekstrt = GetYearWeek(dte);


            
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



                lst.Sort();

            return lst;
        }


        public static double GetNbJoursOutOfOffice(Utilisateur  CurrentUtilisateur,List<DateTime> lstdtes,Status st)
        {
            double total = 0;
            string libferie;
            foreach (UserStatus us in CurrentUtilisateur.status)
            {
                if (us.status.Id == st.Id)
                {
                    foreach (DateTime dte in lstdtes)
                    {
                        if ((dte.Date.AddHours(10) >= us.dateStart) && (dte.Date.AddHours(10) <= us.dateEnd))
                        {
                            if (IsNotFerie(dte.Date, out libferie) && IsAWorkDay(CurrentUtilisateur, dte.Date))
                                total+=0.5;
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

        public static long GetNbJoursOutOfOfficeEnMinutes(Utilisateur CurrentUtilisateur, DateTime startdte,DateTime enddte, Status st)
        {
            long total = 0;
            foreach (UserStatus us in CurrentUtilisateur.status)
            {
                if (us.status.Id == st.Id)
                {
                    DateTime dteS = us.dateStart > startdte ? us.dateStart : startdte;
                    DateTime dteE = us.dateEnd < enddte ? us.dateEnd : enddte;

                    if (dteE>dteS)
                        total += (int)(dteE - dteS).TotalMinutes;                    
                }
            }

            return total;
        }


        public static TimeSpan GetRecup(Utilisateur ut, List<DateTime> lstdte)
        {
            TimeSpan TotalOfTotal = GetNbHeureTravaille(ut,lstdte).Subtract(GetNbHeureTravailleTheorique(ut,lstdte));

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


        public static bool IsOnHoliday(Utilisateur ut, DateTime dteStart, DateTime dteEnd, out DateTime returndate)
        {
            bool m_IsOnHoliday = false;
            returndate = DateTime.MinValue;
            if (ut.Holidays == null) return false;



            foreach (Holiday h in ut.Holidays)
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
            }
            return m_IsOnHoliday;


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
            DateTime dte = new DateTime(hr.Year, 1, 1);
            int CurrentWeek = 1;

            while (CurrentWeek != hr.WeekNum)
            {
                dte = dte.AddDays(1);
                CurrentWeek = GetYearWeek(dte);
            }
            dte = dte.AddDays(hr.week_day-1);
            return dte;
        }

        public static bool IsNotFerie(DateTime dtDate,out string libelle)
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

            double count = 0;
            foreach (HorairesDeTravail app in user.horairesDeTravail)
            {
                double nbHour = (app.endtime - app.starttime).TotalHours;

                if (app.period.tpeperiod == HoraireTrPeriodicity.TypePeriodicity.JourParMois)
                    if (app.period.MonthPeriodicityNum > 0)
                        nbHour /= 4.33f;

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

            DataTable dt = DAC.getUtilisateursInFauteuil(f, dte);


            foreach (DataRow r in dt.Rows)
                foreach (Utilisateur u in utilisateurs)
                    if (Convert.ToInt32(r["ID_USER"]) == u.Id)
                        res.Add(u);

            return res;
        }


        public static List<UserStatus> GetUtilisateursStatus(Utilisateur p_utilisateur)
        {
            List<UserStatus> lst = new List<UserStatus>();
            DataTable dt = DAC.getUtilisateursStatus(p_utilisateur);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildUtilisateur.BuildUserStatus(r, p_utilisateur));
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
