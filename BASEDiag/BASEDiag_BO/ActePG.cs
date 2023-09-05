using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEDiag_BO
{
    public class ActePG
    {
        public string DisplayCodeNVal
        {
            get
            {
                if (Coeff == 1)
                    return prestation.Code;
                if (IsDecomposed)
                    return prestation.Code + CoeffDecompose;
                else
                    return prestation.Code + Coeff;

            }

        }



        private int _NumContention = -1;
        public int NumContention
        {
            get
            {
                return _NumContention;
            }
            set
            {
                _NumContention = value;
            }
        }

        private int _NumSemestre = -1;
        public int NumSemestre
        {
            get
            {
                return _NumSemestre;
            }
            set
            {
                _NumSemestre = value;
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

        private int _IdPatient = -1;
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

        private int _IdPlan;
        public int IdPlan
        {
            get
            {
                return _IdPlan;
            }
            set
            {
                _IdPlan = value;
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

        private CodePrestation _prestation;
        public CodePrestation prestation
        {
            get
            {
                return _prestation;
            }
            set
            {
                _prestation = value;
            }
        }

        private TemplateActePG _Template;
        public TemplateActePG Template
        {
            get
            {
                return _Template;
            }
            set
            {
                _Template = value;
            }
        }

        /*
        private FeuilleDeSoin _FeuilleDeSoinAssocier = null;
        public FeuilleDeSoin FeuilleDeSoinAssocier
        {
            get
            {
                return _FeuilleDeSoinAssocier;
            }
            set
            {
                _FeuilleDeSoinAssocier = value;
            }
        }*/

        private EntentePrealable _DEPAssocier = null;
        public EntentePrealable DEPAssocier
        {
            get
            {
                return _DEPAssocier;
            }
            set
            {
                _DEPAssocier = value;
                _Id_DEP = value.IdModele;
            }
        }

        private int? _NbMois;
        public int? NbMois
        {
            get
            {
                return _NbMois;
            }
            set
            {
                _NbMois = value;
            }
        }

        private int? _NbJours;
        public int? NbJours
        {
            get
            {
                return _NbJours;
            }
            set
            {
                _NbJours = value;
            }
        }

        private int _CodePlan;
        public int CodePlan
        {
            get
            {
                return _CodePlan;
            }
            set
            {
                _CodePlan = value;
            }
        }

        private double _Montant_Honoraire;
        public double Montant_Honoraire
        {
            get
            {
                return _Montant_Honoraire;
            }
            set
            {
                _Montant_Honoraire = value;
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


        private int _IdSemestrePlanTraitementAssocie = -1;
        public int IdSemestrePlanTraitementAssocie
        {
            get
            {
                return _IdSemestrePlanTraitementAssocie;
            }
            set
            {
                _IdSemestrePlanTraitementAssocie = value;
            }
        }

        private DateTime _DateExecution;
        public DateTime DateExecution
        {
            get
            {
                return _DateExecution;
            }
            set
            {
                _DateExecution = value;
            }
        }

        private Patient _patient;
        public Patient patient
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

        private bool _NeedDEP = false;
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

        private bool _NeedFSE = false;
        public bool NeedFSE
        {
            get
            {
                return _NeedFSE;
            }
            set
            {
                _NeedFSE = value;
            }
        }

        #region for FSE



        private PyxVitalWrapperConst.CodeAccordDEP _AccordDEP = PyxVitalWrapperConst.CodeAccordDEP.Ac0;
        public PyxVitalWrapperConst.CodeAccordDEP AccordDEP
        {
            get
            {
                return _AccordDEP;
            }
            set
            {
                _AccordDEP = value;
            }
        }

        private PyxVitalWrapperConst.Domicile _domicile = PyxVitalWrapperConst.Domicile.N;
        public PyxVitalWrapperConst.Domicile domicile
        {
            get
            {
                return _domicile;
            }
            set
            {
                _domicile = value;
            }
        }

        private PyxVitalWrapperConst.Qualificatif_depense _motifdepassement = PyxVitalWrapperConst.Qualificatif_depense.Néant;
        public PyxVitalWrapperConst.Qualificatif_depense motifdepassement
        {
            get
            {
                return _motifdepassement;
            }
            set
            {
                _motifdepassement = value;
            }
        }

        private int _Quantite;
        public int Quantite
        {
            get
            {
                return _Quantite;
            }
            set
            {
                _Quantite = value;
            }
        }

        private PyxVitalWrapperConst.RembExceptionel _rembExceptionel = PyxVitalWrapperConst.RembExceptionel.N;
        public PyxVitalWrapperConst.RembExceptionel rembExceptionel
        {
            get
            {
                return _rembExceptionel;
            }
            set
            {
                _rembExceptionel = value;
            }
        }

        private PyxVitalWrapperConst.SuplCharge _suplCharge = PyxVitalWrapperConst.SuplCharge.N;
        public PyxVitalWrapperConst.SuplCharge suplCharge
        {
            get
            {
                return _suplCharge;
            }
            set
            {
                _suplCharge = value;
            }
        }

        private PyxVitalWrapperConst.RNO _rno = PyxVitalWrapperConst.RNO.Néant;
        public PyxVitalWrapperConst.RNO rno
        {
            get
            {
                return _rno;
            }
            set
            {
                _rno = value;
            }
        }

        private PyxVitalWrapperConst.Nuit _nuit = PyxVitalWrapperConst.Nuit.N;
        public PyxVitalWrapperConst.Nuit nuit
        {
            get
            {
                return _nuit;
            }
            set
            {
                _nuit = value;
            }
        }

        private PyxVitalWrapperConst.Urgence _urgence = PyxVitalWrapperConst.Urgence.N;
        public PyxVitalWrapperConst.Urgence urgence
        {
            get
            {
                return _urgence;
            }
            set
            {
                _urgence = value;
            }
        }

        private PyxVitalWrapperConst.DimancheEtJF _DimancheEtJF = PyxVitalWrapperConst.DimancheEtJF.N;
        public PyxVitalWrapperConst.DimancheEtJF DimancheEtJF
        {
            get
            {
                return _DimancheEtJF;
            }
            set
            {
                _DimancheEtJF = value;
            }
        }

        private PyxVitalWrapperConst.ALD _ald = PyxVitalWrapperConst.ALD.N;
        public PyxVitalWrapperConst.ALD ald
        {
            get
            {
                return _ald;
            }
            set
            {
                _ald = value;
            }
        }

        private PyxVitalWrapperConst.Exoneration _Exoneration = PyxVitalWrapperConst.Exoneration.ExNéant;
        public PyxVitalWrapperConst.Exoneration Exoneration
        {
            get
            {
                return _Exoneration;
            }
            set
            {
                _Exoneration = value;
            }
        }

        private string _ExonerationLibOuTx = "";
        public string ExonerationLibOuTx
        {
            get
            {
                return _ExonerationLibOuTx;
            }
            set
            {
                _ExonerationLibOuTx = value;
            }
        }

        /*
        private int _Id_FS;
        public int Id_FS
        {
            get
            {
                if (FeuilleDeSoinAssocier != null) _Id_FS = FeuilleDeSoinAssocier.Id;
                return _Id_FS;
            }
            set
            {
                _Id_FS = value;
            }
        }
        */

        #region DEP


        private int _Id_DEP = -1;
        public int Id_DEP
        {
            get
            {
                if (DEPAssocier != null) _Id_DEP = DEPAssocier.IdModele;
                return _Id_DEP;
            }
            set
            {
                _Id_DEP = value;
            }
        }

        public PyxVitalWrapperConst.DEP AssocierDEP
        {
            get
            {
                if (_Id_DEP > -1)
                    return PyxVitalWrapperConst.DEP.O;
                else
                    return PyxVitalWrapperConst.DEP.N;
            }

        }




        #endregion

        #region accident
        private DateTime? _DateAccident = null;
        public DateTime? DateAccident
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

        private PyxVitalWrapperConst.Accident _accident = PyxVitalWrapperConst.Accident.N;
        public PyxVitalWrapperConst.Accident accident
        {
            get
            {
                return _accident;
            }
            set
            {
                _accident = value;
            }
        }
        #endregion

        private string _numdent;
        public string numdent
        {
            get
            {
                return _numdent;
            }
            set
            {
                _numdent = value;
            }
        }



        #endregion
    }
}
