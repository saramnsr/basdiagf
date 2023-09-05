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
    public static class BuildTypeDeDepense
    {
        public static TypeDeDepense Build(DataRow r)
        {
            TypeDeDepense obj = new TypeDeDepense();

            obj.Id = r["Id"] == DBNull.Value ? -1 : Convert.ToInt32(r["Id"]);
            obj.CodeComptable = r["CodeComptable"] == DBNull.Value ? null : BasCommon_BL.Compta.MgmtCodeComptable.getFromCode(Convert.ToString(r["CodeComptable"]));
            obj.Libelle = r["LIBELLE"] == DBNull.Value ? "" : Convert.ToString(r["LIBELLE"]);
            obj.organisation = r["organisation"] == DBNull.Value ? "" : Convert.ToString(r["organisation"]);
           

            return obj;

        }

        public static TypeDeDepense BuildJson(JObject r)
        {
            TypeDeDepense obj = new TypeDeDepense();

            obj.Id = r["id"].ToString() == "" ? -1 : Convert.ToInt32(r["id"]);
            obj.CodeComptable = r["codecomptable"].ToString() == "" ? null : BasCommon_BL.Compta.MgmtCodeComptable.getFromCode(Convert.ToString(r["codecomptable"]));
            obj.Libelle = r["libelle"].ToString() == "" ? "" : Convert.ToString(r["libelle"]);
            obj.organisation = r["organisation"].ToString() == "" ? "" : Convert.ToString(r["organisation"]);


            return obj;

        }
    }
}
