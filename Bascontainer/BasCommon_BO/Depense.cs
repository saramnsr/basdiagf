using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Depense
    {

        public const string CODEFRAISBANCAIRE = "FRAISBANCAIRE";//627000
        public const string CODEPERTEETPROFIT = "PERTEETPROFIT";//658000
        public const string CODEREGUL = "REGUL";//471000
        
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

     

        private string _ModeReglement;
        public string ModeReglement
        {
            get
            {
                return _ModeReglement;
            }
            set
            {
                _ModeReglement = value;
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

        private DateTime _DateDepense;
        public DateTime DateDepense
        {
            get
            {
                return _DateDepense;
            }
            set
            {
                _DateDepense = value;
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
