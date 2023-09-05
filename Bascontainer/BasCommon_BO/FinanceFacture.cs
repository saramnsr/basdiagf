using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class FinanceFacture
    {

        private int _id;
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        private DateTime _dateFacture;
        public DateTime dateFacture
        {
            get
            {
                return _dateFacture;
            }
            set
            {
                _dateFacture = value;
            }
        }
        private double _montant;
        public double montant
        {
            get
            {
                return _montant;
            }
            set
            {
                _montant = value;
            }
        }
        private string _nom;
        public string nom
        {
            get
            {
                return _nom;
            }
            set
            {
                _nom = value;
            }
        }

    }
}
