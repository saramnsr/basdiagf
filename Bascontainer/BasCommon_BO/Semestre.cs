using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Semestre
    {

        public override string ToString()
        {
            if (traitementSecu != null)
                return DateDebut == null ? traitementSecu.Libelle : DateDebut.ToShortDateString() + ":" + traitementSecu.Libelle;
            else
                return DateDebut == null ? "" : DateDebut.ToShortDateString();
        }


        private List<Surveillance> _surveillances = new List<Surveillance>();
        [PropertyCanBeSerialized]
        public List<Surveillance> surveillances
        {
            get
            {
                return _surveillances;
            }
            set
            {
                _surveillances = value;
            }
        }

        private int? _IdTraitement = null;
        public int? IdTraitement
        {
            get
            {
                return _IdTraitement;
            }
            set
            {
                _IdTraitement = value;
            }
        }

        private Traitement _Parent;
        [PropertyCanBeSerialized]
        public Traitement Parent
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


        private int _IdDEPPreAssocier;
        [PropertyCanBeSerialized]
        public int IdDEPPreAssocier
        {
            get
            {
                return _IdDEPPreAssocier;
            }
            set
            {
                _IdDEPPreAssocier = value;
            }
        }

        private int _NumSemestre;
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

        private string _CodeSemestre;
        [PropertyCanBeSerialized]
        public string CodeSemestre
        {
            get
            {
                return _CodeSemestre;
            }
            set
            {
                _CodeSemestre = value;
            }
        }

        private TemplateActePG _traitementSecu;
        [PropertyCanBeSerialized]
        public TemplateActePG traitementSecu
        {
            get
            {
                return _traitementSecu;
            }
            set
            {
                _traitementSecu = value;
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


        private double _Montant_AvantRemise;
        [PropertyCanBeSerialized]
        public double Montant_AvantRemise
        {
            get
            {
                return _Montant_AvantRemise;
            }
            set
            {
                _Montant_AvantRemise = value;
            }
        }


        private double? _Solde = null;
        [PropertyCanBeSerialized]
        public double? Solde
        {
            get
            {
                return _Solde;
            }
            set
            {
                _Solde = value;
            }
        }



        private DateTime _DateFin;
        [PropertyCanBeSerialized]
        public DateTime DateFin
        {
            get
            {
                return _DateFin;
            }
            set
            {
                _DateFin = value;
            }
        }

        private DateTime _DateDebut;
        [PropertyCanBeSerialized]
        public DateTime DateDebut
        {
            get
            {
                return _DateDebut;
            }
            set
            {
                _DateDebut = value;
            }
        }
    }
}
