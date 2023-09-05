using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class BaseLog
    {

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

        private int _IdUser;
        public int IdUser
        {
            get
            {
                if (user != null) _IdUser = user.Id;
                return _IdUser;
            }
            set
            {
                _IdUser = value;
            }
        }


        private string _Category;
        public string Category
        {
            get
            {
                return _Category;
            }
            set
            {
                _Category = value;
            }
        }

        private string _CodeAction;
        public string CodeAction
        {
            get
            {
                return _CodeAction;
            }
            set
            {
                _CodeAction = value;
            }
        }

        private string _NomMachine;
        public string NomMachine
        {
            get
            {
                return _NomMachine;
            }
            set
            {
                _NomMachine = value;
            }
        }

        private string _Commentaires;
        public string Commentaires
        {
            get
            {
                return _Commentaires;
            }
            set
            {
                _Commentaires = value;
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

        private int _IdPatient;
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

        private DateTime _DteLog;
        public DateTime DteLog
        {
            get
            {
                return _DteLog;
            }
            set
            {
                _DteLog = value;
            }
        }



    }
}
