using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CS1504.BO;

namespace CS1504.BL
{
    public static class LecteurMgmt
    {
        public static void SetDefault()
        {
            int port = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["COMRegistration"]);

            if (Csp2_Dll_Call.csp2Init(port) == Csp2_Dll_Call.STATUS_OK)
                if (Csp2_Dll_Call.csp2Interrogate() == Csp2_Dll_Call.STATUS_OK)
                {

                    int nRetStatus = Csp2_Dll_Call.csp2SetDefaults();
                    if (nRetStatus < Csp2_Dll_Call.STATUS_OK)
                        throw new System.Exception("Erreur lors de la remise à 0");

                }
        }

        private static void SetDateTimeToNow()
        {

            DateTime dte = DateTime.Now;
            char[] strdatetime = new char[6];
            strdatetime[0] = Convert.ToChar(dte.Second);
            strdatetime[1] = Convert.ToChar(dte.Minute);
            strdatetime[2] = Convert.ToChar(dte.Hour);

            strdatetime[3] = Convert.ToChar(dte.Day);
            strdatetime[4] = Convert.ToChar(dte.Month);
            strdatetime[5] = Convert.ToChar((dte.Year - 2000));

            int nRetStatus = Csp2_Dll_Call.csp2SetTime(strdatetime);
            if (nRetStatus < Csp2_Dll_Call.STATUS_OK)
                throw new System.Exception("Erreur lors de la mise à l'heure du lecteur");


        }

        public static int ClearCodes()
        {
            return Csp2_Dll_Call.csp2ClearData();


        }

        public static List<BarCode> ReadCodes(bool CleanAfterLoad, int PortCOM)
        {
            List<BarCode> lstcodes = new List<BarCode>();

            System.Text.StringBuilder aBuffer = new StringBuilder("", 16);
            Csp2_Dll_Call.csp2SetDebugMode(Csp2_Dll_Call.PARAM_ON);
            if (Csp2_Dll_Call.csp2Init(PortCOM) == Csp2_Dll_Call.STATUS_OK)
                if (Csp2_Dll_Call.csp2Interrogate() == Csp2_Dll_Call.STATUS_OK)
                {
                    int nRetStatus = Csp2_Dll_Call.csp2GetDllVersion(aBuffer, 16);
                    if (nRetStatus >= Csp2_Dll_Call.STATUS_OK)
                    {
                        //textBox1.Text = "Version : " + aBuffer.ToString() + "\r\n";


                        nRetStatus = Csp2_Dll_Call.csp2ReadData(); /* Read barcodes */
                        SetDateTimeToNow();

                        if (nRetStatus == 0) return null;
                        //if (nRetStatus == 0) throw new Exception("Aucun code dans ce lecteur");



                        //textBox1.Text += "nombre de codes : " + nRetStatus.ToString() + "\r\n";
                        int BarcodesRead = nRetStatus;

                        string LastTimeStamp = "";

                        for (int i = 0; i < BarcodesRead; i++)
                        {
                            BarCode bc = new BarCode();

                            int PacketLength = Csp2_Dll_Call.csp2GetPacket(bc.CodeBarValue, i, 200);
                            
                            nRetStatus = Csp2_Dll_Call.csp2GetCodeType((int)bc.CodeBarValue[1], bc.CodeType, 30);
                            //textBox1.Text += "CodeType : " + bc.CodeType + "\r\n";
                            //bc.CodeBarValue = new StringBuilder(bc.CodeBarValue.ToString().Substring(0,12));

                            try
                            {
                                System.Text.StringBuilder tmp = new StringBuilder("0000", 4);
                                tmp[0] = bc.CodeBarValue[PacketLength - 4];
                                tmp[1] = bc.CodeBarValue[PacketLength - 3];
                                tmp[2] = bc.CodeBarValue[PacketLength - 2];
                                tmp[3] = bc.CodeBarValue[PacketLength - 1];
                                Csp2_Dll_Call.csp2TimeStamp2Str(tmp, bc.TimeStamp, 30);

                                LastTimeStamp = bc.TimeStamp.ToString();
                            }
                            catch (System.IndexOutOfRangeException)
                            {
                                bc.TimeStamp = new StringBuilder(LastTimeStamp);
                            }
                            string code = bc.CodeBarValue.ToString().Substring(2, PacketLength - 6);
                            bc.CodeBarValue = new System.Text.StringBuilder(code);
                            //textBox1.Text += bc.TimeStamp + "  :  " + code + "\r\n";
                            
                            lstcodes.Add(bc);
                        }
                        if (CleanAfterLoad)
                        {
                            nRetStatus = ClearCodes();
                            if (nRetStatus < Csp2_Dll_Call.STATUS_OK)
                                throw new System.Exception("Erreur à la suppression des codes");
                        }
                    }


                }
                else
                {
                    throw new Exception("Lecteur non connectée");
                }

            return lstcodes;
        }

    }
}
