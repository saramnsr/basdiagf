using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Attribut
    {
        private int _Id;
        [PropertyCanBeSerialized]
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
        [PropertyCanBeSerialized]
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
        [PropertyCanBeSerialized]
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
