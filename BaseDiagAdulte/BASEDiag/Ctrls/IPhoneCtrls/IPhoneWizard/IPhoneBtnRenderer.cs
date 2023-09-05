using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace BASEPractice.Ctrls.IPhoneCtrls.IPhoneWizard
{
    public class IPhoneStandardBtnRenderer : AbstractIPhoneBtnRenderer
    {
        
        
        
            public override void DrawBtn(System.Drawing.Graphics p_graphics,
            System.Drawing.RectangleF DrawingArea,
           IphoneWizardQuestionBtn button)
        {
            int corner = 10;

            int ButtonWidth = (int)button.Bounds.Width;

            #region Création rectangle + ombre + couleurs

            // Rectangle rect = new Rectangle(0, 0, ButtonWidth, ButtonWidth);
            RectangleF rect = new RectangleF(DrawingArea.X + (DrawingArea.Width - ButtonWidth) / 2, DrawingArea.Y + (DrawingArea.Height - ButtonWidth) / 2, ButtonWidth, ButtonWidth);
            GraphicsPath roundrect = CreateRoundedRectanglePath(rect, corner);

            RectangleF shadowRect = new RectangleF(rect.X, rect.Y + 3, ButtonWidth, ButtonWidth);
            GraphicsPath roundshad = CreateRoundedRectanglePath(shadowRect, corner);

            SolidBrush brushForRect = new SolidBrush(Color.FromArgb(255, button.couleur));
            SolidBrush brushForShad = new SolidBrush(Color.FromArgb(128, Color.Gray));

            p_graphics.FillPath(brushForShad, roundshad);
            p_graphics.FillPath(brushForRect, roundrect);

            #endregion

            #region Reflet du bas

            RectangleF rectBas = new RectangleF(rect.X, rect.Bottom - (rect.Height / 5f), rect.Width, (rect.Height / 5f));
            Region reg = new Region(roundrect);
            reg.Intersect(rectBas);

            LinearGradientBrush aRefletBrush = new LinearGradientBrush(new PointF(rectBas.X, rectBas.Top - 1), new PointF(rectBas.X, rectBas.Bottom), Color.Transparent, Color.FromArgb(125, Color.White));

            p_graphics.FillRegion(aRefletBrush, reg);

            #endregion

            #region Reflet du haut

            GraphicsPath aRefletHaut = new GraphicsPath();
            aRefletHaut.AddLine(new PointF(rect.X + rect.Width, rect.Y), new PointF(rect.X + rect.Width, rect.Y + (rect.Height / 2) - 5));

            aRefletHaut.AddLine(new PointF(rect.X + rect.Width, rect.Y), new PointF(rect.X, rect.Y));
            aRefletHaut.AddLine(new PointF(rect.X, rect.Y + (rect.Height / 2) - 5), new PointF(rect.X, rect.Y));

            aRefletHaut.AddBezier(new PointF(rect.X, rect.Y + (rect.Height / 2) - 5),
                                                new PointF(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2)),
                                                new PointF(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2)),
                                                new PointF(rect.X + rect.Width, rect.Y + (rect.Height / 2) - 5));

            RectangleF rectGlow = new RectangleF(rect.Left, rect.Top - 1, rect.Width + 1, rect.Height / 2 + 2);

            LinearGradientBrush brGlow = new LinearGradientBrush(new PointF(rectGlow.Left, rectGlow.Bottom),
                                                                new PointF(rectGlow.Left, rectGlow.Top),
                                                                Color.FromArgb(30, Color.White), Color.FromArgb(250, Color.White));


            reg = new Region(roundrect);
            reg.Intersect(aRefletHaut);

            p_graphics.FillRegion(brGlow, reg);

            //p_graphics.FillPath(brGlow, aRefletHaut);

            RectangleF recth = new RectangleF(rect.X, 0, rect.Width, (rect.Height / 5f));
            reg = new Region(roundrect);
            reg.Intersect(recth);

            aRefletBrush = new LinearGradientBrush(new PointF(recth.X, recth.Top - 1), new PointF(recth.X, recth.Bottom), Color.FromArgb(125, Color.White), Color.Transparent);

            p_graphics.FillRegion(aRefletBrush, reg);

            //p_graphics.DrawPath(Pens.Red, aRefletHaut);

            #endregion

            #region Gradient (contour)

            RectangleF rectGradient = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
            LinearGradientBrush aGradientBrush = new LinearGradientBrush((RectangleF)rectGradient, Color.Transparent, Color.FromArgb(64, Color.White), LinearGradientMode.Vertical);

            ColorBlend blend = new ColorBlend();
            Color[] colors = new Color[] { Color.FromArgb(128, Color.White), Color.Transparent, Color.FromArgb(128, Color.White) };
            Single[] points = new Single[] { 0, 0.5f, 1 };

            blend.Colors = colors;
            blend.Positions = points;

            aGradientBrush.InterpolationColors = blend;

            Pen aGradientPen = new Pen(aGradientBrush, 2);

            rectGradient.Inflate(-3, -3);
            roundrect = CreateRoundedRectanglePath(rectGradient, 5);

            p_graphics.DrawPath(aGradientPen, roundrect);


            #endregion

            #region Texte/Image du bouton

            if (button.icon != null)
            {
                p_graphics.DrawImage(button.icon, (ButtonWidth / 2) - 20, (ButtonWidth / 2) - 20, 40, 40);
            }

            p_graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            SizeF sz = p_graphics.MeasureString(button.text, button.font, (int)rect.Width);
            string txt = button.text;
            int l = txt.Length;
            while (sz.Height > rect.Height)
            {
                txt = button.text.Substring(0, l) + "...";
                sz = p_graphics.MeasureString(txt, button.font, (int)rect.Width);
                l--;
            }

           

            StringFormat stringFormat = new StringFormat();

            if (button.TextAlign == TextLayout.Center)
            {
                SolidBrush textBrush = new SolidBrush(button.ForeColor);

                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                p_graphics.DrawString(txt, button.font, textBrush, rect, stringFormat);
            }

            if (button.TextAlign == TextLayout.Down)
            {
                //ForeColor = Color.Black;
                SolidBrush textBrush = new SolidBrush(button.ForeColor);

                RectangleF rectText = new RectangleF(0, rect.Width + 5, rect.Width + 20, 20);

                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Near;

                p_graphics.DrawString(button.text, button.font, textBrush, rectText, stringFormat);
            }

            #endregion

        }
    }
}
