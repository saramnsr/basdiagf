using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO.Compta;

namespace BasCommon_BO
{
    public class BanqueDeRemise
    {

        public override string ToString()
        {
            return  Libelle.Trim();
        }

        public Journal journalComptable { get; set; }
        public string CompteComptable { get; set; }

        private string _CodePays;
        [PropertyCanBeSerialized]
        public string CodePays
        {
            get
            {
                return _CodePays;
            }
            set
            {
                _CodePays = value;
            }
        }

        private string _CodeBIC;
        [PropertyCanBeSerialized]
        public string CodeBIC
        {
            get
            {
                return _CodeBIC;
            }
            set
            {
                _CodeBIC = value;
            }
        }

        private string _NumCle;
        [PropertyCanBeSerialized]
        public string NumCle
        {
            get
            {
                return _NumCle;
            }
            set
            {
                _NumCle = value;
            }
        }

      

        private string _NumGui;
        [PropertyCanBeSerialized]
        public string NumGui
        {
            get
            {
                return _NumGui;
            }
            set
            {
                _NumGui = value;
            }
        }

        private string _NumCPT;
        [PropertyCanBeSerialized]
        public string NumCPT
        {
            get
            {
                return _NumCPT;
            }
            set
            {
                _NumCPT = value;
            }
        }

        private string _Titulaire;
        [PropertyCanBeSerialized]
        public string Titulaire
        {
            get
            {
                return _Titulaire;
            }
            set
            {
                _Titulaire = value;
            }
        }

        private string _NumA;
        [PropertyCanBeSerialized]
        public string NumA
        {
            get
            {
                return _NumA;
            }
            set
            {
                _NumA = value;
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

        private string _AddrBAN2;
        [PropertyCanBeSerialized]
        public string AddrBAN2
        {
            get
            {
                return _AddrBAN2;
            }
            set
            {
                _AddrBAN2 = value;
            }
        }

        private string _AddrBAN1;
        [PropertyCanBeSerialized]
        public string AddrBAN1
        {
            get
            {
                return _AddrBAN1;
            }
            set
            {
                _AddrBAN1 = value;
            }
        }

        private string _Libelle;
        [PropertyCanBeSerialized]
        public string Libelle
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

        private string _Code;
        [PropertyCanBeSerialized]
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
            }
        }

        private string _NumNNE;
        [PropertyCanBeSerialized]
        public string NumNNE
        {
            get
            {
                return _NumNNE;
            }
            set
            {
                _NumNNE = value;
            }
        }
    }
}
