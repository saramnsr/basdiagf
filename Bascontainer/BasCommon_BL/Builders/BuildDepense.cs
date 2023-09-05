using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;

namespace BasCommon_BL.Builders
{

    public static class BuildDepense
    {

        public static Depense Build(DataRow r)
        {
            Depense obj = new Depense();

            obj.Id = r["ID"] == DBNull.Value ? -1 : Convert.ToInt32(r["ID"]);
            obj.DateDepense = Convert.ToDateTime(r["DATE_DEPENSE"]);
            obj.DateValeurBque = Convert.ToDateTime(r["DATE_VALEURBQUE"]);
            obj.Code = Convert.ToString(r["CODE_DEPENSE"]);
            obj.Montant = Convert.ToDouble(r["MONTANT"]);
            obj.ModeReglement = Convert.ToString(r["MODE_REGLEMENT"]);
            obj.banqueDeRemise = BanqueMgmt.getBanqueDeRemise(Convert.ToString(r["id_banque_remise"]));
            obj.Details = Convert.ToString(r["DETAILS"]);
            
            return obj;

        }

    }
}
