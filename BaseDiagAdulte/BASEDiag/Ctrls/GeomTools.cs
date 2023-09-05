using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BASEDiagAdulte.Ctrls
{
    public static class GeomTools
    {

        private static bool pnpoly(int nvert, float[] vertx, float[] verty, float testx, float testy)
        {
            int i, j = 0;
            bool c = false;
            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((verty[i] > testy) != (verty[j] > testy)) &&
                 (testx < (vertx[j] - vertx[i]) * (testy - verty[i]) / (verty[j] - verty[i]) + vertx[i]))
                    c = !c;
            }
            return c;
        }


        /// <summary>
        /// Lepoint est il dans le polygon ?
        /// </summary>
        /// <param name="lstpt"></param>
        /// <param name="testPt"></param>
        /// <returns></returns>
        public static bool pnpoly(List<PointF> lstpt, PointF testPt)
        {
            int nvert = lstpt.Count;
            float[] vertx = new float[nvert];
            float[] verty = new float[nvert];

            int i = 0;
            foreach (PointF p in lstpt)
            {
                vertx[i] = (float)p.X;
                verty[i] = (float)p.Y;
                i++;
            }

            return pnpoly(nvert, vertx, verty, (float)testPt.X, (float)testPt.Y);
        }

        public static double GetSurfaceOf(GraphicsPath pth)
        {
            PointF[] pts = pth.PathPoints;

            return GetSurfaceOf(pts);

        }

        public static double GetSurfaceOf(List<PointF> lstpt)
        {
            return GetSurfaceOf(lstpt.ToArray());
        }

        public static double GetSurfaceOf(PointF[] lstpt)
        {
            double area = 0;
            int N = lstpt.Length;
            //We will triangulate the polygon
            //into triangles with points p[0],p[i],p[i+1]

            for (int i = 1; i + 1 < N; i++)
            {
                double x1 = lstpt[i].X - lstpt[0].X;
                double y1 = lstpt[i].Y - lstpt[0].Y;
                double x2 = lstpt[i + 1].X - lstpt[0].X;
                double y2 = lstpt[i + 1].Y - lstpt[0].Y;
                double cross = x1 * y2 - x2 * y1;
                area += cross;
            }
            return Math.Abs(area / 2.0);



        }

        /*
        public static double GetSurfaceOf(List<PointF> lstpt)
        {
            double result = 0;
            int Hi = lstpt.Count - 1;

            for (int i = 0; i < lstpt.Count - 2; i++)
                result += ((lstpt[i].X * lstpt[i + 1].Y) - (lstpt[i].Y * lstpt[i + 1].X));


            result += ((lstpt[Hi].X * lstpt[0].Y) - (lstpt[Hi].Y * lstpt[0].X));

            result = Math.Abs(result / 2);

            return result;


        }
            */
        public static PointF FindMiddlePoint(PointF[] lstpoints)
        {
            return FindMiddlePoint(lstpoints.ToList());
        }

        public static PointF FindMiddlePoint(List<PointF> lstpoints)
        {
            double x = 0;
            double y = 0;

            foreach (PointF pt in lstpoints)
                x += pt.X;
            foreach (PointF pt in lstpoints)
                y += pt.Y;

            return new System.Drawing.PointF((float)x / lstpoints.Count, (float)y / lstpoints.Count);
        }


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
        /// Calcul l'angle entre les 3 points (Pt0 etant le point d'angle) (l'angle tjrs<180°)
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

        public static PointF FindPointAfterRotation(PointF Center, float distance, double alpha)
        {
            PointF PtOrigin = new PointF(Center.X + distance, Center.Y);
            return FindPointAfterRotation(Center, PtOrigin, alpha);
        }

        public static PointF FindPointAfterRotation(PointF Center, PointF PtOrigin, double alpha)
        {
            //vecteur CB est l'image de CA par cette rotation alpha
            //http://fr.wikipedia.org/wiki/Rotation_vectorielle

            PointF C = Center;
            PointF A = PtOrigin;

            A.X -= C.X;
            A.Y -= C.Y;

            double xB = (Math.Cos(alpha) * A.X) - (Math.Sin(alpha) * A.Y);
            double yB = (Math.Sin(alpha) * A.X) + (Math.Cos(alpha) * A.Y);

            xB += C.X;
            yB += C.Y;

            return new PointF((float)xB, (float)yB);

        }


        /// <summary>
        /// Bezier Spline methods
        /// </summary>
        /// <remarks>
        /// Modified: Peter Lee (peterlee.com.cn < at > gmail.com)
        ///   Update: 2009-03-16
        /// 
        /// see also:
        /// Draw a smooth curve through a set of 2D points with Bezier primitives
        /// http://www.codeproject.com/KB/graphics/BezierSpline.aspx
        /// By Oleg V. Polikarpotchkin
        /// 
        /// Algorithm Descripition:
        /// 
        /// To make a sequence of individual Bezier curves to be a spline, we
        /// should calculate Bezier control points so that the spline curve
        /// has two continuous derivatives at knot points.
        /// 
        /// Note: `[]` denotes subscript
        ///        `^` denotes supscript
        ///        `'` denotes first derivative
        ///       `''` denotes second derivative
        ///       
        /// A Bezier curve on a single interval can be expressed as:
        /// 
        /// B(t) = (1-t)^3 P0 + 3(1-t)^2 t P1 + 3(1-t)t^2 P2 + t^3 P3          (*)
        /// 
        /// where t is in [0,1], and
        ///     1. P0 - first knot point
        ///     2. P1 - first control point (close to P0)
        ///     3. P2 - second control point (close to P3)
        ///     4. P3 - second knot point
        ///     
        /// The first derivative of (*) is:
        /// 
        /// B'(t) = -3(1-t)^2 P0 + 3(3t^2–4t+1) P1 + 3(2–3t)t P2 + 3t^2 P3
        /// 
        /// The second derivative of (*) is:
        /// 
        /// B''(t) = 6(1-t) P0 + 6(3t-2) P1 + 6(1–3t) P2 + 6t P3
        /// 
        /// Considering a set of piecewise Bezier curves with n+1 points
        /// (Q[0..n]) and n subintervals, the (i-1)-th curve should connect
        /// to the i-th one:
        /// 
        /// Q[0] = P0[1],
        /// Q[1] = P0[2] = P3[1], ... , Q[i-1] = P0[i] = P3[i-1]  (i = 1..n)   (@)
        /// 
        /// At the i-th subinterval, the Bezier curve is:
        /// 
        /// B[i](t) = (1-t)^3 P0[i] + 3(1-t)^2 t P1[i] + 
        ///           3(1-t)t^2 P2[i] + t^3 P3[i]                 (i = 1..n)
        /// 
        /// applying (@):
        /// 
        /// B[i](t) = (1-t)^3 Q[i-1] + 3(1-t)^2 t P1[i] + 
        ///           3(1-t)t^2 P2[i] + t^3 Q[i]                  (i = 1..n)   (i)
        ///           
        /// From (i), the first derivative at the i-th subinterval is:
        /// 
        /// B'[i](t) = -3(1-t)^2 Q[i-1] + 3(3t^2–4t+1) P1[i] +
        ///            3(2–3t)t P2[i] + 3t^2 Q[i]                 (i = 1..n)
        /// 
        /// Using the first derivative continuity condition:
        /// 
        /// B'[i-1](1) = B'[i](0)
        /// 
        /// we get:
        /// 
        /// P1[i] + P2[i-1] = 2Q[i-1]                             (i = 2..n)   (1)
        /// 
        /// From (i), the second derivative at the i-th subinterval is:
        /// 
        /// B''[i](t) = 6(1-t) Q[i-1] + 6(3t-2) P1[i] +
        ///             6(1-3t) P2[i] + 6t Q[i]                   (i = 1..n)
        /// 
        /// Using the second derivative continuity condition:
        /// 
        /// B''[i-1](1) = B''[i](0)
        /// 
        /// we get:
        /// 
        /// P1[i-1] + 2P1[i] = P2[i] + 2P2[i-1]                   (i = 2..n)   (2)
        /// 
        /// Then, using the so-called "natural conditions":
        /// 
        /// B''[1](0) = 0
        /// 
        /// B''[n](1) = 0
        /// 
        /// to the second derivative equations, and we get:
        /// 
        /// 2P1[1] - P2[1] = Q[0]                                              (3)
        /// 
        /// 2P2[n] - P1[n] = Q[n]                                              (4)
        /// 
        /// From (1)(2)(3)(4), we have 2n conditions for n first control points
        /// P1[1..n], and n second control points P2[1..n].
        /// 
        /// Eliminating P2[1..n], we get (be patient to get :-) a set of n
        /// equations for solving P1[1..n]:
        /// 
        ///   2P1[1]   +  P1[2]   +            = Q[0] + 2Q[1]
        ///    P1[1]   + 4P1[2]   +    P1[3]   = 4Q[1] + 2Q[2]
        ///  ...
        ///    P1[i-1] + 4P1[i]   +    P1[i+1] = 4Q[i-1] + 2Q[i]
        ///  ...
        ///    P1[n-2] + 4P1[n-1] +    P1[n]   = 4Q[n-2] + 2Q[n-1]
        ///               P1[n-1] + 3.5P1[n]   = (8Q[n-1] + Q[n]) / 2
        ///  
        /// From this set of equations, P1[1..n] are easy but tedious to solve.
        /// </remarks>
        /// 



        /// <summary>
        /// Get open-ended Bezier Spline Control Points.
        /// </summary>
        /// <param name="knots">Input Knot Bezier spline points.</param>
        /// <param name="firstControlPoints">Output First Control points array of knots.Length - 1 length.</param>
        /// <param name="secondControlPoints">Output Second Control points array of knots.Length - 1 length.</param>
        /// <exception cref="ArgumentNullException"><paramref name="knots"/> parameter must be not null.</exception>
        /// <exception cref="ArgumentException"><paramref name="knots"/> array must containg at least two points.</exception>
        public static void GetCurveControlPoints(PointF[] knots, out PointF[] firstControlPoints, out PointF[] secondControlPoints)
        {
            if (knots == null)
                throw new ArgumentNullException("knots");
            int n = knots.Length - 1;
            if (n < 1)
            {
                firstControlPoints = new PointF[0];
                secondControlPoints = new PointF[0];
                return;
            }
            if (n == 1)
            { // Special case: Bezier curve should be a straight line.
                firstControlPoints = new PointF[1];
                // 3P1 = 2P0 + P3
                firstControlPoints[0].X = (2 * knots[0].X + knots[1].X) / 3;
                firstControlPoints[0].Y = (2 * knots[0].Y + knots[1].Y) / 3;

                secondControlPoints = new PointF[1];
                // P2 = 2P1 – P0
                secondControlPoints[0].X = 2 * firstControlPoints[0].X - knots[0].X;
                secondControlPoints[0].Y = 2 * firstControlPoints[0].Y - knots[0].Y;
                return;
            }

            // Calculate first Bezier control points
            // Right hand side vector
            double[] rhs = new double[n];

            // Set right hand side X values
            for (int i = 1; i < n - 1; ++i)
                rhs[i] = 4 * knots[i].X + 2 * knots[i + 1].X;
            rhs[0] = knots[0].X + 2 * knots[1].X;
            rhs[n - 1] = (8 * knots[n - 1].X + knots[n].X) / 2.0;
            // Get first control points X-values
            double[] x = GetFirstControlPoints(rhs);

            // Set right hand side Y values
            for (int i = 1; i < n - 1; ++i)
                rhs[i] = 4 * knots[i].Y + 2 * knots[i + 1].Y;
            rhs[0] = knots[0].Y + 2 * knots[1].Y;
            rhs[n - 1] = (8 * knots[n - 1].Y + knots[n].Y) / 2.0;
            // Get first control points Y-values
            double[] y = GetFirstControlPoints(rhs);

            // Fill output arrays.
            firstControlPoints = new PointF[n];
            secondControlPoints = new PointF[n];
            for (int i = 0; i < n; ++i)
            {
                // First control point
                firstControlPoints[i] = new PointF((float)x[i], (float)y[i]);
                // Second control point
                if (i < n - 1)
                    secondControlPoints[i] = new PointF((float)(2 * knots[i + 1].X - x[i + 1]), (float)(2 * knots[i + 1].Y - y[i + 1]));
                else
                    secondControlPoints[i] = new PointF((float)(knots[n].X + x[n - 1]) / 2, (float)(knots[n].Y + y[n - 1]) / 2);
            }
        }



        /// <summary>
        /// Bezier Spline methods
        /// </summary>
        /// <remarks>
        /// Modified: Peter Lee (peterlee.com.cn < at > gmail.com)
        ///   Update: 2009-03-16
        /// 
        /// see also:
        /// Draw a smooth curve through a set of 2D points with Bezier primitives
        /// http://www.codeproject.com/KB/graphics/BezierSpline.aspx
        /// By Oleg V. Polikarpotchkin
        /// 
        /// Algorithm Descripition:
        /// 
        /// To make a sequence of individual Bezier curves to be a spline, we
        /// should calculate Bezier control points so that the spline curve
        /// has two continuous derivatives at knot points.
        /// 
        /// Note: `[]` denotes subscript
        ///        `^` denotes supscript
        ///        `'` denotes first derivative
        ///       `''` denotes second derivative
        ///       
        /// A Bezier curve on a single interval can be expressed as:
        /// 
        /// B(t) = (1-t)^3 P0 + 3(1-t)^2 t P1 + 3(1-t)t^2 P2 + t^3 P3          (*)
        /// 
        /// where t is in [0,1], and
        ///     1. P0 - first knot point
        ///     2. P1 - first control point (close to P0)
        ///     3. P2 - second control point (close to P3)
        ///     4. P3 - second knot point
        ///     
        /// The first derivative of (*) is:
        /// 
        /// B'(t) = -3(1-t)^2 P0 + 3(3t^2–4t+1) P1 + 3(2–3t)t P2 + 3t^2 P3
        /// 
        /// The second derivative of (*) is:
        /// 
        /// B''(t) = 6(1-t) P0 + 6(3t-2) P1 + 6(1–3t) P2 + 6t P3
        /// 
        /// Considering a set of piecewise Bezier curves with n+1 points
        /// (Q[0..n]) and n subintervals, the (i-1)-th curve should connect
        /// to the i-th one:
        /// 
        /// Q[0] = P0[1],
        /// Q[1] = P0[2] = P3[1], ... , Q[i-1] = P0[i] = P3[i-1]  (i = 1..n)   (@)
        /// 
        /// At the i-th subinterval, the Bezier curve is:
        /// 
        /// B[i](t) = (1-t)^3 P0[i] + 3(1-t)^2 t P1[i] + 
        ///           3(1-t)t^2 P2[i] + t^3 P3[i]                 (i = 1..n)
        /// 
        /// applying (@):
        /// 
        /// B[i](t) = (1-t)^3 Q[i-1] + 3(1-t)^2 t P1[i] + 
        ///           3(1-t)t^2 P2[i] + t^3 Q[i]                  (i = 1..n)   (i)
        ///           
        /// From (i), the first derivative at the i-th subinterval is:
        /// 
        /// B'[i](t) = -3(1-t)^2 Q[i-1] + 3(3t^2–4t+1) P1[i] +
        ///            3(2–3t)t P2[i] + 3t^2 Q[i]                 (i = 1..n)
        /// 
        /// Using the first derivative continuity condition:
        /// 
        /// B'[i-1](1) = B'[i](0)
        /// 
        /// we get:
        /// 
        /// P1[i] + P2[i-1] = 2Q[i-1]                             (i = 2..n)   (1)
        /// 
        /// From (i), the second derivative at the i-th subinterval is:
        /// 
        /// B''[i](t) = 6(1-t) Q[i-1] + 6(3t-2) P1[i] +
        ///             6(1-3t) P2[i] + 6t Q[i]                   (i = 1..n)
        /// 
        /// Using the second derivative continuity condition:
        /// 
        /// B''[i-1](1) = B''[i](0)
        /// 
        /// we get:
        /// 
        /// P1[i-1] + 2P1[i] = P2[i] + 2P2[i-1]                   (i = 2..n)   (2)
        /// 
        /// Then, using the so-called "natural conditions":
        /// 
        /// B''[1](0) = 0
        /// 
        /// B''[n](1) = 0
        /// 
        /// to the second derivative equations, and we get:
        /// 
        /// 2P1[1] - P2[1] = Q[0]                                              (3)
        /// 
        /// 2P2[n] - P1[n] = Q[n]                                              (4)
        /// 
        /// From (1)(2)(3)(4), we have 2n conditions for n first control points
        /// P1[1..n], and n second control points P2[1..n].
        /// 
        /// Eliminating P2[1..n], we get (be patient to get :-) a set of n
        /// equations for solving P1[1..n]:
        /// 
        ///   2P1[1]   +  P1[2]   +            = Q[0] + 2Q[1]
        ///    P1[1]   + 4P1[2]   +    P1[3]   = 4Q[1] + 2Q[2]
        ///  ...
        ///    P1[i-1] + 4P1[i]   +    P1[i+1] = 4Q[i-1] + 2Q[i]
        ///  ...
        ///    P1[n-2] + 4P1[n-1] +    P1[n]   = 4Q[n-2] + 2Q[n-1]
        ///               P1[n-1] + 3.5P1[n]   = (8Q[n-1] + Q[n]) / 2
        ///  
        /// From this set of equations, P1[1..n] are easy but tedious to solve.
        /// </remarks>
        /// 






        public static PointF GetPointInCurveAt(PointF start,
                                             PointF c1,
                                             PointF c2,
                                             PointF End,
                                             double t)
        {

            // The Bezier curve is
            // (x(t),y(t)) = (1-t)^3*P0 + 3*(1-t)^2*t*P1 + 3*(1-t)*t^2*P2 + t^3*P3
            // for 0 <= t <= 1.

            //http://www.groupsrv.com/computers/about179364.html

            /*
            y(t) = a t^3 + b t^2 + c t + d

            a = -y0 + 3 y1 - 3 y2 + y3
            b = 3 y0 - 6 y1 + 3 y2
            c = -3 y0 + 3 y1
            d = y0 
                         * */


            double a = -start.Y + 3 * c1.Y - 3 * c2.Y + End.Y;
            double b = 3 * start.Y - 6 * c1.Y + 3 * c2.Y;
            double c = -3 * start.Y + 3 * c1.Y;
            double d = start.Y;

            double y = a * Math.Pow(t, 3) + b * Math.Pow(t, 2) + c * t + d;

            a = -start.X + 3 * c1.X - 3 * c2.X + End.X;
            b = 3 * start.X - 6 * c1.X + 3 * c2.X;
            c = -3 * start.X + 3 * c1.X;
            d = start.X;

            double x = a * Math.Pow(t, 3) + b * Math.Pow(t, 2) + c * t + d;


            return new PointF((float)x, (float)y);


        }










        public static PointF GetNearestPointInCurveFrom(PointF start,
                                             PointF c1,
                                             PointF c2,
                                             PointF End,
                                             PointF pttotest,
                                             ref float minDistance)
        {

            PointF resultat = new PointF(0, 0);
            minDistance = float.MaxValue;
            for (int t = 0; t < 1000; t++)
            {
                PointF tmppt = GetPointInCurveAt(start, c1, c2, End, t / 1000f);
                float res = GeomTools.Distance(tmppt, pttotest);
                if (minDistance > res)
                {
                    minDistance = res;
                    resultat = tmppt;
                }
            }
            return resultat;

        }
        
        public static PointF GetNearestPointWithCurve(PointF start,
                                             PointF c1,
                                             PointF c2,
                                             PointF End,
                                             Droite d)
        {

            PointF minpt = new PointF(0, 0);
            
            double mindist = double.MaxValue;

            for (int t = 0; t < 1000; t++)
            {
                PointF ptOnCurve = GetPointInCurveAt(start, c1, c2, End, t / 1000f);
                PointF ptonDroite = d.FindProjectionOrthogonalOf(ptOnCurve);
                double dist = Distance(ptOnCurve, ptonDroite);

                if (dist < mindist)
                {
                    mindist = dist;
                    minpt = new PointF(ptOnCurve.X, ptOnCurve.Y);
                }
            }

            return minpt;

        }
        
        public static List<PointF> GetIntersectionWithCurve(PointF start,
                                             PointF c1,
                                             PointF c2,
                                             PointF End,
                                             Droite d)
        {

            List<PointF> lst = new List<PointF>();

            for (int t = 0; t < 1000; t++)
            {
                PointF ptOnCurve = GetPointInCurveAt(start, c1, c2, End, t / 1000f);
                PointF ptonDroite = d.FindProjectionOrthogonalOf(ptOnCurve);
                double dist = Distance(ptOnCurve, ptonDroite);
                if (dist < 1)
                {
                    lst.Add(ptOnCurve);
                }
            }
            return lst;

        }


    

     



        /// <summary>
        /// Solves a tridiagonal system for one of coordinates (x or y) of first Bezier control points.
        /// </summary>
        /// <param name="rhs">Right hand side vector.</param>
        /// <returns>Solution vector.</returns>
        private static double[] GetFirstControlPoints(double[] rhs)
        {
            int n = rhs.Length;
            double[] x = new double[n]; // Solution vector.
            double[] tmp = new double[n]; // Temp workspace.

            double b = 2.0;
            x[0] = rhs[0] / b;
            for (int i = 1; i < n; i++) // Decomposition and forward substitution.
            {
                tmp[i] = 1 / b;
                b = (i < n - 1 ? 4.0 : 3.5) - tmp[i];
                x[i] = (rhs[i] - x[i - 1]) / b;
            }
            for (int i = 1; i < n; i++)
                x[n - i - 1] -= tmp[n - i] * x[n - i]; // Backsubstitution.

            return x;
        }



        /// <summary>
        /// Get Closed Bezier Spline Control Points.
        /// </summary>
        /// <param name="knots">Input Knot Bezier spline points.</param>
        /// <param name="firstControlPoints">
        /// Output First Control points array of the same 
        /// length as the <paramref name="knots"> array.</param>
        /// <param name="secondControlPoints">
        /// Output Second Control points array of the same
        /// length as the <paramref name="knots"> array.</param>
        public static void GetClosedCurveControlPoints(PointF[] knots,
            out PointF[] firstControlPoints, out PointF[] secondControlPoints)
        {
            int n = knots.Length;
            if (n <= 2)
            { // There should be at least 3 knots to draw closed curve.
                firstControlPoints = new PointF[0];
                secondControlPoints = new PointF[0];
                return;
            }

            // Calculate first Bezier control points

            // The matrix.
            double[] a = new double[n], b = new double[n], c = new double[n];
            for (int i = 0; i < n; ++i)
            {
                a[i] = 1;
                b[i] = 4;
                c[i] = 1;
            }

            // Right hand side vector for points X coordinates.
            double[] rhs = new double[n];
            for (int i = 0; i < n; ++i)
            {
                int j = (i == n - 1) ? 0 : i + 1;
                rhs[i] = 4 * knots[i].X + 2 * knots[j].X;
            }
            // Solve the system for X.
            double[] x = Cyclic.Solve(a, b, c, 1, 1, rhs);

            // Right hand side vector for points Y coordinates.
            for (int i = 0; i < n; ++i)
            {
                int j = (i == n - 1) ? 0 : i + 1;
                rhs[i] = 4 * knots[i].Y + 2 * knots[j].Y;
            }
            // Solve the system for Y.
            double[] y = Cyclic.Solve(a, b, c, 1, 1, rhs);

            // Fill output arrays.
            firstControlPoints = new PointF[n];
            secondControlPoints = new PointF[n];
            for (int i = 0; i < n; ++i)
            {
                // First control point.
                firstControlPoints[i] = new PointF((float)x[i], (float)y[i]);
                // Second control point.
                secondControlPoints[i] = new PointF
                    ((float)(2 * knots[i].X - x[i]), (float)(2 * knots[i].Y - y[i]));
            }
        }






        /// <summary>
        /// Solves the cyclic set of linear equations.
        /// </summary>
        /// <remarks>
        /// The cyclic set of equations have the form
        /// ---------------------------
        /// b0 c0  0 · · · · · · ß
        ///	a1 b1 c1 · · · · · · ·
        ///  · · · · · · · · · · · 
        ///  · · · a[n-2] b[n-2] c[n-2]
        /// a  · · · · 0  a[n-1] b[n-1]
        /// ---------------------------
        /// This is a tridiagonal system, except for the matrix elements 
        /// a and ß in the corners.
        /// </remarks>
        public static class Cyclic
        {
            /// <summary>
            /// Solves the cyclic set of linear equations. 
            /// </summary>
            /// <remarks>
            /// All vectors have size of n although some elements are not used.
            /// The input is not modified.
            /// </remarks>
            /// <param name="a">Lower diagonal vector of size n; a[0] not used.</param>
            /// <param name="b">Main diagonal vector of size n.</param>
            /// <param name="c">Upper diagonal vector of size n; c[n-1] not used.</param>
            /// <param name="alpha">Bottom-left corner value.</param>
            /// <param name="beta">Top-right corner value.</param>
            /// <param name="rhs">Right hand side vector.</param>
            /// <returns>The solution vector of size n.</returns>
            public static double[] Solve(double[] a, double[] b,
            double[] c, double alpha, double beta, double[] rhs)
            {
                // a, b, c and rhs vectors must have the same size.
                if (a.Length != b.Length || c.Length != b.Length ||
                                rhs.Length != b.Length)
                    throw new ArgumentException
                    ("Diagonal and rhs vectors must have the same size.");
                int n = b.Length;
                if (n <= 2)
                    throw new ArgumentException
                    ("n too small in Cyclic; must be greater than 2.");

                double gamma = -b[0]; // Avoid subtraction error in forming bb[0].
                // Set up the diagonal of the modified tridiagonal system.
                double[] bb = new Double[n];
                bb[0] = b[0] - gamma;
                bb[n - 1] = b[n - 1] - alpha * beta / gamma;
                for (int i = 1; i < n - 1; ++i)
                    bb[i] = b[i];
                // Solve A · x = rhs.
                double[] solution = TridiagonalSolve(a, bb, c, rhs);
                double[] x = new Double[n];
                for (int k = 0; k < n; ++k)
                    x[k] = solution[k];

                // Set up the vector u.
                double[] u = new Double[n];
                u[0] = gamma;
                u[n - 1] = alpha;
                for (int i = 1; i < n - 1; ++i)
                    u[i] = 0.0;
                // Solve A · z = u.
                solution = TridiagonalSolve(a, bb, c, u);
                double[] z = new Double[n];
                for (int k = 0; k < n; ++k)
                    z[k] = solution[k];

                // Form v · x/(1 + v · z).
                double fact = (x[0] + beta * x[n - 1] / gamma)
                    / (1.0 + z[0] + beta * z[n - 1] / gamma);

                // Now get the solution vector x.
                for (int i = 0; i < n; ++i)
                    x[i] -= fact * z[i];
                return x;
            }
        }

        /// <summary>
        /// Solves a tridiagonal system.
        /// </summary>
        /// <remarks>
        /// All vectors have size of n although some elements are not used.
        /// </remarks>
        /// <param name="a">Lower diagonal vector; a[0] not used.</param>
        /// <param name="b">Main diagonal vector.</param>
        /// <param name="c">Upper diagonal vector; c[n-1] not used.</param>
        /// <param name="rhs">Right hand side vector</param>
        /// <returns>system solution vector</returns>
        public static double[] TridiagonalSolve(double[] a, double[] b, double[] c, double[] rhs)
        {
            // a, b, c and rhs vectors must have the same size.
            if (a.Length != b.Length || c.Length != b.Length ||
                            rhs.Length != b.Length)
                throw new ArgumentException
                ("Diagonal and rhs vectors must have the same size.");
            if (b[0] == 0.0)
                throw new InvalidOperationException("Singular matrix.");
            // If this happens then you should rewrite your equations as a set of 
            // order N - 1, with u2 trivially eliminated.

            ulong n = Convert.ToUInt64(rhs.Length);
            double[] u = new Double[n];
            double[] gam = new Double[n]; 	// One vector of workspace, 
            // gam is needed.

            double bet = b[0];
            u[0] = rhs[0] / bet;
            for (ulong j = 1; j < n; j++) // Decomposition and forward substitution.
            {
                gam[j] = c[j - 1] / bet;
                bet = b[j] - a[j] * gam[j];
                if (bet == 0.0)
                    // Algorithm fails.
                    throw new InvalidOperationException
                                ("Singular matrix.");
                u[j] = (rhs[j] - a[j] * u[j - 1]) / bet;
            }
            for (ulong j = 1; j < n; j++)
                u[n - j - 1] -= gam[n - j] * u[n - j]; // Backsubstitution.

            return u;
        }

    }




    [Serializable]
    public class Droite
    {
        public float a;
        public float b;

        public float xIfVertical;
        public float yIfHorizontal;


        public double angle
        {
            get
            {
                if (IsVertical) return 90;
                if (IsHorizontal) return 0;
                return (Math.Atan(a)) * (180 / Math.PI);
            }

        }

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

        public Droite FindPerpendiculaireOn(PointF pt, double angleVoulu)
        {
            while (angleVoulu > 180) angleVoulu = angleVoulu - 180;

            bool reverse = false;
            if (angleVoulu > 90)
            {
                angleVoulu -= 90;
                reverse = true;
            }



            angleVoulu = Math.PI * (angleVoulu) / 180;

            double angleDroiteCourante = Math.Atan(a);


            double AngleDeuxiemeDroite = angleDroiteCourante + angleVoulu;

            float aprim = (float)Math.Tan(AngleDeuxiemeDroite);

            if (reverse) aprim = -1 / aprim;


            // float aprim = -(1 / a);
            // y-pt.y = aprim(x-pt.x)
            // y = (aprim*x)-(pt.x*aprim)+pt.y;
            // y = ax + b
            // bprim = -(pt.x*aprim)+pt.y;

            float bprim = -(pt.X * aprim) + pt.Y;

            Droite dperpend = new Droite();
            dperpend.a = aprim;
            dperpend.b = bprim;


            if (float.IsInfinity(aprim))
            {
                if (this.IsHorizontal)
                {
                    dperpend.IsVertical = true;
                    dperpend.xIfVertical = pt.X;
                }
                else
                {
                    dperpend.IsHorizontal = true;
                    dperpend.yIfHorizontal = pt.Y;
                }
            }

            return dperpend;
        }

        public Droite FindPerpendiculaireOn(PointF pt)
        {

            float aprim = -(1 / a);
            // y-pt.y = aprim(x-pt.x)
            // y = (aprim*x)-(pt.x*aprim)+pt.y;
            // y = ax + b
            // bprim = -(pt.x*aprim)+pt.y;

            float bprim = -(pt.X * aprim) + pt.Y;

            Droite dperpend = new Droite();
            dperpend.a = aprim;
            dperpend.b = bprim;


            if (float.IsInfinity(aprim))
            {
                if (this.IsHorizontal)
                {
                    dperpend.IsVertical = true;
                    dperpend.xIfVertical = pt.X;
                }
                else
                {
                    dperpend.IsHorizontal = true;
                    dperpend.yIfHorizontal = pt.Y;
                }
            }

            return dperpend;
        }



        public Droite FindRotateOn(PointF pt, float angle)
        {
            PointF pt1 = pt;
            PointF pt2 = new PointF(pt.X + 100, GetY(pt.X + 100));

            double alpha = Math.PI * (angle) / 180;
            PointF pt3 = GeomTools.FindPointAfterRotation(pt1, pt2, alpha);

            return GeomTools.FindDroiteOf(pt1, pt3);
        }


        public Droite FindParalleleOn(PointF pt)
        {
            //Trouver la parallele

            float aprim = a;

            //y=a*x + b;
            //y=aprim*x+bprim
            //a=aprim
            //x=pt.x
            //y=pt.y
            //bprim = pt.y - (aprim*pt.x)
            //bprim = pt.y - (a*pt.x);

            float bprim = pt.Y - (a * pt.X);

            Droite dparallele = new Droite();
            dparallele.a = aprim;
            dparallele.b = bprim;

            if (aprim == 0)
            {
                if (this.IsVertical)
                {
                    dparallele.IsVertical = true;
                    dparallele.xIfVertical = pt.X;
                }
                else
                {
                    dparallele.IsHorizontal = true;
                    dparallele.yIfHorizontal = pt.Y;
                }
            }

            return dparallele;
        }

        public PointF FindProjectionOrthogonalOf(PointF pt)
        {
            Droite dp = FindPerpendiculaireOn(pt);
            PointF ptres = GetIntersectionWith(dp);
            return ptres;
        }


        public PointF FindSymetrieOf(PointF pt)
        {
            PointF PointCentral = FindProjectionOrthogonalOf(pt);
            float rayon = GeomTools.Distance(PointCentral, pt);
            Droite d = GeomTools.FindDroiteOf(PointCentral, pt);



            List<PointF> lstpt = d.GetIntersectionWith(PointCentral, rayon);
            foreach (PointF pts in lstpt)
            {
                if ((pts.X != pt.X) && (pts.Y != pt.Y))
                    return pts;
            }
            return pt;

        }

        public PointF FindProjectionOf(PointF pt, double angle)
        {
            Droite dp = FindPerpendiculaireOn(pt, angle);
            PointF ptres = GetIntersectionWith(dp);
            return ptres;
        }

        public PointF FindProjectionWithAngle(PointF pt, float angle)
        {
            Droite dp = FindParalleleOn(pt);
            dp = dp.FindRotateOn(pt, angle);

            PointF ptres = GetIntersectionWith(dp);
            return ptres;
        }


        public List<PointF> GetIntersectionWith(PointF Centre, float rayon)
        {
            /*
             * une solution est de raisonner geometriquement.
- soient D la droite supportant le segment, et C le cercle
- construire P, la perpendiculaire à D passant par le centre de C
- calculer les coordonnees de I, le point d'intersection de P avec D. Ce point est situe entre les deux intersections de la droite avec le cercle, si elles existent.
- calculer la distance du point I au centre du cercle. Si la distance est duperieure au rayon, pas d'intersection. Sinon, par Pythagore, on deduit la distance 'r' entre I et chaque point d'intersection.
- les coordonnees des points d'intersection sont obtenues en ajoutant au point I un vecteur de norme 'r' et d'angle +/- l'angle de la droite

Pour les calculs sur droite, je te conseille la representation parametrique:
x(t) = x0 + t*dx
y(t) = y0 + t*dy
Ca te permet d'avoir la 'position' d'un point sur une droite. Si pour un segment, tu prends x0 et y0 egaux aux coordonnees du premier point, et dx et dy egaux a la difference des coordonnees des extremites, les points du segment correspondent a 0 <= t <= 1.
            */

            Droite P = FindPerpendiculaireOn(Centre);
            PointF I = this.GetIntersectionWith(P);

            float distICentre = GeomTools.Distance(I, Centre);
            double r = Math.Sqrt(Math.Pow(rayon, 2) - Math.Pow(distICentre, 2));

            double teta = Math.Atan(a);

            List<PointF> lst = new List<PointF>();

            if (distICentre < rayon)
            {
                double x = (r * Math.Cos(teta)) + I.X;
                double y = (r * Math.Sin(teta)) + I.Y;
                lst.Add(new PointF((float)x, (float)y));

                teta += Math.PI;

                x = (r * Math.Cos(teta)) + I.X;
                y = (r * Math.Sin(teta)) + I.Y;
                lst.Add(new PointF((float)x, (float)y));

            }
            else
                if (distICentre == rayon)
                {
                    lst.Add(I);
                }
            return lst;
        }

        public PointF GetIntersectionWith(PointF Centre, float rayon, PointF sameSideAs)
        {


            List<PointF> lst = GetIntersectionWith(Centre, rayon);

            if (lst.Count == 0)
                return new PointF(0, 0);

            if (lst.Count == 1)
                return lst[0];

            PointF returnedPoint = new PointF(0, 0);
            float minDist = float.MaxValue;

            foreach (PointF pt in lst)
            {
                float di = GeomTools.Distance(sameSideAs, pt);
                if (minDist > di)
                {
                    minDist = di;
                    returnedPoint = pt;
                }

            }


            return returnedPoint;
        }


        //        public List<PointF> GetIntersectionWith(PointF Centre, float rayon)
        //        {

        //            List<PointF> resultat = new List<PointF>();

        //            float yc = Centre.Y;
        //            float xc = Centre.X;
        //            float R = rayon;
        //            float a = this.a;
        //            float b = this.b;


        //            //Equation d'un cercel 
        //            //(x-xc)²+(y-yc)²=R²

        //            //Equation d'une droite
        //            // y = ax+b

        //            //equation du second degré
        //            // Ax²+Bx+C = 0
        //            //Discriminent = B²-4AC
        //            //Si discriminent > 0 2 solutions
        //            //Si discriminent =0 1 solution
        //            //Si discriminent<0 pas de solution
        //            //http://fr.wikipedia.org/wiki/%C3%89quation_du_second_degr%C3%A9

        //            float A = (1 + a);
        //            float B = 2 * (a * (b - yc) - xc);
        //            float C = ((xc * xc) + ((b - yc) * (b - yc)) - (R * R));

        //            float discriminent = (B * B) - (4 * A * C);

        //            if (discriminent < 0)
        //            {
        //                return resultat;
        //            }

        //            if (discriminent == 0)
        //            {
        //                float x = -B / (2 * A);
        //                float y = GetY(x);
        //                PointF ptf = new PointF(x, y);
        //                resultat.Add(ptf);
        //            }

        //            if (discriminent > 0)
        //            {
        //                float sq = (float)Math.Sqrt((B * B) - 4 * A * C);

        //                float x1 = (-B - sq) / (2 * A);
        //                float x2 = (-B + sq) / (2 * A);
        //                float y1 = GetY(x1);
        //                float y2 = GetY(x2);

        //                PointF ptf = new PointF(x1, y1);
        //                resultat.Add(ptf);
        //                ptf = new PointF(x2, y2);
        //                resultat.Add(ptf);
        //            }
        //            return resultat;
        ////            (1+a)x² + 2(a(b-yc)-xc)x + (xc² + (b-yc)²-R²) = 0

        ////tu poses
        ////A=(1+a)
        ////B=2(a(b-yc)-xc)
        ////C=(xc² + (b-yc)²-R²)

        //        }


    }
}
