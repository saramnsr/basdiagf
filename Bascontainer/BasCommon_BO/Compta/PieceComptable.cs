using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO.Compta
{

    public class MdlPieceComptable
    {

        public override string ToString()
        {
            return LibelleMdl;
        }

        public string LibelleMdl { get; set; }

        int _Id = -1;
        public int Id { get { return _Id; } set { _Id = value; } }

        public Journal journal { get; set; }
        public Devise devise { get; set; }

        public string NumPiece { get; set; }

        public string Libelle { get; set; }

        public string Organisation { get; set; }

        List<MdlEcriture> _ecritures = null;
        public List<MdlEcriture> ecritures
        {
            get { return _ecritures; }
            set { _ecritures = value; }
        }

    }


    public class PieceComptable
    {
        int _Id = -1;
        public int Id { get { return _Id; } set { _Id = value; } }

        public Journal journal { get; set; }
        public DateTime DateOperation { get; set; }
        public DateTime? DateEcheance { get; set; }
        public Devise devise { get; set; }

        public int NumSaisie { get; set; }
        public string NumPiece { get; set; }

        public string Libelle { get; set; }
        

        private bool _Fog = true;
        public bool Fog
        {
            get
            {
                return _Fog;
            }
            set
            {
                _Fog = value;
            }
        }
        

        List<Ecriture> _ecritures = null;
        public List<Ecriture> ecritures
        {
            get { return _ecritures; }
            set { _ecritures = value; }
        }

    }
}
