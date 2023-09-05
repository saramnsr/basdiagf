using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BASEDiag_BO
{
    public class Devis
    {


        private List<ActePGPropose> _actesHorstraitement;
        public List<ActePGPropose> actesHorstraitement
        {
            get
            {
                return _actesHorstraitement;
            }
            set
            {
                _actesHorstraitement = value;
            }
        }

        private List<Proposition> _propositions = null;
        public List<Proposition> propositions
        {
            get
            {
                return _propositions;
            }
            set
            {
                _propositions = value;
            }
        }

        private int _IdObjetBaseView = -1;
        public int IdObjetBaseView
        {
            get
            {
                return _IdObjetBaseView;
            }
            set
            {
                _IdObjetBaseView = value;
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

        private DateTime _DateProposition;
        public DateTime DateProposition
        {
            get
            {
                return _DateProposition;
            }
            set
            {
                _DateProposition = value;
            }
        }

        private DateTime? _DateEcheance;
        public DateTime? DateEcheance
        {
            get
            {
                return _DateEcheance;
            }
            set
            {
                _DateEcheance = value;
            }
        }

        private DateTime? _DateAcceptation;
        public DateTime? DateAcceptation
        {
            get
            {
                return _DateAcceptation;
            }
            set
            {
                _DateAcceptation = value;
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
