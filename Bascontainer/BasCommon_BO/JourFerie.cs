using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class JourFerie
    {
        public override string ToString()
        {
            return Dte.ToString("dd MMM yyyy") + ":" + Libelle;
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

        private DateTime _Dte;
        public DateTime Dte
        {
            get
            {
                return _Dte;
            }
            set
            {
                _Dte = value;
            }
        }

        private int _Id = -1;
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }
    }
}
