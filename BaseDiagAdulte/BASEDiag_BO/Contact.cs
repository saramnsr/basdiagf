using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEDiag_BO
{
    public class ContactAdresse
    {

        private string _Pays;
        public string Pays
        {
            get
            {
                return _Pays;
            }
            set
            {
                _Pays = value;
            }
        }

        private string _Ville;
        public string Ville
        {
            get
            {
                return _Ville;
            }
            set
            {
                _Ville = value;
            }
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

        private string _Adr2;
        public string Adr2
        {
            get
            {
                return _Adr2;
            }
            set
            {
                _Adr2 = value;
            }
        }

        private string _Adr1;
        public string Adr1
        {
            get
            {
                return _Adr1;
            }
            set
            {
                _Adr1 = value;
            }
        }
    }

    public class Contact
    {
        public enum ContactType
        {
            Inconnue = -1,
            Telephone = 1,
            Mail = 2,
            Fax = 3,
            Adresse = 4
        }

        private ContactAdresse _adresse = null;
        public ContactAdresse adresse
        {
            get
            {
                return _adresse;
            }
            set
            {
                _adresse = value;
            }
        }

        private int _IdPersonne = -1;
        public int IdPersonne
        {
            get
            {
                return _IdPersonne;
            }
            set
            {
                _IdPersonne = value;
            }
        }
        /*
        private bool _isMain = false;
        public bool isMain
        {
            get
            {
                return _isMain;
            }
            set
            {
                _isMain = value;
            }
        }
        */
        private string _Value;
        public string Value
        {
            get
            {
                if ((TypeContact == ContactType.Adresse) && (adresse != null))
                    return adresse.Adr1 + " " + adresse.Adr2 + " " + adresse.CodePostal + " " + adresse.Ville;
                else
                    return _Value;
            }
            set
            {

                _Value = value;
            }
        }

        private ContactLib _Libelle;
        public ContactLib Libelle
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

        private ContactType _TypeContact;
        public ContactType TypeContact
        {
            get
            {
                return _TypeContact;
            }
            set
            {
                _TypeContact = value;
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
