using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using BASEDiag_BO;

namespace BASEDiag_BL.OLEAccess
{
    public static class BASELabo
    {
        /*
        #region Direct By Assembly

        public static void CreateDemandeInBaseLabo(Patient pat)
        {
            BASELabo_Cabinet.BO.BASELaboDemande objDem = new BASELabo_Cabinet.BO.BASELaboDemande();
            objDem.patient = BASELabo_Cabinet.BL.PatientMgmt.GetPatientFromDB(pat.Id);
            objDem.Cabinet = "";

            objDem.IsStandBy = true;

            int age = ResumeCliniqueMgmt.resumeCl.patient.AgeNbYears;
            if (age>3 && age<9)
            {
                
                foreach(CommonObjectif co in pat.SelectedObjectifs)
                {
                    //Si "Ingression molaire haut et bas" -> RCC+PM
                    if (co.Id == 39)
                    {
                        objDem.regcc = true;
                        objDem.regccplanmolaire = true;
                    }

                    //Si "egression molaire haut et bas" -> RCC+PRI
                    if (co.Id == 3)
                    {
                        objDem.regcc = true;
                        objDem.regccpri = true;
                    }

                    //Si "stimuler la croissance" -> RCC+Verin median
                    if (co.Id == 8)
                    {
                        objDem.regcc = true;
                        objDem.regccverinmed = true;
                    }

                    
                }
            }


            string suivitxt = "";
            string resume = "";
            BASELabo_Cabinet.BL.DemandesMgmt.AddDemande(objDem, out suivitxt, out resume);
        }

        #endregion
        */

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
