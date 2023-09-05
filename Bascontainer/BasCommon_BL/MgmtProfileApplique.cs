using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.Data;
using BasCommon_DAL;

namespace BasCommon_BL
{
    public static class MgmtProfileApplique
    {


        private static List<ProfilApplique> _lstprofils;
        public static List<ProfilApplique> ProfilsApplique
        {
            get
            {
                if (_lstprofils == null) _lstprofils = getProfilsAppliques();
                return _lstprofils;
            }
            set
            {
                _lstprofils = value;
            }
        }

        private static List<ProfilApplique> getProfilsAppliques()
        {
            List<ProfilApplique> lst = new List<ProfilApplique>();
            DataTable dt = DAC.getProfils();

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildProfilApplique.Build(r));
            }
            return lst;
        }

    }
}
