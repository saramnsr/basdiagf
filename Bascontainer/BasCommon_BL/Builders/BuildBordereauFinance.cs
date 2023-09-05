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
    public static class BuildBordereauFinance
    {


       

        public static BordereauFinance Build(DataRow dr)
        {
            BordereauFinance bf = new BordereauFinance();
            bf.Id = Convert.ToInt32(dr["ID"]);
            bf.BanqueDeRemise = BanqueMgmt.getBanqueDeRemise(Convert.ToString(dr["COD_BAN"]));
            bf.NumBordereau = Convert.ToString(dr["NUM_BORDEREAU"]);
            bf.NumBordereauBancaire = Convert.ToString(dr["NUM_BORDEREAU_BQE"]);

            bf.NbCheques = Convert.ToInt32(dr["NB_CHEQUE"]);
            bf.NbCBs = dr["NB_CB"] is DBNull ? 0 : Convert.ToInt32(dr["NB_CB"]);
            bf.NbVirements = dr["NB_VIREMENT"] is DBNull ? 0 : Convert.ToInt32(dr["NB_VIREMENT"]);
            bf.NbPrelevements = dr["NB_PRELEVEMENT"] is DBNull ? 0 : Convert.ToInt32(dr["NB_PRELEVEMENT"]);
            bf.Montant = Convert.ToDouble(dr["MONTANT_TOTAL"]);
            bf.DateRemise = dr["DATEREMISE"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["DATEREMISE"]);
            bf.DateValeur = dr["DATEDEVALIDATION"] is DBNull?null:(DateTime?)Convert.ToDateTime(dr["DATEDEVALIDATION"]);

            bf.Nbbillets5 = dr["NB_billets5"] is DBNull?0:Convert.ToInt32(dr["NB_billets5"]);
            bf.Nbbillets10 = dr["NB_billets10"] is DBNull ? 0 : Convert.ToInt32(dr["NB_billets10"]);
            bf.Nbbillets20 = dr["NB_billets20"] is DBNull ? 0 : Convert.ToInt32(dr["NB_billets20"]);
            bf.Nbbillets50 = dr["NB_billets50"] is DBNull ? 0 : Convert.ToInt32(dr["NB_billets50"]);
            bf.Nbbillets100 = dr["NB_billets100"] is DBNull ? 0 : Convert.ToInt32(dr["NB_billets100"]);
            bf.Nbbillets200 = dr["NB_billets200"] is DBNull ? 0 : Convert.ToInt32(dr["NB_billets200"]);
            bf.Nbbillets500 = dr["NB_billets500"] is DBNull ? 0 : Convert.ToInt32(dr["NB_billets500"]);
            
            return bf;
        }


        public static BordereauFinance BuildJson(JObject dr)
        {
            BordereauFinance bf = new BordereauFinance();
            bf.Id = Convert.ToInt32(dr["id"]);
            bf.BanqueDeRemise = BanqueMgmt.getBanqueDeRemise(Convert.ToString(dr["cod_ban"]));
            bf.NumBordereau = Convert.ToString(dr["num_bordereau"]);
            bf.NumBordereauBancaire = Convert.ToString(dr["num_bordereau_bqe"]);

            bf.NbCheques = Convert.ToInt32(dr["nb_cheque"]);
            bf.NbCBs = dr["nb_cb"].ToString() == "" ? 0 : Convert.ToInt32(dr["nb_cb"]);
            bf.NbVirements = dr["nb_virement"].ToString()=="" ? 0 : Convert.ToInt32(dr["nb_virement"]);
            bf.NbPrelevements = dr["nb_prelevement"].ToString() == "" ? 0 : Convert.ToInt32(dr["nb_prelevement"]);
            bf.Montant = Convert.ToDouble(dr["montant_total"]);
            bf.DateRemise = dr["dateremise"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(dr["dateremise"]);
            bf.DateValeur = dr["datedevalidation"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(dr["datedevalidation"]);

            bf.Nbbillets5 = dr["nb_billets5"].ToString() == "" ? 0 : Convert.ToInt32(dr["nb_billets5"]);
            bf.Nbbillets10 = dr["nb_billets10"].ToString() == "" ? 0 : Convert.ToInt32(dr["nb_billets10"]);
            bf.Nbbillets20 = dr["nb_billets20"].ToString() == "" ? 0 : Convert.ToInt32(dr["nb_billets20"]);
            bf.Nbbillets50 = dr["nb_billets50"].ToString() == "" ? 0 : Convert.ToInt32(dr["nb_billets50"]);
            bf.Nbbillets100 = dr["nb_billets100"].ToString() == "" ? 0 : Convert.ToInt32(dr["nb_billets100"]);
            bf.Nbbillets200 = dr["nb_billets200"].ToString() == "" ? 0 : Convert.ToInt32(dr["nb_billets200"]);
            bf.Nbbillets500 = dr["nb_billets500"].ToString() == "" ? 0 : Convert.ToInt32(dr["nb_billets500"]);

            return bf;
        }
    }
}
