using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using BasCommon_BO.Compta;
using System.Data;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL.Compta
{
    public static class JournalMgmt
    {
        static List<Journal> _Journaux = null;
        public static List<Journal> Journaux
        {
            get
            {
                if (_Journaux == null) _Journaux = GetJourneaux();
                return _Journaux;
            }
            set { _Journaux = value; }
        }

        public static Journal getJournal(string code)
        {
            foreach (Journal j in Journaux)
                if (j.NumJournal.Trim() == code.Trim()) 
                    return j;


            return null;
        }

        public static void Add(Journal cc)
        {
            BasCommon_DAL.DAC.InsertJournal(cc);

            _Journaux.Add(cc);
        }
        private static List<Journal> GetJourneaux()
        {
            List<Journal> lst = new List<Journal>();
            JArray json = BasCommon_DAL.DAC.getMethodeJsonArray("/Journeaux");
            foreach (JObject dr in json)
            {
                Journal j = Builders.BuildJourneaux.BuildJ(dr);
                lst.Add(j);
            }

            return lst;
        }

        private static List<Journal> GetJourneauxOLD()
        {
            List<Journal> lst = new List<Journal>();
            DataTable dt = BasCommon_DAL.DAC.GetJourneaux();

            foreach (DataRow dr in dt.Rows)
            {
                Journal j = Builders.BuildJourneaux.Build(dr);
                lst.Add(j);
            }

            return lst;
        }
    }
}

