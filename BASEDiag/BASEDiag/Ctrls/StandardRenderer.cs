using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ControlsLibrary.EasyTrackBar
{
    public class StandardRenderer : AbstractRenderer
    {



        public override void DrawBar(Graphics p_graphics, Rectangle p_Bound)
        {

            using (LinearGradientBrush aGB = new LinearGradientBrush(p_Bound, Color.FromArgb(146, 146, 146), Color.FromArgb(246, 246, 246), LinearGradientMode.Vertical))
                p_graphics.FillRectangle(aGB, p_Bound);

            p_graphics.DrawRectangle(new Pen(Color.FromArgb(126, 126, 126)), p_Bound);

        }

        public override void DrawButton(Graphics p_graphics, Rectangle p_Bound, bool p_IsSelected)
        {







            using (SolidBrush aGB = new SolidBrush(Color.FromArgb(50, 20, 20, 20)))
                p_graphics.FillRectangle(aGB, new Rectangle(p_Bound.X + 2, p_Bound.Y + 2, p_Bound.Width, p_Bound.Height));



            using (LinearGradientBrush aGB = new LinearGradientBrush(p_Bound, Color.FromArgb(200, 246, 246, 246), Color.FromArgb(200, 146, 146, 146), LinearGradientMode.Vertical))
                p_graphics.FillRectangle(aGB, p_Bound);

            /*
            if (p_IsSelected)
            {
                p_graphics.FillEllipse(, p_Bound);
            }
            else
            {
                p_graphics.FillEllipse(System.Drawing.SystemBrushes.GradientInactiveCaption, p_Bound);
            }
            */

            p_graphics.DrawRectangle(new Pen(Color.FromArgb(126, 126, 126)), p_Bound);
        }
    }
}
