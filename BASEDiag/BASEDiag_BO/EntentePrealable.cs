using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BASEDiag_BO;
using BasCommon_BO;

namespace BASEDiag_BO
{
    public class EntentePrealable
    {
        public enum RNO
        {
            None,
            R,
            HR
        }

        public enum TypeDeTraitement
        {
            Debut,
            Semestre,
            Surveillance,
            Contention,
            Autre
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
        /// <summary>
        /// Si IdDiag vaut -1 alors Pas de diag
        /// Si IdDiag vaut 0 alors un diag à été précréé par Orthalis est peut etre ecrasé
        /// </summary>
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


        #region Recto

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

        private Utilisateur _Praticien;
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

        private DateTime? _DateAccord;
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

        private ResumeClinique.en_ProRetro _SensSagittalBasalMax;
        public ResumeClinique.en_ProRetro SensSagittalBasalMax
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

        private ResumeClinique.en_ProRetro _SensSagittalBasalMand;
        public ResumeClinique.en_ProRetro SensSagittalBasalMand
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

        private ResumeClinique.en_ProRetro _SensSagittalAlveolaireMax;
        public ResumeClinique.en_ProRetro SensSagittalAlveolaireMax
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

        private ResumeClinique.en_ProRetro _SensSagittalAlveolaireMand;
        public ResumeClinique.en_ProRetro SensSagittalAlveolaireMand
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
        
        private ResumeClinique.en_DiagOsseux _SensTransversalBasalMax;
        public ResumeClinique.en_DiagOsseux SensTransversalBasalMax
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

        private ResumeClinique.en_DiagOsseux _SensTransversalBasalMand;
        public ResumeClinique.en_DiagOsseux SensTransversalBasalMand
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

        private ResumeClinique.en_DiagAlveolaire _SensTransversalAlveolaireMax;
        public ResumeClinique.en_DiagAlveolaire SensTransversalAlveolaireMax
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

        private ResumeClinique.en_DiagAlveolaire _SensTransversalAlveolaireMand;
        public ResumeClinique.en_DiagAlveolaire SensTransversalAlveolaireMand
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
        
        private ResumeClinique.en_Divergence _SensVerticalBasal;
        public ResumeClinique.en_Divergence SensVerticalBasal
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

        private ResumeClinique.en_OccFace _SensVerticalAlveolaire;
        public ResumeClinique.en_OccFace SensVerticalAlveolaire
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
        
        private ResumeClinique.en_Class _ClasseDentaireMolaire;
        public ResumeClinique.en_Class ClasseDentaireMolaire
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

        private ResumeClinique.en_Class _ClasseDentaireCanine;
        public ResumeClinique.en_Class ClasseDentaireCanine
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

        private ResumeClinique.en_OccInverse _occInverse;
        public ResumeClinique.en_OccInverse occInverse
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

        private string _FacteurFonctionnel;
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
