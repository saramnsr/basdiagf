using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BasCommon_BO
{
    [Serializable]
    public class Acte : ICloneable
    {

        public Acte()
        {
            id_acte = -1;
        }

        public override string ToString()
        {
            return acte_libelle;
        }
      


        private string _MailConfirmationAttachments;
        [PropertyCanBeSerialized]
        public string MailConfirmationAttachments
        {
            get
            {
                return _MailConfirmationAttachments;
            }
            set
            {
                _MailConfirmationAttachments = value;
            }
        }


        private string _MailConfirmationRDVBody;
        [PropertyCanBeSerialized]
        public string MailConfirmationRDVBody
        {
            get
            {
                return _MailConfirmationRDVBody;
            }
            set
            {
                _MailConfirmationRDVBody = value;
            }
        }



        private string _MailConfirmationSubject;
        [PropertyCanBeSerialized]
        public string MailConfirmationSubject
        {
            get
            {
                return _MailConfirmationSubject;
            }
            set
            {
                _MailConfirmationSubject = value;
            }
        }




        private int _id_acte;
        [PropertyCanBeSerialized]
        public int id_acte
        {
            get
            {
                return _id_acte;
            }
            set
            {
                _id_acte = value;
            }
        }


        private string _acte_libelle;
        [PropertyCanBeSerialized]
        public string acte_libelle
        {
            get
            {
                return _acte_libelle;
            }
            set
            {
                _acte_libelle = value;
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

        private int _type_acte;
        [PropertyCanBeSerialized]
        public int type_acte
        {
            get
            {
                return _type_acte;
            }
            set
            {
                _type_acte = value;
            }
        }

        private double _nb_fautbloc;
        [PropertyCanBeSerialized]
        public double nb_fautbloc
        {
            get
            {
                return _nb_fautbloc;
            }
            set
            {
                _nb_fautbloc = value;
            }
        }

        private string _code_planing;
        [PropertyCanBeSerialized]
        public string code_planing
        {
            get
            {
                return _code_planing;
            }
            set
            {
                _code_planing = value;
            }
        }



        private FamillesActe _famille_Acte;
        [PropertyCanBeSerialized]
        public FamillesActe famille_Acte
        {
            get
            {
                return _famille_Acte;
            }
            set
            {
                _famille_Acte = value;
            }
        }
        private int _temps_chrono;
        [PropertyCanBeSerialized]
        public int temps_chrono
        {
            get
            {
                return _temps_chrono;
            }
            set
            {
                _temps_chrono = value;
            }
        }



        private int _id_famille;
        [PropertyCanBeSerialized]
        public int id_famille
        {
            get
            {
                return _id_famille;
            }
            set
            {
                _id_famille = value;
            }
        }

        private int _id_fauteuil;
        [PropertyCanBeSerialized]
        public int id_fauteuil
        {
            get
            {
                return _id_fauteuil;
            }
            set
            {
                _id_fauteuil = value;
            }
        }

        private int _tps_ass;
        [PropertyCanBeSerialized]
        public int tps_ass
        {
            get
            {
                return _tps_ass;
            }
            set
            {
                _tps_ass = value;
            }
        }

        private int _tps_prat;
        [PropertyCanBeSerialized]
        public int tps_prat
        {
            get
            {
                return _tps_prat;
            }
            set
            {
                _tps_prat = value;
            }
        }

        private string _nom_raccourci;
        [PropertyCanBeSerialized]
        public string nom_raccourci
        {
            get
            {
                return _nom_raccourci;
            }
            set
            {
                _nom_raccourci = value;
            }
        }

        private string _heure_debut;
        [PropertyCanBeSerialized]
        public string heure_debut
        {
            get
            {
                return _heure_debut;
            }
            set
            {
                _heure_debut = value;
            }
        }

        private string _heure_fin;
        [PropertyCanBeSerialized]
        public string heure_fin
        {
            get
            {
                return _heure_fin;
            }
            set
            {
                _heure_fin = value;
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

          private string _acte_libelle_estimation;
        [PropertyCanBeSerialized]
        public string acte_libelle_estimation
        {
            get
            {
                return _acte_libelle_estimation;
            }
            set
            {
                _acte_libelle_estimation = value;
            }
        }

            private string _acte_libelle_facture;
        [PropertyCanBeSerialized]
        public string acte_libelle_facture
        {
            get
            {
                return _acte_libelle_facture;
            }
            set
            {
                _acte_libelle_facture = value;
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
        private int _ordre;
        [PropertyCanBeSerialized]
        public int ordre
        {
            get
            {
                return _ordre;
            }
            set
            {
                _ordre = value;
            }
        }
        private string _quantite;
        [PropertyCanBeSerialized]
        public string quantite
        {
            get
            {
                return _quantite;
            }
            set
            {
                _quantite = value;
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
        private string _jour;
        [PropertyCanBeSerialized]
        public string jour
        {
            get
            {
                return _jour;
            }
            set
            {
                _jour = value;
            }
        }
        private string _praticien;
        [PropertyCanBeSerialized]
        public string praticien
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


        private string  _emplacement;
        [PropertyCanBeSerialized]
        public string  emplacement
        {
            get
            {
                return _emplacement;
            }
            set
            {
                _emplacement = value;
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
        #region ICloneable Members

        public object Clone()
        {
                Acte acte = new Acte();
                acte.id_acte = id_acte;
                acte.id_famille = id_famille;
                acte.jour = jour;
                acte.MailConfirmationAttachments = MailConfirmationAttachments;
                acte.MailConfirmationRDVBody = MailConfirmationRDVBody;
                acte.MailConfirmationSubject = MailConfirmationSubject;
                acte.nb_fautbloc = nb_fautbloc;
                acte.nom_raccourci = nom_raccourci;
                acte.nombre_points = nombre_points;
                acte.nomenclature = nomenclature;
                acte.ordre = ordre;
                acte.praticien = praticien;
                acte.prix_acte = prix_acte;
                acte.quantite = quantite;
                acte.Remboursement = Remboursement;
                acte.Tarif = Tarif;
                acte.temps_chrono = temps_chrono;
                acte.tps_ass = tps_ass;
                acte.tps_prat = tps_prat;
                acte.type_acte = type_acte;
                acte.acte_couleur = acte_couleur;
                acte.acte_durestd = acte_durestd;
                acte.acte_libelle = acte_libelle;
                acte.acte_libelle_estimation = acte_libelle_estimation;
                acte.acte_libelle_facture = acte_libelle_facture;
                acte.BaseRemboursement = BaseRemboursement;
                acte.code_planing = code_planing;
                acte.CodeTransposition = CodeTransposition;
                acte.coefficient = coefficient;
                acte.cotation = cotation;
                acte.Depassement = Depassement;
                acte.emplacement = emplacement;
                acte.famille_Acte = famille_Acte;
                acte.heure_debut = heure_debut;
                acte.heure_fin = heure_fin;

                return acte;
        }

        #endregion
    }
     
     
    public class ActeTraitement : Acte
    {
        public ActeTraitement()
        {
        }
        public  ActeTraitement(Acte a)
    {

        if (a == null) return ;
        this.prix_acte = a.prix_acte;
        this.quantite = a.quantite;
        this.acte_couleur = a.acte_couleur;
        this.acte_durestd = a.acte_durestd;
        this.acte_libelle = a.acte_libelle;
        this.code_planing = a.code_planing;
        this.famille_Acte = a.famille_Acte;
        this.id_acte = a.id_acte;
        this.id_famille = a.id_famille;
        this.id_fauteuil = a.id_fauteuil;
        this.MailConfirmationAttachments = a.MailConfirmationAttachments;
        this.MailConfirmationRDVBody = a.MailConfirmationRDVBody;
        this.MailConfirmationSubject = a.MailConfirmationSubject;
        this.nb_fautbloc = a.nb_fautbloc;
        this.nom_raccourci = a.nom_raccourci;
        
        this.prix_traitement = a.prix_acte;
        this.temps_chrono = a.temps_chrono;
        this.tps_ass = a.tps_ass;
        this.tps_prat = a.tps_prat;
        this.type_acte = a.type_acte;
        this.nomenclature = a.nomenclature;
        this.nombre_points = a.nombre_points;
        this.cotation = a.cotation;
        this.acte_libelle_estimation = a.acte_libelle_estimation;
        this.acte_libelle_facture = a.acte_libelle_facture;
        this.CodeTransposition = a.CodeTransposition;
        this.BaseRemboursement = a.BaseRemboursement;
        this.Remboursement = a.Remboursement;
        this.Depassement = a.Depassement;
        this.Tarif = a.Tarif;
       
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

    public class ActeDevis : ActeTraitement
    {
        public ActeDevis()
        {
        }
        public ActeDevis(ActeTraitement a)
        {
            this.prix_acte = a.prix_acte;
            this.acte_couleur = a.acte_couleur;
            this.acte_durestd = a.acte_durestd;
            this.acte_libelle = a.acte_libelle;
            this.code_planing = a.code_planing;
            this.famille_Acte = a.famille_Acte;
            this.id_acte = a.id_acte;
            this.id_famille = a.id_famille;
            this.id_fauteuil = a.id_fauteuil;
            this.MailConfirmationAttachments = a.MailConfirmationAttachments;
            this.MailConfirmationRDVBody = a.MailConfirmationRDVBody;
            this.MailConfirmationSubject = a.MailConfirmationSubject;
            this.nb_fautbloc = a.nb_fautbloc;
            this.nom_raccourci = a.nom_raccourci;
            this.prix_acte = a.prix_acte;
            this.prix_traitement = a.prix_acte;
            this.prix_devis_propose = a.prix_traitement;
            this.prix_devis = prix_devis_propose;
            this.temps_chrono = a.temps_chrono;
            this.tps_ass = a.tps_ass;
            this.tps_prat = a.tps_prat;
            this.type_acte = a.type_acte;
            this.CodeTransposition = a.CodeTransposition;
            this.BaseRemboursement = a.BaseRemboursement;
            this.Remboursement = a.Remboursement;
            this.Depassement = a.Depassement;
            this.Tarif = a.Tarif;

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
    
    public class ActeGroupement : Acte
    {
        public ActeGroupement()
        {
        }
        public ActeGroupement(Acte a)
        {
            if (a == null) return;
            this.prix_acte = a.prix_acte;
            this.quantite = a.quantite;
            this.acte_couleur = a.acte_couleur;
            this.acte_durestd = a.acte_durestd;
            this.acte_libelle = a.acte_libelle;
            this.code_planing = a.code_planing;
            this.famille_Acte = a.famille_Acte;
            this.id_acte = a.id_acte;
            this.id_famille = a.id_famille;
            this.id_fauteuil = a.id_fauteuil;
            this.MailConfirmationAttachments = a.MailConfirmationAttachments;
            this.MailConfirmationRDVBody = a.MailConfirmationRDVBody;
            this.MailConfirmationSubject = a.MailConfirmationSubject;
            this.nb_fautbloc = a.nb_fautbloc;
            this.nom_raccourci = a.nom_raccourci;
            this.qte = Convert.ToInt32(a.quantite);
            this.prixTraitement = a.prix_acte;
            this.temps_chrono = a.temps_chrono;
            this.tps_ass = a.tps_ass;
            this.tps_prat = a.tps_prat;
            this.type_acte = a.type_acte;
            this.nomenclature = a.nomenclature;
            this.nombre_points = a.nombre_points;
            this.cotation = a.cotation;
            this.acte_libelle_estimation = a.acte_libelle_estimation;
            this.acte_libelle_facture = a.acte_libelle_facture;
            this.CodeTransposition = a.CodeTransposition;
            this.BaseRemboursement = a.BaseRemboursement;
            this.Remboursement = a.Remboursement;
            this.Depassement = a.Depassement;
            this.Tarif = a.Tarif;

        }


        private List<ActeGroupement> _actes;
       
        [PropertyCanBeSerialized]
        public List<ActeGroupement> actes
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
        
        private double _prixTraitement;        
        [PropertyCanBeSerialized]
        public double prixTraitement
        {
            get
            {
                return _prixTraitement;
            }
            set
            {
                _prixTraitement = value;
            }
        }

        private int _qte;
        public int qte
        {
            get
            {
                return _qte;
            }
            set
            {
                _qte = value;
            }
        }

        private int _id ;
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

        private int _idParent = -1;
        public int idParent
        {
            get
            {
                return _idParent;
            }
            set
            {
                _idParent = value;
            }
        }
    }
}
