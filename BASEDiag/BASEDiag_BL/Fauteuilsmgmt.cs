﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BASEDiag_BO;
using BASEDiag_DAL;
using Microsoft.Win32;

namespace BASEDiag_BL
{
    public static class Fauteuilsmgt
    {

        private static string _RegistryKey = "Software\\BASE\\BASEPractice";
        private static string _RegistryKeyPref = _RegistryKey + "\\Preferences";
        private static string _FautRegistryKey = _RegistryKeyPref + "\\Fauteuils";

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

        private static List<Fauteuil> getfauteuils()
        {
            DataTable dt = DAC.getFauteuils();

            List<Fauteuil> lst = new List<Fauteuil>();
            foreach (DataRow r in dt.Rows)
            {
                lst.Add(Builders.BuildFauteuil(r));
            }
            return lst;
;
        }

        public static Fauteuil getfauteuil(int Id)
        {
            foreach (Fauteuil f in fauteuils)
                if (f.Id==Id) return f;

            return null;

        }

        
    }
}
