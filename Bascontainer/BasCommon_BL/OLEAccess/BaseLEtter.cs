using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace OLEAccess
{
    public static class BASLetter
    {
        public static string CLASSNAME = "BaseLetter.OLEServer";

        public static void AddAttribut(string name, object value)
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
                    throw new System.Exception("BASELetter non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[] { name, value };
                //Call the method
                ServerType.InvokeMember("AddAttribut", BindingFlags.InvokeMethod, null, BASELaboObject, parameters);
            }
            catch (Exception)
            {

            }
        }


        public static void RemoveAttribut(string name, string value)
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
                    throw new System.Exception("BASELetter non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[] { name, value };
                //Call the method
                ServerType.InvokeMember("RemoveAttribut", BindingFlags.InvokeMethod, null, BASELaboObject, parameters);
            }
            catch (Exception)
            {

            }
        }

        public static int Open(string filename)
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
                    throw new System.Exception("BASELetter non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[] { filename };
                //Call the method
                return (int)ServerType.InvokeMember("Open", BindingFlags.InvokeMethod, null, BASELaboObject, parameters);
            }
            catch (Exception)
            {
                return -1;
            }
        }

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
                    throw new System.Exception("BASELetter non installé!");

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
                    throw new System.Exception("BASELetter non installé!");

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

        public static void ActiveWindow(int idx)
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
                    throw new System.Exception("BASELetter non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[] { idx };
                //Call the method
                ServerType.InvokeMember("ActiveWindow", BindingFlags.InvokeMethod, null, BASELaboObject, parameters);
            }
            catch (Exception)
            {

            }
        }

        public static void CloseWindow(int idx)
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
                    throw new System.Exception("BASELetter non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[] { idx };
                //Call the method
                ServerType.InvokeMember("CloseWindow", BindingFlags.InvokeMethod, null, BASELaboObject, parameters);
            }
            catch (Exception)
            {

            }
        }

        public static void Generate()
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
                    throw new System.Exception("BASELetter non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[] { };
                //Call the method
                ServerType.InvokeMember("Generate", BindingFlags.InvokeMethod, null, BASELaboObject, parameters);
            }
            catch (Exception)
            {

            }
        }

        public static void GenerateFrom(string file)
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
                    throw new System.Exception("BASELetter non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[1] { file };
                //Call the method
                ServerType.InvokeMember("GenerateFrom", BindingFlags.InvokeMethod, null, BASELaboObject, parameters);
            }
            catch (Exception)
            {

            }
        }

        public static void PrintFrom(string file)
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
                    throw new System.Exception("BASELetter non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[1] { file };
                //Call the method
                ServerType.InvokeMember("PrintFrom", BindingFlags.InvokeMethod, null, BASELaboObject, parameters);
            }
            catch (Exception)
            {

            }
        }

        public static void MailFrom(string file, string sujet, string body, string adresse)
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
                    throw new System.Exception("BASELetter non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[4] { file, sujet, body, adresse };
                //Call the method
                ServerType.InvokeMember("MailFrom", BindingFlags.InvokeMethod, null, BASELaboObject, parameters);
            }
            catch (Exception)
            {

            }
        }


        public static void AffectPrintSettings(string settingname)
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
                    throw new System.Exception("BASELetter non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[1] { settingname };
                //Call the method
                ServerType.InvokeMember("AffectPrintSettings", BindingFlags.InvokeMethod, null, BASELaboObject, parameters);
            }
            catch (Exception)
            {

            }
        }


        public static void Print(int idx)
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
                    throw new System.Exception("BASELetter non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[] { idx };
                //Call the method
                ServerType.InvokeMember("Print", BindingFlags.InvokeMethod, null, BASELaboObject, parameters);
            }
            catch (Exception)
            {

            }
        }
        public static void KillProcess()
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
                    throw new System.Exception("BASELetter non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[] {};
                //Call the method
                ServerType.InvokeMember("Kill", BindingFlags.InvokeMethod, null, BASELaboObject, parameters);
            }
            catch (Exception)
            {

            }
        }



    }


}
