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
    public static class BuildSmilers
    {
        public static InfoSmilers Build(JObject obj)
        {
            InfoSmilers sm = new InfoSmilers();

            sm.idPatient = obj["idpatient"].ToString() == "" ? -1 : Convert.ToInt32(obj["idpatient"]);
            sm.orderid = obj["orderid"].ToString() == "" ? -1 : Convert.ToInt32(obj["orderid"]);
            sm.numdossier = obj["numdossier"].ToString() == "" ? "" : Convert.ToString(obj["numdossier"]);
            sm.faitpar = obj["faitpar"].ToString() == "" ? -1 : Convert.ToInt32(obj["faitpar"]);
            sm.validepar = obj["validepar"].ToString() == "" ? -1 : Convert.ToInt32(obj["validepar"]);
            sm.datesaisie = obj["datesaisie"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(obj["datesaisie"]);
            sm.comment = obj["comment"].ToString() == "" ? "" : Convert.ToString(obj["comment"]);
            sm.comment = obj["pack"].ToString() == "" ? "" : Convert.ToString(obj["pack"]);
            sm.comment = obj["finition"].ToString() == "" ? "" : Convert.ToString(obj["finition"]);
            return sm;
        }
       

        
    }



}
