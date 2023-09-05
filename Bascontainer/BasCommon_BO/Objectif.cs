using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BasCommon_BO
{
    [Serializable]
    public class Objectif
    {
        public Objectif()
        {
        }
        
        private int _id_objectif;
        [PropertyCanBeSerialized]
        public int id_objectif
        {
            get
            {
                return _id_objectif;
            }
            set
            {
                _id_objectif = value;
            }
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
        
        private string _description;
        [PropertyCanBeSerialized]
        public string description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
        
        private int _categorie;
        [PropertyCanBeSerialized]
        public int categorie
        {
            get
            {
                return _categorie;
            }
            set
            {
                _categorie = value;
            }
        }
    }
}
