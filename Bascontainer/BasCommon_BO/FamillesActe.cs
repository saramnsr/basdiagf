using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BasCommon_BO
{
    [Serializable]
    public class FamillesActe
    {
        public override string ToString()
        {
            return libelle;
        }

        
        private string _libelle;
        [PropertyCanBeSerialized]
        public string libelle
        {
            get
            {
                return _libelle;
            }
            set
            {
                _libelle = value;
            }
        }

        private int _ParentFamillesActeId;
        [PropertyCanBeSerialized]
        public int ParentFamillesActeId
        {
            get
            {
                return _ParentFamillesActeId;
            }
            set
            {
                _ParentFamillesActeId = value;
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

        private Color _couleur;
        [PropertyCanBeSerialized]
        public Color couleur
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

        private List<FamillesActe> _ChildFamillesActe = new List<FamillesActe>();
        [PropertyCanBeSerialized]
        public List<FamillesActe> ChildFamillesActe
        {
            get
            {
                return _ChildFamillesActe;
            }
            set
            {
                _ChildFamillesActe = value;
            }
        }

        private FamillesActe _parent;
        [PropertyCanBeSerialized]
        public FamillesActe parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }

        private int _ordre;
        [PropertyCanBeSerialized]
        public int ordre
        {
            get
            {
                return _ordre;
            }
            set
            {
                _ordre = value;
            }
        }

    }
}
