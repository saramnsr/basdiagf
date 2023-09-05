using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    [Serializable]
    public class Proposition
    {

        public override string ToString()
        {

            return libelle;
        }
        public Proposition()
        {
        }
        public  Proposition(Devis_TK d)
        {
        this._DateEvenement  = d.DatePrevisionnelDeDebutTraitement ;
        this._DateProposition  = DateTime .Now ;
        this.Etat = EtatProposition.Soumis;
        this.IdPatient = d.IdPatient;
        this.patient = d.patient;
        this.libelle = d.Traitement.Traitement_libelle;
        this.echeancestemp = d.echeancestemp;
        Traitement  t;
            
        foreach (CommTraitement at in d.actesTraitement)
        {
            
            //Acte principale
            t = new Traitement();
            t.Id = at.Id ;
            t.Libelle = "traitement " + at.Acte.acte_libelle;
            t.semestres = at.semestres;
            
            this.traitements.Add(t);
          
          /*  
            //Actes Supp
            foreach (CommActesTraitement aa in at.ActesSupp )
            {
                t = new Traitement();
                t.Id = aa.IdActe ;
                t.Libelle = aa.LibActe;
                this.traitements.Add(t);
            }
            //Radios
            foreach (CommActesTraitement aa in at.Radios)
            {
                t = new Traitement();
                t.Id = aa.IdActe;
                t.Libelle = aa.LibActe;
                this.traitements.Add(t);
            }
            //Photos
            foreach (CommActesTraitement aa in at.photos)
            {
                t = new Traitement();
                t.Id = aa.IdActe;
                t.Libelle = aa.LibActe;
                this.traitements.Add(t);
            }
            //Materiels
            foreach (CommMaterielTraitement aa in at.Materiels)
            {
                t = new Traitement();
                t.Id = aa.idMateriel;
                t.Libelle = aa.Libelle ;
                this.traitements.Add(t);
            }*/

        }
        }
        
        private List<ActePGPropose> _matosassociate = null;
        [PropertyCanBeSerialized]
        public List<ActePGPropose> matosassociate
        {
            get
            {
                return _matosassociate;
            }
            set
            {
                _matosassociate = value;
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

        private int _IdScenario = -1;
        [PropertyCanBeSerialized]
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
        [PropertyCanBeSerialized]
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

        

        private int _IdModel = -1;
        [PropertyCanBeSerialized]
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
        [PropertyCanBeSerialized]
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

        public enum EtatProposition
        {
            NonImprimé = -1,
            Soumis = 0,
            Accepté = 1,
            Refusé = 2

        }

        private EtatProposition _Etat = EtatProposition.NonImprimé;
        [PropertyCanBeSerialized]
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

        private DateTime? _DateEvenement;

        /// <summary>
        /// Date de refus/signature
        /// </summary>
        [PropertyCanBeSerialized]
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


        private DateTime _DateProposition = DateTime.Now;
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

        

        private List<Traitement> _traitements = new List<Traitement>();
        [PropertyCanBeSerialized]
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
        [PropertyCanBeSerialized]
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
