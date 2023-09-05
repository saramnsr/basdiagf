using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BASEDiag.Ctrls
{
    public static class GeomTools
    {



        /// <summary>
        /// Calcul l'angle entre 2 segments.Les 2 points de rapprochement sont ViewPt1 et ViewPt2
        /// </summary>
        /// <param name="ViewPt1"></param>
        /// <param name="Pt1"></param>
        /// <param name="ViewPt2"></param>
        /// <param name="Pt2"></param>
        /// <returns></returns>
        public static float AngleOfView(PointF ViewPt1, PointF Pt1, PointF ViewPt2, PointF Pt2)
        {
            PointF newPt2 = new PointF(Pt2.X - (ViewPt2.X - ViewPt1.X), Pt2.Y - (ViewPt2.Y - ViewPt1.Y));
            return AngleOfView(ViewPt1.X, ViewPt1.Y, Pt1.X, Pt1.Y, newPt2.X, newPt2.Y);
        }
        
        /// <summary>
        /// Calcul l'angle entre le segment Pt1,ViewPt et l'axe vertical a ViewPt
        /// </summary>
        /// <param name="ViewPt"></param>
        /// <param name="Pt1"></param>
        /// <returns></returns>
        public static float AngleOfView(PointF ViewPt, PointF Pt1)
        {
            return AngleOfView(ViewPt.X, ViewPt.Y, Pt1.X, Pt1.Y, ViewPt.X, ViewPt.Y + 1000);
        }

        /// <summary>
        /// Calcul l'angle entre les 3 points (ViewPt etant le point d'angle)
        /// </summary>
        /// <param name="ViewPt"></param>
        /// <param name="Pt1"></param>
        /// <param name="Pt2"></param>
        /// <returns></returns>
        public static float AngleOfView(PointF ViewPt, PointF Pt1, PointF Pt2)
        {
            return AngleOfView(ViewPt.X, ViewPt.Y, Pt1.X, Pt1.Y, Pt2.X, Pt2.Y);
        }

        /// <summary>
        /// Calcul l'angle entre les 3 points (Pt0 etant le point d'angle)
        /// </summary>
        /// <param name="Pt0_X"></param>
        /// <param name="Pt0_Y"></param>
        /// <param name="Pt1_X"></param>
        /// <param name="Pt1_Y"></param>
        /// <param name="Pt2_X"></param>
        /// <param name="Pt2_Y"></param>
        /// <returns></returns>
        public static float AngleOfView(double Pt0_X, double Pt0_Y,
               double Pt1_X, double Pt1_Y,
                   double Pt2_X, double Pt2_Y)
        {
            double dx1, dx2, dy1, dy2;

            dx1 = Pt1_X - Pt0_X;
            dy1 = Pt1_Y - Pt0_Y;
            dx2 = Pt2_X - Pt0_X;
            dy2 = Pt2_Y - Pt0_Y;
            if (Math.Abs(dx1) < 0.00000001)
                dx1 = 0.0;
            if (Math.Abs(dx2) < 0.00000001)
                dx2 = 0.0;
            if (Math.Abs(dy1) < 0.00000001)
                dy1 = 0.0;
            if (Math.Abs(dy2) < 0.00000001)
                dy2 = 0.0;




            double a1, b1, a2, b2, a, b, t, cosinus;

            a1 = Pt1_X - Pt0_X;
            a2 = Pt1_Y - Pt0_Y;

            b1 = Pt2_X - Pt0_X;
            b2 = Pt2_Y - Pt0_Y;

            a = Math.Sqrt((a1 * a1) + (a2 * a2));
            b = Math.Sqrt((b1 * b1) + (b2 * b2));

            if ((a == 0.0) || (b == 0.0))
                return (0.0f);

            cosinus = (a1 * b1 + a2 * b2) / (a * b);

            t = Math.Acos(cosinus);

            t = t * 180.0 / Math.PI;

            if ((dx1 * dy2) > (dy1 * dx2))
                return (float)(360 - t);

            if ((dx1 * dy2) < (dy1 * dx2))
                return (float)(t);

            if (((dx1 * dx2) < 0.0) || ((dy1 * dy2) < 0.0))
                return (float)(t);

            if ((dx1 * dx1 + dy1 * dy1) < (dx2 * dx2 + dy2 * dy2))
                return (float)(360 - t);

            return (0);
        }



        public static float Distance(float Xa, float Ya, float Xb, float Yb)
        {
            return Distance(new PointF(Xa, Ya), new PointF(Xb, Yb));
        }

        public static float Distance(PointF A, PointF B)
        {
            return (float)Math.Sqrt(Math.Pow((A.X - B.X), 2) + Math.Pow((A.Y - B.Y), 2));
        }

        public static Droite FindDroiteOf(PointF Pt1, PointF Pt2)
        {
            Droite d = new Droite();
                
            if (Pt1.Y - Pt2.Y == 0)
            {
                d.IsHorizontal = true;
                d.yIfHorizontal = Pt1.Y;
            }
            else
                if (Pt1.X - Pt2.X == 0)
                {
                    d.IsVertical = true;
                    d.xIfVertical = Pt1.X;
                }
                else
                {
                    d.a = (Pt1.Y - Pt2.Y) / (float)(Pt1.X - Pt2.X);
                    d.b = (float)Pt1.Y - (d.a * Pt1.X);
                }
            return d;

        }
    }

    public class Droite
    {
        public float a;
        public float b;

        public float xIfVertical;
        public float yIfHorizontal;


        private bool _IsHorizontal = false;
        public bool IsHorizontal
        {
            get
            {
                return _IsHorizontal;
            }
            set
            {
                _IsHorizontal = value;
            }
        }

        private bool _isVertical = false;
        public bool IsVertical
        {
            get
            {
                return _isVertical;
            }
            set
            {
                _isVertical = value;
            }
        }

        public float GetY(float x)
        {
            if (IsHorizontal) return yIfHorizontal;
            if (IsVertical) return float.MaxValue;

            return (a * x) + b;
        }

        public float GetX(float y)
        {
            if (IsHorizontal) return float.MaxValue;
            if (IsVertical) return xIfVertical;

            return ((y - b) / a);
        }

        public PointF GetIntersectionWith(Droite d)
        {
            float x = 0;
            float y = 0;

            if (IsVertical)
            {
                y = d.GetY(xIfVertical);
                return new PointF(xIfVertical, y);
            }

            if (IsHorizontal)
            {
                x = d.GetX(yIfHorizontal);
                return new PointF(x, yIfHorizontal);
            }

            if (d.IsVertical)
            {
                y = GetY(d.xIfVertical);
                return new PointF(d.xIfVertical, y);
            }

            if (d.IsHorizontal)
            {
                x = GetX(d.yIfHorizontal);
                return new PointF(x, d.yIfHorizontal);
            }

            x = (d.b - this.b) / (this.a - d.a);
            y = (a * x) + b;

            return new PointF(x, y);
        }


    }
}
