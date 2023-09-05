using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO
{
   public partial class Cabinet
    {
       private int _id;
       public int Id
       {
           get
           {
               return _id;
           }
           set
           {
               _id = value;
           }
       }
       private string _nomCabinet;
       public string nomCabinet
       {
           get
           {
               return _nomCabinet;
           }
           set
           {
               _nomCabinet = value;
           }
       }
       private string _prefix;
       public string prefix
       {
           get
           {
               return _prefix;
           }
           set
           {
               _prefix = value;
           }
       }
       public override string ToString()
       {
           return nomCabinet ;
       }
    }
}
