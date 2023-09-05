using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;

namespace BASEDiag_BO
{
    public class BasePrinterSetting
    {

        public override string ToString()
        {
            if (settings == null) return "";
            return settings.ToString();
        }

        private PrinterSettings _settings;
        public PrinterSettings settings
        {
            get
            {
                return _settings;
            }
            set
            {
                _settings = value;
            }
        }

        private string _Descriptif;
        public string Descriptif
        {
            get
            {
                return _Descriptif;
            }
            set
            {
                _Descriptif = value;
            }
        }

        private string _Libelle;
        public string Libelle
        {
            get
            {
                return _Libelle;
            }
            set
            {
                _Libelle = value;
            }
        }

    }
}
