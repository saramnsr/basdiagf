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

    public static class BuildRecontact
    {

        public static Recontact Build(DataRow r)
        {
            Recontact rec = new Recontact();
            rec.Id = SysTools.DataRow_ValueInt(r, "ID");
            rec.IdPatient = SysTools.DataRow_ValueInt(r, "ID_PATIENT");
            rec.Motif = SysTools.DataRow_ValueString(r, "MOTIF");
            rec.ARecontacterDepuisLe = SysTools.DataRow_ValueDateTime(r, "ARECONTACTERDEPUISLE");
            rec.NumTentative = SysTools.DataRow_ValueInt(r, "NUMTENTATIVE");
            rec.IdUserTentative = SysTools.DataRow_ValueInt(r, "ID_USERTENTATIVE");
            rec.DateTentative = r["DATETENTATIVE"] is DBNull ? null : (DateTime?)SysTools.DataRow_ValueDateTime(r, "DATETENTATIVE");
            rec.DateProchaineTentative = r["DATEPROCHAINETENTATIVE"] is DBNull ? null : (DateTime?)SysTools.DataRow_ValueDateTime(r, "DATEPROCHAINETENTATIVE");
            rec.Creator = SysTools.DataRow_ValueString(r, "CREATOR");
            rec.moyen = r["MOYEN"] is DBNull ? Recontact.Moyen.Telephone : (Recontact.Moyen)SysTools.DataRow_ValueInt(r, "MOYEN");
            rec.moyenProchaineTentative = r["MOYENPROCHAINETENTATIVE"] is DBNull ? Recontact.Moyen.Telephone : (Recontact.Moyen)SysTools.DataRow_ValueInt(r, "MOYENPROCHAINETENTATIVE");
            rec.usertentative = BasCommon_BL.UtilisateursMgt.getUtilisateur(rec.IdUserTentative);

            return rec;
        }
        public static Recontact BuildJ(JObject r)
        {
            Recontact rec = new Recontact();
            rec.Id = Convert.ToInt32(r["id"]);
            rec.IdPatient = Convert.ToInt32(r["idPatient"]);
            rec.Motif = r["motif"].ToString();
            rec.ARecontacterDepuisLe = r["arecontacterDepuisLe"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(r["arecontacterDepuisLe"]);
            rec.NumTentative = r["numtentative"].ToString() == "" ? 0 : Convert.ToInt32(r["numtentative"]);
            rec.IdUserTentative = r["idUserTentative"].ToString() == "" ? -1 : Convert.ToInt32(r["idUserTentative"]);
            rec.DateTentative = r["arecontacterDepuisLe"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(r["dateTentative"]);
            rec.DateProchaineTentative = r["dateProchaineTentative"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(r["dateProchaineTentative"]);
            rec.Creator = r["creator"].ToString();
            rec.moyen = r["moyen"].ToString() == "" ? Recontact.Moyen.Telephone : (Recontact.Moyen)Convert.ToInt32(r["moyen"]);
            rec.moyenProchaineTentative = r["moyenProchaineTentative"].ToString() == "" ? Recontact.Moyen.Telephone : (Recontact.Moyen)Convert.ToInt32(r["moyenProchaineTentative"]);
            rec.usertentative = BasCommon_BL.UtilisateursMgt.getUtilisateur(rec.IdUserTentative);

            return rec;
        }
    }
}
