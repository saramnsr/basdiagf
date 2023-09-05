using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BASEDiag.Ctrls
{
    public abstract class AbstractPropositoonRenderer
    {
        
            public abstract Size MeasureProposition(Graphics p_graphics);
            public abstract void DrawProposition(Graphics p_graphics,  PropositionCell cell);
            public abstract Size MeasureTraitement(Graphics p_graphics);
            public abstract void DrawTraitement(Graphics p_graphics, TraitementCell cell);
        
    }
}
