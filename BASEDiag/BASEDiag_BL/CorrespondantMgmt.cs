using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_BO;
using BASEDiag_DAL;
using BasCommon_BO;
using BasCommon_BL;

namespace BASEDiag_BL
{
    public static class CorrespondantMgmt
    {




        public static void setPersonnesAContacter(Patient patient)
        {           
            DAC.setPersonnesAContacter(patient);
        }

        public static List<LienCorrespondant> getPersonnesAContacter(Patient patient)
        {
            List<LienCorrespondant> lst = new List<LienCorrespondant>();
            DataTable dt;

            dt = DAC.getPersonnesAContacter(patient);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildLienCorrespondant(r));
            }
            return lst;
        }


        public static List<LienCorrespondant> getCorrespondantsOf(Patient patient)
        {
            List<LienCorrespondant> lst = new List<LienCorrespondant>();
            DataTable dt;

            dt = DAC.getCorrespondantsOf(patient);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildLienCorrespondant(r));
            }
            return lst;
        }


       
        

        public static Correspondant getCorrespondant(int id)
        {           
            DataRow r  = DAC.getCorrespondant(id);

            Correspondant corres = Builders.BuildCorrespondant(r);

            FillContacts(corres);

            return corres;
        }

        public static List<SmallCorrespondant> getCorrespondants(string search)
        {
            List<SmallCorrespondant> lst = new List<SmallCorrespondant>();
            DataTable dt;

            dt = DAC.getSmallCorrespondants(search);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildSmallCorrespondant(r));
            }
            return lst;
        }

      
        public static List<SmallCorrespondant> getPraticiens()
        {
            List<SmallCorrespondant> lst = new List<SmallCorrespondant>();
            DataTable dt;

            dt = DAC.getPraticiens();

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildSmallCorrespondant(r));
            }
            return lst;
        }

        public static List<object> getCorrespondantsSuggested()
        {

            List<object> lst = new List<object>();
            DataTable dt;

            dt = DAC.getCorrespondantsSugested();

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildRestrictedCorrespondant(r));
            }
            return lst;

        }

        public static List<object> getVilles()
        {

            List<object> lst = new List<object>();
            DataTable dt;

            dt = DAC.getVillesSugested();

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildVille(r));
            }
            return lst;

        }

        public static void SaveCorrespondant(Correspondant correspondant)
        {
            if (correspondant.Id == -1)
            {
                DAC.InsertCorrespondant(correspondant);
            }
            else
            {
                DAC.UpdateCorrespondant(correspondant);
            }
        }



        public static List<RestrictedCorrespondant> getCorrespondantsSuggestedByProfNParam(string Profession, string param)
        {

            List<RestrictedCorrespondant> lst = new List<RestrictedCorrespondant>();
            DataTable dt;

            dt = DAC.getCorrespondantsSugestedByProfNParam(Profession,param);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildRestrictedCorrespondant(r));
            }
            return lst;

        }

        public static List<object> getCorrespondantsSuggestedByProf( string Profession)
        {

            List<object> lst = new List<object>();
            DataTable dt;

            dt = DAC.getCorrespondantsSugestedByProf(Profession);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildRestrictedCorrespondant(r));
            }
            return lst;

        }

    }
}
