using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace OLEAccess
{
    public static class RHBASE
    {
        public const string CLASSNAME = "RHBase.OLEServer";



        [DllImportAttribute("User32.dll")]
        private static extern IntPtr SetForegroundWindow(int hWnd);

        public static System.Diagnostics.Process GetProcess(Guid gui)
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"CLSID\{" + gui.ToString() + @"}\LocalServer32");

            string value = (string)key.GetValue("");

            System.Diagnostics.Process[] prcs = System.Diagnostics.Process.GetProcesses();

            System.Diagnostics.Process OLEProcess = null;

            foreach (System.Diagnostics.Process p in prcs)
            {
                try
                {
                    if (p.MainModule.FileName == value)
                        OLEProcess = p;
                }

                catch (System.Exception ex)
                {

                }
            }

            return OLEProcess;
        }

        public static void ActivateWindow(Guid gui)
        {
            System.Diagnostics.Process proc = GetProcess(gui);

            if (proc != null)
                SetForegroundWindow(proc.MainWindowHandle.ToInt32());
        }

        public static void GotoDate(DateTime dte)
        {




            Type ServerType;
            object BASELaboObject;
            try
            {

                try
                {
                    ServerType = Type.GetTypeFromProgID(CLASSNAME);
                }
                catch (System.Exception)
                {
                    throw new System.Exception("RHBase non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[] { dte };
                //Call the method
                ServerType.InvokeMember("GotoDate", BindingFlags.InvokeMethod, null, BASELaboObject, parameters);

                ActivateWindow(ServerType.GUID);
            }
            catch (Exception)
            {
                return;
            }
        }


        public static void setIdPatient(int IdPatient)
        {
            Type ServerType;
            object RHBaseObject;
            try
            {
                try
                {
                    ServerType = Type.GetTypeFromProgID(CLASSNAME);
                }
                catch (System.Exception)
                {
                    throw new System.Exception("RHBase non installé!");
                }

                //Create instance
                RHBaseObject = Activator.CreateInstance(ServerType);
                //Set the parameter which u want to set

                object[] parameters = new object[] { IdPatient };
                //Call the method
                ServerType.InvokeMember("SetIdPatient", BindingFlags.InvokeMethod, null, RHBaseObject, parameters);
            }
            catch (Exception)
            {

            }
        }

        public static void SetDateCourante(DateTime dte)
        {
            Type ServerType;
            object RHBaseObject;
            try
            {
                try
                {
                    ServerType = Type.GetTypeFromProgID(CLASSNAME);
                }
                catch (System.Exception)
                {
                    throw new System.Exception("RHBase non installé!");
                }

                //Create instance
                RHBaseObject = Activator.CreateInstance(ServerType);
                //Set the parameter which u want to set

                object[] parameters = new object[] { dte };
                //Call the method
                ServerType.InvokeMember("SetDateCourante", BindingFlags.InvokeMethod, null, RHBaseObject, parameters);
            }
            catch (Exception)
            {

            }
        }
    }
}
