using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.Data;
using BasCommon_DAL;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL
{
    public static class MutuelleMgmt
    {
        private static List<Mutuelle> _Mutuelles;
        public static List<Mutuelle> Mutuelles
        {
            get
            {
                if (_Mutuelles == null) _Mutuelles = getAllMutuelles();
                return _Mutuelles;
            }
            set
            {
                _Mutuelles = value;
            }
        }

        static List<Mutuelle> getAllMutuelles()
        {
            JArray json= DAC.getMethodeJsonArray("/Mutuelles");
            List<Mutuelle> lst = new List<Mutuelle>();
    
            foreach (JObject r in json)
            {
                lst.Add(Builders.BuildMutuelle.BuildJ(r));
            }
            return lst;
        }
        static List<Mutuelle> getAllMutuellesOLD()
        {
            List<Mutuelle> lst = new List<Mutuelle>();
            DataTable dt;

            dt = DAC.getMutuelles();

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildMutuelle.Build(r));
            }
            return lst;
        }
        public static Mutuelle getMutuelle(int p_ID)
        {


            foreach (Mutuelle c in Mutuelles)
                if (c.Id == p_ID)
                    return c;

            return null;
        }

        public static void Save(Mutuelle mut)
        {


            if (mut.Id == -1)
            {
                InsertMutuelle(mut);
                Mutuelles.Add(mut);
            }
            else
            {
                UpdateMutuelle(mut);
            }
            Mutuelles = null;
        }

        private static void UpdateMutuelle(Mutuelle mut)
        {
            DAC.UpdateMutuelle(mut);
        }

        private static void InsertMutuelle(Mutuelle mut)
        {
            DAC.InsertMutuelle(mut);
        }


        
    }
}
