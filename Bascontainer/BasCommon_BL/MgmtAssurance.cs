using BasCommon_BO;
using BasCommon_DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BasCommon_BL
{
    public class MgmtAssurance
    {
        public static Assurance GetAssuranceById(int id)
        {

            Assurance assurance = new Assurance();

            JObject json = DAC.getMethodeJsonObjet("/assuranceByIdPatient/" + id);
            if (json == null) return null;
            assurance = Builders.BuildAssurance.Build(json);
            return assurance;
        }
        public static Assurance GetAssuranceByIdOLD(int id)
        {
            Assurance assurance = new Assurance();
            DataRow r = DAC.getAssurance(id);

            if (r == null) return null;
            assurance = Builders.BuildAssurance.Build(r);
            return assurance;
        }
        public static void addAssurance(Assurance assurance)
        {
            DAC.AddAssurance(assurance);

        }
    }
}
