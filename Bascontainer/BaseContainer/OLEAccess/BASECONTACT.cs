using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

namespace OLEAccess
{
    public static class BASECONTACT
    {
        public const string CLASSNAME = "BASE_CONTACT.OLEServer";

        public static void SetPatientFromNomPrenom(string nom, string prenom)
        {
            Type ServerType;
            object BASEContactObject;
            try
            {
                try
                {
                    ServerType = Type.GetTypeFromProgID(CLASSNAME);
                }
                catch (System.Exception)
                {
                    throw new System.Exception("BASEContact non installé!");
                }

                //Create instance
                BASEContactObject = Activator.CreateInstance(ServerType);
                
                //Set the parameter whic u want to set

                object[] parameters = new object[] { nom, prenom };
                //Call the method

                ServerType.InvokeMember("SetPatientFromNomPrenom", BindingFlags.InvokeMethod, null, BASEContactObject, parameters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
