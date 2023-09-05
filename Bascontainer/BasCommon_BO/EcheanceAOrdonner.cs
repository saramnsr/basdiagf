using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BASEPractice_BO
{
    public class EcheanceAOrdonner
    {

        public enum TypeEnvoisFS
        {
            AucunEnvoi = 0,
            Electronique = 1,
            Papier = 2
        }

        public TypeEnvoisFS typeenvoisFS { get; set; }

        private EntiteJuridique _Entite;
        public EntiteJuridique Entite
        {
            get
            {
                return _Entite;
            }
            set
            {
                _Entite = value;
            }
        }

        private BanqueDeRemise _comptecabinet;
        public BanqueDeRemise comptecabinet
        {
            get
            {
                return _comptecabinet;
            }
            set
            {
                _comptecabinet = value;
            }
        }

        private int _IdControl;
        public int IdControl
        {
            get
            {
                return _IdControl;
            }
            set
            {
                _IdControl = value;
            }
        }

        private string _Mutuelle;
        public string Mutuelle
        {
            get
            {
                return _Mutuelle;
            }
            set
            {
                _Mutuelle = value;
            }
        }

        private string _Caisse;
        public string Caisse
        {
            get
            {
                return _Caisse;
            }
            set
            {
                _Caisse = value;
            }
        }


        private Echeance _echeance;
        public Echeance echeance
        {
            get
            {
                return _echeance;
            }
            set
            {
                _echeance = value;
            }
        }
    }
}
