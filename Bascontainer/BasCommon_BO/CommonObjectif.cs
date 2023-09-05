using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class CommonObjectif
    {

        public enum CategorieObjectifs
        {
            Aucune = 0,
            Enfant = 1,
            Adulte11 = 11,
            Adulte12 = 12,
            Adulte21 = 21,
            Adulte22 = 22,
            Adulte31 = 31,
            Adulte32 = 32,
            Adulte33 = 33

            
        }


        private CategorieObjectifs _categorie = CategorieObjectifs.Aucune;
        public CategorieObjectifs categorie
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

        public override string ToString()
        {
            return Libelle;
        }

        private string _Description;
        [PropertyCanBeSerialized]
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
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
