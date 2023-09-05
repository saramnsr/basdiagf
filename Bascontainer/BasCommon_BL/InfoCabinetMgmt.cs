using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;
using System.IO;
namespace BasCommon_BL
{
    public static class InfoCabinetMgmt
    {
        private static InfoCabinet _informationsCabinet;
        public static InfoCabinet informationsCabinet
        {
            get
            {
                if (_informationsCabinet == null) _informationsCabinet = GetInformationCabinet();
                return _informationsCabinet;
            }
            
        }

        private static InfoCabinet GetInformationCabinet()
        {
            InfoCabinet nfo = new InfoCabinet();
            nfo.Adresse1Cabinet = (string)System.Configuration.ConfigurationManager.AppSettings["Adresse1Cabinet"];
            nfo.Adresse2Cabinet = (string)System.Configuration.ConfigurationManager.AppSettings["Adresse2Cabinet"];
            nfo.VilleCabinet = (string)System.Configuration.ConfigurationManager.AppSettings["VilleCabinet"];
            nfo.CodePostalCabinet = (string)System.Configuration.ConfigurationManager.AppSettings["CodePostalCabinet"];
            nfo.NumTelCabinet = (string)System.Configuration.ConfigurationManager.AppSettings["NumTelCabinet"];
            nfo.SiteWebCabinet = (string)System.Configuration.ConfigurationManager.AppSettings["SiteWebCabinet"];
            nfo.NomCabinet = (string)System.Configuration.ConfigurationManager.AppSettings["NomCabinet"];
            nfo.Mailcabinet = (string)System.Configuration.ConfigurationManager.AppSettings["MailCabinet"];
            nfo.PrefixeFactSociete = (string)System.Configuration.ConfigurationManager.AppSettings["PrefixeFactSociete"];
            nfo.TypeCabinet = (string)( System.Configuration.ConfigurationManager.AppSettings["TypeCabinet"]);

            string bmpfile = System.Configuration.ConfigurationManager.AppSettings["logoCabinet"];

            if (File.Exists(bmpfile))
            {
                System.Drawing.Bitmap logo = (System.Drawing.Bitmap)System.Drawing.Bitmap.FromFile(bmpfile);
                nfo.logo = logo;
            }

            return nfo;
        }
    }
}
