using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Mutuelle
    {



        public int IdVille = -1;
        public int IdAdresse = -1;


        private string m_Adresse_Num;
        private string m_Adresse_Type_Voie;
        private string m_Adresse_Nom_Voie;


        private bool _NeedOrder;
        [PropertyCanBeSerialized]
        public bool NeedOrder
        {
            get
            {
                return _NeedOrder;
            }
            set
            {
                _NeedOrder = value;
            }
        }

        private string _NumMutuelle = "";
        [PropertyCanBeSerialized]
        public string NumMutuelle
        {
            get
            {
                return _NumMutuelle;
            }
            set
            {
                _NumMutuelle = value;
            }
        }

        private double _MontantPlafond;
        [PropertyCanBeSerialized]
        public double MontantPlafond
        {
            get
            {
                return _MontantPlafond;
            }
            set
            {
                _MontantPlafond = value;
            }
        }

        private double _TauxParDefaut;
        [PropertyCanBeSerialized]
        public double TauxParDefaut
        {
            get
            {
                return _TauxParDefaut;
            }
            set
            {
                _TauxParDefaut = value;
            }
        }

        private bool _IsTiersPayant;
        [PropertyCanBeSerialized]
        public bool IsTiersPayant
        {
            get
            {
                return _IsTiersPayant;
            }
            set
            {
                _IsTiersPayant = value;
            }
        }

        private bool _IsCMU;
        [PropertyCanBeSerialized]
        public bool IsCMU
        {
            get
            {
                return _IsCMU;
            }
            set
            {
                _IsCMU = value;
            }
        }

        public override string ToString()
        {
            return Nom;
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

        [PropertyCanBeSerialized]
        public string Adresse_Num
        {
            get { return m_Adresse_Num; }
            set { m_Adresse_Num = value; }
        }

        [PropertyCanBeSerialized]
        public string Adresse_Type_Voie
        {
            get { return m_Adresse_Type_Voie; }
            set { m_Adresse_Type_Voie = value; }
        }

        [PropertyCanBeSerialized]
        public string Adresse_Nom_Voie
        {
            get { return m_Adresse_Nom_Voie; }
            set { m_Adresse_Nom_Voie = value; }
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
    }
}
