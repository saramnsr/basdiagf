using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace BasCommon_BO
{
     [Serializable]
    public class FamillesMateriels
    {
         public override string ToString()
         {
             return libelle;
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

         private int _ParentFamillesMaterielId;
         [PropertyCanBeSerialized]
         public int ParentFamillesMaterielId
         {
             get
             {
                 return _ParentFamillesMaterielId;
             }
             set
             {
                 _ParentFamillesMaterielId = value;
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

         private Color _couleur;
         [PropertyCanBeSerialized]
         public Color couleur
         {
             get
             {
                 return _couleur;
             }
             set
             {
                 _couleur = value;
             }
         }

         private List<FamillesMateriels> _ChildFamillesMateriel = new List<FamillesMateriels>();
         [PropertyCanBeSerialized]
         public List<FamillesMateriels> ChildFamillesMateriel
         {
             get
             {
                 return _ChildFamillesMateriel;
             }
             set
             {
                 _ChildFamillesMateriel = value;
             }
         }

         private FamillesMateriels _parent;
         [PropertyCanBeSerialized]
         public FamillesMateriels parent
         {
             get
             {
                 return _parent;
             }
             set
             {
                 _parent = value;
             }
         }

         private int _ordre;
         [PropertyCanBeSerialized]
         public int ordre
         {
             get
             {
                 return _ordre;
             }
             set
             {
                 _ordre = value;
             }
         }



    }
}
