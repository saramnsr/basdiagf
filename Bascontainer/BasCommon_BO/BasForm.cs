using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BasCommon_BO
{
    public interface IBasForm
    {

        void ChangePatient(int IdPatient);
        void ChangePatient();
        void ChangePatient(basePatient patient);

    }

    public interface IRHBasForm
    {
        void GoToDate(DateTime dte);
    }

    public interface IBaseLaboForm
    {
        void NouvelleDemandeInStandByfct(int IdPatient);
        void NouvelleDemande(int IdPatient);
    }

    
}
