using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;

namespace BasCommon_BL.Builders
{

    public static class BuildlnkControlFinancier
    {

        public static lnkControlFinancier Build(DataRow dr)
        {

            lnkControlFinancier ctrl = new lnkControlFinancier();
            ctrl.IdControlFinancier = Convert.ToInt32(dr["ID_CONTROL"]);
            ctrl.IdPaiementReel = dr["ID_PAIEMENT"] is DBNull ? -1 : Convert.ToInt32(dr["ID_PAIEMENT"]);
            ctrl.IdEcheance = dr["ID_ECHEANCE"] is DBNull ? -1 : Convert.ToInt32(dr["ID_ECHEANCE"]);
            ctrl.Remarques = Convert.ToString(dr["REMARQUE"]);
            ctrl.UpdateDate = Convert.ToDateTime(dr["UDATEDATE"]);
            ctrl.CodeErreur = dr["CODE_ERREUR"] is DBNull ? "" : Convert.ToString(dr["CODE_ERREUR"]);
            
            return ctrl;
        }
    }
}
