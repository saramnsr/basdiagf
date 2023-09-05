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

    public static class BuildCaisse
    {

        public static Caisse Build(DataRow r)
        {
            Caisse Caiss = new Caisse();
            Caiss.Id = Convert.ToInt32(r["ID_CAISSE"]);
            Caiss.Nom = Convert.ToString(r["CAISSE_NOM"]).Trim();
            Caiss.TelFixe = Convert.ToString(r["CAISSE_TEL"]).Trim();

            Caiss.Ville = Convert.ToString(r["ville_nom"] is DBNull ? "" : r["ville_nom"]).Trim();
            Caiss.CodePostal = Convert.ToString(r["ville_cpostal"] is DBNull ? "" : r["ville_cpostal"]).Trim();
            Caiss.Adresse_Num = Convert.ToString(r["adr_numero"] is DBNull ? "" : r["adr_numero"]).Trim();
            Caiss.Adresse_Type_Voie = Convert.ToString(r["adr_typevoie"] is DBNull ? "" : r["adr_typevoie"]).Trim();
            Caiss.Adresse_Nom_Voie = Convert.ToString(r["adr_nomvoie"] is DBNull ? "" : r["adr_nomvoie"]).Trim();
            Caiss.Adresse2 = Convert.ToString(r["adr_complement"] is DBNull ? "" : r["adr_complement"]).Trim();
            Caiss.IdAdresse = Convert.ToInt32(r["ID_ADRESSE"] is DBNull ? "" : r["ID_ADRESSE"]);
            Caiss.IdVille = Convert.ToInt32(r["ID_VILLE"] is DBNull ? "0" :r["ID_VILLE"] );
            Caiss.NeedOrder = r["NEEDORDER"] is DBNull?false:Convert.ToBoolean(r["NEEDORDER"]);
            
            Caiss.IsCMU = r["ISCMU"] is DBNull ? false : Convert.ToBoolean(r["ISCMU"]);

            return Caiss;
        }
        public static Caisse BuildJ(JObject r)
        {
            Caisse Caiss = new Caisse();
            Caiss.Id = Convert.ToInt32(r["idCaisse"]);
            Caiss.Nom = Convert.ToString(r["caissenom"]).Trim();
            Caiss.TelFixe = Convert.ToString(r["telephone"]).Trim();

            Caiss.Ville = Convert.ToString(r["ville"].ToString() == "" ? "" : r["ville"]).Trim();
            Caiss.CodePostal = Convert.ToString(r["codePostal"].ToString() == "" ? "" : r["codePostal"]).Trim();
            Caiss.Adresse_Num = Convert.ToString(r["adresseNum"].ToString() == "" ? "" : r["adresseNum"]).Trim();
            Caiss.Adresse_Type_Voie = Convert.ToString(r["adresseTypVoie"].ToString() == "" ? "" : r["adresseTypVoie"]).Trim();
            Caiss.Adresse_Nom_Voie = Convert.ToString(r["adresseNomVoie"].ToString() == "" ? "" : r["adresseNomVoie"]).Trim();
            Caiss.Adresse2 = Convert.ToString(r["adresseCompl"].ToString() == "" ? "" : r["adresseCompl"]).Trim();
            Caiss.IdAdresse = Convert.ToInt32(r["idAdresse"].ToString() == "" ? "" : r["idAdresse"]);
            Caiss.IdVille = Convert.ToInt32(r["idVille"].ToString() == "" ? "0" : r["idVille"]);
            Caiss.NeedOrder = r["needOrder"].ToString() == "" || r["needOrder"].ToString() == "0" ? false : Convert.ToBoolean(r["needOrder"]);

            Caiss.IsCMU = r["isCMU"].ToString() == "" || r["isCMU"].ToString() == "0" ? false : Convert.ToBoolean(r["isCMU"]);

            return Caiss;
        }
    }
}
