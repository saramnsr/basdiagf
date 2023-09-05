using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using BasCommon_DAL;
using System.Data;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL
{
    public static class MgmtContactLib
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


        public static ContactLib getContacLib(int Id)
        {
            foreach (ContactLib cl in contactslibs)
                if (cl.Id == Id) return cl;

            return null;
        }
        private static List<ContactLib> getcontactlibs()
        {
            JArray json = DAC.getMethodeJsonArray("/ContactLibs");
            List<ContactLib> lst = new List<ContactLib>();

            foreach (JObject dr in json)
            {
                ContactLib cl = Builders.BuildContactLib.BuildJ(dr);
                lst.Add(cl);
            }

            return lst;
        }
        private static List<ContactLib> getcontactlibsOLD()
        {
            DataTable dt = DAC.getContactLib();

            List<ContactLib> lst = new List<ContactLib>();

            foreach (DataRow dr in dt.Rows)
            {
                ContactLib cl = Builders.BuildContactLib.Build(dr);
                lst.Add(cl);
            }

            return lst;
        }

        public static List<ContactLib> getLibs(ContactLib.AffecteA typepers)
        {

            List<ContactLib> lst = new List<ContactLib>();

            foreach (ContactLib cl in contactslibs)
                if ((cl.AffectedTo == typepers) || (cl.AffectedTo == ContactLib.AffecteA.Tous)) lst.Add(cl);

            return lst;
        }

        public static List<ContactLib> getLibs(ContactLib.AffecteA typepers, Contact.ContactType type)
        {

            List<ContactLib> lst = new List<ContactLib>();

            foreach (ContactLib cl in contactslibs)
                if ((cl.typeCtact == type) && ((cl.AffectedTo == typepers) || (cl.AffectedTo == ContactLib.AffecteA.Tous))) lst.Add(cl);

            return lst;
        }

        public static List<ContactLib> getLibs(ContactLib.AffecteA typepers, Contact.ContactType type, int priorite)
        {

            List<ContactLib> lst = new List<ContactLib>();

            foreach (ContactLib cl in contactslibs)
                if ((cl.typeCtact == type) && ((cl.AffectedTo == typepers) || (cl.AffectedTo == ContactLib.AffecteA.Tous)) && cl.Priorite == priorite) lst.Add(cl);

            return lst;
        }

        public static ContactLib getLib(int id)
        {
            foreach (ContactLib cl in contactslibs)
                if (cl.Id == id) return cl;

            return null;
        }

        public static ContactLib getLib(string s)
        {
            foreach (ContactLib cl in contactslibs)
                if (cl.Libelle.ToUpper() == s.ToUpper()) return cl;

            return null;
        }
    }
}
