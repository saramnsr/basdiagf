﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_DAL;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL
{
    public static class MgmtCorrespondants
    {

        private static List<baseSmallPersonne> _AllCorrespondants;
        public static List<baseSmallPersonne> AllCorrespondants
        {
            get
            {
                if (_AllCorrespondants == null) _AllCorrespondants = getSmallCorrespondants("");
                return _AllCorrespondants;
            }
            set
            {
                _AllCorrespondants = value;
            }
        }

        private static List<CorrespondantType> _typecorrespondants;
        public static List<CorrespondantType> typecorrespondants
        {
            get
            {
                if (_typecorrespondants == null) _typecorrespondants = getTypeCorrespondants();
                return _typecorrespondants;
            }
            set
            {
                _typecorrespondants = value;
            }
        }

        public static CorrespondantType FindType(string profession)
        {
            foreach (CorrespondantType tpe in MgmtCorrespondants.typecorrespondants)
                if (tpe.Nom.ToUpper() == profession.ToUpper())
                    return tpe;

            return null;
        }

        public static void Delete(Correspondant correspondnat)
        {
            DAC.DeleteCorrespondant(correspondnat.Id);
        }

        public static int GetIdFromLoginPassword(string login, string mdp)
        {
            return BasCommon_DAL.DAC.getIdPersonneByLoginMDP(login, mdp);
        }


        public static List<LienCorrespondant> getCorrespondantsOf(basePatient patient)
        {

            return getCorrespondantsOf(patient.Id);
        }

        public static List<LienCorrespondant> getCorrespondantsOf(int idpatient)
        {
            JArray json = DAC.getMethodeJsonArray("/CorrespondantsOf/" + idpatient);
            List<LienCorrespondant> lst = new List<LienCorrespondant>();


            foreach (JObject r in json)
            {
                lst.Add(Builders.BuildLienCorrespondant.BuildJ(r));
            }
            return lst;
        }
        public static List<LienCorrespondant> getCorrespondantsOfOLD(int idpatient)
        {
            List<LienCorrespondant> lst = new List<LienCorrespondant>();
            DataTable dt;

            dt = DAC.getCorrespondantsOf(idpatient);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildLienCorrespondant.Build(r));
            }
            return lst;
        }

        public static void SendMailTo(Correspondant cor)
        {

            if (cor.contacts == null)
                MgmtCorrespondants.FillContacts(cor);


            if (cor.MainMail != null)
                System.Diagnostics.Process.Start("mailto:" + cor.MainMail.Value);


        }

        public static List<baseSmallPersonne> getPatientsWiththeSameCorrespondant(LienCorrespondant lc)
        {
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();
            JArray json = DAC.getMethodeJsonArray("/SmallPatientsWiththeSameCorrespondant/" + lc.IdCorrespondance + "&" + lc.TypeDeLien);

            foreach (JObject r in json)
            {
                lst.Add(Builders.BuildSmallPersonne.BuildJson(r));
            }
            return lst;
        }
        public static List<baseSmallPersonne> getPatientsWiththeSameCorrespondantOLD(LienCorrespondant lc)
        {
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();
            DataTable dt;

            dt = DAC.getSmallPatientsWiththeSameCorrespondant(lc);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildSmallPersonne.Build(r));
            }
            return lst;
        }
        public static baseSmallPersonne getSmallPersonneFromExactName(string nom, string prenom)
        {
            DataRow dr = DAC.getSmallPersonneFromExactName(nom, prenom);

            return Builders.BuildSmallPersonne.Build(dr);

        }

        public static List<baseSmallPersonne> getSmallPersonne(string nom, string prenom)
        {
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();
            DataTable dt;

            dt = DAC.getSmallPersonneFromName(nom, prenom);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildSmallPersonne.Build(r));
            }
            return lst;
        }



        public static List<baseSmallPersonne> getDentistes()
        {
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();
            JArray array = DAC.getMethodeJsonArray("/getdentiste");

            foreach (JObject r in array)
            {
                lst.Add(Builders.BuildSmallPersonne.BuildJson(r));
            }
            return lst;
        }
        public static bool userMonDentisteExist(int idPersonne)
        {

            JObject obj = DAC.getMethodeJsonObjet("/userById/" + idPersonne);
            return obj != null;

        }
        public static User userMonDentisteExistOLD(int idPersonne)
        {

            DataRow r;

            r = DAC.getUserMonDentisteById(idPersonne);
            if (r == null) return null;
            User user = new User();
            user.id = r["id"] is DBNull ? -1 : Convert.ToInt32(r["id"]);
            user.mail = r["email"] is DBNull ? "" : Convert.ToString(r["email"]).Trim();
            return user;

        }
        public static bool getUserByEmail(string email)
        {

            DataRow r;

            r = DAC.getUserByEmail(email);
            if (r == null) return false;
            return true;

        }

        public static List<baseSmallPersonne> getDentistesold()
        {
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();
            DataTable dt;

            dt = DAC.getDentistes();

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildSmallPersonne.Build(r));
            }
            return lst;
        }


        public static void SupprimerLienCorrespondant(string tpelien, int Idpatient)
        {
            DAC.RemoveLienCorrespondant(tpelien, Idpatient);
        }

        public static void SupprimerLienCorrespondant(LienCorrespondant newlnk)
        {
            SupprimerLienCorrespondant(newlnk.TypeDeLien, newlnk.IdPatient);
        }

        public static void RemplacerLienCorrespondant(LienCorrespondant newlnk)
        {
            SupprimerLienCorrespondant(newlnk);
            InsertLienCorrespondant(newlnk);
        }


        public static void RemoveLienCorrespondant(string typelien, int idpatient)
        {
            DAC.RemoveLienCorrespondant(typelien, idpatient);
        }


        public static void RemoveLienCorrespondant(LienCorrespondant lnkcorrespondnat)
        {
            DAC.RemoveLienCorrespondant(lnkcorrespondnat.correspondant.Id, lnkcorrespondnat.IdPatient);
        }

        public static void InsertLienCorrespondant(LienCorrespondant lnkcorrespondnat)
        {
            DAC.InsertLienCorrespondant(lnkcorrespondnat);
        }

        public static void InsertUpdateLienCorrespondant(LienCorrespondant lnkcorrespondnat)
        {
            DAC.InsertUpdateLienCorrespondant(lnkcorrespondnat);
        }


        public static void FillContacts(Correspondant co)
        {
            co.contacts = MgmtContact.getContactsOf(co.Id);

            foreach (Contact c in co.contacts)
            {
                if ((c.TypeContact == Contact.ContactType.Adresse) && (co.MainAdresse == null))
                {
                    co.MainAdresse = c;
                }

                if ((c.TypeContact == Contact.ContactType.Mail) && (co.MainMail == null))
                {
                    co.MainMail = c;
                }

                if ((c.TypeContact == Contact.ContactType.Telephone) && (co.MainTel == null))
                {
                    co.MainTel = c;
                }

                if ((c.TypeContact == Contact.ContactType.Fax) && (co.MainFax == null))
                {
                    co.MainFax = c;
                }
            }
        }




        public static List<baseSmallPersonne> getAllPatientsOf(Correspondant corres)
        {
            DataTable dt = DAC.getAllPatientsOf(corres);

            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();

            foreach (DataRow dr in dt.Rows)
            {
                baseSmallPersonne p = Builders.BuildSmallPersonne.Build(dr);
                lst.Add(p);
            }

            return lst;
        }

        public static List<LienCorrespondant> getPatientOf(Correspondant corres, DateTime dteValue)
        {
            DataTable dt = DAC.getPatientsOf(corres, dteValue);

            List<LienCorrespondant> lst = new List<LienCorrespondant>();

            foreach (DataRow dr in dt.Rows)
            {
                LienCorrespondant p = Builders.BuildLienCorrespondant.Build(dr);
                lst.Add(p);
            }

            return lst;
        }



        public static List<baseSmallPersonne> getCorrespondantsSuggested(string profession, string param)
        {

            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();
            DataTable dt;

            dt = DAC.getCorrespondantsSugested(profession, param);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildSmallPersonne.Build(r));
            }
            return lst;

        }



        public static List<object> getCorrespondantsSuggested(string param)
        {

            List<object> lst = new List<object>();
            DataTable dt;

            dt = DAC.getCorrespondantsSugested(param);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildSmallPersonne.Build(r));
            }
            return lst;

        }



        public static baseSmallPersonne getSmallCorrespondant(int Id)
        {
            DataRow dr;

            dr = DAC.getSmallCorrespondant(Id);
            if (dr == null) return null;
            return Builders.BuildSmallPersonne.Build(dr);
        }
        public static Correspondant getCorrespondant(int Id)
        {

            JObject json = DAC.getMethodeJsonObjet("/personne/getCorrespondant/" + Id);

            //          if (json.ToString() == "") return null;
            return Builders.BuildCorrespondant.BuildJ(json);
        }
        public static Correspondant getCorrespondantOLD(int Id)
        {

            DataRow dr = DAC.getCorrespondant(Id);

            if (dr == null) return null;
            return Builders.BuildCorrespondant.Build(dr);
        }

        public static void UpdateProfilInternet(Correspondant corres)
        {
            DAC.UpdateProfilInternet(corres);
        }

        public static List<baseSmallPersonne> getCorrespondants(string param)
        {
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();
            DataTable dt;

            dt = DAC.getCorrespondants(param);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildSmallPersonne.Build(r));
            }
            return lst;
        }
        public static List<baseSmallPersonne> getCorrespondantsbyName(string Nom)
        {
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();
            JArray array = DAC.getMethodeJsonArray("/getcorrespondantsByName/" + Nom);

            foreach (JObject r in array)
            {
                lst.Add(Builders.BuildSmallPersonne.BuildJson(r));
            }
            return lst;
        }

        public static List<baseSmallPersonne> getCorrespondantsbyNameold(string Nom)
        {
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();
            DataTable dt;

            dt = DAC.getCorrespondantsbyName(Nom);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildSmallPersonne.Build(r));
            }
            return lst;
        }


        public static void ReaffecterNSupprimerCorrespondant(int IdcorresASuppr, int IdcorresAReaffecter)
        {

            MgmtLienCorrespondant.ReaffecterCorrespondant(IdcorresASuppr, IdcorresAReaffecter);


            DAC.DeleteCorrespondant(IdcorresASuppr);
        }
        public static List<baseSmallPersonne> getSmallCorrespondants(string param, TypePersonne typefiltre)
        {


            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();
            int idFiltre = typefiltre == null ? 0 : typefiltre.Id;
            JArray array = DAC.getMethodeJsonArray("/getsmall/" + idFiltre + "&" + param.ToString());

            foreach (JObject r in array)
            {
                lst.Add(Builders.BuildSmallPersonne.BuildJson(r));
            }
            return lst;
        }

        public static List<baseSmallPersonne> getSmallCorrespondantsold(string param, TypePersonne typefiltre)
        {
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();
            DataTable dt;

            dt = DAC.getSmallCorrespondants(param, typefiltre);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildSmallPersonne.Build(r));
            }
            return lst;
        }


        public static List<baseSmallPersonne> getSmallCorrespondants(string param)
        {
            List<baseSmallPersonne> lst = new List<baseSmallPersonne>();
            DataTable dt;

            dt = DAC.getSmallCorrespondants(param);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildSmallPersonne.Build(r));
            }
            return lst;
        }



        public static void SaveCorrespondant(Correspondant correspondant)
        {
            if (correspondant.Id == -1)
            {
                InsertCorrespondant(correspondant);
            }
            else
            {
                UpdateCorrespondant(correspondant);
            }
        }
        public static void registerMonDentiste(User user)
        {
            Object obj = (Object)user;
            DAC.insertRest("/registerMedecin", obj);
        }
        public static void updateMonDentiste(User user)
        {
            Object obj = (Object)user;
            DAC.insertRest("/updateUserMedecin", obj);
        }
        public static void InsertCorrespondant(Correspondant correspondant)
        {

            DAC.InsertCorrespondant(correspondant);
            if (correspondant.Note != 0)
                NoteMgmt.AffectNote(correspondant.Id, correspondant.Note);
            correspondant.LastAffectedNote = correspondant.Note;

        }

        public static bool CheckNomPrenom(string nom, string prenom)
        {
            return DAC.CheckNomPrenom(nom, prenom);
        }

        private static void UpdateCorrespondant(Correspondant correspondant)
        {
            if (correspondant.LastAffectedNote != correspondant.Note)
                NoteMgmt.AffectNote(correspondant.Id, correspondant.Note);
            DAC.UpdateCorrespondant(correspondant);
            correspondant.LastAffectedNote = correspondant.Note;
        }






        private static List<CorrespondantType> getTypeCorrespondants()
        {
            List<CorrespondantType> lst = new List<CorrespondantType>();
            JArray array = DAC.getMethodeJsonArray("/GetTypeCorrespondants");

            foreach (JObject r in array)
            {
                lst.Add(Builders.BuildCorrespondantType.BuildJson(r));
            }
            return lst;
        }


        private static List<CorrespondantType> getTypeCorrespondantsOlD()
        {
            List<CorrespondantType> lst = new List<CorrespondantType>();
            DataTable dt;

            dt = DAC.getTypeCorrespondants();

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildCorrespondantType.Build(r));
            }
            return lst;
        }
    }
}
