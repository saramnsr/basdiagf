using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
    public class ProfilApplique
    {
        public override string ToString()
        {
            return m_Profil;
        }

        private int m_Id;
        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        private string m_Profil;
        public string Profil
        {
            get { return m_Profil; }
            set { m_Profil = value; }
        }
    }
}
