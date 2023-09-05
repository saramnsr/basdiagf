using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32;
using System.IO;

namespace BasCommon_BL
{
    public static class ApplicationMgmt
    {


        private const int SW_SHOWNORMAL = 1;

        private static string _RegistryKey = "CLSID\\{0}\\LocalServer32";


        [DllImport("user32.dll")] // Ou SetWindowPos
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        public static void ActivateWindow(IntPtr hWnd)
        {
            ShowWindow(hWnd, SW_SHOWNORMAL);
            Thread.Sleep(250);
            SetForegroundWindow(hWnd);
        }

        public static int GetScreenOfProcess(Process pr)
        {


            System.Windows.Forms.Screen scr = System.Windows.Forms.Screen.FromHandle(pr.MainWindowHandle);

            int i = 0;
            foreach (System.Windows.Forms.Screen sc in System.Windows.Forms.Screen.AllScreens)
            {
                if (sc.WorkingArea.Equals(scr.WorkingArea))
                    return i;
                i++;

            }



            return -1;
        }

        public static string GetExePathFromRegistry(string className)
        {

            RegistryKey k = Registry.ClassesRoot.OpenSubKey(className + "\\CLSID");
            if (k == null) return "";
            object obj = k.GetValue("");
            if (obj == null) return "";

            string clsid = (string)obj;

            k = Registry.ClassesRoot.OpenSubKey("CLSID\\" + clsid + "\\LocalServer32");

            // If the return value is null, the key doesn't exist
            if (k == null) return "";

            obj = k.GetValue("");
            if (obj == null) return "";
            else
            {
                FileInfo nfo = new FileInfo((string)obj);
                return nfo.FullName;
            }
        }

        public static string GetExeProcessFromRegistry(string className)
        {

            RegistryKey k = Registry.ClassesRoot.OpenSubKey(className + "\\CLSID");
            if (k == null) return "";
            object obj = k.GetValue("");
            if (obj == null) return "";

            string clsid = (string)obj;

            k = Registry.ClassesRoot.OpenSubKey("CLSID\\" + clsid + "\\LocalServer32");

            // If the return value is null, the key doesn't exist
            if (k == null) return "";

            obj = k.GetValue("");
            if (obj == null) return "";
            else
            {

                FileInfo nfo = new FileInfo((string)obj);

                return Path.GetFileNameWithoutExtension(nfo.FullName);
            }
        }


        private static bool Open(string address)
        {

            try
            {
#if TRACE

                Debug.Write(" try to open " + address);
#endif
                Process myInfo = new Process();
                myInfo.StartInfo.FileName = address;
                myInfo.Start();
                return true;
            }
            catch (System.Exception ex)
            {
#if TRACE
                Debug.Write(" Fail to open " + address + "ex : " + ex.Message);
#endif
                return false;

            }
        }

        public static void OpenOrthalis()
        {
            Open(ConfigurationManager.AppSettings["ORTHALIS"]);

        }

        public static void OpenOrqualCeph()
        {
            Open(ConfigurationManager.AppSettings["ORQUALCEPH"]);

        }

        public static void OpenQ1CS()
        {
            Open(ConfigurationManager.AppSettings["Q1CS"]);

        }
      

        public static void OpenBase1er()
        {
            Open(ConfigurationManager.AppSettings["BASE_1ER"]);

        }



        public static void OpenBaseDiag()
        {
            Open(ConfigurationManager.AppSettings["BASEDiag"]);

        }
        public static void OpenMinBaseDiag()
        {
            Open(ConfigurationManager.AppSettings["MiniBASEDiag"]);

        }

        public static void OpenBaseLetter()
        {
            Open(ConfigurationManager.AppSettings["BaseLetter"]);

        }

        public static void OpenBaseLabo()
        {
            Open(ConfigurationManager.AppSettings["BaseLabo"]);

        }

        public static void OpenUrgenceProcedure()
        {
            Open(ConfigurationManager.AppSettings["URGENCEPROCEDURE"]);

        }
        public static void OpenUrgenceNum()
        {
            Open(ConfigurationManager.AppSettings["URGENCENUM"]);

        }
        public static void OpenUrgenceAccident()
        {
            Open(ConfigurationManager.AppSettings["URGENCEACCIDENT"]);

        }

        public static void OpenDiapoProcedure()
        {
            Open(ConfigurationManager.AppSettings["DIAPOPROCEDURE"]);

        }

        public static void OpenDiapoInvisalign()
        {
            Open(ConfigurationManager.AppSettings["DIAPOINVISALIGN"]);

        }

        public static void OpenDiapoEsthetique()
        {
            Open(ConfigurationManager.AppSettings["DIAPOESTHETIQUE"]);

        }
        public static void OpenDiapoClinique()
        {
            Open(ConfigurationManager.AppSettings["DIAPOCASCLINIQUE"]);

        }

        public static void OpenSuiviInfo()
        {
            Open(ConfigurationManager.AppSettings["SUIVIINFO"]);

        }

        public static void OpenSuiviHorn()
        {
            Open(ConfigurationManager.AppSettings["SUIVIHORN"]);

        }
        public static void OpenSuiviYung()
        {
            Open(ConfigurationManager.AppSettings["SUIVIYUNG"]);

        }

        public static void OpenpbLogiciel()
        {
            Open(ConfigurationManager.AppSettings["PBLOGICIEL"]);

        }

        public static void OpenDossierCasSimilaire()
        {
            Open(ConfigurationManager.AppSettings["DOSSIERCASSIM"]);

        }
        public static void OpenDossierCabinet()
        {
            Open(ConfigurationManager.AppSettings["DOSSIERCABINET"]);

        }

        public static void OpenLabsBasLabo()
        {
            Open(ConfigurationManager.AppSettings["LABSBASLABO"]);

        }
        public static void OpenLabsInviLocal()
        {
            Open(ConfigurationManager.AppSettings["LABSINVLOCAL"]);

        }
        public static void OpenLabsInviInternet()
        {
            Open(ConfigurationManager.AppSettings["LABSINVINTERNET"]);

        }
        public static void OpenLabsCahierProthese()
        {
            Open(ConfigurationManager.AppSettings["LABSCAHIERPROTHESE"]);

        }
        public static void OpenLabsCahierOccl()
        {
            Open(ConfigurationManager.AppSettings["LABSCAHIEROCCL"]);

        }
        public static void OpenLabsCahierInvi()
        {
            Open(ConfigurationManager.AppSettings["LABSCAHIERINVI"]);

        }

        public static void OpenBasCommandeBasSite()
        {
            Open(ConfigurationManager.AppSettings["BASCOMMANDEBASSITE"]);

        }
        public static void OpenBasCommandeClinique()
        {
            Open(ConfigurationManager.AppSettings["BASCOMMANDECLINIQUE"]);

        }
        public static void OpenBasCommandeSecretariat()
        {
            Open(ConfigurationManager.AppSettings["BASCOMMANDESECRETARIAT"]);

        }
        public static void Openweb()
        {
            if (BasCommon_BL.InfoCabinetMgmt.informationsCabinet.NomCabinet == "Bergues")
            {
                Open(ConfigurationManager.AppSettings["Webbergues"]);

            }
            else 
            {

                Open(ConfigurationManager.AppSettings["Web"]);
            }
        }
        public static void OpenSiteIsfeso()
        {
            Open(ConfigurationManager.AppSettings["SITEISFESO"]);

        }
        public static void OpenSiteLaReserve()
        {
            Open(ConfigurationManager.AppSettings["SITELARESERVE"]);

        }
        public static void OpenSiteBas()
        {
            Open(ConfigurationManager.AppSettings["SITEBAS"]);

        }
        public static void OpenSiteBasLabo()
        {
            Open(ConfigurationManager.AppSettings["SITEBASLABO"]);

        }
        public static void OpenSiteToulonSourire()
        {
            Open(ConfigurationManager.AppSettings["SITETOULONSOURIRE"]);

        }
        public static void OpenSiteFormation()
        {
            Open(ConfigurationManager.AppSettings["SITEFORMATION"]);

        }

        public static void OpenSiteUrgence()
        {
            Open(ConfigurationManager.AppSettings["SiteUrgence"]);

        }
    }
}
