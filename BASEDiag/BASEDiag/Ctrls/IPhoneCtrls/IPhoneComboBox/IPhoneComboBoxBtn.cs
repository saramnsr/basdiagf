using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BASE_CONTACT.Ctrls.IPhoneControls.IPhoneComboBox
{
    public class IPhoneComboBoxBtn
    {
        #region Properties

        private int _Width = 80;
        public int ButtonWidth
        {
            get { return _Width; }
            set { _Width = value; }
        }

        private int _Height = 80;
        public int ButtonHeight
        {
            get { return _Height; }
            set { _Height = value; }
        }

        private string _Text = "Button";
        public string Libelle
        {
            get { return _Text; }
            set { _Text = value; }
        }

        public enum TextLayout
        {
            Center,
            Down
        }

        private TextLayout _TextAlign = TextLayout.Center;
        public TextLayout TextAlign
        {
            get { return _TextAlign; }
            set { _TextAlign = value; }
        }

        private Bitmap _Icon = null;
        public Bitmap Icon
        {
            get { return _Icon; }
            set { _Icon = value; }
        }

        private Color _Color = Color.Silver;
        public Color Color
        {
            get { return _Color; }
            set { _Color = value; }
        }

        private Color _ShadowColor = Color.DimGray;
        public Color ShadowColor
        {
            get { return _ShadowColor; }
            set { _ShadowColor = value; }
        }

        private int _ShadowOpacity = 164;
        public int ShadowOpacity
        {
            get { return _ShadowOpacity; }
            set { _ShadowOpacity = value; }
        }


        private Font _Font = new Font("Helvetica", 11, FontStyle.Regular);
        public Font Font
        {
            get { return _Font; }
            set { _Font = value; }
        }

        private Color _ForeColor = Color.Black;
        public Color ForeColor
        {
            get { return _ForeColor; }
            set { _ForeColor = value; }
        }

        private RectangleF _Bounds;
        public RectangleF Bounds
        {
            get
            {
                return _Bounds;
            }
            set
            {
                _Bounds = value;
            }
        }

        private Color _Couleur = Color.LightGray;
        public Color Couleur
        {
            get
            {
                return _Couleur;
            }
            set
            {
                _Couleur = value;
            }
        }

        private object _Value;
        public object Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        private int _Index;
        public int Index
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
            }
        }

        private bool _Selected;
        public bool Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                _Selected = value;
            }
        }

        #endregion



        public static Font AppropriateFont(Graphics g, float minFontSize,
    float maxFontSize, Size layoutSize, string s, Font f)
        {
            SizeF extent;
            if (maxFontSize == minFontSize)
                f = new Font(f.FontFamily, minFontSize, f.Style);

            extent = g.MeasureString(s, f);

            if (maxFontSize <= minFontSize)
                return f;

            float hRatio = layoutSize.Height / extent.Height;
            float wRatio = layoutSize.Width / extent.Width;
            float ratio = (hRatio < wRatio) ? hRatio : wRatio;

            float newSize = f.Size * ratio;

            if (newSize < minFontSize)
                newSize = minFontSize;
            else if (newSize > maxFontSize)
                newSize = maxFontSize;

            f = new Font(f.FontFamily, newSize, f.Style);
            extent = g.MeasureString(s, f);

            return f;
        }


        public void Draw(Graphics g)
        {

            int corner = 10;

            int ButtonWidth = (int)Bounds.Width;

            #region Création rectangle + ombre + couleurs

            // Rectangle rect = new Rectangle(0, 0, ButtonWidth, ButtonWidth);
            RectangleF rect = new RectangleF(Bounds.X + (Bounds.Width - ButtonWidth) / 2, Bounds.Y + (Bounds.Height - ButtonWidth) / 2, ButtonWidth, ButtonWidth);
            GraphicsPath roundrect = CreateRoundedRectanglePath(rect, corner);

            RectangleF shadowRect = new RectangleF(rect.X, rect.Y + 3, ButtonWidth, ButtonWidth);
            GraphicsPath roundshad = CreateRoundedRectanglePath(shadowRect, corner);

            SolidBrush brushForRect = new SolidBrush(Color.FromArgb(255, Couleur));
            SolidBrush brushForShad = new SolidBrush(Color.FromArgb(128, Color.Gray));

            g.FillPath(brushForShad, roundshad);
            g.FillPath(brushForRect, roundrect);

            #endregion

            #region Reflet du bas

            RectangleF rectBas = new RectangleF(rect.X, rect.Bottom - (rect.Height / 5f), rect.Width, (rect.Height / 5f));
            Region reg = new Region(roundrect);
            reg.Intersect(rectBas);

            LinearGradientBrush aRefletBrush = new LinearGradientBrush(new PointF(rectBas.X, rectBas.Top - 1), new PointF(rectBas.X, rectBas.Bottom), Color.Transparent, Color.FromArgb(125, Color.White));

            g.FillRegion(aRefletBrush, reg);

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

            g.FillRegion(brGlow, reg);

            //p_graphics.FillPath(brGlow, aRefletHaut);

            RectangleF recth = new RectangleF(rect.X, 0, rect.Width, (rect.Height / 5f));
            reg = new Region(roundrect);
            reg.Intersect(recth);

            aRefletBrush = new LinearGradientBrush(new PointF(recth.X, recth.Top - 1), new PointF(recth.X, recth.Bottom), Color.FromArgb(125, Color.White), Color.Transparent);

            g.FillRegion(aRefletBrush, reg);

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

            g.DrawPath(aGradientPen, roundrect);


            #endregion

            #region Texte/Image du bouton

            if (Icon != null)
            {

                int w = (int)Math.Min(rect.Width, Icon.Width);
                int h = (int)Math.Min(rect.Height, Icon.Height);
                int x = (int)rect.X + ((int)rect.Width + w) / 2;
                int y = (int)rect.Y + ((int)rect.Height + h) / 2;


                Rectangle DestRect = new Rectangle(x, y, w, h);

                g.DrawImage(Icon, rect);
            }

            if (this.Value is Image)
            {
                float w = Math.Min(rect.Width, ((Image)this.Value).Width);
                float h = Math.Min(rect.Height, ((Image)this.Value).Height);
                float x = rect.X + (rect.Width - w) / 2f;
                float y = rect.Y + (rect.Height - h) / 2f;


                RectangleF DestRect = new RectangleF(x,y,w,h);
                g.DrawImage(((Image)this.Value),DestRect);
            }

            /*g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            SizeF sz = g.MeasureString(Libelle, Font, (int)rect.Width);
            string txt = Libelle;
            int l = txt.Length;
            while (sz.Height > rect.Height)
            {
                txt = Libelle.Substring(0, l) + "...";
                sz = g.MeasureString(txt, Font, (int)rect.Width);
                l--;
            }*/
            if (!(this.Value is Image))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                string _zText = Libelle;

                
                if (TextAlign == TextLayout.Center)
                {
                    SolidBrush textBrush = new SolidBrush(ForeColor);

                    //create  new canvas and set the size
                    SizeF canvasSizeF = new SizeF(rect.Width, rect.Height);

                    //specify formatting options
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.FormatFlags = StringFormatFlags.FitBlackBox & StringFormatFlags.NoClip;
                    stringFormat.LineAlignment = StringAlignment.Center;
                    stringFormat.Alignment = StringAlignment.Center;


                    //set point for where to start drawing the text
                    PointF textPoint = new PointF(0, 0);



                    //define brush with color to paint text with
                    textBrush = new SolidBrush(ForeColor);

                    //apply text
                    g.DrawString(_zText, Font, textBrush, rect, stringFormat);


                    textBrush = null;
                    g = null;
                    stringFormat = null;
                }

                if (TextAlign == TextLayout.Down)
                {
                    //ForeColor = Color.Black;
                    SolidBrush textBrush = new SolidBrush(ForeColor);

                    RectangleF rectText = new RectangleF(0, rect.Width + 5, rect.Width + 20, 20);

                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Near;

                    g.DrawString(Libelle, Font, textBrush, rectText, stringFormat);
                }
            }
            #endregion

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
    }
}
