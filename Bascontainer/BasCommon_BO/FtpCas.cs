using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BasCommon_BO
{
    public class FtpCas : IComparable<FtpCas>
    {

        public int IdPatient { get; set; }

        public DateTime DateInsert { get; set; }

        public DateTime? DateLastVisu { get; set; }
        

        public string Fichier { get; set; }

        public string Site { get; set; }
        public string NomPatient { get; set; }
        public string PrenomPatient { get; set; }

        public string InvisalignLogin { get; set; }
        public string InvisalignPassword { get; set; }

        public bool IsNew { get; set; }


        public int[] Duree { get; set; }

     
        public int CompareTo(FtpCas other)
        {
            return DateInsert.CompareTo(other.DateInsert);
        }
    }
}
