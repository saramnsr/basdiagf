using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Pays
    {

        public override string ToString()
        {
            return indicatif +"(" + shortName + ")";
        }

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
        private string _indicatif;
        public string indicatif
        {
            get
            {
                return _indicatif;
            }
            set
            {
                _indicatif = value;
            }

        }
        private int _ordre;
        public int ordre
        {
            get
            {
                return _ordre;
            }
            set
            {
                _ordre = value;
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
        private string _shortName;
        public string shortName
        {
            get
            {
                return _shortName;
            }
            set
            {
                _shortName = value;
            }

        }
       
     

    }
}
