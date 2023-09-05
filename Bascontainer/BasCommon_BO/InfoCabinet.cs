using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
  
    public class InfoCabinet
    {



        private string _PrefixeFactSociete;
        public string PrefixeFactSociete
        {
            get
            {
                return _PrefixeFactSociete;
            }
            set
            {
                _PrefixeFactSociete = value;
            }
        }


        public string Mailcabinet { get; set; }

        private string _SiteWebCabinet;
        public string SiteWebCabinet
        {
            get
            {
                return _SiteWebCabinet;
            }
            set
            {
                _SiteWebCabinet = value;
            }
        }

        private string _VilleCabinet;
        public string VilleCabinet
        {
            get
            {
                return _VilleCabinet;
            }
            set
            {
                _VilleCabinet = value;
            }
        }

        private string _CodePostalCabinet;
        public string CodePostalCabinet
        {
            get
            {
                return _CodePostalCabinet;
            }
            set
            {
                _CodePostalCabinet = value;
            }
        }

        private string _Adresse2Cabinet;
        public string Adresse2Cabinet
        {
            get
            {
                return _Adresse2Cabinet;
            }
            set
            {
                _Adresse2Cabinet = value;
            }
        }

        private string _Adresse1Cabinet;
        public string Adresse1Cabinet
        {
            get
            {
                return _Adresse1Cabinet;
            }
            set
            {
                _Adresse1Cabinet = value;
            }
        }

        private string _NumTelCabinet;
        public string NumTelCabinet
        {
            get
            {
                return _NumTelCabinet;
            }
            set
            {
                _NumTelCabinet = value;
            }
        }


        private System.Drawing.Bitmap _logo;
        public System.Drawing.Bitmap logo
        {
            get
            {
                return _logo;
            }
            set
            {
                _logo = value;
            }
        }

        private string _NomCabinet;
        public string NomCabinet
        {
            get
            {
                return _NomCabinet;
            }
            set
            {
                _NomCabinet = value;
            }
        }
        private string _TypeCabinet;
        public string TypeCabinet
        {
            get
            {
                return _TypeCabinet;
            }
            set
            {
                _TypeCabinet = value;
            }
        }
    }
}
