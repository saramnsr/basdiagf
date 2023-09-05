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

namespace BASE_CONTACT.Ctrls.IPhoneControls.IPhoneListBox
{
    public partial class IPhoneListBox : UserControl
    {
        #region Properties


        public event EventHandler OnSelectionChange;

        private int totalWidth = 0;

        private bool _MultiSelect = false;
        public bool MultiSelect
        {
            get
            {
                return _MultiSelect;
            }
            set
            {
                _MultiSelect = value;
            }
        }

        private List<object> _Items = new List<object>();        
        public List<object> Items
        {
            get { return _Items; }
            set { _Items = value; }
        }

        
        public object SelectedItem
        {
            get { return SelectedIndices.Count > 0 ? Items[SelectedIndices[0]] : null; }
        }


        private List<int> _SelectedIndices = new List<int>();
        public List<int> SelectedIndices
        {
            get { return _SelectedIndices; }
            set { _SelectedIndices = value; }
        }

        private List<IPhoneListBoxBtn> _Btns = new List<IPhoneListBoxBtn>();
        private List<IPhoneListBoxBtn> Btns
        {
            get
            {
                return _Btns;
            }
            set
            {
                _Btns = value;
            }
        }

        private string _Text = "Button";
        public virtual new string Text
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

        private Font _ItemFont = new Font("Helvetica", 11, FontStyle.Regular);
        public Font ItemFont
        {
            get { return _ItemFont; }
            set { _ItemFont = value; Invalidate(); }
        }

        private Color _ForeColor = Color.Black;
        public new Color ForeColor
        {
            get { return _ForeColor; }
            set { _ForeColor = value; Invalidate(); }
        }

        private Point MouseIsDownAt;
        private bool MouseIsDown = false;
        private bool IsSlideOn = false;

        private Point _SlideOffSet = new Point(0, 0);
        private Point SlideOffSetWhenMouseIsDown = new Point(0, 0);
        public Point SlideOffSet
        {
            get
            {
                return _SlideOffSet;
            }
            set
            {
                _SlideOffSet = value;
            }
        }

        public enum SlideMode
        {
            None,
            Horizontal,
            Vertical
        }

        private SlideMode _slidemode = SlideMode.Horizontal;
        public SlideMode slidemode
        {
            get
            {
                return _slidemode;
            }
            set
            {
                _slidemode = value;
            }
        }

        private int _ItemWidth = 80;
        public int ItemWidth
        {
            get { return _ItemWidth; }
            set { _ItemWidth = value; Invalidate(); }
        }

        private int _ItemHeight = 80;
        public int ItemHeight
        {
            get { return _ItemHeight; }
            set { _ItemHeight = value; Invalidate(); }
        }


        

        private int totalHeight = 0;

        #endregion

        public IPhoneListBox()
        {
            InitializeComponent();
            
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
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            e.Graphics.FillRectangle(new SolidBrush(BackColor), new Rectangle(0, 0, Width, Height));

           // e.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);

            DataBind();

            RecalculateBtns(e.Graphics);
            DrawBoutons(e.Graphics);

            if (!Enabled)
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(120,Color.Gray)), new Rectangle(0, 0, Width, Height));

            base.OnPaint(e);
        }

        private void DrawBoutons(Graphics g)
        {
            int i = 0;
            foreach (IPhoneListBoxBtn btn in Btns)
            {
                btn.Draw(g);
                i++;
            }
        }

        public void DataBind()
        {
            int index = 0;
            Btns.Clear();
            foreach (object obj in Items)
            {
                IPhoneListBoxBtn btn = new IPhoneListBoxBtn();
                btn.Value = obj;
                btn.Libelle = obj.ToString();
                btn.Index = index;
                btn.Selected = SelectedIndices.Contains(index);
                btn.Font = ItemFont;

                Btns.Add(btn);

                index++;
            }
        }

        

        private void RecalculateBtns(Graphics g)
        {
            
            string longestword = "";
            if (Btns.Count == 0) return;

            int idx = 0;

            //int MaxWidth = ItemWidth;
            int IntervallesY = 10;
            int Nblines = 1;
            int BtnPerLine = 1;
            int IntervallesX = 5;
            float currentX;
            float currentY;
            
            totalHeight = (Btns.Count * ItemHeight) + (IntervallesX * Btns.Count);

            if (slidemode == SlideMode.None)
            {
                BtnPerLine = Btns.Count;
                if (BtnPerLine <= 0) BtnPerLine = 1;
                IntervallesX = (int)((Width - (BtnPerLine * ItemWidth)) / BtnPerLine);

                while ((IntervallesX < 0) && (BtnPerLine > 1))
                {
                    BtnPerLine--;
                    IntervallesX = (int)((Width - (BtnPerLine * ItemWidth)) / BtnPerLine);
                }
                Nblines = (int)Math.Ceiling(Btns.Count / (float)BtnPerLine);

                currentX = (IntervallesX / 2);
                currentY = 0;
            }
            else
            {
                if (slidemode == SlideMode.Vertical)
                {
                    currentX = (Width - ItemWidth) / 2;
                }
                else
                    currentX = SlideOffSet.X;
                
                currentY = SlideOffSet.Y;
            }

            int xcounter = 0;
            foreach (IPhoneListBoxBtn btn in Btns)
            {
                RectangleF r = new RectangleF(currentX, currentY, ItemWidth, ItemHeight);
                btn.Bounds = r;
                btn.Font = this.Font;

                currentX += btn.Bounds.Width + IntervallesX;
                xcounter++;
                idx++;
                if (((xcounter >= BtnPerLine) && (slidemode == SlideMode.None)) || (slidemode == SlideMode.Vertical))
                {

                    xcounter = 0;

                    switch (slidemode)
                    {
                        case SlideMode.None: currentX = (IntervallesX / 2); break;
                        case SlideMode.Vertical: currentX = (Width - ItemWidth) / 2; break;
                        case SlideMode.Horizontal: currentX = SlideOffSet.X; break;
                    }

                    currentY += ItemHeight + IntervallesY;
                }

                foreach (string s in btn.Libelle.Split(' '))
                    if (s.Length > longestword.Length) longestword = s;
            }
            
            foreach (object o in Items)
                foreach (string s in o.ToString().Split(' '))
                    if (s.Length > longestword.Length) longestword = s;

            Font BestFont = IPhoneListBoxBtn.AppropriateFont(g, 6f, Font.Size, new Size(ItemWidth, ItemHeight), longestword, Font);
            foreach (IPhoneListBoxBtn btn in Btns)
                btn.Font = BestFont;

            totalWidth = (((int)currentX + ItemWidth)) + (IntervallesX * Btns.Count);
            
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            MouseIsDown = false;

            object hittedOn = Hittest(new Point(e.X, e.Y));

            if ((hittedOn is IPhoneListBoxBtn) && (!IsSlideOn))
            {


                if (!((IPhoneListBoxBtn)hittedOn).Selected)
                {
                    if (!MultiSelect)
                    {
                        foreach (IPhoneListBoxBtn btn in Btns) btn.Selected = false;
                        SelectedIndices.Clear();
                    }

                    SelectedIndices.Add(((IPhoneListBoxBtn)hittedOn).Index);
                    ((IPhoneListBoxBtn)hittedOn).Selected = true;
                }
                else
                {
                    ((IPhoneListBoxBtn)hittedOn).Selected = !((IPhoneListBoxBtn)hittedOn).Selected;
                    SelectedIndices.Remove(((IPhoneListBoxBtn)hittedOn).Index);
                }
                if (OnSelectionChange != null) 
                    OnSelectionChange(this, new EventArgs());

            }

            Invalidate();

            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if ((MouseIsDown) && (slidemode != SlideMode.None))
            {
                IsSlideOn = ((Math.Abs(MouseIsDownAt.X - e.X) > 5) || (Math.Abs(MouseIsDownAt.Y - e.Y) > 5));
                Point tmp = new Point(0, 0);
                if (slidemode == SlideMode.Horizontal)
                    tmp = new Point(SlideOffSetWhenMouseIsDown.X - (MouseIsDownAt.X - e.X), SlideOffSetWhenMouseIsDown.Y);
                if (slidemode == SlideMode.Vertical)
                    if (e.Y > ItemHeight && e.Y < (totalHeight - ItemHeight))
                        tmp = new Point(SlideOffSetWhenMouseIsDown.X, SlideOffSetWhenMouseIsDown.Y - (MouseIsDownAt.Y - e.Y));
                
                if (tmp.X < 5)
                    SlideOffSet = tmp;
                
                

                Invalidate();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (slidemode != SlideMode.None)
            {
                MouseIsDown = true;
                IsSlideOn = false;
                MouseIsDownAt = new Point(e.X, e.Y);
                SlideOffSetWhenMouseIsDown = new Point(SlideOffSet.X, SlideOffSet.Y);
            }

            base.OnMouseDown(e);
        }

        private object Hittest(Point pt)
        {
            foreach (IPhoneListBoxBtn btn in Btns)
                if (btn.Bounds.Contains(pt))
                    return btn;
            
            return null;
        }
        
    }
}
