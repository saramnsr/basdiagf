using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Configuration;
using SwitchCabinet;

namespace OLEAccess
{
    public static class BASEVIEW
    {
        public const string CLASSNAME = "BAS_PHOTO.OLEServer";

        public static void SetPatientCourantByNomPrenom(string Nom, string Prenom, bool CreateIfNotExist)
        {
            Type ServerType;
            object BASEViewObject;
            try
            {
                try
                {
                    ServerType = Type.GetTypeFromProgID(CLASSNAME);
                }
                catch (System.Exception)
                {
                    throw new System.Exception("BASEView non installé!");
                }

                //Create instance
                BASEViewObject = Activator.CreateInstance(ServerType);
                //Set the parameter which u want to set

                object[] parameters = new object[] { Nom, Prenom, CreateIfNotExist };
                //Call the method
                ServerType.InvokeMember("SetPatientCourantByNomPrenom", BindingFlags.InvokeMethod, null, BASEViewObject, parameters);
            }
            catch (Exception)
            {

            }
        }

        public static void SetPatient(int Id)
        {
            Type ServerType;
            object BASEViewObject;
            try
            {
                try
                {
                    ServerType = Type.GetTypeFromProgID(CLASSNAME);
                }
                catch (System.Exception)
                {
                    throw new System.Exception("BASEView non installé!");

                }


                //Create instance 
                BASEViewObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[] { Id };
                //Call the method
                ServerType.InvokeMember("SetPatient", BindingFlags.InvokeMethod, null, BASEViewObject, parameters);
            }
            catch (Exception)
            {

            }
        }
        public static void KillProcess()
        {

            string value = ConfigurationManager.AppSettings["BASEVIEW"];
            System.Diagnostics.Process[] prcs = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(value));


            foreach (System.Diagnostics.Process p in prcs)
            {
                p.Kill();
            }
        }
        private static void switchCabinet()
        {
            KillProcess();
            ApplicationMngr.OpenBaseView();

        }
        public static void SwitchCabinet()
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
                switchCabinet();
            }
            catch (Exception e)
            {
                return;
            }
        }
    }
}