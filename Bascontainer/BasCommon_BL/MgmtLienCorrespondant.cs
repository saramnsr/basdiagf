using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using BasCommon_DAL;
using BasCommon_BO;

namespace BasCommon_BL
{
    public static class MgmtLienCorrespondant
    {

        public static void ReaffecterCorrespondant(int IdOldCorrespondant, int IdNewCorrespondant)
        {
            DAC.ReaffecterCorrespondant(IdOldCorrespondant, IdNewCorrespondant);
        }


        public static void InsertLienPers(LienCorrespondant lp)
        {
            DAC.insertLienPers(lp);
        }
    }
}
