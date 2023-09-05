﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BasCommon_BO
{
    public class CommCliniqueDetailsScenario
    {


        public enum typePeriode
        {
            Unknown,
            Semestre,
            Surveillance,
            Contention
        }

        public typePeriode TypeDePeriode
        {
            get
            {
                switch (numSemestre[0])
                {
                    case 'S': return typePeriode.Semestre;
                    case 'C': return typePeriode.Contention;
                    case 'U': return typePeriode.Surveillance;
                }
                return typePeriode.Unknown;
            }

        }


        private CommClinique _RealComm = null;
        public CommClinique RealComm
        {
            get
            {
                return _RealComm;
            }
            set
            {
                _RealComm = value;
            }
        }

        private int _Numero;
        public int Numero
        {
            get
            {
                return Convert.ToInt32(numSemestre[1].ToString());
            }
            set
            {
                _Numero = value;
            }
        }

        private List<ScenarCommPhoto> _photos = null;
        public List<ScenarCommPhoto> photos
        {
            get
            {
                return _photos;
            }
            set
            {
                _photos = value;
            }
        }

        private List<ScenarCommActes> _actes = null;
        public List<ScenarCommActes> actes
        {
            get
            {
                return _actes;
            }
            set
            {
                _actes = value;
            }
        }

        private List<ScenarCommMateriel> _Materiels = null;
        public List<ScenarCommMateriel> Materiels
        {
            get
            {
                return _Materiels;
            }
            set
            {
                _Materiels = value;
            }
        }


        private List<ScenarCommActes> _ActesSupp = null;
        public List<ScenarCommActes> ActesSupp
        {
            get
            {
                return _ActesSupp;
            }
            set
            {
                _ActesSupp = value;
            }
        }

        private List<ScenarCommActes> _ActesSupp1 = null;
        public List<ScenarCommActes> ActesSupp1
        {
            get
            {
                return _ActesSupp1;
            }
            set
            {
                _ActesSupp1 = value;
            }
        }

        private List<ScenarCommRadio> _Radios = null;
        public List<ScenarCommRadio> Radios
        {
            get
            {
                return _Radios;
            }
            set
            {
                _Radios = value;
            }
        }

        private List<ScenarEnBouche> _EnBouches = null;
        public List<ScenarEnBouche> EnBouches
        {
            get
            {
                return _EnBouches;
            }
            set
            {
                _EnBouches = value;
            }
        }




        private int _IdScenario;
        public int IdScenario
        {
            get
            {
                return _IdScenario;
            }
            set
            {
                _IdScenario = value;
            }
        }



        private CommCliniqueDetailsScenario _ParentAfaire = null;
        public CommCliniqueDetailsScenario ParentAfaire
        {
            get
            {
                return _ParentAfaire;
            }
            set
            {
                _ParentAfaire = value;
            }
        }

        private int _IdParentAFaire;
        public int IdParentAFaire
        {
            get
            {
                if (ParentAfaire != null) _IdParentAFaire = ParentAfaire.Id;
                return _IdParentAFaire;
            }
            set
            {
                _IdParentAFaire = value;
            }
        }

        private bool _IsReferenceDate;
        public bool IsReferenceDate
        {
            get
            {
                return _IsReferenceDate;
            }
            set
            {
                _IsReferenceDate = value;
            }
        }

        private int _IdParent;
        public int IdParent
        {
            get
            {
                return _IdParent;
            }
            set
            {
                _IdParent = value;
            }
        }

        private int _Ordre;
        public int Ordre
        {
            get
            {
                return _Ordre;
            }
            set
            {
                _Ordre = value;
            }
        }

        private string _numSemestre;
        public string numSemestre
        {
            get
            {
                return _numSemestre;
            }
            set
            {
                _numSemestre = value;
            }
        }

        private int _NbMois;
        public int NbMois
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

        private int _NbJours;
        public int NbJours
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



        private Acte _Acte = null;
        public Acte Acte
        {
            get
            {
                return _Acte;
            }
            set
            {
                _Acte = value;
            }
        }

        private int _IdActe = -1;
        public int IdActe
        {
            get
            {
                if (Acte != null) IdActe = Acte.id_acte;
                return _IdActe;
            }
            set
            {
                _IdActe = value;
            }
        }


        //Materiel
        private Materiel _Materiel = null;
        public Materiel Materiel
        {
            get
            {
                return _Materiel;
            }
            set
            {
                _Materiel = value;
            }
        }

        private int _IdMateriel = -1;
        public int IdMateriel
        {
            get
            {
                if (Materiel != null) IdMateriel = Materiel.id_materiel;
                return _IdMateriel;
            }
            set
            {
                _IdMateriel = value;
            }
        }


        //Fin Materiel
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


        private string _CommentaireAFaire;
        public string CommentaireAFaire
        {
            get
            {
                return _CommentaireAFaire;
            }
            set
            {
                _CommentaireAFaire = value;
            }
        }
    }


    public class CommClinique
    {

        public CommClinique()
        {
           // this.
            this.ActesSupp = new List<CommActes>();
            this.Radios = new List<CommActes>();
            this.photos = new List<CommActes>();
            this.Materiels = new List<CommMateriel>();
         //   this.Smilers = new List<InfoSmilers>();



        }
        public class DateComparer : IComparer<CommClinique>
        {

            public int Compare(CommClinique x, CommClinique y)
            {
                return ((x.DatePrevisionnnelle != null) && (y.DatePrevisionnnelle != null)) ? x.DatePrevisionnnelle.Value.CompareTo(y.DatePrevisionnnelle.Value) : 0;
            }
        }
        private string _dents;
        public string dents
        {
            get
            {
                return _dents;
            }
            set
            {
                _dents = value;
            }
        }
        public enum EtatCommentaire
        {
            Afaire = 0,
            Prevus = 1,
            EnCours = 2,
            Termine = 3,
            Undefined = -1

        }

        public enum ModeCreation
        {
            Manuel,
            Auto

        }

        private List<TempEcheanceDefinition> _echeancestemp = null;
        [PropertyCanBeSerialized]
        public List<TempEcheanceDefinition> echeancestemp
        {
            get
            {
                return _echeancestemp;
            }
            set
            {
                _echeancestemp = value;
            }
        }

        private ModeCreation _modecreation = ModeCreation.Manuel;
        [PropertyCanBeSerialized]
        public ModeCreation modecreation
        {
            get
            {
                return _modecreation;
            }
            set
            {
                _modecreation = value;
            }
        }

        private bool _IsDateDeRef = false;
        [PropertyCanBeSerialized]
        public bool IsDateDeRef
        {
            get
            {
                return _IsDateDeRef;
            }
            set
            {
                _IsDateDeRef = value;
            }
        }
        private int _echeance;
        [PropertyCanBeSerialized]
        public int echeance
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

        private EtatCommentaire _etat = EtatCommentaire.Undefined;
        [PropertyCanBeSerialized]
        public EtatCommentaire etat
        {
            get
            {
                if (_etat == EtatCommentaire.Undefined) _etat = date == null ? EtatCommentaire.Prevus : EtatCommentaire.Termine;
                return _etat;
            }
            set
            {
                _etat = value;
            }
        }

       


        private int _Hygiene = 0;
        [PropertyCanBeSerialized]
        public int Hygiene
        {
            get
            {
                return _Hygiene;
            }
            set
            {
                _Hygiene = value;
            }
        }



        private int _IdSemestre = -1;
        public int IdSemestre
        {
            get
            {
                return _IdSemestre;
            }
            set
            {
                _IdSemestre = value;
            }
        }

        private int _IdScenario = -1;
        public int IdScenario
        {
            get
            {
                return _IdScenario;
            }
            set
            {
                _IdScenario = value;
            }
        }

        private int _NbMois;
        [PropertyCanBeSerialized]
        public int NbMois
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
       

        private int _NbJours;
        [PropertyCanBeSerialized]
        public int NbJours
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
        private double _rabais;
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
        private List<CommDentAExtraire> _DentsAExtraire = null;
        [PropertyCanBeSerialized]
        public List<CommDentAExtraire> DentsAExtraire
        {
            get
            {
                return _DentsAExtraire;
            }
            set
            {
                _DentsAExtraire = value;
            }
        }

        private List<CommAutrePersonne> _AutrePersonnes = null;
        [PropertyCanBeSerialized]
        public List<CommAutrePersonne> AutrePersonnes
        {
            get
            {
                return _AutrePersonnes;
            }
            set
            {
                _AutrePersonnes = value;
            }
        }

        private List<CommActes> _photos = null;
        [PropertyCanBeSerialized]
        public List<CommActes> photos
        {
            get
            {
                return _photos;
            }
            set
            {
                _photos = value;
            }
        }

        private List<CommMateriel> _Materiels = null;
        [PropertyCanBeSerialized]
        public List<CommMateriel> Materiels
        {
            get
            {
                return _Materiels;
            }
            set
            {
                _Materiels = value;
            }
        }

        private List<CommActes> _ActesSupp = null;
        [PropertyCanBeSerialized]
        public List<CommActes> ActesSupp
        {
            get
            {
                return _ActesSupp;
            }
            set
            {
                _ActesSupp = value;
            }
        }
        private List<CommActes> _ActesSupp1 = null;
        [PropertyCanBeSerialized]
        public List<CommActes> ActesSupp1
        {
            get
            {
                return _ActesSupp1;
            }
            set
            {
                _ActesSupp1 = value;
            }
        }
        private List<CommActes> _Radios = null;
        [PropertyCanBeSerialized]
        public List<CommActes> Radios
        {
            get
            {
                return _Radios;
            }
            set
            {
                _Radios = value;
            }
        }





        private DateTime? _DatePrevisionnnelle;
        [PropertyCanBeSerialized]
        public DateTime? DatePrevisionnnelle
        {
            get
            {
                return (this.date == null) ? _DatePrevisionnnelle : date;
            }
            set
            {
                _DatePrevisionnnelle = value;
            }
        }

        private DateTime? _date;
        [PropertyCanBeSerialized]
        public DateTime? date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                if (DatePrevisionnnelle == null)
                    DatePrevisionnnelle = date;
            }
        }

        private int _Id_devis = -1;
        public int Id_devis
        {
            get
            {
                return _Id_devis;
            }
            set
            {
                _Id_devis = value;
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

        private int _IdRDV = -1;
        public int IdRDV
        {
            get
            {
                if (Appointement != null) _IdRDV = Appointement.Id;
                return _IdRDV;
            }
            set
            {
                _IdRDV = value;
            }
        }

        private int _IdActe = -1;
        public int IdActe
        {
            get
            {
                if (Acte != null) IdActe = Acte.id_acte;
                return _IdActe;
            }
            set
            {
                _IdActe = value;
            }
        }

        private int _IdSecretaire = -1;
        public int IdSecretaire
        {
            get
            {
                if (Secretaire != null) _IdSecretaire = Secretaire.Id;
                return _IdSecretaire;
            }
            set
            {
                _IdSecretaire = value;
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

        private int _IdAssistante = -1;
        public int IdAssistante
        {
            get
            {
                if (Assistante != null) _IdAssistante = Assistante.Id;
                return _IdAssistante;
            }
            set
            {
                _IdAssistante = value;
            }
        }

        private int _IdPraticien = -1;
        public int IdPraticien
        {
            get
            {
                if (praticien != null) _IdPraticien = praticien.Id;
                return _IdPraticien;
            }
            set
            {
                _IdPraticien = value;
            }
        }



        private int _IdParentComment = -1;
        public int IdParentComment
        {
            get
            {
                if (ParentComment != null) _IdParentComment = ParentComment.Id;
                return _IdParentComment;
            }
            set
            {
                _IdParentComment = value;
            }
        }

        private CommClinique _ParentComment = null;
        [PropertyCanBeSerialized]
        public CommClinique ParentComment
        {
            get
            {
                return _ParentComment;
            }
            set
            {
                _ParentComment = value;
            }
        }

        private Utilisateur _praticien;
        [PropertyCanBeSerialized]
        public Utilisateur praticien
        {
            get
            {
                return _praticien;
            }
            set
            {
                _praticien = value;
            }
        }

        private Utilisateur _Assistante;
        [PropertyCanBeSerialized]
        public Utilisateur Assistante
        {
            get
            {
                return _Assistante;
            }
            set
            {
                _Assistante = value;
            }
        }

        private Utilisateur _Secretaire;
        [PropertyCanBeSerialized]
        public Utilisateur Secretaire
        {
            get
            {
                return _Secretaire;
            }
            set
            {
                _Secretaire = value;
            }
        }

        private Acte _Acte;
        [PropertyCanBeSerialized]
        public Acte Acte
        {
            get
            {
                return _Acte;
            }
            set
            {
                _Acte = value;
            }
        }
        private Boolean _desactive;
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
        private basePatient _patient = null;
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
                if ((Appointement != null) && (Appointement.IdPatient == -1))
                {
                    Appointement.IdPatient = value.Id;
                    Appointement.PatientName = value.ToShortString();
                }
            }
        }

        private RHAppointment _Appointement;
        [PropertyCanBeSerialized]
        public RHAppointment Appointement
        {
            get
            {
                return _Appointement;
            }
            set
            {
                _Appointement = value;
            }
        }

        private string _Commentaire = "";
        [PropertyCanBeSerialized]
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
        private string _VU = "";
        [PropertyCanBeSerialized]
        public string Vu
        {
            get
            {
                return _VU;
            }
            set
            {
                _VU = value;
            }
        }
        private string _Donne = "";
        [PropertyCanBeSerialized]
        public string Donne
        {
            get
            {
                return _Donne;
            }
            set
            {
                _Donne = value;
            }
        }

        private string _CommentaireAFaire = "";
        [PropertyCanBeSerialized]
        public string CommentaireAFaire
        {
            get
            {
                return _CommentaireAFaire;
            }
            set
            {
                _CommentaireAFaire = value;
            }
        }



        private Boolean _isfulltime=false;
        [PropertyCanBeSerialized]
        public Boolean isfulltime
        { 
            get
            {
             return  _isfulltime ;
            }
            set
            {
                _isfulltime =value;
               

            }
        }

        private double _prix;
        [PropertyCanBeSerialized]
        public double prix
        {
            get
            {
                return _prix;
            }
            set
            {
                _prix = value;
            }
        }

        private double _prix_traitement;
        [PropertyCanBeSerialized]
        public double prix_traitement
        {
            get
            {
                //double vPrix = 0;
                //vPrix = vPrix + this.prix_traitement * Convert.ToInt32(this.Acte.quantite);
                //foreach (CommActes cc in this.ActesSupp)
                //    vPrix = vPrix + cc.prix_traitement * cc.Qte;
                //foreach (CommActes cc in this.Radios)
                //    vPrix = vPrix + cc.prix_traitement * cc.Qte;
                //foreach (CommActes cc in this.photos)
                //    vPrix = vPrix + cc.prix_traitement * cc.Qte;


                return _prix_traitement;
            }
            set
            {
                _prix_traitement = value;
            }
        }
        private Autre _tour;
        [PropertyCanBeSerialized]
        public Autre tour
        {
            get
            {
                return _tour;
            }
            set
            {
                _tour = value;
            }
        }
        private int _iDtour;
        [PropertyCanBeSerialized]
        public int idTour
        {
            get
            {
                return _iDtour;
            }
            set
            {
                _iDtour = value;
            }
        }
        private int _iDPortGouttier;
        [PropertyCanBeSerialized]
        public int iDPortGouttier
        {
            get
            {
                return _iDPortGouttier;
            }
            set
            {
                _iDPortGouttier = value;
            }
        }
        private Autre _chgt;
        [PropertyCanBeSerialized]
        public Autre chgt
        {
            get
            {
                return _chgt;
            }
            set
            {
                _chgt = value;
            }
        }
        private int _iDchgt;
        [PropertyCanBeSerialized]
        public int idChgt
        {
            get
            {
                return _iDchgt;
            }
            set
            {
                _iDchgt = value;
            }
        }
        private Autre _tim;
        [PropertyCanBeSerialized]
        public Autre tim
        {
            get
            {
                return _tim;
            }
            set
            {
                _tim = value;
            }
        }
        private int _iDtim;
        [PropertyCanBeSerialized]
        public int idTim
        {
            get
            {
                return _iDtim;
            }
            set
            {
                _iDtim = value;
            }
        }
        private Autre _numLogiciel;
        [PropertyCanBeSerialized]
        public Autre numLogiciel
        {
            get
            {
                return _numLogiciel;
            }
            set
            {
                _numLogiciel = value;
            }
        }
        private int _iDnumLogiciel;
        [PropertyCanBeSerialized]
        public int idNumLogiciel
        {
            get
            {
                return _iDnumLogiciel;
            }
            set
            {
                _iDnumLogiciel = value;
            }
        }
        private Autre _droit;
        [PropertyCanBeSerialized]
        public Autre droit
        {
            get
            {
                return _droit;
            }
            set
            {
                _droit = value;
            }
        }
        private int _iDdroit;
        [PropertyCanBeSerialized]
        public int idDroit
        {
            get
            {
                return _iDdroit;
            }
            set
            {
                _iDdroit = value;
            }
        }
        private Autre _gauche;
        [PropertyCanBeSerialized]
        public Autre gauche
        {
            get
            {
                return _gauche;
            }
            set
            {
                _gauche = value;
            }
        }
        private int _iDgauche;
        [PropertyCanBeSerialized]
        public int idGauche
        {
            get
            {
                return _iDgauche;
            }
            set
            {
                _iDgauche = value;
            }
        }

        private int _iDblanchiment;
        [PropertyCanBeSerialized]
        public int idBlanchiment
        {
            get
            {
                return _iDblanchiment;
            }
            set
            {
                _iDblanchiment = value;
            }
        }
        private Autre _diaporama;
        [PropertyCanBeSerialized]
        public Autre diaporama
        {
            get
            {
                return _diaporama;
            }
            set
            {
                _diaporama = value;
            }
        }
        private int _IDdiaporama;
        [PropertyCanBeSerialized]
        public int idDiaporama
        {
            get
            {
                return _IDdiaporama;
            }
            set
            {
                _IDdiaporama = value;
            }
        }
        private Autre _Accelerateur;
        [PropertyCanBeSerialized]
        public Autre Accelerateur
        {
            get
            {
                return _Accelerateur;
            }
            set
            {
                _Accelerateur = value;
            }
        }
        private int _iDAccelerateur;
        [PropertyCanBeSerialized]
        public int idAccelerateur
        {
            get
            {
                return _iDAccelerateur;
            }
            set
            {
                _iDAccelerateur = value;
            }
        }


    }



    public class CommDentAExtraire
    {


        private int _IdComm;
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

        private CommClinique _Parent;
        [PropertyCanBeSerialized]
        public CommClinique Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                _Parent = value;
            }
        }

        private string _dents;
        [PropertyCanBeSerialized]
        public string dents
        {
            get
            {
                return _dents;
            }
            set
            {
                _dents = value;
            }
        }

    }


    public class CommAutrePersonne
    {

        public override string ToString()
        {
            return Nom + " " + Prenom;
        }

        private int _IdComm;
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

        private string _Prenom;
        [PropertyCanBeSerialized]
        public string Prenom
        {
            get
            {
                return _Prenom;
            }
            set
            {
                _Prenom = value;
            }
        }

        private string _Nom;
        [PropertyCanBeSerialized]
        public string Nom
        {
            get
            {
                return _Nom;
            }
            set
            {
                _Nom = value;
            }
        }

        private int _IdCorrespondant;
        public int IdCorrespondant
        {
            get
            {
                if (correspondant != null) _IdCorrespondant = correspondant.Id;
                return _IdCorrespondant;
            }
            set
            {
                _IdCorrespondant = value;
            }
        }

        private CommClinique _Parent;
        [PropertyCanBeSerialized]
        public CommClinique Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                _Parent = value;
            }
        }

        private baseSmallPersonne _correspondant;
        [PropertyCanBeSerialized]
        public baseSmallPersonne correspondant
        {
            get
            {
                return _correspondant;
            }
            set
            {
                _correspondant = value;
            }
        }

    }

    public class ScenarCommRadio : CommRadio
    {

        private new CommCliniqueDetailsScenario _Parent;
        public new CommCliniqueDetailsScenario Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                _Parent = value;
            }
        }
    }

    public class CommRadio
    {


        private CommClinique _Parent;
        [PropertyCanBeSerialized]
        public CommClinique Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                _Parent = value;
            }
        }


        public enum TypeRadio
        {
            Pano_Diagnostique,
            Pano_control,
            Pano_surveillance,
            Profil,
            Facial,
            Axial,
            Scanner,
            IntraAlveolaire
        }


        private int _IdComm;
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

        private TypeRadio _typeradio;
        [PropertyCanBeSerialized]
        public TypeRadio typeradio
        {
            get
            {
                return _typeradio;
            }
            set
            {
                _typeradio = value;
            }
        }

    }


    public class ScenarCommPhoto : CommPhoto
    {

        private new CommCliniqueDetailsScenario _Parent;
        public new CommCliniqueDetailsScenario Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                _Parent = value;
            }
        }
    }

    public class CommPhoto
    {


        private CommClinique _Parent;
        [PropertyCanBeSerialized]
        public CommClinique Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                _Parent = value;
            }
        }


        public enum TypePhoto
        {
            IntraGauche,
            IntraDroite,
            IntraFace,
            IntraMaxilaire,
            IntraMandibulaire,
            IntraSurplomb,
            IntraSourire,
            ExtProfil,
            ExtProfilSourire,
            ExtFace,
            ExtFaceSourire,
            Ext34,
            MoulageGauche,
            MoulageDroite,
            MoulageFace,
            MoulageMaxilaire,
            MoulageMandibulaire
        }


        private int _IdComm;
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

        private TypePhoto _typephoto;
        [PropertyCanBeSerialized]
        public TypePhoto typephoto
        {
            get
            {
                return _typephoto;
            }
            set
            {
                _typephoto = value;
            }
        }

    }



    public class ScenarCommActes : CommActes
    {

        private new CommCliniqueDetailsScenario _Parent;
        public new CommCliniqueDetailsScenario Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                _Parent = value;
            }
        }
    }

    public class ScenarCommMateriel : CommMateriel
    {

        private new CommCliniqueDetailsScenario _Parent;
        public new CommCliniqueDetailsScenario Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                _Parent = value;
            }
        }
    }


    public class CommActes
    {


        private CommClinique _Parent;
        [PropertyCanBeSerialized]
        public CommClinique Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                _Parent = value;
            }
        }

        private int _IdComm;
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

        private string _typeActeSupp;
        [PropertyCanBeSerialized]
        public string typeActeSupp
        {
            get
            {
                return _typeActeSupp;
            }
            set
            {
                _typeActeSupp = value;
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

        private string _LibActe_estimation;
        [PropertyCanBeSerialized]
        public string LibActe_estimation
        {
            get
            {
                return _LibActe_estimation;
            }
            set
            {
                _LibActe_estimation = value;
            }
        }

        private string _LibActe_facture;
        [PropertyCanBeSerialized]
        public string LibActe_facture
        {
            get
            {
                return _LibActe_facture;
            }
            set
            {
                _LibActe_facture = value;
            }
        }


        private string _LibActe;
        [PropertyCanBeSerialized]
        public string LibActe
        {
            get
            {
                return _LibActe;
            }
            set
            {
                _LibActe = value;
            }
        }

        private string _ShortLib;
        [PropertyCanBeSerialized]
        public string ShortLib
        {
            get
            {
                return _ShortLib;
            }
            set
            {
                _ShortLib = value;
            }
        }


        private int _Qte;
        [PropertyCanBeSerialized]
        public int Qte
        {
            get
            {
                return _Qte;
            }
            set
            {
                _Qte = value;
            }
        }

        private double _Rabais;
        [PropertyCanBeSerialized]
        public double rabais
        {
            get
            {
                return _Rabais;
            }
            set
            {
                _Rabais = value;
            }
        }
        private Color _acte_couleur;
        [PropertyCanBeSerialized]
        public Color acte_couleur
        {
            get
            {
                return _acte_couleur;
            }
            set
            {
                _acte_couleur = value;
            }
        }
        private int _acte_durestd;
        [PropertyCanBeSerialized]
        public int acte_durestd
        {
            get
            {
                return _acte_durestd;
            }
            set
            {
                _acte_durestd = value;
            }
        }

        private string _nombre_points;
        [PropertyCanBeSerialized]
        public string nombre_points
        {
            get
            {
                return _nombre_points;
            }
            set
            {
                _nombre_points = value;
            }
        }
        private string _nomenclature;
        [PropertyCanBeSerialized]
        public string nomenclature
        {
            get
            {
                return _nomenclature;
            }
            set
            {
                _nomenclature = value;
            }
        }

        private string _cotation;
        [PropertyCanBeSerialized]
        public string cotation
        {
            get
            {
                return _cotation;
            }
            set
            {
                _cotation = value;
            }
        }
        private int _coefficient;
        [PropertyCanBeSerialized]
        public int coefficient
        {
            get
            {
                return _coefficient;
            }
            set
            {
                _coefficient = value;
            }
        }


        private double _prix_acte;
        [PropertyCanBeSerialized]
        public double prix_acte
        {
            get
            {
                return _prix_acte;
            }
            set
            {
                _prix_acte = value;
            }
        }
        private double _prix_traitement;
        [PropertyCanBeSerialized]
        public double prix_traitement
        {
            get
            {
                return _prix_traitement;
            }
            set
            {
                _prix_traitement = value;
            }
        }
        private Boolean _desactive;
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
        private int _echeancestemp = 1;
        [PropertyCanBeSerialized]
        public int echeancestemp
        {
            get
            {
                return _echeancestemp;
            }
            set
            {
                _echeancestemp = value;
            }
        }

        private int _idencaissement;
        [PropertyCanBeSerialized]
        public int idencaissement
        {
            get
            {
                return _idencaissement;
            }
            set
            {
                _idencaissement = value;
            }
        }

        private double _BaseRemboursement;
        [PropertyCanBeSerialized]
        public double BaseRemboursement
        {
            get
            {
                return _BaseRemboursement;
            }
            set
            {
                _BaseRemboursement = value;
            }
        }
        private double _Remboursement;
        [PropertyCanBeSerialized]
        public double Remboursement
        {
            get
            {
                return _Remboursement;
            }
            set
            {
                _Remboursement = value;
            }
        }
        private double _RembMutuelle;
        [PropertyCanBeSerialized]
        public double RembMutuelle
        {
            get
            {
                return _RembMutuelle;
            }
            set
            {
                _RembMutuelle = value;
            }
        }
        private double _partPatient;
        [PropertyCanBeSerialized]
        public double partPatient
        {
            get
            {
                return _partPatient;
            }
            set
            {
                _partPatient = value;
            }
        }
        private double _Depassement;
        [PropertyCanBeSerialized]
        public double Depassement
        {
            get
            {
                return _Depassement;
            }
            set
            {
                _Depassement = value;
            }
        }
        private double _Tarif;
        [PropertyCanBeSerialized]
        public double Tarif
        {
            get
            {
                return _Tarif;
            }
            set
            {
                _Tarif = value;
            }
        }
        private string _CodeTransposition;
        [PropertyCanBeSerialized]
        public string CodeTransposition
        {
            get
            {
                return _CodeTransposition;
            }
            set
            {
                _CodeTransposition = value;
            }
        }
        public CommActes()
        {
        }
        public CommActes(Acte a)
        {
            this.acte_couleur = a.acte_couleur;
            this.prix_acte = a.prix_acte;
            this.LibActe = a.acte_libelle;
            this.ShortLib = a.nom_raccourci;
            this.LibActe_estimation = a.acte_libelle_estimation;
            this.LibActe_facture = a.acte_libelle_facture;
            this.ShortLib = a.nom_raccourci;
            this.prix_traitement = a.prix_acte * Convert.ToDouble(a.quantite);
            this.Qte = Convert.ToInt32(a.quantite);
            this.IdActe = a.id_acte;
            this.acte_durestd = a.acte_durestd;
            //this. = a.IdBaseProduit;
            //this.idMateriel = a.idMateriel;
            //this.Libelle = a.Libelle;
            //this.materiel_couleur = a.materiel_couleur;
            //this.prix_materiel = a.prix_materiel;
            //this.Qte = a.Qte;
            //this.desactive = a.desactive;

        }
        public CommActes(ActeGroupement a)
        {
            this.acte_couleur = a.acte_couleur;
            this.prix_acte = a.prix_acte;
            this.LibActe = a.acte_libelle;
            this.ShortLib = a.nom_raccourci;
            this.LibActe_estimation = a.acte_libelle_estimation;
            this.LibActe_facture = a.acte_libelle_facture;
            this.ShortLib = a.nom_raccourci;
            this.prix_traitement = a.prixTraitement * Convert.ToDouble(a.qte);
            this.Qte = Convert.ToInt32(a.qte);
            this.IdActe = a.id_acte;
            this.acte_durestd = a.acte_durestd;

        }
    }

    public class commAutre
    {
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

    }
    public class CommMateriel
    {

        private int _IdComm;
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

        private string _ShortLib = "";
        [PropertyCanBeSerialized]
        public string ShortLib
        {
            get
            {
                return _ShortLib;
            }
            set
            {
                _ShortLib = value;
            }
        }

        private Color _materiel_couleur;
        [PropertyCanBeSerialized]
        public Color materiel_couleur
        {
            get
            {
                return _materiel_couleur;
            }
            set
            {
                _materiel_couleur = value;
            }
        }

        private int _Qte;
        [PropertyCanBeSerialized]
        public int Qte
        {
            get
            {
                return _Qte;
            }
            set
            {
                _Qte = value;
            }
        }
        private double _rabais;
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
        private int _idMateriel;
        [PropertyCanBeSerialized]
        public int idMateriel
        {
            get
            {
                return _idMateriel;
            }
            set
            {
                _idMateriel = value;
            }
        }

        private FamillesMateriels _Famille;
        [PropertyCanBeSerialized]
        public FamillesMateriels Famille
        {
            get
            {
                return _Famille;
            }
            set
            {
                _Famille = value;
            }
        }

        private string _Libelle = "";
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

        private CommClinique _Parent;
        [PropertyCanBeSerialized]
        public CommClinique Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                _Parent = value;
            }
        }

        private int _IdBaseProduit = -1;
        [PropertyCanBeSerialized]
        public int IdBaseProduit
        {
            get
            {
                return _IdBaseProduit;
            }
            set
            {
                _IdBaseProduit = value;
            }
        }


        private double _prix_materiel;
        [PropertyCanBeSerialized]
        public double prix_materiel
        {
            get
            {
                return _prix_materiel;
            }
            set
            {
                _prix_materiel = value;
            }
        }

        private double _prix_materiel_traitement;
        [PropertyCanBeSerialized]
        public double prix_materiel_traitement
        {
            get
            {
                return _prix_materiel_traitement;
            }
            set
            {
                _prix_materiel_traitement = value;
            }
        }

        private int _echeancestemp;
        [PropertyCanBeSerialized]
        public int echeancestemp
        {
            get
            {
                return _echeancestemp;
            }
            set
            {
                _echeancestemp = value;
            }
        }
        private List<TempEcheanceDefinition> _Listecheancestemp = null;
        [PropertyCanBeSerialized]
        public List<TempEcheanceDefinition> Listecheancestemp
        {
            get
            {
                return _Listecheancestemp;
            }
            set
            {
                _Listecheancestemp = value;
            }
        }
        private int _idencaissement;
        [PropertyCanBeSerialized]
        public int idencaissement
        {
            get
            {
                return _idencaissement;
            }
            set
            {
                _idencaissement = value;
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
        public CommMateriel()
        {
        }
        public CommMateriel(Materiel a)
        {
            this.prix_materiel_traitement = a.prix_materiel;
            this.ShortLib = a.materiel_shortlib;
            this.idMateriel = a.id_materiel;
            this.Libelle = a.materiel_libelle;
            this.materiel_couleur = a.materiel_couleur;
            this.prix_materiel = a.prix_materiel;

            this.Qte = a.Qte;

        }
    }
    public class CommMaterielTraitement : CommMateriel
    {
        public CommMaterielTraitement()
        {
        }
        public CommMaterielTraitement(CommMaterielTraitement a)
        {
            this.prix_traitement = a.prix_traitement;
            this.prix_materiel_traitement = a.prix_traitement;
            this.IdComm = a.IdComm;
            this.Parent = a.Parent;
            this.ShortLib = a.ShortLib;
            this.IdBaseProduit = a.IdBaseProduit;
            this.idMateriel = a.idMateriel;
            this.Libelle = a.Libelle;
            this.materiel_couleur = a.materiel_couleur;
            this.prix_materiel = a.prix_materiel;
            this.desactive = a.desactive;
            //coef
            //    nom
            //    cot
            this.Qte = a.Qte;


        }
        public CommMaterielTraitement(Materiel a)
        {
            this.prix_traitement = a.prix_materiel;
            this.prix_materiel_traitement = a.prix_materiel;
            this.ShortLib = a.materiel_shortlib;
            this.idMateriel = a.id_materiel;
            this.Libelle = a.materiel_libelle;
            this.materiel_couleur = a.materiel_couleur;
            this.prix_materiel = a.prix_materiel;
            //coef
            //    nom
            //    cot
            this.Qte = a.Qte;


        }
        public CommMaterielTraitement(CommMateriel a)
        {
            this.prix_traitement = a.prix_materiel;
            this.IdComm = a.IdComm;
            this.Parent = a.Parent;
            this.ShortLib = a.ShortLib;
            this.IdBaseProduit = a.IdBaseProduit;
            this.idMateriel = a.idMateriel;
            this.Libelle = a.Libelle;
            this.materiel_couleur = a.materiel_couleur;
            this.prix_materiel = a.prix_materiel;
            this.Qte = a.Qte;
            this.desactive = a.desactive;

        }

        private double _prix_traitement;
        [PropertyCanBeSerialized]
        public double prix_traitement
        {
            get
            {
                return _prix_traitement;
            }
            set
            {
                _prix_traitement = value;
            }
        }

    }

    public class CommMaterielDevis : CommMaterielTraitement
    {
        public CommMaterielDevis()
        {
        }
        public CommMaterielDevis(CommMaterielTraitement a)
        {
            this.prix_devis_propose = a.prix_traitement;
            this.prix_devis = prix_devis_propose;

            this.IdComm = a.IdComm;
            this.Parent = a.Parent;
            this.ShortLib = a.ShortLib;
            this.IdBaseProduit = a.IdBaseProduit;
            this.idMateriel = a.idMateriel;
            this.Libelle = a.Libelle;
            this.materiel_couleur = a.materiel_couleur;
            this.prix_materiel = a.prix_materiel;
            this.Qte = a.Qte;
            this.desactive = a.desactive;

        }
        private double _prix_devis_propose;
        [PropertyCanBeSerialized]
        public double prix_devis_propose
        {
            get
            {
                return _prix_devis_propose;
            }
            set
            {
                _prix_devis_propose = value;
            }
        }
        private double _prix_devis;
        [PropertyCanBeSerialized]
        public double prix_devis
        {
            get
            {
                return _prix_devis;
            }
            set
            {
                _prix_devis = value;
            }
        }
    }



    public class ScenarEnBouche
    {


        private bool _Bas;
        public bool Bas
        {
            get
            {
                return _Bas;
            }
            set
            {
                _Bas = value;
            }
        }

        private bool _Haut;
        public bool Haut
        {
            get
            {
                return _Haut;
            }
            set
            {
                _Haut = value;
            }
        }

        private int _IdAppareil = -1;
        public int IdAppareil
        {
            get
            {
                return _IdAppareil;
            }
            set
            {
                _IdAppareil = value;
            }
        }

        private int _IdCommFin = -1;
        public int IdCommFin
        {
            get
            {
                return _IdCommFin;
            }
            set
            {
                _IdCommFin = value;
            }
        }

        private int _IdCommDebut = -1;
        public int IdCommDebut
        {
            get
            {
                return _IdCommDebut;
            }
            set
            {
                _IdCommDebut = value;
            }
        }

        private string _Dents;
        public string Dents
        {
            get
            {
                return _Dents;
            }
            set
            {
                _Dents = value;
            }
        }

        private BasCommon_BO.ElementsEnBouche.BO.ElementDent.Materials _type;
        public BasCommon_BO.ElementsEnBouche.BO.ElementDent.Materials type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
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

        private new CommCliniqueDetailsScenario _Parent;
        public new CommCliniqueDetailsScenario Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                _Parent = value;
            }
        }




    }



    public class CommActesTraitement : CommActes
    {
        public CommActesTraitement()
        {
        }
        public CommActesTraitement(CommActesTraitement a)
        {
            this.prix_acte = a.prix_acte;
            this.prix_traitement = a.prix_traitement;
            this.prix_traitement2 = a.prix_traitement;

            this.acte_couleur = a.acte_couleur;
            this.acte_durestd = a.acte_durestd;
            this.prix_acte = a.prix_acte;
            this.prix_traitement = a.prix_traitement;
            this.IdActe = a.IdActe;
            this.IdComm = a.IdComm;
            this.LibActe = a.LibActe;
            this.Parent = a.Parent;
            this.ShortLib = a.ShortLib;

            this.Qte = a.Qte;
            this.BaseRemboursement = a.BaseRemboursement;
            this.Remboursement = a.Remboursement;
            this.partPatient = a.partPatient;
            this.RembMutuelle = a.RembMutuelle;
            this.CodeTransposition = a.CodeTransposition;
            this.Depassement = a.Depassement;
            this.Tarif = a.Tarif;
            this.desactive = a.desactive;
            if (this.desactive)
                this.echeancestemp = 0;

        }
        public CommActesTraitement(Acte a)
        {
            this.prix_acte = a.prix_acte;
            this.acte_couleur = a.acte_couleur;
            this.acte_durestd = a.acte_durestd;
            this.prix_traitement = a.prix_acte;
            this.IdActe = a.id_acte;
            this.LibActe = a.acte_libelle;
            this.ShortLib = a.nom_raccourci;
            this.Qte = Convert.ToInt32(a.quantite);


        }
        public CommActesTraitement(ActeGroupement a)
        {
            this.prix_acte = a.prix_acte;
            this.acte_couleur = a.acte_couleur;
            this.acte_durestd = a.acte_durestd;
            this.prix_traitement = a.prixTraitement;
            this.IdActe = a.id_acte;
            this.LibActe = a.acte_libelle;
            this.ShortLib = a.nom_raccourci;
            this.Qte = Convert.ToInt32(a.qte);


        }
        public CommActesTraitement(CommActes a)
        {

            this.prix_acte = a.prix_acte;
            this.acte_couleur = a.acte_couleur;
            this.acte_durestd = a.acte_durestd;
            this.prix_acte = a.prix_acte;
            this.prix_traitement = a.prix_traitement;
            this.IdActe = a.IdActe;
            this.IdComm = a.IdComm;
            this.LibActe = a.LibActe;
            this.Parent = a.Parent;
            this.ShortLib = a.ShortLib;
            this.BaseRemboursement = a.BaseRemboursement;
            this.Remboursement = a.Remboursement;
            this.partPatient = a.partPatient;
            this.RembMutuelle = a.RembMutuelle;
            this.CodeTransposition = a.CodeTransposition;
            this.Depassement = a.Depassement;
            this.desactive = a.desactive;
            if (this.desactive)
                this.echeancestemp = 0;
            this.Tarif = a.Tarif;
        }
        private double _prix_traitement2;
        [PropertyCanBeSerialized]
        public double prix_traitement2
        {
            get
            {
                return _prix_traitement2;
            }
            set
            {
                _prix_traitement2 = value;
            }
        }
    }


    public class CommActesDevis : CommActesTraitement
    {
        public CommActesDevis()
        {
        }
        public CommActesDevis(CommActesTraitement a)
        {
            this.prix_acte = a.prix_acte;
            this.acte_couleur = a.acte_couleur;
            this.acte_durestd = a.acte_durestd;
            this.prix_traitement = a.prix_traitement;
            this.IdActe = a.IdActe;
            this.IdComm = a.IdComm;
            this.LibActe = a.LibActe;
            this.Parent = a.Parent;
            this.ShortLib = a.ShortLib;
            this.partPatient = a.partPatient;
            this.RembMutuelle = a.RembMutuelle;
            this.desactive = a.desactive;

        }

        private int _Id_devis;
        public int Id_devis
        {
            get
            {
                return _Id_devis;
            }
            set
            {
                _Id_devis = value;
            }
        }

        private double _prix_devis;
        [PropertyCanBeSerialized]
        public double prix_devis
        {
            get
            {
                return _prix_devis;
            }
            set
            {
                _prix_devis = value;
            }
        }

        private double _prix_devis_propose;
        [PropertyCanBeSerialized]
        public double prix_devis_propose
        {
            get
            {
                return _prix_devis_propose;
            }
            set
            {
                _prix_devis_propose = value;
            }
        }
    }





}
