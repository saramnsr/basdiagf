using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class EntentePrealable
    {

        public override string ToString()
        {

            string periode = "";

            if (typetraitement == EntentePrealable.TypeDeTraitement.Semestre)
                periode = "semestre " + Semestre.ToString();
            if (typetraitement == EntentePrealable.TypeDeTraitement.Debut)
                periode = "Debut ";
            if (typetraitement == EntentePrealable.TypeDeTraitement.Contention)
                periode = "Contention " + Contention;

            if (typetraitement == EntentePrealable.TypeDeTraitement.Autre)
                periode = "Autre " + Autre;

            if (typetraitement == EntentePrealable.TypeDeTraitement.Surveillance)
                periode = "Surveillance ";

            return dateProposition.ToShortDateString() + ":" + periode;
        }

        #region Enums


        public enum Appareillage
        {
            RCC,
            QuadHelix,
            ArcLingual,
            Disjoncteur,
            GoutieresChirurgical,
            GoutiereBAS,
            PEI,
            ASI,
            Goutiere_Invisalign,
            undefined
        }

        public enum TypeTraitement
        {
            RCC,
            MultiBague,
            Invisalign,
            Chirurgie,
            undefined
        }



        public enum en_FormeArcade
        {
            U,
            V,
            undefined
        }

        public enum en_Respiration
        {
            undefined,
            buccale,
            exclusive,
            Buccaleetexclusive


        }

        public enum en_OuiNon
        {
            Oui,
            Non,
            undefined
        }

        public enum en_InterpositionLingual
        {
            undefined,
            Normal,
            posterieur,
            anterieur,
            AnterieurEtPosterieur
        }

        public enum en_EtageInf
        {
            undefined,
            Augmentation,
            Normal,
            Diminution,
            Effondrement
        }

        public enum en_Sourire
        {
            undefined,
            Gingival,
            Labial,
            Normal
        }

        public enum en_OccFace
        {
            undefined,
            Supraclusion,
            Infraclusion,
            Normal
        }

        public enum en_SourireDentaire
        {
            undefined,
            Etroit,
            Normal,
            Large
        }

        public enum en_BoiteALangue
        {
            undefined,
            Etroite,
            Convenable
        }

        public enum en_OccInverse
        {
            undefined,
            Aucun,
            Droite,
            Gauche,
            Anterieur,
            Droite_Et_Gauche
        }

        public enum en_Laterodeviation
        {
            undefined,
            Aucun,
            Droite,
            Gauche,
            Fonctionnel,
            RCetOIM,
            Droite_Et_Gauche,
            Fonctionnel_Et_RCetOIM

        }

        public enum en_DiagOsseux
        {
            undefined,
            Endognatie,
            Normognatie,
            Exognatie
        }

        public enum en_DiagAlveolaire
        {
            undefined,
            Endoalveolie,
            Normoalveolie,
            Exoalveolie
        }

        public enum en_TypeTriangleNoirLateral
        {
            undefined,
            Triangle_noir_lateral_1,
            Triangle_noir_lateral_2,
            Triangle_noir_lateral_3,
            Triangle_noir_lateral_4
        }

        public enum en_TriangleNoirLateral
        {
            undefined,
            Droit,
            Gauche,
            Droit_Et_Gauche,
            Aucun
        }

        public enum en_TriangleNoirLateralType
        {
            undefined = 0,
            TNL1 = 1,
            TNL2 = 2,
            TNL3 = 3
        }

        public enum en_ProRetro
        {
            undefined,
            Pro,
            Retro,
            Normo
        }

        public enum en_Divergence
        {
            undefined,
            Hypodivergent,
            Hyperdivergent,
            Normodivergent
        }

        public enum en_Class
        {
            undefined,
            Class_I,
            Class_II,
            Class_III
        }

        public enum en_Traitement_EI
        {
            undefined,
            Rien,
            Egression,
            Ingression
        }

        public enum en_Traitement_AR
        {
            undefined,
            Rien,
            Avancer,
            Reculer
        }

        public enum en_Traitement_LR
        {
            undefined,
            Rien,
            Lente,
            Rapide
        }

        public enum en_SPP
        {
            normale,
            hyperdivergence,
            hypodivergence
        }

        #endregion

        public enum RNO
        {
            None,
            R,
            HR
        }


        private int _NumDate;
        public int NumDate
        {
            get
            {
                return _NumDate;
            }
            set
            {
                _NumDate = value;
            }
        }

        public enum TypeDeTraitement
        {
            Debut,
            Semestre,
            Surveillance,
            Contention,
            Autre
        }


        private Utilisateur _Praticien;
        [PropertyCanBeSerialized]
        public Utilisateur Praticien
        {
            get
            {
                return _Praticien;
            }
            set
            {
                _Praticien = value;
            }
        }

        private int _IdModele = -1;
        public int IdModele
        {
            get
            {
                return _IdModele;
            }
            set
            {
                _IdModele = value;
            }
        }


        private int _IdDiag = -1;
        public int IdDiag
        {
            get
            {
                return _IdDiag;
            }
            set
            {
                _IdDiag = value;
            }
        }

        private PyxVitalWrapperConst.CodeAccordDEP _CodeAccordDEP = PyxVitalWrapperConst.CodeAccordDEP.Ac0;
        [PropertyCanBeSerialized]
        public PyxVitalWrapperConst.CodeAccordDEP CodeAccordDEP
        {
            get
            {
                return _CodeAccordDEP;
            }
            set
            {
                _CodeAccordDEP = value;
            }
        }

        #region Recto


        private int _idPatient;
        public int idPatient
        {
            get
            {
                if (patient != null) _idPatient = patient.Id;
                return _idPatient;
            }
            set
            {
                _idPatient = value;
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

        #region Partie reserve au chirurgien dentiste traitant
        private RNO _ReferenceNationalOpposable;
        [PropertyCanBeSerialized]
        public RNO ReferenceNationalOpposable
        {
            get
            {
                return _ReferenceNationalOpposable;
            }
            set
            {
                _ReferenceNationalOpposable = value;
            }
        }

        private DateTime? _DateAccord;
        [PropertyCanBeSerialized]
        public DateTime? DateAccord
        {
            get
            {
                return _DateAccord;
            }
            set
            {
                _DateAccord = value;
            }
        }

        private DateTime? _DateImpression;
        [PropertyCanBeSerialized]
        public DateTime? DateImpression
        {
            get
            {
                return _DateImpression;
            }
            set
            {
                _DateImpression = value;
            }
        }

        private DateTime _dateProposition;
        [PropertyCanBeSerialized]
        public DateTime dateProposition
        {
            get
            {
                return _dateProposition;
            }
            set
            {
                _dateProposition = value;
            }
        }

        private DateTime _DateDebutTraitement;
        [PropertyCanBeSerialized]
        public DateTime DateDebutTraitement
        {
            get
            {
                return _DateDebutTraitement;
            }
            set
            {
                _DateDebutTraitement = value;
            }
        }

        private string _cotationDesActes = "";
        [PropertyCanBeSerialized]
        public string cotationDesActes
        {
            get
            {
                return _cotationDesActes;
            }
            set
            {
                _cotationDesActes = value;
            }
        }

        private bool _IsDevisSigned;
        [PropertyCanBeSerialized]
        public bool IsDevisSigned
        {
            get
            {
                return _IsDevisSigned;
            }
            set
            {
                _IsDevisSigned = value;
            }
        }
        #endregion
        #region Partie patient
        public string NomPrenom
        {
            get
            {
                return patient.Nom + " " + patient.Prenom;
            }

        }




        #endregion
        #region Partie Renseignements medicaux

        private TypeDeTraitement _typetraitement;
        [PropertyCanBeSerialized]
        public TypeDeTraitement typetraitement
        {
            get
            {
                return _typetraitement;
            }
            set
            {
                _typetraitement = value;
            }
        }

        private int _Semestre = 0;
        [PropertyCanBeSerialized]
        public int Semestre
        {
            get
            {
                return _Semestre;
            }
            set
            {
                _Semestre = value;
            }
        }

        private int _Contention = 0;
        [PropertyCanBeSerialized]
        public int Contention
        {
            get
            {
                return _Contention;
            }
            set
            {
                _Contention = value;
            }
        }

        private string _Autre = "";
        [PropertyCanBeSerialized]
        public string Autre
        {
            get
            {
                return _Autre;
            }
            set
            {
                _Autre = value;
            }
        }
        #endregion
        #region Partie Renseignements medicaux (Diagnostic)

        private en_ProRetro _SensSagittalBasalMax;
        [PropertyCanBeSerialized]
        public en_ProRetro SensSagittalBasalMax
        {
            get
            {
                return _SensSagittalBasalMax;
            }
            set
            {
                _SensSagittalBasalMax = value;
            }
        }

        private en_ProRetro _SensSagittalBasalMand;
        [PropertyCanBeSerialized]
        public en_ProRetro SensSagittalBasalMand
        {
            get
            {
                return _SensSagittalBasalMand;
            }
            set
            {
                _SensSagittalBasalMand = value;
            }
        }

        private en_ProRetro _SensSagittalAlveolaireMax;
        [PropertyCanBeSerialized]
        public en_ProRetro SensSagittalAlveolaireMax
        {
            get
            {
                return _SensSagittalAlveolaireMax;
            }
            set
            {
                _SensSagittalAlveolaireMax = value;
            }
        }

        private en_ProRetro _SensSagittalAlveolaireMand;
        [PropertyCanBeSerialized]
        public en_ProRetro SensSagittalAlveolaireMand
        {
            get
            {
                return _SensSagittalAlveolaireMand;
            }
            set
            {
                _SensSagittalAlveolaireMand = value;
            }
        }

        private en_DiagOsseux _SensTransversalBasalMax;
        [PropertyCanBeSerialized]
        public en_DiagOsseux SensTransversalBasalMax
        {
            get
            {
                return _SensTransversalBasalMax;
            }
            set
            {
                _SensTransversalBasalMax = value;
            }
        }

        private en_DiagOsseux _SensTransversalBasalMand;
        [PropertyCanBeSerialized]
        public en_DiagOsseux SensTransversalBasalMand
        {
            get
            {
                return _SensTransversalBasalMand;
            }
            set
            {
                _SensTransversalBasalMand = value;
            }
        }

        private en_DiagAlveolaire _SensTransversalAlveolaireMax;
        [PropertyCanBeSerialized]
        public en_DiagAlveolaire SensTransversalAlveolaireMax
        {
            get
            {
                return _SensTransversalAlveolaireMax;
            }
            set
            {
                _SensTransversalAlveolaireMax = value;
            }
        }

        private en_DiagAlveolaire _SensTransversalAlveolaireMand;
        [PropertyCanBeSerialized]
        public en_DiagAlveolaire SensTransversalAlveolaireMand
        {
            get
            {
                return _SensTransversalAlveolaireMand;
            }
            set
            {
                _SensTransversalAlveolaireMand = value;
            }
        }

        private en_Divergence _SensVerticalBasal;
        [PropertyCanBeSerialized]
        public en_Divergence SensVerticalBasal
        {
            get
            {
                return _SensVerticalBasal;
            }
            set
            {
                _SensVerticalBasal = value;
            }
        }

        private en_OccFace _SensVerticalAlveolaire;
        [PropertyCanBeSerialized]
        public en_OccFace SensVerticalAlveolaire
        {
            get
            {
                return _SensVerticalAlveolaire;
            }
            set
            {
                _SensVerticalAlveolaire = value;
            }
        }

        private en_Class _ClasseDentaireMolaire;
        [PropertyCanBeSerialized]
        public en_Class ClasseDentaireMolaire
        {
            get
            {
                return _ClasseDentaireMolaire;
            }
            set
            {
                _ClasseDentaireMolaire = value;
            }
        }

        private en_Class _ClasseDentaireCanine;
        [PropertyCanBeSerialized]
        public en_Class ClasseDentaireCanine
        {
            get
            {
                return _ClasseDentaireCanine;
            }
            set
            {
                _ClasseDentaireCanine = value;
            }
        }

        private string _ClasseDentaireCanineTxt = "";
        [PropertyCanBeSerialized]
        public string ClasseDentaireCanineTxt
        {
            get
            {
                return _ClasseDentaireCanineTxt;
            }
            set
            {
                _ClasseDentaireCanineTxt = value;
            }
        }

        private string _ClasseDentaireMolaireTxt = "";
        [PropertyCanBeSerialized]
        public string ClasseDentaireMolaireTxt
        {
            get
            {
                return _ClasseDentaireMolaireTxt;
            }
            set
            {
                _ClasseDentaireMolaireTxt = value;
            }
        }

        private bool _DDD;
        [PropertyCanBeSerialized]
        public bool DDD
        {
            get
            {
                return _DDD;
            }
            set
            {
                _DDD = value;
            }
        }

        private bool _DDM;
        [PropertyCanBeSerialized]
        public bool DDM
        {
            get
            {
                return _DDM;
            }
            set
            {
                _DDM = value;
            }
        }

        private string _Agenesie;
        [PropertyCanBeSerialized]
        public string Agenesie
        {
            get
            {
                return _Agenesie;
            }
            set
            {
                _Agenesie = value;
            }
        }

        private string _DentsIncluseSurnum;
        [PropertyCanBeSerialized]
        public string DentsIncluseSurnum
        {
            get
            {
                return _DentsIncluseSurnum;
            }
            set
            {
                _DentsIncluseSurnum = value;
            }
        }

        private string _Malposition;
        [PropertyCanBeSerialized]
        public string Malposition
        {
            get
            {
                return _Malposition;
            }
            set
            {
                _Malposition = value;
            }
        }

        private en_OccInverse _occInverse;
        [PropertyCanBeSerialized]
        public en_OccInverse occInverse
        {
            get
            {
                return _occInverse;
            }
            set
            {
                _occInverse = value;
            }
        }
        private en_Laterodeviation _laterodeviationF;
        [PropertyCanBeSerialized]
        public en_Laterodeviation laterodeviationF
        {
            get
            {
                return _laterodeviationF;
            }
            set
            {
                _laterodeviationF = value;
            }
        }

        private en_InterpositionLingual _interposition;
        [PropertyCanBeSerialized]
        public en_InterpositionLingual interposition
        {
            get
            {
                return _interposition;
            }
            set
            {
                _interposition = value;
            }
        }

        private en_Respiration _formerespiration;
        [PropertyCanBeSerialized]
        public en_Respiration formerespiration
        {
            get
            {
                return _formerespiration;
            }
            set
            {
                _formerespiration = value;
            }
        }

        private string _FacteurFonctionnel;
        [PropertyCanBeSerialized]
        public string FacteurFonctionnel
        {
            get
            {
                return _FacteurFonctionnel;
            }
            set
            {
                _FacteurFonctionnel = value;
            }
        }

        private string _PlanDeTraitement = "";
        [PropertyCanBeSerialized]
        public string PlanDeTraitement
        {
            get
            {
                return _PlanDeTraitement;
            }
            set
            {
                _PlanDeTraitement = value;
            }
        }

        private string _Commentaires = "";
        [PropertyCanBeSerialized]
        public string Commentaires
        {
            get
            {
                return _Commentaires;
            }
            set
            {
                _Commentaires = value;
            }
        }
        #endregion
        #endregion

        #region Verso
        #region Assure

        #region Administratif de l'assure
        private string _AdresseAssure = "";
        [PropertyCanBeSerialized]
        public string AdresseAssure
        {
            get
            {
                return _AdresseAssure;
            }
            set
            {
                _AdresseAssure = value;
            }
        }

        private string _NomPrenomAssure = "";
        [PropertyCanBeSerialized]
        public string NomPrenomAssure
        {
            get
            {
                return _NomPrenomAssure;
            }
            set
            {
                _NomPrenomAssure = value;
            }
        }

        private DateTime _DatenaissanceAssure;
        [PropertyCanBeSerialized]
        public DateTime DatenaissanceAssure
        {
            get
            {
                return _DatenaissanceAssure;
            }
            set
            {
                _DatenaissanceAssure = value;
            }
        }

        private string _ImmatAssure = "";
        [PropertyCanBeSerialized]
        public string ImmatAssure
        {
            get
            {
                return _ImmatAssure;
            }
            set
            {
                _ImmatAssure = value;
            }
        }

        #endregion

        #region Situation de l'assure

        private string _LibAutreCas;
        [PropertyCanBeSerialized]
        public string LibAutreCas
        {
            get
            {
                return _LibAutreCas;
            }
            set
            {
                _LibAutreCas = value;
            }
        }

        private bool _AutreCas;
        [PropertyCanBeSerialized]
        public bool AutreCas
        {
            get
            {
                return _AutreCas;
            }
            set
            {
                _AutreCas = value;
            }
        }

        private bool _PensionAssure;
        [PropertyCanBeSerialized]
        public bool PensionAssure
        {
            get
            {
                return _PensionAssure;
            }
            set
            {
                _PensionAssure = value;
            }
        }

        private bool _PensionPatient;
        [PropertyCanBeSerialized]
        public bool PensionPatient
        {
            get
            {
                return _PensionPatient;
            }
            set
            {
                _PensionPatient = value;
            }
        }

        private DateTime _DateDeCessationActivite;
        [PropertyCanBeSerialized]
        public DateTime DateDeCessationActivite
        {
            get
            {
                return _DateDeCessationActivite;
            }
            set
            {
                _DateDeCessationActivite = value;
            }
        }

        private bool _SansEmplois;
        [PropertyCanBeSerialized]
        public bool SansEmplois
        {
            get
            {
                return _SansEmplois;
            }
            set
            {
                _SansEmplois = value;
            }
        }

        private bool _NonSalarie;
        [PropertyCanBeSerialized]
        public bool NonSalarie
        {
            get
            {
                return _NonSalarie;
            }
            set
            {
                _NonSalarie = value;
            }
        }

        private bool _Salarie;
        [PropertyCanBeSerialized]
        public bool Salarie
        {
            get
            {
                return _Salarie;
            }
            set
            {
                _Salarie = value;
            }
        }
        #endregion

        #endregion

        #region Patient

        #region Patient
        private string _Profession;
        [PropertyCanBeSerialized]
        public string Profession
        {
            get
            {
                return _Profession;
            }
            set
            {
                _Profession = value;
            }
        }

        private DateTime _DateAccident;
        [PropertyCanBeSerialized]
        public DateTime DateAccident
        {
            get
            {
                return _DateAccident;
            }
            set
            {
                _DateAccident = value;
            }
        }

        private bool _Accident;
        [PropertyCanBeSerialized]
        public bool Accident
        {
            get
            {
                return _Accident;
            }
            set
            {
                _Accident = value;
            }
        }

        private bool _SoinPourPensionne;
        [PropertyCanBeSerialized]
        public bool SoinPourPensionne
        {
            get
            {
                return _SoinPourPensionne;
            }
            set
            {
                _SoinPourPensionne = value;
            }
        }

        #endregion

        #region Si Patient <> Assure
        public string NomPatient
        {
            get
            {
                return patient.Nom;
            }
            set
            {
                patient.Nom = value;
            }
        }

        public string PrenomPatient
        {
            get
            {
                return patient.Prenom;
            }
            set
            {
                patient.Prenom = value;
            }
        }

        public DateTime DateNaissancePatient
        {
            get
            {
                return patient.DateNaissance;
            }
            set
            {
                patient.DateNaissance = value;
            }
        }

        private bool _Pensionne;
        public bool Pensionne
        {
            get
            {
                return _Pensionne;
            }
            set
            {
                _Pensionne = value;
            }
        }

        private string _AdressePatient;
        [PropertyCanBeSerialized]
        public string AdressePatient
        {
            get
            {
                return _AdressePatient;
            }
            set
            {
                _AdressePatient = value;
            }
        }
        #endregion

        #endregion

        #endregion
    }
}
