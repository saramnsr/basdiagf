using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace BasCommon_BO
{
    public class MaterielCabinet
    {

        public override string ToString()
        {
            return  Libelle + " " + Description ;
        }

        private string _Libelle;
        [Description("Libelle du matériel")]
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

        private string _Description;
        [Description("Description du matériel")]
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
