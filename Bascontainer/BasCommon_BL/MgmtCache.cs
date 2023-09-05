using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BasCommon_BL
{
    public static class MgmtCommonCache
    {

       

        public static basePatient CurrentPatient
        {
            get
            {
                if (HistoPatient.Count == 0) return null;
                return HistoPatient[HistoPatient.Count-1];
            }
            
        }



        private static List<basePatient> _HistoPatient = new List<basePatient>();
        public static List<basePatient> HistoPatient
        {
            get
            {
                return _HistoPatient;
            }
            set
            {
                _HistoPatient = value;
            }
        }


        public static void Change(basePatient pat)
        {
            if (HistoPatient.Contains(pat))
                HistoPatient.Remove(pat);

           HistoPatient.Add(pat);
        }

        public static void Change(int Idpat)
        {
            basePatient p = null;

            foreach (basePatient bp in HistoPatient)
                if (bp.Id == Idpat)
                {

                    HistoPatient.Remove(bp);
                    HistoPatient.Add(bp);
                    return;
                }

            p = baseMgmtPatient.GetPatient(Idpat);
            HistoPatient.Add(p);
        }

       
    }
}
