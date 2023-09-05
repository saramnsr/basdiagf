using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEDiag_BO
{
    public class ObjSuivi
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

        private DateTime? _PoseApp;
        public DateTime? PoseApp
        {
            get
            {
                if (_PoseApp == DateTime.MinValue) return null; else return _PoseApp;
            }
            set
            {
                _PoseApp = value;
            }
        }

      

        private DateTime? _Empreinte;
        public DateTime? Empreinte
        {
            get
            {
                return _Empreinte;
            }
            set
            {
                _Empreinte = value;
            }
        }


       
        private string _Details;
        public string Details
        {
            get
            {
                return _Details;
            }
            set
            {
                _Details = value;
            }
        }

        private string _NatureTravaux;
        public string NatureTravaux
        {
            get
            {
                return _NatureTravaux;
            }
            set
            {
                _NatureTravaux = value;
            }
        }

       

    }
}
