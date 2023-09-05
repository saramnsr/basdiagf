using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WindowsFormsControlLibrary1.IPhoneControls.IPhoneButton
{
    public partial class IPhoneButton : UserControl
    {
        #region Properties

        private int _Width = 80;
        public int ButtonWidth
        {
            get { return _Width; }
            set { _Width = value; Invalidate(); }
        }

        private string _Text = "button avec un long texte";
        public string Libelle
        {
            get { return _Text; }
            set { _Text = value; Invalidate(); }
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
            set { _TextAlign = value; Invalidate(); }
        }

        private Bitmap _Icon = null;
        public Bitmap Icon
        {
            get { return _Icon; }
            set { _Icon = value; Invalidate(); }
        }

        private Color _Color = Color.CornflowerBlue;
        public Color Color
        {
            get { return _Color; }
            set { _Color = value; Invalidate(); }
        }

        private Color _ShadowColor = Color.DimGray;
        public Color ShadowColor
        {
            get { return _ShadowColor; }
            set { _ShadowColor = value; Invalidate(); }
        }

        private int _ShadowOpacity = 164;
        public int ShadowOpacity
        {
            get { return _ShadowOpacity; }
            set { _ShadowOpacity = value; Invalidate(); }
        }


        public new Font Font
        {
            get { return base.Font; }
            set { base.Font = value; Invalidate(); }
        }

        public new Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; Invalidate(); }
        }

        #endregion

        public IPhoneButton()
        {
            InitializeComponent();
            //Mécanisme de DoubleBuffering de Windows afin
            //d’éviter les scintillements de rafraîchissement
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

        }

        private void IPhoneButton_Load(object sender, EventArgs e)
        {

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


        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            #region Création rectangle + ombre + couleurs

           // Rectangle rect = new Rectangle(0, 0, ButtonWidth, ButtonWidth);
            Rectangle rect = new Rectangle((Width - ButtonWidth) / 2, (Height - ButtonWidth) / 2, ButtonWidth, ButtonWidth);
            GraphicsPath roundrect = CreateRoundedRectanglePath(rect, 10);

            Rectangle shadowRect = new Rectangle(rect.X, rect.Y+3, ButtonWidth, ButtonWidth);
            GraphicsPath roundshad = CreateRoundedRectanglePath(shadowRect, 10);

            SolidBrush brushForRect = new SolidBrush(Color.FromArgb(255, Color));
            SolidBrush brushForShad = new SolidBrush(Color.FromArgb(ShadowOpacity, ShadowColor));

            e.Graphics.FillPath(brushForShad, roundshad);
            e.Graphics.FillPath(brushForRect, roundrect);

            #endregion
            
            #region Reflet du bas

            RectangleF rectBas = new RectangleF(rect.X, rect.Bottom - (rect.Height / 5f), rect.Width, (rect.Height / 5f));
            Region reg = new Region(roundrect);
            reg.Intersect(rectBas);
            
            LinearGradientBrush aRefletBrush = new LinearGradientBrush(new PointF(rectBas.X,rectBas.Top-1),new PointF(rectBas.X,rectBas.Bottom), Color.Transparent, Color.FromArgb(125, Color.White));

            e.Graphics.FillRegion(aRefletBrush, reg);
            
            #endregion

            #region Reflet du haut

            GraphicsPath aRefletHaut = new GraphicsPath();
            aRefletHaut.AddLine(new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + (rect.Height / 2) - 5));

            aRefletHaut.AddLine(new Point(rect.X + rect.Width, rect.Y), new Point(rect.X, rect.Y));
            aRefletHaut.AddLine(new Point(rect.X, rect.Y + (rect.Height / 2) - 5), new Point(rect.X, rect.Y));

            aRefletHaut.AddBezier(new Point(rect.X, rect.Y + (rect.Height / 2) - 5),
                                                new Point(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2)),
                                                new Point(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2)),
                                                new Point(rect.X + rect.Width, rect.Y + (rect.Height / 2) - 5));
                                    
            RectangleF rectGlow = new RectangleF(rect.Left, rect.Top-1, rect.Width+1, rect.Height / 2+2);

            LinearGradientBrush brGlow = new LinearGradientBrush(new PointF(rectGlow.Left, rectGlow.Bottom),
                                                                new PointF(rectGlow.Left, rectGlow.Top),
                                                                Color.FromArgb(30, Color.White), Color.FromArgb(250, Color.White));


            reg = new Region(roundrect);
            reg.Intersect(aRefletHaut);
                      
            e.Graphics.FillRegion(brGlow, reg);
            e.Graphics.DrawPath(new Pen(Color.FromArgb(30, Color.White)), aRefletHaut);

            RectangleF recth = new RectangleF(rect.X, 0, rect.Width, (rect.Height / 5f));
            reg = new Region(roundrect);
            reg.Intersect(recth);

            aRefletBrush = new LinearGradientBrush(new PointF(recth.X, recth.Top - 1), new PointF(recth.X, recth.Bottom), Color.FromArgb(125, Color.White), Color.Transparent);

            e.Graphics.FillRegion(aRefletBrush, reg);

            //e.Graphics.DrawPath(Pens.Red, aRefletHaut);
            
            #endregion

            #region Gradient (contour)

            Rectangle rectGradient = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
            LinearGradientBrush aGradientBrush = new LinearGradientBrush((RectangleF)rectGradient, Color.Transparent, Color.FromArgb(64, Color.White), LinearGradientMode.Vertical );
            
            ColorBlend blend = new ColorBlend();
            Color[] colors = new Color[] { Color.FromArgb(128, Color.White), Color.Transparent, Color.FromArgb(128, Color.White) };
            Single[] points = new Single[] {0, 0.5f, 1};

            blend.Colors = colors;
            blend.Positions = points;

            aGradientBrush.InterpolationColors = blend;
            
            Pen aGradientPen = new Pen(aGradientBrush,2);

            rectGradient.Inflate(-3, -3);
            roundrect = CreateRoundedRectanglePath(rectGradient, 5);

            e.Graphics.DrawPath(aGradientPen, roundrect);

            
            #endregion

            #region Texte/Image du bouton

            if (Icon != null)
            {
                e.Graphics.DrawImage(Icon, (ButtonWidth / 2) - 20, (ButtonWidth / 2) - 20, 40, 40);
            }

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            SizeF sz = e.Graphics.MeasureString(Libelle,Font,rect.Width);
            string txt = Libelle;
            int l = txt.Length;
            while (sz.Height > rect.Height)
            {
                txt = Libelle.Substring(0, l) + "...";
                sz = e.Graphics.MeasureString(txt, Font, rect.Width);
                l--;
            }

            //if (Text.Length > 12)
            //{
                
            //    for (int i = 0; i < 12; i++)
            //    {
            //        txt += Text[i];
            //    }
            //    txt += "...";
            //    _Text = txt;
            //}

            
            if (TextAlign == TextLayout.Center)
            {
                Graphics g = e.Graphics;

                Font font = Font;
                string _zText = Libelle;

                string longestword = "";

                foreach (string s in _zText.Split(' '))
                    if (s.Length > longestword.Length) longestword = s;

                //antialias fonts.
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                //create  new canvas and set the size
                SizeF canvasSizeF = new SizeF(Width - 5, Height - 5);

                //specify formatting options
                StringFormat stringFormat = new StringFormat();
                stringFormat.FormatFlags = StringFormatFlags.FitBlackBox & StringFormatFlags.LineLimit;
                stringFormat.LineAlignment = StringAlignment.Center;
                stringFormat.Alignment = StringAlignment.Center;

                SizeF textSizeF = new SizeF(0, 0);

                //fit text
                while (true)
                {
                    //measure text
                    textSizeF = g.MeasureString(longestword, font);

                    //see if it fits
                    if (canvasSizeF.Width >= textSizeF.Width)
                        break;
                    else
                        //text doesn't fit.  lower point size and try again.
                        font = new Font(font.FontFamily, font.SizeInPoints - 1, font.Style);
                }

                //set point for where to start drawing the text
                PointF textPoint = new PointF(0, 0);

                //define brush with color to paint text with
                SolidBrush textBrush = new SolidBrush(ForeColor);

                //apply text
                g.DrawString(_zText, font, textBrush, new RectangleF(textPoint, canvasSizeF), stringFormat);


                textBrush = null;
                g = null;
                stringFormat = null;
            }

            if (TextAlign == TextLayout.Down)
            {
                //ForeColor = Color.Black;
                SolidBrush txtBrush = new SolidBrush(ForeColor);

                Rectangle rectText = new Rectangle(0, rect.Width + 5, rect.Width + 20, 20);
                StringFormat strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Near;

                e.Graphics.DrawString(Libelle, Font, txtBrush, rectText, strFormat);
            }

            #endregion

            base.OnPaint(e);
            
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
        }
    }
}
