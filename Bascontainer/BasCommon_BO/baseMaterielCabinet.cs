using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using BasCommon_BO.ElementsEnBouche.BO;

namespace BasCommon_BO
{
    public class baseMaterielCabinet
    {

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
        private DateTime _DateAchat;
        [PropertyCanBeSerialized]
        public DateTime DateAchat
        {
            get
            {
                return _DateAchat;
            }
            set
            {
                _DateAchat = value;
            }
        }



    }
}
