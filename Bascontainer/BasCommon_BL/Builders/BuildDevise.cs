using BasCommon_BO;
using BasCommon_BO.Compta;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL.Builders
{
    public static class BuildDevise
    {
        public static Devise Build(DataRow r)
        {

            //code, libelle, cours, datecours

            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            Devise act = new Devise();
            act.CodeMonnaie =  Convert.ToString(r["code"]);
            act.LibelleMonnaie =Convert.ToString(r["libelle"]).Trim();
            act.Cours = Convert.ToDouble(r["cours"]);
            act.DateMAJCours = Convert.ToDateTime(r["datecours"]);
            return act;
        }


        public static Devise BuildJson(JObject r)
        {

            //code, libelle, cours, datecours

            //a.id_acte, acte_libelle, acte_durestd, acte_couleur, type_acte, nb_fautbloc, code_planing, temps_chrono
            Devise act = new Devise();
            act.CodeMonnaie = Convert.ToString(r["code"]).ToString() == "" ? "" : Convert.ToString(r["code"]).Trim();
            act.LibelleMonnaie = Convert.ToString(r["libelle"]).ToString().Trim() == "" ? "" : Convert.ToString(r["libelle"]).Trim();
            act.Cours = Convert.ToDouble(r["cours"]).ToString() == "" ? -1 : Convert.ToDouble(r["cours"]);
            act.DateMAJCours = Convert.ToDateTime(r["datecours"]);
            return act;
        }

    }
}
