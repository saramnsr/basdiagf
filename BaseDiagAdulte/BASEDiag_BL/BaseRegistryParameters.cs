using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Win32;


namespace BASEDiag_BL
{
    public static class RegistryParameters
    {
        private static string _RegistryKey = "Software\\BASE\\BASEDiag";
        private static string _RegistryKeyPref = _RegistryKey + "\\Preferences";

        public static object GetParameter(string param)
        {

            RegistryKey key = Registry.CurrentUser.OpenSubKey(_RegistryKeyPref);

            // If the return value is null, the key doesn't exist
            if (key == null) return null;

            object obj = key.GetValue(param);
            return obj;
        }

        public static void SetParameter(string param, object value)
        {


            RegistryKey key = Registry.CurrentUser.OpenSubKey(_RegistryKeyPref, true);

            // If the return value is null, the key doesn't exist
            if (key == null)
            {
                // The key doesn't exist; create it / open it
                key = Registry.CurrentUser.CreateSubKey(_RegistryKeyPref);
            }


            key.SetValue(param, value);


        }


        public static int GetScreenNumberOf(Type tpe)
        {
            object obj = GetParameter(tpe.Name+"_ScreenNumber");
            if (obj == null) return 0;
            else
                return (int)obj;
        }

        public static void SetScreenNumberOf(Type tpe, int value)
        {
            SetParameter(tpe.Name + "_ScreenNumber", value);
            
        }

    }
}
