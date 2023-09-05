using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BasCommon_BL.OLEAccess
{
    public static class CEFSharp
    {
        public const string CLASSNAME = "CEFSharp.OLEServer";

        public static void Refresh(int idPatient)
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
                    throw new System.Exception("CEFSharp non installé!");
                }

                //Create instance
                BASEViewObject = Activator.CreateInstance(ServerType);
                //Set the parameter which u want to set

                object[] parameters = new object[] { idPatient };
                //Call the method
                ServerType.InvokeMember("Refresh", BindingFlags.InvokeMethod, null, BASEViewObject, parameters);
            }
            catch (Exception)
            {

            }
        }

     
       
    }
}
