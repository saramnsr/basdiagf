using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class ActePGPropose
    {
        public override string ToString()
        {
            if (Qte <= 1)
                return Libelle;
            else
                return Qte.ToString() + " x " + Libelle;
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

        private bool _Optionnel = true;
        [PropertyCanBeSerialized]
        public bool Optionnel
        {
            get
            {
                return _Optionnel;
            }
            set
            {
                _Optionnel = value;
            }
        }

        

         private Proposition _proposition;
        [PropertyCanBeSerialized]
        public Proposition proposition
        {
            get
            {
                return _proposition;
            }
            set
            {
                _proposition = value;
            }
        }

        private Devis _devis;
        [PropertyCanBeSerialized]
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



        private int _IdProposition = -1;
        public int IdProposition
        {
            get
            {
                if (proposition != null) _IdProposition = proposition.Id;
                return _IdProposition;
            }
            set
            {
                _IdProposition = value;
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

        private DateTime? _DateExecution;
        [PropertyCanBeSerialized]
        public DateTime? DateExecution
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
        private int _QteModifiee;
        [PropertyCanBeSerialized]
        public int QteModifiee
        {
            get
            {
                return _QteModifiee;
            }
            set
            {
                _QteModifiee = value;
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


        private int _IdTemplateActePG = -1;
        [PropertyCanBeSerialized]
        public int IdTemplateActePG
        {
            get
            {
                if (template != null) _IdTemplateActePG = template.Id;
                return _IdTemplateActePG;
            }
            set
            {
                _IdTemplateActePG = value;
            }
        }

        private TemplateActePG _template;
        [PropertyCanBeSerialized]
        public TemplateActePG template
        {
            get
            {
                return _template;
            }
            set
            {
                _template = value;
            }
        }

        private string _Type_Acte;
        [PropertyCanBeSerialized]
        public string Type_Acte
        {
            get
            {
                return _Type_Acte;
            }
            set
            {
                _Type_Acte = value;
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

        /*
        public object Clone()
        {
            ActePGPropose a = new ActePGPropose();
            a.devis = devis;
            a.IdDevis = IdDevis;
            a.Montant = Montant;
            a.DateExecution = DateExecution;
            a.Qte = Qte;
            a.template = template;
            a.IdTemplateActePG = IdTemplateActePG;

            return a;
        }
        */
        #endregion
    }
}
