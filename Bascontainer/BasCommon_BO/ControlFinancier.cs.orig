using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class ControlFinancier
    {

        public enum EtatControl
        {
            Ok = 0,
            EnErreurs = 1,
            EnAttente = 2            
        }

        public double Montant
        {
            get
            {
                double total = 0;

                foreach (lnkControlFinancier pr in lnkctrlPaiement)
                    if (pr.paiementReel == null)
                        throw new System.Exception("Calcul du montant impossible");
                    else
                        total += pr.paiementReel==null?0:pr.paiementReel.Montant;
     
                return total;
            }
            
        }

        private List<lnkControlFinancier> _lnkctrlPaiement = null;
        public List<lnkControlFinancier> lnkctrlPaiement
        {
            get
            {
                return _lnkctrlPaiement;
            }
            set
            {
                _lnkctrlPaiement = value;
            }
        }


        private EtatControl _Etat = EtatControl.EnAttente;
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

        private string _CodeControl;
        public string CodeControl
        {
            get
            {
                return _CodeControl;
            }
            set
            {
                _CodeControl = value;
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


        private int _IdControleur = -1;
        public int IdControleur
        {
            get
            {
                if (ControlerPar != null) _IdControleur = ControlerPar.Id;
                return _IdControleur;
            }
            set
            {
                _IdControleur = value;
            }
        }

        private Utilisateur _ControlerPar = null;
        public Utilisateur ControlerPar
        {
            get
            {
                return _ControlerPar;
            }
            set
            {
                _ControlerPar = value;
            }
        }

        private DateTime _DateControl;
        public DateTime DateControl
        {
            get
            {
                return _DateControl;
            }
            set
            {
                _DateControl = value;
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
