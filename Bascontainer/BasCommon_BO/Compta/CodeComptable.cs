using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO.Compta
{
    public class CodeComptable
    {
        public override string ToString()
        {
            return FullName;
        }
        public  string FullName
        {
            get
            {
               
                return "[" + Code + "] " + Libelle;
            }
        }

      


        public string Libelle { get; set; }

        string _Code;
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;

                while (_Code.Length < 6) _Code += "0";
            }
        }

        public string Classe
        {
            get
            {
                return Code.Substring(0, 1);
            }
        }

        public string SousClasse
        {
            get
            {
                return Code.Substring(0, 2);
            }
        }

        public string SousSousClasse
        {
            get
            {
                return Code.Substring(0, 3);
            }
        }
    }
}
