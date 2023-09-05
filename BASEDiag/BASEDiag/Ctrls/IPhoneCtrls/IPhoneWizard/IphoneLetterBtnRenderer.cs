using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;


namespace BASEPractice.Ctrls.IPhoneCtrls.IPhoneWizard
{



    public class IphoneKeyBoardBtnRenderer : AbstractIPhoneBtnRenderer
    {




       


        public override void DrawBtn(System.Drawing.Graphics p_graphics,
            System.Drawing.RectangleF DrawingArea,
           IphoneWizardQuestionBtn button)
        {

            int corner = 10;

            int ButtonWidth = (int)button.Bounds.Width;
            int ButtonHeight = (int)button.Bounds.Height;


            p_graphics.SmoothingMode = SmoothingMode.HighQuality;

            #region Création rectangle + ombre + couleurs

            RectangleF rect = new RectangleF(DrawingArea.X + (DrawingArea.Width - ButtonWidth) / 2, DrawingArea.Y + (DrawingArea.Height - ButtonHeight) / 2, ButtonWidth, ButtonHeight);
            GraphicsPath roundrect = CreateRoundedRectanglePath(rect, corner);

            RectangleF shadowRect;
            
            if (button.Selected)
                shadowRect = new RectangleF(rect.X-2, rect.Y - 2, ButtonWidth, ButtonHeight);
            else
                shadowRect = new RectangleF(rect.X, rect.Y + 3, ButtonWidth, ButtonHeight);

            GraphicsPath roundshad = CreateRoundedRectanglePath(shadowRect, corner);


            SolidBrush brushForRect;
            SolidBrush brushForShad;

            
                brushForRect = new SolidBrush(Color.FromArgb(255, button.couleur));
                brushForShad = new SolidBrush(Color.FromArgb(128, Color.Gray));
           


            if (!button.Selected)
            {
                p_graphics.FillPath(brushForShad, roundshad);
                p_graphics.FillPath(brushForRect, roundrect);
            }
            else
            {
                p_graphics.FillPath(brushForRect, roundrect);
                p_graphics.FillPath(brushForShad, roundshad);
                
            }
            

            #endregion

            #region Reflet du bas


            RectangleF rectBas = new RectangleF(rect.X, rect.Bottom - (rect.Height / 5f), rect.Width, (rect.Height / 5f));
            Region reg = new Region(roundrect);
            reg.Intersect(rectBas);

            LinearGradientBrush aRefletBrush = new LinearGradientBrush(new PointF(rectBas.X, rectBas.Top - 1), new PointF(rectBas.X, rectBas.Bottom), Color.Transparent, Color.FromArgb(125, Color.White));

            p_graphics.FillRegion(aRefletBrush, reg);

            #endregion

            #region Reflet du haut

                        //p_graphics.FillPath(brGlow, aRefletHaut);

            RectangleF recth = new RectangleF(rect.X, 0, rect.Width, (rect.Height / 5f));
            reg = new Region(roundrect);
            reg.Intersect(recth);

            aRefletBrush = new LinearGradientBrush(new PointF(recth.X, recth.Top - 1), new PointF(recth.X, recth.Bottom), Color.FromArgb(125, Color.White), Color.Transparent);

            p_graphics.FillRegion(aRefletBrush, reg);


            #endregion

           
            #region Texte/Image du bouton

            p_graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            SizeF sz = p_graphics.MeasureString(button.text, button.font, (int)rect.Width);
            string txt = button.text;
            int l = txt.Length;
            while ((l>0) && (sz.Height > rect.Height))
            {
                txt = button.text.Substring(0, l) + "...";
                sz = p_graphics.MeasureString(txt, button.font, (int)rect.Width);
                l--;
            }

            StringFormat stringFormat = new StringFormat();

            SolidBrush textBrush = new SolidBrush(button.ForeColor);

            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            p_graphics.DrawString(txt, button.font, textBrush, rect, stringFormat);
            //rect.Offset(1, 0);
            //p_graphics.DrawString(txt, button.font, new SolidBrush(button.couleur), rect, stringFormat);
            #endregion
        }
    }
}
