using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class CodePrestation
    {
        public override string ToString()
        {
            return Code;
        }
        private double _Valeur;
        [PropertyCanBeSerialized]
        public double Valeur
        {
            get
            {
                return _Valeur;
            }
            set
            {
                _Valeur = value;
            }
        }

        private string _Libelle;
        [PropertyCanBeSerialized]
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

        private string _Code;
        [PropertyCanBeSerialized]
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
            }
        }
    }
}
