using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using BasCommon_DAL;
using System.Data;

namespace BasCommon_BL
{
    public static class MgmtMvtCaisse
    {


        public static double getMontantEnCaisse()
        {
            return Convert.ToDouble(DAC.getMethodeJsonString("/MontantEnCaisse").Replace('.',','));
        }
        public static double getMontantEnCaisseOLD()
        {
            return DAC.getMontantEnCaisse();
        }
        public static List<MvtCaisse> getMvtCaisse()
        {

            DataTable dt = DAC.getMvtsCaisse();

            List<MvtCaisse> lst = new List<MvtCaisse>();

            foreach (DataRow r in dt.Rows)
            {
                MvtCaisse ec = Builders.BuildMvtCaisse.Build(r);
                lst.Add(ec);
            }



            return lst;
        }

        public static void AddMvtCaisse(double Montant, Utilisateur utilisateur)
        {
            if (Montant!=0)
                DAC.InsertMvtCaisse(Montant, utilisateur);
        }
    }
}
