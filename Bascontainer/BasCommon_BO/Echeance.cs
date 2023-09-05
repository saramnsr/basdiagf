using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Echeance : IComparable, ICloneable
    {

        public enum NiveauRelance
        {
            Aucun,
            ReleveDeCompte,
            Relance,
            PreContentieux,
            Majoration,
            Contentieux
        }

        public struct WorkFlowRelance
        {
            public DateTime? ReleveDeCompte;
            public DateTime? Relance;
            public DateTime? PreContentieux;
            public DateTime? Majoration;
            public DateTime? Contentieux;


        }

        public enum typepayeur
        {
            inconnu = -1,
            patient = 0,
            Mutuelle = 1,
            Secu = 2,
            Banque = 3,
            AutrePersonne = 4 ,//Pour les credits : Optalion,PNF...
        }

        public WorkFlowRelance Relances = new WorkFlowRelance();


       public NiveauRelance niveauRelance
        {
            get
            {
                if (Relances.Contentieux != null) return NiveauRelance.Contentieux;
                if (Relances.Majoration != null) return NiveauRelance.Majoration;
                if (Relances.PreContentieux != null) return NiveauRelance.PreContentieux;
                if (Relances.Relance != null) return NiveauRelance.Relance;
                if (Relances.ReleveDeCompte != null) return NiveauRelance.ReleveDeCompte;
                
                return NiveauRelance.Aucun;

            }
           
        }
        

        private typepayeur _payeur = typepayeur.patient;
        [PropertyCanBeSerialized]
        public typepayeur payeur
        {
            get
            {
                return _payeur;
            }
            set
            {
                _payeur = value;
            }
        }


        private int _IdComm ;
        [PropertyCanBeSerialized]
        public int IdComm
        {
            get
            {
                return _IdComm;
            }
            set
            {
                _IdComm = value;
            }
        }

        private int _IdActeTraitement = -1;
        [PropertyCanBeSerialized]
        public int IdActeTraitement
        {
            get
            {
                return _IdActeTraitement;
            }
            set
            {
                _IdActeTraitement = value;
            }
        }
        private bool _ParVirement = false;
        [PropertyCanBeSerialized]
        public bool ParVirement
        {
            get
            {
                return _ParVirement;
            }
            set
            {
                _ParVirement = value;
            }
        }

        private bool _ParPrelevement = false;
        [PropertyCanBeSerialized]
        public bool ParPrelevement
        {
            get
            {
                return _ParPrelevement;
            }
            set
            {
                _ParPrelevement = value;
            }
        }
        private Facture _facture = null;
        [PropertyCanBeSerialized]
        public Facture facture 
        {
            get
            {
                return _facture;
            }
            set
            {
                _facture = value;
            }
        }
        private Encaissement _encaissement = null;
        [PropertyCanBeSerialized]
        public Encaissement encaissement
        {
            get
            {
                return _encaissement;
            }
            set
            {
                _encaissement = value;
            }
        }

        private Mutuelle _mutuelle;
        [PropertyCanBeSerialized]
        public Mutuelle mutuelle
        {
            get
            {
                return _mutuelle;
            }
            set
            {
                _mutuelle = value;
            }
        }

        private int _ID_Encaissement = -1;
        [PropertyCanBeSerialized]
        public int ID_Encaissement
        {
            get
            {
                if (encaissement != null) _ID_Encaissement = encaissement.Id;
                return _ID_Encaissement;
            }
            set
            {
                _ID_Encaissement = value;
            }
        }

        private int _perte = 0;
        [PropertyCanBeSerialized]
        public int perte
        {
            get
            {

                return _perte;
            }
            set
            {
                _perte = value;
            }
        }
        private int _ID_Facturation = -1;
        [PropertyCanBeSerialized]
        public int ID_Facturation
        {
            get
            {
                if (facture != null) _ID_Facturation = facture.id;
                return _ID_Facturation;
            }
            set
            {
                _ID_Facturation  = value;
            }
        }
        private int _status = 0;
        [PropertyCanBeSerialized]
        public int status
        {
            get
            {
                if (ID_Encaissement == -1)
                { _status = 0; }
                else
                {
                    if (ID_Facturation == -1)
                        _status = 1;
                    else {
                        _status = 2;
                    }
                }
                return _status;
            }
            set
            {
                _status = value;
            }
        }
        private int _Id = -1;
        [PropertyCanBeSerialized]
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
      

        private string _TypeActe = "";
        [PropertyCanBeSerialized]
        public string TypeActe
        {
            get
            {
                return _TypeActe;
            }
            set
            {
                _TypeActe = value;
            }
        }



        private ActePG _acte = null;
        [PropertyCanBeSerialized]
        public ActePG acte
        {
            get
            {
                return _acte;
            }
            set
            {
                _acte = value;
                if (value == null) IdActe = -1;
                else IdActe = value.Id;
            }
        }

        private int _IdActe = -1;
        [PropertyCanBeSerialized]
        public int IdActe
        {
            get
            {
                if (acte != null) return acte.Id;
                return _IdActe;
            }
            set
            {
                _IdActe = value;
            }
        }




        private string _NomPatient;
        [PropertyCanBeSerialized]
        public string NomPatient
        {
            get
            {
                if (patient != null) _NomPatient = patient.Nom+" "+_patient.Prenom;
                return _NomPatient;
            }
            set
            {
                _NomPatient = value;
            }
        }

        private int _IdPatient;
        [PropertyCanBeSerialized]
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


        private string _Libelle;
        [PropertyCanBeSerialized]
        public string Libelle
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
        private DateTime _DateFix;
        [PropertyCanBeSerialized]
        public DateTime DateFix
        {
            get
            {
                return _DateFix;
            }
            set
            {
                _DateFix = value;
            }
        }
        private DateTime _DateEcheance;
        [PropertyCanBeSerialized]
        public DateTime DateEcheance
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

        private basePatient _patient;
        [PropertyCanBeSerialized]
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

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (!(obj is Echeance)) return 0;

            Echeance ech = (Echeance)obj;
            return ech.DateEcheance < DateEcheance ? 1 : -1;

        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            Echeance ec = new Echeance();

            ec.acte = acte;
            ec.DateEcheance = DateEcheance;
            ec.encaissement = encaissement;
            ec.Id = Id;
            ec.ID_Encaissement = ID_Encaissement;
            ec.IdActe = IdActe;
            ec.IdPatient = IdPatient;
            ec.Libelle = ec.Libelle;
            ec.Montant = ec.Montant;
            ec.mutuelle = ec.mutuelle;
            ec.ParPrelevement = ParPrelevement;
            ec.ParVirement = ParVirement;
            ec.patient = patient;
            ec.payeur = this.payeur;

            return ec;
        }

        #endregion
    }




    public class BaseTempEcheanceDefinition :  IComparable
    {



        private Echeance.typepayeur _payeur = Echeance.typepayeur.patient;
        [PropertyCanBeSerialized]
        public Echeance.typepayeur payeur
        {
            get
            {
                return _payeur;
            }
            set
            {
                _payeur = value;
            }
        }


      

        private int _Id;
        [PropertyCanBeSerialized]
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

        private bool _AlreadyPayed;
        [PropertyCanBeSerialized]
        public bool AlreadyPayed
        {
            get
            {
                return _AlreadyPayed;
            }
            set
            {
                _AlreadyPayed = value;
            }
        }

        private bool _CanRecalculate = true;
        [PropertyCanBeSerialized]
        public bool CanRecalculate
        {
            get
            {
                if (_AlreadyPayed)
                    return false;
                return _CanRecalculate;
            }
            set
            {
                _CanRecalculate = value;
            }
        }

        private ActePG _acte;
        [PropertyCanBeSerialized]
        public ActePG acte
        {
            get
            {
                return _acte;
            }
            set
            {
                _acte = value;
            }
        }

        private bool _ParVirement = false;
        [PropertyCanBeSerialized]
        public bool ParVirement
        {
            get
            {
                return _ParVirement;
            }
            set
            {
                _ParVirement = value;
            }
        }

        private bool _ParPrelevement = false;
        [PropertyCanBeSerialized]
        public bool ParPrelevement
        {
            get
            {
                return _ParPrelevement;
            }
            set
            {
                _ParPrelevement = value;
            }
        }
        private Boolean _desactive = false;
        [PropertyCanBeSerialized]
        public Boolean desactive
        {
            get
            {
                return _desactive;
            }
            set
            {
                _desactive = value;
            }
        }
        private string _Libelle;
        [PropertyCanBeSerialized]
        public string Libelle
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

        private Double _Montant;
        [PropertyCanBeSerialized]
        public Double Montant
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
        private DateTime _DateFix;
        [PropertyCanBeSerialized]
        public DateTime DateFix
        {
            get
            {
                return _DateFix;
            }
            set
            {
                _DateFix = value;
            }
        }
        private DateTime _DAteEcheance;
        [PropertyCanBeSerialized]
        public DateTime DAteEcheance
        {
            get
            {
                return _DAteEcheance;
            }
            set
            {
                _DAteEcheance = value;
            }
        }

        private string _TypeActe;
        [PropertyCanBeSerialized]
        public string TypeActe
        {
            get
            {
                return _TypeActe;
            }
            set
            {
                _TypeActe = value;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj is BaseTempEcheanceDefinition)
            {
                if ((DAteEcheance != null) && (((BaseTempEcheanceDefinition)obj).DAteEcheance != null))
                    return (int)(DAteEcheance - ((BaseTempEcheanceDefinition)obj).DAteEcheance).TotalDays;

                if ((DAteEcheance == null) && (((BaseTempEcheanceDefinition)obj).DAteEcheance != null))
                    return -1;

                if ((DAteEcheance != null) && (((BaseTempEcheanceDefinition)obj).DAteEcheance == null))
                    return 1;

                if ((DAteEcheance == null) && (((BaseTempEcheanceDefinition)obj).DAteEcheance == null))
                    return 0;
            }

            return 0;
        }
    }


    public class TempEcheanceDefinition : BaseTempEcheanceDefinition,IComparable
    {


        public static TempEcheanceDefinition FromBaseTempEcheanceDefinition(BaseTempEcheanceDefinition org)
        {
            TempEcheanceDefinition edc = new TempEcheanceDefinition();
            edc.acte = org.acte;
            edc.AlreadyPayed = org.AlreadyPayed;
            edc.CanRecalculate = org.CanRecalculate;
            edc.DAteEcheance = org.DAteEcheance;
            edc.Id = org.Id;
            edc.Libelle = org.Libelle;
            edc.Montant = org.Montant;
            edc.ParPrelevement = org.ParPrelevement;
            edc.ParVirement = org.ParVirement;
            edc.payeur = org.payeur;


            return edc;

        }
       

        private int _IdSemestre;
        [PropertyCanBeSerialized]
        public int IdSemestre
        {
            get
            {
                if (semestre != null) _IdSemestre = semestre.Id;
                return _IdSemestre;
            }
            set
            {
                _IdSemestre = value;
            }
        }

        private Semestre _semestre;
        public Semestre semestre
        {
            get
            {
                return _semestre;
            }
            set
            {
                _semestre = value;
            }
        }

   
    }



    public class EcheanceDevisALaCarte : BaseTempEcheanceDefinition
    {




        public static EcheanceDevisALaCarte FromBaseTempEcheanceDefinition(BaseTempEcheanceDefinition org)
        {
            EcheanceDevisALaCarte edc = new EcheanceDevisALaCarte();
            edc.acte = org.acte;
            edc.AlreadyPayed = org.AlreadyPayed;
            edc.CanRecalculate = org.CanRecalculate;
            edc.DAteEcheance = org.DAteEcheance;
            edc.Id = org.Id;
            edc.Libelle = org.Libelle;
            edc.Montant = org.Montant;
            edc.ParPrelevement = org.ParPrelevement;
            edc.ParVirement = org.ParVirement;
            edc.payeur = org.payeur;


            return edc;

        }

        private Devis _devis = null;
        public Devis devis
        {
            get
            {
                return _devis;
            }
            set
            {
                _devis = value;
            }
        }
        

        private int _IdDevis = -1;
        public int IdDevis
        {
            get
            {
                if (devis != null) _IdDevis = devis.Id;
                return _IdDevis;
            }
            set
            {
                _IdDevis = value;
            }
        }
        
    }

}
