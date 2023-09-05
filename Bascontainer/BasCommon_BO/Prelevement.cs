using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{

    public class GroupedPrelevement
    {

        private DateTime _DatePremierPrelevement;
        public DateTime DatePremierPrelevement
        {
            get
            {
                return _DatePremierPrelevement;
            }
            set
            {
                _DatePremierPrelevement = value;
            }
        }

        private List<int> _EcheanceIds;
        public List<int> EcheanceIds
        {
            get
            {
                return _EcheanceIds;
            }
            set
            {
                _EcheanceIds = value;
            }
        }

        private List<int> _EcheanceDays = new List<int>();
        public List<int> EcheanceDays
        {
            get
            {
                return _EcheanceDays;
            }
            set
            {
                _EcheanceDays = value;
            }
        }

        private List<double> _Montants;
        public List<double> Montants
        {
            get
            {
                return _Montants;
            }
            set
            {
                _Montants = value;
            }
        }




        public string EcheanceDay
        {
            get
            {
                List<int> dico = new List<int>();

                if (EcheanceDays == null) return "Aucun";

                foreach (int d in EcheanceDays)
                    if (!dico.Contains(d))
                        dico.Add(d);

                if (dico.Count == 1)
                    return dico[0].ToString();
                else
                    return "Voir details";
            }
            
        }


       public string MontantParPrelevement
        {
            get
            {
                List<double> dico = new List<double>();

                if (Montants == null) return "Aucun";
                foreach (double d in Montants)
                    if (!dico.Contains(d))
                        dico.Add(d);

                if (dico.Count == 1)
                    return dico[0].ToString("C2");
                else
                    return "Voir details";
            }
            
        }

       public double TotalDue
        {
            get
            {
                double total = 0;
                foreach (double mnt in Montants)
                    total += mnt;

                return total;
            }
            
        }


       private basePatient _patient = null;
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

        private string _traitement;
        public string traitement
        {
            get
            {
                return _traitement;
            }
            set
            {
                _traitement = value;
            }
        }

        private int _idtraitement;
        public int idtraitement
        {
            get
            {
                return _idtraitement;
            }
            set
            {
                _idtraitement = value;
            }
        }

        private string _Patient;
        public string Patient
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

        private List<Echeance> _echeances = null;
        public List<Echeance> echeances
        {
            get
            {
                return _echeances;
            }
            set
            {
                _echeances = value;
            }
        }
    }

    public class Prelevement
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

        private DateTime? _DateEncaissement;
        public DateTime? DateEncaissement
        {
            get
            {
                return _DateEncaissement;
            }
            set
            {
                _DateEncaissement = value;
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
