using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEDiag_BO
{
    public class Appareil
    {
        public override string ToString()
        {
            return Libelle;
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

        private List<String> _Risques;
        public List<String> Risques
        {
            get
            {
                return _Risques;
            }
            set
            {
                _Risques = value;
            }
        }

        private string _InfoDEP;
        public string InfoDEP
        {
            get
            {
                return _InfoDEP;
            }
            set
            {
                _InfoDEP = value;
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
