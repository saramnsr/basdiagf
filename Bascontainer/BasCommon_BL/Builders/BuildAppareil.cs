using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;

namespace BasCommon_BL.Builders
{

    public static class BuildAppareil
    {

        public static Appareil Build(DataRow r)
        {
            Appareil obj = new Appareil();

            obj.Id = r["ID"] == DBNull.Value ? -1 : Convert.ToInt32(r["ID"]);
            obj.Libelle = r["LIBELLE"] == DBNull.Value ? "" : Convert.ToString(r["LIBELLE"]);
            obj.Code = r["CODE"] == DBNull.Value ? "" : Convert.ToString(r["CODE"]);
            obj.Risques = Convert.ToString(r["RISQUES"]).Split(',').ToList();
            obj.InfoDEP = Convert.ToString(r["InfoDEP"]);


            return obj;

        }

    }
}
