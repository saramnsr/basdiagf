using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class TemplateActePG
    {


        private List<Appareil> _SuggestedAppareils;
        [PropertyCanBeSerialized]
        public List<Appareil> SuggestedAppareils
        {
            get
            {
                return _SuggestedAppareils;
            }
            set
            {
                _SuggestedAppareils = value;
            }
        }


        private int _CorrespondanceOrthalis;
        [PropertyCanBeSerialized]
        public int CorrespondanceOrthalis
        {
            get
            {
                return _CorrespondanceOrthalis;
            }
            set
            {
                _CorrespondanceOrthalis = value;
            }
        }


        private ActePG.TypeReglement _TypeDeReglement = ActePG.TypeReglement.Forfaitaire;
        [PropertyCanBeSerialized]
        public ActePG.TypeReglement TypeDeReglement
        {
            get
            {
                return _TypeDeReglement;
            }
            set
            {
                _TypeDeReglement = value;
            }
        }

        private BasCommon_BO.Traitement.EnumPhase _phase;
        [PropertyCanBeSerialized]
        public BasCommon_BO.Traitement.EnumPhase phase
        {
            get
            {
                return _phase;
            }
            set
            {
                _phase = value;
            }
        }


        public string DisplayCodeNVal
        {
            get
            {
                if (Coeff == 1)
                    return Code.Code;
                if (IsDecomposed)
                    return Code.Code + CoeffDecompose;
                else
                    return Code.Code + Coeff;

            }

        }



        public override string ToString()
        {
            return Libelle;
        }

        private int? _NBMois = 0;
        [PropertyCanBeSerialized]
        public int? NBMois
        {
            get
            {
                return _NBMois;
            }
            set
            {
                _NBMois = value;
            }
        }

        private int? _NBJours = 0;
        [PropertyCanBeSerialized]
        public int? NBJours
        {
            get
            {
                return _NBJours;
            }
            set
            {
                _NBJours = value;
            }
        }

        private bool _NeedFS;
        [PropertyCanBeSerialized]
        public bool NeedFS
        {
            get
            {
                return _NeedFS;
            }
            set
            {
                _NeedFS = value;
            }
        }

        private bool _NeedDEP;
        [PropertyCanBeSerialized]
        public bool NeedDEP
        {
            get
            {
                return _NeedDEP;
            }
            set
            {
                _NeedDEP = value;
            }
        }

        private bool _IsDecomposed;
        [PropertyCanBeSerialized]
        public bool IsDecomposed
        {
            get
            {
                return _IsDecomposed;
            }
            set
            {
                _IsDecomposed = value;
            }
        }

        private string _CoeffDecompose;
        [PropertyCanBeSerialized]
        public string CoeffDecompose
        {
            get
            {
                return _CoeffDecompose;
            }
            set
            {
                _CoeffDecompose = value;
            }
        }

        private int _Coeff;
        [PropertyCanBeSerialized]
        public int Coeff
        {
            get
            {
                return _Coeff;
            }
            set
            {
                _Coeff = value;
            }
        }

        private CodePrestation _Code;
        [PropertyCanBeSerialized]
        public CodePrestation Code
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

       


        private string _Organisation;
        [PropertyCanBeSerialized]
        public string Organisation
        {
            get
            {
                return _Organisation;
            }
            set
            {
                _Organisation = value;
            }
        }

        private double _Valeur;
        [PropertyCanBeSerialized]
        public double Valeur
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

        private double _VALEUR_CMU;
        [PropertyCanBeSerialized]
        public double VALEUR_CMU
        {
            get
            {
                return _VALEUR_CMU;
            }
            set
            {
                _VALEUR_CMU = value;
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

        private string _Nom;
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
    }
}
