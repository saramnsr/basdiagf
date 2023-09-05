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

    public static class BuildCodePresta
    {

        public static CodePrestation Build(DataRow r)
        {
            CodePrestation cs = new CodePrestation();
            cs.Code = Convert.ToString(r["ID_PRESTATION"]).Trim();
            cs.Libelle = Convert.ToString(r["LIBELLE"]).Trim();
            cs.Valeur = Convert.ToDouble(r["VALEUR_CLE_EURO"]);
            return cs;
        }
        public static CodePrestation Build(JObject r)
        {
            CodePrestation cs = new CodePrestation();
            cs.Code = Convert.ToString(r["id_prestation"]).Trim();
            cs.Libelle = Convert.ToString(r["libelle"]).Trim();
            cs.Valeur = Convert.ToDouble(r["valeur_cle_euro"]);
            return cs;
        }
    }
}
