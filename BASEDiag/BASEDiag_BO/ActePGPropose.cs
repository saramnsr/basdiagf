using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BASEDiag_BO
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

        private Devis _devis;
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

        private int _IdDevis;
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

        private double _Montant;
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


        private int _IdTemplateActePG = -1;
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
