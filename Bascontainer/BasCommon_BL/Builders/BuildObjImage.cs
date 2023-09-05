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

    public static class BuildObjImage
    {
        
        public static ObjImage Build(DataRow r)
        {

            /*
              string selectQuery = " select pk_objet, ";
                selectQuery += "       nom, ";
                selectQuery += "       extension, ";
                //selectQuery += "       id_patient, ";
                selectQuery += "       datas, ";
                selectQuery += "       vignette, ";
                selectQuery += "       width, ";
                selectQuery += "       height, ";
                selectQuery += "       taille, ";
                selectQuery += "       estidentite, ";
                selectQuery += "       datecreation, ";
                selectQuery += "       echelle, ";
                selectQuery += "       fichier ";
                selectQuery += "       last_modif, ";
                selectQuery += "       rep_stockage, ";
                selectQuery += "       syncpath, ";
                selectQuery += "       dateinsertion, ";
                selectQuery += "       auteur, ";
                selectQuery += "       id_gabarit, ";
                selectQuery += "       id_patient_orthalis";
                selectQuery += " from objet";
                selectQuery += " where ID_PATIENT_ORTHALIS=@id_patient_orthalis";
                selectQuery += " order by DATECREATION";
             */

            ObjImage obj = null;

            obj = new ObjImage();
            obj.Id = Convert.ToInt32(r["pk_objet"]);
            obj.fichier = r["fichier"] is DBNull?"": Convert.ToString(r["fichier"]);
            obj.nom = r["nom"] is DBNull ? "" : Convert.ToString(r["nom"]);

            obj.extension = r["extension"] is DBNull ? "" : Convert.ToString(r["extension"]);
            //obj.datas = Convert.ToString(r["datas"]);
            //obj.vignette = Convert.ToString(r["vignette"]);
            obj.width = r["width"] is DBNull ? 0 : Convert.ToInt32(r["width"]);
            obj.height = r["height"] is DBNull ? 0 : Convert.ToInt32(r["height"]);
            obj.taille = r["taille"] is DBNull ? 0 : Convert.ToInt32(r["taille"]);
            obj.estidentite = r["estidentite"] is DBNull ? 0 : Convert.ToInt32(r["estidentite"]);
            obj.datecreation = r["datecreation"] is DBNull ? DateTime.Now : Convert.ToDateTime(r["datecreation"]);
            obj.echelle = r["echelle"] is DBNull ? 0 : Convert.ToDouble(r["echelle"]);
            obj.fichier = r["fichier"] is DBNull ? "" : Convert.ToString(r["fichier"]);
            obj.last_modif = r["last_modif"] is DBNull ? DateTime.Now : Convert.ToDateTime(r["last_modif"]);
            obj.rep_stockage = r["rep_stockage"] is DBNull ? "" : Convert.ToString(r["rep_stockage"]);
            obj.syncpath = r["syncpath"] is DBNull ? "" : Convert.ToString(r["syncpath"]);
            obj.dateinsertion = r["dateinsertion"] is DBNull ? DateTime.Now : Convert.ToDateTime(r["dateinsertion"]);
            obj.auteur = r["datecreation"] is DBNull ? "" : Convert.ToString(r["auteur"]);
            obj.IdGabarit = r["id_gabarit"] is DBNull ? -1 : Convert.ToInt32(r["id_gabarit"]);
            obj.Idpatient = r["id_patient_orthalis"] is DBNull ? -1 : Convert.ToInt32(r["id_patient_orthalis"]);

            return obj;
        }
        public static ObjImage BuildJ(JObject r)
        {

            ObjImage obj = null;

            obj = new ObjImage();
            obj.Id = Convert.ToInt32(r["pkObjet"]);
            obj.fichier = r["fichier"].ToString() == "" ? "" : Convert.ToString(r["fichier"]);
            obj.nom = r["nom"].ToString() == "" ? "" : Convert.ToString(r["nom"]);

            obj.extension = r["extension"].ToString() == "" ? "" : Convert.ToString(r["extension"]);
            //obj.datas = Convert.ToString(r["datas"]);
            //obj.vignette = Convert.ToString(r["vignette"]);
            obj.width = r["width"].ToString() == "" ? 0 : Convert.ToInt32(r["width"]);
            obj.height = r["height"].ToString() == "" ? 0 : Convert.ToInt32(r["height"]);
            obj.taille = r["taille"].ToString() == "" ? 0 : Convert.ToInt32(r["taille"]);
            obj.estidentite = r["estidentite"].ToString() == "" ? 0 : Convert.ToInt32(r["estidentite"]);
            obj.datecreation = r["dateCreation"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(r["dateCreation"]);
            obj.echelle = r["echelle"].ToString() == "" ? 0 : Convert.ToDouble(r["echelle"]);
            // a fixer
             obj.last_modif = r["lastModif"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(r["lastModif"]);
            obj.rep_stockage =  r["repStockage"].ToString() == "" ? "" : Convert.ToString(r["repStockage"]);
            obj.syncpath = r["syncPath"].ToString() == "" ? "" : Convert.ToString(r["syncPath"]);

            // a fixer
            obj.dateinsertion = r["dateInsertion"].ToString() == "" ? DateTime.Now : Convert.ToDateTime(r["dateInsertion"]);
            obj.auteur = r["auteur"].ToString() == "" ? "" : Convert.ToString(r["auteur"]);
            obj.IdGabarit = r["idGabarit"].ToString() == "" ? -1: Convert.ToInt32(r["idGabarit"]);
            obj.Idpatient = r["id_patient_orthalis"].ToString() == "" ? -1 : Convert.ToInt32(r["id_patient_orthalis"]);


            return obj;
        }

    }
}
