using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.Data;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL.Builders
{
    public static class BuildAppointement
    {
        public static RHAppointment BuildFullRHAppointmentJ(JObject r)
        {           
            RHAppointment App = new RHAppointment();
            App.Id = Convert.ToInt32(r["id_rdv"]);
            App.StartDate = Convert.ToDateTime(r["rdv_date"]);

            App.DateArrive = r["rdv_arrivee"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["rdv_arrivee"]);
            App.DateFauteuil = r["heure_fauteuil"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["heure_fauteuil"]);
            App.DateSecretariat = r["heure_secretariat"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["heure_secretariat"]);
            App.DateSortie = r["heure_sorti"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["heure_sorti"]);
            App.IdCommClinique = String.IsNullOrEmpty(r["id_COMMCLINIQUE"].ToString()) ? -1 : Convert.ToInt32(r["id_COMMCLINIQUE"]);


            App.IdPraticien = r["praticienResp"].ToString() == "" ? -1 : Convert.ToInt32(r["praticienResp"]);
            try
            {
                App.IsPraticienUnique = r["praticienUnique"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToString(r["praticienUnique"]));
            }
            catch (Exception e)
            {
                App.IsPraticienUnique = r["praticienUnique"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToInt32(r["praticienUnique"]));
            }
            try
            {
                App.IsAssistanteUnique = r["assistanteUnique"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToString(r["assistanteUnique"]));
            }
            catch (Exception e)
            {
                App.IsAssistanteUnique = r["assistanteUnique"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToInt32(r["assistanteUnique"]));
            }
            if (App.DateArrive == DateTime.MinValue) App.DateArrive = null;
            if (App.DateFauteuil == DateTime.MinValue) App.DateFauteuil = null;
            if (App.DateSecretariat == DateTime.MinValue) App.DateSecretariat = null;
            if (App.DateSortie == DateTime.MinValue) App.DateSortie = null;

            App.Localisation = r["localisation"].ToString() == "" ? RHAppointment.EnumLocalisation.Aucune : (RHAppointment.EnumLocalisation)Convert.ToInt16(r["localisation"]);
            App.Status = r["rdv_statut"].ToString() == "" ? RHAppointment.EnumStatus.Attendu : (RHAppointment.EnumStatus)Convert.ToInt16(r["rdv_statut"]);

            App.AAvancer = r["aavancer"].ToString() == "" ? false : Convert.ToString(r["aavancer"]) == "True";

            App.NextRDV = null;

            App.EndDate = App.StartDate.AddMinutes(Convert.ToInt32(r["rdv_duree"]));

            int ActeId;

            App.idNextact = r["id_NEXT_ACTE"].ToString() == "" ? -1 : Convert.ToInt32(r["id_NEXT_ACTE"]);
            ActeId = r["id_acte"].ToString() == "" ? -1 : Convert.ToInt32(r["id_acte"]);
            App.acte = ActesMgmt.Actes.Find(x => x.id_acte == ActeId);

            int fautId = r["id_fauteuil"].ToString() == "" ? -1 : Convert.ToInt32(r["id_fauteuil"]);
            App.Resource = Fauteuilsmgt.fauteuils.Find(w => w.Id == fautId);

            fautId = r["faut_utilise"].ToString() == "" ? -1 : Convert.ToInt32(r["faut_utilise"]);
            App.FauteuilReel = Fauteuilsmgt.fauteuils.Find(w => w.Id == fautId);

            /*App.patient = BasCommon_BL.baseMgmtPatient.GetPatient(Convert.ToInt32(r["id_personne"]));
            App.Comment = Convert.ToString(r["rdv_comm"]);
            App.Title = App.patient == null ? "" : App.patient.ToString() + "\n" + App.Comment;*/

            App.IdPatient = String.IsNullOrEmpty(r["id_personne"].ToString()) ? -1 : Convert.ToInt32(r["id_personne"]);
          //  App.patient = App.IdPatient == -1 ? null : BasCommon_BL.baseMgmtPatient.GetPatient(App.IdPatient);

            App.Title = App.PatientName + "\n" + App.Comment;

            return App;
        }

        public static RHAppointment BuildRHAppointment(JObject obj)
        {
 
            RHAppointment rhApp = new RHAppointment();

            rhApp.idNextact = obj["id_NEXT_ACTE"].ToString().Length==0 ? -1 : Convert.ToInt32(obj["id_NEXT_ACTE"]);

            rhApp.Id = Convert.ToInt32(obj["id_rdv"]);
            rhApp.StartDate = Convert.ToDateTime(obj["rdv_date"]);
            rhApp.IdCommClinique = String.IsNullOrEmpty(obj["id_COMMCLINIQUE"].ToString())  ? -1 : Convert.ToInt32(obj["id_COMMCLINIQUE"]);
            rhApp.DateSecretariat = obj["heure_secretariat"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(obj["heure_secretariat"]);


            rhApp.Localisation = String.IsNullOrEmpty(obj["localisation"].ToString()) ? RHAppointment.EnumLocalisation.Aucune :(RHAppointment.EnumLocalisation)Convert.ToInt32(obj["localisation"]);
            rhApp.Status = String.IsNullOrEmpty(obj["rdv_statut"].ToString()) ? RHAppointment.EnumStatus.Attendu : (RHAppointment.EnumStatus)Convert.ToInt32(obj["rdv_statut"]);
            rhApp.DateSortie = obj["heure_sorti"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(obj["heure_sorti"]);
            rhApp.NextRDV = obj["NextRDV"] == null ? null : (DateTime?)Convert.ToDateTime(obj["NextRDV"]);

            rhApp.DateArrive = String.IsNullOrEmpty(obj["rdv_arrivee"].ToString()) ? null : (DateTime?)Convert.ToDateTime(obj["rdv_arrivee"]);
            rhApp.Comment = String.IsNullOrEmpty(obj["rdv_comm"].ToString()) ? "" : obj["rdv_comm"].ToString().Trim();

            rhApp.fromweb = obj["fromweb"].ToString() == "" ? 0 : Convert.ToInt32(obj["fromweb"]);
            rhApp.perIdPersonne = obj["perIdPersonne"].ToString() == "" ? 0 : Convert.ToInt32(obj["perIdPersonne"]);
            rhApp.userName = obj["patname"].ToString().Trim();
            
         
            if(rhApp.StartDate != null)
                rhApp.EndDate = rhApp.StartDate.AddMinutes(String.IsNullOrEmpty(obj["rdv_duree"].ToString())  ? 0 :Convert.ToInt32(obj["rdv_duree"]));
           
            int idActe =string.IsNullOrEmpty(obj["id_acte"].ToString())? -1 : Convert.ToInt32(obj["id_acte"]);
            rhApp.acte =idActe == -1 ? null : BasCommon_BL.ActesMgmt.Actes.Find(x => x.id_acte == idActe);


            rhApp.Resource = String.IsNullOrEmpty(obj["id_fauteuil"].ToString())  ? null : Fauteuilsmgt.fauteuils.Find(x => x.Id == Convert.ToInt32(obj["id_fauteuil"]));           
            rhApp.FauteuilReel =String.IsNullOrEmpty(obj["faut_utilise"].ToString())  ? null: BasCommon_BL.Fauteuilsmgt.fauteuils.Find(x => x.Id == Convert.ToInt32(obj["faut_utilise"]));

            rhApp.DateFauteuil = obj["heure_fauteuil"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(obj["heure_fauteuil"]);

            rhApp.AAvancer = "T".Equals(obj["aavancer"].ToString()[0]) ? true : false;
           
            rhApp.PatientName = obj["patname"].ToString().Trim();

            rhApp.IdPatient =  String.IsNullOrEmpty(obj["id_personne"].ToString())? -1 :Convert.ToInt32(obj["id_personne"]);
            //rhApp.patient = rhApp.IdPatient == -1 ? null : BasCommon_BL.baseMgmtPatient.GetPatient(rhApp.IdPatient);

          rhApp.Title = rhApp.PatientName + "\n" + rhApp.Comment ;
          
            

            
            return rhApp;
        }
        public static RHAppointment BuildRHAppointmentWithoutPatient(JObject obj)
        {

            RHAppointment rhApp = new RHAppointment();

            rhApp.idNextact = obj["id_NEXT_ACTE"].ToString().Length == 0 ? -1 : Convert.ToInt32(obj["id_NEXT_ACTE"]);

            rhApp.Id = Convert.ToInt32(obj["id_rdv"]);
            rhApp.StartDate = Convert.ToDateTime(obj["rdv_date"]);
            rhApp.IdCommClinique = String.IsNullOrEmpty(obj["id_COMMCLINIQUE"].ToString()) ? -1 : Convert.ToInt32(obj["id_COMMCLINIQUE"]);
            rhApp.DateSecretariat = obj["heure_secretariat"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(obj["heure_secretariat"]);


            rhApp.Localisation = String.IsNullOrEmpty(obj["localisation"].ToString()) ? RHAppointment.EnumLocalisation.Aucune : (RHAppointment.EnumLocalisation)Convert.ToInt32(obj["localisation"]);
            rhApp.Status = String.IsNullOrEmpty(obj["rdv_statut"].ToString()) ? RHAppointment.EnumStatus.Attendu : (RHAppointment.EnumStatus)Convert.ToInt32(obj["rdv_statut"]);
            rhApp.DateSortie = obj["heure_sorti"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(obj["heure_sorti"]);
            rhApp.NextRDV = obj["NextRDV"] == null ? null : (DateTime?)Convert.ToDateTime(obj["NextRDV"]);

            rhApp.DateArrive = String.IsNullOrEmpty(obj["rdv_arrivee"].ToString()) ? null : (DateTime?)Convert.ToDateTime(obj["rdv_arrivee"]);
            rhApp.Comment = String.IsNullOrEmpty(obj["rdv_comm"].ToString()) ? "" : obj["rdv_comm"].ToString().Trim();



            if (rhApp.StartDate != null)
                rhApp.EndDate = rhApp.StartDate.AddMinutes(String.IsNullOrEmpty(obj["rdv_duree"].ToString()) ? 0 : Convert.ToInt32(obj["rdv_duree"]));

            int idActe = string.IsNullOrEmpty(obj["id_acte"].ToString()) ? -1 : Convert.ToInt32(obj["id_acte"]);
            rhApp.acte = idActe == -1 ? null : BasCommon_BL.ActesMgmt.Actes.Find(x => x.id_acte == idActe);


            rhApp.Resource = String.IsNullOrEmpty(obj["id_fauteuil"].ToString()) ? null : Fauteuilsmgt.fauteuils.Find(x => x.Id == Convert.ToInt32(obj["id_fauteuil"]));
            rhApp.FauteuilReel = String.IsNullOrEmpty(obj["faut_utilise"].ToString()) ? null : BasCommon_BL.Fauteuilsmgt.fauteuils.Find(x => x.Id == Convert.ToInt32(obj["faut_utilise"]));

            rhApp.DateFauteuil = obj["heure_fauteuil"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(obj["heure_fauteuil"]);

            rhApp.AAvancer = "T".Equals(obj["aavancer"].ToString()[0]) ? true : false;

            rhApp.PatientName = obj["patname"].ToString().Trim();

            rhApp.IdPatient = String.IsNullOrEmpty(obj["id_personne"].ToString()) ? -1 : Convert.ToInt32(obj["id_personne"]);


            return rhApp;
        }
        public static RHAppointment BuildRHAppointment(DataRow r)
        {
            RHAppointment App = new RHAppointment();

            App.Id = Convert.ToInt32(r["id_rdv"]);
            App.StartDate = Convert.ToDateTime(r["rdv_date"]);
            //App.fromweb = Convert.ToInt32(r["fromweb"]);
            App.DateArrive = r["RDV_ARRIVEE"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["RDV_ARRIVEE"]);
            App.DateFauteuil = r["HEURE_FAUTEUIL"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["HEURE_FAUTEUIL"]);
            App.DateSecretariat = r["HEURE_SECRETARIAT"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["HEURE_SECRETARIAT"]);
            App.DateSortie = r["HEURE_SORTI"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["HEURE_SORTI"]);
            App.Localisation = r["LOCALISATION"] == DBNull.Value ? RHAppointment.EnumLocalisation.Aucune : (RHAppointment.EnumLocalisation)Convert.ToInt16(r["LOCALISATION"]);
            App.Status = r["RDV_STATUT"] == DBNull.Value ? RHAppointment.EnumStatus.Attendu : (RHAppointment.EnumStatus)Convert.ToInt16(r["RDV_STATUT"]);

            if (r["NextRDV"] == DBNull.Value)
                App.NextRDV = null;
            else
                App.NextRDV = (DateTime?)Convert.ToDateTime(r["NextRDV"]);

            App.IdCommClinique = r["ID_COMMCLINIQUE"] is DBNull ? -1 : Convert.ToInt32(r["ID_COMMCLINIQUE"]);
            App.idNextact = r["ID_NEXT_ACTE"] is DBNull ? 0 : Convert.ToInt32(r["ID_NEXT_ACTE"]);
            App.EndDate = App.StartDate.AddMinutes(Convert.ToInt32(r["rdv_duree"]));

            

            App.acte = ActesMgmt.getActe(Convert.ToInt32(r["id_acte"]));          
            App.Resource = Fauteuilsmgt.getfauteuil(Convert.ToInt32(r["id_fauteuil"]));
            App.FauteuilReel = r["FAUT_UTILISE"] is DBNull ? null : Fauteuilsmgt.getfauteuil(Convert.ToInt32(r["FAUT_UTILISE"]));
           


            App.IdPatient = Convert.ToInt32(r["ID_PERSONNE"]);
            App.PatientName = Convert.ToString(r["PATNAME"]);
            App.Comment = r["rdv_comm"] is DBNull ? "" : Convert.ToString(r["rdv_comm"]);

            return App;
        }
        public static TraceAppointment BuildTraceAppointment(DataRow r, List<Acte> p_Actes)
        {
            TraceAppointment App = new TraceAppointment();
            App.Id = Convert.ToInt32(r["id_rdv"]);

            App.StartDate = Convert.ToDateTime(r["rdv_date"]);

            if (r["RDV_ARRIVEE"] == DBNull.Value)
            {
                App.DateArrive = r["HEURE_SALLEATTENTE"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["HEURE_SALLEATTENTE"]);
            }
            else
            {
                App.DateArrive = r["RDV_ARRIVEE"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["RDV_ARRIVEE"]);
            }

            App.DateFauteuil = r["HEURE_FAUTEUIL"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["HEURE_FAUTEUIL"]);
            App.DateSecretariat = r["HEURE_SECRETARIAT"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["HEURE_SECRETARIAT"]);
            App.DateSortie = r["HEURE_SORTI"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["HEURE_SORTI"]);



            if (App.DateArrive == DateTime.MinValue) App.DateArrive = null;
            if (App.DateFauteuil == DateTime.MinValue) App.DateFauteuil = null;
            if (App.DateSecretariat == DateTime.MinValue) App.DateSecretariat = null;
            if (App.DateSortie == DateTime.MinValue) App.DateSortie = null;


            App.Localisation = r["LOCALISATION"] == DBNull.Value ? RHAppointment.EnumLocalisation.Aucune : (RHAppointment.EnumLocalisation)Convert.ToInt16(r["LOCALISATION"]);
            App.Status = r["RDV_STATUT"] == DBNull.Value ? RHAppointment.EnumStatus.Attendu : (RHAppointment.EnumStatus)Convert.ToInt16(r["RDV_STATUT"]);
          

            App.EndDate = App.StartDate.AddMinutes(Convert.ToInt32(r["rdv_duree"]));

            int ActeId;
            ActeId = Convert.ToInt32(r["id_acte"]);
            foreach (Acte a in p_Actes)
                if (a.id_acte == ActeId) App.acte = a;

            int fautId = Convert.ToInt32(r["id_fauteuil"]);
            foreach (Fauteuil f in Fauteuilsmgt.fauteuils)
                if (f.Id == fautId) App.Resource = f;

            fautId = r["FAUT_UTILISE"] is DBNull ? -1 : Convert.ToInt32(r["FAUT_UTILISE"]);
            foreach (Fauteuil f in Fauteuilsmgt.fauteuils)
                if (f.Id == fautId) App.FauteuilReel = f;

            App.IdPatient = Convert.ToInt32(r["id_personne"]);
            if (r.Table.Columns.Contains("AAvancer")) 
                App.AAvancer = r["AAvancer"] is DBNull?false:Convert.ToString(r["AAvancer"])=="True";

            App.Comment = Convert.ToString(r["rdv_comm"]);
            App.PatientName = Convert.ToString(r["NOMPATIENT"]);
            App.Title = App.PatientName + "\n" + App.Comment;
            
            App.TraceComment = Convert.ToString(r["TRACE_COMMENT"]);
            App.TraceDate = Convert.ToDateTime(r["TRACE_DATE"]);
            App.Utilisateur = UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["id_utilisateur"]));

            return App;
        }

        public static RHAppointment BuildRHAppointment(DataRow r, List<Acte> p_Actes)
        {
            RHAppointment App = new RHAppointment();
            App.Id = Convert.ToInt32(r["id_rdv"]);

            App.StartDate = Convert.ToDateTime(r["rdv_date"]);
           
            App.DateArrive = r["RDV_ARRIVEE"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["RDV_ARRIVEE"]);
            App.DateFauteuil = r["HEURE_FAUTEUIL"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["HEURE_FAUTEUIL"]);
            App.DateSecretariat = r["HEURE_SECRETARIAT"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["HEURE_SECRETARIAT"]);
            App.DateSortie = r["HEURE_SORTI"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["HEURE_SORTI"]);

            App.IdPraticien = r["PRATICIEN_RESP"] is DBNull ? -1 : Convert.ToInt32(r["PRATICIEN_RESP"]);
            App.IsPraticienUnique = r["PRATICIEN_UNIQUE"] is DBNull ? false : Convert.ToBoolean(r["PRATICIEN_UNIQUE"]);
            App.IsAssistanteUnique = r["ASSISTANTE_UNIQUE"] is DBNull ? false : Convert.ToBoolean(r["ASSISTANTE_UNIQUE"]);

            App.idNextact = r["ID_NEXT_ACTE"] is DBNull ? 0 : Convert.ToInt32(r["ID_NEXT_ACTE"]);
            if (App.DateArrive == DateTime.MinValue) App.DateArrive = null;
            if (App.DateFauteuil == DateTime.MinValue) App.DateFauteuil = null;
            if (App.DateSecretariat == DateTime.MinValue) App.DateSecretariat = null;
            if (App.DateSortie == DateTime.MinValue) App.DateSortie = null;




            App.Localisation = r["LOCALISATION"] == DBNull.Value ? RHAppointment.EnumLocalisation.Aucune : (RHAppointment.EnumLocalisation)Convert.ToInt16(r["LOCALISATION"]);
            App.Status = r["RDV_STATUT"] == DBNull.Value ? RHAppointment.EnumStatus.Attendu : (RHAppointment.EnumStatus)Convert.ToInt16(r["RDV_STATUT"]);

            if (r["NextRDV"] == DBNull.Value)
                App.NextRDV = null;
            else
                App.NextRDV = (DateTime?)Convert.ToDateTime(r["NextRDV"]);




            App.EndDate = App.StartDate.AddMinutes(Convert.ToInt32(r["rdv_duree"]));

            int ActeId;
            

            ActeId = Convert.ToInt32(r["id_acte"]);
            foreach (Acte a in p_Actes)
                if (a.id_acte == ActeId) App.acte = a;

            int fautId = Convert.ToInt32(r["id_fauteuil"]);
            foreach (Fauteuil f in Fauteuilsmgt.fauteuils)
                if (f.Id == fautId) App.Resource = f;

            fautId = r["FAUT_UTILISE"] is DBNull ? -1 : Convert.ToInt32(r["FAUT_UTILISE"]);
            foreach (Fauteuil f in Fauteuilsmgt.fauteuils)
                if (f.Id == fautId) App.FauteuilReel = f;

            //App.patient = BasCommon_BL.Builders.BuildPatient.Build(r);
            App.IdPatient = r["ID_PERSONNE"] is DBNull ? -1 : Convert.ToInt32(r["ID_PERSONNE"]);
            App.Comment = r["rdv_comm"] is DBNull ? "" : Convert.ToString(r["rdv_comm"]);
            App.Title = App.patient == null ? "" : App.patient.ToString() + "\n" + App.Comment;
            App.AAvancer = r["AAvancer"] is DBNull ? false : Convert.ToString(r["AAvancer"])=="True";

            return App;
        }              
        public static RHAppointment BuildFullRHAppointment(DataRow r)
        {
            RHAppointment App = new RHAppointment();
            App.Id = Convert.ToInt32(r["id_rdv"]);

            App.StartDate = Convert.ToDateTime(r["rdv_date"]);
            App.fromweb = Convert.ToInt32(r["fromweb"]);
            App.DateArrive = r["RDV_ARRIVEE"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["RDV_ARRIVEE"]);
            App.DateFauteuil = r["HEURE_FAUTEUIL"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["HEURE_FAUTEUIL"]);
            App.DateSecretariat = r["HEURE_SECRETARIAT"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["HEURE_SECRETARIAT"]);
            App.DateSortie = r["HEURE_SORTI"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["HEURE_SORTI"]);


            App.IdPraticien = r["PRATICIEN_RESP"] is DBNull ? -1 : Convert.ToInt32(r["PRATICIEN_RESP"]);
            App.IsPraticienUnique = r["PRATICIEN_UNIQUE"] is DBNull ? false : Convert.ToBoolean(r["PRATICIEN_UNIQUE"]);
            App.IsAssistanteUnique = r["ASSISTANTE_UNIQUE"] is DBNull ? false : Convert.ToBoolean(r["ASSISTANTE_UNIQUE"]);
            if (App.DateArrive == DateTime.MinValue) App.DateArrive = null;
            if (App.DateFauteuil == DateTime.MinValue) App.DateFauteuil = null;
            if (App.DateSecretariat == DateTime.MinValue) App.DateSecretariat = null;
            if (App.DateSortie == DateTime.MinValue) App.DateSortie = null;

            App.Localisation = r["LOCALISATION"] == DBNull.Value ? RHAppointment.EnumLocalisation.Aucune : (RHAppointment.EnumLocalisation)Convert.ToInt16(r["LOCALISATION"]);
            App.Status = r["RDV_STATUT"] == DBNull.Value ? RHAppointment.EnumStatus.Attendu : (RHAppointment.EnumStatus)Convert.ToInt16(r["RDV_STATUT"]);

            App.AAvancer = r["AAvancer"] == DBNull.Value ? false : Convert.ToString(r["AAvancer"])=="True";

            App.NextRDV = null;

            App.EndDate = App.StartDate.AddMinutes(Convert.ToInt32(r["rdv_duree"]));

            int ActeId;

            App.idNextact = r["ID_NEXT_ACTE"] is DBNull ? 0 : Convert.ToInt32(r["ID_NEXT_ACTE"]);
            ActeId = Convert.ToInt32(r["id_acte"]);
            foreach (Acte a in ActesMgmt.Actes)
                if (a.id_acte == ActeId) App.acte = a;

            int fautId = Convert.ToInt32(r["id_fauteuil"]);
            foreach (Fauteuil f in Fauteuilsmgt.fauteuils)
                if (f.Id == fautId) App.Resource = f;

            fautId = r["FAUT_UTILISE"] is DBNull ? -1 : Convert.ToInt32(r["FAUT_UTILISE"]);
            foreach (Fauteuil f in Fauteuilsmgt.fauteuils)
                if (f.Id == fautId) App.FauteuilReel = f;

            App.patient = BasCommon_BL.baseMgmtPatient.GetPatient(Convert.ToInt32(r["ID_PERSONNE"]));
            App.Comment = r["rdv_comm"] is DBNull ? "" : Convert.ToString(r["rdv_comm"]);
            App.Title = App.patient == null ? "" : App.patient.ToString() + "\n" + App.Comment ;

            return App;
        }
    }
}
