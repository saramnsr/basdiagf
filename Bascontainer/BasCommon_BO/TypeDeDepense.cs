using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO.Compta;

namespace BasCommon_BO
{
    public class TypeDeDepense
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



        private string _organisation;
        public string organisation
        {
            get
            {
                return _organisation;
            }
            set
            {
                _organisation = value;
            }
        }
        

        private String _Libelle;
        public String Libelle
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
        

        private CodeComptable _CodeComptable;
        public CodeComptable CodeComptable
        {
            get
            {
                return _CodeComptable;
            }
            set
            {
                _CodeComptable = value;
            }
        }
        
    }
}
