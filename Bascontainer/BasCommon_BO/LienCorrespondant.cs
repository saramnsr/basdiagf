using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    [Serializable]
    public class LienCorrespondant
    {


        private int _IdCorrespondance = -1;
        [PropertyCanBeSerialized]
        public int IdCorrespondance
        {
            get
            {
                if (correspondant != null) _IdCorrespondance = correspondant.Id;
                return _IdCorrespondance;
            }
            set
            {
                _IdCorrespondance = value;
            }
        }


        private basePatient _patient;
        [PropertyCanBeSerialized]
        public basePatient patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
            }
        }

        private int _IdPatient;
        [PropertyCanBeSerialized]
        public int IdPatient
        {
            get
            {
                return _IdPatient;
            }
            set
            {
                _IdPatient = value;
            }
        }

        private Correspondant _correspondant = null;
        [PropertyCanBeSerialized]
        public Correspondant correspondant
        {
            get
            {
                return _correspondant;
            }
            set
            {
                _correspondant = value;
            }
        }

        public override string ToString()
        {
            if (correspondant == null)
                return _Fonctions[TypeDeLien];
            else
                return _Fonctions[TypeDeLien] + " : " + correspondant.Nom + " " + correspondant.Prenom;// +" (" + correspondant.Profession + ")" + "[" + LienLibelle + "]" + "[" + TypeDeLien + "]";
        }



        private static Dictionary<String, string> _Fonctions;
        public static Dictionary<String, string> Fonctions
        {
            get
            {
                return _Fonctions;
            }
        }

        static LienCorrespondant()
        {
            _Fonctions = new Dictionary<string, string>();
            _Fonctions.Add("As", "Assuré");
            _Fonctions.Add("Au", "Autre");
            _Fonctions.Add("Ca", "Caisse");
            _Fonctions.Add("Ch", "Chirurgien");
            _Fonctions.Add("Co", "Coéquipier");
            _Fonctions.Add("De", "Dentiste");
            _Fonctions.Add("Fo", "Fournisseur");
            _Fonctions.Add("Ki", "Kinesitherapeute");
            _Fonctions.Add("MO", "Medecin ORL");
            _Fonctions.Add("MT", "Medecin Traitant");
            _Fonctions.Add("Md", "Medecin");
            _Fonctions.Add("Mu", "Mutuelle");
            _Fonctions.Add("Od", "Orthodontiste");
            _Fonctions.Add("Or", "Orthophoniste");
            _Fonctions.Add("Pa", "Payeur");
            _Fonctions.Add("Pb", "En cas de probleme");
            _Fonctions.Add("Pe", "Pediatre");
            _Fonctions.Add("Pr", "Paradon");
            _Fonctions.Add("Rc", "Recommandé par");
            _Fonctions.Add("Rs", "Représentant légal");
            _Fonctions.Add("St", "Stomatologue");
            _Fonctions.Add("im", "implanto");
            _Fonctions.Add("os", "ostéopathe");
            _Fonctions.Add("ho", "homéopathe");

        }

        public bool IsFamille
        {
            get
            {
                return TypeDeLien == "Au" |
                       TypeDeLien == "As" |
                       TypeDeLien == "Pa" |
                       TypeDeLien == "Pb" |
                       TypeDeLien == "Rs";
            }
        }



        public bool IsAssure
        {
            get
            {
                return TypeDeLien == "As";
            }
        }

        public bool IsDentiste
        {
            get
            {
                return TypeDeLien == "De";
            }
        }




        public bool IsRepresentantLegal
        {
            get
            {
                return TypeDeLien == "Rs";
            }
        }

        public bool IsPayeur
        {
            get
            {
                return TypeDeLien == "Pa";
            }
        }

        public bool IsCaisse
        {
            get
            {
                return TypeDeLien == "Ca";
            }
        }

        public bool IsMutuelle
        {
            get
            {
                return TypeDeLien == "Mu";
            }
        }

        public bool IsInstitution
        {
            get
            {
                return TypeDeLien == "Ca" |
                       TypeDeLien == "Mu";
            }
        }

        public bool IsRecomande
        {
            get
            {
                return TypeDeLien == "Rc";
            }
        }

        public bool IsProfessionnel
        {
            get
            {
                return TypeDeLien == "Ch" |
                       TypeDeLien == "De" |
                       TypeDeLien == "Ki" |
                       TypeDeLien == "MO" |
                       TypeDeLien == "MT" |
                       TypeDeLien == "Md" |
                       TypeDeLien == "Od" |
                       TypeDeLien == "Or" |
                       TypeDeLien == "Pe" |
                       TypeDeLien == "Pr" |
                       TypeDeLien == "im" |
                       TypeDeLien == "os" |
                       TypeDeLien == "St";
            }
        }

        public string Lien
        {
            get
            {
                try
                {
                    return Fonctions[TypeDeLien];
                }
                catch (System.Exception)
                {
                    return "";
                }


            }

        }


        private string _LienLibelle;
        [PropertyCanBeSerialized]
        public string LienLibelle
        {
            get
            {
                return _LienLibelle;
            }
            set
            {
                _LienLibelle = value;
            }
        }


        private string _TypeDeLien;
        [PropertyCanBeSerialized]
        public string TypeDeLien
        {
            get
            {
                return _TypeDeLien;
            }
            set
            {
                _TypeDeLien = value;
            }
        }
    }
}
