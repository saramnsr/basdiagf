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

    public static class BuildPointage
    {

        public static Pointage BuildJson(JObject r)
        {

            Pointage p = new Pointage();

            p.Id = Convert.ToInt32(r["id"]);
            p.DateTimePointage = Convert.ToDateTime(r["datePointage"]);
            p.IdUser = Convert.ToInt32(r["idUtilisateur"]);
            p.sens = (Pointage.SensPointage)Convert.ToInt32(r["entreeSortie"]);
            p.user = BasCommon_BL.UtilisateursMgt.getUtilisateur(p.IdUser);
            return p;
        }
        
        public static Pointage Build(DataRow r)
        {
            //DATEPOINTAGE,ID,ID_UTILISATEUR,ENTRESORTIE,ID_UTILISATEUR
            Pointage obj = null;

            obj = new Pointage();
            //obj.Id = Convert.ToInt32(r["ID"]);
            obj.DateTimePointage = Convert.ToDateTime(r["DATEPOINTAGE"]);
            obj.IdUser = Convert.ToInt32(r["ID_UTILISATEUR"]);
            obj.sens = (Pointage.SensPointage)Convert.ToInt32(r["ENTRESORTIE"]);
            obj.user = BasCommon_BL.UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["ID_UTILISATEUR"])); 
            return obj;
        }


        public static Pointage BuildUserJson(JObject r)
        {

            Pointage obj = null;

            obj = new Pointage();
            obj.Id = Convert.ToInt32(r["orderCol"]);
            obj.DateTimePointage = Convert.ToDateTime(r["datepointage"]);
            obj.IdUser = Convert.ToInt32(r["id_utilisateur"]);
            obj.sens = (Pointage.SensPointage)Convert.ToInt32(r["entresortie"]);
            obj.user = BasCommon_BL.UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["id_utilisateur"]));
            return obj;
        }


        public static Pointage BuildUser(DataRow r)
        {

            Pointage obj = null;

            obj = new Pointage();
            obj.Id = Convert.ToInt32(r["ORDERCOL"]);
            obj.DateTimePointage = Convert.ToDateTime(r["DATEPOINTAGE"]);
            obj.IdUser = Convert.ToInt32(r["ID_UTILISATEUR"]);
            obj.sens = (Pointage.SensPointage)Convert.ToInt32(r["ENTRESORTIE"]);
            obj.user  = BasCommon_BL.UtilisateursMgt.getUtilisateur(Convert.ToInt32(r["ID_UTILISATEUR"]));
            return obj;
        }


    }
}
