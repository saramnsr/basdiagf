using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class PropositionObjectForLetters
    {

        private double _Honoraires;
        public double Honoraires
        {
            get
            {
                return _Honoraires;
            }
            set
            {
                _Honoraires = value;
            }
        }

        private double _TarifParMois;
        public double TarifParMois
        {
            get
            {
                return _TarifParMois;
            }
            set
            {
                _TarifParMois = value;
            }
        }

        private int _NbMois;
        public int NbMois
        {
            get
            {
                return _NbMois;
            }
            set
            {
                _NbMois = value;
            }
        }

        private double _PartSecu;
        public double PartSecu
        {
            get
            {
                return _PartSecu;
            }
            set
            {
                _PartSecu = value;
            }
        }

        private double _PartMutuelle;
        public double PartMutuelle
        {
            get
            {
                return _PartMutuelle;
            }
            set
            {
                _PartMutuelle = value;
            }
        }

        private string _LibSecu;
        public string LibSecu
        {
            get
            {
                return _LibSecu;
            }
            set
            {
                _LibSecu = value;
            }
        }

        private string _CodeTraitement;
        public string CodeTraitement
        {
            get
            {
                return _CodeTraitement;
            }
            set
            {
                _CodeTraitement = value;
            }
        }
        
    }
}
