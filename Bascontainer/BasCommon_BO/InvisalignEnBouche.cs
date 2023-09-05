using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class InvisalignEnBouche
    {


        public enum RDVFrequency
        {
            _3Mois,
            _6Mois,
            _1an

        };



        public enum ChangeFrequency
        {
            //old
            //_14J,
            //_1Mois,
            //_1Sem,
            //_3Sems,
            //_4Mois,
            S1,
            S2,
            S3,
            S4,           

        };


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
        
        private DateTime? _DateDebut;
        public DateTime? DateDebut
        {
            get
            {
                return _DateDebut;
            }
            set
            {
                _DateDebut = value;
            }
        }
        
        private DateTime? _DateFin;
        public DateTime? DateFin
        {
            get
            {
                return _DateFin;
            }
            set
            {
                _DateFin = value;
            }
        }
        
        private int _NumAligneur;
        public int NumAligneur
        {
            get
            {
                return _NumAligneur;
            }
            set
            {
                _NumAligneur = value;
            }
        }
        
        private basePatient _Patient;
        public basePatient Patient
        {
            get
            {
                return _Patient;
            }
            set
            {
                _Patient = value;
            }
        }
        
        private int _IdPatient;
        public int IdPatient
        {
            get
            {
                if (Patient != null) IdPatient = Patient.Id;
                return _IdPatient;
            }
            set
            {
                _IdPatient = value;
            }
        }
             
        private bool _IsHaut;
        public bool IsHaut
        {
            get
            {
                return _IsHaut;
            }
            set
            {
                _IsHaut = value;
            }
        }
        
        
        
    }
}
