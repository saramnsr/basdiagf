using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BASEDiag_BO
{

    [Serializable]
    public class Proposition
    {

        private int _IdScenario = -1;
        public int IdScenario
        {
            get
            {
                return _IdScenario;
            }
            set
            {
                _IdScenario = value;
            }
        }


        private int _IdDevis;
        public int IdDevis
        {
            get
            {
                return _IdDevis;
            }
            set
            {
                _IdDevis = value;
            }
        }

        private List<PoseAppareil> _poseAppareils = new List<PoseAppareil>();
        public List<PoseAppareil> poseAppareils
        {
            get
            {
                return _poseAppareils;
            }
            set
            {
                _poseAppareils = value;
            }
        }

        private int _IdModel =-1;
        public int IdModel
        {
            get
            {
                return _IdModel;
            }
            set
            {
                _IdModel = value;
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
                if ((patient != null) && (patient.Id != _IdPatient))
                    patient = null;
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

        public enum EtatProposition
        {
            NonImprimé =-1,
            Soumis = 0,
            Accepté = 1,
            Refusé = 2
            
        }

        private EtatProposition _Etat = EtatProposition.NonImprimé;
        public EtatProposition Etat
        {
            get
            {
                return _Etat;
            }
            set
            {
                _Etat = value;
            }
        }

        private DateTime? _DateEvenement= DateTime.Now;

        /// <summary>
        /// Date de refus/signature
        /// </summary>
        public DateTime? DateEvenement
        {
            get
            {
                return _DateEvenement;
            }
            set
            {
                _DateEvenement = value;
            }
        }

        private DateTime? _DateAcceptation;
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

        private DateTime _DateProposition;
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
       
        public override string ToString()
        {
            
            return libelle;
        }
                
        private List<Traitement> _traitements = new List<Traitement>();
        public List<Traitement> traitements
        {
            get
            {
                return _traitements;
            }
            set
            {
                _traitements = value;
            }
        }
           
        private string _libelle;
        public string libelle
        {
            get
            {
                return _libelle;
            }
            set
            {
                _libelle = value;
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
