using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.Data;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL.Builders
{
    public static class BuildStatus
    {
        public static BasCommon_BO.UserStatus BuildUserStatus(JObject r, BasCommon_BO.Utilisateur p_utilisateur)
        {

            int idStatus= Convert.ToInt32(r["idRhStatus"]);
            BasCommon_BO.Status status = BasCommon_BL.StatusMgt.Status.Find(x => x.Id == idStatus);

            BasCommon_BO.UserStatus userStatus = new UserStatus();

            userStatus.Id = Convert.ToInt32(r["ID"]);
            userStatus.dateStart = Convert.ToDateTime(r["dateStart"]);
            userStatus.dateEnd = Convert.ToDateTime(r["dateEnd"]);

            userStatus.utilisateur = p_utilisateur;
            status.Libelle = status.Libelle.Trim();
           
            status.IsAnAbsence = Convert.ToString(r["absence"]).Trim().ToUpper() == "Y" ? true : false;

            userStatus.status = status;


            return userStatus;
        }

        public static BasCommon_BO.UserStatus BuildUserStatusOld(DataRow r, BasCommon_BO.Utilisateur p_utilisateur)
        {
            BasCommon_BO.UserStatus status = new BasCommon_BO.UserStatus();
            status.status = new BasCommon_BO.Status();

            status.utilisateur = p_utilisateur;
            status.status.Id = Convert.ToInt32(r["ID_STATUS"]);
            status.status.Libelle = Convert.ToString(r["libelle"]).Trim();
            status.status.IsAnAbsence = Convert.ToString(r["absence"]).Trim() == "Y";
            status.dateStart = Convert.ToDateTime(r["date_status_start"]);
            status.dateEnd = Convert.ToDateTime(r["date_status_end"]);
            status.Id = Convert.ToInt32(r["ID"]);


            return status;
        }

        public static Status Build(JObject r)
        {
            Status status = new Status();

            status.Id = Convert.ToInt32(r["id"]);
            status.Libelle = Convert.ToString(r["libelle"]).Trim();
            status.IsAnAbsence = Convert.ToString(r["absence"]).Trim() == "Y";
            status.code = Convert.ToString(r["Code"]).Trim();
           
            return status;
        }

        public static Status BuildOld(DataRow r)
        {
            Status status = new Status();

            status.Id = Convert.ToInt32(r["id"]);
            status.Libelle = Convert.ToString(r["libelle"]).Trim();
            status.IsAnAbsence = Convert.ToString(r["absence"]).Trim() == "Y";
            status.code = Convert.ToString(r["Code"]).Trim();
            return status;
        }


    }
}
