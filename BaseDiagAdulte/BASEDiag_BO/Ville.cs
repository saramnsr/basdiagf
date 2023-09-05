using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEDiag_BO
{
    public class Ville
    {

        public override string ToString()
        {
            return Nom;
        }

        private string _CodePostal;
        public string CodePostal
        {
            get
            {
                return _CodePostal;
            }
            set
            {
                _CodePostal = value;
            }
        }

        private string _Nom;
        public string Nom
        {
            get
            {
                return _Nom;
            }
            set
            {
                _Nom = value;
            }
        }

        private int _Id;
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
