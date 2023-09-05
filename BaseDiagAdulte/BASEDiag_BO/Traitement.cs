using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BASEDiag_BO
{
    public class Traitement
    {

        private BasCommon_BO.Traitement.EnumPhase _Phase;
        public BasCommon_BO.Traitement.EnumPhase Phase
        {
            get
            {
                return _Phase;
            }
            set
            {
                _Phase = value;
            }
        }
        
        private Proposition _Parent;
        public Proposition Parent
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

        private string _CodeTraitement;
        public string CodeTraitement
        {
            get
            {
                return _CodeTraitement;
            }
            set
            {
                _CodeTraitement = value;
            }
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

        private List<Semestre> _semestres = new List<Semestre>();
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
    }
}
