using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;
using System.Diagnostics;
using MySql.Data.Types;
using Newtonsoft.Json.Linq;
namespace BasCommon_BL.Builders
{

    public static class BuildUtilisateur
    {

        public static Utilisateur Build(DataRow r)
        {

            Utilisateur Ut = new Utilisateur();

            Ut.Google_Login = Convert.ToString(r["GOOGLE_LOGIN"]).Trim();

            Ut.Google_Password =  Convert.ToString(r["GOOGLE_PASSWD"]).Trim();

           
            Ut.Id = Convert.ToInt32(r["ID_PERSONNE"]);
            Ut.Nom = Convert.ToString(r["PER_NOM"]).Trim();

            Ut.DateEmbauche = r["DATEEMBAUCHE"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEEMBAUCHE"]);
            if (Convert.ToString(r["UTIL_PWD"]).Trim()  == "pt04")
            {
                int pp = 0;
                pp = 1;
            }
            Ut.DateFinContrat = r["DATEFINCONTRAT"] is DBNull ? null : (DateTime?)((MySqlDateTime)(r["DATEFINCONTRAT"]));
            Ut.Actif = ((Convert.ToString(r["UTIL_ACTIF"]) == "Y") && ((Ut.DateFinContrat > DateTime.Now) || (Ut.DateFinContrat == null)));

            Ut.Fauteuils =Fauteuilsmgt.GetFauteuils (Ut);
            //Ut.Actif = Convert.ToString(r["UTIL_ACTIF"]) == "Y";

            Ut.Prenom = Convert.ToString(r["PER_PRENOM"]).Trim();

            Ut.Mail = Convert.ToString(r["PER_EMAIL"]).Trim();

            Ut.password = Convert.ToString(r["UTIL_PWD"]).Trim();

            Ut.EntiteJuridique = r["ID_ENTITYJURIDIQUE"] is DBNull ? null : EntiteJuridiqueMgmt.getentite(Convert.ToInt32(r["ID_ENTITYJURIDIQUE"]));


            Ut.Profession = Convert.ToString(r["PROFESSION"]).Trim();
            Ut.Fonction = Convert.ToString(r["NOMTYPE"]).Trim();
            Ut.Tel = Convert.ToString(r["PER_TELPRINC"]).Trim();

            Ut.Adresse.Adress1 = Convert.ToString(r["PER_ADR1"]).Trim();
            Ut.Adresse.Adress2 = Convert.ToString(r["PER_ADR2"]).Trim();
            Ut.Adresse.CP = Convert.ToString(r["PER_CPOSTAL"]).Trim();
            Ut.Adresse.Ville = Convert.ToString(r["PER_VILLE"]).Trim();

            Ut.Civilite = Convert.ToString(r["PERS_TITRE"]).Trim();

            Ut.AssMaladie = Convert.ToString(r["AssMaladie"]).Trim();
            Ut.NumSecu = Convert.ToString(r["NumSecu"]).Trim();
            Ut.NumOrdre = Convert.ToString(r["NumOrdre"]).Trim();



            Ut.DateNaiss = r["PER_DATNAISS"] is DBNull?null:(DateTime?)Convert.ToDateTime(r["PER_DATNAISS"]);
            Ut.NomJeuneFille = Convert.ToString(r["PER_NOMJF"]).Trim();

            Ut.DiplomeUniversitaire = Convert.ToString(r["DIPLOMEUNIVERSITAIRE"]).Trim();
            Ut.DiplomeOptionNational = Convert.ToString(r["DiplomeOptionNATIONAL"]).Trim();
            Ut.DiplomeNational = Convert.ToString(r["DIPLOMENATIONAL"]).Trim();


            Ut.type = (Utilisateur.typeUtilisateur)Convert.ToInt32(r["UTIL_TYPE"]);
          


            return Ut;
        }
        public static Utilisateur BuildJ(JObject r)
        {            

            Utilisateur Ut = new Utilisateur();

            Ut.Google_Login = Convert.ToString(r["google_Login"]).Trim();

            Ut.Google_Password = Convert.ToString(r["google_password"]).Trim();


            Ut.Id = Convert.ToInt32(r["idPersonne"]);
            Ut.Nom = Convert.ToString(r["nom"]).Trim();

            Ut.DateEmbauche = r["dateEmbauche"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateEmbauche"]);

            Ut.DateFinContrat = r["dateFinContract"].ToString() == "" ? null : (DateTime?)((DateTime)(r["dateFinContract"]));
            Ut.Actif = ((Convert.ToString(r["actif"]) == "Y") && ((Ut.DateFinContrat > DateTime.Now) || (Ut.DateFinContrat == null)));

            Ut.Fauteuils = Fauteuilsmgt.GetFauteuils(Ut);
            //Ut.Actif = Convert.ToString(r["UTIL_ACTIF"]) == "Y";

            Ut.Prenom = Convert.ToString(r["prenom"]).Trim();

            Ut.Mail = Convert.ToString(r["mail"]).Trim();

            Ut.password = Convert.ToString(r["password"]).Trim();

            Ut.EntiteJuridique = r["entiteJuridique"].ToString() == "" ? null : EntiteJuridiqueMgmt.getentite(Convert.ToInt32(r["entiteJuridique"]));


            Ut.Profession = Convert.ToString(r["proffession"]).Trim();
            Ut.Fonction = Convert.ToString(r["fonction"]).Trim();
            Ut.Tel = Convert.ToString(r["tel"]).Trim();

            Ut.Adresse.Adress1 = Convert.ToString(r["adresse1"]).Trim();
            Ut.Adresse.Adress2 = Convert.ToString(r["adresse2"]).Trim();
            Ut.Adresse.CP = Convert.ToString(r["cp"]).Trim();
            Ut.Adresse.Ville = Convert.ToString(r["ville"]).Trim();

            Ut.Civilite = Convert.ToString(r["civilite"]).Trim();

            Ut.AssMaladie = Convert.ToString(r["assMaladie"]).Trim();
            Ut.NumSecu = Convert.ToString(r["numsecu"]).Trim();
            Ut.NumOrdre = Convert.ToString(r["numordre"]).Trim();



            Ut.DateNaiss = r["dateNaisseance"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateNaisseance"]);
            Ut.NomJeuneFille = Convert.ToString(r["nomjf"]).Trim();

            Ut.DiplomeUniversitaire = Convert.ToString(r["diplomeUniversitaire"]).Trim();
            Ut.DiplomeOptionNational = Convert.ToString(r["diplomeOptionNational"]).Trim();
            Ut.DiplomeNational = Convert.ToString(r["diplomeNational"]).Trim();


            Ut.type = r["typeUtil"].ToString() == "" ? Utilisateur.typeUtilisateur.Assistante : (Utilisateur.typeUtilisateur)Convert.ToInt32(r["typeUtil"]);



            return Ut;
        }
        public static UserStatus BuildUserStatus(JObject r, Utilisateur p_utilisateur)
        {
            
            UserStatus status = new UserStatus();
            status.status = new Status();
            status.utilisateur = p_utilisateur;
            status.status = StatusMgt.getStatus(Convert.ToInt32(r["idRhStatus"]));
            status.dateStart = Convert.ToDateTime(r["dateStart"]);
            status.dateEnd = Convert.ToDateTime(r["dateEnd"]);
            status.Id = Convert.ToInt32(r["id"]);


            return status;
        }
        public static UserStatus BuildUserStatusOld(DataRow r, Utilisateur p_utilisateur)
        {
            UserStatus status = new UserStatus();
            status.status = new Status();
            status.utilisateur = p_utilisateur;
            status.status = StatusMgt.getStatus(Convert.ToInt32(r["ID_STATUS"]));
            status.dateStart = Convert.ToDateTime(r["date_status_start"]);
            status.dateEnd = Convert.ToDateTime(r["date_status_end"]);
            status.Id = Convert.ToInt32(r["ID"]);


            return status;
        }

        public static AffectedUtilisateurs BuildAffectedUtilisateur(DataRow r, DateTime Dte)
        {

            try
            {
                int fautId = Convert.ToInt32(r["fauteuil"]);
                int utId = Convert.ToInt32(r["ID"]);

                AffectedUtilisateurs Ut = null;

                foreach (Utilisateur u in BasCommon_BL.UtilisateursMgt.utilisateurs)
                {
                    if (u.Id == utId)
                    {
                        Ut = new AffectedUtilisateurs(u);
                        break;
                    }
                }
                if (Ut == null) return null;

                foreach (Fauteuil f in Fauteuilsmgt.fauteuils)
                    if (f.Id == fautId) Ut.fauteuil = f;

                Ut.date = Dte;
                return Ut;
            }

#if TRACE
            catch (System.Exception ex)
            {

                Debug.Write("BuildAffectedUtilisateur : " + ex.Message);

                return null;
            }
#else
            catch (System.Exception ex)
            {
                return null;
            }
#endif
        }

    }
}
