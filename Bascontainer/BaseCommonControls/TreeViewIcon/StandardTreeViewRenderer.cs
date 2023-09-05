using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BaseCommonControls.Ctrls.BO;


namespace BaseCommonControls.Ctrls
{
    class StandardTreeViewRenderer : AbstractTreeViewRenderer
    {
        private Font ft = new Font("Segoe UI", 8, FontStyle.Regular);

        public override void DrawHeader(Graphics p_graphics, Rectangle p_Bound, TreeNode p_currentNode)
        {

            StringFormat sf = new StringFormat();

            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            p_graphics.FillRectangle(Brushes.WhiteSmoke, p_Bound);
            
            p_graphics.FillRectangle(Brushes.LightBlue, p_Bound);
            p_graphics.DrawRectangle(new Pen(Brushes.Black), p_Bound);

            
            string txt = p_currentNode.Text;
            //SizeF sz = p_graphics.MeasureString(txt, ft);
           // PointF txts = new PointF(((p_Bound.Width - sz.Width) / 2) + p_Bound.X, p_Bound.Y + 2);
            p_graphics.DrawString(txt, ft, Brushes.Black, p_Bound, sf);
        }


        public override void DrawButton(Graphics p_graphics, Rectangle p_Bound, trButton p_button, bool p_IsSelected)
        {

            StringFormat sf = new StringFormat();

            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            p_graphics.SmoothingMode = SmoothingMode.AntiAlias;
            // get the corner path
            GraphicsPath path;
            // get path
            path = GraphicUtils.CreateRoundedRectanglePath(p_Bound, 5);

            Color start;
            Color end;

            start = InterpolateColors(p_button.Color, Color.White, 0.4f);
            end = InterpolateColors(p_button.Color, Color.Gray, 0.7f);
            

            // little transparent
            start = Color.FromArgb(230, start);
            end = Color.FromArgb(180, end);

            if (p_Bound.Width > 0)
                using (LinearGradientBrush aGB = new LinearGradientBrush(p_Bound, start, end, LinearGradientMode.Vertical))
                    p_graphics.FillPath(aGB, path);
            if (p_IsSelected) p_graphics.DrawPath(new Pen(Brushes.PowderBlue),path);

            PointF txts = new PointF(p_graphics.MeasureString(p_button.Text, ft).Width, p_graphics.MeasureString(p_button.Text, ft).Height);
            p_graphics.DrawString(p_button.Text, ft, Brushes.Black, p_Bound,sf);

        }

        private Color InterpolateColors(Color color1, Color color2, float percentage)
        {
            int num1 = ((int)color1.R);
            int num2 = ((int)color1.G);
            int num3 = ((int)color1.B);
            int num4 = ((int)color2.R);
            int num5 = ((int)color2.G);
            int num6 = ((int)color2.B);
            byte num7 = Convert.ToByte(((float)(((float)num1) + (((float)(num4 - num1)) * percentage))));
            byte num8 = Convert.ToByte(((float)(((float)num2) + (((float)(num5 - num2)) * percentage))));
            byte num9 = Convert.ToByte(((float)(((float)num3) + (((float)(num6 - num3)) * percentage))));
            return Color.FromArgb(num7, num8, num9);
        }


    }
}
