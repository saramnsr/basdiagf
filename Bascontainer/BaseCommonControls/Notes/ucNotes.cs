using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseCommonControls
{
    public partial class ucNotes : UserControl
    {

         

        private bool _ReadOnly = false;
        public bool ReadOnly
        {
            get
            {
                return _ReadOnly;
            }
            set
            {
                _ReadOnly = value;
            }
        }

        public int RealValue
        {
            get
            {
                 return (int)((Max - Min) * (Value / 100)) + Min;
            }
           set
            {
                Value = ((float)value - Min) / (Max - Min) * 100;
                Invalidate();
            }
        }

        private bool _ClipOnStars = false;
        public bool ClipOnStars
        {
            get
            {
                return _ClipOnStars;
            }
            set
            {
                _ClipOnStars = value;
            }
        }

        bool MouseIsDown = false;

        private int _max = 20;
        public int Max
        {
            get
            {
                return _max;
            }
            set
            {
                _max = value;
            }
        }

        private int _min = 0;
        public int Min
        {
            get
            {
                return _min;
            }
            set
            {
                _min = value;
            }
        }

        private float _Value = 30;
        public float Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (value < 0) return;
                if (value > 100) return;

                if (ClipOnStars)
                {
                    //_Value = ((int)(value / (100 / NbEtoiles)) * (100 / NbEtoiles));
                    _Value = (float)Math.Round((value / (100 / (NbEtoiles * 2))) / 2f, MidpointRounding.ToEven);
                    _Value = ((int)(_Value) * (100 / NbEtoiles));
                }
                else
                    _Value = value;
            }
        }

        private int _NbEtoiles = 5;
        public int NbEtoiles
        {
            get
            {
                return _NbEtoiles;
            }
            set
            {
                _NbEtoiles = value;
            }
        }

        public ucNotes()
        {
            InitializeComponent();

            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);
        }


        private PointF[] FindStars( float r, float xc, float yc)
        {
            // r: determines the size of the star.
            // xc, yc: determine the location of the star.
            float sin36 = (float)Math.Sin(36.0 * Math.PI / 180.0);
            float sin72 = (float)Math.Sin(72.0 * Math.PI / 180.0);
            float cos36 = (float)Math.Cos(36.0 * Math.PI / 180.0);
            float cos72 = (float)Math.Cos(72.0 * Math.PI / 180.0);
            float r1 = r * cos72 / cos36;
            // Fill the star:
            PointF[] pts = new PointF[10];
            pts[0] = new PointF(xc, yc - r);
            pts[1] = new PointF(xc + r1 * sin36, yc - r1 * cos36);
            pts[2] = new PointF(xc + r * sin72, yc - r * cos72);
            pts[3] = new PointF(xc + r1 * sin72, yc + r1 * cos72);
            pts[4] = new PointF(xc + r * sin36, yc + r * cos36);
            pts[5] = new PointF(xc, yc + r1);
            pts[6] = new PointF(xc - r * sin36, yc + r * cos36);
            pts[7] = new PointF(xc - r1 * sin72, yc + r1 * cos72);
            pts[8] = new PointF(xc - r * sin72, yc - r * cos72);
            pts[9] = new PointF(xc - r1 * sin36, yc - r1 * cos36);
            return pts;
        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            MouseIsDown = false;
            Invalidate();
            base.OnMouseUp(e);

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
             
            MouseIsDown = true;
            if ((e.Button == MouseButtons.Left)&&(!ReadOnly))
            {
                float max = Height * NbEtoiles;
                Value = (e.X / max) * 100f;
                Invalidate();
            }
            base.OnMouseDown(e);
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && (!ReadOnly))
            {
                float max = Height * NbEtoiles;
                Value = (e.X / max) * 100f;
                Invalidate();
            }
            base.OnMouseMove(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int StarSquareSize = Height / 2;
            int max = Height * NbEtoiles;
            GraphicsPath gp = new GraphicsPath();
            for (int i = 0; i < NbEtoiles; i++)
            {
                gp.AddPolygon(FindStars(StarSquareSize, StarSquareSize + ((i * 2) * StarSquareSize), StarSquareSize));
            }

            RectangleF rect = new RectangleF(0, 0, max * (Value / 100f), Height);

            Region r = new Region(gp);
            r.Intersect(rect);

            e.Graphics.FillRegion(Brushes.Yellow,r);
            e.Graphics.DrawPath(Pens.LightGray, gp);

            if (MouseIsDown)
            {
               
                string s = RealValue.ToString();

                Font ft = new Font("Arial", 8, FontStyle.Regular);
                SizeF sz = e.Graphics.MeasureString(s, ft);

                e.Graphics.FillRectangle(Brushes.White, new RectangleF(0, 0, sz.Width, sz.Height));
                e.Graphics.DrawRectangle(Pens.LightGray, new Rectangle(0, 0, (int)sz.Width, (int)sz.Height));

                e.Graphics.DrawString(s, ft, Brushes.Gray, new PointF(0, 0));
            }

            base.OnPaint(e);
        }

        private void ucNotes_Load(object sender, EventArgs e)
        {

        }
    }
}
