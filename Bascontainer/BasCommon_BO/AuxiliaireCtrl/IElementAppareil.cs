using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasCommon_BO.ElementsEnBouche.BO
{

    
    public interface IElementAppareil
    {
        [PropertyCanBeSerialized]
        Appareil Appareil { get; set; }
        string ShortLib { get; }
        [PropertyCanBeSerialized]
        DateTime? Datesuppression { get; set; }
        [PropertyCanBeSerialized]
        DateTime? DateInstallation { get; set; }

        [PropertyCanBeSerialized]
        bool Haut { get; set; }
        [PropertyCanBeSerialized]
        bool Bas { get; set; }

        [PropertyCanBeSerialized]
        int IdCommFin { get; set; }
        [PropertyCanBeSerialized]
        int IdCommDebut { get; set; }
        [PropertyCanBeSerialized]
        int IdPatient { get; set; }
        [PropertyCanBeSerialized]
        int Id { get; set; }
    }
}
