using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BASEDiag.Ctrls
{

    

    public partial class ChoixDents : UserControl
    {


        List<Dent> Lstdents = new List<Dent>();

        private char _separator = ',';
        public char separator
        {
            get
            {
                return _separator;
            }
            set
            {
                _separator = value;
            }
        }

        public new int Width
        {
            get
            {
                return global::BASEDiag.Ctrls.ChoixDentRsx.AdulteEnfant.Width;
            }
            
        }

        public new int Height
        {
            get
            {
                return global::BASEDiag.Ctrls.ChoixDentRsx.AdulteEnfant.Height;
            }
            
        }

        [Browsable(true)]
        public String SelectedDents
        {
            get
            {
                String res = "";
                foreach (Dent d in Lstdents)
                    if (d.IsSelected)
                    {
                        if (res != "") res += _separator.ToString();
                        res += d.Name;
                    }
                return res;
            }
            set
            {
                string[] ss = value.Split(separator);
                foreach (Dent d in Lstdents)
                    d.IsSelected = false;
                foreach (string s in ss)
                {
                    foreach (Dent d in Lstdents)
                        if (s == d.Name) d.IsSelected = true;
                }
                Invalidate();
            }
        }

        public void InitDents()
        {

            #region 11-18
            Dent d = new Dent();
            d.Name = "11";
            d.Bound = new Rectangle(95, 13, 117 - 95, 37 - 13);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "12";
            d.Bound = new Rectangle(76, 27, 95 - 76, 47 - 27);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "13";
            d.Bound = new Rectangle(55, 36, 78 - 55, 57 - 36);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "14";
            d.Bound = new Rectangle(46, 56, 71 - 46, 79 - 56);
            Lstdents.Add(d);

            d = new Dent();
            d.Name = "15";
            d.Bound = new Rectangle(35, 77, 62 - 35, 102 - 77);
            Lstdents.Add(d);

            d = new Dent();
            d.Name = "16";
            d.Bound = new Rectangle(27, 99, 59 - 27, 131 - 99);
            Lstdents.Add(d);

            d = new Dent();
            d.Name = "17";
            d.Bound = new Rectangle(22, 129, 56 - 22, 160 - 129);
            Lstdents.Add(d);

            d = new Dent();
            d.Name = "18";
            d.Bound = new Rectangle(22, 159, 54 - 22, 189 - 159);
            Lstdents.Add(d);

            #endregion

            #region 21-28
            d = new Dent();
            d.Name = "21";
            d.Bound = new Rectangle(119, 13, 141 - 119, 37 - 13);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "22";
            d.Bound = new Rectangle(139, 27, 159 - 139, 47 - 27);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "23";
            d.Bound = new Rectangle(155, 36, 180 - 155, 57 - 36);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "24";
            d.Bound = new Rectangle(165, 56, 189 - 165, 79 - 56);
            Lstdents.Add(d);

            d = new Dent();
            d.Name = "25";
            d.Bound = new Rectangle(173, 77, 200 - 173, 102 - 77);
            Lstdents.Add(d);

            d = new Dent();
            d.Name = "26";
            d.Bound = new Rectangle(178, 99, 209 - 178, 131 - 99);
            Lstdents.Add(d);

            d = new Dent();
            d.Name = "27";
            d.Bound = new Rectangle(179, 129, 213 - 179, 160 - 129);
            Lstdents.Add(d);

            d = new Dent();
            d.Name = "28";
            d.Bound = new Rectangle(180, 159, 213 - 180, 189 - 159);
            Lstdents.Add(d);

            #endregion

            #region 31-38
            d = new Dent();
            d.Name = "31";
            d.Bound = new Rectangle(119, 357, 135 - 119, 373 - 357);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "32";
            d.Bound = new Rectangle(135, 347, 153 - 135, 366 - 347);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "33";
            d.Bound = new Rectangle(146, 336, 169 - 146, 358 - 336);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "34";
            d.Bound = new Rectangle(160, 319, 180 - 160, 340 - 319);
            Lstdents.Add(d);

            d = new Dent();
            d.Name = "35";
            d.Bound = new Rectangle(168, 298, 191 - 168, 321 - 298);
            Lstdents.Add(d);

            d = new Dent();
            d.Name = "36";
            d.Bound = new Rectangle(173, 267, 205 - 173, 300 - 267);
            Lstdents.Add(d);

            d = new Dent();
            d.Name = "37";
            d.Bound = new Rectangle(177, 238, 211 - 177, 270 - 238);
            Lstdents.Add(d);

            d = new Dent();
            d.Name = "38";
            d.Bound = new Rectangle(183, 210, 214 - 183, 238 - 210);
            Lstdents.Add(d);

            #endregion

            #region 41-48
            d = new Dent();
            d.Name = "41";
            d.Bound = new Rectangle(100, 357, 118 - 100, 373 - 357);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "42";
            d.Bound = new Rectangle(85, 347, 101 - 85, 366 - 347);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "43";
            d.Bound = new Rectangle(66, 336, 89 - 66, 358 - 336);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "44";
            d.Bound = new Rectangle(55, 319, 75 - 55, 340 - 319);
            Lstdents.Add(d);

            d = new Dent();
            d.Name = "45";
            d.Bound = new Rectangle(43, 298, 67 - 43, 321 - 298);
            Lstdents.Add(d);

            d = new Dent();
            d.Name = "46";
            d.Bound = new Rectangle(27, 267, 64 - 27, 300 - 267);
            Lstdents.Add(d);

            d = new Dent();
            d.Name = "47";
            d.Bound = new Rectangle(24, 238, 59 - 24, 270 - 238);
            Lstdents.Add(d);

            d = new Dent();
            d.Name = "48";
            d.Bound = new Rectangle(23, 210, 52 - 23, 238 - 210);
            Lstdents.Add(d);

            #endregion

            #region 51-55
            d = new Dent();
            d.Name = "51";
            d.Bound = new Rectangle(107, 118, 118 - 107, 127 - 118);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "52";
            d.Bound = new Rectangle(93, 121, 107 - 93, 133 - 121);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "53";
            d.Bound = new Rectangle(84, 131, 101 - 84, 146 - 131);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "54";
            d.Bound = new Rectangle(75, 145, 95 - 75, 161 - 145);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "55";
            d.Bound = new Rectangle(67, 161, 91 - 67, 182 - 161);
            Lstdents.Add(d);

            #endregion

            #region 61-65
            d = new Dent();
            d.Name = "61";
            d.Bound = new Rectangle(118, 116, 129 - 118, 128 - 116);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "62";
            d.Bound = new Rectangle(128, 121, 140 - 128, 133 - 121);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "63";
            d.Bound = new Rectangle(136, 131, 151 - 136, 143 - 131);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "64";
            d.Bound = new Rectangle(142, 145, 156 - 142, 161 - 145);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "65";
            d.Bound = new Rectangle(146, 161, 167 - 146, 181 - 161);
            Lstdents.Add(d);

            #endregion

            #region 71-75
            d = new Dent();
            d.Name = "71";
            d.Bound = new Rectangle(118, 265, 129 - 118, 276 - 265);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "72";
            d.Bound = new Rectangle(128, 260, 140 - 128, 272 - 260);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "73";
            d.Bound = new Rectangle(136, 251, 151 - 136, 264 - 251);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "74";
            d.Bound = new Rectangle(142, 238, 156 - 142, 252 - 238);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "75";
            d.Bound = new Rectangle(146, 224, 167 - 146, 238 - 224);
            Lstdents.Add(d);

            #endregion

            #region 81-85
            d = new Dent();
            d.Name = "81";
            d.Bound = new Rectangle(108, 266, 118 - 108, 277 - 266);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "82";
            d.Bound = new Rectangle(96, 260, 109 - 96, 272 - 260);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "83";
            d.Bound = new Rectangle(86, 251, 100 - 86, 264 - 251);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "84";
            d.Bound = new Rectangle(79, 238, 93 - 79, 252 - 238);
            Lstdents.Add(d);
            d = new Dent();
            d.Name = "85";
            d.Bound = new Rectangle(70, 224, 85 - 70, 238 - 224);
            Lstdents.Add(d);

            #endregion
        }
                

        


        

        public ChoixDents()
        {
            InitializeComponent();

            this.SetStyle(
           ControlStyles.UserPaint |
           ControlStyles.AllPaintingInWmPaint |
           ControlStyles.DoubleBuffer, true);

            InitDents();

            
        
        }

      
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Dent d = Hittest(new Point(e.X, e.Y));
            if (d != null)
            {
                d.IsSelected = !d.IsSelected;
                Invalidate();
            }
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Image img = global::BASEDiag.Ctrls.ChoixDentRsx.AdulteEnfant;

            e.Graphics.DrawImage(img,new Point(0,0));

            foreach (Dent d in Lstdents)
                d.Draw(e.Graphics);
            
            e.Graphics.DrawRectangle(Pens.Black, 0, 0,Width-1,Height-1);

            

            Font ft = new Font("Segoe UI",8,FontStyle.Regular);
            SizeF sz = e.Graphics.MeasureString(SelectedDents,ft);
            int y = (int)(Height- sz.Height)/2;

            RectangleF r = new RectangleF(2, y, Width - 5, sz.Height);

            e.Graphics.FillRectangle(Brushes.LightGray,r);
            e.Graphics.DrawString(SelectedDents, ft, Brushes.Blue, r);

            base.OnPaint(e);
        }

        private Dent Hittest(Point p)
        {
            foreach (Dent d in Lstdents)
            {
                if (d.Bound.Contains(p))
                    return d;
            }
            return null;

        }
    }

    public class Dent
    {

        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        private RectangleF _Bound;
        public RectangleF Bound
        {
            get
            {
                return _Bound;
            }
            set
            {
                _Bound = value;
            }
        }

        private bool _IsSelected;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
            }
        }

        public void Draw(Graphics g)
        {

            Pen p = new Pen(Brushes.Red, 3);
            //g.DrawRectangle(Pens.Black,Bound);
            //  g.DrawString(Name, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, Bound.Location);

            if (IsSelected)
                g.DrawEllipse(p, Bound);
        }
    }


}
