using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BasCommon_BO
{
    [Serializable]
    public class Diagnostic
    {
        public Diagnostic()
        {
        }
       
        private int _id_diagnostic;
        [PropertyCanBeSerialized]
        public int id_diagnostic
        {
            get
            {
                return _id_diagnostic;
            }
            set
            {
                _id_diagnostic = value;
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
        
        private string _question;
        [PropertyCanBeSerialized]
        public string question
        {
            get
            {
                return _question;
            }
            set
            {
                _question = value;
            }
        }
        
        private string _categorie;
        [PropertyCanBeSerialized]
        public string categorie
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
       
        private string _photo;
        [PropertyCanBeSerialized]
        public string photo
        {
            get
            {
                return _photo;
            }
            set
            {
                _photo = value;
            }
        }

    }
}
