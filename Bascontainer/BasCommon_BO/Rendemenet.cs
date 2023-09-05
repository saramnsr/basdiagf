using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Rendemenet
    {
        private DateTime _DateRDV;
        public DateTime DateRDV
        {
            get
            {
                return _DateRDV;
            }
            set
            {
                _DateRDV = value;
            }
        }
        private int _Order;
        public int Order
        {
            get
            {
                return _Order;
            }
            set
            {
                _Order = value;
            }
        }
        private double _Somme;
        public double Somme
        {
            get
            {
                return _Somme;
            }
            set
            {
                _Somme = value;
            }
        }

      
    }
}
