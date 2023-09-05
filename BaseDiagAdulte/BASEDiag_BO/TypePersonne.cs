using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEDiag_BO
{



    #region ObjectClass

    public class TypePers
    {

        public override string ToString()
        {
            return Nom;
        }
       
        private int _IdType = -1;
        public int IdType
        {
            get
            {
                return _IdType;
            }
            set
            {
                _IdType = value;
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

    #endregion

        #region TypePers






        #endregion



}
