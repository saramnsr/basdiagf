using BasCommon_BO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL.Builders
{
    public class BuildRendement
    {
        public static Rendemenet Build(DataRow r)
        {

            Rendemenet obj = null;
            obj = new Rendemenet();
            obj.Somme = Convert.ToDouble (r["ADD"]);
       //     obj.DateRDV = Convert.ToDateTime(r["RDV_DATE"]);
            obj.Order = Convert.ToInt32(r["ORDERCOL"]);
      
            return obj;
        }

        public static Rendemenet BuildJson(JObject r)
        {

            Rendemenet obj = null;
            obj = new Rendemenet();
            obj.Somme = Convert.ToDouble(r["somme"]);
            //     obj.DateRDV = Convert.ToDateTime(r["RDV_DATE"]);
            obj.Order = Convert.ToInt32(r["ordercol"]);

            return obj;
        }
    }
}
