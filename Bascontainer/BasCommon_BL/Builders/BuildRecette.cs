using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;

namespace BasCommon_BL.Builders
{

    public static class BuildRecette
    {

        public static Recette Build(DataRow r)
        {
            Recette obj = new Recette();

            obj.Id = r["ID"] == DBNull.Value ? -1 : Convert.ToInt32(r["ID"]);
            obj.DateRemiseEnBanque = Convert.ToDateTime(r["DATE_REMISEENBQUE"]);
            obj.DateValeurBque = Convert.ToDateTime(r["DATE_VALEURBQUE"]);
            obj.Code = Convert.ToString(r["CODE_Recette"]);
            obj.Montant = Convert.ToDouble(r["MONTANT"]);
            obj.IDPaiementReel = r["id_paiementreel"] is DBNull?-1: Convert.ToInt32(r["id_paiementreel"]);
            obj.IDBordereau = r["id_Bordereau"] is DBNull ? -1 : Convert.ToInt32(r["id_Bordereau"]);
            obj.banqueDeRemise = BanqueMgmt.getBanqueDeRemise(Convert.ToString(r["id_banque_remise"]));
            obj.Details = Convert.ToString(r["DETAILS"]);

            
            return obj;

        }

    }
}
