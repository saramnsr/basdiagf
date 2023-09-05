using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using BASEDiag_BO;
using BASEDiag_DAL;
using Microsoft.Win32;


namespace BASEDiag_BL
{
    public static class PrinterSettingsMgmt
    {

        private static string _RegistryKey = "Software\\BASE\\BASELetter";
        private static string _RegistryKeyPref = _RegistryKey + "\\Preferences";
        private static string _ImpRegistryKey = _RegistryKeyPref + "\\Impressions";
        private static string _ImpDescRegistryKey = _ImpRegistryKey + "\\Description";
        private static string _ImpValuesRegistryKey = _ImpRegistryKey + "\\Valeurs";



        private static List<BasePrinterSetting> _printsettings;
        public static List<BasePrinterSetting> printsettings
        {
            get
            {
                if (_printsettings == null) _printsettings = getPrintSettings();
                return _printsettings;
            }
            set
            {
                _printsettings = value;
            }
        }

        
        public static List<string> getPrintSettingNames()
        {
            List<string> lst = new List<string>();

            foreach (BasePrinterSetting bps in _printsettings)
                lst.Add(bps.Libelle);

            return lst;

        }

        public static BasePrinterSetting getPrintSettingsByName(string name)
        {
            foreach (BasePrinterSetting bpset in printsettings)
            {
                if (name == bpset.Libelle)
                    return bpset;
            }
            return null;
        }


        public const string ImpressionConsentement = "ImpressionConsentement";
        public  const string ImpressionDevis = "ImpressionDevis";
        public  const string ImpressionPatient = "ImpressionPatient";
        public  const string ImpressionCompteRendu = "ImpressionCompteRendu";
        public  const string ImpressionAnalyse = "ImpressionAnalyse";
        public  const string ImpressionSpecialistes = "ImpressionSpecialistes";
        public  const string ImpressionDEP = "ImpressionDEP";

        private static List<BasePrinterSetting> getPrintSettings()
        {
            List<BasePrinterSetting> lst = new List<BasePrinterSetting>();

            BasePrinterSetting bps = new BasePrinterSetting();
            bps.Libelle = ImpressionDevis;
            lst.Add(bps);
            bps = new BasePrinterSetting();
            bps.Libelle = ImpressionConsentement;
            lst.Add(bps);
            bps = new BasePrinterSetting();
            bps.Libelle = ImpressionPatient;
            lst.Add(bps);
            bps = new BasePrinterSetting();
            bps.Libelle = ImpressionCompteRendu;
            lst.Add(bps);
            bps = new BasePrinterSetting();
            bps.Libelle = ImpressionAnalyse;
            lst.Add(bps);
            bps = new BasePrinterSetting();
            bps.Libelle = ImpressionSpecialistes;
            lst.Add(bps);
            bps = new BasePrinterSetting();
            bps.Libelle = ImpressionDEP;
            lst.Add(bps);


            foreach (BasePrinterSetting bpset in lst)
            {
                bpset.Descriptif = ReadDescription(bpset.Libelle);
                bpset.settings = ReadSetting(bpset.Libelle);
            }
            
            return lst;
        }

        private static string ReadDescription(string Key)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(_ImpDescRegistryKey);

            // If the return value is null, the key doesn't exist
            if (key == null) return "";

            object obj = key.GetValue(Key);
            if (obj == null) return "";
            else return Convert.ToString(obj);
        }

        private static System.Drawing.Printing.PrinterSettings ReadSetting(string Key)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(_ImpValuesRegistryKey);

            // If the return value is null, the key doesn't exist
            if (key == null) return null;

            object obj = key.GetValue(Key);
            if (obj == null) return null;


            System.Drawing.Printing.PrinterSettings objectToSerialize;
            Stream stream = new MemoryStream((byte[])obj);
            BinaryFormatter bFormatter = new BinaryFormatter();
            objectToSerialize = (System.Drawing.Printing.PrinterSettings)bFormatter.Deserialize(stream);
            stream.Close();
            return objectToSerialize;

        }
        
        private static void SetDescription(string Key, BasePrinterSetting ps)
        {


            RegistryKey key = Registry.CurrentUser.OpenSubKey(_ImpDescRegistryKey, true);

            // If the return value is null, the key doesn't exist
            if (key == null)
            {
                // The key doesn't exist; create it / open it
                key = Registry.CurrentUser.CreateSubKey(_ImpDescRegistryKey);
            }


            key.SetValue(Key, ps.Descriptif);


        }

        private static void SetSetting(string Key, BasePrinterSetting ps)
        {


            RegistryKey key = Registry.CurrentUser.OpenSubKey(_ImpValuesRegistryKey, true);

            // If the return value is null, the key doesn't exist
            if (key == null)
            {
                // The key doesn't exist; create it / open it
                key = Registry.CurrentUser.CreateSubKey(_ImpValuesRegistryKey);
            }


            if (ps.settings != null)
            {
                MemoryStream stream = new MemoryStream();
                BinaryFormatter bFormatter = new BinaryFormatter();
                bFormatter.Serialize(stream, ps.settings);
                stream.Close();

                key.SetValue(Key, stream.ToArray(), RegistryValueKind.Binary);
            }


        }
        
        public static void SavePrintSettings()
        {
            foreach (BasePrinterSetting bpset in printsettings)
            {
                SetDescription(bpset.Libelle,bpset);
                SetSetting(bpset.Libelle, bpset);
            }
        }


    }
}
