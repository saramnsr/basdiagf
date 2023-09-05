using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BO;

namespace BASEDiagAdulte.Ctrls
{


    

    public partial class AnalysePlanTraitement : ImageCtrlAgg, IAnalyse
    {

        public event EventHandler OnModeSaisieChanged;

        public class HitTestObj
        {
            public enum HittedOnHandle
            {
                First,
                Second,
                None
            }

            private HittedOnHandle _hithandle = HittedOnHandle.None;
            public HittedOnHandle hithandle
            {
                get
                {
                    return _hithandle;
                }
                set
                {
                    _hithandle = value;
                }
            }

            private PtObject _objectHitted;
            public PtObject objectHitted
            {
                get
                {
                    return _objectHitted;
                }
                set
                {
                    _objectHitted = value;
                }
            }
        }

        private PtObject _SelectedObj = null;
        public PtObject SelectedObj
        {
            get
            {
                return _SelectedObj;
            }
            set
            {
                _SelectedObj = value;
            }
        }

        string resourcesaisie = "";
        enum SaisieMode
        {
            None,
            Point1,
            Point2            
        }
        private SaisieMode _modesaisie = SaisieMode.None;
       
        Point Saisie1 = new Point();
        Point Saisie2 = new Point();


        
        private List<PtObject> _objectsToDraw = new List<PtObject>();
        public List<PtObject> objectsToDraw
        {
            get
            {
                return _objectsToDraw;
            }
            set
            {
                _objectsToDraw = value;
            }
        }

        private Dictionary<string,Bitmap> _resources = new Dictionary<string,Bitmap>();
        public Dictionary<string,Bitmap> resources
        {
            get
            {
                return _resources;
            }
            set
            {
                _resources = value;
            }
        }


        public void AddToResource(string key, Bitmap bmp)
        {
            if (!resources.ContainsKey(key)) 
                resources.Add(key,bmp);

        }


        public void RecalculateAllPos()
        {
            foreach (PtObject o in objectsToDraw)
                REcalculateObjPos(o);
            Invalidate();
        }


        public void  startPoseResource(string ResourceName)
        {
            if (!resources.ContainsKey(ResourceName)) return;
            _modesaisie = SaisieMode.Point1;
            resourcesaisie = ResourceName;
        }

        public void StopPoseResource(string ResourceName)
        {
            _modesaisie = SaisieMode.None;
            resourcesaisie = "";
        }

        public void MoveFirstPtResource(Point pt1, PtObject obj)
        {

            Point pt = TransformPtScreenToRadio(pt1);
            obj.FirstPointOnRadio = pt;

            REcalculateObjPos(obj);
            Invalidate();
            
        }

        public void MoveSecondPtResource(Point pt1, PtObject obj)
        {
            Point pt = TransformPtScreenToRadio(pt1);
            obj.SecondPointOnRadio = pt;

            REcalculateObjPos(obj);
            Invalidate();
        }



        public PtObject AddResource(Point pt1,Point pt2, string key)
        {

            if (!resources.ContainsKey(key)) return null;
            Point rpt1 = TransformPtScreenToRadio(pt1);
            Point rpt2 = TransformPtScreenToRadio(pt2);

            float dist = GeomTools.Distance(rpt1, rpt2);
            if (dist < 5) return null;

            PtObject o = new PtObject();
            o.resourcekey = key;

            Bitmap tmpbmp = new Bitmap(resources[o.resourcekey]);
            o.img = (Epsitec.Common.Drawing.Bitmap)Epsitec.Common.Drawing.Bitmap.FromNativeBitmap(tmpbmp);

            o.FirstPointOnRadio = new PointF(rpt1.X, rpt1.Y);
            o.SecondPointOnRadio = new PointF(rpt2.X, rpt2.Y);
            objectsToDraw.Add(o);

            REcalculateObjPos(o);

            return o;
        }

        private void REcalculateObjPos(PtObject o)
        {

            if (o.img == null)
            {
                Bitmap tmpbmp = new Bitmap(resources[o.resourcekey]);
                o.img = (Epsitec.Common.Drawing.Bitmap)Epsitec.Common.Drawing.Bitmap.FromNativeBitmap(tmpbmp);
            }


            PointF centre = GeomTools.FindMiddlePoint(new PointF[] { o.FirstPointOnRadio, o.SecondPointOnRadio });


            o.matrix.Reset();

            double scale = (GeomTools.Distance(o.FirstPointOnRadio, o.SecondPointOnRadio) / o.img.Height);

            //o.matrix.Translate(rpt1.X,rpt1.Y);
            float a = GeomTools.AngleOfView(o.FirstPointOnRadio, o.SecondPointOnRadio);


            o.matrix.Translate(-o.img.Width / 2, -o.img.Height / 2);

            o.matrix.Scale(scale, scale);
            o.matrix.RotateDeg(a);
            o.matrix.Translate(centre.X, centre.Y);
            
        }

        public HitTestObj HitTest(Point pt)
        {
            HitTestObj h = new HitTestObj();

            Point pOnRadio = TransformPtScreenToRadio(pt);
                    
           
            foreach (PtObject pto in objectsToDraw)
            {

                

                double pOnObjx = pt.X;
                double pOnObjy = Height - pt.Y;

                Epsitec.Common.Drawing.Transform trc = new Epsitec.Common.Drawing.Transform(RadioTransform);
                trc.MultiplyByPostfix(pto.matrix);


                trc.TransformInverse(ref pOnObjx, ref pOnObjy);
                             

                RectangleF r = new RectangleF(0, 0, (float)pto.img.Width, (float)pto.img.Height);
                if (r.Contains(new PointF((float)pOnObjx, (float)pOnObjy)))
                    h.objectHitted = pto;
                
                r = new RectangleF((float)pOnRadio.X - 5f, (float)pOnRadio.Y - 5f, 10f, 10f);

                if (r.Contains(pto.FirstPointOnRadio))
                {
                    h.hithandle = HitTestObj.HittedOnHandle.First;
                    h.objectHitted = pto;
                }
                if (r.Contains(pto.SecondPointOnRadio))
                {
                    h.hithandle = HitTestObj.HittedOnHandle.Second;
                    h.objectHitted = pto;
                }

            }

            return h;
        }

        public void AddResourceToCursor(string key)
        {

            Point pt = this.PointToClient(System.Windows.Forms.Control.MousePosition);

            pt = TransformPtScreenToRadio(pt);

            
            PtObject o = new PtObject();
            o.resourcekey = key;
            Bitmap tmpbmp = new Bitmap(resources[o.resourcekey]);


            o.FirstPointOnRadio = new PointF(pt.X, pt.Y - (tmpbmp.Height/2));
            o.SecondPointOnRadio = new PointF(pt.X, pt.Y + (tmpbmp.Height / 2));


            pt.Offset(-tmpbmp.Width / 2, -tmpbmp.Height / 2);
            o.matrix.Translate(new Epsitec.Common.Drawing.Point(pt));
            

          //  o.matrix.Translate(objectsToDraw.Count,0);

            o.img = (Epsitec.Common.Drawing.Bitmap)Epsitec.Common.Drawing.Bitmap.FromNativeBitmap(tmpbmp);
            objectsToDraw.Add(o);


            REcalculateObjPos(o);
            SelectedObj = o;
            Invalidate();
        }

        public void DrawExtraForms(Epsitec.Common.Drawing.Graphics gr, bool ForImpression)
        {
           // foreach (PtObject o in objectsToDraw)
            for (int i=objectsToDraw.Count-1;i>=0;i--)
            {
               // System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //    sw.Start();
                if (!resources.ContainsKey(objectsToDraw[i].resourcekey)) continue;

                if (!DrawObj(gr, objectsToDraw[i], ForImpression))
                {
                    objectsToDraw.Remove(objectsToDraw[i]);
                }


             //   Console.Out.WriteLine(sw.ElapsedTicks.ToString());
            //    sw.Stop();
                

            }
        }

        Point DownAt;
        Point MoveAt;
        HitTestObj HittedOn = null;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            DownAt = new Point(e.X, e.Y);
            MoveAt = new Point(e.X, e.Y);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                HittedOn = HitTest(new Point(e.X, e.Y));
                if ((_modesaisie == SaisieMode.None) && (HittedOn.objectHitted == null))
                {
                    _modesaisie = SaisieMode.Point1;
                    if (OnModeSaisieChanged != null)
                        OnModeSaisieChanged(this, new EventArgs());
                }
                if ((_modesaisie != SaisieMode.None) && (HittedOn.objectHitted != null))
                {


                    _modesaisie = SaisieMode.None;

                    SelectedObj = HittedOn.objectHitted;
                    Invalidate();
                    if (OnModeSaisieChanged != null)
                        OnModeSaisieChanged(this, new EventArgs());
                }
                

                if (_modesaisie == SaisieMode.Point1)
                {
                    Saisie1 = new Point(e.X, e.Y);
                    Saisie2 = new Point(e.X+50, e.Y+50);
                    _modesaisie = SaisieMode.Point2;
                    HittedOn = new HitTestObj();
                    HittedOn.objectHitted =  AddResource(Saisie1, Saisie2, resourcesaisie);
                    SelectedObj = HittedOn.objectHitted;
                    HittedOn.hithandle = HitTestObj.HittedOnHandle.Second;
                }
                else
                {
                    SelectedObj = HittedOn.objectHitted;
                    Invalidate();
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (SelectedObj != null)
                {
                    objectsToDraw.Remove(SelectedObj);
                    Invalidate();
                }
            }
            base.OnKeyDown(e);
        } 

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {

                if ((HittedOn!=null) && (HittedOn.objectHitted != null))
                {
                    Point pt = TransformPtScreenToRadio(new Point(e.X,e.Y));
                    if (HittedOn.hithandle == HitTestObj.HittedOnHandle.Second)
                        HittedOn.objectHitted.SecondPointOnRadio = pt;
                    else
                        if (HittedOn.hithandle == HitTestObj.HittedOnHandle.First)
                            HittedOn.objectHitted.FirstPointOnRadio = pt;
                        else
                        {
                            PointF delta = new PointF(e.X - MoveAt.X, e.Y - MoveAt.Y);
                            HittedOn.objectHitted.SecondPointOnRadio = new PointF(HittedOn.objectHitted.SecondPointOnRadio.X + delta.X, HittedOn.objectHitted.SecondPointOnRadio.Y - delta.Y);
                            HittedOn.objectHitted.FirstPointOnRadio = new PointF(HittedOn.objectHitted.FirstPointOnRadio.X + delta.X, HittedOn.objectHitted.FirstPointOnRadio.Y - delta.Y);
                        }

                    REcalculateObjPos(HittedOn.objectHitted);
                    Invalidate();
                }
            }
            MoveAt = new Point(e.X, e.Y);
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            

            if ((e.Button == System.Windows.Forms.MouseButtons.Left) && (_modesaisie == SaisieMode.Point2))
            {
                Saisie2 = new Point(e.X, e.Y);
                _modesaisie = SaisieMode.Point1;
            }

           

            HittedOn = null;

            base.OnMouseDown(e);
        }

        private void DrawHandles(Epsitec.Common.Drawing.Graphics gr, PtObject o)
        {
           
            double x = o.FirstPointOnRadio.X;
            double y = o.FirstPointOnRadio.Y;

            RadioTransform.TransformDirect(ref x, ref y);

            Epsitec.Common.Drawing.Point Ept = new Epsitec.Common.Drawing.Point(x,y);
            Epsitec.Common.Drawing.Path circle;

            circle = Epsitec.Common.Drawing.Path.CreateCircle(Ept, 2, 2);

            gr.Rasterizer.AddSurface(circle);

            gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 1, 0, 0);

            gr.RenderSolid();


             x = o.SecondPointOnRadio.X;
             y = o.SecondPointOnRadio.Y;

             RadioTransform.TransformDirect(ref x, ref y);

            Ept = new Epsitec.Common.Drawing.Point(x,y);

            circle = Epsitec.Common.Drawing.Path.CreateCircle(Ept, 2, 2);

            gr.Rasterizer.AddSurface(circle);

            gr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 1, 0, 0);

            gr.RenderSolid();

        }

        private bool DrawObj(Epsitec.Common.Drawing.Graphics gr, PtObject o, bool ForImpression)
        {

            try
            {
                Epsitec.Common.Drawing.Transform trc = new Epsitec.Common.Drawing.Transform(RadioTransform);
                trc.MultiplyByPostfix(o.matrix);


                gr.AddFilledRectangle(0, 0, Width, Height);


                gr.ImageRenderer.BitmapImage = o.img;
                gr.ImageRenderer.Transform = new Epsitec.Common.Drawing.Transform(trc);

                gr.RenderImage();

                if ((SelectedObj == o) && (!ForImpression))
                {
                    DrawHandles(gr, o);
                }
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public AnalysePlanTraitement()
        {
          
            InitializeComponent();
        }

        public string GetTextResultat()
        {
            return "";
        }

        public void Recalculate()
        {
        }

        public void ReinitRotationAuto()
        {
        }

        private void AnalysePlanTraitement_Load(object sender, EventArgs e)
        {

        }
    }


    public class PtObject
    {

        private PointF _SecondPointOnRadio;
        public PointF SecondPointOnRadio
        {
            get
            {
                return _SecondPointOnRadio;
            }
            set
            {
                _SecondPointOnRadio = value;
            }
        }

        private PointF _FirstPointOnRadio;
        public PointF FirstPointOnRadio
        {
            get
            {
                return _FirstPointOnRadio;
            }
            set
            {
                _FirstPointOnRadio = value;
            }
        }

        private Epsitec.Common.Drawing.Bitmap _img;
        public Epsitec.Common.Drawing.Bitmap img
        {
            get
            {
                return _img;
            }
            set
            {
                _img = value;
            }
        }

        private string _resourcekey;
        public string resourcekey
        {
            get
            {
                return _resourcekey;
            }
            set
            {
                _resourcekey = value;
            }
        }

        private Epsitec.Common.Drawing.Transform _matrix = new Epsitec.Common.Drawing.Transform();
        public Epsitec.Common.Drawing.Transform matrix
        {
            get
            {
                return _matrix;
            }
            set
            {
                _matrix = value;
            }
        }
    }

    

}
