using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BASEDiag_BO
{
    public class Surveillance
    {



        private Double? _Solde = null;
        public Double? Solde
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

        private int _IdSemestre;
        public int IdSemestre
        {
            get
            {
                if (Semestre != null) _IdSemestre = Semestre.Id;
                return _IdSemestre;
            }
            set
            {
                _IdSemestre = value;
            }
        }

        private Semestre _Semestre;
        public Semestre Semestre
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
