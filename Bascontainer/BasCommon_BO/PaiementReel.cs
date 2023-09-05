using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class PaiementReel
    {

        public override string ToString()
        {

            switch (typeencaissement)
            {
                case TypeEncaissement.Cheque: return "Cheque n° " + NumCheque + " de " + Montant.ToString("C2") + " pour " + Patients;
                case TypeEncaissement.AMEX: return "AMEX de " + Montant.ToString("C2") + " pour " + Patients;
                case TypeEncaissement.CB: return "CB de " + Montant.ToString("C2") + " pour " + Patients;
                case TypeEncaissement.Especes: return "Especes de " + Montant.ToString("C2") +" pour "+Patients;
                case TypeEncaissement.Optalion: return "PNF de " + Montant.ToString("C2") + " pour " + Patients;
                case TypeEncaissement.Pnf: return "PNF de " + Montant.ToString("C2") + " pour " + Patients;
                case TypeEncaissement.Prelevement: return "Prelevement de " + Montant.ToString("C2") + " pour " + Patients;
                case TypeEncaissement.Virement: return "Virement de " + Montant.ToString("C2") + " pour " + Patients;
                default: return "reglement de " + Montant.ToString("C2") + " pour " + Patients;

            }

            
        }

        private bool _IsInBlackCase = false;
        public bool IsInBlackCase
        {
            get
            {
                return _IsInBlackCase;
            }
            set
            {
                _IsInBlackCase = value;
            }
        }

        private bool _IsPnf = false;
        public bool IsPnf
        {
            get
            {
                return _IsPnf;
            } 
            set
            {
                _IsPnf =value;
            } 
        }

        private DateTime? _DateExecutionRemise;
        public DateTime? DateExecutionRemise
        {
            get
            {
                if (Controls == null) return null;

                switch (this.typeencaissement)
                {

                    case TypeEncaissement.Cheque:
                        {
                            foreach (ControlFinancier cf in Controls)
                            {
                                if (cf.CodeControl == "CHQDUJOUR")
                                    return cf.DateControl;
                            }
                            return null;
                        }
                    default:
                        return DateRemiseEnBanque;
                }

            }
        }

        private List<ControlFinancier> _Controls = null;
        public List<ControlFinancier> Controls
        {
            get
            {
                return _Controls;
            }
            set
            {
                _Controls = value;
            }
        }


        public enum RemisEnBanque
        {
            Oui = 1,
            Non = 0,
            NA = -1
        }

        public enum TypeEncaissement
        {
            Inconnus = -2,
            Tous = -1,
            Cheque = 0,
            Especes = 1,
            CB = 3,
            Optalion = 4,
            Prelevement = 6,
            Virement = 7,
            Pnf = 8,
            AMEX = 9,
            Perte = 10
        }


        public enum statusEncaissement
        {
            None = -1,
            Remis = 0,
            NonSigne = 1,

        }


        private EntiteJuridique _EntiteJuridique;
        [PropertyCanBeSerialized]
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


        private List<int> _lstpatient = null;
        [PropertyCanBeSerialized]
        public List<int> lstpatient
        {
            get
            {
                return _lstpatient;
            }
            set
            {
                _lstpatient = value;
            }
        }

        private string _Patients;
        [PropertyCanBeSerialized]
        public string Patients
        {
            get
            {
                return _Patients;
            }
            set
            {
                _Patients = value;
            }
        }

        private DateTime? _DateEcheance;
        [PropertyCanBeSerialized]
        public DateTime? DateEcheance
        {
            get
            {
                return _DateEcheance;
            }
            set
            {
                _DateEcheance = value;
            }
        }

        private DateTime? _DateValeurBqe = null;
        [PropertyCanBeSerialized]
        public DateTime? DateValeurBqe
        {
            get
            {
                return _DateValeurBqe;
            }
            set
            {
                _DateValeurBqe = value;
            }
        }

        


        private Double _MontantEnBanque;
        [PropertyCanBeSerialized]
        public Double MontantEnBanque
        {
            get
            {
                return _MontantEnBanque;
            }
            set
            {
                _MontantEnBanque = value;
            }
        }

        private double _MontantRemis;
        [PropertyCanBeSerialized]
        public double MontantRemis
        {
            get
            {
                return _MontantRemis;
            }
            set
            {
                _MontantRemis = value;
            }
        }

      
        private statusEncaissement _Status = statusEncaissement.None;
        [PropertyCanBeSerialized]
        public statusEncaissement Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }




        private DateTime? _DateRemiseEnBanque;
        [PropertyCanBeSerialized]
        public DateTime? DateRemiseEnBanque
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

        private RemisEnBanque _EstRemisEnBanque = RemisEnBanque.NA;
        [PropertyCanBeSerialized]
        public RemisEnBanque EstRemisEnBanque
        {
            get
            {
                return _EstRemisEnBanque;
            }
            set
            {
                _EstRemisEnBanque = value;
            }
        }

        private BanqueDeRemise _BanqueDeRemise;
        [PropertyCanBeSerialized]
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

        private Banque _BanqueEmetrice;
        [PropertyCanBeSerialized]
        public Banque BanqueEmetrice
        {
            get
            {
                return _BanqueEmetrice;
            }
            set
            {
                _BanqueEmetrice = value;
            }
        }



        private Mutuelle _Mutuelle;
        [PropertyCanBeSerialized]
        public Mutuelle Mutuelle
        {
            get
            {
                return _Mutuelle;
            }
            set
            {
                _Mutuelle = value;
            }
        }

        private int _IdPayeur = -1;
        [PropertyCanBeSerialized]
        public int IdPayeur
        {
            get
            {
                return _IdPayeur;
            }
            set
            {
                _IdPayeur = value;
            }
        }

        private int _IdPraticien = -1;
        [PropertyCanBeSerialized]
        public int IdPraticien
        {
            get
            {
                return _IdPraticien;
            }
            set
            {
                _IdPraticien = value;
            }
        }

        private string _payeur;
        [PropertyCanBeSerialized]
        public string payeur
        {
            get
            {
                return _payeur;
            }
            set
            {
                _payeur = value;
            }
        }



        private DateTime _DateEncaissement;
        [PropertyCanBeSerialized]
        public DateTime DateEncaissement
        {
            get
            {
                return _DateEncaissement;
            }
            set
            {
                _DateEncaissement = value;
            }
        }



        private TypeEncaissement _typeencaissement;
        [PropertyCanBeSerialized]
        public TypeEncaissement typeencaissement
        {
            get
            {
                return _typeencaissement;
            }
            set
            {
                _typeencaissement = value;
            }
        }



        private double _EspecesMisEncaisse = 0;
        [PropertyCanBeSerialized]
        public double EspecesMisEncaisse
        {
            get
            {
                return _EspecesMisEncaisse;
            }
            set
            {
                _EspecesMisEncaisse = value;
            }
        }

        private double _EspecesRendus = 0;
        [PropertyCanBeSerialized]
        public double EspecesRendus
        {
            get
            {
                return _EspecesRendus;
            }
            set
            {
                _EspecesRendus = value;
            }
        }

        private Double _EspecesRecu;
        [PropertyCanBeSerialized]
        public Double EspecesRecu
        {
            get
            {
                return _EspecesRecu;
            }
            set
            {
                _EspecesRecu = value;
            }
        }

        private Double _Montant;
        [PropertyCanBeSerialized]
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


        private string _NumCheque;
        [PropertyCanBeSerialized]
        public string NumCheque
        {
            get
            {
                return _NumCheque;
            }
            set
            {
                _NumCheque = value;
            }
        }

        private string _Commentaires;
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



    }
}
