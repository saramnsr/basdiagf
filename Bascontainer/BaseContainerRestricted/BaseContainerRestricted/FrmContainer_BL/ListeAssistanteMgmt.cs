using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FrmContainer_BO;
using BasCommon_BO;
using BASEPractice_DAL;

namespace FrmContainer_BL
{
    public static class ListeAssistanteMgmt
    {


        public static List<PatientSoldeNegatifCeJour> GetPatientsSoldeNegatifCeJour(Utilisateur responsable)
        {

            List<PatientSoldeNegatifCeJour> lst = new List<PatientSoldeNegatifCeJour>();
            DataTable dt;

            dt = DAC.GetPatientsSoldeNegatifCeJour(responsable);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildPatientSoldeNegatifCeJour(r));
            }
            return lst;
        }

        public static List<PatientEnRecontact> getPatientsEnRecontact(Utilisateur Assresponsable)
        {

            List<PatientEnRecontact> lst = new List<PatientEnRecontact>();
            DataTable dt;

            dt = DAC.getPatientEnRecontact(Assresponsable);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildPatientEnRecontact(r));
            }
            return lst;
        }

        public static List<PatientARecontacter> getPatientsSansProchainsRDV()
        {

            List<PatientARecontacter> lst = new List<PatientARecontacter>();
            DataTable dt;

            dt = DAC.getPatientSansProchainRDV();

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildPatientARecontacter(r));
            }
            return lst;
        }


        public static List<PatientARelancer> getPatientsARelancer()
        {
            List<PatientARelancer> lst = new List<PatientARelancer>();
            DataTable dt;

            dt = DAC.GetPatientsARelancer();

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildPatientARelancer(r));
            }
            return lst;
        }

        public static List<baseSmallPersonne> getPatientsEnStatus(StatusCliniqueManuel statut, Utilisateur AssResponsable)
        {

            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();

            if (statut == null) return lst;
            DataTable dt;

            dt = DAC.getPatientsEnStatus(statut, AssResponsable);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(BasCommon_BL.Builders.BuildSmallPersonne.Build(r));
            }
            return lst;
        }

    }
}
