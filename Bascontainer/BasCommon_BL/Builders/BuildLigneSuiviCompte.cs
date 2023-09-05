using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;

namespace BasCommon_BL.Builders
{

    public static class BuildLigneSuiviCompte
    {



        public static LigneSuiviCompte Build(DataRow dr)
        {
            LigneSuiviCompte b = new LigneSuiviCompte();
            b.Id = Convert.ToInt32(dr["ID"]);
            b.entite = EntiteJuridiqueMgmt.getentite(Convert.ToInt32(dr["ID_ENTITE"]));
            b.BanqueDeRemise = BanqueMgmt.getBanqueDeRemise(Convert.ToString(dr["ID_BANQUE"]));
            b.Libelle = Convert.ToString(dr["LIBELLE"]);
            b.DateCabinet = dr["DATECABINET"] is DBNull?null: (DateTime?)Convert.ToDateTime(dr["DATECABINET"]);
            b.DateBanque = dr["DATEBANQUE"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["DATEBANQUE"]);

            b.Recette = dr["RECETTE"] is DBNull ? null : (Double?)Convert.ToDouble(dr["RECETTE"]);
            b.Depense = dr["DEPENSE"] is DBNull ? null : (Double?)Convert.ToDouble(dr["DEPENSE"]);
            b.IdPaiement = dr["ID_PAIEMENT"] is DBNull ? -1 : Convert.ToInt32(dr["ID_PAIEMENT"]);
            b.IdDepense = dr["ID_DEPENSE"] is DBNull?-1: Convert.ToInt32(dr["ID_DEPENSE"]);

            return b;
        }
    }
}
