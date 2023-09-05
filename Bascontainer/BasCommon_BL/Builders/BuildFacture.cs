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
    public static class BuildFacture
    {
        public static Facture Build(DataRow r)
        {

           // a.ID, a.DATEFACTURE, a.ID_PATIENT, a.NOMBRE_POINTS, a.MONTANT, a.MONTANT_LABO, a.MONTANT_STER, a.MONTANT_TOTAL, a.POINTS


            Facture facture = new Facture();
            facture.id = Convert.ToInt32(r["ID"]);
            facture.DateFacture = Convert.ToDateTime(r["DATEFACTURE"]);
            facture.patientFacture  = baseMgmtPatient .GetPatient (  Convert.ToInt32(r["ID_PATIENT"]));
            facture.NombrePoints = r["NOMBRE_POINTS"] is DBNull ? 0 : Convert.ToInt32(r["NOMBRE_POINTS"]);
            facture.Montant = r["MONTANT"] is DBNull ? 0 : Convert.ToDouble (r["MONTANT"]);
            facture.MontantLabo  = r["MONTANT_LABO"] is DBNull ? 0 : Convert.ToDouble(r["MONTANT_LABO"]);
            facture.MontantSterilisation = r["MONTANT_STER"] is DBNull ? 0 : Convert.ToDouble(r["MONTANT_STER"]);
            facture.MontantAchats = r["MONTANT_ACHATS"] is DBNull ? 0 : Convert.ToDouble(r["MONTANT_ACHATS"]);
            facture.MontantTotal = r["MONTANT_TOTAL"] is DBNull ? 0 : Convert.ToDouble(r["MONTANT_TOTAL"]);
            facture.Points = r["POINTS"] is DBNull ? 0 : Convert.ToDouble(r["POINTS"]);
           facture.MontantPaye  = r["MONTANT_PAYE"] is DBNull ? 0 : Convert.ToDouble(r["MONTANT_PAYE"]);
           facture.MontantRestant   = r["MONTANT_RESTANT"] is DBNull ? 0 : Convert.ToDouble(r["MONTANT_RESTANT"]);
           facture.DateDebutFacture = Convert.ToDateTime(r["DATEDEBUTFACTURE"]);
           facture.DateFinFacture = Convert.ToDateTime(r["DATEFINFACTURE"]);


                

            return facture;
        }

        public static FactureLigne BuildLigne(DataRow r)
        {
            FactureLigne factureLigne = new FactureLigne();
            factureLigne.id_Ligne = Convert.ToInt32(r["ID_LIGNE"]);
            factureLigne.id_Facture = Convert.ToInt32(r["ID_FACTURE"]);
            factureLigne.id_Echeance = Convert.ToInt32(r["ID_ECHEANCE"]);
            factureLigne.montantLigneFacture = Convert.ToDouble(r["MONTANTLIGNEFACTURE"]);
        //    factureLigne.date_Execution = Convert.ToDateTime(r["DATEEXECUTION"]);

            return factureLigne;
        }
        public static FinanceFacture BuildFinance(JObject obj)
        {
            FinanceFacture stat = new FinanceFacture();
            stat.id = Convert.ToInt32(obj["id"]);
            stat.montant = Convert.ToDouble(obj["montant"]);
            stat.dateFacture = Convert.ToDateTime(obj["date"]);
            stat.nom = Convert.ToString(obj["nom"]);

            return stat;
        }
    }
}
