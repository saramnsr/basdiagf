using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.Data;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL.Builders
{


    public static class BuildPnFCheck
    {
        public static PnFCheck Build(DataRow r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            PnFCheck act = new PnFCheck();
            act.Id = r["id"] is DBNull ? -1 : Convert.ToInt32(r["id"]);
            act.IdPartPatient = r["ID_PART_PATIENT"] is DBNull ? -1 : Convert.ToInt32(r["ID_PART_PATIENT"]);
            act.IdPartBanque = r["ID_PART_BANQUE"] is DBNull ? -1 : Convert.ToInt32(r["ID_PART_BANQUE"]);
            act.Libelle = r["LIBELLE"] is DBNull ? "" : Convert.ToString(r["LIBELLE"]);
            act.Type = r["TYPE"] is DBNull ? "" : Convert.ToString(r["TYPE"]);
            act.Date = Convert.ToDateTime(r["DATE"]);
            act.Montant = Convert.ToDouble(r["MONTANT_TOTAL"]);
            act.IdPatient = Convert.ToInt32(r["IDPATIENT"]);
            act.Payeur = Convert.ToString(r["payeur"]);
            act.EntiteJuridique = EntiteJuridiqueMgmt.getentite(Convert.ToInt32(r["ID_ENTITYJURIDIQUE"]));
            act.BanqueDeRemise = BanqueMgmt.getBanquesDeRemise(act.EntiteJuridique)[0];
            act.NomPatient = Convert.ToString(r["PATIENT"]);
            return act;

        }
        public static PnFCheck BuildJ(JObject r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            PnFCheck act = new PnFCheck();
            act.Id = r["id"].ToString() == "" ? -1 : Convert.ToInt32(r["id"]);
            act.IdPartPatient = r["idPartPatient"].ToString() == "" ? -1 : Convert.ToInt32(r["idPartPatient"]);
            act.IdPartBanque = r["idPartBanque"].ToString() == "" ? -1 : Convert.ToInt32(r["idPartBanque"]);
            act.Libelle = r["libelle"].ToString() == "" ? "" : Convert.ToString(r["libelle"]);
            act.Type = r["type"].ToString() == "" ? "" : Convert.ToString(r["type"]);
            act.Date = Convert.ToDateTime(r["date"]);
            act.Montant = Convert.ToDouble(r["montantTotal"]);
            act.IdPatient = Convert.ToInt32(r["idpatient"]);
            act.Payeur = Convert.ToString(r["payeur"]);
            act.EntiteJuridique = EntiteJuridiqueMgmt.getentite(Convert.ToInt32(r["IdEntiteJuridique"]));
            act.BanqueDeRemise = BanqueMgmt.getBanquesDeRemise(act.EntiteJuridique)[0];
            act.NomPatient = Convert.ToString(r["patient"]);
            return act;

        }
    }


}
