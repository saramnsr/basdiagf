using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace BasCommon_BO
{

    public enum TypeFamilleTraitement
    {

        Senario = 0,
        RDV = 1,
        GroupementActe2 = 2,
    }

    [Serializable]
    public class FamillesTraitements
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


        public TypeFamilleTraitement typeFamilleTraitement;

        // regler au niveau du build urgent


        private int _ParentFamillesTraitementId;
        [PropertyCanBeSerialized]
        public int ParentFamillesTraitementId
        {
            get
            {
                return _ParentFamillesTraitementId;
            }
            set
            {
                _ParentFamillesTraitementId = value;
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


        private string _TitreDevis;
        [PropertyCanBeSerialized]
        public string TitreDevis
        {
            get
            {
                return _TitreDevis;
            }
            set
            {
                _TitreDevis = value;
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

        private List<FamillesTraitements> _ChildFamillesTraitement = new List<FamillesTraitements>();
        [PropertyCanBeSerialized]
        public List<FamillesTraitements> ChildFamillesTraitement
        {
            get
            {
                return _ChildFamillesTraitement;
            }
            set
            {
                _ChildFamillesTraitement = value;
            }
        }


        private FamillesTraitements _parent;
        [PropertyCanBeSerialized]
        public FamillesTraitements parent
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
        // à retirer
        private int _isDevis;
        [PropertyCanBeSerialized]
        public int isNotDevis
        {
            get
            {
                return _isDevis;
            }
            set
            {
                _isDevis = value;
            }
        }



        // ca sera changer tout de suite
        //private Boolean _isDevis;
        //[PropertyCanBeSerialized]
        //public Boolean isNotDevis
        //{
        //    get
        //    {
        //        return _isDevis;
        //    }
        //    set
        //    {
        //        _isDevis = value;
        //    }
        //}

    }
}
