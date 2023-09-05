using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasCommon_BO;

namespace BASEDiag_BO
{
    public class ScenarioCommClinique
    {

        public enum enumtypePropositon
        {

            Unknown = -1,
            Sucette = 0,
            Orthopedie = 1,
            Orthodontie = 2,
            Contention = 3,
            Adulte = 5,
        }


        public override string ToString()
        {
            return Libelle + " \r\n " + NbSemestres.ToString() + " semestre(s)";
        }

        private List<CommCliniqueDetailsScenario> _commentaires = null;
        public List<CommCliniqueDetailsScenario> commentaires
        {
            get
            {
                return _commentaires;
            }
            set
            {
                _commentaires = value;
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


        public enumtypePropositon typettmnt
        {
            get
            {
                if (TypeTtmnt == "ORTHOPEDIE") return enumtypePropositon.Orthopedie;
                if (TypeTtmnt == "ORTHODONTIE") return enumtypePropositon.Orthodontie;
                if (TypeTtmnt == "ADULTE") return enumtypePropositon.Adulte;
                if (TypeTtmnt == "CONTENTION") return enumtypePropositon.Contention;

                return enumtypePropositon.Unknown;
            }

        }


        private string _TypeTtmnt;
        public string TypeTtmnt
        {
            get
            {
                return _TypeTtmnt;
            }
            set
            {
                _TypeTtmnt = value;
            }
        }

        private int _NbSemestres;
        public int NbSemestres
        {
            get
            {
                return _NbSemestres;
            }
            set
            {
                _NbSemestres = value;
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
