using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FrmContainer_BO;
using BasCommon_BO;

namespace FrmContainer_BL
{
    public static class Builders
    {


        public static PatientARecontacter BuildPatientARecontacter(DataRow r)
        {
            PatientARecontacter patientARecontacter = new PatientARecontacter();
            patientARecontacter.Id =BasCommon_BL.Builders.SysTools.DataRow_ValueInt(r, "ID_PERSONNE");
            patientARecontacter.Nom =BasCommon_BL.Builders.SysTools.DataRow_ValueString(r, "per_nom");
            patientARecontacter.Prenom =BasCommon_BL.Builders.SysTools.DataRow_ValueString(r, "per_prenom");
            patientARecontacter.DateDernierRDV = r["DateRDV"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DateRDV"]);

            return patientARecontacter;
        }

        public static PatientEnRecontact BuildPatientEnRecontact(DataRow r)
        {


            PatientEnRecontact patientARecontacter = new PatientEnRecontact();
            patientARecontacter.Id =BasCommon_BL.Builders.SysTools.DataRow_ValueInt(r, "ID_PATIENT");
            patientARecontacter.Nom =BasCommon_BL.Builders.SysTools.DataRow_ValueString(r, "per_nom");
            patientARecontacter.Prenom =BasCommon_BL.Builders.SysTools.DataRow_ValueString(r, "per_prenom");
            patientARecontacter.DateDernierRDV = r["DateRDV"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DateRDV"]);
            patientARecontacter.Motif =BasCommon_BL.Builders.SysTools.DataRow_ValueString(r, "MOTIF");
            patientARecontacter.DepuisLe =BasCommon_BL.Builders.SysTools.DataRow_ValueDateTime(r, "ARECONTACTERDEPUISLE");
            patientARecontacter.ProchaineTentative =BasCommon_BL.Builders.SysTools.DataRow_ValueDateTime(r, "DATEPROCHAINETENTATIVE");
            patientARecontacter.DerniereTentative =BasCommon_BL.Builders.SysTools.DataRow_ValueDateTime(r, "DATETENTATIVE");
            patientARecontacter.NumTentative =BasCommon_BL.Builders.SysTools.DataRow_ValueInt(r, "NUMTENTATIVE");
            patientARecontacter.IdUserTentative =BasCommon_BL.Builders.SysTools.DataRow_ValueInt(r, "ID_USERTENTATIVE");
            patientARecontacter.IsPatientOrthalis =BasCommon_BL.Builders.SysTools.DataRow_ValueInt(r, "TEST_BP") != 1;


            return patientARecontacter;
        }

        public static PatientARelancer BuildPatientARelancer(DataRow r)
        {
            PatientARelancer cs = new PatientARelancer();
            cs.IdPatient = Convert.ToInt32(r["IDPATIENT"]);
            cs.IdResponsableFi = r["IDRESPFI"] is DBNull ? -1 : Convert.ToInt32(r["IDRESPFI"]);
            cs.SommesDue = Convert.ToDouble(r["MONTANT"]);
            cs.DueDepuis = Convert.ToDateTime(r["DTEECHEANCE"]);
            cs.ResponsableFi = Convert.ToString(r["NOMRESPFI"]).Trim() + " " + Convert.ToString(r["PRENOMRESPFI"]).Trim();
            cs.patient = Convert.ToString(r["NOMPATIENT"]).Trim() + " " + Convert.ToString(r["PRENOMPATIENT"]).Trim();

            cs.CurrentStatus = r["NIVEAU_RELANCE"] == DBNull.Value ? Relance.ModeRelance.Aucun : (Relance.ModeRelance)Convert.ToInt32(r["NIVEAU_RELANCE"]);

            return cs;
        }

        public static PatientSoldeNegatifCeJour BuildPatientSoldeNegatifCeJour(DataRow r)
        {
            PatientSoldeNegatifCeJour patientARecontacter = new PatientSoldeNegatifCeJour();
            patientARecontacter.Id = BasCommon_BL.Builders.SysTools.DataRow_ValueInt(r, "IDPATIENT");
            patientARecontacter.Nom = BasCommon_BL.Builders.SysTools.DataRow_ValueString(r, "NOMPATIENT");
            patientARecontacter.Prenom = BasCommon_BL.Builders.SysTools.DataRow_ValueString(r, "PRENOMPATIENT");
            patientARecontacter.DateDeDerniereEcheance = Convert.ToDateTime(r["DTEECHEANCE"]);
            patientARecontacter.Montant = Convert.ToDouble(r["MONTANT"]);

            return patientARecontacter;
        }

    }
}
