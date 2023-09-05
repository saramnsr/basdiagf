﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BASEDiag_BO
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

        public enum EtatCommentaire
        {
            Afaire,
            Prevus,
            EnCours,
            Termine,
            Undefined

        }



        private bool _IsDateDeRef = false;
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

        private EtatCommentaire _etat = EtatCommentaire.Undefined;
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

        private List<CommDentAExtraire> _DentsAExtraire = null;
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

        private List<CommPhoto> _photos = null;
        public List<CommPhoto> photos
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

        private List<CommRadio> _Radios = null;
        public List<CommRadio> Radios
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

        private DateTime? _date;
        public DateTime? date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
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
                if ((Appointement != null) && (Appointement.IdPatient == -1))
                {
                    Appointement.IdPatient = value.Id;
                    Appointement.PatientName = value.ToShortString();
                }
            }
        }

        private RHAppointment _Appointement;
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

        private string _CommentaireAFaire = "";
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



    public class CommDentAExtraire
    {

        private CommClinique _Parent;
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

        private string _Prenom;
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


        private TypeRadio _typeradio;
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


        private TypePhoto _typephoto;
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

    public class CommMateriel
    {

        private string _ShortLib = "";
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

        private int _Qte = 1;
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

        private string _Libelle = "";
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

        private ElementDent.Materials _type;
        public ElementDent.Materials type
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
    
}
