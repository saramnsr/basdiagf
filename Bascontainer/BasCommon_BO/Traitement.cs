using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Traitement
    {

        public override string ToString()
        {
            return Libelle;
        }

        public enum EnumPhase
        {
            Aucune = 0,
            Pédiatrique = 1,
            Orthopedique = 2,
            Orthodontique = 3,
            Surveillance = 4,
            Contention = 5,
            Adulte = 6,
            FinitionAdulte = 7,

        }

        private int? _IdProposition = null;
        public int? IdProposition
        {
            get
            {
                if (Parent != null) _IdProposition = Parent.Id;
                return _IdProposition;
            }
            set
            {
                _IdProposition = value;
            }
        }

        private string _CodeTraitement;
        [PropertyCanBeSerialized]
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

        private EnumPhase _Phase;
        [PropertyCanBeSerialized]
        public EnumPhase Phase
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
        [PropertyCanBeSerialized]
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
    }
}
