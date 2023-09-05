using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BASEDiag_BO
{
    public class Semestre
    {



        private List<Surveillance> _surveillances = new List<Surveillance>();
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

        private Traitement _Parent;
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


        private double? _Solde = null;
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



        private DateTime? _DateFin;
        public DateTime? DateFin
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

        private DateTime? _DateDebut;
        public DateTime? DateDebut
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
