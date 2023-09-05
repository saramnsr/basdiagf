using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BASEDiag_BO
{
    public class TemplateActePG
    {
        public enum EnumPhase
        {
            Aucune = 0,
            Pédiatrique = 1,
            Orthopedique = 2,
            Orthodontique = 3,
            Surveillance = 4,
            Contention = 5,
            Adulte = 6

        }


        private EnumPhase _phase;
        public EnumPhase phase
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

        private List<Appareil> _SuggestedAppareils = new List<Appareil>();
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

        private List<Appareil> _appareils = new List<Appareil>();
        [Obsolete("Remplacer par SuggestedAppareils")]
        public List<Appareil> appareils
        {
            get
            {
                return _appareils;
            }
            set
            {
                _appareils = value;
            }
        }

        public override string ToString()
        {
            return Libelle;
        }

        private int? _NBMois = 0;
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

        private double _Valeur;
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

        private string _Libelle;
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

        private int _CorrespondanceOrthalis;
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
