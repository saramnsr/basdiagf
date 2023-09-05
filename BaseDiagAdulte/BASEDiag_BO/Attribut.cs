using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEDiag_BO
{
    public class Attribut
    {
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

        private string _Valeur;
        public string Valeur
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

        private String _Nom;
        public String Nom
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


    }
}
