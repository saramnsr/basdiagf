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

    public static class BuildTemplateActePG
    {

        public static TemplateActePG Build(DataRow r)
        {
            TemplateActePG cs = new TemplateActePG();
            cs.Id = Convert.ToInt32(r["ID"]);
            cs.Nom = Convert.ToString(r["CODE"]).Trim();
            cs.Libelle = Convert.ToString(r["LIBELLE"]).Trim();
            cs.Code = TemplateApctePGMgmt.getCodePrestation(Convert.ToString(r["CODE_PRESTATION"]).Trim());
            cs.Coeff = Convert.ToInt32(r["ACTE_COEFF"]);
            cs.CoeffDecompose = Convert.ToString(r["DECOMP"]);
            cs.IsDecomposed = Convert.ToBoolean(r["DECOMPISVISIBLE"]);
            cs.NeedDEP = r["NEED_DEP"] is DBNull ? false : Convert.ToInt16(r["NEED_DEP"]) == 1;
            cs.NeedFS = r["NEED_FSE"] is DBNull ? false : Convert.ToInt16(r["NEED_FSE"]) == 1;
            cs.NBJours = r["NB_JOURS"] is DBNull ? null : (int?)Convert.ToInt32(r["NB_JOURS"]);
            cs.NBMois = r["NB_MOIS"] is DBNull ? null : (int?)Convert.ToInt32(r["NB_MOIS"]);
            cs.Valeur = Convert.ToDouble(r["VALEUR"]);
            cs.Organisation = r["ORGANISATION"] is DBNull ? "" : Convert.ToString(r["ORGANISATION"]);
            cs.phase = r["PHASE"] is DBNull ? Traitement.EnumPhase.Aucune : ((Traitement.EnumPhase)Convert.ToInt32(r["PHASE"]));
            cs.TypeDeReglement = r["TYPEDEREGLEMENT"] is DBNull ? ActePG.TypeReglement.Forfaitaire : (ActePG.TypeReglement)Convert.ToChar(r["TYPEDEREGLEMENT"]);
            cs.VALEUR_CMU = r["VALEUR_CMU"] is DBNull ? 0 : Convert.ToDouble(r["VALEUR_CMU"]);
            

            return cs;
        }
        public static TemplateActePG BuildJ(JObject r)
        {
            TemplateActePG cs = new TemplateActePG();
            cs.Id = Convert.ToInt32(r["id"]);
            cs.Nom = Convert.ToString(r["code"]).Trim();
            cs.Libelle = Convert.ToString(r["libelle"]).Trim();
            cs.Code = TemplateApctePGMgmt.getCodePrestation(Convert.ToString(r["codePrestation"]).Trim());
            cs.Coeff = Convert.ToInt32(r["acteCoeff"]);
            cs.CoeffDecompose = Convert.ToString(r["decomp"]);
            cs.IsDecomposed = Convert.ToBoolean(r["decompisvisible"]);
            cs.NeedDEP = r["needDep"].ToString() == "" ? false : Convert.ToInt16(r["needDep"]) == 1;
            cs.NeedFS = r["needFse"].ToString() == "" ? false : Convert.ToInt16(r["needFse"]) == 1;
            cs.NBJours = r["nbJours"].ToString() == "" ? null : (int?)Convert.ToInt32(r["nbJours"]);
            cs.NBMois = r["nbMois"].ToString() == "" ? null : (int?)Convert.ToInt32(r["nbMois"]);
            cs.Valeur = Convert.ToDouble(r["valeur"]);
            cs.Organisation = r["organisation"].ToString() == "" ? "" : Convert.ToString(r["organisation"]);
            cs.phase = r["phase"].ToString() == "" ? Traitement.EnumPhase.Aucune : ((Traitement.EnumPhase)Convert.ToInt32(r["phase"]));
            cs.TypeDeReglement = r["typedereglement"].ToString() == "" ? ActePG.TypeReglement.Forfaitaire : (ActePG.TypeReglement)Convert.ToChar(r["typedereglement"]);
            cs.VALEUR_CMU = r["valeurCmu"].ToString() == "" ? 0 : Convert.ToDouble(r["valeurCmu"]);


            return cs;
        }
    }
}
