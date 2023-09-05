using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace BASE_CONTACT.Ctrls.IPhoneControls.IPhoneComboBox
{
    public partial class frmComboChoice : Form
    {      

        

        private IPhoneComboBox _owner;
        public IPhoneComboBox owner
        {
            get
            {
                return _owner;
            }
            set
            {
                _owner = value;
            }
        }

        #region Properties

        private List<IPhoneComboBoxBtn> _Btns = new List<IPhoneComboBoxBtn>();
        private List<IPhoneComboBoxBtn> Btns
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

        private int _ButtonsHeight = 80;
        public int ButtonsHeight
        {
            get
            {
                return _ButtonsHeight;
            }
            set
            {
                _ButtonsHeight = value;
            }
        }

        private int _ButtonsLocation = 65;
        public int ButtonsLocation
        {
            get
            {
                return _ButtonsLocation;
            }
            set
            {
                _ButtonsLocation = value;
            }
        }
        
        public event EventHandler Canceled;

        private object _Answer = new object();
        public object Answer
        {
            get
            {
                return _Answer;
            }
            set
            {
                _Answer = value;
            }
        }

        private int _AnswerIdx = 0;
        public int AnswerIdx
        {
            get
            {
                return _AnswerIdx;
            }
            set
            {
                _AnswerIdx = value;
            }
        }

        private string _AnswerTxt;
        public string AnswerTxt
        {
            get
            {
                return _AnswerTxt;
            }
            set
            {
                _AnswerTxt = value;
            }
        }

        private int totalWidth = 0;

        #endregion

        public frmComboChoice(IPhoneComboBox owner)
        {
            this.owner = owner;
            InitializeComponent();

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
        }

        private void frmComboChoice_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            e.Graphics.FillRectangle(new SolidBrush(BackColor), new Rectangle(0, 0, Width, Height));

            e.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);

            RecalculateBtns(e.Graphics);
            
            DrawBoutons(e.Graphics);

            base.OnPaint(e);
        }
        
        public void BoutonsDataBind(List<object> lstObj)
        {
            int index = 0;
            Btns.Clear();
            foreach (object obj in lstObj)
            {
                IPhoneComboBoxBtn btn = new IPhoneComboBoxBtn();
                btn.Value = obj;
                btn.Libelle = obj.ToString();
                btn.Index = index;
                btn.ButtonHeight = ButtonsHeight;
                btn.ButtonWidth = ButtonsHeight;

                Btns.Add(btn);

                index++;
            }
        }

        private void DrawBoutons(Graphics g)
        {
            foreach (IPhoneComboBoxBtn btn in Btns)
                btn.Draw(g);
        }

        private void RecalculateBtns(Graphics g)
        {
            if (Btns.Count == 0) return;

                int MaxWidth = ButtonsHeight;
                int IntervallesY = 10;
                int Nblines = 1;
                int BtnPerLine = 1;
                int IntervallesX = 5;
                float currentX;
                float currentY;

                totalWidth = (Btns.Count * MaxWidth) + (IntervallesX * Btns.Count);

                if (slidemode == SlideMode.None)
                {
                    BtnPerLine = Btns.Count;
                    if (BtnPerLine <= 0) BtnPerLine = 1;
                    IntervallesX = (int)((Width - (BtnPerLine * MaxWidth)) / BtnPerLine);

                    while ((IntervallesX < 0) && (BtnPerLine > 1))
                    {
                        BtnPerLine--;
                        IntervallesX = (int)((Width - (BtnPerLine * MaxWidth)) / BtnPerLine);
                    }
                    Nblines = (int)Math.Ceiling(Btns.Count / (float)BtnPerLine);

                    currentX = (IntervallesX / 2);
                    currentY = ButtonsLocation;
                }
                else
                {
                    if (slidemode == SlideMode.Vertical)
                        currentX = (Width - MaxWidth) / 2;
                    else
                        currentX = SlideOffSet.X;

                    currentY = ButtonsLocation + SlideOffSet.Y;
                }

                int xcounter = 0;
                string longestword = "";
                foreach (IPhoneComboBoxBtn btn in Btns)
                {
                    RectangleF r = new RectangleF(currentX, currentY, MaxWidth, ButtonsHeight);
                    btn.Bounds = r;
                    currentX += btn.Bounds.Width + IntervallesX;
                    xcounter++;
                    if (((xcounter >= BtnPerLine) && (slidemode == SlideMode.None)) || (slidemode == SlideMode.Vertical))
                    {
                        xcounter = 0;


                        switch (slidemode)
                        {
                            case SlideMode.None: currentX = (IntervallesX / 2); break;
                            case SlideMode.Vertical: currentX = (Width - MaxWidth) / 2; break;
                            case SlideMode.Horizontal: currentX = SlideOffSet.X; break;
                        }


                        currentY += ButtonsHeight + IntervallesY;
                    }
                }

                foreach (IPhoneComboBoxBtn btn in Btns)
                    foreach (string s in btn.Libelle.Split(' '))
                        if (s.Length > longestword.Length) longestword = s;

                Font BestFont = IPhoneComboBoxBtn.AppropriateFont(g, 6f, Font.Size, new Size(ButtonsHeight, ButtonsHeight), longestword, Font);
                foreach (IPhoneComboBoxBtn btn in Btns)
                    btn.Font = BestFont;
        }

        private void frmComboChoice_Deactivate(object sender, EventArgs e)
        {
            if (Canceled != null) Canceled(this, new EventArgs());
            Close();
        }

        private void frmComboChoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            MouseIsDown = false;
            
            if (e.Button == MouseButtons.Left && (Math.Abs(MouseIsDownAt.X - e.X) > 5 || Math.Abs(MouseIsDownAt.Y - e.Y) > 5))
            {

            }
            else
            {
                object hittedOn = Hittest(new Point(e.X, e.Y));

                if ((hittedOn is IPhoneComboBoxBtn) && (!IsSlideOn))
                {
                    if ((((IPhoneComboBoxBtn)hittedOn).Selected) && ((IPhoneComboBoxBtn)hittedOn) != Answer)
                    {
                        Answer = ((IPhoneComboBoxBtn)hittedOn).Value;
                        AnswerIdx = ((IPhoneComboBoxBtn)hittedOn).Index;
                        AnswerTxt = ((IPhoneComboBoxBtn)hittedOn).Libelle;
                                            }
                }

                Invalidate();
                Close();
            }

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
                    tmp = new Point(SlideOffSetWhenMouseIsDown.X, SlideOffSetWhenMouseIsDown.Y - (MouseIsDownAt.Y - e.Y));

                if (tmp.X < 5 && !((totalWidth + tmp.X) < this.Width))
                {
                    SlideOffSet = tmp;
                }

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
            foreach (IPhoneComboBoxBtn b in Btns)
                b.Selected = false;

            foreach (IPhoneComboBoxBtn btn in Btns)
                if (btn.Bounds.Contains(pt))
                {
                    btn.Selected = true;
                    return btn;
                }
            return null;
        }

        private void frmComboChoice_VisibleChanged(object sender, EventArgs e)
        {
            SlideOffSet = new Point(0, 0);
        }

    }
}
