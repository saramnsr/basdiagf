using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BASEDiag.Ctrls.PropositionCtrlV2
{
    public abstract class AbstractPropCtrlV2Renderer
    {
        public abstract void DrawProposition(Graphics p_graphics, PropositionRow row);
        public abstract void DrawSemestreCell(Graphics p_graphics, SemestreCell cell,PropositionRow row);
        public abstract void DrawTraitement(Graphics p_graphics, TraitementCell cell, PropositionRow row);
        
            
    }
}
