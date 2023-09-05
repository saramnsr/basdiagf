using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Devis_TK
    {

        private string _EmplacementArchivage;
        [PropertyCanBeSerialized]
        public string EmplacementArchivage
        {
            get
            {
                return _EmplacementArchivage;
            }
            set
            {
                _EmplacementArchivage = value;
            }
        }

        private string _RemarqueArchivage;
        [PropertyCanBeSerialized]
        public string RemarqueArchivage
        {
            get
            {
                return _RemarqueArchivage;
            }
            set
            {
                _RemarqueArchivage = value;
            }
        }

        private Utilisateur _ArchivePar = null;
        [PropertyCanBeSerialized]
        public Utilisateur ArchivePar
        {
            get
            {
                return _ArchivePar;
            }
            set
            {
                _ArchivePar = value;
            }
        }

        private DateTime? _DateArchivage = null;
        [PropertyCanBeSerialized]
        public DateTime? DateArchivage
        {
            get
            {
                return _DateArchivage;
            }
            set
            {
                _DateArchivage = value;
            }
        }


        private List<CommTraitement > _actesTraitement;
        [PropertyCanBeSerialized]
        public List<CommTraitement> actesTraitement
        {
            get
            {
                return _actesTraitement;
            }
            set
            {
                _actesTraitement = value;
            }
        }

        private basePatient _patient;
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
        public int IdPatient
        {
            get
            {
                if (patient != null) _IdPatient = patient.Id;
                return _IdPatient;
            }
            set
            {
                _IdPatient = value;
            }


        }


        private double? _MontantScenario = 0;
        public double? MontantScenario
        {
            get
            {

                return _MontantScenario;



            }
            set
            {
                _MontantScenario = value;
            }
        }


        private int _EcheancierDocteur = 0 ;
        public int EcheancierDocteur
        {
            get
            {

                return _EcheancierDocteur;



            }
            set
            {
                _EcheancierDocteur = value;
            }
        }


        private double? _MontantDocteur=0;
        public double? MontantDocteur
        {
            get
            {
                
                return _MontantDocteur ;



            }
            set
            {
                _MontantDocteur = value;
            }
        }

        private double? _Montant;
        public double? Montant
        {
            get
            {
                double vMontantLigne = 0;
                if (_actesTraitement != null)
                {
                    foreach (CommTraitement act in _actesTraitement)
                    {
                        if (act.desactive) continue;
                        vMontantLigne = vMontantLigne + act.MontantLigne;
                    }
                }
                return vMontantLigne;


               
            }
            set
            {
                _Montant = value;
            }
        }

        private double? _MontantAvantRemise;
        public double? MontantAvantRemise
        {
            get
            {
                double vMontantLigneAvantRemise = 0;
                if (_actesTraitement != null)
                {
                    foreach (CommTraitement act in _actesTraitement)
                    {
                        if (act.desactive) continue;
                        vMontantLigneAvantRemise = vMontantLigneAvantRemise + act.MontantLigneAvantRemise;
                    }
                }
                return vMontantLigneAvantRemise;

               
            }
            set
            {
                _MontantAvantRemise = value;
            }
        }



        private DateTime _DateProposition;
        [PropertyCanBeSerialized]
        public DateTime DateProposition
        {
            get
            {
                return _DateProposition;
            }
            set
            {
                _DateProposition = value;
            }
        }

        private DateTime? _DatePrevisionnelDeFinTraitement;
        [PropertyCanBeSerialized]
        public DateTime? DatePrevisionnelDeFinTraitement
        {
            get
            {
                return _DatePrevisionnelDeFinTraitement;
            }
            set
            {
                _DatePrevisionnelDeFinTraitement = value;
            }
        }

        private DateTime _DatePrevisionnelDeDebutTraitement;
        [PropertyCanBeSerialized]
        public DateTime DatePrevisionnelDeDebutTraitement
        {
            get
            {
                return _DatePrevisionnelDeDebutTraitement;
            }
            set
            {
                _DatePrevisionnelDeDebutTraitement = value;
            }
        }
        private List<Semestre> _semestres = new List<Semestre>();
        [PropertyCanBeSerialized]
        public List<Semestre> semestres
        {
            get
            {
                return _semestres;
            }
            set
            {
                _semestres = value;
            }
        }
        private List<TempEcheanceDefinition> _echeancestemp = null;
        [PropertyCanBeSerialized]
        public List<TempEcheanceDefinition> echeancestemp
        {
            get
            {
                return _echeancestemp;
            }
            set
            {
                _echeancestemp = value;
            }
        }


        private DateTime? _DateEcheance;
        [PropertyCanBeSerialized]
        public DateTime? DateEcheance
        {
            get
            {
                return _DateEcheance;
            }
            set
            {
                _DateEcheance = value;
            }
        }

        private DateTime? _DateAcceptation;
        [PropertyCanBeSerialized]
        public DateTime? DateAcceptation
        {
            get
            {
                return _DateAcceptation;
            }
            set
            {
                _DateAcceptation = value;
            }
        }


        private int _Id_Traitement = -1;
        public int Id_Traitement
        {
            get
            {
                return _Id_Traitement;
            }
            set
            {
                _Id_Traitement = value;
            }
        }


        private NewTraitement  _Traitement ;
        public NewTraitement Traitement
        {
            get
            {
                return _Traitement;
            }
            set
            {
                _Traitement = value;
            }
        }


        private string  _Titre = "";
        public string Titre
        {
            get
            {
                return _Titre;
            }
            set
            {
                _Titre = value;
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
        private double _RembMutuelle;
        [PropertyCanBeSerialized]
        public double RembMutuelle
        {
            get
            {
                return _RembMutuelle;
            }
            set
            {
                _RembMutuelle = value;
            }
        }
        private double _partPatient;
        [PropertyCanBeSerialized]
        public double partPatient
        {
            get
            {
                return _partPatient;
            }
            set
            {
                _partPatient = value;
            }
        }
        private string _localisationAnatomiuque;
        [PropertyCanBeSerialized]
        public string localisationAnatomiuque
        {
            get
            {
                return _localisationAnatomiuque;
            }
            set
            {
                _localisationAnatomiuque = value;
            }
        }
    }
}
