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

    public static class BuildSurveillance
    {
        public static Surveillance Build(DataRow r)
        {
            Surveillance su = new Surveillance();
            su.DateDebut = SysTools.DataRow_ValueDateTime(r, "DATEDEBUT");
            su.DateFin = SysTools.DataRow_ValueDateTime(r, "DATEFIN");
            su.Id = SysTools.DataRow_ValueInt(r, "ID");
            su.Montant_Honoraire = SysTools.DataRow_ValueDouble(r, "MONTANT");
            su.traitementSecu = r["ID_TRAITMNTSECU"] is DBNull ? null : TemplateApctePGMgmt.getTemplatesActeGestion(Convert.ToInt32(r["ID_TRAITMNTSECU"]));
            su.IdSemestre = SysTools.DataRow_ValueInt(r, "ID_SEMESTRE");

            return su;

        }
        public static Surveillance BuildJ(JObject dr)
        {
            Surveillance su = new Surveillance();
            su.DateDebut = dr["datedebut"].ToString() == "" ? DateTime.MinValue : (DateTime?)Convert.ToDateTime(dr["datedebut"]);
            su.DateFin =  dr["datefin"].ToString() == "" ? DateTime.MinValue : (DateTime?) Convert.ToDateTime(dr["datefin"]);
            su.Id = dr["id"].ToString() == "" ? -1 : Convert.ToInt32(dr["id"]);
            su.Montant_Honoraire = dr["montant"].ToString() == "" ? 0 : Convert.ToDouble(dr["montant"]);
            su.traitementSecu = dr["idTraitmntsecu"].ToString() == "" ? null : TemplateApctePGMgmt.getTemplatesActeGestion(Convert.ToInt32(dr["idTraitmntsecu"]));
            su.IdSemestre = dr["idSemestre"].ToString() == "" ? -1 : Convert.ToInt32(dr["idSemestre"]); 

            return su;

        }
    }
}
