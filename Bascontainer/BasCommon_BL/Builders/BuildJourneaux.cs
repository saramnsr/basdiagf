using BasCommon_BO.Compta;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL.Builders
{
    public static class BuildJourneaux
    {
        public static Journal Build(DataRow r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            Journal act = new Journal();
            act.NumJournal = Convert.ToString(r["NumJournal"]);
            act.LibelleJournal = Convert.ToString(r["Libelle"]).Trim();
            return act;
        }
        public static Journal BuildJ(JObject r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            Journal act = new Journal();
            act.NumJournal = Convert.ToString(r["numjournal"]);
            act.LibelleJournal = Convert.ToString(r["libelle"]).Trim();
            return act;
        }
    }
}
