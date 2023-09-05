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

    public static class BuildVirement
    {

      


        public static Virement Build(JObject r)
        {
            
            Virement v = new Virement();
            Echeance apg = new Echeance();

           
            apg.Id =  Convert.ToInt32(r["id"]);
            apg.IdActe =  Convert.ToInt32(r["idTraitement"]);            

            apg.DateEcheance = String.IsNullOrEmpty( r["dateEcheance"].ToString())  ? DateTime.Now.AddDays(-1) : Convert.ToDateTime(r["dateEcheance"]);
            apg.Libelle =String.IsNullOrEmpty( r["libelle"].ToString()) ? "" : Convert.ToString(r["libelle"]).Trim();
            apg.Montant =  Convert.ToDouble(r["montant"]);


            
            apg.ParPrelevement =  Convert.ToBoolean(r["parPrelevement"]);
            apg.ParVirement =  Convert.ToBoolean(r["parVirement"]);

            apg.IdPatient =  Convert.ToInt32(r["idPatient"]);
            apg.ID_Encaissement =  Convert.ToInt32(r["idEncaissement"]);
            apg.mutuelle =  Convert.ToInt32(r["idMutuelle"])== -1 ? null :MutuelleMgmt.getMutuelle(Convert.ToInt32(r["idMutuelle"]));
            apg.payeur =   (Echeance.typepayeur)Convert.ToInt32(r["typePayeur"]);


            
                apg.NomPatient =  Convert.ToString(r["nomPatient"]);


            v.echeance = apg;

            v.IdEntite =  Convert.ToInt32(r["idEntiteJuridique"]);
            v.Entite = EntiteJuridiqueMgmt.getentite(v.IdEntite);

            if (v.Entite.ComptesBancaire == null)
                v.Entite.ComptesBancaire = BanqueMgmt.getBanquesDeRemise(v.Entite);
            v.comptecabinet = v.Entite.ComptesBancaire.Count == 0 ? null : v.Entite.ComptesBancaire[0];

            return v;
        
 
        }

        public static Virement Build(DataRow r)
        {
            #region Old Commented Code
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
            #endregion

            Virement v = new Virement();
            Echeance apg = new Echeance();
            apg.Id = r["ID"] is DBNull ? -1 : Convert.ToInt32(r["ID"]);
            apg.IdActe = r["ID_TRAITEMENT"] is DBNull ? -1 : Convert.ToInt32(r["ID_TRAITEMENT"]);

            //La date d'echeance ne peut plus être à null
            //Donc si on tombe sur une date d'echeance à null, elle est remise à hier pour etre réglé le plus vite possible

            apg.DateEcheance = r["DTEECHEANCE"] is DBNull ? DateTime.Now.AddDays(-1) : Convert.ToDateTime(r["DTEECHEANCE"]);
            apg.Libelle = r["LIBELLE"] is DBNull ? "" : Convert.ToString(r["LIBELLE"]).Trim();
            apg.Montant = r["MONTANT"] is DBNull ? 0 : Convert.ToDouble(r["MONTANT"]);
            apg.ParPrelevement = r["PARPRELEVEMENT"] is DBNull ? false : Convert.ToBoolean(r["PARPRELEVEMENT"]);
            apg.ParVirement = r["PARVIREMENT"] is DBNull ? false : Convert.ToBoolean(r["PARVIREMENT"]);

            apg.IdPatient = r["ID_PATIENT"] is DBNull ? 0 : Convert.ToInt32(r["ID_PATIENT"]);
            apg.ID_Encaissement = r["ID_ENCAISSEMENT"] is DBNull ? -1 : Convert.ToInt32(r["ID_ENCAISSEMENT"]);
            apg.mutuelle = r["ID_MUTUELLE"] is DBNull ? null : MutuelleMgmt.getMutuelle(Convert.ToInt32(r["ID_MUTUELLE"]));
            apg.payeur = r["TYPEPAYEUR"] is DBNull ? Echeance.typepayeur.patient : (Echeance.typepayeur)Convert.ToInt32(r["TYPEPAYEUR"]);


            if (r.Table.Columns.Contains("NomPatient"))
                apg.NomPatient = r["NomPatient"] is DBNull ? null : Convert.ToString(r["NomPatient"]);


            v.echeance = apg;

            v.IdEntite = r["ID_ENTITYJURIDIQUE"] is DBNull ? -1 : Convert.ToInt32(r["ID_ENTITYJURIDIQUE"]);
            v.Entite = EntiteJuridiqueMgmt.getentite(v.IdEntite);

            if (v.Entite.ComptesBancaire == null)
                v.Entite.ComptesBancaire = BanqueMgmt.getBanquesDeRemise(v.Entite);
            v.comptecabinet = v.Entite.ComptesBancaire.Count == 0 ? null : v.Entite.ComptesBancaire[0];

            return v;
        }
    }
}
