using BasCommon_BO;
using BasCommon_BO.Compta;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL.Compta
{
    public static class DeviseMgmt
    {
        static  List<Devise> _devises = null;
        public static List<Devise> devises
        {
            get
            {
            if (_devises == null) _devises = getDevises();
            return _devises;
        }
            set
            {
                _devises = value;
            }
        }

        public static Devise getDevise(string devise)
        {
            foreach (Devise d in devises)
                if (d.CodeMonnaie == devise)
                    return d;

            return null;
        }

        private static List<Devise> getDevises()
        {
            JArray array = BasCommon_DAL.DAC.getMethodeJsonArray("/getDevises");
            List<Devise> lst = new List<Devise>();

            foreach (JObject dr in array)
            {
                Devise d = Builders.BuildDevise.BuildJson(dr);
                lst.Add(d);
            }
            return lst;
        }

        private static List<Devise> getDevisesOLD()
        {
            DataTable dt = BasCommon_DAL.DAC.getDevises();
            List<Devise> lst = new List<Devise>();

            foreach (DataRow dr in dt.Rows)
            {
                Devise d = Builders.BuildDevise.Build(dr);
                lst.Add(d);
            }
            return lst;
        }
    }
}
