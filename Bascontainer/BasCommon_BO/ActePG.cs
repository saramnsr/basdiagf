using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class ActePG : ICloneable
    {

       

        public enum TypeReglement
        {
            Unitaire = 'U',
            Forfaitaire = 'F',
            Semestriel = 'S',
            Mensuel = 'M',
            Journalier = 'J',
            AuDevis = 'C',
        }

        public override string ToString()
        {
            return DateExecution.ToShortDateString() + "  :  " + Montant_Honoraire.ToString("C2") + "  :  " + Libelle;
        }

        public enum etatpaiement
        {
            Aucun,
            Partiel,
            Complet

        }


        private EntentePrealable.TypeDeTraitement _typetraitement;
        [PropertyCanBeSerialized]
        public EntentePrealable.TypeDeTraitement typetraitement
        {
            get
            {
                return _typetraitement;
            }
            set
            {
                _typetraitement = value;
            }
        }

        private string _TypeActe = "0";
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

        private int _Facturee = 0;
        [PropertyCanBeSerialized]
        public int Facturee
        {
            get
            {
                return _Facturee;
            }
            set
            {
                _Facturee = value;
            }
        }

        private int _Id_facture = 0;
        [PropertyCanBeSerialized]
        public int Id_facture
        {
            get
            {
                return _Id_facture;
            }
            set
            {
                _Id_facture = value;
            }
        }


        private int _Semestre = 0;
        [PropertyCanBeSerialized]
        public int Semestre
        {
            get
            {
                return _Semestre;
            }
            set
            {
                _Semestre = value;
            }
        }

        private int _Contention = 0;
        [PropertyCanBeSerialized]
        public int Contention
        {
            get
            {
                return _Contention;
            }
            set
            {
                _Contention = value;
            }
        }

        private int _IdComm = -1;
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

      



        private int _IdPatient = -1;
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

        private int _IdPlan;
        [PropertyCanBeSerialized]
        public int IdPlan
        {
            get
            {
                return _IdPlan;
            }
            set
            {
                _IdPlan = value;
            }
        }

        private int _IdActe;
        [PropertyCanBeSerialized]
        public int IdActe
        {
            get
            {
                return _IdActe;
            }
            set
            {
                _IdActe = value;
            }
        }

        private int _Coeff;
        [PropertyCanBeSerialized]
        public int Coeff
        {
            get
            {
                return _Coeff;
            }
            set
            {
                _Coeff = value;
            }
        }

        public ActePG()
        {

        }

        public ActePG(CommClinique cc, CommActes cm, string TypeActe)
        {
            string lib = "";


            this._DateExecution = cc.DatePrevisionnnelle.Value;



            //this.Libelle = cc.Acte.acte_libelle + lib;
            List<Echeance> listech = new List<Echeance>();
            if (cc.echeancestemp != null)
            {

                foreach (BaseTempEcheanceDefinition bted in cc.echeancestemp)
                {
                    Echeance ec = new Echeance();
                    ec.DateEcheance = bted.DAteEcheance;
                    ec.encaissement = null;



                    ec.IdPatient = cc.IdPatient;
                    ec.Libelle = cm.LibActe ;
                   
                  ///  if (cm.prix_traitement > 0)
                        ec.Montant = Convert.ToDouble(cm.prix_traitement * cm.Qte) * (bted.Montant / cc.prix);
                 //   else
                 //       ec.Montant = Convert.ToDouble(cm.prix_acte * cm.Qte) * (bted.Montant / cc.prix) ;
                    ec.mutuelle = ec.mutuelle;
                    ec.ParPrelevement = bted.ParPrelevement;
                    ec.ParVirement = bted.ParVirement;
                    ec.patient = cc.patient;
                    ec.payeur = bted.payeur;

                    //ec.IdActe = ct.IdActe;
                    listech.Add(ec);
                }
                this.lstEcheances = listech;
            }
            else
            {

                Echeance ec = new Echeance();
                ec.payeur = Echeance.typepayeur.patient;
                ec.DateEcheance = cc.DatePrevisionnnelle.Value;
                ec.encaissement = null;


                //ec.IdActe = ct.Acte.id_acte;
                ec.IdPatient = cc.IdPatient;
                ec.Libelle = cc.Acte.acte_libelle;
                ec.Montant = cc.prix;
                ec.mutuelle = ec.mutuelle;

                ec.patient = cc.patient;


                listech.Add(ec);
                this.lstEcheances = listech;
            }
            this.IdPatient = cc.IdPatient;
            this.Libelle = cm.LibActe;
            this.IdComm = cc.Id;
            this.TypeActe = TypeActe ;
            this.IdActe = cm.IdActe; 
          //  if (cm.prix_traitement  > 0)
                this.Montant_Honoraire = cm.prix_traitement  * cm.Qte;
          //  else
          //      this.Montant_Honoraire = cm.prix_acte  * cm.Qte;

        }
        public ActePG(CommClinique  cc, CommMateriel cm)
        {
            string lib = "";
            
           
            this._DateExecution = cc.DatePrevisionnnelle.Value;
            


            //this.Libelle = cc.Acte.acte_libelle + lib;
            List<Echeance> listech = new List<Echeance>();
            if (cc.echeancestemp != null )
            {
                Double MontantEcheances=0;
                foreach (BaseTempEcheanceDefinition bted in cc.echeancestemp)
                {
                    MontantEcheances = MontantEcheances + bted.Montant;
                }
                foreach (BaseTempEcheanceDefinition bted in cc.echeancestemp)
                {
                    Echeance ec = new Echeance();
                    ec.DateEcheance = bted.DAteEcheance;
                    ec.encaissement = null;



                    ec.IdPatient = cc.IdPatient;
                    ec.Libelle = cm.Libelle ;
                   // if (cc.prix_traitement >0)

                    ec.Montant = (cm.prix_materiel_traitement * cm.Qte) * (bted.Montant / MontantEcheances);


                  //  ec.Montant =Convert.ToDouble  (cm.prix_materiel_traitement * cm.Qte )  ;
                  //  else
                  //      ec.Montant =Convert.ToDouble  (cm.prix_materiel_traitement * cm.Qte ) ;
                    ec.mutuelle = ec.mutuelle;
                    ec.ParPrelevement = bted.ParPrelevement;
                    ec.ParVirement = bted.ParVirement;
                    ec.patient = cc.patient;
                    ec.payeur = bted.payeur;
                    
                    //ec.IdActe = ct.IdActe;
                    listech.Add(ec);
                }
                this.lstEcheances = listech;
            }
            else
            {

                Echeance ec = new Echeance();
                ec.payeur = Echeance.typepayeur.patient;
                ec.DateEcheance = cc.DatePrevisionnnelle.Value;
                ec.encaissement = null;


                //ec.IdActe = ct.Acte.id_acte;
                ec.IdPatient = cc.IdPatient;
                ec.Libelle = cc.Acte.acte_libelle;
                ec.Montant = cc.prix;
                ec.mutuelle = ec.mutuelle;

                ec.patient = cc.patient;


                listech.Add(ec);
                this.lstEcheances = listech;
            }
            this.IdPatient = cc.IdPatient;
            this.Libelle = cm.Libelle;
            this.IdComm = cc.Id;
            this.IdActe = cm.idMateriel;
            this.TypeActe ="M";
          //  if (cm.prix_materiel_traitement > 0 )
            this.Montant_Honoraire = cm.prix_materiel_traitement * cm.Qte  ;
          //  else
           //     this.Montant_Honoraire = cm.prix_materiel * cm.Qte;
            
        }

        public ActePG(CommTraitement ct)
        {
            string lib = "";
            List<String> libelle = new List<string>();
            this._DateExecution = ct.DatePrevisionnnelle.Value;
            if (ct.ActesSupp.Count != 0 || ct.photos.Count != 0 || ct.Radios.Count != 0 || ct.Materiels.Count != 0)
            {
                lib = "(";
                foreach (CommActesTraitement cs in ct.ActesSupp)
                {

                    libelle.Add(cs.LibActe);
                }
                foreach (CommActesTraitement cr in ct.Radios)
                {

                    libelle.Add(cr.LibActe);

                }
                foreach (CommActesTraitement cp in ct.photos)
                {
                    libelle.Add(cp.LibActe);
                }
                foreach (CommMateriel cm in ct.Materiels)
                {
                    libelle.Add(cm.Libelle);
                }
                foreach (string s in libelle)
                {
                    lib = lib + s;
                    if (s != libelle.Last())
                        lib = lib + ", ";
                }
                lib = lib + ")";
            }

            
            this.Libelle = ct.Acte.acte_libelle + lib;
            List<Echeance> listech = new List<Echeance>();
            if (ct.echeancestemp != null)
            {

                foreach (BaseTempEcheanceDefinition bted in ct.echeancestemp)
                {
                    Echeance ec = new Echeance();
                    ec.DateEcheance = bted.DAteEcheance;
                    ec.encaissement = null;



                    ec.IdPatient = ct.devis.IdPatient;
                    ec.Libelle = bted.Libelle;
                    ec.Montant = bted.Montant;
                    ec.mutuelle = ec.mutuelle;
                    ec.ParPrelevement = bted.ParPrelevement;
                    ec.ParVirement = bted.ParVirement;
                    ec.patient = ct.devis.patient;
                    ec.payeur = bted.payeur;
                    //ec.IdActe = ct.IdActe;
                    listech.Add(ec);
                }
                this.lstEcheances = listech;
            }
            else
            {

                Echeance ec = new Echeance();
                ec.payeur = Echeance.typepayeur.patient;
                ec.DateEcheance = ct.DatePrevisionnnelle.Value;
                ec.encaissement = null;


                //ec.IdActe = ct.Acte.id_acte;
                ec.IdPatient = ct.devis.IdPatient;
                ec.Libelle = ct.Acte.acte_libelle;
                ec.Montant = ct.prix;
                ec.mutuelle = ec.mutuelle;

                ec.patient = ct.devis.patient;


                listech.Add(ec);
                this.lstEcheances = listech;
            }
            this.IdPatient = ct.devis.IdPatient;

            this.Montant_Honoraire = ct.prix;


        }
        public ActePG(CommClinique cc, int AllComment = 0, Boolean LigneVide = false, string prefix = "")
        {
            
            string lib = "";
            List<String> libelle = new List<string>();
            this._DateExecution = cc.DatePrevisionnnelle.Value;
            this.IdDevisAssociate = cc.Id_devis;
           //this.IdDevisAssociate
            this.Libelle = cc.Acte.acte_libelle + lib;
            List<Echeance> listech = new List<Echeance>();
            if (cc.echeancestemp.Count > 0)
            {

                foreach (BaseTempEcheanceDefinition bted in cc.echeancestemp)
                {
                    Echeance ec = new Echeance();
                    ec.DateEcheance = bted.DAteEcheance;
                    ec.encaissement = null;



                    ec.IdPatient = cc.IdPatient;
                    ec.Libelle = bted.Libelle;
                    ec.Montant = bted.Montant;
                    this.Montant_Honoraire = this.Montant_Honoraire + cc.prix_traitement;
                    //  if (cc.prix_traitement > 0)
                    /*    ec.Montant = (cc.prix_traitement * int.Parse ( cc.Acte.quantite))  * (bted.Montant / cc.prix);
                        foreach (CommActes ca in cc.ActesSupp)
                        {
                            ec.Montant = ec.Montant + (ca.prix_traitement * ca.Qte) * (bted.Montant / cc.prix);

                        }
                        foreach (CommActes ca in cc.Radios)
                        {
                            ec.Montant = ec.Montant + (ca.prix_traitement * ca.Qte) * (bted.Montant / ca.prix_acte);

                        }
                        foreach (CommActes ca in cc.photos)
                        {
                            ec.Montant = ec.Montant + (ca.prix_traitement * ca.Qte) * (bted.Montant / ca.prix_acte);

                        }²
                        foreach (CommMateriel  cm in cc.Materiels )
                        {
                            ec.Montant = ec.Montant  +  (cm.prix_materiel_traitement * cm.Qte) * (bted.Montant / cm.prix_materiel);

                        }*/
                    //  else
                    //      ec.Montant = (cc.Acte.prix_acte * int.Parse ( cc.Acte.quantite)) * (bted.Montant / cc.prix);
                    // ec.Montant = bted.Montant;
                    ec.mutuelle = ec.mutuelle;
                    ec.ParPrelevement = bted.ParPrelevement;
                    ec.ParVirement = bted.ParVirement;
                    ec.patient = cc.patient;
                    ec.payeur = bted.payeur;
                    //ec.IdActe = ct.IdActe;
                    listech.Add(ec);
                }
                this.lstEcheances = listech;
            }
            else
            {
                if ((System.Configuration.ConfigurationManager.AppSettings["PaysCabinet" + prefix] == "FR"))
                {
                    double partsecu = 0;
                    double parPatient = 0;
                    Echeance ec = new Echeance();

                    getMontantEcheToulon(cc, ref partsecu, ref parPatient);

                    if (parPatient > 0)
                    {
                         ec = new Echeance();
                         ec.Montant = parPatient;
                         ec.TypeActe = "0";
                         ec.DateEcheance = cc.DatePrevisionnnelle.Value;
                         ec.Libelle = cc.Acte.acte_libelle + "[part patient]";
                         ec.ParPrelevement = false;
                        ec.ParVirement = false;
                        ec.payeur = Echeance.typepayeur.patient;
                        ec.patient = cc.patient;
                        ec.IdActeTraitement = cc.Acte.id_acte;

                        listech.Add(ec);

                    }
                    if (partsecu > 0)
                    {
                         ec = new Echeance();
                         ec.Montant = partsecu;
                         ec.TypeActe = "0";
                         ec.DateEcheance = cc.DatePrevisionnnelle.Value;
                         ec.Libelle = cc.Acte.acte_libelle + "[part secu]";
                        ec.payeur = Echeance.typepayeur.Secu;
                        ec.ParPrelevement = false;
                        ec.ParVirement = false;
                        ec.patient = cc.patient;
                        ec.IdActeTraitement = cc.Acte.id_acte;
                        listech.Add(ec);
                    }
                    this.Montant_Honoraire = cc.prix_traitement;
                }
                else
                {
                    Echeance ec = new Echeance();
                    ec.payeur = Echeance.typepayeur.patient;
                    ec.DateEcheance = cc.DatePrevisionnnelle.Value;
                    ec.encaissement = null;
                    if (AllComment == 0)
                    {
                        ec.TypeActe = "0";
                        ec.IdActeTraitement = cc.Acte.id_acte;
                        ec.Libelle = cc.Acte.acte_libelle;
                    }
                    else
                    {
                        ec.TypeActe = "";
                        ec.IdActeTraitement = cc.Acte.id_acte;
                        ec.Libelle = cc.Acte.acte_libelle;
                    }


                    ec.IdPatient = cc.IdPatient;
                    //   ec.Libelle = cc.Acte.acte_libelle;
                    if (cc.prix_traitement > 0)
                        ec.Montant = cc.prix_traitement * int.Parse(cc.Acte.quantite);
                    else
                        ec.Montant = cc.Acte.prix_acte * int.Parse(cc.Acte.quantite);

                    if (AllComment == 1)
                    {
                        foreach (CommActes ca in cc.ActesSupp)
                        {
                            ec.Montant = ec.Montant + (ca.prix_traitement * ca.Qte);

                        }
                        foreach (CommActes ca in cc.Radios)
                        {
                            ec.Montant = ec.Montant + (ca.prix_traitement * ca.Qte);

                        }
                        foreach (CommActes ca in cc.photos)
                        {
                            ec.Montant = ec.Montant + (ca.prix_traitement * ca.Qte);

                        }
                        foreach (CommMateriel cm in cc.Materiels)
                        {
                            ec.Montant = ec.Montant + (cm.prix_materiel_traitement * cm.Qte);

                        }

                    }
                    this.Montant_Honoraire = ec.Montant;
                    if (LigneVide)
                    {
                        ec.Montant = 0;
                        this.Montant_Honoraire = 0;
                    }
                    // ec.Montant = cc.prix;
                    ec.mutuelle = ec.mutuelle;

                    ec.patient = cc.patient;


                    listech.Add(ec);
                   
                }
             
                //if (cc.prix_traitement > 0)
                //    this.Montant_Honoraire = cc.prix_traitement * Convert.ToDouble(cc.Acte.quantite);
                //else
                //    this.Montant_Honoraire = cc.Acte.prix_acte * Convert.ToDouble(cc.Acte.quantite);
                //this.Montant_Honoraire = cc.prix_traitement;
            }
            this.lstEcheances = listech;
            this.patient = cc.patient;
            // this.IdActe = cc.Acte.id_acte;
            this.IdPatient = cc.IdPatient;
            this.IdComm = cc.Id;


        }
        private static double getPercent(Double prixActe, Double part, Double prixTraitement)
        {
            double percent = 0;
            if (prixActe > 0)
                percent = (((part / prixActe) * 100.0) * prixTraitement) / 100.0;
            return percent;
        }
        public static void getMontantEcheToulon(CommClinique ct, ref double partsecu, ref double parPatient)
        {


            if (ct.prix_traitement > 0)
            {
                double partpatient = ct.Acte.prix_acte - ct.Acte.Remboursement;
                partsecu += getPercent(ct.Acte.prix_acte, ct.Acte.Remboursement, ct.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
                //  partmutuelle += getPercent(ct.Acte.prix_acte, ct.Acte.Remboursement, ct.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
                parPatient += getPercent(ct.Acte.prix_acte, partpatient, ct.prix_traitement) * Convert.ToInt32(ct.Acte.quantite);
            }
            if (ct.Acte.prix_acte == 0)
                parPatient = ct.prix_traitement;

        }
        private List<Echeance> _lstEcheances = null;
        [PropertyCanBeSerialized]
        public List<Echeance> lstEcheances
        {
            get
            {
                return _lstEcheances;
            }
            set
            {
                _lstEcheances = value;
            }
        }


        public string DisplayCodeNVal
        {
            get
            {
                if (Coeff == 1)
                    return prestation.Code;
                if (IsDecomposed)
                    return prestation.Code + CoeffDecompose;
                else
                    return prestation.Code + Coeff;

            }

        }

        private CodePrestation _prestation;
        [PropertyCanBeSerialized]
        public CodePrestation prestation
        {
            get
            {
                return _prestation;
            }
            set
            {
                _prestation = value;
            }
        }

        private TemplateActePG _Template;
        [PropertyCanBeSerialized]
        public TemplateActePG Template
        {
            get
            {
                return _Template;
            }
            set
            {
                _Template = value;
            }
        }
        private Materiel _materiel;
        [PropertyCanBeSerialized]
        public Materiel materiel
        {
            get
            {
                return _materiel;
            }
            set
            {
                _materiel = value;
            }
        }
        private List<Materiel> _lstmateriel;
        [PropertyCanBeSerialized]
        public List<Materiel> lstmateriel
        {
            get
            {
                return _lstmateriel;
            }
            set
            {
                _lstmateriel = value;
            }
        }

        private FeuilleDeSoin _FeuilleDeSoinAssocier = null;
        [PropertyCanBeSerialized]
        public FeuilleDeSoin FeuilleDeSoinAssocier
        {
            get
            {
                return _FeuilleDeSoinAssocier;
            }
            set
            {
                _FeuilleDeSoinAssocier = value;
            }
        }

        private EntentePrealable _DEPAssocier = null;
        [PropertyCanBeSerialized]
        public EntentePrealable DEPAssocier
        {
            get
            {
                return _DEPAssocier;
            }
            set
            {
                _DEPAssocier = value;
                
            }
        }

        private int? _NbMois;
        [PropertyCanBeSerialized]
        public int? NbMois
        {
            get
            {
                return _NbMois;
            }
            set
            {
                _NbMois = value;
            }
        }

        private int? _NbJours;
        [PropertyCanBeSerialized]
        public int? NbJours
        {
            get
            {
                return _NbJours;
            }
            set
            {
                _NbJours = value;
            }
        }
        private double _montantTotalRestant;
        [PropertyCanBeSerialized]
        public double montantTotalRestant
        {
            get
            {
                return _montantTotalRestant;
            }
            set
            {
                _montantTotalRestant = value;
            }
        }
        private double _montantTotal;
        [PropertyCanBeSerialized]
        public double montantTotal
        {
            get
            {
                return _montantTotal;
            }
            set
            {
                _montantTotal = value;
            }
        }
        private string _lastRappel;
        [PropertyCanBeSerialized]
        public string lastRappel
        {
            get
            {
                return _lastRappel;
            }
            set
            {
                _lastRappel = value;
            }
        }
        private int _nbRappelle;
        [PropertyCanBeSerialized]
        public int nbRappelle
        {
            get
            {
                return _nbRappelle;
            }
            set
            {
                _nbRappelle = value;
            }
        }
        private int _CodePlan;
        [PropertyCanBeSerialized]
        public int CodePlan
        {
            get
            {
                return _CodePlan;
            }
            set
            {
                _CodePlan = value;
            }
        }

        private double _Montant_Honoraire;
        [PropertyCanBeSerialized]
        public double Montant_Honoraire
        {
            get
            {
                return _Montant_Honoraire;
            }
            set
            {
                _Montant_Honoraire = value;
            }
        }

        private string _Libelle;
        [PropertyCanBeSerialized]
        public string Libelle
        {
            get
            {

                if (Quantite > 1)
                    return Quantite.ToString() + " x " + _Libelle;      
                else
                    return _Libelle;
            }
            set
            {
                _Libelle = value;
            }
        }

        public etatpaiement PaimentEtat
        {
            get
            {
                etatpaiement res = etatpaiement.Aucun;
                if (lstEcheances == null) return res;
                bool allpayed = true;
                foreach (Echeance ech in lstEcheances)
                {
                    if ((ech.ID_Encaissement > 0)||(ech.ParPrelevement)) res = etatpaiement.Partiel;
                    if ((ech.ID_Encaissement <= 0)&&(!ech.ParPrelevement)) allpayed = false;
                }
                if (allpayed) res = etatpaiement.Complet;
                return res;
            }

        }

       public DateTime DateFinExecution
        {
            get
            {
                if (NbMois!=null && NbJours != null)
                    return _DateExecution.AddMonths(NbMois.Value).AddDays(NbJours.Value);

                if (NbJours != null)
                    return _DateExecution.AddDays(NbJours.Value);

                if (NbMois != null)
                    return _DateExecution.AddMonths(NbMois.Value);

                return _DateExecution;
            }
           
        }

        private DateTime _DateExecution;
        [PropertyCanBeSerialized]
        public DateTime DateExecution
        {
            get
            {
                return _DateExecution;
            }
            set
            {
                _DateExecution = value;
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

        private bool _NeedDEP = false;
        [PropertyCanBeSerialized]
        public bool NeedDEP
        {
            get
            {
                return _NeedDEP;
            }
            set
            {
                _NeedDEP = value;
            }
        }

        private bool _NeedFSE = false;
        [PropertyCanBeSerialized]
        public bool NeedFSE
        {
            get
            {
                return _NeedFSE;
            }
            set
            {
                _NeedFSE = value;
            }
        }

        private string _CoeffDecompose;
        [PropertyCanBeSerialized]
        public string CoeffDecompose
        {
            get
            {
                return _CoeffDecompose;
            }
            set
            {
                _CoeffDecompose = value;
            }
        }

        private bool _IsDecomposed;
        [PropertyCanBeSerialized]
        public bool IsDecomposed
        {
            get
            {
                return _IsDecomposed;
            }
            set
            {
                _IsDecomposed = value;
            }
        }



        public string designation
        {
            get
            {

                if (prestation != null)
                {
                    if (IsDecomposed)
                        return (prestation.Code + CoeffDecompose.ToString());
                    else
                        return prestation.Code + Coeff.ToString();
                }
                return "";
            }

        }


        #region for FSE

        private PyxVitalWrapperConst.Domicile _domicile = PyxVitalWrapperConst.Domicile.N;
        [PropertyCanBeSerialized]
        public PyxVitalWrapperConst.Domicile domicile
        {
            get
            {
                return _domicile;
            }
            set
            {
                _domicile = value;
            }
        }

        private PyxVitalWrapperConst.Qualificatif_depense _motifdepassement = PyxVitalWrapperConst.Qualificatif_depense.Néant;
        [PropertyCanBeSerialized]
        public PyxVitalWrapperConst.Qualificatif_depense motifdepassement
        {
            get
            {
                return _motifdepassement;
            }
            set
            {
                _motifdepassement = value;
            }
        }

        private int _Quantite = 1;
        [PropertyCanBeSerialized]
        public int Quantite
        {
            get
            {
                return _Quantite;
            }
            set
            {
                _Quantite = value;
            }
        }

        private double _rabais = 0;
        [PropertyCanBeSerialized]
        public double rabais
        {
            get
            {
                return _rabais;
            }
            set
            {
                _rabais = value;
            }
        }

        private PyxVitalWrapperConst.RembExceptionel _rembExceptionel = PyxVitalWrapperConst.RembExceptionel.N;
        [PropertyCanBeSerialized]
        public PyxVitalWrapperConst.RembExceptionel rembExceptionel
        {
            get
            {
                return _rembExceptionel;
            }
            set
            {
                _rembExceptionel = value;
            }
        }

        private PyxVitalWrapperConst.SuplCharge _suplCharge = PyxVitalWrapperConst.SuplCharge.N;
        [PropertyCanBeSerialized]
        public PyxVitalWrapperConst.SuplCharge suplCharge
        {
            get
            {
                return _suplCharge;
            }
            set
            {
                _suplCharge = value;
            }
        }

        private PyxVitalWrapperConst.RNO _rno = PyxVitalWrapperConst.RNO.Néant;
        [PropertyCanBeSerialized]
        public PyxVitalWrapperConst.RNO rno
        {
            get
            {
                return _rno;
            }
            set
            {
                _rno = value;
            }
        }

        private PyxVitalWrapperConst.Nuit _nuit = PyxVitalWrapperConst.Nuit.N;
        [PropertyCanBeSerialized]
        public PyxVitalWrapperConst.Nuit nuit
        {
            get
            {
                return _nuit;
            }
            set
            {
                _nuit = value;
            }
        }

        private PyxVitalWrapperConst.Urgence _urgence = PyxVitalWrapperConst.Urgence.N;
        [PropertyCanBeSerialized]
        public PyxVitalWrapperConst.Urgence urgence
        {
            get
            {
                return _urgence;
            }
            set
            {
                _urgence = value;
            }
        }

        private PyxVitalWrapperConst.DimancheEtJF _DimancheEtJF = PyxVitalWrapperConst.DimancheEtJF.N;
        [PropertyCanBeSerialized]
        public PyxVitalWrapperConst.DimancheEtJF DimancheEtJF
        {
            get
            {
                return _DimancheEtJF;
            }
            set
            {
                _DimancheEtJF = value;
            }
        }

        private PyxVitalWrapperConst.ALD _ald = PyxVitalWrapperConst.ALD.N;
        [PropertyCanBeSerialized]
        public PyxVitalWrapperConst.ALD ald
        {
            get
            {
                return _ald;
            }
            set
            {
                _ald = value;
            }
        }

        private PyxVitalWrapperConst.Exoneration _Exoneration = PyxVitalWrapperConst.Exoneration.ExNéant;
        [PropertyCanBeSerialized]
        public PyxVitalWrapperConst.Exoneration Exoneration
        {
            get
            {
                return _Exoneration;
            }
            set
            {
                _Exoneration = value;
            }
        }

        private string _ExonerationLibOuTx = "";
        [PropertyCanBeSerialized]
        public string ExonerationLibOuTx
        {
            get
            {
                return _ExonerationLibOuTx;
            }
            set
            {
                _ExonerationLibOuTx = value;
            }
        }

        private int _Id_FS = -1;
        [PropertyCanBeSerialized]
        public int Id_FS
        {
            get
            {
                if (FeuilleDeSoinAssocier != null) _Id_FS = FeuilleDeSoinAssocier.Id;
                return _Id_FS;
            }
            set
            {
                _Id_FS = value;
            }
        }


        #region DEP



        private int _NumContention = -1;
        [PropertyCanBeSerialized]
        public int NumContention
        {
            get
            {
                return _NumContention;
            }
            set
            {
                _NumContention = value;
            }
        }

        private int _NumSemestre = -1;
        [PropertyCanBeSerialized]
        public int NumSemestre
        {
            get
            {
                return _NumSemestre;
            }
            set
            {
                _NumSemestre = value;
            }
        }

        private int _Id_DEP = -1;
        [PropertyCanBeSerialized]
        public int Id_DEP
        {
            get
            {
                if (DEPAssocier != null) _Id_DEP = DEPAssocier.IdModele;
                return _Id_DEP;
            }
            set
            {
                _Id_DEP = value;
            }
        }

        public PyxVitalWrapperConst.DEP AssocierDEP
        {
            get
            {
                if (_Id_DEP > -1)
                    return PyxVitalWrapperConst.DEP.O;
                else
                    return PyxVitalWrapperConst.DEP.N;
            }

        }




        #endregion

        #region accident
        private DateTime? _DateAccident = null;
        [PropertyCanBeSerialized]
        public DateTime? DateAccident
        {
            get
            {
                return _DateAccident;
            }
            set
            {
                _DateAccident = value;
            }
        }



        private PyxVitalWrapperConst.Ordonnance _ordonnance = PyxVitalWrapperConst.Ordonnance.N;
        [PropertyCanBeSerialized]
        public PyxVitalWrapperConst.Ordonnance ordonnance
        {
            get
            {
                return _ordonnance;
            }
            set
            {
                _ordonnance = value;
            }
        }

        private PyxVitalWrapperConst.Accident _accident = PyxVitalWrapperConst.Accident.N;
        [PropertyCanBeSerialized]
        public PyxVitalWrapperConst.Accident accident
        {
            get
            {
                return _accident;
            }
            set
            {
                _accident = value;
            }
        }
        #endregion

        private string _numdent;
        [PropertyCanBeSerialized]
        public string numdent
        {
            get
            {
                return _numdent;
            }
            set
            {
                _numdent = value;
            }
        }


        private string _NumMutuelle;
        [PropertyCanBeSerialized]
        public string NumMutuelle
        {
            get
            {
                return _NumMutuelle;
            }
            set
            {
                _NumMutuelle = value;
            }
        }

        private PyxVitalWrapperConst.CMU _ActeCMU;
        [PropertyCanBeSerialized]
        public PyxVitalWrapperConst.CMU ActeCMU
        {
            get
            {
                return _ActeCMU;
            }
            set
            {
                _ActeCMU = value;
            }
        }


        private Devis _DevisAssociate = null;
        [PropertyCanBeSerialized]
        public Devis DevisAssociate
        {
            get
            {
                return _DevisAssociate;
            }
            set
            {
                _DevisAssociate = value;
            }
        }

        private int _IdDevisAssociate = -1;
        [PropertyCanBeSerialized]
        public int IdDevisAssociate
        {
            get
            {
                if (DevisAssociate != null)
                    _IdDevisAssociate = DevisAssociate.Id;

                return _IdDevisAssociate;
            }
            set
            {
                _IdDevisAssociate = value;
            }
        }


        private int _IdSurvPlanGestionAssocie = -1;
        [PropertyCanBeSerialized]
        public int IdSurvPlanGestionAssocie
        {
            get
            {
                return _IdSurvPlanGestionAssocie;
            }
            set
            {
                _IdSurvPlanGestionAssocie = value;
            }
        }

        private int _IdSemestrePlanGestionAssocie = -1;
        [PropertyCanBeSerialized]
        public int IdSemestrePlanGestionAssocie
        {
            get
            {
                return _IdSemestrePlanGestionAssocie;
            }
            set
            {
                _IdSemestrePlanGestionAssocie = value;
            }
        }


        #endregion





        #region ICloneable Members



        #endregion

        #region ICloneable Members

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}
