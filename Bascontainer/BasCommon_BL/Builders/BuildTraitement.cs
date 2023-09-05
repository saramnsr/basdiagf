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

    public static class BuildTraitement
    {
        public static Traitement Build(DataRow r)
        {

            Traitement tt = new Traitement();
            tt.Id = Convert.ToInt32(r["ID"]);
            tt.Libelle = Convert.ToString(r["LIBELLE"]);
            tt.Phase = ((BasCommon_BO.Traitement.EnumPhase)Convert.ToInt32(r["PHASE"]));
            tt.CodeTraitement = r["CodeTraitement"] is DBNull ? "" : Convert.ToString(r["CodeTraitement"]);
            tt.IdProposition = r["id_proposition"] is DBNull ? -1 : Convert.ToInt32(r["id_proposition"]);


            return tt;
        }
        public static Traitement BuildJ(JObject r)
        {

            Traitement tt = new Traitement();
            tt.Id = Convert.ToInt32(r["id"]);
            tt.Libelle = Convert.ToString(r["libelle"]);
            tt.Phase = ((BasCommon_BO.Traitement.EnumPhase)Convert.ToInt32(r["phase"]));
            tt.CodeTraitement = r["codetraitement"].ToString() == "" ? "" : Convert.ToString(r["codetraitement"]);
            tt.IdProposition = r["idProposition"].ToString() == "" ? -1 : Convert.ToInt32(r["idProposition"]);


            return tt;
        }
    }
}
