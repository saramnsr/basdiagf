using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO.ElementsEnBouche.BO
{
    public interface IElementDent
    {


        List<int> LstDent { get; }
        [PropertyCanBeSerialized]
        string Dents { get; set; }
        string ShortLib { get;  }
        [PropertyCanBeSerialized]
        DateTime? Datesuppression { get; set; }
        [PropertyCanBeSerialized]
        DateTime? DateInstallation { get; set; }

        [PropertyCanBeSerialized]
        int IdCommFin { get; set; }
        [PropertyCanBeSerialized]
        int IdCommDebut { get; set; }
        [PropertyCanBeSerialized]
        int IdPatient { get; set; }
        [PropertyCanBeSerialized]
        int Id { get; set; }

        bool Haut { get;  }
        bool Bas { get;  }
        

        ElementDent.Materials typeMaterial { get; } 
    }
}
