using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEDiag_BO
{
    public class CodePrestation
    {
        private double _Valeur;
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
