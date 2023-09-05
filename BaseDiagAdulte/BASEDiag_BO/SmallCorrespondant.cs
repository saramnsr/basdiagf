using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEDiag_BO
{
   
    /// <summary>
    /// Description résumée de Correspondant
    /// </summary>
    public class SmallCorrespondant
    {

        

        private int _Id = -1;
        private string m_Nom;
        private string m_Prenom;


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
        
        public string Nom
        {
            get { return m_Nom; }
            set { m_Nom = value; }
        }

        public string Prenom
        {
            get { return m_Prenom; }
            set { m_Prenom = value; }
        }


        public override string ToString()
        {
            return Nom + " " + Prenom;
        }

    }
}
