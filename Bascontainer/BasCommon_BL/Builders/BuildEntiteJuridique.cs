using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;
using Newtonsoft.Json.Linq;
using MySql.Data.Types;

namespace BasCommon_BL.Builders
{

    public static class BuildEntiteJuridique
    {
        public static EntiteJuridique Build(DataRow r)
        {

            EntiteJuridique apg = new EntiteJuridique();
            apg.Id = r["ID"] is DBNull ? -1 : Convert.ToInt32(r["ID"]);
            apg.Nom = r["NOM"] is DBNull ? "" : Convert.ToString(r["NOM"]);
            apg.Adresse1 = r["ADRESSE1"] is DBNull ? "" : Convert.ToString(r["ADRESSE1"]);
            apg.Adresse2 = r["ADRESSE2"] is DBNull ? "" : Convert.ToString(r["ADRESSE2"]);
            apg.CodePostal = r["CODEPOSTAL"] is DBNull ? "" : Convert.ToString(r["CODEPOSTAL"]);
            apg.Ville = r["VILLE"] is DBNull ? "" : Convert.ToString(r["VILLE"]);

            apg.Telephone = r["Telephone"] is DBNull ? "" : Convert.ToString(r["Telephone"]);
            apg.Mail = r["Mail"] is DBNull ? "" : Convert.ToString(r["Mail"]);
            apg.SiteWeb = r["SiteWeb"] is DBNull ? "" : Convert.ToString(r["SiteWeb"]);
            apg.FormeSocial = r["FormeSocial"] is DBNull ? "" : Convert.ToString(r["FormeSocial"]);
            apg.DateCreation = r["DateCreation"] is DBNull ? null : (DateTime?)((MySqlDateTime)(r["DateCreation"]));
            apg.NumSIRET = r["NumSIRET"] is DBNull ? "" : Convert.ToString(r["NumSIRET"]);
            apg.RCS = r["RCS"] is DBNull ? "" : Convert.ToString(r["RCS"]);
            apg.NumOrdre = r["NumOrdre"] is DBNull ? "" : Convert.ToString(r["NumOrdre"]);
            apg.OrdreDe = r["OrdreDe"] is DBNull ? "" : Convert.ToString(r["OrdreDe"]);
            apg.Gerant = r["Gerant"] is DBNull ? "" : Convert.ToString(r["Gerant"]);
            apg.Collaborateur = r["Collaborateur"] is DBNull ? "" : Convert.ToString(r["Collaborateur"]);

            return apg;
        }
        public static EntiteJuridique BuildJ(JObject r)
        {

            EntiteJuridique apg = new EntiteJuridique();
            apg.Id =  Convert.ToInt32(r["id"]);
            apg.Nom =  Convert.ToString(r["nom"]);
            apg.Adresse1 =  Convert.ToString(r["adresse1"]);
            apg.Adresse2 =  Convert.ToString(r["adresse2"]);
            apg.CodePostal = Convert.ToString(r["codepostal"]);
            apg.Ville =  Convert.ToString(r["ville"]);

            apg.Telephone =  Convert.ToString(r["telephone"]);
            apg.Mail = Convert.ToString(r["mail"]);
            apg.SiteWeb = Convert.ToString(r["siteweb"]);
            apg.FormeSocial = Convert.ToString(r["formesocial"]);
            apg.DateCreation = r["datecreation"].ToString() == "" ? null : (DateTime?)((DateTime)(r["datecreation"]));
            apg.NumSIRET =  Convert.ToString(r["numsiret"]);
            apg.RCS =  Convert.ToString(r["rcs"]);
            apg.NumOrdre =  Convert.ToString(r["numordre"]);
            apg.OrdreDe = Convert.ToString(r["ordrede"]);
            apg.Gerant = Convert.ToString(r["gerant"]);
            apg.Collaborateur =  Convert.ToString(r["collaborateur"]);

            return apg;
        }
    }
}
