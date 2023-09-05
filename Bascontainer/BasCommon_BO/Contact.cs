using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Contact
    {
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

        private int? _IdPersonne = null;
        [PropertyCanBeSerialized]
        public int? IdPersonne
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

        public enum ContactType
        {
            Inconnue = -1,
            Telephone = 1,
            Mail = 2,
            Fax = 3,
            Adresse = 4
        }

        private ContactType? _TypeContact = null;
        [PropertyCanBeSerialized]
        public ContactType? TypeContact
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

        private int? _prefOrder = null;
        [PropertyCanBeSerialized]
        public int? prefOrder
        {
            get
            {
                return _prefOrder;
            }
            set
            {
                _prefOrder = value;
            }
        }

        private ContactAdresse _adresse = null;
        [PropertyCanBeSerialized]
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

        private string _Value;
        [PropertyCanBeSerialized]
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
        [PropertyCanBeSerialized]
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
        private int _id_pays = -1;
        [PropertyCanBeSerialized]
        public int id_pays
        {
            get
            {
                return _id_pays;
            }
            set
            {
                _id_pays = value;
            }
        }
        private Pays _pays;
        [PropertyCanBeSerialized]
        public Pays pays
        {
            get
            {
                
                    return _pays;
            }
            set
            {
                _pays = value;
            }
        }
        private Boolean _isSms;
        [PropertyCanBeSerialized]
        public Boolean isSms
        {
            get
            {
                return _isSms;
            }
            set
            {
                _isSms = value;
            }
        }
    }
}
