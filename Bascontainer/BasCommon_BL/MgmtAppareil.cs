using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.Data;
using BasCommon_DAL;

namespace BasCommon_BL
{
    public class AppareilMgmt
    {
        private static List<Appareil> _appareils;
        public static List<Appareil> appareils
        {
            get
            {
                if (_appareils == null)
                    _appareils = getappareils();

                return _appareils;
            }

        }

        private static List<Appareil> getappareils()
        {
            DataTable dt = DAC.getAppareils();

            List<Appareil> lst = new List<Appareil>();
            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildAppareil.Build(r));
            }
            return lst;
        }

        public static Appareil getAppareil(int Id)
        {
            foreach (Appareil f in appareils)
                if (f.Id == Id) return f;

            return null;

        }

        public static Appareil getAppareil(string Code)
        {
            foreach (Appareil f in appareils)
                if (f.Code == Code) return f;

            return null;

        }
    }
}
