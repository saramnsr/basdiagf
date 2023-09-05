using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BASEDiag_BO
{
    [Serializable]
    public class FamillesActe
    {
        public override string ToString()
        {
            return libelle;
        }

        public int Id;
        public string libelle;
        public int ParentFamillesActeId;
        public Color couleur = Color.WhiteSmoke;
        public List<FamillesActe> ChildFamillesActe = new List<FamillesActe>();
        public FamillesActe parent = null;

        public int ordre;

    }
}
