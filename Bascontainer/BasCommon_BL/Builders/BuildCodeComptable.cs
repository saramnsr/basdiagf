using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;
using BasCommon_BO.Compta;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL.Builders
{

    public static class BuildCodeComptable
    {
        public static CodeComptable Build(DataRow r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            CodeComptable act = new CodeComptable();
            act.Code = Convert.ToString(r["CODECOMPTA"]).Trim();
            act.Libelle = Convert.ToString(r["LIBELLE_COMPTA"]).Trim();
           return act;
        }

        public static CodeComptable BuildJson(JObject r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            CodeComptable act = new CodeComptable();
            act.Code = Convert.ToString(r["codecompta"]).Trim().ToString() == "" ? "" : Convert.ToString(r["codecompta"]).Trim();
            act.Libelle = Convert.ToString(r["libelle_compta"]).Trim().ToString() == "" ? "" : Convert.ToString(r["libelle_compta"]).Trim();
            return act;
        }
    }

}
