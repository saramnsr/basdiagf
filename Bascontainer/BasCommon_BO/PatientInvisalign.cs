using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class PatientInvisalign
    {
        private int _OrderID;
        public int OrderID
        {
            get
            {
                return _OrderID;
            }
            set
            {
                _OrderID = value;
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

        private string  _Prenom;
        public string  Prenom
        {
            get
            {
                return _Prenom;
            }
            set
            {
                _Prenom = value;
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
    }
}
