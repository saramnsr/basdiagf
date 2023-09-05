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
    public static class StatusMgt
    {

        public static List<Status> Status
        {
            get
            {
                return GetRHStatus();
            }
            
        }
        private static List<Status> GetRHStatus()
        {
            List<Status> lst = new List<Status>();
            JArray jArray = BasCommon_DAL.DAC.getMethodeJsonArray("/getAllRhStatus");
            foreach(JObject obj in jArray)
                lst.Add(BasCommon_BL.Builders.BuildStatus.Build(obj));
            
            return lst;
        }
        

        private static List<Status> GetRHStatusOld()
        {
            
            List<Status> lst = new List<Status>();

            
            DataTable dt = DAC.getUserStatus();

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(BasCommon_BL.Builders.BuildStatus.BuildOld(r));
            }

            return lst;
        }

        public static Status getStatus(int Id)
        {
            foreach (Status s in Status)
                if (s.Id == Id) return s;
            return null;
        }

        public static Status getStatus(string code)
        {
            foreach (Status s in Status)
                if (s.code.Equals(code)) return s;
            return null;
        }

        public static List<Status> GetStatus(bool IsAbsence)
        {
            List<Status> lst = new List<Status>();
            
            foreach (Status s in Status)
                if (s.IsAnAbsence) lst.Add(s);
            return lst;
          
        }



        public static Boolean IsExistStatus(Utilisateur p_Utilisateur, UserStatus p_statusToApply)
        {
            return DAC.IsExistStatus(p_Utilisateur, p_statusToApply);
        }

        public static void Deletestatuts(StatusCliniqueManuel s)
        {
            
           DAC.Deletestatuts(s);
        }

        public static Boolean verifstatut(StatusCliniqueManuel s)
        {
            return DAC.verifstatut(s);
        
        
        }
        
    
            
    }
}
