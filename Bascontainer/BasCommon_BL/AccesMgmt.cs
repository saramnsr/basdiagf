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
    public static class AccessMgmt
    {


        private static List<AccessObject> _lstaccessobj;
        public static List<AccessObject> lstaccessobj
        {
            get
            {
                if (_lstaccessobj == null)
                    _lstaccessobj = getAccessObjects();
                return _lstaccessobj;
            }
            set
            {
                _lstaccessobj = value;
            }
        }
        private static List<AccessObject> getAccessObjects()
        {
            JArray json = DAC.getMethodeJsonArray("/AllAccessObject");
            List<AccessObject> lst = new List<AccessObject>();
            

            foreach (JObject dr in json)
            {
                AccessObject b = Builders.BuildAccessObject.BuildJ(dr);
                if (b.Utilisateur != null)
                    lst.Add(b);
            }

            return lst;
        }
        private static List<AccessObject> getAccessObjectsOLD()
        {
            List<AccessObject> lst = new List<AccessObject>();
            DataTable dt = DAC.getAllAccessObject();

            foreach (DataRow dr in dt.Rows)
            {
                AccessObject b = Builders.BuildAccessObject.Build(dr);
                if (b.Utilisateur != null)
                    lst.Add(b);
            }

            return lst;
        }

        public static AccessObject getAccessObject(int IdUser)
        {
            foreach (AccessObject ao in lstaccessobj)
                if ((ao.Utilisateur != null) && (ao.Utilisateur.Id == IdUser))
                    return ao;

            return null;
        }

        public static AccessObject GetAccessObj(string password)
        {            
            foreach (AccessObject ao in lstaccessobj)
            {
                if (ao.Password.Equals(password))
                {
                    return ao;
                }
            }
            return null;
        }

        public static void AddNewAO(AccessObject access)
        {
            DAC.AddNewAO(access);
        }
        public static void updateAccessObject(AccessObject access)
        {
            DAC.updateAccessObject(access);
        }

    }
}
