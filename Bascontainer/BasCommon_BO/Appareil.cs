using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Appareil
    {
        
        public override string ToString()
        {
            return Libelle;
        }





        private string _InfoDEP;
        [PropertyCanBeSerialized]
        public string InfoDEP
        {
            get
            {
                return _InfoDEP;
            }
            set
            {
                _InfoDEP = value;
            }
        }

        private List<String> _Risques;
        [PropertyCanBeSerialized]
        public List<String> Risques
        {
            get
            {
                return _Risques;
            }
            set
            {
                _Risques = value;
            }
        }

        private string _Code;
        [PropertyCanBeSerialized]
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

        private string _Libelle;
        [PropertyCanBeSerialized]
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
