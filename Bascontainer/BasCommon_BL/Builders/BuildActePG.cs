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

    public static class BuildActePG
    {

        public static ActePG BuildJ(JObject r)
        {
            ActePG apg = new ActePG();

            apg.Id = Convert.ToInt32(r["id"]);

            apg.IdPatient = Convert.ToInt32(r["id_PATIENT"]);
            apg.IdActe =r["id_ACTE"].ToString() == "" ? -1 : Convert.ToInt32(r["id_ACTE"]);

            apg.DateExecution = r["date_DEBUT"]==null ? DateTime.Now : Convert.ToDateTime(r["date_DEBUT"]);


            apg.NbJours = r["nb_JOURS"].ToString() == "" ? 0 : (int?)Convert.ToInt32(r["nb_JOURS"]);
            apg.NbMois = r["nb_MOIS"].ToString() == "" ? 0 : (int?)Convert.ToInt32(r["nb_MOIS"]);

            apg.IdPlan = r["id_PLAN"].ToString() == "" ? -1 : Convert.ToInt32(r["id_PLAN"]);
            apg.Template = r["id_ACTEGESTION"].ToString() == "" ? null : TemplateApctePGMgmt.getCodeSecu(Convert.ToInt32(r["id_ACTEGESTION"]));
            apg.Libelle = r["libelle"].ToString() == "" ? "" : Convert.ToString(r["libelle"]).Trim();
            apg.prestation = r["id_PRESTATION"].ToString() == "" ? null : TemplateApctePGMgmt.getCodePrestation(Convert.ToString(r["id_PRESTATION"]).Trim());
            apg.Coeff = r["coeff"].ToString() == "" ? -1 : Convert.ToInt32(r["coeff"]);
            apg.Montant_Honoraire = Convert.ToDouble(r["montant"]);
            apg.NeedFSE = r["need_FSE"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToInt32(r["need_FSE"]));
            apg.NeedDEP = r["need_DEP"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToInt32(r["need_DEP"]));
            try
            {
                apg.IsDecomposed = r["isdecomposed"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToInt32(r["isdecomposed"]));
            }
            catch (Exception e)
            {
                apg.IsDecomposed = r["isdecomposed"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToString(r["isdecomposed"]));
            }
            apg.CoeffDecompose = r["decomposed"].ToString() == "" ? "" : Convert.ToString(r["decomposed"]);

            apg.NumSemestre = r["num_SEMESTRE"].ToString() == "" ? 0 : Convert.ToInt16(r["num_SEMESTRE"]);
            apg.NumContention = r["num_SEMESTRE"].ToString() == "" ? 0 : Convert.ToInt16(r["num_Contention"]);



            apg.motifdepassement = r["motifdepassement"].ToString() == "" ? PyxVitalWrapperConst.Qualificatif_depense.Néant : (PyxVitalWrapperConst.Qualificatif_depense)Convert.ToInt32(r["motifdepassement"]);
            apg.domicile = r["lieuexecution"].ToString() == "" ? PyxVitalWrapperConst.Domicile.N : ((PyxVitalWrapperConst.Domicile)Convert.ToInt32(r["lieuexecution"]));
            
            apg.Quantite = Convert.ToInt32(r["nbkilometre"]);
            //apg.rembExceptionel = r[""] is DBNull ? (PyxVitalWrapperConst.RembExceptionel.N) : Convert.ToInt32(r["nbkilometre"]);
            //apg.suplCharge = r[""] is DBNull ? (PyxVitalWrapperConst.SuplCharge.N) : Convert.ToInt32(r["nbkilometre"]);

            apg.rno = r["rno"].ToString() == "" ? PyxVitalWrapperConst.RNO.Néant : (PyxVitalWrapperConst.RNO)Convert.ToInt32(r["rno"]);
            apg.nuit = r["nuit"].ToString() == "" ? PyxVitalWrapperConst.Nuit.N : (PyxVitalWrapperConst.Nuit)Convert.ToInt32(r["nuit"]);
            //apg.urgence = r[""] is DBNull ? (PyxVitalWrapperConst.Urgence.N) : Convert.ToInt32(r[""]);
            apg.DimancheEtJF = r["dimancheetjf"].ToString() == "" ? PyxVitalWrapperConst.DimancheEtJF.N : (PyxVitalWrapperConst.DimancheEtJF)Convert.ToInt32(r["dimancheetjf"]);
            apg.ald = r["ald"].ToString() == "" ? PyxVitalWrapperConst.ALD.N : (PyxVitalWrapperConst.ALD)Convert.ToInt32(r["ald"]);
            apg.Exoneration = r["exoneration"].ToString() == "" ? PyxVitalWrapperConst.Exoneration.ExNéant : (PyxVitalWrapperConst.Exoneration)Convert.ToInt32(r["exoneration"]);

            //apg.ExonerationLibOuTx = r[""] is DBNull ? "" : Convert.ToInt32(r[""]);

            //apg.DateDEP = r[""] is DBNull ? null : Convert.ToInt32(r[""]);
            apg.Id_DEP = r["id_DEP"].ToString() == "" ? -1 : Convert.ToInt32(r["id_DEP"]);
            apg.Id_FS = r["id_FS"].ToString() == "" ? -1 : Convert.ToInt32(r["id_FS"]);

            //apg.CodeAccordDEP = r["codeaccentente"] is DBNull ? (PyxVitalWrapperConst.CodeAccordDEP.Ac0) : (PyxVitalWrapperConst.CodeAccordDEP)Convert.ToInt32(r["codeaccentente"]);

            apg.accident = r["accident"].ToString() == "" ? PyxVitalWrapperConst.Accident.N : (PyxVitalWrapperConst.Accident)Convert.ToInt32(r["accident"]);
            apg.DateAccident = r["dateaccident"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateaccident"]);

            apg.NumMutuelle = r["nummutuelle"].ToString() == "" ? "" : Convert.ToString(r["nummutuelle"]);
            apg.ActeCMU = r["actecmu"].ToString() == "" ? PyxVitalWrapperConst.CMU.N : (PyxVitalWrapperConst.CMU)Convert.ToInt32(r["actecmu"]);

            apg.numdent = r["numdent"].ToString() == "" ? "" : Convert.ToString(r["numdent"]);
            apg.IdSemestrePlanGestionAssocie = r["idsem_PTA"].ToString() == "" ? -1 : Convert.ToInt32(r["idsem_PTA"]);
            apg.IdSurvPlanGestionAssocie = r["idsurv_PTA"].ToString() == "" ? -1 : Convert.ToInt32(r["idsurv_PTA"]);
            apg.IdDevisAssociate = r["iddevis_PTA"].ToString() == "" ? -1 : Convert.ToInt32(r["iddevis_PTA"]);
            apg.IdComm = r["id_COMM"].ToString() == "" ? -1 : Convert.ToInt32(r["id_COMM"]);
            apg.Facturee = r["facturee"].ToString() == "" ? -1 : Convert.ToInt32(r["facturee"]);
            apg.Id_facture = r["numdent"].ToString() == "" ? -1 :Convert.ToInt32(r["id_FACTURE"]);
            apg.TypeActe = r["type_COMMENT"].ToString() == "" ? "0" : Convert.ToString(r["type_COMMENT"]);
            apg.rabais =r["rabais"].ToString() == "" ? 0 : Convert.ToDouble(r["rabais"]);
            return apg;
        }
        public static ActePG Build(DataRow r)
        {


            ActePG apg = new ActePG();
           
                apg.Id = r["id"] is DBNull ? -1 : Convert.ToInt32(r["id"]);
            
            apg.IdPatient = r["id_patient"] is DBNull ? -1 : Convert.ToInt32(r["id_patient"]);
            apg.IdActe = r["ID_ACTE"] is DBNull ? -1 : Convert.ToInt32(r["ID_ACTE"]);
           
               apg.DateExecution = Convert.ToDateTime(r["DATE_DEBUT"]);
           
            
            apg.NbJours = r["NB_JOURS"] is DBNull ? null : (int?)Convert.ToInt32(r["NB_JOURS"]);
            apg.NbMois = r["NB_MOIS"] is DBNull ? null : (int?)Convert.ToInt32(r["NB_MOIS"]);
          
                apg.IdPlan = r["ID_PLAN"] is DBNull ? -1 : Convert.ToInt32(r["ID_PLAN"]);
            apg.Template = r["ID_ACTEGESTION"] is DBNull ? null : TemplateApctePGMgmt.getCodeSecu(Convert.ToInt32(r["ID_ACTEGESTION"]));
            apg.Libelle = r["libelle"] is DBNull ? "" : Convert.ToString(r["libelle"]).Trim();
            apg.prestation = r["ID_PRESTATION"] is DBNull ? null : TemplateApctePGMgmt.getCodePrestation(Convert.ToString(r["ID_PRESTATION"]).Trim());
            apg.Coeff = r["COEFF"] is DBNull ? 1 : Convert.ToInt32(r["COEFF"]);
            apg.Montant_Honoraire = r["MONTANT"] is DBNull ? 0 : Convert.ToDouble(r["MONTANT"]);
            apg.NeedFSE = r["NEED_FSE"] is DBNull ? false : Convert.ToBoolean(Convert.ToInt32(r["NEED_FSE"]));
            apg.NeedDEP = r["NEED_DEP"] is DBNull ? false : Convert.ToBoolean(Convert.ToInt32(r["NEED_DEP"]));
            try
            {
                apg.IsDecomposed = r["ISDECOMPOSED"] is DBNull ? false : Convert.ToBoolean(Convert.ToInt32(r["ISDECOMPOSED"]));
            }catch(Exception e)
            {
                apg.IsDecomposed = r["ISDECOMPOSED"] is DBNull ? false : Convert.ToBoolean(Convert.ToString(r["ISDECOMPOSED"]));
            }
            apg.CoeffDecompose = r["DECOMPOSED"] is DBNull ? "" : Convert.ToString(r["DECOMPOSED"]);

            apg.NumSemestre = r["NUM_SEMESTRE"] is DBNull ? -1 : Convert.ToInt16(r["NUM_SEMESTRE"]);
            apg.NumContention = r["NUM_Contention"] is DBNull ? -1 : Convert.ToInt16(r["NUM_Contention"]);



            apg.motifdepassement = r["MOTIFDEPASSEMENT"] is DBNull ? PyxVitalWrapperConst.Qualificatif_depense.Néant : (PyxVitalWrapperConst.Qualificatif_depense)Convert.ToInt32(r["MOTIFDEPASSEMENT"]);
            apg.domicile = r["lieuexecution"] is DBNull ? (PyxVitalWrapperConst.Domicile.N) : ((PyxVitalWrapperConst.Domicile)Convert.ToInt32(r["lieuexecution"]));
            apg.Quantite = r["nbkilometre"] is DBNull ? 1 : Convert.ToInt32(r["nbkilometre"]);
            //apg.rembExceptionel = r[""] is DBNull ? (PyxVitalWrapperConst.RembExceptionel.N) : Convert.ToInt32(r["nbkilometre"]);
            //apg.suplCharge = r[""] is DBNull ? (PyxVitalWrapperConst.SuplCharge.N) : Convert.ToInt32(r["nbkilometre"]);

            apg.rno = r["rno"] is DBNull ? (PyxVitalWrapperConst.RNO.Néant) : (PyxVitalWrapperConst.RNO)Convert.ToInt32(r["rno"]);
            apg.nuit = r["nuit"] is DBNull ? (PyxVitalWrapperConst.Nuit.N) : (PyxVitalWrapperConst.Nuit)Convert.ToInt32(r["nuit"]);
            //apg.urgence = r[""] is DBNull ? (PyxVitalWrapperConst.Urgence.N) : Convert.ToInt32(r[""]);
            apg.DimancheEtJF = r["DIMANCHEETJF"] is DBNull ? (PyxVitalWrapperConst.DimancheEtJF.N) : (PyxVitalWrapperConst.DimancheEtJF)Convert.ToInt32(r["DIMANCHEETJF"]);
            apg.ald = r["ald"] is DBNull ? (PyxVitalWrapperConst.ALD.N) : (PyxVitalWrapperConst.ALD)Convert.ToInt32(r["ald"]);
            apg.Exoneration = r["exoneration"] is DBNull ? (PyxVitalWrapperConst.Exoneration.ExNéant) : (PyxVitalWrapperConst.Exoneration)Convert.ToInt32(r["exoneration"]);

            //apg.ExonerationLibOuTx = r[""] is DBNull ? "" : Convert.ToInt32(r[""]);

            //apg.DateDEP = r[""] is DBNull ? null : Convert.ToInt32(r[""]);
            apg.Id_DEP = r["ID_DEP"] is DBNull ? -1 : Convert.ToInt32(r["ID_DEP"]);
            apg.Id_FS = r["ID_FS"] is DBNull ? -1 : Convert.ToInt32(r["ID_FS"]);

            //apg.CodeAccordDEP = r["codeaccentente"] is DBNull ? (PyxVitalWrapperConst.CodeAccordDEP.Ac0) : (PyxVitalWrapperConst.CodeAccordDEP)Convert.ToInt32(r["codeaccentente"]);

            apg.accident = r["accident"] is DBNull ? (PyxVitalWrapperConst.Accident.N) : (PyxVitalWrapperConst.Accident)Convert.ToInt32(r["accident"]);
            apg.DateAccident = r["dateaccident"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["dateaccident"]);

            apg.NumMutuelle = r["NumMutuelle"] is DBNull ? "" : Convert.ToString(r["NumMutuelle"]);
            apg.ActeCMU = r["ActeCMU"] is DBNull ? PyxVitalWrapperConst.CMU.N : (PyxVitalWrapperConst.CMU)Convert.ToInt32(r["ActeCMU"]);

            apg.numdent = r["numdent"] is DBNull ? "" : Convert.ToString(r["numdent"]);
            apg.IdSemestrePlanGestionAssocie = r["IDSEM_PTA"] is DBNull ? -1 : Convert.ToInt32(r["IDSEM_PTA"]);
            apg.IdSurvPlanGestionAssocie = r["IDSURV_PTA"] is DBNull ? -1 : Convert.ToInt32(r["IDSURV_PTA"]);
            apg.IdDevisAssociate = r["IDDEVIS_PTA"] is DBNull ? -1 : Convert.ToInt32(r["IDDEVIS_PTA"]);
            apg.IdComm = r["ID_COMM"] is DBNull ? -1 : Convert.ToInt32(r["ID_COMM"]);
          apg.Facturee = r["FACTUREE"] is DBNull ? 0 : Convert.ToInt32(r["FACTUREE"]);
         apg.Id_facture = r["ID_FACTURE"] is DBNull ? 0 : Convert.ToInt32(r["ID_FACTURE"]);
         apg.TypeActe = r["TYPE_COMMENT"] is DBNull ? "0" : Convert.ToString(r["TYPE_COMMENT"]);
         apg.rabais = r["RABAIS"] is DBNull ? 0 : Convert.ToDouble(r["RABAIS"]);
            return apg;
        }
  
    }
}
