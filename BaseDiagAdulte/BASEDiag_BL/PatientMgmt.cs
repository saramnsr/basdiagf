using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_BO;
using BASEDiag_DAL;
using BasCommon_BL;
using BasCommon_BO;

namespace BASEDiag_BL
{
    public static class PatientMgmt
    {



        public static List<RestrictedPat> getRestrictedPatientsInAttente()
        {
            List<RestrictedPat> lst = new List<RestrictedPat>();
            DataTable dt = DAC.getPatientsEnAttenteOuSurFauteuil();

            foreach (DataRow r in dt.Rows)
                lst.Add(Builders.BuildRestrictedPatient(r));


            return lst;

        }




        public static void FillContacts(basePatient pat)
        {
            pat.contacts = MgmtContact.getContactsOf(pat.Id);

            foreach (Contact c in pat.contacts)
            {
                if ((c.TypeContact == Contact.ContactType.Adresse) && (pat.MainAdresse==null))
                {
                    pat.MainAdresse = c;
                }

                if ((c.TypeContact == Contact.ContactType.Mail) && (pat.MainMail == null))
                {
                    pat.MainMail = c;
                }
            }
        }

        public static List<RestrictedPat> getRestrictedPatientsInAttenteFor(Fauteuil f)
        {
            List<RestrictedPat> lst = new List<RestrictedPat>();
            DataTable dt = DAC.getPatientsEnAttenteOuSurFauteuil();

            foreach (DataRow r in dt.Rows)
            {
                int idfauteuil = r["ID_FAUTEUIL"] is DBNull ? -1 : Convert.ToInt32(r["ID_FAUTEUIL"]);
                if (idfauteuil == f.Id) lst.Add(Builders.BuildRestrictedPatient(r));
            }
            return lst;

        }
                
        public static List<RestrictedPat> getRestrictedPatients(string nom)
        {
            DataTable dt = DAC.getRestrictedPatients(nom);
            List<RestrictedPat> lst = new List<RestrictedPat>();

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildRestrictedPatient(r));

            }
            return lst;
        }

        /*
        public static Patient getPatient(int Id)
        {
            DataRow dr = DAC.getPatient(Id);
            if (dr == null) return null;
            Patient pat = Builders.BuildPatient(dr);
                        
            AffectRepertoireToPatient(pat);

            ImagesMgmt.AffectImageToPatient(ref pat);


            List<LienCorrespondant> lst = BASEDiag_BL.CorrespondantMgmt.getCorrespondantsOf(pat);

            foreach (LienCorrespondant lc in lst)
            {
                if (lc.IsRecomande)
                    pat.RecoBy.Add(lc);
                
                pat.Correspondants.Add(lc);

            }
                
            return pat;

        }
        */

        private static void AffectRepertoireToPatient(basePatient pat)
        {
            string rep = DAC_BASeView.getOldRepertoireName(pat.Id);
            pat.Repertoire = rep;
        }


        public static void getinfocomplementaire(basePatient pat)
        {
            DataRow dr = DAC.getinfocomplementaire(pat);
            if (dr != null)
                pat.infoscomplementaire = Builders.BuildInfoPatientComplementaire(dr);
            else
                pat.infoscomplementaire = new InfoPatientComplementaire();


        }


        public static void ChangePraticienResponsable(basePatient patient, Utilisateur praticien)
        {
            DAC.ChangePraticienResponsable(patient, praticien);
        }

        public static void ChangeAssistanteResponsable(basePatient patient, Utilisateur assistante)
        {
            DAC.ChangeAssistanteResponsable(patient, assistante);
        }

        public static void setinfocomplementaire(basePatient patient)
        {
            DAC.setinfocomplementaire(patient);

        }

        

        

        

        public static void getRisques(Patient pat)
        {
            pat.Risques = DAC.getRisques(pat);


        }

        public static void setRisques(Patient patient)
        {
            DAC.setRisques(patient);

        }


         

    }
}
