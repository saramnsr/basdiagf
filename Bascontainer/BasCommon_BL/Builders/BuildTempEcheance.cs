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

    public static class BuildTempEcheance
    {
        public static TempEcheanceDefinition Build(DataRow r)
        {
            TempEcheanceDefinition ted = new TempEcheanceDefinition();

            ted.Id = Convert.ToInt32(r["ID"]);
            ted.AlreadyPayed = false;
            ted.CanRecalculate = true;
            ted.DAteEcheance = r["DTEECHEANCE"]==DBNull.Value?DateTime.Now.AddYears(1):Convert.ToDateTime(r["DTEECHEANCE"]);
            ted.Libelle = Convert.ToString(r["LIBELLE"]);
            ted.ParPrelevement = Convert.ToString(r["PARPRELEVEMENT"]) == "True";
            ted.ParVirement = Convert.ToString(r["ParVirement"]) == "True";
            
            ted.payeur = (Echeance.typepayeur)Convert.ToInt32(r["TYPEPAYEUR"]);
            ted.Montant = Convert.ToDouble(r["MONTANT"]);
            ted.IdSemestre = Convert.ToInt32(r["ID_SEM_PROPOSE"]);
            
            return ted;
        }
        public static TempEcheanceDefinition BuildEcheance(DataRow r)
        {
            TempEcheanceDefinition ted = new TempEcheanceDefinition();

            ted.Id = Convert.ToInt32(r["ID"]);
            ted.AlreadyPayed = false;
            ted.CanRecalculate = true;
            ted.DAteEcheance = r["DTEECHEANCE"] == DBNull.Value ? DateTime.Now.AddYears(1) : Convert.ToDateTime(r["DTEECHEANCE"]);
            ted.Libelle = Convert.ToString(r["LIBELLE"]);
            ted.ParPrelevement = Convert.ToString(r["PARPRELEVEMENT"]) == "True";
            ted.ParVirement = Convert.ToString(r["ParVirement"]) == "True";

            ted.payeur = (Echeance.typepayeur)Convert.ToInt32(r["TYPEPAYEUR"]);
            ted.Montant = Convert.ToDouble(r["MONTANT"]);
           

            return ted;
        }

        public static TempEcheanceDefinition Build(JObject r)
        {
            TempEcheanceDefinition ted = new TempEcheanceDefinition();

            ted.Id = Convert.ToInt32(r["id"]);
            ted.AlreadyPayed = false;
            ted.CanRecalculate = true;
            ted.DAteEcheance = r["dteecheance"].ToString() == "" ? DateTime.Now.AddYears(1) : Convert.ToDateTime(r["dteecheance"]);
            ted.Libelle = Convert.ToString(r["libelle"]);
            ted.ParPrelevement = Convert.ToString(r["parprelevement"]) == "True";
            ted.ParVirement = Convert.ToString(r["parvirement"]) == "True";

            ted.payeur = (Echeance.typepayeur)Convert.ToInt32(r["typepayeur"]);
            ted.Montant = Convert.ToDouble(r["montant"]);
            ted.IdSemestre = Convert.ToInt32(r["id_SEM_PROPOSE"]);

            return ted;
        }
    }


    public static class BuildEcheanceDevisALaCarte
    {
        public static EcheanceDevisALaCarte Build(DataRow r)
        {
            EcheanceDevisALaCarte ted = new EcheanceDevisALaCarte();

            ted.Id = Convert.ToInt32(r["ID"]);
            ted.AlreadyPayed = false;
            ted.CanRecalculate = true;
            ted.DAteEcheance = r["DTEECHEANCE"]==DBNull.Value?DateTime.Now.AddYears(1):Convert.ToDateTime(r["DTEECHEANCE"]);
            ted.Libelle = Convert.ToString(r["LIBELLE"]);
            ted.ParPrelevement = Convert.ToString(r["PARPRELEVEMENT"]) == "True";
            ted.ParVirement = Convert.ToString(r["ParVirement"]) == "True";
            
            ted.payeur = (Echeance.typepayeur)Convert.ToInt32(r["TYPEPAYEUR"]);
            ted.Montant = Convert.ToDouble(r["MONTANT"]);
            ted.IdDevis = Convert.ToInt32(r["ID_DEVIS"]);

            return ted;
        }


        
    }
}
