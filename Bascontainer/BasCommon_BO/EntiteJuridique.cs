using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class EntiteJuridique
    {

        public override string ToString()
        {
            return Nom;
        }


        private string _Collaborateur;
        [PropertyCanBeSerialized]
        public string Collaborateur
        {
            get
            {
                return _Collaborateur;
            }
            set
            {
                _Collaborateur = value;
            }
        }

        private string _Gerant;
        [PropertyCanBeSerialized]
        public string Gerant
        {
            get
            {
                return _Gerant;
            }
            set
            {
                _Gerant = value;
            }
        }

        private string _OrdreDe;
        [PropertyCanBeSerialized]
        public string OrdreDe
        {
            get
            {
                return _OrdreDe;
            }
            set
            {
                _OrdreDe = value;
            }
        }

        private string _NumOrdre;
        [PropertyCanBeSerialized]
        public string NumOrdre
        {
            get
            {
                return _NumOrdre;
            }
            set
            {
                _NumOrdre = value;
            }
        }

        private string _RCS;
        [PropertyCanBeSerialized]
        public string RCS
        {
            get
            {
                return _RCS;
            }
            set
            {
                _RCS = value;
            }
        }

        private string _NumSIRET;
        [PropertyCanBeSerialized]
        public string NumSIRET
        {
            get
            {
                return _NumSIRET;
            }
            set
            {
                _NumSIRET = value;
            }
        }

        private DateTime? _DateCreation;
        [PropertyCanBeSerialized]
        public DateTime? DateCreation
        {
            get
            {
                return _DateCreation;
            }
            set
            {
                _DateCreation = value;
            }
        }

        private string _FormeSocial;
        [PropertyCanBeSerialized]
        public string FormeSocial
        {
            get
            {
                return _FormeSocial;
            }
            set
            {
                _FormeSocial = value;
            }
        }

        private string _SiteWeb;
        [PropertyCanBeSerialized]
        public string SiteWeb
        {
            get
            {
                return _SiteWeb;
            }
            set
            {
                _SiteWeb = value;
            }
        }

        private string _Mail;
        [PropertyCanBeSerialized]
        public string Mail
        {
            get
            {
                return _Mail;
            }
            set
            {
                _Mail = value;
            }
        }

        private string _Telephone;
        [PropertyCanBeSerialized]
        public string Telephone
        {
            get
            {
                return _Telephone;
            }
            set
            {
                _Telephone = value;
            }
        }

        private List<BanqueDeRemise> _ComptesBancaire = null;
        [PropertyCanBeSerialized]
        public List<BanqueDeRemise> ComptesBancaire
        {
            get
            {
                return _ComptesBancaire;
            }
            set
            {
                _ComptesBancaire = value;
            }
        }

        

        private string _CodePostal;
        [PropertyCanBeSerialized]
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

        private string _Ville;
        [PropertyCanBeSerialized]
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

        private string _Adresse2;
        [PropertyCanBeSerialized]
        public string Adresse2
        {
            get
            {
                return _Adresse2;
            }
            set
            {
                _Adresse2 = value;
            }
        }

        private string _Adresse1;
        [PropertyCanBeSerialized]
        public string Adresse1
        {
            get
            {
                return _Adresse1;
            }
            set
            {
                _Adresse1 = value;
            }
        }

        private string _Nom = "Nouvelle entité";
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
