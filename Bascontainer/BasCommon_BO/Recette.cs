using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Recette
    {

        public  const string CODEREGLEMENTPATIENT = "REGLEMENT";//706000
        public  const string CODEPERTEETPROFIT = "PERTEETPROFIT";//758000
        public  const string CODEREGUL = "REGUL";//471000
        

        private BordereauFinance _bordereau = null;
        public BordereauFinance bordereau
        {
            get
            {
                return _bordereau;
            }
            set
            {
                _bordereau = value;
            }
        }

        private int _IDBordereau = -1;
        public int IDBordereau
        {
            get
            {
                if (bordereau != null) _IDBordereau = bordereau.Id;
                return _IDBordereau;
            }
            set
            {
                _IDBordereau = value;
            }
        }

        private string _Details;
        public string Details
        {
            get
            {
                return _Details;
            }
            set
            {
                _Details = value;
            }
        }

        private BanqueDeRemise _banqueDeRemise;
        public BanqueDeRemise banqueDeRemise
        {
            get
            {
                return _banqueDeRemise;
            }
            set
            {
                _banqueDeRemise = value;
            }
        }


        private int _IDPaiementReel = -1;
        public int IDPaiementReel
        {
            get
            {
                if (PaiementReel != null) _IDPaiementReel = PaiementReel.Id;
                return _IDPaiementReel;
            }
            set
            {
                _IDPaiementReel = value;
            }
        }

        private PaiementReel _PaiementReel = null;
        public PaiementReel PaiementReel
        {
            get
            {
                return _PaiementReel;
            }
            set
            {
                _PaiementReel = value;
            }
        }

        private double _Montant;
        public double Montant
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

        private string _Code;
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
            }
        }

        private DateTime _DateValeurBque;
        public DateTime DateValeurBque
        {
            get
            {
                return _DateValeurBque;
            }
            set
            {
                _DateValeurBque = value;
            }
        }

        private DateTime _DateRemiseEnBanque   ;
        public DateTime DateRemiseEnBanque
        {
            get
            {
                return _DateRemiseEnBanque;
            }
            set
            {
                _DateRemiseEnBanque = value;
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
