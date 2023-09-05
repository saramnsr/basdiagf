using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_DAL;
using BASEDiag_BO;

namespace BASEDiag_BL
{
    public static class ContactLibMgmt
    {



        private static List<ContactLib> _contactslibs = null;
        public static List<ContactLib> contactslibs
        {
            get
            {
                if (_contactslibs == null) _contactslibs = getcontactlibs();
                return _contactslibs;
            }
            set
            {
                _contactslibs = value;
            }
        }




        private static List<ContactLib> getcontactlibs()
        {
            DataTable dt = DAC.getContactLib();

            List<ContactLib> lst = new List<ContactLib>();

            foreach (DataRow dr in dt.Rows)
            {
                ContactLib cl = Builders.BuildContactLib(dr);
                lst.Add(cl);
            }

            return lst;
        }


        public static ContactLib getLib(int id)
        {
            foreach (ContactLib cl in contactslibs)
                if (cl.Id == id) return cl;

            return null;
        }
    }
}
