using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Reflection;

namespace OLEAccess
{
    public static class Orthalis
    {
        public const string CLASSNAME = "Orthalis.OrthalisOLE";


        public static void Activate()
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
                    throw new System.Exception("Orthalis non installé!");

                }


                //Create instance 
                BASEViewObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[] { };
                //Call the method
                ServerType.InvokeMember("Activate", BindingFlags.InvokeMethod, null, BASEViewObject, parameters);
            }
            catch (Exception)
            {

            }
        }

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
                    throw new System.Exception("Orthalis non installé!");
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
                    throw new System.Exception("Orthalis non installé!");

                }


                //Create instance 
                BASEViewObject = Activator.CreateInstance(ServerType);
                //Set the parameter whic u want to set

                object[] parameters = new object[] { Id };
                //Call the method
                ServerType.InvokeMember("SetPatientCourantById", BindingFlags.InvokeMethod, null, BASEViewObject, parameters);
            }
            catch (Exception)
            {

            }
        }
    }
}
