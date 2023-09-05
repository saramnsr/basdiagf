using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL.Builders
{
    public static class BuildPatient
    {

        public static basePatient Build(DataRow r)
        {
            if (r == null) return null;
            basePatient Pat = new basePatient();
            Pat.Id = Convert.ToInt32(r["ID_PERSONNE"]);
            Pat.Civilite = Convert.ToString(r["PERS_TITRE"]);
            Pat.Genre = (Convert.ToString(r["PER_GENRE"]) == "M") ? basePatient.Sexe.Masculin : basePatient.Sexe.Feminin;
            Pat.Nom = Convert.ToString(r["PER_NOM"]).Trim();
            Pat.NomJF = Convert.ToString(r["PER_NOMJF"]).Trim();

            Pat.IOlogin = r["OI_LOGIN"] is DBNull ?"": Convert.ToString(r["OI_LOGIN"]).Trim();
            Pat.password = r["OI_MDP"] is DBNull ?"": Convert.ToString(r["OI_MDP"]).Trim();
            Pat.publication = r["OI_AUTORISATION"] is DBNull ? true : Convert.ToBoolean(r["OI_AUTORISATION"]);
            Pat.Idprofile = r["OI_PROFIL"] is DBNull?-1: Convert.ToInt32(r["OI_PROFIL"]);
            Pat.BasePracticeState = r["test_bp"] is DBNull ? basePatient.BasePracticeStateEnum.NonDefini : (basePatient.BasePracticeStateEnum)Convert.ToInt32(r["test_bp"]);
            

            Pat.Prenom = Convert.ToString(r["PER_PRENOM"]).Trim();
            Pat.DateNaissance = (r["per_datnaiss"].GetType().Name != "DBNull") ? Convert.ToDateTime(r["per_datnaiss"]) : DateTime.MinValue;
            Pat.Dossier = Convert.ToInt32(r["PAT_NUMDOSSIER"]);
            Pat.Moulage = Convert.ToString(r["NUM_MOULAGE"]);
            Pat.NumSecu = r["PER_SECU"] is DBNull ? "" : Convert.ToString(r["PER_SECU"]);


            try
            {
                Pat.CodeBanque = r["CODE_BANQUE"] is DBNull ? "00000" : Convert.ToString(r["CODE_BANQUE"]).Trim();
                Pat.CodeGuichet = r["CODE_GUICHET"] is DBNull ? "00000" : Convert.ToString(r["CODE_GUICHET"]).Trim();
                Pat.NumCompte = r["NUM_COMPTE"] is DBNull ? "00000000000" : Convert.ToString(r["NUM_COMPTE"]).Trim();
                Pat.CleRIB = r["CLE_RIB"] is DBNull ? "00" : Convert.ToString(r["CLE_RIB"]).Trim();
                Pat.NomBanque = r["NOM_BANQUE"] is DBNull ? "" : Convert.ToString(r["NOM_BANQUE"]).Trim();
                Pat.Titulaire = r["Titulaire"] is DBNull ? "" : Convert.ToString(r["Titulaire"]).Trim();
                
            }
            catch (System.Exception) { }

            Pat.Profession = Convert.ToString(r["PROFESSION"]).Trim();
            Pat.Tutoiement = r["TUVOUS"] is DBNull? true :Convert.ToBoolean(r["TUVOUS"]);

            if (!(r["ALLERGIE"] is DBNull))
            {
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                Pat.Allergie = enc.GetString((byte[])r["ALLERGIE"]);
            }
            Pat.CasierInvisalign = Convert.ToString(r["PAT_REFDOSSIER"]).Trim();
            Pat.RefArchive = Convert.ToString(r["RefArchive"]).Trim();
            Pat.numMoulage = Convert.ToString(r["NUM_MOULAGE"]).Trim();

            Pat.Diagnostic = Convert.ToString(r["PAT_DIAG"]);
            Pat.Traitement = Convert.ToString(r["PAT_PLAN"]);
            Pat.Objectif = Convert.ToString(r["PAT_OBJECTIF_TRAIT"]);
            Pat.CommentApparreil = Convert.ToString(r["PAT_APPAREIL"]);
            Pat.PourcentageMutuelle = r["PERCENTAGEMUTUELLE"] is DBNull?null: (int?)Convert.ToInt32(r["PERCENTAGEMUTUELLE"]);
            

            Pat.Notes = Convert.ToString(r["per_notes"]);
            Pat.PrefCom = r["PREF_COM"] is DBNull ? Correspondant.EnumPrefCom.Courrier : (Correspondant.EnumPrefCom)Convert.ToString(r["PREF_COM"])[0];
            Pat.statusClinique = r["STATUSCLINIQUE"] is DBNull ? basePatient.StatusClinique.Inconnue : (basePatient.StatusClinique)Convert.ToInt32(r["STATUSCLINIQUE"]);
            Pat.DateAbandon = r["DATEABANDON"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["DATEABANDON"]);


            Pat.statusManuel = r["ID_STATUT"] is DBNull ? null : StatusCliniqueManuelMgmt.GetStatus(Convert.ToInt32(r["ID_STATUT"]));

            return Pat;
        }


        public static basePatient BuildPatientJson(JObject r)
        {
            if (r == null) return null;
            basePatient Pat = new basePatient();
            Pat.Id = Convert.ToInt32(r["id_personne"]);
            Pat.Civilite = Convert.ToString(r["pers_titre"]);
            Pat.Genre = (Convert.ToString(r["per_genre"]) == "M") ? basePatient.Sexe.Masculin : basePatient.Sexe.Feminin;
            Pat.Nom = Convert.ToString(r["per_nom"]).Trim();
            Pat.NomJF = Convert.ToString(r["per_nomjf"]).Trim();

            Pat.IOlogin = r["oi_login"].ToString() == "" ? "" : Convert.ToString(r["oi_login"]).Trim();
            Pat.password = r["oi_mdp"].ToString() == "" ? "" : Convert.ToString(r["oi_mdp"]).Trim();
            Pat.publication = r["oi_autorisation"].ToString() == "" ? true : Convert.ToBoolean(r["oi_autorisation"]);
            Pat.Idprofile = r["oi_profil"].ToString() == "" ? -1 : Convert.ToInt32(r["oi_profil"]);
            Pat.BasePracticeState = r["test_bp"].ToString() == "" ? basePatient.BasePracticeStateEnum.NonDefini : (basePatient.BasePracticeStateEnum)Convert.ToInt32(r["test_bp"]);


            Pat.Prenom = Convert.ToString(r["per_prenom"]).Trim();
            Pat.DateNaissance = r["per_datnaiss"].ToString() == "" ? DateTime.MinValue : Convert.ToDateTime(r["per_datnaiss"]);
            Pat.Dossier = Convert.ToInt32(r["pat_numdossier"]);
            Pat.Moulage = Convert.ToString(r["num_moulage"]);
            Pat.NumSecu = r["per_secu"].ToString() == "" ? "" : Convert.ToString(r["per_secu"]);


            try
            {
                Pat.CodeBanque = r["code_banque"].ToString() == "" ? "00000" : Convert.ToString(r["code_banque"]).Trim();
                Pat.CodeGuichet = r["code_guichet"].ToString() == "" ? "00000" : Convert.ToString(r["code_guichet"]).Trim();
                Pat.NumCompte = r["num_compte"].ToString() == "" ? "00000000000" : Convert.ToString(r["num_compte"]).Trim();
                Pat.CleRIB = r["cle_rib"].ToString() == "" ? "00" : Convert.ToString(r["cle_rib"]).Trim();
                Pat.NomBanque = r["nom_banque"].ToString() == "" ? "" : Convert.ToString(r["nom_banque"]).Trim();
                Pat.Titulaire = r["titulaire"].ToString() == "" ? "" : Convert.ToString(r["titulaire"]).Trim();
                Pat.regelement = r["reglement"].ToString() == "" ? basePatient.Enumregelement.Aucun : (basePatient.Enumregelement)Convert.ToInt32(r["reglement"]);

            }
            catch (System.Exception) { }
            Pat.Profession = Convert.ToString(r["profession"]).Trim();
            Pat.Tutoiement = r["tuvous"].ToString() == "" ? true : Convert.ToBoolean(r["tuvous"]);

            if (!(r["allergie"].ToString() == ""))
            {
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                Pat.Allergie = r["allergie"].ToString() == "" ? null : enc.GetString((byte[])r["allergie"]);
            }
            Pat.CasierInvisalign = Convert.ToString(r["pat_refdossier"]).Trim();
            Pat.RefArchive = Convert.ToString(r["refarchive"]).Trim();
            Pat.numMoulage = Convert.ToString(r["num_moulage"]).Trim();

            Pat.Diagnostic = Convert.ToString(r["pat_diag"]);
            Pat.Traitement = Convert.ToString(r["pat_plan"]);
            Pat.Objectif = Convert.ToString(r["pat_objectif_trait"]);
            Pat.CommentApparreil = Convert.ToString(r["pat_appareil"]);
            Pat.PourcentageMutuelle = r["percentagemutuelle"].ToString() == "" ? 0 : (int?)Convert.ToInt32(r["percentagemutuelle"]);


            Pat.Notes = Convert.ToString(r["per_notes"]);
            Pat.PrefCom = r["pref_com"].ToString() == "" ? Correspondant.EnumPrefCom.Courrier : (Correspondant.EnumPrefCom)Convert.ToString(r["pref_com"])[0];
            Pat.statusClinique = r["statusclinique"].ToString() == "" ? basePatient.StatusClinique.Inconnue : (basePatient.StatusClinique)Convert.ToInt32(r["statusclinique"]);
            Pat.DateAbandon = r["dateabandon"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["dateabandon"]);


            Pat.statusManuel = StatusCliniqueManuelMgmt.GetStatus(Convert.ToInt32(r["id_STATUT"]));

            return Pat;
        }


        public static basePatient BuildJson(JObject r)
        {
            if (r == null) return null;
            basePatient Pat = new basePatient();
            Pat.Id = Convert.ToInt32(r["id_personne"]);
            Pat.Civilite = Convert.ToString(r["pers_titre"]);
            Pat.Genre = (Convert.ToString(r["per_genre"]) == "M") ? basePatient.Sexe.Masculin : basePatient.Sexe.Feminin;
            Pat.Nom = Convert.ToString(r["per_nom"]).Trim();
            Pat.NomJF = Convert.ToString(r["per_nomjf"]).Trim();

            Pat.IOlogin = r["oi_login"].ToString() == ""  ? "" : Convert.ToString(r["OI_LOGIN"]).Trim();
            Pat.password = r["oi_mdp"].ToString() == ""  ? "" : Convert.ToString(r["OI_MDP"]).Trim();
            Pat.publication = r["oi_autorisation"].ToString() == ""  ? true : Convert.ToBoolean(r["OI_AUTORISATION"]);
            Pat.Idprofile = r["oi_profil"].ToString() == ""  ? -1 : Convert.ToInt32(r["OI_PROFIL"]);
            Pat.BasePracticeState = r["test_bp"].ToString() == ""  ? basePatient.BasePracticeStateEnum.NonDefini : (basePatient.BasePracticeStateEnum)Convert.ToInt32(r["test_bp"]);


            Pat.Prenom = Convert.ToString(r["per_prenom"]).Trim();
            Pat.DateNaissance = (r["per_datnaiss"].GetType().Name != "DBNull") ? Convert.ToDateTime(r["per_datnaiss"]) : DateTime.MinValue;
            Pat.Dossier = Convert.ToInt32(r["pat_numdossier"]);
            Pat.Moulage = Convert.ToString(r["num_moulage"]);
            Pat.NumSecu = r["per_secu"].ToString() == ""  ? "" : Convert.ToString(r["per_secu"]);


            try
            {
                Pat.CodeBanque = r["code_banque"].ToString() == ""  ? "00000" : Convert.ToString(r["CODE_BANQUE"]).Trim();
                Pat.CodeGuichet = r["code_guichet"].ToString() == ""  ? "00000" : Convert.ToString(r["CODE_GUICHET"]).Trim();
                Pat.NumCompte = r["num_compte"].ToString() == ""  ? "00000000000" : Convert.ToString(r["NUM_COMPTE"]).Trim();
                Pat.CleRIB = r["cle_rib"].ToString() == ""  ? "00" : Convert.ToString(r["CLE_RIB"]).Trim();
                Pat.NomBanque = r["nom_banque"].ToString() == ""  ? "" : Convert.ToString(r["NOM_BANQUE"]).Trim();
                Pat.Titulaire = r["titulaire"].ToString() == ""  ? "" : Convert.ToString(r["Titulaire"]).Trim();

            }
            catch (System.Exception) { }

            Pat.Profession = Convert.ToString(r["profession"]).Trim();
            Pat.Tutoiement =  Convert.ToBoolean(r["tuvous"]);

            if (!(r["allergie"].ToString()==""))
            {
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                Pat.Allergie = enc.GetString((byte[])r["allergie"]);
            }
            Pat.CasierInvisalign = Convert.ToString(r["pat_refdossier"]).Trim();
            Pat.RefArchive = Convert.ToString(r["refarchive"]).Trim();
            Pat.numMoulage = Convert.ToString(r["num_moulage"]).Trim();

            Pat.Diagnostic = Convert.ToString(r["pat_diag"]);
            Pat.Traitement = Convert.ToString(r["pat_plan"]);
            Pat.Objectif = Convert.ToString(r["pat_objectif_trait"]);
            Pat.CommentApparreil = Convert.ToString(r["pat_appareil"]);
            Pat.PourcentageMutuelle =(int?)Convert.ToInt32(r["percentagemutuelle"]);


            Pat.Notes = Convert.ToString(r["per_notes"]);
            Pat.PrefCom = r["pref_com"].ToString()=="" ? Correspondant.EnumPrefCom.Courrier : (Correspondant.EnumPrefCom)Convert.ToString(r["pref_com"])[0];
            Pat.statusClinique = r["statusclinique"].ToString() == ""  ? basePatient.StatusClinique.Inconnue : (basePatient.StatusClinique)Convert.ToInt32(r["statusclinique"]);
            Pat.DateAbandon = r["dateabandon"].ToString() == ""  ? null : (DateTime?)Convert.ToDateTime(r["dateabandon"]);


            Pat.statusManuel = StatusCliniqueManuelMgmt.GetStatus(Convert.ToInt32(r["id_statut"]));

            return Pat;
        }

    
    }
}
