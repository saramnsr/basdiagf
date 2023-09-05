using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
   public class Laboratoire
    {

       public override string ToString()
       {
           return nom;
       }
       private int _id;
       public int id
       {
           get { return _id; }
           set { _id = value; }
       }
       private string _nom;
       public string nom
       {
           get { return _nom; }
           set { _nom = value; }
       }
    }
}
