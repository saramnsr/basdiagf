using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEDiag_BO
{
    public class Phase
    {

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

        public enum PhaseType
        {
            undefine = -1,
            Pediatrie = 0,
            Orthopedie = 1,
            Orthodontie = 2
        }
        
        private Double _TarifSemestre;
        public Double TarifSemestre
        {
            get
            {
                return _TarifSemestre;
            }
            set
            {
                _TarifSemestre = value;
            }
        }
        

        private int _Duree;
        
        /// <summary>
        /// Duree du traitement (en semestres)
        /// </summary>
        public int Duree
        {
            get
            {
                return _Duree;
            }
            set
            {
                _Duree = value;
            }
        }
                      
        private PhaseType _TypeDePhase;
        public PhaseType TypeDePhase
        {
            get
            {
                return _TypeDePhase;
            }
            set
            {
                _TypeDePhase = value;
            }
        }

        private TemplateActePG _traitement;
        public TemplateActePG traitement
        {
            get
            {
                return _traitement;
            }
            set
            {
                _traitement = value;
            }
        }

        private int _IdProposition;
        public int IdProposition
        {
            get
            {
                return _IdProposition;
            }
            set
            {
                _IdProposition = value;
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
       

    }
}
