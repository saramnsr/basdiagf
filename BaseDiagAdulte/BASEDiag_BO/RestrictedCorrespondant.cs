using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEDiag_BO
{
    /// <summary>
    /// Description résumée de Correspondant
    /// </summary>
    [Serializable]
    public class RestrictedCorrespondant
    {



        private int m_Id = -1;
        private string m_Nom;
        private string m_Prenom;
        private string m_VilleOffice;
        



        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }


        public string VilleOffice
        {
            get
            {
                return m_VilleOffice;
            }
            set
            {
                m_VilleOffice = value;
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
            return Nom + " " + Prenom + " (" + VilleOffice + ")";
        }

    }
}
