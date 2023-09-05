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

    public static class BuildCustomStatusClinique
    {
        public static CustomStatusClinique Build(DataRow r)
        {
            CustomStatusClinique customcat = new CustomStatusClinique();
            customcat.Id = Convert.ToInt32(r["ID"]);
            customcat.status = StatusCliniqueManuelMgmt.GetStatus(Convert.ToInt32(r["ID_STATUS"]));
            customcat.IdPersonne = Convert.ToInt32(r["ID_PATIENT"]);
            customcat.DateDebut = Convert.ToDateTime(r["DATEDEBUT"]);
            customcat.DateFin = (r["DATEFIN"] == DBNull.Value) ? null : (DateTime?)Convert.ToDateTime(r["DATEFIN"]);

            return customcat;
        }
        public static CustomStatusClinique BuildJ(JObject r)
        {
            CustomStatusClinique customcat = new CustomStatusClinique();
            customcat.Id = Convert.ToInt32(r["id"]);
            customcat.status = StatusCliniqueManuelMgmt.GetStatus(Convert.ToInt32(r["id_STATUS"]));
            customcat.IdPersonne = Convert.ToInt32(r["id_PATIENT"]);
            customcat.DateDebut = Convert.ToDateTime(r["datedebut"]);
            customcat.DateFin = (r["datefin"].ToString() == "") ? null : (DateTime?)Convert.ToDateTime(r["datefin"]);

            return customcat;
        }
    }
}
