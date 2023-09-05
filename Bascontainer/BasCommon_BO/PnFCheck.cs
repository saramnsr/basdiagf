using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class PnFCheck
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

        private EntiteJuridique _EntiteJuridique;
        public EntiteJuridique EntiteJuridique
        {
            get
            {
                return _EntiteJuridique;
            }
            set
            {
                _EntiteJuridique = value;
            }
        }

        private string _Payeur;
        public string Payeur
        {
            get
            {
                return _Payeur;
            }
            set
            {
                _Payeur = value;
            }
        }

        private string _NomPatient;
        public string NomPatient
        {
            get
            {
                return _NomPatient;
            }
            set
            {
                _NomPatient = value;
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

        private DateTime _Date;
        public DateTime Date
        {
            get
            {
                return _Date;
            }
            set
            {
                _Date = value;
            }
        }

        private string _Type;
        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
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

        private int _IdPartBanque;
        public int IdPartBanque
        {
            get
            {
                if (PartBanque != null) _IdPartBanque = PartBanque.Id;
                return _IdPartBanque;
            }
            set
            {
                _IdPartBanque = value;
            }
        }

        private Echeance _PartBanque;
        public Echeance PartBanque
        {
            get
            {
                return _PartBanque;
            }
            set
            {
                _PartBanque = value;
            }
        }

        private int _IdPartPatient;
        public int IdPartPatient
        {
            get
            {
                if (PartPatient != null) _IdPartPatient = PartPatient.Id;
                return _IdPartPatient;
            }
            set
            {
                _IdPartPatient = value;
            }
        }

        private Echeance _PartPatient;
        public Echeance PartPatient
        {
            get
            {
                return _PartPatient;
            }
            set
            {
                _PartPatient = value;
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
