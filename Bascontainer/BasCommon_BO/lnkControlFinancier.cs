using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class lnkControlFinancier
    {



       

        public enum EtatControl
        {
            Inconnue = -1,
            Erreur = 0,
            Valide = 1,
            Warning = 2

        }

        private EtatControl _Etat;
        public EtatControl Etat
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

        private DateTime _UpdateDate;
        public DateTime UpdateDate
        {
            get
            {
                return _UpdateDate;
            }
            set
            {
                _UpdateDate = value;
            }
        }

        private string _CodeErreur;
        public string CodeErreur
        {
            get
            {
                return _CodeErreur;
            }
            set
            {
                _CodeErreur = value;
            }
        }

        private string _Remarques;
        public string Remarques
        {
            get
            {
                return _Remarques;
            }
            set
            {
                _Remarques = value;
            }
        }


        private ControlFinancier _controlFinancier = null;
        public ControlFinancier controlFinancier
        {
            get
            {
                return _controlFinancier;
            }
            set
            {
                _controlFinancier = value;
            }
        }

        private int _IdControlFinancier;
        public int IdControlFinancier
        {
            get
            {
                if (controlFinancier != null) _IdControlFinancier = controlFinancier.Id;
                return _IdControlFinancier;
            }
            set
            {
                _IdControlFinancier = value;
            }
        }

        private Echeance _Echeance = null;
        public Echeance Echeance
        {
            get
            {
                return _Echeance;
            }
            set
            {
                _Echeance = value;
            }
        }


        private PaiementReel _paiementReel = null;
        public PaiementReel paiementReel
        {
            get
            {
                return _paiementReel;
            }
            set
            {
                _paiementReel = value;
            }
        }


        private int _IdEcheance = -1;
        public int IdEcheance
        {
            get
            {
                if (Echeance != null)
                    _IdEcheance = Echeance.Id;
                return _IdEcheance;
            }
            set
            {
                _IdEcheance = value;
            }
        }

        private int _IdPaiementReel = -1;
        public int IdPaiementReel
        {
            get
            {
                if (paiementReel != null)
                    _IdPaiementReel = paiementReel.Id;
                return _IdPaiementReel;
            }
            set
            {
                _IdPaiementReel = value;
            }
        }
    }
}
