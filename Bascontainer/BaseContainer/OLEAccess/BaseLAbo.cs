using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Reflection;

namespace OLEAccess
{
    public static class BaseLAbo
    {
        public const string CLASSNAME = "BASELabo_Cabinet.OLEServer";


        #region By OLE
        public static void NouvelleDemandeInStandBy(int IdPatient)
        {
            Type BASELabo;
            object[] parameter = new object[1];
            object BASELaboObject;
            try
            {

                try
                {
                    BASELabo = Type.GetTypeFromProgID("BASELabo_Cabinet.OLEServer");
                }
                catch (System.Exception)
                {
                    throw new System.Exception("BASELabo non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(BASELabo);
                //Set the parameter whic u want to set
                parameter[0] = IdPatient;
                //Call the method
                BASELabo.InvokeMember("NouvelleDemandeInStandBy", BindingFlags.InvokeMethod, null, BASELaboObject, parameter);
            }
            catch (Exception)
            {

            }
        }

        public static void NouvelleDemande(int IdPatient)
        {
            Type BASELabo;
            object[] parameter = new object[1];
            object BASELaboObject;
            try
            {

                try
                {
                    BASELabo = Type.GetTypeFromProgID("BASELabo_Cabinet.OLEServer");
                }
                catch (System.Exception)
                {
                    throw new System.Exception("BASELabo non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(BASELabo);
                //Set the parameter whic u want to set
                parameter[0] = IdPatient;
                //Call the method
                BASELabo.InvokeMember("NouvelleDemande", BindingFlags.InvokeMethod, null, BASELaboObject, parameter);
            }
            catch (Exception)
            {

            }
        }

        public static void PrintDemande(int demande)
        {
            Type BASELabo;
            object[] parameter = new object[1];
            object BASELaboObject;
            try
            {

                try
                {
                    BASELabo = Type.GetTypeFromProgID("BASELabo_Cabinet.OLEServer");
                }
                catch (System.Exception)
                {
                    throw new System.Exception("BASELabo non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(BASELabo);
                //Set the parameter whic u want to set
                parameter[0] = demande;
                //Call the method
                BASELabo.InvokeMember("PrintDemande", BindingFlags.InvokeMethod, null, BASELaboObject, parameter);
            }
            catch (Exception)
            {

            }
        }


        #endregion

    }
}
