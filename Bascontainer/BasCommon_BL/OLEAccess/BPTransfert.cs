using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Reflection;

namespace OLEAccess
{
   

    public static class BPTransfert
    {

        public const string CLASSNAME = "BPTransfert.OLEServer";

        #region By OLE
        public static void SetPatient(int IdPatient)
        {
            Type BASELabo;
            object[] parameter = new object[1];
            object BASELaboObject;
            try
            {

                try
                {
                    BASELabo = Type.GetTypeFromProgID(CLASSNAME);
                }
                catch (System.Exception)
                {
                    throw new System.Exception("BPTransfert non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(BASELabo);
                //Set the parameter whic u want to set
                parameter[0] = IdPatient;
                //Call the method
                BASELabo.InvokeMember("SetIdPatient", BindingFlags.InvokeMethod, null, BASELaboObject, parameter);
            }
            catch (Exception)
            {

            }
        }


        public static void Activate()
        {
            Type BASELabo;
            object[] parameter = new object[0];
            object BASELaboObject;
            try
            {

                try
                {
                    BASELabo = Type.GetTypeFromProgID(CLASSNAME);
                }
                catch (System.Exception)
                {
                    throw new System.Exception("BPTransfert non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(BASELabo);
                //Set the parameter whic u want to set
                //Call the method
                BASELabo.InvokeMember("Activate", BindingFlags.InvokeMethod, null, BASELaboObject, parameter);
            }
            catch (Exception)
            {

            }
        }


        #endregion
    }
}
