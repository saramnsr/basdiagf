using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_BL;

namespace BasCommon_BL.Builders
{

    public static class BuildDebiteurs
    {

        public static Debiteurs BuildDbiteurs(DataRow r)
        {
            Debiteurs stat = new Debiteurs();
            stat.idPersonne = Convert.ToInt32(r["id_personne"]);
            stat.Montant = Convert.ToDouble(r["Montant"]);
           // stat.rangecalendar = r["typepayeur"] is DBNull ? Debiteurs.RangeCalendar.De0a6Mois : (Debiteurs.RangeCalendar)Convert.ToInt32(r["rangecalendar"]);
            stat.rangecalendar = r["rangecalendar"] is DBNull ? Debiteurs.RangeCalendar.De0a6Mois : (Debiteurs.RangeCalendar)Convert.ToInt32(r["rangecalendar"]);
           stat.typepayeur = r["typepayeur"] is DBNull ? BasCommon_BO.Echeance.typepayeur.inconnu : (BasCommon_BO.Echeance.typepayeur)Convert.ToInt32(r["typepayeur"]);
            stat.libelle = Convert.ToString(r["libelle"]);
            stat.patient = Convert.ToString(r["patient"]);
            stat.dateEcheance = Convert.ToDateTime(r["DTEECHEANCE"]);
            return stat;
        }
    }
}
