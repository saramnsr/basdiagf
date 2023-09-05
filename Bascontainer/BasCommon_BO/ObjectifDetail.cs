using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BasCommon_BO
{
    [Serializable]
    public class ObjectifDetail
    {
        public ObjectifDetail()
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
        private string _txtinvisalign;
        [PropertyCanBeSerialized]
        public string txtinvisalign
        {
            get
            {
                return _txtinvisalign;
            }
            set
            {
                _txtinvisalign = value;
            }
        }
        private string _instSpecial;
        [PropertyCanBeSerialized]
        public string instSpecial
        {
            get
            {
                return _instSpecial;
            }
            set
            {
                _instSpecial = value;
            }
        }
        private int _solution;
        [PropertyCanBeSerialized]
        public int solution
        {
            get
            {
                return _solution;
            }
            set
            {
                _solution = value;
            }
        }
    }
}
