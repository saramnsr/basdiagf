using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEDiag_BO
{
    [Serializable]
    public class Category
    {

        public override string ToString()
        {
            return Nom;
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

        private string _Nom;
        public string Nom
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
    }
}
