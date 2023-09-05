using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using BASEDiag_BO;
using System.IO;
namespace BASEDiagAdulte.Ctrls
{

    [Serializable]
    public partial class ImageCtrl : UserControl
    {
        int CurrentPointIdx = -1;
        protected bool SaisieStarted = false;

        public event EventHandler OnImageChanged;

        private Matrix TransformMatrix = new Matrix();
        private Matrix TransformMatrixInverse = new Matrix();


        [NonSerialized]
        protected List<PointToTake> _ListOfPoints = new List<PointToTake>();
        protected List<PointToTake> ListOfPoints
        {
            get
            {
                return _ListOfPoints;
            }
            set
            {
                _ListOfPoints = value;
            }
        }


        bool _showHelp = false;

        private float _Brightness = 0f;
        public float Brightness
        {
            set
            {
                _Brightness = value;
                Invalidate();
            }
            get { return _Brightness; }
        }
        
        private float _angleDeRotation = 0f;
        public float AngleDeRotation
        {
            set
            {
                _angleDeRotation = value;
                Invalidate();
            }
            get { return _angleDeRotation; }
        }

        private float _Contraste = 1f;
        public float Contraste
        {
            set
            {
                _Contraste = value;
                Invalidate();
            }
            get { return _Contraste; }
        }

        private Point _Offset = new Point(0, 0);
        public Point Offset
        {
            set
            {
                _Offset = value;                
                Invalidate();
            }
            get { return _Offset; }
        }
        
        private Bitmap _OriginalImage;
        public Bitmap OriginalImage
        {
            get
            {
                return _OriginalImage;
            }
            set
            {
                _OriginalImage = value;
                if (OnImageChanged != null)
                    OnImageChanged(this, new EventArgs());
            }
        }

        public ImageCtrl()
        {
            InitializeComponent();

            this.SetStyle(
           ControlStyles.UserPaint |
           ControlStyles.AllPaintingInWmPaint |
           ControlStyles.DoubleBuffer, true);


        }

        private float _zoom = 1f;
        public float zoom
        {
            set
            {
                _zoom = value;
                _zoom = _zoom <= 0 ? 0.05f : _zoom;
                _zoom = _zoom >= 3 ? 3f : _zoom;
                if (OriginalImage == null) return;
                int zh = Convert.ToInt32(OriginalImage.Height * _zoom);
                int zw = Convert.ToInt32(OriginalImage.Width * _zoom);

                
               
                Invalidate();
            }
            get { return _zoom; }
        }

        private string _file;
        public string file
        {
            set
            {
                if (value == null) return;
                _file = value;
                loadImage(_file);
                Invalidate();
            }
            get { return _file; }
        }




        public void Clear()
        {
            if (OriginalImage != null) OriginalImage.Dispose();
            OriginalImage = null;
        }

          private static bool exist(string path)
        {
            try
                    {
                        var req = System.Net.WebRequest.Create(path);
                        using (Stream stream = req.GetResponse().GetResponseStream())
                        {
                          
                        }
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
            return true;

        }

        public void loadImage(string imagefile)
        {
            _file = imagefile;
            Clear();
            /*
            imagefilestream = new FileStream(imagefile, FileMode.Open);
            OriginalImage = new Bitmap(imagefilestream);
            */

            try
            {
                var req = System.Net.WebRequest.Create(_file);
                using (Stream stream = req.GetResponse().GetResponseStream())
                {
                    Bitmap image = (Bitmap)System.Drawing.Bitmap.FromStream(stream);
                    Bitmap btmp = new Bitmap(image);
                    OriginalImage = new Bitmap(btmp);
                    btmp.Dispose();
                   
                }
                Reinit();
            }
            catch (Exception e)
            {
            }
        }

        public void Reinit()
        {
            _zoom = 1;
            _Offset = new Point(0, 0);
            _Contraste = 1f;
            _Brightness = 0f;
            _angleDeRotation = 0;
            Invalidate();
        }

        public void zoomAuto()
        {
            if (OriginalImage == null) return;
            float zW = (float)this.Width / (float)OriginalImage.Width;
            float zH = (float)this.Height / (float)OriginalImage.Height;

            zoom = zW > zH ? zH : zW;
        }

        public void Center()
        {
            if (OriginalImage == null) return;
            Offset = new Point(0 - (int)((((float)OriginalImage.Width * zoom) - (float)this.Width) / 2),
                               0 - (int)((((float)OriginalImage.Height * zoom) - (float)this.Height) / 2));           


        }

        public void Rotate90()
        {
            OriginalImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            Invalidate();
        }

        public void Rotate180()
        {
            OriginalImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            Invalidate();
        }

        public void Rotate270()
        {
            OriginalImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
            Invalidate();
        }

        public void FlipH()
        {
            OriginalImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
            Invalidate();
        }

        public void FlipV()
        {
            OriginalImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
            Invalidate();
        }



        public void StartSaisie()
        {
            if (_ListOfPoints.Count > 0)
            {
                CurrentPointIdx = 0;
                SaisieStarted = true;
            }
        }


        private void ReinitMatrix()
        {

            Rectangle destrect = new Rectangle(0 - (OriginalImage.Width / 2), (0 - (OriginalImage.Height / 2)), Convert.ToInt32(OriginalImage.Width), Convert.ToInt32(OriginalImage.Height));

            TransformMatrix.Reset();
            TransformMatrix.Scale(_zoom, _zoom);
            TransformMatrix.Translate((_Offset.X / _zoom) + (destrect.Width / 2), (_Offset.Y / _zoom) + (destrect.Height / 2));
            TransformMatrix.Rotate(_angleDeRotation);
            TransformMatrixInverse = TransformMatrix.Clone();
            TransformMatrixInverse.Invert();
                
        }


        public PointToTake HitTest(Point pt)
        {
            Point pts = TransformPtScreenToBmp(pt);

                

            foreach (PointToTake ptT in _ListOfPoints)
            {
                Rectangle r = new Rectangle(ptT.Pt.X-5,ptT.Pt.Y-5,10,10);
                if (r.Contains(pts))
                {
                    return ptT;
                }

            }
            return null;
        }


        public void PaintOn(Graphics gr)
        {

            gr.SmoothingMode = SmoothingMode.AntiAlias;
            gr.InterpolationMode = InterpolationMode.HighQualityBicubic;


            if (OriginalImage == null) return;

            SolidBrush selectionBrush = new SolidBrush(Color.FromArgb(100, 255, 255, 255));

            Rectangle destrect = new Rectangle(0 - (OriginalImage.Width / 2), (0 - (OriginalImage.Height / 2)), Convert.ToInt32(OriginalImage.Width), Convert.ToInt32(OriginalImage.Height));

            /*
            gr.ResetTransform();
                

            gr.ScaleTransform(_zoom, _zoom);

            gr.TranslateTransform((_Offset.X / _zoom) + (destrect.Width / 2), (_Offset.Y / _zoom) + (destrect.Height / 2));
            gr.RotateTransform(_angleDeRotation);

            */

            ReinitMatrix();
            gr.Transform = TransformMatrix;
            

            ImageAttributes _att = new ImageAttributes();
            ColorMatrix cm = new ColorMatrix();
            cm.Matrix00 = cm.Matrix11 = cm.Matrix22 = cm.Matrix33 = _Contraste;

            cm.Matrix40 = cm.Matrix41 = cm.Matrix42 = _Brightness;
            cm.Matrix44 = cm.Matrix43 = cm.Matrix33 = 1;


            _att.SetColorMatrix(cm);




            gr.DrawImage(OriginalImage, destrect, 0, 0, OriginalImage.Width, OriginalImage.Height, GraphicsUnit.Pixel, _att);

            gr.ResetTransform();


            Font ft = new Font("Arial", 12, FontStyle.Regular);

            if (_showHelp)
            {
                string helptxt = "R = Tourner(Rotate)\n";
                helptxt += "M = Bouger(Move)\n";
                helptxt += "Z = Zoom\n";
                helptxt += "B = Lumiere(Brightness)\n";
                helptxt += "C = Contraste(Contrast)\n";
                helptxt += "H = Voir l'aide(Help)\n";

                SizeF sz = gr.MeasureString(helptxt, ft);

                GraphicsPath path = new GraphicsPath();
                path.AddString(helptxt, ft.FontFamily, (int)ft.Style, ft.Size, new Rectangle((int)((Width - sz.Width) / 2), (int)((Height - sz.Height) / 2), (int)sz.Width, (int)sz.Height), StringFormat.GenericTypographic);


                gr.FillPath(Brushes.Black, path);
                gr.DrawPath(Pens.White, path);

                //gr.DrawString(helptxt, ft, Brushes.Black, new PointF(((Width - sz.Width) / 2), ((Height - sz.Height) / 2)));
            }
            else
            {


                foreach (PointToTake ptT in _ListOfPoints)
                {
                    if (!ptT.visible) continue;
                    Point pt = (ptT.Pt);

                    pt = TransformPtBmpToScreen(pt);

                    Font ftpt = new Font("Arial", 16, FontStyle.Regular);


                    gr.DrawEllipse(Pens.White, pt.X - 5, pt.Y - 5, 10, 10);

                    string PtName = ptT.PtName;
                    SizeF sz = gr.MeasureString(PtName, ftpt);

                    GraphicsPath path = new GraphicsPath();
                    path.AddString(PtName, ftpt.FontFamily, (int)ftpt.Style, ftpt.Size, new PointF(pt.X + 5, pt.Y + 5), StringFormat.GenericTypographic);


                    gr.DrawPath(new Pen(Brushes.White,2), path);
                    gr.FillPath(Brushes.Black, path);
                    
                }

            }
            if (SaisieStarted)
            {
                gr.DrawString(_ListOfPoints[CurrentPointIdx].PtName, ft, Brushes.Red, new PointF(2, 2));
            }
            else
            {
                if (this is IAnalyse)
                    gr.DrawString(((IAnalyse)this).GetTextResultat(), ft, Brushes.Red, new PointF(2, 2));
            }
            /*
            if (this is IAnalyse)
                ((IAnalyse)this).DrawExtraForms(gr);
             * */
        }

        

        Keys keyDown = Keys.None;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (OriginalImage == null) return;
            keyDown = e.KeyCode;
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.H)
            {
                _showHelp = !_showHelp;
                Invalidate();
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (OriginalImage == null) return;
            keyDown = Keys.None;
            base.OnKeyUp(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PaintOn(e.Graphics);

        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            Reinit();
            zoomAuto();
            Center();
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (OriginalImage == null) return;
            if (HittedOn != null)
            {
                Point pts = TransformPtScreenToBmp(new Point(e.X, e.Y));
                HittedOn.Pt = pts;

                if ((this is IAnalyse) && (!SaisieStarted))
                    ((IAnalyse)this).Recalculate();

                Invalidate();
            }

            if (e.Button == MouseButtons.Left)
            {
                if (keyDown == Keys.R)
                {
                    AngleDeRotation = AngleOnMouseDown + ((e.X - DownAt.X)/4);
                    Invalidate();
                }

                if (keyDown == Keys.M)
                {
                    Offset = new Point(OffsetOnMseDwn.X + (e.X - DownAt.X), OffsetOnMseDwn.Y + (e.Y - DownAt.Y));

                    Invalidate();
                }

                if (keyDown == Keys.Z)
                {
                    zoom = ZoomOnMouseDown + (((float)e.X - DownAt.X)/100f);
                    Invalidate();
                }

                if (keyDown == Keys.B)
                {
                    Brightness = BrightnessOnMseDown + (((float)e.X - DownAt.X) / 100f);
                    Invalidate();
                }

                if (keyDown == Keys.C)
                {
                    Contraste = ContrastOnMseDown + (((float)e.X - DownAt.X) / 100f);
                    Invalidate();
                }
            }
        }

        Point DownAt;
        Point OffsetOnMseDwn;
        float AngleOnMouseDown;
        float ZoomOnMouseDown;
        float BrightnessOnMseDown;
        float ContrastOnMseDown;
        PointToTake HittedOn = null;


        public Point TransformPtScreenToBmp(Point pt)
        {
            Point[] ptAfterTransform = new Point[1] { pt };
            TransformMatrixInverse.TransformPoints(ptAfterTransform);
            ptAfterTransform[0].Offset(OriginalImage.Width / 2, OriginalImage.Height / 2);
            return ptAfterTransform[0];
        }

        public Point TransformPtBmpToScreen(Point pt)
        {
            pt.Offset(-OriginalImage.Width / 2, -OriginalImage.Height / 2);
            Point[] ptAfterTransform = new Point[1] { pt };
            TransformMatrix.TransformPoints(ptAfterTransform);
            return ptAfterTransform[0];
        }

        public Point[] TransformPtScreenToBmp(Point[] pts)
        {
            TransformMatrixInverse.TransformPoints(pts);
            foreach (Point pt in pts)
                pt.Offset(OriginalImage.Width / 2, OriginalImage.Height / 2);
            return pts;
        }

        public Point[] TransformPtBmpToScreen(Point[] pts)
        {
            for (int i=0;i<pts.Length;i++)
                pts[i] = new Point(pts[i].X - (OriginalImage.Width / 2), pts[i].Y - (OriginalImage.Height / 2));
            TransformMatrix.TransformPoints(pts);
            return pts;
        }



        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (OriginalImage == null) return;
            DownAt = new Point(e.X, e.Y);

            HittedOn = HitTest(DownAt);

            if (HittedOn == null)
            {
                if ((SaisieStarted) && (keyDown == Keys.None))
                {


                    _ListOfPoints[CurrentPointIdx].Pt = TransformPtScreenToBmp(DownAt);
                    CurrentPointIdx++;

                    if (CurrentPointIdx >= _ListOfPoints.Count)
                    {
                        CurrentPointIdx = -1;
                        SaisieStarted = false;
                        if (this is IAnalyse)
                            ((IAnalyse)this).Recalculate();
                    }

                    Invalidate();
                }
            }
        
            
            
            
            OffsetOnMseDwn = Offset;
            AngleOnMouseDown = AngleDeRotation;
            ZoomOnMouseDown = zoom;
            BrightnessOnMseDown = Brightness;
            ContrastOnMseDown = Contraste;
            Focus();
            
        }
        
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (HittedOn != null)
            {
                HittedOn = null;

                
            }

            if ((this is IAnalyse) && (!SaisieStarted))
                ((IAnalyse)this).Recalculate();

            base.OnMouseUp(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta < 0)
                zoom *= 1.2f;
            else
                zoom /= 1.2f;
        }
                

        private void ImageCtrl_DragEnter(object sender, DragEventArgs e)
        {
            if ((e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                ||e.Data.GetDataPresent(DataFormats.Tiff)
                ||e.Data.GetDataPresent(DataFormats.Bitmap)
                )
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }


        private void ImageCtrl_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string f in files)
                {
                    try
                    {
                        loadImage(f);
                        zoomAuto();
                        Center();
                        Invalidate();
                        return;
                    }
                    catch (System.Exception)
                    { }
                }
            }

            if (e.Data.GetDataPresent(DataFormats.Bitmap, false) == true)
            {
                OriginalImage = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
                zoomAuto();
                Center();
                Invalidate();
                return;
            }
        }


    }

    


    
    

}
