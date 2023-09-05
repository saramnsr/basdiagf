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

    
    public static class BuildTypePersonne
    {

        public static TypePersonne BuildJson(JObject o)
        {
            TypePersonne tp = new TypePersonne();
            tp.Id = Convert.ToInt32(o["idType"]);
            tp.Nom = Convert.ToString(o["nom"]);
            tp.DisplayOrder = o["displayorder"].ToString() == "" ? 0 : Convert.ToInt32(o["displayorder"]);
            tp.Categorie = Convert.ToString(o["categorie"]);
            return tp;
        }


        public static TypePersonne Build(DataRow dr)
        {
            TypePersonne tp = new TypePersonne();
            tp.Id = Convert.ToInt32(dr["ID"]);
            tp.Nom = Convert.ToString(dr["NOM"]);
            tp.DisplayOrder = dr["DisplayOrder"] is DBNull?0: Convert.ToInt32(dr["DisplayOrder"]);
            tp.Categorie = Convert.ToString(dr["CATEGORIE"]);
            return tp;
        }
    }
}
