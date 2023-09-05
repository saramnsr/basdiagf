using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class HistoResponsable
    {

        private bool _PraticienUnique;
        public bool PraticienUnique
        {
            get
            {
                return _PraticienUnique;
            }
            set
            {
                _PraticienUnique = value;
            }
        }
        

        private Utilisateur _PaticienResp;
        public Utilisateur PaticienResp
        {
            get
            {
                return _PaticienResp;
            }
            set
            {
                _PaticienResp = value;
            }
        }
        

        private Utilisateur _AssistanteResp;
        public Utilisateur AssistanteResp
        {
            get
            {
                return _AssistanteResp;
            }
            set
            {
                _AssistanteResp = value;
            }
        }
        private bool _AssistanteUnique;
        public bool AssistanteUnique
        {
            get
            {
                return _AssistanteUnique;
            }
            set
            {
                _AssistanteUnique = value;
            }
        }

        private int _IdPatient;
        public int IdPatient
        {
            get
            {
                if (patient != null)
                    _IdPatient = patient.Id;
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
        

        private Utilisateur _user;
        public Utilisateur user
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }
        

        private DateTime _DateEvenement;
        public DateTime DateEvenement
        {
            get
            {
                return _DateEvenement;
            }
            set
            {
                _DateEvenement = value;
            }
        }
        
    }
}
