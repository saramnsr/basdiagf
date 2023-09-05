using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BasCommon_BO;
using BasCommon_DAL;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
namespace BasCommon_BL
{
    public static class Fauteuilsmgt
    {

        private static string _RegistryKey = "Software\\BASE\\BASEPractice";
        private static string _RegistryKeyPref = _RegistryKey + "\\Preferences";
        private static string _FautRegistryKey = _RegistryKeyPref + "\\Fauteuils";

        public static bool IsPrefVisible(Fauteuil faut)
        {

            RegistryKey key = Registry.CurrentUser.OpenSubKey(_FautRegistryKey);

            // If the return value is null, the key doesn't exist
            if (key == null) return true;

            object obj = key.GetValue(faut.Id.ToString());
            if (obj == null) return true;
            else return Convert.ToBoolean(obj);
        }

        public static void SetPrefVisible(Fauteuil faut, bool value)
        {


            RegistryKey key = Registry.CurrentUser.OpenSubKey(_FautRegistryKey, true);

            // If the return value is null, the key doesn't exist
            if (key == null)
            {
                // The key doesn't exist; create it / open it
                key = Registry.CurrentUser.CreateSubKey(_FautRegistryKey);
            }


            key.SetValue(faut.Id.ToString(), value);


        }

        private static List<Fauteuil> _fauteuils;
        public static List<Fauteuil> fauteuils
        {
            get
            {
                if (_fauteuils == null)
                    _fauteuils = getfauteuils();
                return _fauteuils;
            }

        }


        public static Fauteuil FauteuilRadio
        {
            get
            {
                foreach (Fauteuil f in fauteuils)
                    if (f.Id == 11)
                        return f;

                return null;
            }

        }



        public static Fauteuil GetFauteuil(int Id)
        {
            foreach (Fauteuil f in fauteuils)
                if (f.Id == Id) return f;

            return null;
        }

        public static Fauteuil GetFauteuil(string name)
        {
            foreach (Fauteuil f in fauteuils)
                if (f.libelle == name) return f;

            return null;
        }

        public static Fauteuil GetWhoIam()
        {

            RegistryKey key = Registry.CurrentUser.OpenSubKey(_FautRegistryKey);

            // If the return value is null, the key doesn't exist
            if (key == null) return null;

            object obj = key.GetValue("CurrentFauteuil");
            if (obj == null)
                return null;
            else
                return getfauteuil((int)obj);
        }

        public static void SetWhoIam(Fauteuil faut)
        {


            RegistryKey key = Registry.CurrentUser.OpenSubKey(_FautRegistryKey, true);

            // If the return value is null, the key doesn't exist
            if (key == null)
            {
                // The key doesn't exist; create it / open it
                key = Registry.CurrentUser.CreateSubKey(_FautRegistryKey);
            }


            key.SetValue("CurrentFauteuil", faut.Id);


        }


        private static List<Fauteuil> getfauteuils()
        {

            JArray json = DAC.getMethodeJsonArray("/Fauteuils");
            List<Fauteuil> lst = new List<Fauteuil>();
            foreach (JObject r in json)
            {
                lst.Add(Builders.BuildFauteuil.BuildJ(r));
            }
            return lst;
        }
        private static List<Fauteuil> getfauteuilsOLD()
        {
            DataTable dt = DAC.getFauteuils();

            List<Fauteuil> lst = new List<Fauteuil>();
            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildFauteuil.Build(r));
            }
            return lst;
        }

        public static Fauteuil getfauteuil(int Id)
        {
            foreach (Fauteuil f in fauteuils)
                if (f.Id == Id) return f;

            return null;

        }
       
        public static List<Fauteuil> GetFauteuils(Utilisateur Ut)
        {
            List<Fauteuil> lst = new List<Fauteuil>();
            JArray json = DAC.getMethodeJsonArray("/fauteuilsDefaut/" + Ut.Id);
            
            foreach (int r in json)
            {
                foreach (Fauteuil f in Fauteuilsmgt.fauteuils)
                    if (Convert.ToInt32(r) == f.Id)
                        lst.Add(f);
            }

            return lst;
        }
        public static List<Fauteuil> GetFauteuilsOLD(Utilisateur Ut)
        {
            List<Fauteuil> lst = new List<Fauteuil>();
            DataTable dt = DAC.getFauteuils(Ut);
            foreach (DataRow r in dt.Rows)
            {
                foreach (Fauteuil f in Fauteuilsmgt.fauteuils)
                    if (Convert.ToInt32(r["id_fauteuil"]) == f.Id)
                        lst.Add(f);
            }

            return lst;
        }

    }
}
