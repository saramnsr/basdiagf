using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class Categorie
    {
        public override string ToString()
        {
            return Nom;
        }

        private int _IdCateg = -1;
        public int IdCateg
        {
            get
            {
                return _IdCateg;
            }
            set
            {
                _IdCateg = value;
            }
        }

        private string _Nom;
        public string Nom
        {
            get
            {
                return _Nom;
            }
            set
            {
                _Nom = value;
            }
        }
    }


    public class CustomCategorie : Categorie
    {
        public override string ToString()
        {
            return Nom;
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

        private int _IdPersonne = -1;
        public int IdPersonne
        {
            get
            {
                return _IdPersonne;
            }
            set
            {
                _IdPersonne = value;
            }
        }

        private DateTime _DateDebutCat;
        public DateTime DateDebutCat
        {
            get
            {
                return _DateDebutCat;
            }
            set
            {
                _DateDebutCat = value;
            }
        }

        private DateTime? _DateFinCat;
        public DateTime? DateFinCat
        {
            get
            {
                return _DateFinCat;
            }
            set
            {
                _DateFinCat = value;
            }
        }

        private int _Note = -1;
        public int Note
        {
            get
            {
                return _Note;
            }
            set
            {
                _Note = value;
            }
        }
    }
}
