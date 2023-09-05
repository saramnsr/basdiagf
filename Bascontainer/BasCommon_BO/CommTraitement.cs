using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class CommTraitement 
    {

      

            public class DateComparer : IComparer<CommTraitement>
            {

                public int Compare(CommTraitement x, CommTraitement y)
                {
                    return ((x.DatePrevisionnnelle != null) && (y.DatePrevisionnnelle != null)) ? x.DatePrevisionnnelle.Value.CompareTo(y.DatePrevisionnnelle.Value) : 0;
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

            private Devis_TK _devis;
            [PropertyCanBeSerialized]
            public Devis_TK devis
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
            private List<TempEcheanceDefinition > _echeancestemp = null;
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


            private List<Semestre> _semestres = new List<Semestre>();
            [PropertyCanBeSerialized]
            public List<Semestre> semestres
            {
                get
                {
                    return _semestres;
                }
                set
                {
                    _semestres = value;
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
            private Boolean _desactive = false;
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



            private double _MontantLigne;
            [PropertyCanBeSerialized]
            public double MontantLigne
            {
                get
                {
                    double vMontantLigne = 0;
                    if(Acte != null)
                    vMontantLigne = vMontantLigne + (Acte.prix_traitement * Convert.ToInt32 (Acte.quantite ));
                    foreach (CommActesTraitement act in _ActesSupp)
                    {
                        if (act.desactive) continue;
                        vMontantLigne = vMontantLigne + (act.prix_traitement * Convert.ToInt32(act.Qte));
                    }
                    foreach (CommActesTraitement act in _Radios)
                    {
                        if (act.desactive) continue;
                        vMontantLigne = vMontantLigne + (act.prix_traitement * Convert.ToInt32(act.Qte));
                    }
                    foreach (CommActesTraitement act in _photos)
                    {
                        if (act.desactive) continue;
                        vMontantLigne = vMontantLigne + (act.prix_traitement * Convert.ToInt32(act.Qte));
                    }
                    foreach (CommMaterielTraitement act in _Materiels)
                    {
                      
                        if (act.Famille != null)
                            if (act.Famille.libelle.ToLower().IndexOf("laboratoire") >= 0 || act.Famille.libelle.ToLower().IndexOf("stérilisation") >= 0 || act.Famille.libelle.ToLower().IndexOf("achats") >= 0) 
                            {
                                if (act.desactive) continue;
                                vMontantLigne = vMontantLigne + (act.prix_traitement * Convert.ToInt32(act.Qte));
                            }

                    }
                    return vMontantLigne;
                }
                set
                {
                    _MontantLigne = value;
                }
            }

            private double _MontantLigneAvantRemise;
            [PropertyCanBeSerialized]
            public double MontantLigneAvantRemise
            {
                get
                {
                    double vMontantLigneAvantRemise = 0;
                    if(Acte != null)
                    vMontantLigneAvantRemise = vMontantLigneAvantRemise + (Acte.prix_acte * Convert.ToInt32 (Acte.quantite ));
                    foreach (CommActesTraitement act in _ActesSupp)
                    {
                        if (act.desactive) continue;
                        vMontantLigneAvantRemise = vMontantLigneAvantRemise + (act.prix_acte * Convert.ToInt32(act.Qte));
                    }
                    foreach (CommActesTraitement act in _Radios)
                    {
                        if (act.desactive) continue;
                        vMontantLigneAvantRemise = vMontantLigneAvantRemise + (act.prix_acte * Convert.ToInt32(act.Qte));
                    }
                    foreach (CommActesTraitement act in _photos)
                    {
                        if (act.desactive) continue;
                        vMontantLigneAvantRemise = vMontantLigneAvantRemise + (act.prix_acte * Convert.ToInt32(act.Qte));
                    }
                    foreach (CommMaterielTraitement act in _Materiels)
                    {

                        if(act.Famille != null)
                            if (act.Famille.libelle.ToLower().IndexOf("laboratoire") >= 0 || act.Famille.libelle.ToLower().IndexOf("stérilisation") >= 0 || act.Famille.libelle.ToLower().IndexOf("achats") >= 0)
                            {
                                if (act.desactive) continue;
                                vMontantLigneAvantRemise = vMontantLigneAvantRemise + (act.prix_materiel * Convert.ToInt32(act.Qte));
                            }
                    }
                    return vMontantLigneAvantRemise;
                }
                set
                {
                    _MontantLigneAvantRemise = value;
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

            private double _MontantAvantRemise;
            [PropertyCanBeSerialized]
            public double MontantAvantRemise
            {
                get
                {
                    return _MontantAvantRemise;
                }
                set
                {
                    _MontantAvantRemise = value;
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

            private List<CommActesTraitement> _photos = null;
            [PropertyCanBeSerialized]
            public List<CommActesTraitement> photos
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
            private List<CommMaterielTraitement> _Materiels = null;
            [PropertyCanBeSerialized]
            public List<CommMaterielTraitement> Materiels
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

            private List<CommActesTraitement> _ActesSupp = null;
            [PropertyCanBeSerialized]
            public List<CommActesTraitement> ActesSupp
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
            private List<CommActesTraitement> _ActesSupp1 = null;
            [PropertyCanBeSerialized]
            public List<CommActesTraitement> ActesSupp1
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
            //private List<CommActesTraitement> _ActesSupp1 = null;
            //[PropertyCanBeSerialized]
            //public List<CommActesTraitement> ActesSupp1
            //{
            //    get
            //    {
            //        return _ActesSupp1;
            //    }
            //    set
            //    {
            //        _ActesSupp1 = value;
            //    }
            //}
            private List<CommActesTraitement> _Radios = null;
            [PropertyCanBeSerialized]
            public List<CommActesTraitement> Radios
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

            private CommTraitement  _ParentComment = null;
            [PropertyCanBeSerialized]
            public CommTraitement ParentComment
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

            private ActeTraitement _Acte;
            [PropertyCanBeSerialized]
            public ActeTraitement Acte
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

            private double _prix;
            [PropertyCanBeSerialized]
            public double prix
            {
                get
                {
                    if(_Acte != null)
                    _prix = _Acte.prix_traitement * Convert.ToInt32 (_Acte.quantite) ;
                    foreach (CommActesTraitement rr in _ActesSupp)
                    {
                        if(!rr.desactive)
                        _prix = _prix + (rr.prix_traitement * Convert.ToInt32(rr.Qte ));
                    }
                    foreach (CommActesTraitement rr in _Radios)
                    {
                        if (!rr.desactive)
                        _prix = _prix + (rr.prix_traitement * Convert.ToInt32(rr.Qte));
                    }

                    foreach (CommActesTraitement rr in _photos)
                    {
                        if (!rr.desactive)
                        _prix = _prix + (rr.prix_traitement * Convert.ToInt32(rr.Qte));
                    }
                    foreach (CommMaterielTraitement  rr in _Materiels)
                    {
                        if (!rr.desactive)
                        if(rr.Famille != null)
                            if (rr.Famille.libelle.ToLower().IndexOf("laboratoire") >= 0 || rr.Famille.libelle.ToLower().IndexOf("stérilisation") >= 0 || rr.Famille.libelle.ToLower().IndexOf("achats") >= 0) 
                            _prix = _prix + (rr.prix_traitement * Convert.ToInt32(rr.Qte));
                    }

                    return _prix;
                }
                set
                {
                    _prix = value;
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
    }
}
