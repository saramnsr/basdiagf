using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class BordereauFinance
    {

        public int Nbbillets
        {
            get
            {
                return Nbbillets10 + Nbbillets100 + Nbbillets20 + Nbbillets200 + Nbbillets5 + Nbbillets50 + Nbbillets500;
            }

        }

        private DateTime? _DateValeur = null;
        public DateTime? DateValeur
        {
            get
            {
                return _DateValeur;
            }
            set
            {
                _DateValeur = value;
            }
        }

        private DateTime? _DateRemise = DateTime.Now;
        public DateTime? DateRemise
        {
            get
            {
                return _DateRemise;
            }
            set
            {
                _DateRemise = value;
            }
        }

        public double Total
        {
            get
            {
                double ttl = 0;

                foreach (PaiementReel pr in paiements)
                    ttl += pr.Montant;

                return ttl;
            }
           
        }

        public double TotalEnEspece
        {
            get
            {
                double ttl = 0;

                ttl += 5 * Nbbillets5;
                ttl += 10 * Nbbillets10;
                ttl += 20 * Nbbillets20;
                ttl += 50 * Nbbillets50;
                ttl += 100 * Nbbillets100;
                ttl += 200 * Nbbillets200;
                ttl += 500 * Nbbillets500;


                return ttl;
            }

        }

        private List<PaiementReel> _paiements = null;
        public List<PaiementReel> paiements
        {
            get
            {
                return _paiements;
            }
            set
            {
                _paiements = value;
            }
        }

        private Double _Montant;
        public Double Montant
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


        private int _Nbbillets5=0;
        public int Nbbillets5
        {
            get
            {
                return _Nbbillets5;
            }
            set
            {
                _Nbbillets5 = value;
            }
        }

        private int _Nbbillets10 = 0;
        public int Nbbillets10
        {
            get
            {
                return _Nbbillets10;
            }
            set
            {
                _Nbbillets10 = value;
            }
        }

        private int _Nbbillets20 = 0;
        public int Nbbillets20
        {
            get
            {
                return _Nbbillets20;
            }
            set
            {
                _Nbbillets20 = value;
            }
        }

        private int _Nbbillets50 = 0;
        public int Nbbillets50
        {
            get
            {
                return _Nbbillets50;
            }
            set
            {
                _Nbbillets50 = value;
            }
        }

        private int _Nbbillets100 = 0;
        public int Nbbillets100
        {
            get
            {
                return _Nbbillets100;
            }
            set
            {
                _Nbbillets100 = value;
            }
        }

        private int _Nbbillets200 = 0;
        public int Nbbillets200
        {
            get
            {
                return _Nbbillets200;
            }
            set
            {
                _Nbbillets200 = value;
            }
        }

        private int _Nbbillets500 = 0;
        public int Nbbillets500
        {
            get
            {
                return _Nbbillets500;
            }
            set
            {
                _Nbbillets500 = value;
            }
        }



        private int _NbVirements = 0;
        public int NbVirements
        {
            get
            {
                return _NbVirements;
            }
            set
            {
                _NbVirements = value;
            }
        }

        private int _NbPrelevements = 0;
        public int NbPrelevements
        {
            get
            {
                return _NbPrelevements;
            }
            set
            {
                _NbPrelevements = value;
            }
        }

        private int _NbCBs = 0;
        public int NbCBs
        {
            get
            {
                return _NbCBs;
            }
            set
            {
                _NbCBs = value;
            }
        }

        private int _NbCheques = 0;
        public int NbCheques
        {
            get
            {
                return _NbCheques;
            }
            set
            {
                _NbCheques = value;
            }
        }

        private BanqueDeRemise _BanqueDeRemise;
        public BanqueDeRemise BanqueDeRemise
        {
            get
            {
                return _BanqueDeRemise;
            }
            set
            {
                _BanqueDeRemise = value;
            }
        }



        private int _IdControlFinancier = -1;
        public int IdControlFinancier
        {
            get
            {
                if (Controlfinance != null) _IdControlFinancier = Controlfinance.Id;
                return _IdControlFinancier;
            }
            set
            {
                _IdControlFinancier = value;
            }
        }

        private ControlFinancier _Controlfinance = null;
        public ControlFinancier Controlfinance
        {
            get
            {
                return _Controlfinance;
            }
            set
            {
                _Controlfinance = value;
            }
        }

        private string _NumBordereau;
        public string NumBordereau
        {
            get
            {
                return _NumBordereau;
            }
            set
            {
                _NumBordereau = value;
            }
        }

        private string _NumBordereauBancaire;
        public string NumBordereauBancaire
        {
            get
            {
                return _NumBordereauBancaire;
            }
            set
            {
                _NumBordereauBancaire = value;
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
