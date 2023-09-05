using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class DefinitionCredit
    {

        private int _NombreMensualite;
        public int NombreMensualite
        {
            get
            {
                return _NombreMensualite;
            }
            set
            {
                _NombreMensualite = value;
            }
        }

        private string _Organisation;
        public string Organisation
        {
            get
            {
                return _Organisation;
            }
            set
            {
                _Organisation = value;
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

        private double _maximum;
        public double maximum
        {
            get
            {
                return _maximum;
            }
            set
            {
                _maximum = value;
            }
        }

        private double _Minimum;
        public double Minimum
        {
            get
            {
                return _Minimum;
            }
            set
            {
                _Minimum = value;
            }
        }
    }
}
