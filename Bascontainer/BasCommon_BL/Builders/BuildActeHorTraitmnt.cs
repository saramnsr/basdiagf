using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;

namespace BasCommon_BL.Builders
{

    public static class BuildActesHorstraitement
    {

        public static ActePGPropose Build(DataRow dr)
        {


            ActePGPropose co = new ActePGPropose();
            co.IdDevis = dr["ID_DEVIS"] is DBNull ? -1 : Convert.ToInt32(dr["ID_DEVIS"]); 
            co.DateExecution = dr["DATE_EXECUTION"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["DATE_EXECUTION"]);
            co.IdTemplateActePG = Convert.ToInt32(dr["ID_TEMPLATE_ACTE_GESTION"]);
            co.Montant = Convert.ToDouble(dr["MONTANT"]);
            co.MontantAvantRemise = dr["MontantAvantRemise"] is DBNull?co.Montant: Convert.ToDouble(dr["MontantAvantRemise"]);
            co.Optionnel = Convert.ToBoolean(dr["OPTIONAL"]);
            co.Libelle = Convert.ToString(dr["Libelle"]);
            co.Qte = Convert.ToInt32(dr["QTE"]);
            co.IdProposition = dr["ID_PROPOSITION"] is DBNull ? -1 : Convert.ToInt32(dr["ID_PROPOSITION"]);
            return co;

        }
    }
}
