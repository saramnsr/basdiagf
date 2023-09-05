using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BasCommon_BO
{
    public class baseSmallPersonne
    {

        public override string ToString()
        {
            return Nom + " " + Prenom;
        }

        private string _Prenom;
        [Description("Prenom du patient")]
        [PropertyCanBeSerialized]
        public string Prenom
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
        [Description("Nom du patient")]
        [PropertyCanBeSerialized]
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

        private int _Id = -1;
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


        private int _TYPE_MATERIEL;
        [PropertyCanBeSerialized]
        public int TYPE_MATERIEL
        {
            get
            {
                return _TYPE_MATERIEL;
            }
            set
            {
                _TYPE_MATERIEL = value;
            }
        }
        private Contact _MainFax = null;
        [PropertyCanBeSerialized]
        public Contact MainFax
        {
            get
            {
                return _MainFax;
            }
            set
            {
                _MainFax = value;
            }
        }


        private Contact _MainTel = null;
        [PropertyCanBeSerialized]
        public Contact MainTel
        {
            get
            {
                return _MainTel;
            }
            set
            {
                _MainTel = value;
            }
        }

        private Contact _MainMail = null;
        [PropertyCanBeSerialized]
        public Contact MainMail
        {
            get
            {
                return _MainMail;
            }
            set
            {
                _MainMail = value;
            }
        }

        private Contact _MainAdresse = null;
        [PropertyCanBeSerialized]
        public Contact MainAdresse
        {
            get
            {
                return _MainAdresse;
            }
            set
            {
                _MainAdresse = value;
            }
        }


        public string Mail
        {
            get
            {                
                return (MainMail == null) ? "" : MainMail.Value;
            }
        }

        public string Fax
        {
            get
            {
                return (MainFax == null) ? "" : MainFax.Value;
            }
        }

        public string Tel
        {
            get
            {
                return (MainTel == null) ? "" : MainTel.Value;
            }
        }

        public string Ville
        {
            get
            {
                return ((MainAdresse == null) || (MainAdresse.adresse == null)) ? "" : MainAdresse.adresse.Ville;
            }
        }

        public string CodePostal
        {
            get
            {
                return ((MainAdresse == null) || (MainAdresse.adresse == null)) ? "" : MainAdresse.adresse.CodePostal;
            }
        }

        public string Adresse1
        {
            get
            {
                return ((MainAdresse == null) || (MainAdresse.adresse == null)) ? "" : MainAdresse.adresse.Adr1;
            }
        }

        public string Adresse2
        {
            get
            {
                return ((MainAdresse == null) || (MainAdresse.adresse == null)) ? "" : MainAdresse.adresse.Adr2;
            }
        }

        private List<Contact> _contacts = null;
        [PropertyCanBeSerialized]
        public List<Contact> contacts
        {
            get
            {
                return _contacts;
            }
            set
            {
                _contacts = value;
            }
        }
        
    
    
    }
}
