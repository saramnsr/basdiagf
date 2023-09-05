using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_BO;
using BASEDiag_DAL;
using BasCommon_BO;

namespace BASEDiag_BL
{
    public static class SurveillanceMgmt
    {


        public static void DeleteSurveillance(Surveillance surv)
        {
            DAC.DeleteSurveillance(surv);


        }

        public static void AddSurveillance(Surveillance surv)
        {
            if (surv.Id == -1)
                DAC.InsertSurveillance(surv);


        }

        public static List<Surveillance> getSurveillances(Semestre sem)
        {
            List<Surveillance> lst = new List<Surveillance>();
            DataTable dt = DAC.getSurveillances(sem);

            foreach (DataRow dr in dt.Rows)
            {
                Surveillance s = Builders.BuildSurveillance(dr);
                lst.Add(s);
            }
            return lst;
        }





    }
}
