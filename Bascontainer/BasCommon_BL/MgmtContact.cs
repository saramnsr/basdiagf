using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using BasCommon_BO;
using BasCommon_DAL;
using Newtonsoft.Json.Linq;

namespace BasCommon_BL
{
    public static class MgmtContact
    {


        public static void SaveContactTo(Contact contact)
        {
            if (contact.Id == -1)
                InsertContactTo(contact);
            else
                UpdateContact(contact);
        }

        public static void SaveContactTo(int IdPersonne, Contact contact)
        {

            contact.IdPersonne = IdPersonne;

            if (contact.Id == -1)
                InsertContactTo(contact);
            else
                UpdateContact(contact);
        }

        public static void UpdateContact(Contact contact)
        {
            DAC.UpdateContactTo(contact);
        }

        public static void DeleteContact(Contact contact)
        {
            DAC.deleteContactFromId(contact.Id);
        }

        public static void DeleteContactOf(int IdPersonne)
        {
            DAC.deleteContactOf(IdPersonne);
        }

        public static void DeleteContactFromId(int IdContact)
        {
            DAC.deleteContactFromId(IdContact);
        }


        

        public static void InsertContacts(List<Contact> lst)
        {

            foreach (Contact c in lst)
                DAC.InsertContactTo(c);
        }

        public static List<Contact> getContactsOf(int idPersonne)
        {
            JArray json = DAC.getMethodeJsonArray("/get_contact_of/" + idPersonne);
            List<Contact> lst = new List<Contact>();
            foreach (JObject r in json)
            {
                Contact c = Builders.BuildContact.BuildJson(r);
                lst.Add(c);
            }
            return lst;
        }

        public static List<Contact> getContactsOfOld(int idPersonne)
        {
            List<Contact> lst = new List<Contact>();
            DataTable dt = DAC.getContactsOf(idPersonne);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildContact.Build(r));
            }
            return lst;
        }

        public static List<Contact> getMails(string mail)
        {
            List<Contact> lst = new List<Contact>();
            DataTable dt = DAC.getMails(mail);

            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildContact.Build(r));
            }
            return lst;
        }

        

        public static void InsertContactTo(Contact c)
        {
            DAC.InsertContactTo(c);
        }

       
    }
}
