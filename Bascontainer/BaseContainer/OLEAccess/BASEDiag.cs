using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Reflection;

namespace OLEAccess
{
   

    public static class BASEDiag
    {

        public const string CLASSNAME = "BASEDiag.OLEServer";

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
                    throw new System.Exception("BASEDiag non installé!");

                }


                //Create instance 
                BASELaboObject = Activator.CreateInstance(BASELabo);
                //Set the parameter whic u want to set
                parameter[0] = IdPatient;
                //Call the method
                BASELabo.InvokeMember("SetPatient", BindingFlags.InvokeMethod, null, BASELaboObject, parameter);
            }
            catch (Exception)
            {

            }
        }




        #endregion
    }
}
