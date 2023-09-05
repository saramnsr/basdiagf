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

    public static class BuildAttribut
    {

        public static Attribut Build(DataRow r)
        {

            Attribut obj = null;

            obj = new Attribut();
            obj.Id = Convert.ToInt32(r["PK_ATTRIBUT"]);
            obj.Nom = Convert.ToString(r["NOM"]);


            obj.Valeur = Convert.ToString(r["VALEUR"]);


            return obj;
        }

        public static Attribut BuildAttributJson(JObject r)
        {
            if (r == null) return null;
            Attribut a = new Attribut();
            a.Id = Convert.ToInt32(r["pk_attribut"]);
            a.Nom = Convert.ToString(r["nom"]);
            a.Valeur = Convert.ToString(r["valeur"]);
            return a;
        }

    }
}
