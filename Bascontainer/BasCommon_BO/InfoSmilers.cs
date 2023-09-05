using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BL;

namespace BasCommon_BO
{
   public   class InfoSmilers
    {
       public override string ToString()
       {
           string namevalidepar = UtilisateursMgt.getUtilisateur(validepar).Nom + "" + UtilisateursMgt.getUtilisateur(validepar).Prenom;
           return numdossier + ":"+"Valide par : " + namevalidepar + " le " + datesaisie.ToString("dd MMM yy");
       }
        private basePatient _CurrentPatient;
        public basePatient CurrentPatient
        {
            get
            {
                return _CurrentPatient;
            }
            set
            {
                _CurrentPatient = value;
            }
        }
       private string _numdossier;
       public string numdossier
        {
            get
            {
                return _numdossier;
            }
            set
            {
                _numdossier = value;
            }
        }
        private string _nom;
        public string nom
        {
            get
            {
                return _nom;
            }
            set
            {
                _nom = value;
            }
        }
        private string _prenom;
        public string prenom
        {
            get
            {
                return _prenom;
            }
            set
            {
                _prenom = value;
            }
        }
        private string _genre;
        public string genre
        {
            get
            {
                return _genre;
            }
            set
            {
                _genre = value;
            }
        }
        private DateTime _dateNaissance;
        public DateTime dataNaissance
        {
            get
            {
                return _dateNaissance;
            }
            set
            {
                _dateNaissance = value;
            }
        }
        private int _orderid;
        public int orderid
        {
            get
            {
                return _orderid;
            }
            set
            {
                _orderid = value;
            }
        }
        private int _idPatient;
        public int idPatient
        {
            get
            {
                return _idPatient;
            }
            set
            {
                _idPatient = value;
            }
        }
        private int _faitpar;
        public int faitpar
        {
            get
            {
                return _faitpar;
            }
            set
            {
                _faitpar = value;
            }
        }
        private int _validepar;
        public int validepar
        {
            get
            {
                return _validepar;
            }
            set
            {
                _validepar = value;
            }
        }
        private DateTime _datesaisie;
        public DateTime datesaisie
        {
            get
            {
                return _datesaisie;
            }
            set
            {
                _datesaisie = value;
            }
        }
        private string _comment;
        public string comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
            }
        }
        private string _pack;
        public string pack
        {
            get
            {
                return _pack;
            }
            set
            {
                _pack = value;
            }
        }
        private string _finition;
        public string finition
        {
            get
            {
                return _finition;
            }
            set
            {
                _finition = value;
            }
        }

    }
}
