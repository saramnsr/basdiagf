using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using BASEPractice.Ctrls.IPhoneCtrls;

namespace BASE_CONTACT.Ctrls.IPhoneControls.IPhoneComboBox
{
    public partial class IPhoneComboBox : UserControl
    {
        public event EventHandler OnSelectChanged;
        #region Properties

        private List<object> _Items = new List<object>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public List<object> Items
        {
            get { return _Items; }
            set { _Items = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public object SelectedItem
        {
            get { return Items.Count==0?null:Items[_SelectedIndex]; }
            set
            {
                int idx = 0;
                foreach (object o in Items)
                {
                    if (value == o)
                    {
                        SelectedIndex = idx;
                        break;
                    }
                    idx++;
                }
            }
        }

        private int _SelectedIndex;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set { _SelectedIndex = value; }
        }

       

        private int _Width = 80;
        public int ButtonWidth
        {
            get { return _Width; }
            set { _Width = value; Invalidate(); }
        }

        private int _Height = 80;
        public int ButtonHeight
        {
            get { return _Height; }
            set { _Height = value; Invalidate(); }
        }

        private string _Text = "";
        public string Libelle
        {
            get
            {
                if (SelectedItem != null)
                    return SelectedItem.ToString();
                else
                    return "";
            }
            set
            {
                _Text = value; Invalidate();
            }
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

        private Color _Color = Color.Silver;
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

        private Font _Font = new Font("Helvetica", 11, FontStyle.Regular);
        public virtual new Font Font
        {
            get { return _Font; }
            set { _Font = value; Invalidate(); }
        }

        private Color _ForeColor = Color.Black;
        public virtual new Color ForeColor
        {
            get { return _ForeColor; }
            set { _ForeColor = value; Invalidate(); }
        }

        private frmComboChoice frmChoices;

        #endregion

        public IPhoneComboBox()
        {
            InitializeComponent();

            frmChoices = new frmComboChoice(this);
            frmChoices.Visible = false;


            //Mécanisme de DoubleBuffering de Windows afin
            //d’éviter les scintillements de rafraîchissement
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

        }

        private void IPhoneComboBox_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            #region Création rectangle + ombre + couleurs

            Rectangle rect = new Rectangle((Width - ButtonWidth) / 2, (Height - ButtonHeight) / 2, ButtonWidth, ButtonHeight);
            GraphicsPath roundrect = GraphicUtils.CreateRoundedRectanglePath(rect, 5);

            Rectangle shadowRect = new Rectangle((Width - ButtonWidth) / 2, (Height - ButtonHeight) / 2 + 3, ButtonWidth, ButtonHeight);
            GraphicsPath roundshad = GraphicUtils.CreateRoundedRectanglePath(shadowRect, 5);

            SolidBrush brushForRect = new SolidBrush(Color.FromArgb(255, Color));
            SolidBrush brushForShad = new SolidBrush(Color.FromArgb(ShadowOpacity, ShadowColor));

            e.Graphics.FillPath(brushForShad, roundshad);
            e.Graphics.FillPath(brushForRect, roundrect);

            #endregion

            #region Reflet du bas

            RectangleF rectBas = new RectangleF(rect.X, rect.Bottom - (rect.Height / 5f), rect.Width, (rect.Height / 5f));
            Region reg = new Region(roundrect);
            reg.Intersect(rectBas);

            LinearGradientBrush aRefletBrush = new LinearGradientBrush(new PointF(rectBas.X, rectBas.Top - 1), new PointF(rectBas.X, rectBas.Bottom), Color.Transparent, Color.FromArgb(125, Color.White));

            e.Graphics.FillRegion(aRefletBrush, reg);

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
                                                                Color.Transparent, Color.White);


            reg = new Region(roundrect);
            reg.Intersect(aRefletHaut);

            e.Graphics.FillRegion(brGlow, reg);
            
            #endregion

            #region Gradient (contour)

            Rectangle rectGradient = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
            LinearGradientBrush aGradientBrush = new LinearGradientBrush((RectangleF)rectGradient, Color.Transparent, Color.FromArgb(64, Color.White), LinearGradientMode.Vertical );
            
            ColorBlend blend = new ColorBlend();
            Color[] colors = new Color[] { Color.FromArgb(20, Color.White), Color.Transparent, Color.FromArgb(192, Color.White) };
            Single[] points = new Single[] {0, 0.5f, 1};

            blend.Colors = colors;
            blend.Positions = points;

            aGradientBrush.InterpolationColors = blend;
            
            Pen aGradientPen = new Pen(aGradientBrush,1);

            rectGradient.Inflate(-3, -3);
            roundrect = GraphicUtils.CreateRoundedRectanglePath(rectGradient, 5);

            e.Graphics.DrawPath(aGradientPen, roundrect);

            
            #endregion

        
            #region Texte/Image du bouton

            if (Icon != null)
            {

                int w = (int)Math.Min(rect.Width, Icon.Width);
                int h = (int)Math.Min(rect.Height, Icon.Height);
                int x = (int)rect.X + ((int)rect.Width + w) / 2;
                int y = (int)rect.Y + ((int)rect.Height + h) / 2;


                Rectangle DestRect = new Rectangle(x, y, w, h);

                e.Graphics.DrawImage(Icon, rect);
            }

            if (this.SelectedItem == null) return;

            if (this.SelectedItem is Image)
            {
                float w = Math.Min(rect.Width, ((Image)this.SelectedItem).Width);
                float h = Math.Min(rect.Height, ((Image)this.SelectedItem).Height);
                float x = rect.X + (rect.Width - w) / 2f;
                float y = rect.Y + (rect.Height - h) / 2f;


                RectangleF DestRect = new RectangleF(x, y, w, h);
                e.Graphics.DrawImage(((Image)this.SelectedItem), DestRect);
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

 
            if (!(this.SelectedItem is Image))
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                string _zText = Libelle;

                string longestword = "";

                foreach (string s in _zText.ToString().Split(' '))
                        if (s.Length > longestword.Length) longestword = s;


                _Font = IPhoneComboBoxBtn.AppropriateFont(e.Graphics, 6f, Font.Size, Size.Round(rect.Size), longestword, Font);
              
                
                

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
                    e.Graphics.DrawString(_zText, Font, textBrush, rect, stringFormat);


                    textBrush = null;
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

                    e.Graphics.DrawString(Libelle, Font, textBrush, rectText, stringFormat);
                }
            }
            #endregion

        }

        protected override void OnClick(EventArgs e)
        {

            base.OnClick(e);
            ShowChoices();
        }

        private void ShowChoices()
        {
            int margin = 20;
            frmChoices.ButtonsHeight = Math.Min(this.ButtonHeight, this.ButtonWidth);
            if (frmChoices.slidemode == frmComboChoice.SlideMode.Horizontal)
            {
                frmChoices.Width = Math.Min(800, (frmChoices.ButtonsHeight * Items.Count)+(Items.Count*5));
                frmChoices.Height = frmChoices.ButtonsHeight + margin;
                frmChoices.ButtonsLocation = margin / 2;
            }

            if (frmChoices.slidemode == frmComboChoice.SlideMode.Vertical)
            {
                frmChoices.Width = frmChoices.ButtonsHeight + margin;
                frmChoices.Height = Math.Min(600, (frmChoices.ButtonsHeight * Items.Count) + (Items.Count * 5)); ;
                frmChoices.ButtonsLocation = margin / 2;
            }

            if (frmChoices.slidemode == frmComboChoice.SlideMode.None)
            {
                frmChoices.Width = 800;
                frmChoices.Height = 600;
            }

            Point ptoscreen = this.PointToScreen(new Point(0, 0));
            frmChoices.Left = (Width - frmChoices.Width) / 2 + ptoscreen.X;
            frmChoices.Top = (Height - frmChoices.Height) / 2 + ptoscreen.Y;

            if (frmChoices.Left < 0) frmChoices.Left = 5;
            if (frmChoices.Right > Screen.GetBounds(this).Right) frmChoices.Left = frmChoices.Left - (frmChoices.Right - Screen.GetBounds(this).Right) - 5;
            if (frmChoices.Top < 0) frmChoices.Top = 5;
            if (frmChoices.Bottom > Screen.GetBounds(this).Bottom) frmChoices.Top = frmChoices.Top - (frmChoices.Bottom - Screen.GetBounds(this).Bottom) - 5;

            frmChoices.BoutonsDataBind(Items);
            frmChoices.Show();
            frmChoices.FormClosing += new FormClosingEventHandler(frmChoices_FormClosing);
        }

        void frmChoices_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (frmChoices.AnswerTxt != null)
            {
                this.SelectedIndex = frmChoices.AnswerIdx;
                this.Libelle = frmChoices.AnswerTxt;
                if (OnSelectChanged != null) OnSelectChanged(this, new EventArgs());

            }
            else
            {

                this.Libelle = "";
            }
        }
    }
}
