using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BASEDiag_BO
{
    public class PoseAppareil
    {


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

        private Proposition _Parent;
        public Proposition Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                _Parent = value;
            }
        }

        private List<Semestre> _semestres = new List<Semestre>();
        public List<Semestre> semestres
        {
            get
            {
                return _semestres;
            }
            set
            {
                _semestres = value;
            }
        }

        private Appareil _appareil;
        public Appareil appareil
        {
            get
            {
                return _appareil;
            }
            set
            {
                _appareil = value;
            }
        }
    }
}
