using BasCommon_BO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL.Builders
{
    public class BuildPays
    {
        public static Pays Build(DataRow r)
        {
            Pays pays = new Pays();
            pays.id = Convert.ToInt32(r["ID_PAYS"]);
            pays.ordre = r["ORDRE"] is DBNull ? -1 : Convert.ToInt32(r["ORDRE"]);
            pays.nom = Convert.ToString(r["NOM"]).Trim();
            pays.shortName = r["CODE"] is DBNull ? "" : Convert.ToString(r["CODE"]).Trim();
            pays.indicatif = Convert.ToString(r["INDICATIF_TELEPHONIQUE"]).Trim();
                    return pays;
        }

        public static Pays BuildJson(JObject r)
        {
            Pays pays = new Pays();
            pays.id = Convert.ToInt32(r["id_pays"]);
            pays.ordre = r["ordre"].ToString() == "" ? -1 : Convert.ToInt32(r["ordre"]);
            pays.nom = Convert.ToString(r["nom"]).Trim();
            pays.shortName = r["code"].ToString() == "" ? "" : Convert.ToString(r["code"]).Trim();
            pays.indicatif = Convert.ToString(r["indicatif_telephonique"]).Trim();
            return pays;
        }
    }
}
