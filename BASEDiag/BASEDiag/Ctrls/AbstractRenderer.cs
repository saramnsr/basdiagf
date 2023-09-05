using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ControlsLibrary.EasyTrackBar
{
    public abstract class AbstractRenderer
    {
        public abstract void DrawBar(Graphics p_graphics, Rectangle p_Bound);
        public abstract void DrawButton(Graphics p_graphics, Rectangle p_Bound, bool p_IsSelected);
    }
}
