using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
 
    public class Devis
    {

        public enum enumtypePropositon
        {
            Aucun = -1,
            Pediatrie = 0,
            Orthopedie = 1,
            Orthodontie = 2,
            Cont1 = 3,
            Cont2 = 4,
            Adulte = 5,
            Materiel = 6,
            ContentionAdulte = 7,
            FinitionAdulte = 8,
            ALaCarte = 9
        }

       

        private enumtypePropositon _TypeDevis = enumtypePropositon.Aucun;
        [PropertyCanBeSerialized]
        public enumtypePropositon TypeDevis
        {
            get
            {
                return _TypeDevis;
            }
            set
            {
                _TypeDevis = value;
            }
        }

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

        private List<ActePGPropose> _actesHorstraitement;
        [PropertyCanBeSerialized]
        public List<ActePGPropose> actesHorstraitement
        {
            get
            {
                return _actesHorstraitement;
            }
            set
            {
                _actesHorstraitement = value;
            }
        }

        private List<Proposition> _propositions = null;
        [PropertyCanBeSerialized]
        public List<Proposition> propositions
        {
            get
            {
                return _propositions;
            }
            set
            {
                _propositions = value;
            }
        }

        private int _IdObjetBaseView = -1;
        public int IdObjetBaseView
        {
            get
            {
                return _IdObjetBaseView;
            }
            set
            {
                _IdObjetBaseView = value;
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



        private double? _Montant;
        public double? Montant
        {
            get
            {
                return _Montant;
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
                return _MontantAvantRemise;
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


        private List<EcheanceDevisALaCarte> _echeancestemp = null;
        [PropertyCanBeSerialized]
        public List<EcheanceDevisALaCarte> echeancestemp
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
