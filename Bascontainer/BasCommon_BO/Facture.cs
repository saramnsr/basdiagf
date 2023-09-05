using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BasCommon_BO
{
    public class Facture
    {
        public Facture()
        {
        }

  
      
        public Facture(basePatient patient, Boolean vAcquite)
        {
            _patientFacture = patient;
          

         /*   factureLigne = new List<FactureLigne>();
            foreach (ActePG ag in lstActesPG)
            {

              

                FactureLigne fl = new FactureLigne();
                fl.id_Facture = id;
                fl.id_Echeance = ag.Id;
                fl.date_Execution = ag.DateExecution ;
                fl.montantLigneFacture = ag.Montant_Honoraire ;
                factureLigne.Add(fl);
            }*/

        }
        private int _id;
        [PropertyCanBeSerialized]
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private DateTime _DateFacture;
        [PropertyCanBeSerialized]
        public DateTime DateFacture
        {
            get
            {
                return _DateFacture;
            }
            set
            {
                _DateFacture = value;
            }
        }

        private DateTime _DateDebutFacture;
        [PropertyCanBeSerialized]
        public DateTime DateDebutFacture
        {
            get
            {
                return _DateDebutFacture;
            }
            set
            {
                _DateDebutFacture = value;
            }
        }


        private DateTime _DateFinFacture;
        [PropertyCanBeSerialized]
        public DateTime DateFinFacture
        {
            get
            {
                return _DateFinFacture;
            }
            set
            {
                _DateFinFacture = value;
            }
        }




        private basePatient _patientFacture;
        [PropertyCanBeSerialized]
        public basePatient patientFacture
        {
            get
            {
                return _patientFacture;
            }
            set
            {
                _patientFacture = value;
            }
        }


      /*  private int _id_patient;
        [PropertyCanBeSerialized]
        public int id_patient
        {
            get
            {
                return _id_patient;
            }
            set
            {
                _id_patient = value;
            }
        }*/

        private double _NombrePoints;
        [PropertyCanBeSerialized]
        public double NombrePoints
        {
            get
            {
                return _NombrePoints;
            }
            set
            {
                _NombrePoints = value;
            }
        }


        private double _Points;
        [PropertyCanBeSerialized]
        public double Points
        {
            get
            {
                return _Points;
            }
            set
            {
                _Points = value;
            }
        }

        private double _Montant;
        [PropertyCanBeSerialized]
        public double Montant
        {
            get
            {
                return _Montant;
            }
            set
            {
                _Montant = value;
            }
        }

        private double _MontantTotal;
        [PropertyCanBeSerialized]
        public double MontantTotal
        {
            get
            {
                return _MontantTotal;
            }
            set
            {
                _MontantTotal = value;
            }
        }


        private double _MontantPaye;
        [PropertyCanBeSerialized]
        public double MontantPaye
        {
            get
            {
                return _MontantPaye;
            }
            set
            {
                _MontantPaye = value;
            }
        }


        private double _MontantRestant;
        [PropertyCanBeSerialized]
        public double MontantRestant
        {
            get
            {
                return _MontantRestant;
            }
            set
            {
                _MontantRestant = value;
            }
        }


        private double _MontantLabo;
        [PropertyCanBeSerialized]
        public double MontantLabo
        {
            get
            {
                return _MontantLabo;
            }
            set
            {
                _MontantLabo = value;
            }
        }
        private double _MontantAchats;
        [PropertyCanBeSerialized]
        public double MontantAchats
        {
            get
            {
                return _MontantAchats;
            }
            set
            {
                _MontantAchats = value;
            }
        }
        private double _MontantSterilisation;
        [PropertyCanBeSerialized]
        public double MontantSterilisation
        {
            get
            {
                return _MontantSterilisation;
            }
            set
            {
                _MontantSterilisation = value;
            }
        }


        private List<FactureLigne> _factureLigne = null;
        [PropertyCanBeSerialized]
        public List<FactureLigne> factureLigne
        {
            get
            {
                return _factureLigne;
            }
            set
            {
                _factureLigne = value;
            }
        }

    }
}
