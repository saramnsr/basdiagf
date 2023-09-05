using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;
using BasCommon_DAL;
using System.IO;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL
{
    public static class AppointementsMgmt
    {
        public static Utilisateur lastUser = null;

        public static void NetWorkSend(DateTime dteToBroadcast)
        {
            IPEndPoint client = new IPEndPoint(new IPAddress(new byte[] { 255, 255, 255, 255 }), 9001);


            UdpClient server = new UdpClient();
            server.EnableBroadcast = true;


            MemoryStream ms = new MemoryStream();
            byte[] _data = System.BitConverter.GetBytes(dteToBroadcast.ToBinary());
            server.Connect(client);

            server.Send(_data, _data.Length);

            server.Close();
        }

        public static RHAppointment getAppointment(int idRdv) {

            JObject jObject = BasCommon_DAL.DAC.getMethodeJsonObjet("/Appointement/"+idRdv);
            if (jObject != null)
                return Builders.BuildAppointement.BuildRHAppointment(jObject);
            return null;
        }


        public static RHAppointment getAppointmentOld(int IdRDV)
        {

            List<RHAppointment> lst = new List<RHAppointment>();
            DataRow r = DAC.getAppointment(IdRDV);
            if (r == null) return null;
            return Builders.BuildAppointement.BuildRHAppointment(r);
        }
       
        public static bool VerifNewValidAppointment(RHAppointment app)
        {

            return DAC.VerifNewValidAppointment(app);
          
        }
       
        public static RHAppointment VerifNewValidAppointment(DateTime startDate,DateTime endDate,int idFaut)
        {
            DataRow r = DAC.VerifNewValidAppointment(startDate, endDate, idFaut);

            if (r == null) return null;
            return Builders.BuildAppointement.BuildFullRHAppointment(r);


        }

        public static List<RHAppointment> getAppointments(basePatient p_patient, DateTime Dte)
        {

            List<RHAppointment> lst = new List<RHAppointment>();
            JArray json = DAC.PostingToPKFAndDecompress("","/AppointementByDate/"+Dte+" 00:00:00&"+Dte+" 23:59:59/"+p_patient.Id);
            foreach (JObject r in json)
            {
                RHAppointment app = Builders.BuildAppointement.BuildFullRHAppointmentJ(r);
                app.patient = p_patient;
                lst.Add(app);
            }

            return lst;
        }

        public static List<RHAppointment> getAppointments(basePatient p_patient)
        {

            List<RHAppointment> lst = new List<RHAppointment>();
            JArray json = DAC.getMethodeJsonArray("/getAppointments/" + p_patient.Id);
            foreach (JObject r in json)
            {
                RHAppointment app = Builders.BuildAppointement.BuildFullRHAppointmentJ(r);
                app.patient = p_patient;
                lst.Add(app);
            }

            return lst;
        }

        public static List<RHAppointment> getAppointmentsByDate(basePatient p_patient, DateTime Dte)
        {

            List<RHAppointment> lst = new List<RHAppointment>();
            JArray json = DAC.getMethodeJsonArray("/AppointementByDate/" + Dte + " 00:00:00&" + Dte + " 23:59:59/" + p_patient.Id);
            foreach (JObject r in json)
            {
                RHAppointment app = Builders.BuildAppointement.BuildFullRHAppointmentJ(r);
                app.patient = p_patient;
                lst.Add(app);
            }

            return lst;
        }

        public static List<RHAppointment> getAppointmentsByDate(basePatient p_patient, DateTime Dte, int idRDV)
        {

            List<RHAppointment> lst = new List<RHAppointment>();
            JArray json = DAC.getMethodeJsonArray("/AppointementByDate/" + Dte + " 00:00:00&" + Dte + " 23:59:59/" + p_patient.Id + "/" + idRDV);
            foreach (JObject r in json)
            {
                RHAppointment app = Builders.BuildAppointement.BuildFullRHAppointmentJ(r);
                app.patient = p_patient;
                lst.Add(app);
            }

            return lst;
        }

        public static List<RHAppointment> getAppointments(int IdPatient)
        {

            List<RHAppointment> lst = new List<RHAppointment>();
            JArray json = DAC.getMethodeJsonArray("/getAppointments/"+IdPatient);
          
            foreach (JObject r in json)
            {
                RHAppointment app = Builders.BuildAppointement.BuildFullRHAppointmentJ(r);
                lst.Add(app);
            }

             return lst;
        }

        public static List<RHAppointment> getAppointmentsByDate(int IdPatient, DateTime Dte)
        {
            List<RHAppointment> lst = new List<RHAppointment>();
            //JArray json = DAC.PostingToPKFAndDecompress("","/AppointementByDate/" + Dte.Year + "-" + Dte.Month + "-" + Dte.Day + " 00:00:00&" + Dte.Year + "-" + Dte.Month + "-" + Dte.Day + " 23:59:59/" + IdPatient);
            JArray json = DAC.getMethodeJsonArray("/AppointementByDate/" + Dte.Year + "-" + Dte.Month + "-" + Dte.Day + " 00:00:00&" + Dte.Year + "-" + Dte.Month + "-" + Dte.Day + " 23:59:59/" + IdPatient);
            foreach (JObject r in json)
            {
                RHAppointment app = Builders.BuildAppointement.BuildFullRHAppointmentJ(r);
                lst.Add(app);
            }
            return lst;
        }

        public static List<RHAppointment> getAppointmentsByDate(int IdPatient, DateTime Dte, int idRDV)
        {
            List<RHAppointment> lst = new List<RHAppointment>();
            JArray json = DAC.getMethodeJsonArray("/AppointementByDate/" + Dte.Year + "-" + Dte.Month + "-" + Dte.Day + " 00:00:00&" + Dte.Year + "-" + Dte.Month + "-" + Dte.Day + " 23:59:59/" + IdPatient + "/" + idRDV);
            //JArray json = DAC.PostingToPKFAndDecompress("", "/AppointementByDate/" + Dte.Year + "-" + Dte.Month + "-" + Dte.Day + " 00:00:00&" + Dte.Year + "-" + Dte.Month + "-" + Dte.Day + " 23:59:59/" + IdPatient + "/" + idRDV);
            foreach (JObject r in json)
            {
                RHAppointment app = Builders.BuildAppointement.BuildFullRHAppointmentJ(r);
                lst.Add(app);
            }
            return lst;
        }

        public static string getTokenDevice(int IdPatient)
        {

            JObject json = DAC.getMethodeJsonObjet("/notificationsByIdPatient/" + IdPatient);
            if (json == null) return "";

            return json["token"].ToString();
        }
        public static List<RHAppointment> getAppointmentsOLD(int IdPatient)
        {


            List<RHAppointment> lst = new List<RHAppointment>();
            DataTable dt = DAC.getAppointments(IdPatient);

            foreach (DataRow r in dt.Rows)
            {
                RHAppointment app = Builders.BuildAppointement.BuildRHAppointment(r);
                lst.Add(app);
            }


            return lst;
        }


        public static List<RHAppointment> getAppointmentsOLD(basePatient p_patient)
        {


            List<RHAppointment> lst = new List<RHAppointment>();
            DataTable dt = DAC.getAppointments(p_patient);

            foreach (DataRow r in dt.Rows)
            {
                RHAppointment app = Builders.BuildAppointement.BuildRHAppointment(r);
                lst.Add(app);
            }


            return lst;
        }

        public static List<TraceAppointment> GetTraceAppointements(RHAppointment app, List<Acte> p_Actes)
        {
            DataTable dt = DAC.getRDVTrace(app, p_Actes);
            List<TraceAppointment> lst = new List<TraceAppointment>();

            foreach (DataRow r in dt.Rows)
	        {
		        lst.Add(Builders.BuildAppointement.BuildTraceAppointment(r, p_Actes));
	        }

            return lst;
        }


        public static void SetAAvancerFlagOnly(RHAppointment appointment)
        {
            DAC.SetAAvancerFlagOnly(appointment);
        }       
        public static List<RHAppointment> getCurrentAppointments()
        {

            JArray json = DAC.getMethodeJsonArray("/CurrentAppointments");
            List<RHAppointment> lst = new List<RHAppointment>();

            foreach (JObject r in json)
            {
                RHAppointment app = Builders.BuildAppointement.BuildFullRHAppointmentJ(r);
                lst.Add(app);
            }


            return lst;
        }
        public static List<RHAppointment> getCurrentAppointmentsOLD()
        {

            List<RHAppointment> lst = new List<RHAppointment>();
            DataTable dt = DAC.getCurrentAppointments();

            foreach (DataRow r in dt.Rows)
            {
                RHAppointment app = Builders.BuildAppointement.BuildFullRHAppointment(r);
                lst.Add(app);
            }


            return lst;
        }

        public static List<RHAppointment> getCurrentAppointments(basePatient pat)
        {

            List<RHAppointment> lst = new List<RHAppointment>();
            DataTable dt = DAC.getCurrentAppointments(pat.Id);

            foreach (DataRow r in dt.Rows)
            {
                RHAppointment app = Builders.BuildAppointement.BuildFullRHAppointment(r);
                lst.Add(app);
            }


            return lst;
        }

        public static int CheckAppointmentsupp(RHAppointment appointement)
        {
            return DAC.CheckAppointmentsupp(appointement);
        }


        public static string GetStrActionTrace(RHAppointment Previous, RHAppointment Current)
        {
            string Resultat = "";
            if (Previous == null)
            {
                Resultat += "Création du Rendez-vous";
                return Resultat;
            }
            if (Previous.DateFauteuil != Current.DateFauteuil) Resultat += " Status 'au fauteuil' ";
            if (Previous.DateSortie != Current.DateSortie) Resultat += " Status 'Sortie' ";
            if (Previous.DateSecretariat != Current.DateSecretariat) Resultat += " Status 'Secretariat' ";
            if (Previous.DateArrive != Current.DateArrive) Resultat += " Status 'Arrivée' ";
            if (Previous.StartDate != Current.StartDate) Resultat += " Déplacement de la date du RDV ";
            if (Previous.Duree != Current.Duree) Resultat += " Modification de la durée du RDV ";
            if (Previous.Resource != Current.Resource) Resultat += " Changement de Fauteuil ";
            if (Previous.Comment != Current.Comment) Resultat += " Changement du commentaire ";
            if (Previous.acte != Current.acte) Resultat += " Changement de l'acte ";
            if (Previous.IdPatient != Current.IdPatient) Resultat += " Changement du patient ";
            return Resultat;
        }

        public static void UpdateAppointment(RHAppointment app, Utilisateur modifiePar, RHAppointment.AnnulerPar Annulation)
        {
            UpdateAppointment(app, modifiePar);
        }

        public static void UpdateAppointment(RHAppointment app, Utilisateur modifiePar)
        {
            if (modifiePar!=null)lastUser = modifiePar;
            if (modifiePar!=null) DAC.InsertRDVTrace("Update", app, modifiePar);
            if (app.Localisation == RHAppointment.EnumLocalisation.Sortie) DAC.UpdateDatePoseLaboRequest(app.patient.Id);
            DAC.UpdateAppointment(app);            
            NetWorkSend(app.StartDate.Date);
        }


        public static void UpdateAppointmentArrive(RHAppointment app, Utilisateur modifiePar, RHAppointment.AnnulerPar Annulation)
        {
            UpdateAppointmentArrive(app, modifiePar);
        }
        
        public static void UpdateAppointmentArrive(RHAppointment app, Utilisateur modifiePar)
        {
            if (modifiePar != null) lastUser = modifiePar;
            if (app.Localisation == RHAppointment.EnumLocalisation.Sortie) DAC.UpdateDatePoseLaboRequest(app.patient.Id);
            DAC.UpdateHeureArriveAppointment(app);
            if (modifiePar != null) DAC.InsertRDVTrace("Update", app, modifiePar);
            NetWorkSend(app.StartDate.Date);
        }

        public static void UpdateAppointmentAuFauteuil(RHAppointment app, Utilisateur modifiePar, RHAppointment.AnnulerPar Annulation)
        {
            UpdateAppointmentAuFauteuil(app, modifiePar);
        }

        public static void UpdateAppointmentAuFauteuil(RHAppointment app, Utilisateur modifiePar)
        {
            if (modifiePar != null) lastUser = modifiePar;
            if ((app.patient != null ) && (app.Localisation == RHAppointment.EnumLocalisation.Sortie)) DAC.UpdateDatePoseLaboRequest(app.patient.Id);
            DAC.UpdateHeureFauteuilAppointment(app);
            if (modifiePar != null) DAC.InsertRDVTrace("Update", app, modifiePar);
            NetWorkSend(app.StartDate.Date);
        }


        public static void UpdateAppointmentAuSecretariat(RHAppointment app, Utilisateur modifiePar, RHAppointment.AnnulerPar Annulation)
        {
            UpdateAppointmentAuSecretariat(app, modifiePar);
        }

        public static void UpdateAppointmentAuSecretariat(RHAppointment app, Utilisateur modifiePar)
        {
            if (modifiePar != null) lastUser = modifiePar;
            if (app.Localisation == RHAppointment.EnumLocalisation.Sortie) DAC.UpdateDatePoseLaboRequest(app.patient.Id);
            DAC.UpdateHeureSecretariatAppointment(app);
            if (modifiePar != null) DAC.InsertRDVTrace("Update", app, modifiePar);
            NetWorkSend(app.StartDate.Date);
        }

        public static void UpdateAppointmentSortie(RHAppointment app, Utilisateur modifiePar, RHAppointment.AnnulerPar Annulation)
        {
            UpdateAppointmentSortie(app, modifiePar);
        }

        public static void UpdateAppointmentSortie(RHAppointment app, Utilisateur modifiePar)
        {
            if (modifiePar != null) lastUser = modifiePar;
            if (app.Localisation == RHAppointment.EnumLocalisation.Sortie) DAC.UpdateDatePoseLaboRequest(app.patient.Id);
            DAC.UpdateHeureSortieAppointment(app);
            if (modifiePar != null) DAC.InsertRDVTrace("Update", app, modifiePar);
            NetWorkSend(app.StartDate.Date);
        }


        public static void AbsenceAuto(DateTime ChkDte)
        {
            DAC.UpdateAbsenceAuto(ChkDte,false);
            NetWorkSend(ChkDte);
        }

        public static void AbsenceAuto()
        {
            DAC.UpdateAbsenceAuto(DateTime.Now, true);
            NetWorkSend(DateTime.Now);
        }


        public static RHAppointment AddRadioLike(RHAppointment app, Utilisateur modifiePar)
        {

            RHAppointment radApp = new RHAppointment();
            radApp.acte = ActesMgmt.ActeRadio;
            radApp.StartDate = app.StartDate;
            radApp.EndDate = app.StartDate.AddMinutes(radApp.acte.acte_durestd);
            radApp.patient = app.patient;
            radApp.Resource = Fauteuilsmgt.FauteuilRadio;
            radApp.Title = radApp.patient.ToShortString();


            if (modifiePar != null) lastUser = modifiePar;
            DAC.InsertAppointment(radApp);
            DAC.InsertRDVTrace("Insert", radApp, modifiePar);

            return radApp;
        }

        public static void AddAppointment(RHAppointment app, Utilisateur modifiePar)
        {
            if (modifiePar != null) lastUser = modifiePar;
            DAC.InsertAppointment(app);
            DAC.InsertRDVTrace("Insert", app, modifiePar);
            NetWorkSend(app.StartDate.Date);
            if (app.CommCl != null)
            {
                app.CommCl.Appointement = app;
                MgmtCommentairesFaitAFaire.updateCommCliniqueIdRDV(app.CommCl);
            }
        }



        public static byte[] BuildTimeSlotArray(List<RHAppointment> lstApp, int TimeSlotLength, baseSmallPersonne PatientToExclude, DateTime minDate, DateTime maxDate, Fauteuil f)
        {

            

            TimeSpan tsp = maxDate - minDate;
            int nbslot = (int)tsp.TotalMinutes / TimeSlotLength;

            byte[] CurrentDispo = new byte[nbslot];

            for (int i = 0; i < CurrentDispo.Length; i++)
            {
                DateTime dte = minDate.AddMinutes(TimeSlotLength * i);
                foreach (RHAppointment app in lstApp)
                {
                    if ((PatientToExclude!=null) && (app.IdPatient == PatientToExclude.Id)) continue;
                    if ((app.StartDate <= dte) && (app.EndDate > dte))
                    {
                        CurrentDispo[i] = 1;
                        break;
                    }
                }
            }
        

            foreach (Utilisateur u in UtilisateursMgt.Praticiens)
                MgmtHoraire.GetHorairesReel(u);


            //List<AffectationFauteuil> lst = PresenceMgt.getAffectationFauteuils(minDate, maxDate);

            //for (int i = 0; i < CurrentDispo.Length; i++)
            //{
            //    DateTime dte = minDate.AddMinutes(TimeSlotLength * i);
            //    bool isbusy = true;
            //    foreach (AffectationFauteuil af in lst)
            //    {
            //        if (af.fauteuil != f) continue;
            //        if ((dte > af.DteFrom) && (dte < af.DteTo))
            //            isbusy = false;
            //    }
            //    if (isbusy) 
            //        CurrentDispo[i] = (byte)1;
            //}

            //foreach (AffectationFauteuil af in lst)
            //{
            //    if (af.fauteuil != f) continue;

            //    for (int i = 0; i < CurrentDispo.Length; i++)
            //    {
            //        DateTime dte = minDate.AddMinutes(TimeSlotLength * i);
            //        if ((dte <= af.DteFrom) || (dte >= af.DteTo))
            //        {
            //            CurrentDispo[i] = (byte)1;
            //        }
            //    }
            //}

            for (int i = 0; i < CurrentDispo.Length; i++)
            {
                DateTime dte = minDate.AddMinutes(TimeSlotLength * i);

                bool found = false;
                foreach (Utilisateur u in UtilisateursMgt.Praticiens)
                    foreach (HoraireReel hr in u.horairesreels)
                    {
                        DateTime hrdte = UtilisateursMgt.GetDateFromHoraireReel(hr);
                        DateTime hrStartdte = hrdte.AddTicks(hr.starttime.Ticks);
                        DateTime hrEnddte = hrdte.AddTicks(hr.endtime.Ticks);

                        if (hrStartdte <= dte && dte < hrEnddte)
                        {
                            found = true;
                            break;
                        }
                    }

                CurrentDispo[i] = found ? CurrentDispo[i] : (byte)1;
            }


        

            for (int i = 0; i < CurrentDispo.Length; i++)
            {
                DateTime dte = minDate.AddMinutes(TimeSlotLength * i);

                string lib;
                CurrentDispo[i] = UtilisateursMgt.IsNotFerie(dte, out lib) ? CurrentDispo[i] : (byte)1;
            }
            return CurrentDispo;

        }
        
        public static void Delete(RHAppointment app, Utilisateur modifiePar, BasCommon_BO.RHAppointment.AnnulerPar annulerpar)
        {
            List<RHAppointment> appSupp = new List<RHAppointment>();
            if (modifiePar != null) lastUser = modifiePar;
            DAC.InsertRDVTrace("Delete", app, modifiePar);
            appSupp = getAppointments(app.IdPatient);
            if (appSupp.Count != 0 && appSupp != null && app.idNextact != 0)
            {
                foreach (RHAppointment rha in appSupp)
                {
                    if (rha.IdCommClinique == app.IdCommClinique)
                    {
                        DAC.DeleteAppointment(rha);
                        DAC.InsertAnnulationTrace(rha, (int)annulerpar);
                        NetWorkSend(rha.StartDate.Date);
                    }
                }
            }
            else
            {

                DAC.DeleteAppointment(app);

                if (app.CommCl != null)
                {
                    app.CommCl.IdRDV = -1;
                    app.CommCl.Appointement = null;
                }
                NetWorkSend(app.StartDate.Date);
                DAC.InsertAnnulationTrace(app, (int)annulerpar);
            }
        }
       
        #region Old
        //public static void Delete(RHAppointment app, Utilisateur modifiePar, BasCommon_BO.RHAppointment.AnnulerPar annulerpar)
        //{
        //    if (modifiePar != null) lastUser = modifiePar;
        //    DAC.InsertRDVTrace("Delete", app, modifiePar);
        //    DAC.DeleteAppointment(app);
        //    if (app.CommCl != null)
        //    {
        //        app.CommCl.IdRDV = -1;
        //        app.CommCl.Appointement = null;
        //    }
        //    NetWorkSend(app.StartDate.Date);
        //    DAC.InsertAnnulationTrace(app, (int)annulerpar);
        //}
        #endregion

        public static List<RHAppointment> GetAppointments(DateTime p_date, List<Acte> p_Actes)
        {
            List<RHAppointment> lst = new List<RHAppointment>();
            DataTable dt = DAC.getAppointments(p_date);

            foreach (DataRow r in dt.Rows)
            {
                RHAppointment app = Builders.BuildAppointement.BuildRHAppointment(r, p_Actes);
                lst.Add(app);
            }

            return lst;
        }

        public static List<RHAppointment> GetAppointments(DateTime p_dateS, DateTime p_dateE, Fauteuil f)
        {
            DateTime dte = p_dateE.Date.AddHours((double)23).AddMinutes((double)59);
            
            List<RHAppointment> liste = new List<RHAppointment>();
            

            String date1 = p_dateS.ToString("yyyy-MM-dd HH:mm:ss");            
            String date2 = dte.ToString("yyyy-MM-dd HH:mm:ss");
            
            string methodPath = "/Appointment/" + date1 + "&" + date2 + "&";
            
            methodPath += f == null ? -1 : f.Id;
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray(methodPath);

            foreach(JObject item in jArray){
                RHAppointment rhApp = Builders.BuildAppointement.BuildRHAppointmentWithoutPatient(item);
                liste.Add(rhApp);
            }
            return liste;
        }

        public static List<RHAppointment> GetAppointmentsOld(DateTime p_dateS, DateTime p_dateE,Fauteuil f)
        {          
            List<RHAppointment> lst = new List<RHAppointment>();
            DataTable dt = DAC.getAppointments(p_dateS, p_dateE,f);

            foreach (DataRow r in dt.Rows)
            {
                RHAppointment app = Builders.BuildAppointement.BuildRHAppointment(r);
                lst.Add(app);
            }

            return lst;
        }

        public static List<RHAppointment> GetAppointments(DateTime p_dateS, DateTime p_dateE)        
        {
            return GetAppointments(p_dateS, p_dateE, null);
        }

        public static List<RHAppointment> GetAppointmentsOld(DateTime p_dateS,DateTime p_dateE)
        {
            List<RHAppointment> lst = new List<RHAppointment>();
            DataTable dt = DAC.getAppointments(p_dateS, p_dateE);

            foreach (DataRow r in dt.Rows)
            {
                RHAppointment app = Builders.BuildAppointement.BuildRHAppointment(r);
                lst.Add(app);
            }

            return lst;
        }

        public static List<RHAppointment> GetAppointments(basePatient p_patient, List<Acte> p_Actes)
        {
            List<RHAppointment> lst = new List<RHAppointment>();
            DataTable dt = DAC.getAppointments(p_patient);

            foreach (DataRow r in dt.Rows)
            {
                RHAppointment app = Builders.BuildAppointement.BuildRHAppointment(r, p_Actes);
                lst.Add(app);
            }

            return lst;
        }

        public static List<RHAppointment> GetAppointments(int IdPatient)
        {
            List<RHAppointment> lst = new List<RHAppointment>();
            DataTable dt = DAC.getAppointments(IdPatient);

            foreach (DataRow r in dt.Rows)
            {
                RHAppointment app = Builders.BuildAppointement.BuildRHAppointment(r);
                lst.Add(app);
            }

            return lst;
        }

        public static RHAppointment GetAppointment(int idRdv) {

            return getAppointment(idRdv);
        
        }


        public static RHAppointment GetAppointmentOld(int IdRdv)
        {
            
            DataRow r = DAC.getAppointment(IdRdv);

            return Builders.BuildAppointement.BuildRHAppointment(r);
        }

        public static List<TraceAppointment> GetRDVTrace(RHAppointment app, List<Acte> p_Actes)
        {
            List<TraceAppointment> list = new List<TraceAppointment>();
            DataTable dt = DAC.getRDVTrace(app, p_Actes);

            foreach (DataRow r in dt.Rows)
            {
                list.Add(Builders.BuildAppointement.BuildTraceAppointment(r, p_Actes));
            }
            return list;
        }
    }
}
