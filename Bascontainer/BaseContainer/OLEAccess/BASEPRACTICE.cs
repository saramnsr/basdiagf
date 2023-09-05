using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Reflection;

namespace OLEAccess
{
    
    public static class BASEPRACTICE
    {
        public static string CLASSNAME = "BASEPractice.OLEServer";


        public static string Version()
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
                    throw new System.Exception("BASEPractice non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[] { };
                //Call the method
                return (string)ServerType.InvokeMember("Version", BindingFlags.InvokeMethod, null, BASELaboObject, parameters);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string FullPath()
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
                    throw new System.Exception("BASEPractice non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[] { };
                //Call the method
                return (string)ServerType.InvokeMember("FullPath", BindingFlags.InvokeMethod, null, BASELaboObject, parameters);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string Activate()
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
                    throw new System.Exception("BASEPractice non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[] { };
                //Call the method
                return (string)ServerType.InvokeMember("Activate", BindingFlags.InvokeMethod, null, BASELaboObject, parameters);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string SetPatientCourantById(int Id)
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
                    throw new System.Exception("BASEPractice non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[2] { Id, 0 };
                //Call the method
                return (string)ServerType.InvokeMember("SetPatientCourantById", BindingFlags.InvokeMethod, null, BASELaboObject, parameters);
            }
            catch (Exception)
            {
                return "";
            }
        }



    }
}
