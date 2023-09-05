using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_DAL;
using Newtonsoft.Json.Linq;
namespace BasCommon_BL
{
    public static class StatusCliniqueManuelMgmt
    {

        private static List<StatusCliniqueManuel> _status = null;
        public static List<StatusCliniqueManuel> status
        {
            get
            {
                if (_status == null) _status = getStatus();
                return _status;
            }
            set
            {
                _status = value;
            }
        }


        private static List<StatusCliniqueManuel> getStatus()
        {
            JArray json = DAC.getMethodeJsonArray("/Statuts");

            List<StatusCliniqueManuel> lst = new List<StatusCliniqueManuel>();

            foreach (JObject dr in json)
            {
                StatusCliniqueManuel scm = Builders.BuildStatusCliniqueManuel.BuildJ(dr);
                lst.Add(scm);

            }
            return lst;
        }
        private static List<StatusCliniqueManuel> getStatusOLD()
        {
            DataTable dt = DAC.getStatus();

            List<StatusCliniqueManuel> lst = new List<StatusCliniqueManuel>();

            foreach (DataRow dr in dt.Rows)
            {
                StatusCliniqueManuel scm = Builders.BuildStatusCliniqueManuel.Build(dr);
                lst.Add(scm);

            }
            return lst;
        }



        public static StatusCliniqueManuel GetStatus(int id)
        {
            foreach (StatusCliniqueManuel scm in status)
                if (scm.Id == id)
                    return scm;

            return null;
        }

        
        

        public static List<CustomStatusClinique> getCurrentCustomStatusCliniqueFromIdPersonne(basePatient pat)
        {
            List<CustomStatusClinique> lst = new List<CustomStatusClinique>();
            if (pat.Customstatus==null)
                pat.Customstatus = GetCustomStatusCliniqueFromIdPersonne(pat.Id);

            foreach(CustomStatusClinique csc in pat.Customstatus)
                if (csc.DateFin==null)
                    lst.Add(csc);

            return lst;
        }


        public static void Save(StatusCliniqueManuel s)
        {
            if (s.Id == -1)
                InsertStatut(s);
            else
                UpdateSatut(s);
        }

        public static void InsertStatut(StatusCliniqueManuel s)
        {
            DAC.insertstatuts(s);
            status.Add(s);
        }

        public static void UpdateSatut(StatusCliniqueManuel s)
        {
            DAC.updatestatuts(s);
        }

        public static void DeleteSatut(StatusCliniqueManuel statusToDelete, StatusCliniqueManuel statusToReplaceWith)
        {
            DAC.ReaffectStatus(statusToDelete, statusToReplaceWith);
            DAC.deletestatuts(statusToDelete);
            status.Remove(statusToDelete);
        }

        public static void ReaffectStatus(StatusCliniqueManuel oldstatus,StatusCliniqueManuel newstatus)
        {

            DAC.ReaffectStatus(oldstatus, newstatus);
        }

       
        public static List<CustomStatusClinique> GetCustomStatusCliniqueFromIdPersonne(int id)
        {
            List<CustomStatusClinique> lst = new List<CustomStatusClinique>();

            JArray json = DAC.getMethodeJsonArray("/HistoStatusFromIdPersonne/" + id);
            foreach (JObject r in json)
            {
                lst.Add(Builders.BuildCustomStatusClinique.BuildJ(r));
            }
            return lst;
        }
        public static List<CustomStatusClinique> GetCustomStatusCliniqueFromIdPersonneOLD(int id)
        {
            List<CustomStatusClinique> lst = new List<CustomStatusClinique>();
            DataTable dt;

            dt = DAC.getCustomStatusCliniqueFromIdPersonne(id);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildCustomStatusClinique.Build(r));
            }
            return lst;
        }
        public static void UpdateCategorieBeToWas(CustomStatusClinique custo)
        {
            DAC.updateCustomStatusCliniqueBeToWas(custo);
        }

        public static void UpdateCategorieWasToBe(CustomStatusClinique custo)
        {
            DAC.updateCustomStatusCliniqueWasToBe(custo);
        }
    }
}
