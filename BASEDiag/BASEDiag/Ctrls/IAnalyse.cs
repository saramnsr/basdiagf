using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BASEDiag.Ctrls
{
    interface IAnalyse
    {

        string GetTextResultat();
        void Recalculate();
        void ReinitRotationAuto();
     //   void DrawExtraForms(Epsitec.Common.Drawing.Graphics gr);
        void DrawExtraForms(Epsitec.Common.Drawing.Graphics gr, bool ForImpression);
    }
}
