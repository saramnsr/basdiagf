using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlsLibrary.EasyTrackBar
{
    public partial class EasyTrackBar : UserControl
    {
        public EasyTrackBar()
        {
            InitializeComponent();

            this.SetStyle(
           ControlStyles.UserPaint |
           ControlStyles.AllPaintingInWmPaint |
           ControlStyles.DoubleBuffer, true);

        }

        public enum EnumDisplayMode
        {
            IsVertical,
            IsHorizontal
        }

        public EnumDisplayMode _DisplayMode = EnumDisplayMode.IsVertical;

        AbstractRenderer renderer = new StandardRenderer();


        private int MARGEWIDTH = 20;
        private int HOLEHEIGHT = 3;

        private int MARGEHEIGHT = 10;
        private int HOLEWIDTH = 3;


        private int BUTTONWIDTH = 40;

        private double _min = 0;
        public double Min
        {
            set
            {
                _min = value;
                percent = (value - _min) / (_max - _min);
                Invalidate();
            }
            get { return _min; }
        }

        private double _max = 100;
        public double Max
        {
            set
            {
                _max = value;
                percent = (value - _min) / (_max - _min);
                Invalidate();
            }
            get { return _max; }
        }


        public EnumDisplayMode DisplayMode
        {
            set
            {
                _DisplayMode = value;
                int tmp = Height;
                Height = Width;
                Width = tmp;
            }
            get
            {
                return _DisplayMode;
            }
        }

        private double PxlValue
        {
            set
            {
                double _pxlValue = 0;
                if (DisplayMode == EnumDisplayMode.IsVertical)
                {
                    if (value < MARGEHEIGHT) _pxlValue = MARGEHEIGHT;
                    else
                        if (value > Height - (MARGEHEIGHT)) _pxlValue = Height - (MARGEHEIGHT);
                        else
                            _pxlValue = value;
                    percent = (_pxlValue - MARGEHEIGHT) / ((Height - (MARGEHEIGHT)) - MARGEHEIGHT);
                }
                else
                {
                    if (value < MARGEWIDTH) _pxlValue = MARGEWIDTH;
                    else
                        if (value > Width - (MARGEWIDTH)) 
                            _pxlValue = Width - (MARGEWIDTH);
                        else
                            _pxlValue = value;
                    percent = (_pxlValue - MARGEWIDTH) / ((Width - (MARGEWIDTH)) - MARGEWIDTH);

                }

                if (ValueChange != null) ValueChange(this, new EventArgs());
            }
            get
            {
                if (DisplayMode == EnumDisplayMode.IsVertical)
                {
                    return MARGEHEIGHT + (percent * (Height - (2 * MARGEHEIGHT)));
                }
                else
                {
                    return MARGEWIDTH + (percent * (Width - (2 * MARGEWIDTH)));
                }

            }
        }


        private double percent = 0;
        public double Value
        {
            set
            {
                percent = (value - _min) / (_max - _min);
                Invalidate();
            }
            get
            {

                double ret = (((_max - _min) * percent) + _min);
                return ret;
            }
        }


        [Category("Configuration"), Browsable(true), Description("Evènement intervenant au changement de valeur")]
        public event EventHandler ValueChange;

        public enum enumHitter
        {
            Nothing,
            OnHole,
            OnButton
        }

        private Font ft = new Font("Arial", 4, FontStyle.Regular, GraphicsUnit.Point);

        public enumHitter HittestAt(Point pt)
        {
            Rectangle hitrect;

            if (DisplayMode == EnumDisplayMode.IsVertical)
                hitrect = new Rectangle(MARGEWIDTH,
                                   (Convert.ToInt32(PxlValue) - (MARGEHEIGHT / 2)),
                                   this.Width - 2 * MARGEWIDTH,
                                   BUTTONWIDTH);
            else
                hitrect = new Rectangle((Convert.ToInt32(PxlValue) - (BUTTONWIDTH / 2)),
                                   MARGEHEIGHT,
                                   BUTTONWIDTH,
                                   this.Height - 2 * MARGEHEIGHT);

            if ((pt.X > hitrect.Left) && (pt.X < hitrect.Right) && (pt.Y > hitrect.Top) && (pt.Y < hitrect.Bottom)) return enumHitter.OnButton;


            if (DisplayMode == EnumDisplayMode.IsVertical)
                hitrect = new Rectangle((Width - HOLEWIDTH) / 2, MARGEHEIGHT, HOLEWIDTH, this.Size.Height - 2 * MARGEHEIGHT);
            else
                hitrect = new Rectangle(MARGEWIDTH, (Height - HOLEHEIGHT) / 2, this.Size.Width - (2 * MARGEWIDTH), HOLEHEIGHT);
                
            if ((pt.X > hitrect.Left) && (pt.X < hitrect.Right) && (pt.Y > hitrect.Top) && (pt.Y < hitrect.Bottom)) return enumHitter.OnHole;

            return enumHitter.Nothing;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (DisplayMode == EnumDisplayMode.IsVertical)
            {
                //drawBar
                Rectangle r = new Rectangle((Width - HOLEWIDTH) / 2, MARGEHEIGHT, HOLEWIDTH, this.Size.Height - 2 * MARGEHEIGHT);
                renderer.DrawBar(e.Graphics, r);


                //drawButton
                r = new Rectangle(MARGEWIDTH,
                                   (Convert.ToInt32(PxlValue) - (MARGEHEIGHT / 2)),
                                   this.Width - 2 * MARGEWIDTH,
                                   BUTTONWIDTH);
                renderer.DrawButton(e.Graphics, r, this.Focused);

                //e.Graphics.DrawString(Value.ToString(), ft, Brushes.Black, new Point(0, 0));
            }
            else
            {




                //drawBar
                Rectangle r = new Rectangle(MARGEWIDTH, (Height - HOLEHEIGHT) / 2, this.Size.Width - (2 * MARGEWIDTH), HOLEHEIGHT);
                renderer.DrawBar(e.Graphics, r);


                //drawButton
                r = new Rectangle((Convert.ToInt32(PxlValue) - (BUTTONWIDTH / 2)),
                                   MARGEHEIGHT,
                                   BUTTONWIDTH,
                                   this.Height - 2 * MARGEHEIGHT);
                renderer.DrawButton(e.Graphics, r, this.Focused);

                //e.Graphics.DrawString(Value.ToString(), ft, Brushes.Black, new Point(0, 0));
            }

        }

        private enumHitter _mousedownOn;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _mousedownOn = HittestAt(new Point(e.X, e.Y));

            if (_mousedownOn == enumHitter.OnHole)
            {
                if (DisplayMode == EnumDisplayMode.IsVertical)
                    PxlValue = e.Y;
                else
                    PxlValue = e.X;
                Invalidate();
            }

        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Left)
            {
                switch (_mousedownOn)
                {
                    case enumHitter.OnButton:
                        if (DisplayMode == EnumDisplayMode.IsVertical)
                            PxlValue = e.Y;
                        else
                            PxlValue = e.X;
                        Invalidate();
                        break;
                    case enumHitter.OnHole:
                        if (DisplayMode == EnumDisplayMode.IsVertical)
                            PxlValue = e.Y;
                        else
                            PxlValue = e.X;
                        Invalidate();
                        break;
                }
            }
        }
    }
}
