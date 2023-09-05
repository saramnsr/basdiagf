using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL.Builders
{

    public static class BuildObjSuivi
    {
        public static ObjSuivi BuildJ(JObject r)
        {



            ObjSuivi suivi = new ObjSuivi();
            suivi.Id = Convert.ToInt32(r["id"]);
            suivi.CodeBarre = Convert.ToString(r["codeabarre"]);
            suivi.NatureTravaux = Convert.ToString(r["nature"]);
            suivi.Details = Convert.ToString(r["detail"]);
            suivi.SortieCabinet = r["sortieCabinet"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["sortieCabinet"]);

            suivi.SortieCabinetAvec = Convert.ToString(r["sortieCabWith"]);
            suivi.EntreeCabinetAvec =Convert.ToString(r["entreeCabWith"]);
            suivi.SortieLaboAvec = r["detailsortie_labo_with"] == null ? "" : Convert.ToString(r["detailsortie_labo_with"]);
            suivi.EntreeLaboAvec = r["entree_labo_with"] == null ? "" : Convert.ToString(r["entree_labo_with"]);
            suivi.Empreinte = r["dateEmpreinte"] == null ? null : (DateTime?)Convert.ToDateTime(r["dateEmpreinte"]);


            suivi.EntreeLabo = r["entreLab"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["entreLab"]);
            suivi.PatientName = Convert.ToString(r["patientName"]);
            suivi.PatientId = Convert.ToInt32(r["patientId"]);
            suivi.RequestorName = Convert.ToString(r["requestorName"]);
            suivi.RequestorId = Convert.ToInt32(r["requestorId"]);
            suivi.ValidatorName = Convert.ToString(r["validorName"]);
            suivi.ValidatorId = Convert.ToInt32(r["validorId"]);
            suivi.RecupereParName = Convert.ToString(r["recupererPar"]);
            suivi.WorkerName = Convert.ToString(r["workerName"]);
            suivi.SortieLabo = r["sortieLabo"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["sortieLabo"]);
            suivi.ReceptionCab = r["receptionCab"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["receptionCab"]);
            suivi.PoseApp = r["poseApp"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["poseApp"]);
            suivi.tarif = (float)Convert.ToDouble(r["tarif"]);
            suivi.LastCommentDate = r["lastCommentDate"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["lastCommentDate"]);
            suivi.DemandeID = Convert.ToInt32(r["demandeId"]);
            suivi.IsToSend = Convert.ToBoolean(r["aEnvoye"]);

            return suivi;
        }
        public static ObjSuivi Build(DataRow r)
        {



            ObjSuivi suivi = new ObjSuivi();
            suivi.Id = SysTools.DataRow_ValueInt(r, "id");
            suivi.CodeBarre = SysTools.DataRow_ValueString(r, "codebarre");
            suivi.NatureTravaux = SysTools.DataRow_ValueString(r, "nature");
            suivi.Details = SysTools.DataRow_ValueString(r, "Detail");
            suivi.SortieCabinet = SysTools.DataRow_ValueDateTime(r, "sortie_cab");

            suivi.SortieCabinetAvec = SysTools.DataRow_ValueString(r, "sortie_cab_with");
            suivi.EntreeCabinetAvec = SysTools.DataRow_ValueString(r, "entree_cab_with");
            suivi.SortieLaboAvec = SysTools.DataRow_ValueString(r, "sortie_labo_with");
            suivi.EntreeLaboAvec = SysTools.DataRow_ValueString(r, "entree_labo_with");



            suivi.EntreeLabo = SysTools.DataRow_ValueDateTime(r, "entre_labo");
            suivi.PatientName = SysTools.DataRow_ValueString(r, "PatientName");
            suivi.PatientId = SysTools.DataRow_ValueInt(r, "PatientId");
            suivi.RequestorName = SysTools.DataRow_ValueString(r, "RequestorName");
            suivi.RequestorId = SysTools.DataRow_ValueInt(r, "RequestorId");
            suivi.ValidatorName = SysTools.DataRow_ValueString(r, "ValidatorName");
            suivi.ValidatorId = SysTools.DataRow_ValueInt(r, "ValidatorId");
            suivi.RecupereParName = SysTools.DataRow_ValueString(r, "RecupereParName");
            suivi.WorkerName = SysTools.DataRow_ValueString(r, "WorkerName");
            suivi.SortieLabo = SysTools.DataRow_ValueDateTime(r, "sortie_labo");
            suivi.ReceptionCab = SysTools.DataRow_ValueDateTime(r, "reception_cab");
            suivi.PoseApp = SysTools.DataRow_ValueDateTime(r, "pose_app");
            suivi.tarif = (float)SysTools.DataRow_ValueDouble(r, "tarif");
            suivi.PaymentEffectueLe = SysTools.DataRow_ValueDateTime(r, "PAYMENTEFFECTUELE");
            suivi.Empreinte = SysTools.DataRow_ValueDateTime(r, "DATEEMPREINTE");
            suivi.LastCommentDate = SysTools.DataRow_ValueDateTime(r, "LastCommentDate");
            suivi.DemandeID = SysTools.DataRow_ValueInt(r, "Demande_ID");
            suivi.IsToSend = SysTools.DataRow_ValueBool(r, "AEnvoye");

            return suivi;
        }


    }
}
