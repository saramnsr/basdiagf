using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Text;

namespace BaseCommonControls
{
    class StandardRenderer : AbstractRenderer
    {


        private Font ft = new Font("Segoe UI", 8, FontStyle.Regular);

        public override void DrawHeader(Graphics p_graphics, RectangleF p_Bound, TreeNode p_currentNode, bool p_IsHovered, bool p_IsDown)
        {
            Color headerColr = Color.Gray;

            p_graphics.SmoothingMode = SmoothingMode.AntiAlias;
            // get the corner path
            GraphicsPath path;
            // get path
            path = CreateRoundedRectanglePath(p_Bound, 5);

            Color start;
            Color end;

            start = InterpolateColors(headerColr, Color.White, 0.4f);
            end = InterpolateColors(headerColr, Color.Gray, 0.7f);


            Color start2;
            Color end2;

            start2 = InterpolateColors(headerColr, Color.White, 0.5f);
            end2 = InterpolateColors(headerColr, Color.Gray, 0.6f);


            // little transparent
            start = Color.FromArgb(230, start);
            end = Color.FromArgb(180, end);
            start2 = Color.FromArgb(220, start2);
            end2 = Color.FromArgb(190, end2);

            if (p_Bound.Width > 0)
            {
                Brush aGB;


                if (p_IsDown)
                    aGB = new SolidBrush(end);
                else
                    if (p_IsHovered)
                        aGB = new LinearGradientBrush(p_Bound, start2, end2, LinearGradientMode.Vertical);
                    else
                        aGB = new LinearGradientBrush(p_Bound, start, end, LinearGradientMode.Vertical);


                p_graphics.FillPath(aGB, path);
            }


            StringFormat sf = new StringFormat(StringFormatFlags.DirectionVertical);
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            string txt = p_currentNode == null ? "" : p_currentNode.Text;

            p_graphics.DrawString(txt, ft, Brushes.Black, p_Bound, sf);




        }


        public static GraphicsPath CreateRoundedRectanglePath(RectangleF rect, int cornerRadius)
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



        public override SizeF MeasureButton(Graphics p_graphics, BASButton p_Button)
        {

            StringFormat sf = new StringFormat(StringFormatFlags.NoWrap);
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;


            return p_graphics.MeasureString(p_Button.Text, ft, int.MaxValue, sf);
        }

        public override void DrawButton(Graphics p_graphics, RectangleF p_Bound, TreeNode tn, BASButton p_button, bool p_IsSelected, bool p_IsHovered, bool p_IsDown)
        {

           

            p_graphics.SmoothingMode = SmoothingMode.AntiAlias;
            // get the corner path
            GraphicsPath path;
            // get path
            path = CreateRoundedRectanglePath(p_Bound, 5);

            Color start;
            Color end;

            start = InterpolateColors(p_button.Color, Color.White, 0.4f);
            end = InterpolateColors(p_button.Color, Color.Gray, 0.7f);


            Color start2;
            Color end2;

            start2 = InterpolateColors(p_button.Color, Color.White, 0.5f);
            end2 = InterpolateColors(p_button.Color, Color.Gray, 0.6f);


            // little transparent
            start = Color.FromArgb(230, start);
            end = Color.FromArgb(180, end);
            start2 = Color.FromArgb(220, start2);
            end2 = Color.FromArgb(190, end2);

            if (p_Bound.Width > 0)
            {
                Brush aGB;


                if (p_IsDown)
                    aGB = new SolidBrush(end);
                else
                    if (p_IsHovered)
                        aGB = new LinearGradientBrush(p_Bound, start2, end2, LinearGradientMode.Vertical);
                    else
                        aGB = new LinearGradientBrush(p_Bound, start, end, LinearGradientMode.Vertical);


                p_graphics.FillPath(aGB, path);
            }
            if (p_IsSelected) p_graphics.DrawPath(new Pen(Brushes.PowderBlue,4), path);

            PointF txts = new PointF(p_graphics.MeasureString(p_button.Text, ft).Width, p_graphics.MeasureString(p_button.Text, ft).Height);


            StringFormat sf = new StringFormat(StringFormatFlags.NoWrap);
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            if ((imgList != null) && (tn.ImageKey != null))
            {
                sf.LineAlignment = StringAlignment.Far;
                Image im = imgList.Images[tn.ImageIndex];
                if (im != null)
                {
                    PointF pt = new PointF(p_Bound.Location.X + (p_Bound.Width - im.Width) / 2, p_Bound.Location.Y + (p_Bound.Height  - im.Height) / 2);
                    p_graphics.DrawImage(im, pt);
                }
            }



            


            p_graphics.DrawString(p_button.Text, ft, Brushes.Black, p_Bound, sf);

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
