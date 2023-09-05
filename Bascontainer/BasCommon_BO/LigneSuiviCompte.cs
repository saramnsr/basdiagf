using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class LigneSuiviCompte
    {



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

        private EntiteJuridique _entite;
        public EntiteJuridique entite
        {
            get
            {
                return _entite;
            }
            set
            {
                _entite = value;
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


        private int _IdDepense;
        public int IdDepense
        {
            get
            {
                return _IdDepense;
            }
            set
            {
                _IdDepense = value;
            }
        }

        private PaiementReel _paiement = null;
        public PaiementReel paiement
        {
            get
            {
                return _paiement;
            }
            set
            {
                _paiement = value;
            }
        }

        private int _IdPaiement;
        public int IdPaiement
        {
            get
            {
                return _IdPaiement;
            }
            set
            {
                _IdPaiement = value;
            }
        }

        private double? _Depense;
        public double? Depense
        {
            get
            {
                return _Depense;
            }
            set
            {
                _Depense = value;
            }
        }

        private double? _Recette;
        public double? Recette
        {
            get
            {
                return _Recette;
            }
            set
            {
                _Recette = value;
            }
        }

        private DateTime? _DateBanque;
        public DateTime? DateBanque
        {
            get
            {
                return _DateBanque;
            }
            set
            {
                _DateBanque = value;
            }
        }

        private DateTime? _DateCabinet;
        public DateTime? DateCabinet
        {
            get
            {
                return _DateCabinet;
            }
            set
            {
                _DateCabinet = value;
            }
        }
        
        private int _Id;
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
