using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO.Compta
{
    public class Journal
    {

        public override string ToString()
        {
            return LibelleJournal;
        }
        public string NumJournal { get; set; }
        public string LibelleJournal { get; set; }
    }
}
