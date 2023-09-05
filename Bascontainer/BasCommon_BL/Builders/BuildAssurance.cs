using BasCommon_BO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL.Builders
{
    public class BuildAssurance
    {
        public static Assurance Build(DataRow r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            Assurance asr = new Assurance();
            asr.Id = r["id"] is DBNull ? -1 : Convert.ToInt32(r["id"]);
            asr.libelle = r["libelle"] is DBNull ? "" : Convert.ToString(r["libelle"]).Trim();
            asr.montant = r["montant"] is DBNull ? -1 : Convert.ToDouble(r["montant"]);
            asr.pourcentage = r["pourcentage"] is DBNull ? -1 : Convert.ToInt32(r["pourcentage"]);
            asr.idPatient = r["id_patient"] is DBNull ? -1 : Convert.ToInt32(r["id_patient"]);
            asr.Partmontpat = r["partmontpat"].ToString() == "" ? 0 : Convert.ToDouble(r["partmontpat"]);
            asr.Partcentpat = r["partcentpat"].ToString() == "" ? 0 : Convert.ToDouble(r["partcentpat"]);
            return asr;
        }
        public static Assurance Build(JObject r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            Assurance asr = new Assurance();
            asr.Id = r["id"].ToString() == "" ? -1 : Convert.ToInt32(r["id"]);
            asr.libelle = r["libelle"].ToString() == "" ? "" : Convert.ToString(r["libelle"]).Trim();
            asr.montant = r["montant"].ToString() == "" ? -1 : Convert.ToDouble(r["montant"]);
            asr.pourcentage = r["pourcentage"].ToString() == "" ? -1 : Convert.ToInt32(r["pourcentage"]);
            asr.idPatient = r["idPatient"].ToString() == "" ? -1 : Convert.ToInt32(r["idPatient"]);
            asr.Partmontpat = r["partmontpat"].ToString() == "" ? 0 : Convert.ToDouble(r["partmontpat"]);
            asr.Partcentpat = r["partcentpat"].ToString() == "" ? 0 : Convert.ToDouble(r["partcentpat"]);

            return asr;
        }
    }
}
