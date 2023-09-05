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
    public static class MgmtRecontact
    {





        public static void AddRecontact(Recontact rec)
        {
            DAC.AddRecontact(rec);
        }

        public static void ValidateRecontacts(int IdPatient)
        {
            DAC.ValidateRecontacts(IdPatient);
        }
        public static Recontact GetCurrentRecontact(basePatient pat)
        {
            JObject json = DAC.getMethodeJsonObjet("/CurrentRecontact/"+ pat.Id);
            if (json == null) return null;
            Recontact re = Builders.BuildRecontact.BuildJ(json);

            re.usertentative = BasCommon_BL.UtilisateursMgt.getUtilisateur(re.IdUserTentative);
            re.patient = pat;

            return re;
        }
        public static Recontact GetCurrentRecontactOLD(basePatient pat)
        {
            DataRow r = DAC.getCurrentRecontactPatient(pat.Id);
            if (r == null) return null;
            Recontact re = Builders.BuildRecontact.Build(r);

            re.usertentative = BasCommon_BL.UtilisateursMgt.getUtilisateur(re.IdUserTentative);
            re.patient = pat;

            return re;
        }


        public static List<Recontact> GetAllRecontacts(int IdPatient)
        {
            DataTable dt = DAC.getAllRecontacts(IdPatient);
            if (dt == null) return null;            
            
            List<Recontact> lst = new List<Recontact>();
            foreach (DataRow r in dt.Rows)
            {
                Recontact re = Builders.BuildRecontact.Build(r);
                

                lst.Add(re);
            }

            return lst;
        }




    }
}
