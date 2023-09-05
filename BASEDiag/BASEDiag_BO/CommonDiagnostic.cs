using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEDiag_BO
{
    public class CommonDiagnostic : IComparable
    {

        private string _Photos;
        const char delimiter = ';';
        public List<string> Photos
        {
            get
            {
                List<string> lst = new List<string>();
                foreach (string s in _Photos.Split(delimiter))
                    lst.Add(s.Trim());
                return lst;
            }
            set
            {
                string ss = "";
                foreach (string s in value)
                {
                    if (!string.IsNullOrEmpty(ss))
                        ss += delimiter;

                    ss += s;
                }
                _Photos = ss;
            }
        }


        public void SetPhotos(string s)
        {
            _Photos = s.ToLower();
        }


        private int _DisplayOrder;
        public int DisplayOrder
        {
            get
            {
                return _DisplayOrder;
            }
            set
            {
                _DisplayOrder = value;
            }
        }
        

        private string _question;
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

        private bool _SelectedObjectif = false;
        public bool SelectedObjectif
        {
            get
            {
                return _SelectedObjectif;
            }
            set
            {
                _SelectedObjectif = value;
            }
        }
        
        public override string ToString()
        {
            return Libelle;
        }

        private string _Libelle;
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
        public int CompareTo(object obj)
        {
            if (obj is CommonDiagnostic)
                return DisplayOrder.CompareTo(((CommonDiagnostic)obj).DisplayOrder);
            else
                return 0;
        }
    }
}
