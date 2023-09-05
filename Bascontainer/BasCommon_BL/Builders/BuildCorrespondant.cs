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

    public static class BuildCorrespondant
    {

        public static Correspondant Build(DataRow r)
        {
            Correspondant Corres = new Correspondant();
            Corres.Id = Convert.ToInt32(r["ID_PERSONNE"]);
            Corres.Nom = Convert.ToString(r["PER_NOM"]).Trim();
            Corres.Prenom = Convert.ToString(r["PER_PRENOM"]).Trim();



           Corres.Type = Convert.ToInt32(r["TYPE"]);
            Corres.Notes = Convert.ToString(r["PER_NOTES"]);
            Corres.GenreFeminin = r["PER_GENRE"] is DBNull ? true : Convert.ToChar(r["PER_GENRE"]) == 'F';
            Corres.Titre = Convert.ToString(r["PERS_TITRE"]);
            Corres.PrefCom = r["PREF_COM"] is DBNull ? Correspondant.EnumPrefCom.Courrier : (Correspondant.EnumPrefCom)Convert.ToString(r["PREF_COM"])[0];

            Corres.TuToiement = r["TUVOUS"] is DBNull ? false : Convert.ToBoolean(r["TUVOUS"]);

            Corres.numSecu = r["PER_SECU"] is DBNull ? "" : Convert.ToString(r["PER_SECU"]).Trim();
            Corres.DateNaissance = r["PER_DATNAISS"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["PER_DATNAISS"]);
            Corres.Note = r["NOTE"] is DBNull ? 0 : Convert.ToInt16(r["NOTE"]);

            Corres.IOlogin = r["OI_LOGIN"] is DBNull ? "" : Convert.ToString(r["OI_LOGIN"]);
            Corres.password = r["OI_MDP"] is DBNull ? "" : Convert.ToString(r["OI_MDP"]);
            Corres.Idprofile = r["OI_PROFIL"] is DBNull ? 0 : Convert.ToInt32(r["OI_PROFIL"]);
            Corres.publication = r["OI_AUTORISATION"] is DBNull ? true : Convert.ToBoolean(r["OI_AUTORISATION"]);
            Corres.Profession = r["PROFESSION"] is DBNull ? "" : Convert.ToString(r["PROFESSION"]).Trim();
            Corres.AutreProfession = r["AutreProfession"] is DBNull ? "" : Convert.ToString(r["AutreProfession"]).Trim();




            return Corres;
        }
        public static Correspondant BuildJ(JObject r)
        {
            if (r == null) return null;
            Correspondant Corres = new Correspondant();
            Corres.Id = Convert.ToInt32(r["idPersonne"]);
            Corres.Nom = Convert.ToString(r["nom"]).Trim();
            Corres.Prenom = Convert.ToString(r["prenom"]).Trim();



            Corres.Type = Convert.ToInt32(r["type"]);
            Corres.Notes = Convert.ToString(r["notes"]);
            Corres.GenreFeminin = r["genre"].ToString() == "" ? true : Convert.ToChar(r["genre"]) == 'F';
            Corres.Titre = Convert.ToString(r["titre"]);
            Corres.PrefCom = r["preCom"].ToString() == "" ? Correspondant.EnumPrefCom.Courrier : (Correspondant.EnumPrefCom)Convert.ToString(r["preCom"])[0];

            Corres.TuToiement = Convert.ToBoolean(r["tuvous"]);

            Corres.numSecu = Convert.ToString(r["PER_SECU"]).Trim();
            Corres.DateNaissance = r["dateNaissance"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateNaissance"]);
            Corres.Note = Convert.ToInt16(r["note"]);

            Corres.IOlogin = Convert.ToString(r["oiLogin"]);
            Corres.password = Convert.ToString(r["passwaord"]);
            Corres.Idprofile =  Convert.ToInt32(r["profil"]);
            Corres.publication =  Convert.ToBoolean(r["publication"]);
            Corres.Profession = Convert.ToString(r["profession"]).Trim();
            Corres.AutreProfession =  Convert.ToString(r["autreprofession"]).Trim();




            return Corres;
        }
    }
}
