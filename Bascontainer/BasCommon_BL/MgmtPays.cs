using BasCommon_BO;
using BasCommon_DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL
{
    public class MgmtPays
    {
        private static List<Pays> _pays;
        public static List<Pays> pays
        {
            get
            {
                if (_pays == null)
                    _pays = getPays();
                return _pays;
            }
            set
            {
                _pays = value;
            }
        }
        public static Pays getPaysById(int id)
        {
            Pays p = pays.Find(w => w.id == id);
            return p;
        }

        public static Pays getPaysByIdOld(int id)
        {
            Pays p = new Pays();
            DataRow dr = DAC.getPaysById(id);
            p = Builders.BuildPays.Build(dr);
            return p;
        }

        private static List<Pays> getPays()
        {
            List<Pays> lst = new List<Pays>();
            JArray json = DAC.getMethodeJsonArray("/get_all_pays");
            foreach (JObject r in json)
            {
                Pays p = Builders.BuildPays.BuildJson(r);
                lst.Add(p);
            }
            return lst;
        }

        private static List<Pays> getPaysOld()
        {
            List<Pays> lst = new List<Pays>();

            DataTable dt = DAC.getPays();
            foreach (DataRow r in dt.Rows)
            {
                Pays p = Builders.BuildPays.Build(r);
                lst.Add(p);
            }
            return lst;
        }

    }
}
