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

    public static class BuildSmallPersonne
    {
        public static baseSmallPersonne Build(DataRow r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            baseSmallPersonne act = new baseSmallPersonne();
            act.Id = r["id_personne"] is DBNull ? -1 : Convert.ToInt32(r["id_personne"]);
            act.Nom = r["per_Nom"] is DBNull ? "" : Convert.ToString(r["per_Nom"]).Trim();
            act.Prenom = r["per_Prenom"] is DBNull ? "" : Convert.ToString(r["per_Prenom"]).Trim();
            Contact vMainTel = new Contact();
            Contact vMainMail = new Contact();
            vMainTel.Value  = r["PER_TELPRINC"] is DBNull ? "" : Convert.ToString(r["PER_TELPRINC"]).Trim();
            vMainMail.Value = r["PER_EMAIL"] is DBNull ? "" : Convert.ToString(r["PER_EMAIL"]).Trim();

            act.MainTel = vMainTel;
            act.MainMail = vMainMail;

        
            return act;
        }
      
        public static baseSmallPersonne BuildJson(JObject r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            baseSmallPersonne act = new baseSmallPersonne();
            act.Id = Convert.ToInt32(r.GetValue("id_personne"));
            act.Nom =  Convert.ToString(r.GetValue("per_nom")).Trim();
            act.Prenom = Convert.ToString(r.GetValue("per_prenom")).Trim();
           
            Contact vMainTel = new Contact();
            Contact vMainMail = new Contact();
            vMainTel.Value = Convert.ToString(r.GetValue("per_telprinc")).Trim();
            vMainMail.Value = Convert.ToString(r.GetValue("per_email")).Trim();
             

            act.MainTel = vMainTel;
            act.MainMail = vMainMail;


            return act;
        }

        public static baseSmallPersonne BuildMateriels(DataRow r)
        {
            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            baseSmallPersonne act = new baseSmallPersonne();
            act.Id = r["id_personne"] is DBNull ? -1 : Convert.ToInt32(r["id_personne"]);
            act.Nom = r["per_Nom"] is DBNull ? "" : Convert.ToString(r["per_Nom"]).Trim();
            act.Prenom = r["per_Prenom"] is DBNull ? "" : Convert.ToString(r["per_Prenom"]).Trim();
            act.TYPE_MATERIEL = r["TYPE_MATERIEL"] is DBNull ? -1 : Convert.ToInt32(r["TYPE_MATERIEL"]);

            return act;
        }

        public static baseSmallPersonne BuildJsonMat(JObject r)
        {
            baseSmallPersonne act = new baseSmallPersonne();
            act.Id = Convert.ToInt32(r.GetValue("id_personne"));
            act.Nom = Convert.ToString(r.GetValue("per_nom")).Trim();
            act.Prenom = Convert.ToString(r.GetValue("per_prenom")).Trim();
            act.TYPE_MATERIEL = Convert.ToInt32(r.GetValue("type_materiel"));

            return act;
        }


    }
}
