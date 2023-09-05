using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace BaseCommonControls.Ctrls.BO
{
    public static class GraphicUtils
    {
        public static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {

            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);

            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);

            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;

        }

        public static GraphicsPath CreateRoundedRectanglePath(RectangleF rect, int cornerRadius)
        {

            return CreateRoundedRectanglePath(Rectangle.Round(rect),cornerRadius);

        }


        public static GraphicsPath CreateNextPath(Rectangle rect)
        {

            GraphicsPath nTriaRect = new GraphicsPath();
            nTriaRect.AddLine(rect.X, rect.Y, rect.Right, ((rect.Bottom - rect.Y) / 2) + rect.Y);
            nTriaRect.AddLine(rect.Right, ((rect.Bottom - rect.Y) / 2) + rect.Y, rect.X, rect.Bottom);
            nTriaRect.AddLine(rect.X, rect.Bottom, rect.X, rect.Y);
            nTriaRect.CloseFigure();
            return nTriaRect;

        }

        public static GraphicsPath CreatePreviousPath(Rectangle rect)
        {

            GraphicsPath nTriaRect = new GraphicsPath();
            nTriaRect.AddLine(rect.Right, rect.Y, rect.X, ((rect.Bottom - rect.Y) / 2) + rect.Y);
            nTriaRect.AddLine(rect.X, ((rect.Bottom - rect.Y) / 2) + rect.Y, rect.Right, rect.Bottom);
            nTriaRect.AddLine(rect.Right, rect.Bottom, rect.Right, rect.Y);
            nTriaRect.CloseFigure();
            return nTriaRect;

        } 


    }
}
