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

    public static class BuildEcheance
    {

        public static Echeance BuildJ(JObject r)
        {

            /*
             string selectQuery = "select base_echeance.ID, ";
                selectQuery += "        base_echeance.ID_TRAITEMENT, ";
                selectQuery += "        base_echeance.ID_TRAITEMENT, ";
                selectQuery += "        base_echeance.MONTANT, ";
                selectQuery += "        base_echeance.DTEECHEANCE, ";
                selectQuery += "        base_echeance.LIBELLE ";   
                selectQuery += " from base_echeance";
                selectQuery += " inner join base_traitement on base_traitement.ID = base_echeance.ID_TRAITEMENT and base_traitement.ID_PATIENT = @id_patient";
                selectQuery += " order by DTEECHEANCE";
             */
            Echeance apg = new Echeance();
            apg.Id = Convert.ToInt32(r["id"]);
            apg.IdActe = Convert.ToInt32(r["id_TRAITEMENT"]);

            apg.ID_Facturation = r["id_FACTURATION"].ToString() == "" ? -1 : Convert.ToInt32(r["id_FACTURATION"]);
            //La date d'echeance ne peut plus être à null
            //Donc si on tombe sur une date d'echeance à null, elle est remise à hier pour etre réglé le plus vite possible

            apg.DateEcheance = r["dteecheance"].ToString() == "" ? DateTime.Now.AddDays(-1) : Convert.ToDateTime(r["dteecheance"]);
            apg.Libelle = r["libelle"].ToString() == "null" ? "" : Convert.ToString(r["libelle"]).Trim();
            apg.Montant = r["montant"].ToString() == "" ? 0 : Convert.ToDouble(r["montant"]);
          

            try
            {
                apg.ParPrelevement = r["parprelevement"].ToString() == "null" ? false : Convert.ToBoolean(Convert.ToString(r["parprelevement"]));
            }
            catch (Exception e)
            {
                apg.ParPrelevement = r["parprelevement"].ToString() == "null" ? false : Convert.ToBoolean(Convert.ToInt32(r["parprelevement"]));
            }
            try
            {
                apg.ParVirement = r["parvirement"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToString(r["parvirement"]));
            }
            catch (Exception ex)
            {
                apg.ParVirement = r["parvirement"].ToString() == "" ? false : Convert.ToBoolean(Convert.ToInt32(r["parvirement"]));
            }


            apg.IdPatient = Convert.ToInt32(r["id_PATIENT"]);
            apg.ID_Encaissement = r["id_ENCAISSEMENT"].ToString() == "" ? -1 : Convert.ToInt32(r["id_ENCAISSEMENT"]);
            apg.mutuelle =r["id_MUTUELLE"].ToString() == "" ? null :  MutuelleMgmt.getMutuelle(Convert.ToInt32(r["id_MUTUELLE"]));
            apg.payeur = r["typepayeur"].ToString() == "" ? Echeance.typepayeur.patient : (Echeance.typepayeur)Convert.ToInt32(r["typepayeur"]);

            apg.Relances.ReleveDeCompte = r["relevedecompte"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["relevedecompte"]);
            apg.Relances.Relance = r["relance"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["relance"]);
            apg.Relances.PreContentieux = r["precontentieux"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["precontentieux"]);
            apg.Relances.Majoration = r["majoration"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["majoration"]);
            apg.Relances.Contentieux = r["contentieux"].ToString() == "" ? null : (DateTime?)Convert.ToDateTime(r["contentieux"]);
            apg.IdComm = r["idComm"].ToString() == "" ? -1 : Convert.ToInt32(r["idComm"]);
          
            if (r["nomPatient"] != null)
                  apg.NomPatient = r["nomPatient"].ToString() == "" ? null : Convert.ToString(r["nomPatient"]);
            apg.TypeActe =  Convert.ToString(r["typeacte"]);
            return apg;
        }
        public static Echeance Build(DataRow r)
        {

            /*
             string selectQuery = "select base_echeance.ID, ";
                selectQuery += "        base_echeance.ID_TRAITEMENT, ";
                selectQuery += "        base_echeance.ID_TRAITEMENT, ";
                selectQuery += "        base_echeance.MONTANT, ";
                selectQuery += "        base_echeance.DTEECHEANCE, ";
                selectQuery += "        base_echeance.LIBELLE ";   
                selectQuery += " from base_echeance";
                selectQuery += " inner join base_traitement on base_traitement.ID = base_echeance.ID_TRAITEMENT and base_traitement.ID_PATIENT = @id_patient";
                selectQuery += " order by DTEECHEANCE";
             */
            Echeance apg = new Echeance();
            apg.Id = r["ID"] is DBNull ? -1 : Convert.ToInt32(r["ID"]);
            apg.IdActe = r["ID_TRAITEMENT"] is DBNull ? -1 : Convert.ToInt32(r["ID_TRAITEMENT"]);

            apg.ID_Facturation = r["ID_FACTURATION"] is DBNull ? -1 : Convert.ToInt32(r["ID_FACTURATION"]);
            //La date d'echeance ne peut plus être à null
            //Donc si on tombe sur une date d'echeance à null, elle est remise à hier pour etre réglé le plus vite possible

            apg.DateEcheance = r["DTEECHEANCE"] is DBNull ? DateTime.Now.AddDays(-1) : Convert.ToDateTime(r["DTEECHEANCE"]);
            apg.Libelle = r["LIBELLE"] is DBNull ? "" : Convert.ToString(r["LIBELLE"]).Trim();
            apg.Montant = r["MONTANT"] is DBNull ? 0 : Convert.ToDouble(r["MONTANT"]);
          
            try
            {
                apg.ParPrelevement = r["PARPRELEVEMENT"] is DBNull ? false : Convert.ToBoolean(Convert.ToString(r["PARPRELEVEMENT"]));
            }
            catch (Exception e)
            {
                apg.ParPrelevement = r["PARPRELEVEMENT"] is DBNull ? false : Convert.ToBoolean(Convert.ToInt32(r["PARPRELEVEMENT"]));
            }
            try
            {
                apg.ParVirement = r["PARVIREMENT"] is DBNull ? false : Convert.ToBoolean(Convert.ToString(r["PARVIREMENT"]));
            }
            catch (Exception ex)
            {
                apg.ParVirement = r["PARVIREMENT"] is DBNull ? false : Convert.ToBoolean(Convert.ToInt32(r["PARVIREMENT"]));
            }
           

            apg.IdPatient = r["ID_PATIENT"] is DBNull ? 0 : Convert.ToInt32(r["ID_PATIENT"]);
            apg.ID_Encaissement = r["ID_ENCAISSEMENT"] is DBNull ? -1 : Convert.ToInt32(r["ID_ENCAISSEMENT"]);
            apg.mutuelle = r["ID_MUTUELLE"] is DBNull ? null : MutuelleMgmt.getMutuelle(Convert.ToInt32(r["ID_MUTUELLE"]));
            apg.payeur = r["TYPEPAYEUR"] is DBNull ? Echeance.typepayeur.patient : (Echeance.typepayeur)Convert.ToInt32(r["TYPEPAYEUR"]);

            apg.Relances.ReleveDeCompte = r["ReleveDeCompte"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["ReleveDeCompte"]);
            apg.Relances.Relance = r["Relance"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["Relance"]);
            apg.Relances.PreContentieux = r["PreContentieux"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["PreContentieux"]);
            apg.Relances.Majoration = r["Majoration"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["Majoration"]);
            apg.Relances.Contentieux = r["Contentieux"] is DBNull ? null : (DateTime?)Convert.ToDateTime(r["Contentieux"]);
            apg.IdComm = r["ID_COMM"] is DBNull ? -1 : Convert.ToInt32(r["ID_COMM"]);
            if (r.Table.Columns.Contains("NomPatient"))
                apg.NomPatient = r["NomPatient"] is DBNull ? null : Convert.ToString(r["NomPatient"]);
            apg.TypeActe = r["TYPEACTE"] is DBNull ? "" : Convert.ToString(r["TYPEACTE"]);
            return apg;
        }

    }
}
