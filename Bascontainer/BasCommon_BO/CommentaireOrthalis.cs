using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class CommentaireOrthalis
    {

        private int _IdPraticien;
        public int IdPraticien
        {
            get
            {
                return _IdPraticien;
            }
            set
            {
                _IdPraticien = value;
            }
        }

        private string _Praticien;
        public string Praticien
        {
            get
            {
                return _Praticien;
            }
            set
            {
                _Praticien = value;
            }
        }

        private string _Afaire;
        public string Afaire
        {
            get
            {
                return _Afaire;
            }
            set
            {
                _Afaire = value;
            }
        }

        private string _Fait;
        public string Fait
        {
            get
            {
                return _Fait;
            }
            set
            {
                _Fait = value;
            }
        }

        private string _Libre;
        public string Libre
        {
            get
            {
                return _Libre;
            }
            set
            {
                _Libre = value;
            }
        }

        private string _hygiene;
        public string hygiene
        {
            get
            {
                return _hygiene;
            }
            set
            {
                _hygiene = value;
            }
        }

        private DateTime _Date;
        public DateTime Date
        {
            get
            {
                return _Date;
            }
            set
            {
                _Date = value;
            }
        }
    }
}
