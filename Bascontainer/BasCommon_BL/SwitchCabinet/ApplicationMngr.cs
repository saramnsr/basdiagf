using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Security;
using System.Security.Cryptography;
using BasCommon_BL;
namespace SwitchCabinet
{
    public static class ApplicationMngr
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

        private static string _RegistryKey1 = "Software\\BASE\\BASEPractice";

        private static string _RegistryKeyPref = _RegistryKey1 + "\\Preferences";
        private static string _CurrentCabRegistryKey = _RegistryKeyPref + "\\CurrentCab";

        public static void GetCurrentCabOnRegistry()
        {

            RegistryKey key = Registry.CurrentUser.OpenSubKey(_CurrentCabRegistryKey);

            // If the return value is null, the key doesn't exist
            if (key == null) return;

            string objValidityDate = (string)key.GetValue("ValidityDate");
            string objValidityUser = (string)key.GetValue("ValidityCab");

            key.Close();

            DateTime ValidityDate;

            if (DateTime.TryParse(objValidityDate, out ValidityDate))
            {
                int idCabinet = Convert.ToInt32(objValidityUser);
                prefix = CabinetMgmt.Cabinet.Find(c => c.Id == idCabinet).prefix;
            }
        }
        private static string _prefix;
        public static string prefix
        {
            get
            {
                if (_prefix == null || _prefix == "")
                    GetCurrentCabOnRegistry();
                return _prefix;
            }
            set
            {
                _prefix = "_" + value;
            }


        }
        public static string GetExePathFromRegistry(string className)
        {

            RegistryKey k = Registry.ClassesRoot.OpenSubKey(className + "\\CLSID");
            if (k == null) return className + "\\CLSID ->not found";
            object obj = k.GetValue("");
            if (obj == null) return "-2";

            string clsid = (string)obj;

            k = Registry.ClassesRoot.OpenSubKey("CLSID\\" + clsid + "\\LocalServer32");

            // If the return value is null, the key doesn't exist
            if (k == null) return "CLSID\\" + clsid + "\\LocalServer32 ->not found";

            obj = k.GetValue("");
            if (obj == null) return "-4";
            else
            {

                System.IO.FileInfo nfo = new FileInfo((string)obj);
                return nfo.FullName;
            }
        }

        public static string GetExeProcessFromRegistry(string className)
        {

            RegistryKey k = Registry.ClassesRoot.OpenSubKey(className + "\\CLSID");
            if (k == null) return className + "\\CLSID ->not found";
            object obj = k.GetValue("");
            if (obj == null) return "";

            string clsid = (string)obj;

            k = Registry.ClassesRoot.OpenSubKey("CLSID\\" + clsid + "\\LocalServer32");

            // If the return value is null, the key doesn't exist
            if (k == null) return "CLSID\\" + clsid + "\\LocalServer32 ->not found";

            obj = k.GetValue("");
            if (obj == null) return "";
            else
            {
                System.IO.FileInfo nfo = new FileInfo((string)obj);

                return System.IO.Path.GetFileNameWithoutExtension(nfo.FullName);
            }
        }


        public static void ActivateWindow(IntPtr hWnd)
        {
            ShowWindow(hWnd, SW_SHOWNORMAL);
            System.Threading.Thread.Sleep(250);
            SetForegroundWindow(hWnd);
        }

        private static bool IsApplicationExecutingFromName(string classname)
        {

            string applicationName = GetExePathFromRegistry(classname);
            string processName = GetExeProcessFromRegistry(classname);

            //Recherche du processus par nom
            Process[] processes = Process.GetProcessesByName(processName);
            //Il y a au moins un processus démarré
            if (processes.Length > 0)
            {
                return true;
            }
            else
            {
                return false;

            }

        }


        private static Process LoadApplicationFromName(string classname)
        {

            string applicationName = GetExePathFromRegistry(classname);
            applicationName = classname;
            string processName = GetExeProcessFromRegistry(classname);
            processName = Path.GetFileNameWithoutExtension(applicationName);
            Console.Out.WriteLine("Launching..." + applicationName);
            Console.Out.WriteLine("Process name : " + processName);
            Process process = null;

            //Recherche du processus par nom
            Process[] processes = Process.GetProcessesByName(processName);
            //Il y a au moins un processus démarré
            if (processes.Length > 0)
            {
                //On récupère l'objet Process en fonction du tableau
                //le premier élément du tableau suffit
                process = processes[0];

                //Le processus a quitté?
                if (process.HasExited)
                {
                    //On l'éxecute
                    process = Execute(classname);

                }
                else
                {
                    //Sinon on active sa fenetre au premier plan
                    ActivateWindow(process.MainWindowHandle);
                }
            }
            else
            {
                //Démarrage normal
                process = Execute(classname);

            }

            return process;
        }

        //old
        /* private static Process Execute(string classname)
         {

             try
             {
                 string applicationName = GetExePathFromRegistry(classname);
                 string ProcessName = GetExeProcessFromRegistry(classname);

                 if (applicationName == "") return null;
                 //Process process = null;

                 return Process.Start(applicationName);
             }catch(System.Exception)
             { }
             return null;
         }*/



        private static Process Execute(string classname)
        {

            try
            {
                string applicationName = GetExePathFromRegistry(classname);
                string ProcessName = GetExeProcessFromRegistry(classname);
                applicationName = classname;
                ProcessName = Path.GetFileNameWithoutExtension(applicationName);
                if (applicationName == "") return null;
                //Process process = null;
                Process pp = new Process();
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(applicationName, "");
                info.Verb = "runas";
                info.UseShellExecute = false;

                return Process.Start(info);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n'" + classname + "'");
            }
            return null;
        }
        private static bool Open(string adress)
        {

            try
            {
                Process myInfo = new Process();
                myInfo.StartInfo.FileName = adress;
                myInfo.Start();
                return true;
            }
            catch (System.Exception ex)
            {

                return false;

            }


        }


        public static void OpenBase1er()
        {
            reloadApp(ConfigurationSettings.AppSettings["BASE1ER"]);


        }
        public static void LaunchBase1er()
        {
            LoadApplicationFromName(ConfigurationSettings.AppSettings["BASE1ER"]);


        }
        public static void LaunchCefSharp()
        {
            LoadApplicationFromName(ConfigurationSettings.AppSettings["CEFSharp"]);


        }
        public static void OpenBaseDiag()
        {
            reloadApp(ConfigurationSettings.AppSettings["BASEDiag"]);


        }

        public static void OpenBaseDiagAdulte()
        {
            reloadApp(ConfigurationSettings.AppSettings["BASEDiagAdulte"]);

        }
        public static void OpenMiniBaseDiag()
        {
            reloadApp(ConfigurationSettings.AppSettings["MiniBASEDiag"]);


        }

        public static void OpenMiniBaseDiagAdulte()
        {
            reloadApp(ConfigurationSettings.AppSettings["MiniBASEDiagAdulte"]);

        }
        public static void OpenBaseContact()
        {

            reloadApp(ConfigurationSettings.AppSettings["BASEContact"]);


        }

        public static void OpenBaseLetter()
        {
            reloadApp(ConfigurationSettings.AppSettings["BASELetter"]);

        }

        public static void OpenBaseLabo()
        {

            reloadApp(ConfigurationSettings.AppSettings["BASELabo"]);

        }


        public static void OpenRHBase()
        {
            reloadApp(ConfigurationSettings.AppSettings["RHBASE"]);

        }
        public static void OpenBaseViewMateriel()
        {
            reloadApp(ConfigurationSettings.AppSettings["BASEVIEWMateriel"]);
        }
        public static void OpenBaseView()
        {
            reloadApp(ConfigurationSettings.AppSettings["BASEVIEW"]);
        }

       

        public static void openBasStat()
        {

            reloadApp(ConfigurationSettings.AppSettings["BaseStat"]);

        }
        public static void openweb()
        {
            if (prefix == "_bergues")
            {
                reloadApp(ConfigurationManager.AppSettings["Webbergues"]);

            }
            else
            {

                reloadApp(ConfigurationManager.AppSettings["Web"]);
            }
            //reloadApp(ConfigurationSettings.AppSettings["Web"]);

        }

        public static void reloadApp(string app)
        {
            System.Diagnostics.Process[] prcs = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(app));

            if (prcs.Length > 0)
            {
                foreach (System.Diagnostics.Process p in prcs)
                {
                    p.Kill();
                }
                LoadApplicationFromName(app);
            }
        }


        public static void openBasePractice()
        {

            reloadApp(ConfigurationSettings.AppSettings["BasePractice"]);

        }

        
    }
}
