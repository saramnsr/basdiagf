using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_BO;
using BASEDiag_DAL;

namespace BASEDiag_BL
{
    public class DevisMgmt
    {



        public static Devis getLastDevis(Patient patient)
        {
            DataRow dr = DAC.getLastDevis(patient);

            if (dr==null) return null;
            return Builders.BuildDevis(dr);
        }


        public static List<Devis> getDevis(Patient patient)
        {
            DataTable dt = DAC.getDevis(patient);

            List<Devis> lst = new List<Devis>();
            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildDevis(r));
            }
            return lst;
        }

        public static void InsertDevis(Devis devis)
        {
            DAC.InsertDevis(devis);
        }

        public static void UpdateDevis(Devis devis)
        {
            DAC.UpdateDevis(devis);
        }

        public static void Save(Devis devis)
        {

            if (devis.Id == -1)
                DAC.InsertDevis(devis);
            else
                DAC.UpdateDevis(devis);
        }

        
    }
}
