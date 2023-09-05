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

    public static class BuildLienCorrespondant
    {
        public static LienCorrespondant BuildJ(JObject r)
        {
            LienCorrespondant Corres = new LienCorrespondant();
            Corres.TypeDeLien = Convert.ToString(r["relation"]);
            Corres.IdCorrespondance = Convert.ToInt32(r["idPersonne"]);
            Corres.LienLibelle = Convert.ToString(r["typelien"]).Trim();
            Corres.IdPatient = Convert.ToInt32(r["idPatient"]);


            if ((r.GetValue("PER_NOM") == null) || (r["PER_NOM"].ToString() == ""))
            {
                Corres.correspondant = null;
            }
            else
            {
                Corres.correspondant = new Correspondant();
                Corres.correspondant.Id = Convert.ToInt32(r["ID_PERSONNE"]);
                Corres.correspondant.Nom = Convert.ToString(r["PER_NOM"]).Trim();
                Corres.correspondant.Prenom = Convert.ToString(r["PER_PRENOM"]).Trim();



                Corres.correspondant.Profession = Convert.ToString(r["PROFESSION"]).Trim();
                Corres.correspondant.AutreProfession = Convert.ToString(r["AutreProfession"]).Trim();

                Corres.correspondant.Type = Convert.ToInt32(r["TYPE"]);
                Corres.correspondant.Notes = Convert.ToString(r["PER_NOTES"]);
                Corres.correspondant.GenreFeminin = r["PER_GENRE"].ToString() == "" ? true : Convert.ToChar(r["PER_GENRE"]) == 'F';
                Corres.correspondant.Titre = Convert.ToString(r["PERS_TITRE"]);
                Corres.correspondant.PrefCom = r["PREF_COM"].ToString() == "" ? Correspondant.EnumPrefCom.Courrier : (Correspondant.EnumPrefCom)Convert.ToString(r["PREF_COM"])[0];

                Corres.correspondant.numSecu = Convert.ToString(r["PER_SECU"]);
                Corres.correspondant.DateNaissance = r["PER_DATNAISS"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["PER_DATNAISS"]);
                Corres.correspondant.Note = Convert.ToInt16(r["NOTE"]);


                Corres.correspondant.TuToiement = Convert.ToInt32(r["TUVOUS"]) == 0;

            }
            return Corres;

        }
        public static LienCorrespondant BuildJson(JObject r)
        {
            if(r==null) return null;
            LienCorrespondant Corres = new LienCorrespondant();
            Corres.TypeDeLien = Convert.ToString(r["relation"]);
            Corres.IdCorrespondance = Convert.ToInt32(r["id_personne"]);
            Corres.LienLibelle = Convert.ToString(r["typelien"]).Trim();
            Corres.IdPatient = Convert.ToInt32(r["id_patient"]);
            if ((r.GetValue("per_nom") == null) || (r["per_nom"].ToString() == ""))
            {
                Corres.correspondant = null;
            }
            else
            {
                Corres.correspondant = new Correspondant();
                Corres.correspondant.Id = Convert.ToInt32(r["id_personne"]);
                Corres.correspondant.Nom = Convert.ToString(r["per_nom"]).Trim();
                Corres.correspondant.Prenom = Convert.ToString(r["per_prenom"]).Trim();
                Corres.correspondant.Profession = Convert.ToString(r["profession"]).Trim();
                Corres.correspondant.AutreProfession = Convert.ToString(r["autreprofession"]).Trim();
                Corres.correspondant.Type = Convert.ToInt32(r["type"]);
                Corres.correspondant.Notes = Convert.ToString(r["per_notes"]);
                Corres.correspondant.GenreFeminin = r["per_genre"].ToString() == "" ? true : Convert.ToChar(r["per_genre"]) == 'F';
                Corres.correspondant.Titre = Convert.ToString(r["pers_titre"]);
                Corres.correspondant.PrefCom = r["pref_com"].ToString() == "" ? Correspondant.EnumPrefCom.Courrier : (Correspondant.EnumPrefCom)Convert.ToString(r["pref_com"])[0];
                Corres.correspondant.numSecu = Convert.ToString(r["per_secu"]);
                Corres.correspondant.DateNaissance = r["per_datnaiss"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["per_datnaiss"]);
                Corres.correspondant.Note = Convert.ToInt16(r["note"]);
                Corres.correspondant.TuToiement = Convert.ToInt32(r["tuvous"]) == 0;
            }
            return Corres;
        }


            
        public static LienCorrespondant Build(DataRow r)
        {
            LienCorrespondant Corres = new LienCorrespondant();
            Corres.TypeDeLien = Convert.ToString(r["RELATION"]);
            Corres.IdCorrespondance = Convert.ToInt32(r["ID_PERSONNE"]);
            Corres.LienLibelle = Convert.ToString(r["TYPELIEN"]).Trim();
            Corres.IdPatient = Convert.ToInt32(r["ID_PATIENT"]);


            if ((r.Table.Columns["PER_NOM"]==null)||(r["PER_NOM"] is DBNull))
            {
                Corres.correspondant = null;
            }
            else
            {
                Corres.correspondant = new Correspondant();
                Corres.correspondant.Id = Convert.ToInt32(r["ID_PERSONNE"]);
                Corres.correspondant.Nom = Convert.ToString(r["PER_NOM"]).Trim();
                Corres.correspondant.Prenom = Convert.ToString(r["PER_PRENOM"]).Trim();



                Corres.correspondant.Profession = Convert.ToString(r["PROFESSION"]).Trim();
                Corres.correspondant.AutreProfession = Convert.ToString(r["AutreProfession"]).Trim();
                
                Corres.correspondant.Type = Convert.ToInt32(r["TYPE"]);
                Corres.correspondant.Notes = Convert.ToString(r["PER_NOTES"]);
                Corres.correspondant.GenreFeminin = r["PER_GENRE"] is DBNull ? true : Convert.ToChar(r["PER_GENRE"]) == 'F';
                Corres.correspondant.Titre = Convert.ToString(r["PERS_TITRE"]);
                Corres.correspondant.PrefCom = r["PREF_COM"] is DBNull ? Correspondant.EnumPrefCom.Courrier : (Correspondant.EnumPrefCom)Convert.ToString(r["PREF_COM"])[0];

                Corres.correspondant.numSecu = r["PER_SECU"] is DBNull ? "" : Convert.ToString(r["PER_SECU"]);
                Corres.correspondant.DateNaissance = r["PER_DATNAISS"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["PER_DATNAISS"]);
                Corres.correspondant.Note = r["NOTE"] is DBNull ? 0 : Convert.ToInt16(r["NOTE"]);


                Corres.correspondant.TuToiement = r["TUVOUS"] is DBNull ? false : (Convert.ToInt32(r["TUVOUS"]) == 0);

            }

            
            return Corres;

        }

       /* public static LienCorrespondant BuildJson(JObject r)
        {
            LienCorrespondant Corres = new LienCorrespondant();
            Corres.TypeDeLien = Convert.ToString(r["relation"]);
            Corres.IdCorrespondance =r["id_personne"].ToString()=="" ? -1 : Convert.ToInt32(r["id_personne"]);
            Corres.LienLibelle = Convert.ToString(r["typelien"]).Trim();
            Corres.IdPatient =r["id_patient"].ToString()=="" ? -1 : Convert.ToInt32(r["id_patient"]);


            if ((r["per_nom"] == null) || (r["per_nom"].ToString()==""))
            {
                Corres.correspondant = null;
            }
            else
            {
                Corres.correspondant = new Correspondant();
                Corres.correspondant.Id =r["id_personne"].ToString()== "" ? -1 : Convert.ToInt32(r["id_personne"]);
                Corres.correspondant.Nom = Convert.ToString(r["per_nom"]).Trim();
                Corres.correspondant.Prenom = Convert.ToString(r["per_prenom"]).Trim();



                Corres.correspondant.Profession = Convert.ToString(r["profession"]).Trim();
                Corres.correspondant.AutreProfession = Convert.ToString(r["autreprofession"]).Trim();

                Corres.correspondant.Type =r["type"].ToString()=="" ? -1 : Convert.ToInt32(r["type"]);
                Corres.correspondant.Notes = Convert.ToString(r["per_notes"]);
                Corres.correspondant.GenreFeminin = r["per_genre"] == null ? true : Convert.ToChar(r["per_genre"]) == 'F';
                Corres.correspondant.Titre = Convert.ToString(r["pers_titre"]);
                Corres.correspondant.PrefCom = r["pref_com"].ToString() == "" ? Correspondant.EnumPrefCom.Courrier : (Correspondant.EnumPrefCom)Convert.ToString(r["pref_com"])[0];

                Corres.correspondant.numSecu = r["per_secu"].ToString() == "" ? "" : Convert.ToString(r["per_secu"]);
                Corres.correspondant.DateNaissance = r["per_datnaiss"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["per_datnaiss"]);
                Corres.correspondant.Note = r["note"].ToString()=="" ? 0 : Convert.ToInt16(r["note"]);


                Corres.correspondant.TuToiement = r["tuvous"].ToString()=="" ? false : (Convert.ToInt32(r["tuvous"]) == 0);

            }


            return Corres;

        }*/
    }
}
