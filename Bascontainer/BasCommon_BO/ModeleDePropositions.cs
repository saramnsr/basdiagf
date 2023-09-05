using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class ModeleDePropositions
    {

        public override string ToString()
        {
            return Nom;
        }

        private List<Proposition> _lstproposition = new List<Proposition>();
        public List<Proposition> propositions
        {
            get
            {
                return _lstproposition;
            }
            set
            {
                _lstproposition = value;
            }
        }

        private String _Nom;
        public String Nom
        {
            get
            {
                return _Nom;
            }
            set
            {
                _Nom = value;
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
