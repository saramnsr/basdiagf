using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_DAL;
using BasCommon_BO;

namespace BasCommon_BL
{
    public static class MgmtMotifRecontact
    {


        private static List<RecontactLib> _LibsRecontact = null;
        public static List<RecontactLib> LibsRecontact
        {
            get
            {
                if (_LibsRecontact == null) _LibsRecontact = GetLibRecontact();
                return _LibsRecontact;
            }
            set
            {
                _LibsRecontact = value;
            }
        }



        private static List<RecontactLib> GetLibRecontact()
        {
            DataTable dt = DAC.getRecontactLib();

            List<RecontactLib> ls = new List<RecontactLib>();

            foreach (DataRow dr in dt.Rows)
            {
                RecontactLib rl = Builders.BuildRecontactLib.Build(dr);
                ls.Add(rl);
            }

            return ls;
        }
    }
}
