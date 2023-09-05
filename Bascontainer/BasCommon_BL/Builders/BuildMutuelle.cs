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

    public static class BuildMutuelle
    {

        public static Mutuelle Build(DataRow r)
        {
            Mutuelle pa = new Mutuelle();
            pa.Id = r["ID_MUTUELLE"] is DBNull ? -1 : Convert.ToInt32(r["ID_MUTUELLE"]);
            pa.Nom = r["MUTUELLE_NOM"] is DBNull ? "" : Convert.ToString(r["MUTUELLE_NOM"]).Trim();
            pa.Telephone = r["MUTUELLE_TEL"] is DBNull ? "" : Convert.ToString(r["MUTUELLE_TEL"]).Trim();

            pa.Ville = Convert.ToString(r["ville_nom"]).Trim();
            pa.CodePostal = Convert.ToString(r["ville_cpostal"]).Trim();
            pa.Adresse_Num = Convert.ToString(r["adr_numero"]).Trim();
            pa.Adresse_Type_Voie = Convert.ToString(r["adr_typevoie"]).Trim();
            pa.Adresse_Nom_Voie = Convert.ToString(r["adr_nomvoie"]).Trim();
            pa.Adresse2 = Convert.ToString(r["adr_complement"]).Trim();
            pa.IdAdresse = r["ID_ADRESSE"] is DBNull?-1:Convert.ToInt32(r["ID_ADRESSE"]);
            pa.IdVille = r["ID_VILLE"] is DBNull ? -1 : Convert.ToInt32(r["ID_VILLE"]);

            pa.IsCMU = r["ISCMU"] is DBNull ? false : Convert.ToBoolean(r["ISCMU"]);
            pa.IsTiersPayant = r["ISTIERPAYANT"] is DBNull ? false : Convert.ToBoolean(r["ISTIERPAYANT"]);
            pa.NumMutuelle = r["NUM_MUTUELLE"] is DBNull ? "" : Convert.ToString(r["NUM_MUTUELLE"]);
            pa.NeedOrder = r["NEEDORDER"] is DBNull ? false : Convert.ToBoolean(r["NEEDORDER"]);
            

            pa.MontantPlafond = r["MontantPlafond"] is DBNull ? 100000 : Convert.ToDouble(r["MontantPlafond"]);
            pa.TauxParDefaut = r["TAUXREMBPARDEFAUT"] is DBNull ? 100 : Convert.ToDouble(r["TAUXREMBPARDEFAUT"]);
            return pa;

        }


        public static Mutuelle BuildJ(JObject r)
        {
            Mutuelle pa = new Mutuelle();
            pa.Id =  Convert.ToInt32(r["id"]);
            pa.Nom = Convert.ToString(r["nom"]).Trim();
            pa.Telephone = Convert.ToString(r["telephone"]).Trim();

            pa.Ville = Convert.ToString(r["ville"]).Trim();
            pa.CodePostal = Convert.ToString(r["codePostal"]).Trim();
            pa.Adresse_Num = Convert.ToString(r["adresseNum"]).Trim();
            pa.Adresse_Type_Voie = Convert.ToString(r["adresseTypVoie"]).Trim();
            pa.Adresse_Nom_Voie = Convert.ToString(r["adresseNomVoie"]).Trim();
            pa.Adresse2 = Convert.ToString(r["adresseCompl"]).Trim();
            pa.IdAdresse = r["idAdresse"].ToString() == "" ? -1 : Convert.ToInt32(r["idAdresse"]);
            pa.IdVille = r["idVille"].ToString() == "" ? -1 : Convert.ToInt32(r["idVille"]);

            pa.IsCMU = r["isCMU"].ToString() == "" || r["isCMU"].ToString() == "0"? false : Convert.ToBoolean(Convert.ToString(r["isCMU"]));
            pa.IsTiersPayant = r["isTiersPayant"].ToString() == "" || r["isTiersPayant"].ToString() == "0" ? false : Convert.ToBoolean(r["isTiersPayant"].ToString());
            pa.NumMutuelle = r["numMutuelle"].ToString() == "" ? "" : Convert.ToString(r["numMutuelle"]);
            if (r["needOrder"].ToString() == "" || r["needOrder"].ToString() == "0")
            {
                pa.NeedOrder = false;
            }
            else if (r["needOrder"].ToString() == "1")
            {
                pa.NeedOrder = true;
            }
            else 
            {
                pa.NeedOrder = Convert.ToBoolean(r["needOrder"].ToString());
            }
            pa.MontantPlafond = r["montantPlafond"].ToString() == "" ? 100000 : Convert.ToDouble(r["montantPlafond"]);
            pa.TauxParDefaut = r["tauxParDefaut"].ToString() == "" ? 100 : Convert.ToDouble(r["tauxParDefaut"]);
            return pa;

        }

    }
}
