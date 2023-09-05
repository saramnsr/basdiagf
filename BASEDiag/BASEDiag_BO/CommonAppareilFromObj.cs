using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BASEDiag_BO
{
    public class CommonAppareilFromObj
    {

        public override string ToString()
        {
            if (Description != "")
                return appareil.Libelle + "(" + Description + ")";
            else
                return appareil.Libelle;

        }

        private string _Description;
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        private CommonObjectif _objectif;
        public CommonObjectif objectif
        {
            get
            {
                return _objectif;
            }
            set
            {
                _objectif = value;
            }
        }

        private Appareil _appareil;
        public Appareil appareil
        {
            get
            {
                return _appareil;
            }
            set
            {
                _appareil = value;
            }
        }
    }
}
