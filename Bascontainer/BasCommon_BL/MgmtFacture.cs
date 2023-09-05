using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using BasCommon_DAL;
using System.Data;

namespace BasCommon_BL
{
    public static class MgmtFacture
    {

        public static void addFacture(Facture facture,Boolean vAcquitter)
        {
            DAC.AddFacture(facture, vAcquitter );
            
           
        }
        public static Facture  GetFactureById(int id)
          {

              DataRow r = DAC.GetFactureById(id);

              if (r == null) return null;
              return Builders.BuildFacture.Build(r);


              Facture facture = GetFactureById(id);
            
              facture.factureLigne = GetLigneFacture(id);

              return facture;
        }

        public static void addFactureLigne(FactureLigne factureLigne, Boolean vAcquitter)
        {
            DAC.AddFactureLigne(factureLigne, vAcquitter);

        }
        public static List<Facture> GetFactures (basePatient patient)
        {
            DataTable dt = DAC.GetFactures(patient);

            List<Facture> _listFactures = new List<Facture>();

            foreach (DataRow r in dt.Rows)
            {
                Facture facture = Builders.BuildFacture.Build(r);
                _listFactures.Add(facture);

            }

            return _listFactures;
        }
        public static List<FactureLigne> GetLigneFacture(int idFacture)
        {
            DataTable dt = DAC.GetLigneFacture(idFacture);

            List<FactureLigne> _listLigneFactures = new List<FactureLigne>();

            foreach (DataRow r in dt.Rows)
            {
                FactureLigne factureLigne = Builders.BuildFacture.BuildLigne(r);
                _listLigneFactures.Add(factureLigne);

            }

            return _listLigneFactures;
        }
        public static Facture GetFactureAcquitter(int idFacture)
        {
            DataRow r = DAC.GetFactureAcquiter(idFacture);
            if (r == null) return null;
                Facture facture = Builders.BuildFacture.Build(r);
           

            return facture;
        }
    }
}
