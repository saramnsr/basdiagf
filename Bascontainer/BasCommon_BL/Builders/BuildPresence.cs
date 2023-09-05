using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using Newtonsoft.Json.Linq;
namespace BasCommon_BL.Builders
{
    public static class BuildPresence
    {
        
        public static AffectationFauteuil BuildAffectationFauteuil(DataRow r)
        {
            AffectationFauteuil Ut = new AffectationFauteuil();
            Ut.Id = Convert.ToInt32(r["ID"]);
            Ut.Iduser = Convert.ToInt32(r["id_user"]);
            Ut.fauteuil = BasCommon_BL.Fauteuilsmgt.getfauteuil(Convert.ToInt32(r["id_fauteuil"]));
            Ut.DteFrom = (Convert.ToDateTime(r["affecte_from"]));
            Ut.DteTo = (Convert.ToDateTime(r["affecte_to"]));
            Ut.Remarque = (Convert.ToString(r["remarques"]));
            Ut.user = UtilisateursMgt.getUtilisateur(Ut.Iduser);
            return Ut;
        }
        public static AffectationFauteuil BuildAffectationFauteuilJ(JObject r)
        {
            AffectationFauteuil Ut = new AffectationFauteuil();
            Ut.Id = Convert.ToInt32(r["id"]);
            Ut.Iduser = Convert.ToInt32(r["id_USER"]);
            Ut.fauteuil = BasCommon_BL.Fauteuilsmgt.getfauteuil(Convert.ToInt32(r["id_FAUTEUIL"]));
            Ut.DteFrom = (Convert.ToDateTime(r["affecte_FROM"]));
            Ut.DteTo = (Convert.ToDateTime(r["affecte_TO"]));
            Ut.Remarque = (Convert.ToString(r["remarques"]));
            Ut.user = UtilisateursMgt.getUtilisateur(Ut.Iduser);
            return Ut;
        }

        public static JourFerie BuildJourFerie(DataRow r)
        {
            JourFerie jf = new JourFerie();
            jf.Dte = Convert.ToDateTime(r["dte"]);
            jf.Libelle = r["Libelle"] == DBNull.Value ? "" : Convert.ToString(r["Libelle"]);
            jf.Id = Convert.ToInt32(r["Id"]);
            return jf;
        }

        public  static JourFerie BuildJourFerieFromJObject(JObject obj)
        {
            JourFerie jour = new JourFerie();

            jour.Id =  Convert.ToInt32(obj["id"]);

            jour.Dte = Convert.ToDateTime(obj["date"]);
            jour.Libelle = String.IsNullOrEmpty(obj["libelle"].ToString()) ? "" : Convert.ToString(obj["libelle"]);

            return jour;
        }
    }
}
