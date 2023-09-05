using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEDiag_BO
{
    public class TypeDevis
    {

        public enum CategorieDevis
        {
            Inconnue = -1,
            Sucette = 1,
            Orthopedique = 2,
            Orthodontique = 3,
            Invisalign = 4

        }

        private CategorieDevis _Categorie;
        public CategorieDevis Categorie
        {
            get
            {
                return _Categorie;
            }
            set
            {
                _Categorie = value;
            }
        }

        public override string ToString()
        {
            return libelle;
        }


        public bool DevisInvisalign
        {
            get
            {
                return Categorie == CategorieDevis.Invisalign;
            }
        }

        private string _libelle;
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
