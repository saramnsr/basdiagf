using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class StatusCliniqueManuel
    {


        public override string ToString()
        {
            return Libelle;
        }


        private string _Organisation;
        [PropertyCanBeSerialized]
        public string Organisation
        {
            get
            {
                return _Organisation;
            }
            set
            {
                _Organisation = value;
            }
        }

        private System.Drawing.Color _couleur;
        [PropertyCanBeSerialized]
        public System.Drawing.Color couleur
        {
            get
            {
                return _couleur;
            }
            set
            {
                _couleur = value;
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

        public enum FamilyStatut
        {
            HorsCabinet = 1,
            Autre = 0
        };

        private FamilyStatut _FamilleStatut;
        [PropertyCanBeSerialized]
        public FamilyStatut FamilleStatut
        {
            get
            {
                return _FamilleStatut;
            }
            set
            {
                _FamilleStatut = value;
            }
        }
    }
}
