using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.Data;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL.Builders
{
    public static class BuildCorrespondantType
    {

        public static CorrespondantType Build(DataRow r)
        {
            CorrespondantType ct = new CorrespondantType();
            ct.Id = Convert.ToInt32(r["ID"]);
            ct.Nom = Convert.ToString(r["NOM"]);

            return ct;
        }

        public static CorrespondantType BuildJson(JObject r)
        {
            CorrespondantType ct = new CorrespondantType();
            ct.Id = Convert.ToInt32(r["idType"]).ToString() == "" ? -1 : Convert.ToInt32(r["idType"]);
            ct.Nom = Convert.ToString(r["nom"]);

            return ct;
        }
    }
}
