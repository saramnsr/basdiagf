using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class SuiviSpecialiste
    {
        public override string ToString()
        {
            return NomCorrespondant;
        }


        private string _NomCorrespondant;
        public string NomCorrespondant
        {
            get
            {
                if (correpondant != null) _NomCorrespondant = correpondant.ToString();
                return _NomCorrespondant;
            }
            set
            {
                _NomCorrespondant = value;
            }
        }
        

        private string _Commentaire;
        public string Commentaire
        {
            get
            {
                return _Commentaire;
            }
            set
            {
                _Commentaire = value;
            }
        }
        

        private int _IdCorrespondant = -1;
        public int IdCorrespondant
        {
            get
            {
                if (correpondant != null) _IdCorrespondant = correpondant.Id;
                return _IdCorrespondant;
            }
            set
            {
                _IdCorrespondant = value;
            }
        }
        

        private Correspondant _correpondant;
        public Correspondant correpondant
        {
            get
            {
                return _correpondant;
            }
            set
            {
                _correpondant = value;
            }
        }
        

        private DateTime _DateEnvois;
        public DateTime DateEnvois
        {
            get
            {
                return _DateEnvois;
            }
            set
            {
                _DateEnvois = value;
            }
        }
        


        private int _IdPatient = -1;
        public int IdPatient
        {
            get
            {
                if (patient != null) _IdPatient = patient.Id;
                return _IdPatient;
            }
            set
            {
                _IdPatient = value;
            }
        }
        
        private basePatient _patient;
        public basePatient patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
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
