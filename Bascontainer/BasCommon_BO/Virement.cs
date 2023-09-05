using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Virement
    {

        private Echeance _echeance;
        public Echeance echeance
        {
            get
            {
                return _echeance;
            }
            set
            {
                _echeance = value;
            }
        }

        private BanqueDeRemise _comptecabinet = null;
        public BanqueDeRemise comptecabinet
        {
            get { return _comptecabinet; }
            set { _comptecabinet = value; }
        }

        public int Idpatient
        {
            get { return echeance.IdPatient; }
        }

        public basePatient patient
        {
            get { return echeance.patient; }

        }
        
        private DateTime? _DateReception = null;
        public DateTime? DateReception
        {
            get
            {
                return _DateReception;
            }
            set
            {
                _DateReception = value;
            }
        }

        private DateTime? _DateValeurBqe = null;
        public DateTime? DateValeurBqe
        {
            get
            {
                return _DateValeurBqe;
            }
            set
            {
                _DateValeurBqe = value;
            }
        }

        public string Libelle { get; set; }



        public Double Montant
        {
            get
            {
                return echeance.Montant;
            }

        }

        public DateTime? DateEcheance
        {
            get
            {
                return echeance.DateEcheance;
            }

        }

        private int _IdEntite = -1;
        public int IdEntite
        {
            get
            {
                return _IdEntite;
            }
            set
            {
                _IdEntite = value;
            }
        }

        private EntiteJuridique _Entite = null;
        public EntiteJuridique Entite
        {
            get
            {
                return _Entite;
            }
            set
            {
                _Entite = value;
            }
        }
    }
}
