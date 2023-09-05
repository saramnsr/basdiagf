using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BO;
using System.IO;
namespace BASEDiagAdulte.Ctrls
{
    public partial class ImageCtrlAgg : UserControl,IDisposable
    {
        public event EventHandler OnEndSaisie;
        public event EventHandler OnEndSynchro;
        public event EventHandler OnSaisie;
        public event EventHandler OnRadioChanged;
        public event EventHandler OnPhotoChanged;


        private bool allowOffset = false;
        public bool AllowOffset
        {
            get { return allowOffset ; }
            set { allowOffset  = value; }
        }
        

        private string _TextIfNoImage = "Pas d'image";
        public string TextIfNoImage
        {
            get
            {
                return _TextIfNoImage;
            }
            set
            {
                _TextIfNoImage = value;
            }
        }
        private string _NoImage = "Pas d'image";
        public string NoImage
        {
            get
            {
                return _NoImage;
            }
            set
            {
                _NoImage = value;
            }
        }


        public enum ModeSaisie
        {
            None,
            Synchro,
            Saisie
        }

        #region properties



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


        private string _HelpFolder = ".\\";
        public string HelpFolder
        {
            get
            {
                return _HelpFolder;
            }
            set
            {
                _HelpFolder = value;
            }
        }


        private bool _Synchronized = false;
        public bool Synchronized
        {
            get
            {
                return _Synchronized;
            }
            set
            {
                _Synchronized = value;
            }
        }

        private ModeSaisie _currentmode = ModeSaisie.None;
        public ModeSaisie currentmode
        {
            get
            {
                return _currentmode;
            }
            set
            {
                _currentmode = value;
            }

        }

        private double _Transparence = 1;
        public double Transparence
        {
            get
            {
                return _Transparence;
            }
            set
            {

                _Transparence = value < 0.001 ? 0.001 : value;
                _Transparence = value > 0.99 ? 0.99 : _Transparence;

                Invalidate();
            }
        }

        protected PointToTake[] _PointSynchro = new PointToTake[4];
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public PointToTake[] PointSynchro
        {
            get
            {
                return _PointSynchro;
            }

        }


        protected List<PointToTake> _ListOfPoints = new List<PointToTake>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public List<PointToTake> ListOfPoints
        {
            get
            {
                return _ListOfPoints;
            }
            set
            {

                if (value.Count > 0)
                {
                    _ListOfPoints = value;

                    CurrentPointIdx = -1;
                    SaisieStarted = false;
                    currentmode = ModeSaisie.None;
                    if (this is IAnalyse)
                        ((IAnalyse)this).Recalculate();

                    if (this is IAnalyse)
                        ((IAnalyse)this).ReinitRotationAuto();

                    Invalidate();
                }
            }
        }



        private bool _DrawPointName = false;
        public bool DrawPointName
        {
            get
            {
                return _DrawPointName;
            }
            set
            {
                _DrawPointName = value;
            }
        }

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

        private float _angleDeRotationRadio = 0f;
        public float AngleDeRotationRadio
        {
            set
            {
                _angleDeRotationRadio = value;
                Invalidate();
            }
            get { return _angleDeRotationRadio; }
        }

        private float _angleDeRotationPhoto = 0f;
        public float AngleDeRotationPhoto
        {
            set
            {
                _angleDeRotationPhoto = value;
                Invalidate();
            }
            get { return _angleDeRotationPhoto; }
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

        private PointF _OffsetRadio = new PointF(0, 0);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public PointF OffsetRadio
        {
            set
            {
                _OffsetRadio = value;
                Invalidate();
            }
            get { return _OffsetRadio; }
        }

        private PointF _OffsetPhoto = new PointF(0, 0);
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public PointF OffsetPhoto
        {
            set
            {
                _OffsetPhoto = value;
                Invalidate();
            }
            get { return _OffsetPhoto; }
        }

        private string _file;
        public string file
        {
            set
            {
                if (value == null) return;
                _file = value;
                loadRadio(_file);
                Invalidate();
            }
            get { return _file; }
        }

        private Epsitec.Common.Drawing.Bitmap _RadioImage;
        public Epsitec.Common.Drawing.Bitmap RadioImage
        {
            get
            {
                return _RadioImage;
            }
            set
            {
                _RadioImage = value;
                if (OnRadioChanged != null)
                    OnRadioChanged(this, new EventArgs());
            }
        }

        private Epsitec.Common.Drawing.Bitmap _PhotoImage;
        public Epsitec.Common.Drawing.Bitmap PhotoImage
        {
            get
            {
                return _PhotoImage;
            }
            set
            {
                _PhotoImage = value;
                if (OnPhotoChanged != null)
                    OnPhotoChanged(this, new EventArgs());
            }
        }

        private Epsitec.Common.Drawing.Bitmap _HelpImage;
        public Epsitec.Common.Drawing.Bitmap HelpImage
        {
            get
            {
                return _HelpImage;
            }
            set
            {
                _HelpImage = value;
            }
        }


        private float _zoomRadio = 1f;
        public float zoomRadio
        {
            set
            {
                _zoomRadio = value;
                _zoomRadio = _zoomRadio <= 0 ? 0.05f : _zoomRadio;
                _zoomRadio = _zoomRadio >= 3 ? 3f : _zoomRadio;
                Invalidate();
            }
            get { return _zoomRadio; }
        }

        private float _zoomPhoto = 1f;
        public float zoomPhoto
        {
            set
            {
                _zoomPhoto = value;
                _zoomPhoto = _zoomPhoto <= 0 ? 0.05f : _zoomPhoto;
                _zoomPhoto = _zoomPhoto >= 3 ? 3f : _zoomPhoto;
                Invalidate();
            }
            get { return _zoomPhoto; }
        }

        private Point _RotationPointInRadio;
        public Point RotationPointInRadio
        {
            get
            {
                return _RotationPointInRadio;
            }
            set
            {
                _RotationPointInRadio = value;
            }
        }

        private Point _RotationPointInPhoto;
        public Point RotationPointInPhoto
        {
            get
            {
                return _RotationPointInPhoto;
            }
            set
            {
                _RotationPointInPhoto = value;
            }
        }
        #endregion

        #region privates


        #region Loupe Mgmt
        bool LoupeIsOn = false;
        int LOUPESIZE = 100;
        Epsitec.Common.Drawing.Bitmap loupebmp;

        #endregion

        public int CurrentPointIdx = -1;
        int CurrentPointSynchroIdx = -1;
        protected bool SaisieStarted = false;
        protected bool SaisieSyncroStarted = false;
        bool _showHelp = false;


        private Epsitec.Common.Drawing.Graphics AggGr;
        private Epsitec.Common.Drawing.Font Font;
        protected Epsitec.Common.Drawing.Transform RadioTransform = new Epsitec.Common.Drawing.Transform();
        protected Epsitec.Common.Drawing.Transform PhotoTransform = new Epsitec.Common.Drawing.Transform();


        #endregion

        #region ctors
        public ImageCtrlAgg()
        {

            InitializeComponent();

            this.SetStyle(
           ControlStyles.UserPaint |
           ControlStyles.AllPaintingInWmPaint |
           ControlStyles.DoubleBuffer, true);


            _PointSynchro[0] = new PointToTake("Radio 1");
            _PointSynchro[1] = new PointToTake("Radio 2");
            _PointSynchro[2] = new PointToTake("Photo 1");
            _PointSynchro[3] = new PointToTake("Photo 2");

        }

        void ActivateLoupe()
        {
            LoupeIsOn = true;

            Point pts = TransformPtScreenToRadio(DownAt);
            if (RadioImage != null)
                loupebmp = RadioImage.GetContrastedDetail(pts.X, RadioImage.PixelHeight - pts.Y, LOUPESIZE);
            else
            {
                if (PhotoImage == null)
                {
                    LoupeIsOn = false;
                    return;
                }
                loupebmp = PhotoImage.GetContrastedDetail(pts.X, PhotoImage.PixelHeight - pts.Y, LOUPESIZE);
            }
            Invalidate();
        }


        #endregion

        #region methods

        private void ReinitMatrix()
        {


            ReinitMatrixRadio();




            ReinitMatrixPhoto();




        }

        private void ReinitMatrixPhoto()
        {
            PhotoTransform.Reset();
            PhotoTransform.Translate(-RotationPointInPhoto.X, -RotationPointInPhoto.Y);
            PhotoTransform.Scale(_zoomPhoto, _zoomPhoto);
            PhotoTransform.RotateDeg(_angleDeRotationPhoto);
            PhotoTransform.Translate(RotationPointInPhoto.X, RotationPointInPhoto.Y);

            PhotoTransform.Translate(_OffsetPhoto.X, -_OffsetPhoto.Y);
        }

        private void ReinitMatrixRadio()
        {
            RadioTransform.Reset();
            RadioTransform.Translate(-RotationPointInRadio.X, -RotationPointInRadio.Y);
            RadioTransform.Scale(_zoomRadio, _zoomRadio);
            RadioTransform.RotateDeg(_angleDeRotationRadio);
            RadioTransform.Translate(RotationPointInRadio.X, RotationPointInRadio.Y);

            RadioTransform.Translate(_OffsetRadio.X, -_OffsetRadio.Y);
        }

        public void zoomAuto()
        {
            zoomAuto(false);
        }

        public void zoomAuto(bool zoomOnPhoto)
        {
            if ((PhotoImage == null) && (RadioImage == null)) return;

            Epsitec.Common.Drawing.Bitmap OriginalImage = RadioImage;
            if ((OriginalImage == null)||(zoomOnPhoto)) OriginalImage = PhotoImage;

            if (OriginalImage == null) return;
            float zW = (float)this.Width / (float)OriginalImage.NativeBitmap.Width;
            float zH = (float)this.Height / (float)OriginalImage.NativeBitmap.Height;

            zoomRadio = zW > zH ? zH : zW;
        }

        public void zoomAutoReverse()
        {
            if ((PhotoImage == null) && (RadioImage == null)) return;

            Epsitec.Common.Drawing.Bitmap OriginalImage = RadioImage;
            if (OriginalImage == null) OriginalImage = PhotoImage;

            if (OriginalImage == null) return;
            float zW = (float)this.Width / (float)OriginalImage.NativeBitmap.Width;
            float zH = (float)this.Height / (float)OriginalImage.NativeBitmap.Height;

            zoomRadio = zW < zH ? zH : zW;
        }

        public void zoomAutoHeight()
        {
            if ((PhotoImage == null) && (RadioImage == null)) return;

            Epsitec.Common.Drawing.Bitmap OriginalImage = RadioImage;
            if (OriginalImage == null) OriginalImage = PhotoImage;

            if (OriginalImage == null) return;
            float zH = (float)this.Height / (float)OriginalImage.NativeBitmap.Height;

            zoomRadio = zH;
        }

        public void zoomAutoWidth()
        {
            if ((PhotoImage == null) && (RadioImage == null)) return;

            Epsitec.Common.Drawing.Bitmap OriginalImage = RadioImage;
            if (OriginalImage == null) OriginalImage = PhotoImage;

            if (OriginalImage == null) return;
            float zW = (float)this.Width / (float)OriginalImage.NativeBitmap.Width;
            
            zoomRadio = zW;
        }

        public void Center()
        {
            Center(false);
        }

        public void Center(bool centerOnPhoto)
        {
            if ((PhotoImage == null) && (RadioImage == null)) return;

            Epsitec.Common.Drawing.Bitmap OriginalImage = RadioImage;


            if ((OriginalImage == null)||(centerOnPhoto)) OriginalImage = PhotoImage;
            /*
            Offset = new Point(0 - (int)((((float)OriginalImage.Width * zoom) - (float)this.Width) / 2),
                               0 - (int)((((float)OriginalImage.Height * zoom) - (float)this.Height) / 2));
            */




            OffsetRadio = new PointF((float)-(OriginalImage.Width - Width) / 2, (float)(OriginalImage.Height - Height) / 2f);




        }
        public void Center(Rectangle r)
        {
            if ((PhotoImage == null) && (RadioImage == null)) return;

            Epsitec.Common.Drawing.Bitmap OriginalImage = RadioImage;


            if (OriginalImage == null) OriginalImage = PhotoImage;
            /*
            Offset = new Point(0 - (int)((((float)OriginalImage.Width * zoom) - (float)this.Width) / 2),
                               0 - (int)((((float)OriginalImage.Height * zoom) - (float)this.Height) / 2));
            */




            OffsetRadio = new PointF((float)-(OriginalImage.Width - r.Width) / 2, (float)(OriginalImage.Height - r.Height) / 2f);




        }

        public void Clear()
        {
            if (RadioImage != null)
            {
                RadioImage.Dispose();
                RadioImage = null;
            }

            if (PhotoImage != null)
            {
                PhotoImage.Dispose();
                PhotoImage = null;
            }
        }

        public void loadRadio(string imagefile)
        {
            _file = imagefile;

            /*
            imagefilestream = new FileStream(imagefile, FileMode.Open);
            OriginalImage = new Bitmap(imagefilestream);
            */
           try
            {
                var req = System.Net.WebRequest.Create(imagefile);
                using (Stream stream = req.GetResponse().GetResponseStream())
                {
                    Bitmap image = (Bitmap)System.Drawing.Bitmap.FromStream(stream);

                float ratio = (float)image.Width / (float)image.Height;
                int w = Convert.ToInt32(1000);
                int h = Convert.ToInt32(1000 / ratio);


                Bitmap bmp = new Bitmap(w, h, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                bmp.SetResolution(72, 72);
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(Color.Black);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                g.DrawImage(image, new Rectangle(0, 0, w, h), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

                g.Dispose();

                RadioImage = (Epsitec.Common.Drawing.Bitmap)Epsitec.Common.Drawing.Bitmap.FromNativeBitmap(bmp);

                //bmp.Dispose();
                image.Dispose();




                //RadioImage = Epsitec.Common.Drawing.Bitmap.FromNativeBitmap(imagefile).BitmapImage;




                Reinit();
                }
            }
            catch (Exception e)
            {
            }


               
            
        }
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
       public void loadPhoto(string imagefile)
        {
            _file = imagefile;
            /*
            imagefilestream = new FileStream(imagefile, FileMode.Open);
            OriginalImage = new Bitmap(imagefilestream) ;
            */
            try
            {
                var req = System.Net.WebRequest.Create(imagefile);
                using (Stream stream = req.GetResponse().GetResponseStream())
                {                    
                        byte[] bytes = ReadFully(stream);
                        PhotoImage = Epsitec.Common.Drawing.Bitmap.FromData(bytes).BitmapImage;
                   
                  //  btmp.Dispose();

                }
                Reinit();
            }
            catch (Exception e)
            {
            }
        
        }

        public void loadRadio(Epsitec.Common.Drawing.Bitmap image)
        {
            _file = "";
            Clear();
            /*
            imagefilestream = new FileStream(imagefile, FileMode.Open);
            OriginalImage = new Bitmap(imagefilestream);
            */


            //if (resize)
            //{
            //    float ratio = (float)image.Width / (float)image.Height;
            //    int w = Convert.ToInt32(1000);
            //    int h = Convert.ToInt32(1000 / ratio);

            //    Bitmap bmp = (Bitmap)image.NativeBitmap.GetThumbnailImage(w, h, null, System.IntPtr.Zero);
            //    RadioImage = (Epsitec.Common.Drawing.Bitmap)Epsitec.Common.Drawing.Bitmap.FromNativeBitmap(bmp);

            //    bmp.Dispose();

            //}else
                RadioImage = (Epsitec.Common.Drawing.Bitmap)Epsitec.Common.Drawing.Bitmap.CopyImage(image);

            



            Reinit();

        }

        public void loadPhoto(Epsitec.Common.Drawing.Bitmap image)
        {
            _file = "";
            Clear();
            /*
            imagefilestream = new FileStream(imagefile, FileMode.Open);
            OriginalImage = new Bitmap(imagefilestream);
            */

            PhotoImage = (Epsitec.Common.Drawing.Bitmap)Epsitec.Common.Drawing.Bitmap.CopyImage(image);



            Reinit();

        }


        public void Reinit()
        {
            _zoomRadio = 1f;
            _OffsetRadio = new Point(0, 0);
            _Contraste = 1f;
            _Brightness = 0f;
            _angleDeRotationRadio = 0;

            if (RadioImage != null)
                _RotationPointInRadio = new Point((int)RadioImage.Width / 2, (int)RadioImage.Height / 2);
            if (PhotoImage != null)
                _RotationPointInPhoto = new Point((int)PhotoImage.Width / 2, (int)PhotoImage.Height / 2);
            Invalidate();
        }


        public void SynchroRadioPhoto(float zoom, float anglerotation, PointF offset)
        {
            zoomPhoto = zoom;
            AngleDeRotationPhoto = anglerotation;
            OffsetPhoto = offset;
            Invalidate();
            Synchronized = true;
        }

        private void SynchroImg()
        {


            zoomRadio = zoomRadBck;
            AngleDeRotationRadio = AngleDeRotationRadBck;
            OffsetRadio = new PointF(OffSetRadBck.X, OffSetRadBck.Y);



            double d1 = GeomTools.Distance(PointSynchro[0].Pt, PointSynchro[1].Pt);
            double d2 = GeomTools.Distance(PointSynchro[2].Pt, PointSynchro[3].Pt);

            //zoomPhoto = 1;
            zoomPhoto = (float)(d1 / d2);

            Epsitec.Common.Drawing.Transform t = new Epsitec.Common.Drawing.Transform();

            float a = GeomTools.AngleOfView(PointSynchro[0].Pt, PointSynchro[1].Pt, PointSynchro[2].Pt, PointSynchro[3].Pt);
            AngleDeRotationPhoto = a;


            t.Translate(-RotationPointInPhoto.X, -RotationPointInPhoto.Y);
            t.Scale(zoomPhoto);
            t.RotateDeg(_angleDeRotationPhoto);
            t.Translate(RotationPointInPhoto.X, RotationPointInPhoto.Y);

            double x = PointSynchro[2].Pt.X;
            double y = PointSynchro[2].Pt.Y;


            t.TransformDirect(ref x, ref y);


            OffsetPhoto = new PointF((PointSynchro[0].Pt.X - (float)x), ((float)y - PointSynchro[0].Pt.Y));

            if (OnEndSynchro != null) OnEndSynchro(this, new EventArgs());

            Invalidate();
        }

        public PointToTake HitTest(Point pt)
        {
            //Point pts = TransformPtScreenToBmp(pt);

            if (currentmode != ModeSaisie.Synchro)
            {

                foreach (PointToTake ptT in _ListOfPoints)
                {
                    PointF pts = TransformPtRadioToScreen(ptT.Pt);

                    RectangleF r = new RectangleF(pts.X - 5, (Height - pts.Y) - 5, 10, 10);
                    if (r.Contains(pt))
                    {
                        return ptT;
                    }

                }
            }
            else
            {
                foreach (PointToTake ptT in PointSynchro)
                {
                    PointF pts = TransformPtRadioToScreen(ptT.Pt);

                    RectangleF r = new RectangleF(pts.X - 5, (Height - pts.Y) - 5, 10, 10);
                    if (r.Contains(pt))
                    {
                        return ptT;
                    }

                }
            }
            return null;
        }


        public void StartSaisie()
        {
            if (_ListOfPoints.Count > 0)
            {
                CurrentPointIdx = 0;
                SaisieStarted = true;
                currentmode = ModeSaisie.Saisie;

                string CurrentHelpImage = HelpFolder + "\\" + _ListOfPoints[CurrentPointIdx].PtName + ".jpg";
                if (File.Exists(CurrentHelpImage))
                {
                    HelpImage = Epsitec.Common.Drawing.Bitmap.FromFile(CurrentHelpImage).BitmapImage;
                }

            }
        }

        float zoomRadBck;
        float AngleDeRotationRadBck;
        PointF OffSetRadBck;

        public void StartSynchro()
        {
            if (PhotoImage == null) return;
            if (RadioImage == null) return;
            FrmSynchroRadioPhoto frm = new FrmSynchroRadioPhoto(RadioImage, PhotoImage);

            if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                SynchroRadioPhoto(frm.Zoom, frm.AngleRot, frm.OffSet);
                if (OnEndSynchro != null)
                    OnEndSynchro(this, new EventArgs());
            }
        }

        public void StartOldSynchro()
        {
            if ((RadioImage == null) || (PhotoImage == null)) return;

            float ratioh = (float)(RadioImage.Height / PhotoImage.Height);

            zoomRadBck = zoomRadio;
            AngleDeRotationRadBck = AngleDeRotationRadio;
            OffSetRadBck = new PointF(OffsetRadio.X, OffsetRadio.Y);

            


            zoomPhoto = ratioh;
            AngleDeRotationPhoto = 0;
            OffsetPhoto = new PointF(0, 0);

            Reinit();
            zoomAuto();
            Center();


            SaisieSyncroStarted = CurrentPointSynchroIdx == -1;
            if (CurrentPointSynchroIdx == -1)
            {
                CurrentPointSynchroIdx = 0;
                Transparence = 1;
            }
            currentmode = ModeSaisie.Synchro;


        }
        #endregion

        #region events

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            Reinit();
            zoomAuto();
            Center();
            Invalidate();
            base.OnMouseDoubleClick(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!DesignMode)
            {
                try
                {
                    AggGr = new Epsitec.Common.Drawing.Graphics();
                }
                catch (TypeInitializationException tie)
                {
                    //Exception sous-jacente
                    MessageBox.Show(tie.InnerException.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Autre exception : " + ex.Message);
                }
            }
                
            base.OnLoad(e);
        }

        public void DrawOutlinedText(Epsitec.Common.Drawing.Graphics Agr, string text, Epsitec.Common.Drawing.Font ft, double size, double x, double y)
        {
            DrawOutlinedText(Agr, text, ft, size, x, y, new Epsitec.Common.Drawing.Color(1, 0, 0, 0), new Epsitec.Common.Drawing.Color(1, 1, 1, 1));
        }

        public void DrawOutlinedText(Epsitec.Common.Drawing.Graphics Agr, string text, Epsitec.Common.Drawing.Font ft, double size, double x, double y,
                Epsitec.Common.Drawing.Color TextColor,
                Epsitec.Common.Drawing.Color BackColor)
        {
            Epsitec.Common.Drawing.Path txtPth = GetGlyphs(x, y, text, ft, size);

            

            AggGr.Rasterizer.AddOutline(txtPth, 5, Epsitec.Common.Drawing.CapStyle.Round, Epsitec.Common.Drawing.JoinStyle.Round);

            AggGr.SolidRenderer.Color = BackColor;// new Epsitec.Common.Drawing.Color(1, 1, 1, 1);
            AggGr.RenderSolid();

            AggGr.Rasterizer.AddSurface(txtPth);

            AggGr.SolidRenderer.Color = TextColor;// new Epsitec.Common.Drawing.Color(1, 0, 0, 0);
            AggGr.RenderSolid();
        }

        public Epsitec.Common.Drawing.Path GetGlyphs(double x, double y, string text, Epsitec.Common.Drawing.Font ft, double size)
        {
            int n = text.Length;
            Epsitec.Common.Drawing.Path path = new Epsitec.Common.Drawing.Path();

            double currentx = x;
            double currenty = y - ft.LineHeight * size;
            for (int i = 0; i < n; i++)
            {
                if (text[i] == '\n')
                {
                    currentx = x;
                    currenty -= ft.LineHeight * size;
                }
                else
                {

                    int glyph = ft.GetGlyphIndex(text[i]);
                    double d = ft.GetGlyphAdvance(glyph);
                    path.Append(ft, glyph, currentx, currenty, size);
                    currentx += d * size;
                }
            }

            return path;
        }

        public Epsitec.Common.Drawing.Size GetSize(string text, Epsitec.Common.Drawing.Font ft, double size)
        {
            int n = text.Length;

            double currentx = 0;
            double maxx = 0;
            double currenty = 0 - ft.LineHeight * size;
            for (int i = 0; i < n; i++)
            {
                if (text[i] == '\n')
                {
                    maxx = maxx<currentx?currentx:maxx;

                    currentx = 0;
                    currenty -= ft.LineHeight * size;
                }
                else
                {

                    int glyph = ft.GetGlyphIndex(text[i]);
                    double d = ft.GetGlyphAdvance(glyph);
                    currentx += d * size;
                }
            }
            maxx = maxx < currentx ? currentx : maxx;
            return new Epsitec.Common.Drawing.Size(maxx, currenty);
        }


        public void PaintOn(Graphics gr, Rectangle r,bool ForImpression)
        {

            Epsitec.Common.Drawing.Font ft;

            ft = Epsitec.Common.Drawing.Font.GetFont("Arial", "Regular");


            if (AggGr == null) AggGr = new Epsitec.Common.Drawing.Graphics();
            double fontsize = 12;

            ReinitMatrix();

            AggGr.Pixmap.Size = r.Size;
            AggGr.Pixmap.Clear();

            #region Inittialisation GRaphics
            AggGr.SolidRenderer.Pixmap = AggGr.Pixmap;
            AggGr.SolidRenderer.Clear(Epsitec.Common.Drawing.Color.FromBrightness(1));
            AggGr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(Color.Black);

            AggGr.ImageRenderer.Pixmap = AggGr.Pixmap;
            AggGr.GradientRenderer.Pixmap = AggGr.Pixmap;

            AggGr.Rasterizer.Gamma = 1.2;
            #endregion

            #region Drawing Images

            Epsitec.Common.Drawing.Graphics maskPhoto = AggGr.CreateAlphaMask();

            Epsitec.Common.Drawing.Path mskpth = new Epsitec.Common.Drawing.Path();

            mskpth.MoveTo(0, 0);
            mskpth.LineTo(0, r.Height);
            mskpth.LineTo(r.Width, r.Height);
            mskpth.LineTo(r.Width, 0);
            mskpth.Close();

            maskPhoto.Rasterizer.AddSurface(mskpth);
            maskPhoto.RenderSolid(Epsitec.Common.Drawing.Color.FromARGB(1 - _Transparence, 0, 0, 0));



            Epsitec.Common.Drawing.Graphics maskRadio = AggGr.CreateAlphaMask();
            maskRadio.Rasterizer.AddSurface(mskpth);

            maskRadio.RenderSolid(Epsitec.Common.Drawing.Color.FromARGB(_Transparence, 0, 0, 0));

            AggGr.AddFilledRectangle(0, 0, r.Width, r.Height);
            AggGr.ImageRenderer.BitmapImage = RadioImage;

            AggGr.Rasterizer.Transform = new Epsitec.Common.Drawing.Transform();

            AggGr.ImageRenderer.Transform = RadioTransform;
            // AggGr.ImageRenderer.SelectBilinearFilter();

            AggGr.ImageRenderer.SetAlphaMask(maskRadio.Pixmap, Epsitec.Common.Drawing.MaskComponent.A);
            AggGr.RenderImage();


            AggGr.AddFilledRectangle(0, 0, r.Width, r.Height);
            AggGr.ImageRenderer.BitmapImage = PhotoImage;

            Epsitec.Common.Drawing.Transform trc = new Epsitec.Common.Drawing.Transform(RadioTransform);
            trc.MultiplyByPostfix(PhotoTransform);

            AggGr.ImageRenderer.Transform = trc;
            //AggGr.ImageRenderer.SelectBilinearFilter();

            AggGr.ImageRenderer.SetAlphaMask(maskPhoto.Pixmap, Epsitec.Common.Drawing.MaskComponent.A);
            AggGr.RenderImage();


            maskPhoto.Rasterizer.AddSurface(mskpth);
            maskPhoto.RenderSolid(Epsitec.Common.Drawing.Color.FromARGB(1, 0, 0, 0));

            AggGr.ImageRenderer.SetAlphaMask(maskPhoto.Pixmap, Epsitec.Common.Drawing.MaskComponent.A);


            #endregion

            #region Drawing points
            if ((!_showHelp) && (loupebmp == null))
            {
                if (currentmode != ModeSaisie.Synchro)
                {
                    if (this is IAnalyse)
                        ((IAnalyse)this).DrawExtraForms(AggGr, ForImpression);

                    foreach (PointToTake ptT in ListOfPoints)
                    {

                        PointF pt = (ptT.Pt);

                        pt = TransformPtRadioToScreen(pt);
                        if (ptT.visible)
                        {



                            pt = DrawPoint(ptT, pt);
                        }
                    }
                }
                else
                {


                    PointF pt = (PointSynchro[0].Pt);
                    pt = TransformPtRadioToScreen(pt);
                    DrawPoint(PointSynchro[0], pt);

                    pt = (PointSynchro[1].Pt);
                    pt = TransformPtRadioToScreen(pt);
                    DrawPoint(PointSynchro[1], pt);

                    pt = (PointSynchro[2].Pt);
                    pt = TransformPtPhotoToScreen(pt);
                    DrawPoint(PointSynchro[2], pt);

                    pt = (PointSynchro[3].Pt);
                    pt = TransformPtPhotoToScreen(pt);
                    DrawPoint(PointSynchro[3], pt);

                }
            }
            #endregion

            #region Drawing Magnifier
            if (loupebmp != null)
            {

                Point pt = new Point(MoveAt.X - LOUPESIZE, Height - MoveAt.Y);

                AggGr.AddFilledCircle(LOUPESIZE + pt.X, LOUPESIZE + pt.Y, LOUPESIZE, LOUPESIZE);
                AggGr.ImageRenderer.BitmapImage = loupebmp;
                Epsitec.Common.Drawing.Transform tr = new Epsitec.Common.Drawing.Transform();
                tr.Translate(pt.X, pt.Y);
                tr.RotateDeg(AngleDeRotationRadio, LOUPESIZE + pt.X, LOUPESIZE + pt.Y);
                AggGr.ImageRenderer.Transform = tr;
                AggGr.RenderImage();



                pt.Offset(LOUPESIZE, LOUPESIZE);

                Epsitec.Common.Drawing.Point Ept = new Epsitec.Common.Drawing.Point(pt.X, pt.Y);

                Epsitec.Common.Drawing.Path circle = Epsitec.Common.Drawing.Path.CreateCircle(Ept, 3, 3);

                AggGr.Rasterizer.AddSurface(circle);
                AggGr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(.5, 1, 1, 1);
                AggGr.RenderSolid();

                AggGr.Rasterizer.AddOutline(circle);
                AggGr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 0, 0, 0);
                AggGr.RenderSolid();

                /*
                    
                string txt = "";

                if (SaisieStarted)
                    txt = _ListOfPoints[CurrentPointIdx].PtName;
                if (HittedOn != null)
                    txt = HittedOn.PtName;
                DrawOutlinedText(AggGr, txt, ft, Ptfontsize, pt.X + 5, pt.Y + 10);
                */

            }

            #endregion

            #region Drawing Help
            if (_showHelp)
            {

                string helptxt = "R = Tourner(Rotate)\n";
                helptxt += "M = Bouger(Move)\n";
                helptxt += "L = Loupe\n";
                helptxt += "Z = Zoom\n";
                helptxt += "N = Name\n";
                if ((PhotoImage != null) && (RadioImage != null))
                {
                    helptxt += "S = Synchroniser Radio/Photo\n";
                    helptxt += "+/- = Transparence Radio/Photo\n";
                }
                //helptxt += "B = Lumiere(Brightness)\n"; 
                //helptxt += "C = Contraste(Contrast)\n"; 
                helptxt += "H = Voir l'aide(Help)\n";




                double Helpfontsize = 12;
                Epsitec.Common.Drawing.Font Helpft = Epsitec.Common.Drawing.Font.GetFont("Arial", "Regular");

                DrawOutlinedText(AggGr, helptxt, Helpft, Helpfontsize, 100, r.Height - 100);

            }
            #endregion

            #region Drawing UpperRight text/Image
            if (currentmode == ImageCtrlAgg.ModeSaisie.Saisie)
            {
                string text = "veuillez saisir le point : " + ListOfPoints[CurrentPointIdx].PtName;

                Epsitec.Common.Drawing.Path txtPth = GetGlyphs(0, 0, text, ft, fontsize);
                Epsitec.Common.Drawing.Rectangle recttxt = txtPth.ComputeBounds();

                if (CurrentPointIdx > -1) DrawOutlinedText(AggGr, text, ft, fontsize, r.Width - recttxt.Width - 5, r.Height - 2);


                if (HelpImage != null)
                {
                    AggGr.AddFilledRectangle(0, 0, r.Width, r.Height);
                    AggGr.ImageRenderer.BitmapImage = HelpImage;


                    Epsitec.Common.Drawing.Transform tr = new Epsitec.Common.Drawing.Transform();
                    tr.Translate(r.Width - HelpImage.Width, r.Height - HelpImage.Height - recttxt.Height - 15);
                    AggGr.ImageRenderer.Transform = tr;
                    // AggGr.ImageRenderer.SelectBilinearFilter();

                    AggGr.RenderImage();

                }

            }
            if (currentmode == ImageCtrlAgg.ModeSaisie.None)
            {
                if (this is IAnalyse)
                {
                    DrawTextResult(AggGr);
                }

            }
            if (currentmode == ImageCtrlAgg.ModeSaisie.Synchro)
            {
                Epsitec.Common.Drawing.Path txtPth = GetGlyphs(0, 0, "Deuxieme point sur la radio  ", ft, fontsize);
                Epsitec.Common.Drawing.Rectangle recttxt = txtPth.ComputeBounds();


                if (CurrentPointSynchroIdx == 0)
                    DrawOutlinedText(AggGr, "Premier point sur la radio", ft, fontsize, r.Width - recttxt.Width - 5, r.Height - 2);
                if (CurrentPointSynchroIdx == 1)
                    DrawOutlinedText(AggGr, "Deuxieme point sur la radio", ft, fontsize, r.Width - recttxt.Width - 5, r.Height - 2);
                if (CurrentPointSynchroIdx == 2)
                    DrawOutlinedText(AggGr, "Premier point sur la photo", ft, fontsize, r.Width - recttxt.Width - 5, r.Height - 2);
                if (CurrentPointSynchroIdx == 3)
                    DrawOutlinedText(AggGr, "Deuxieme point sur la photo", ft, fontsize, r.Width - recttxt.Width - 5, r.Height - 2);


            }


            #endregion

            #region No Image Text Drawing

            if ((RadioImage == null)&&(TextIfNoImage!=""))
            {
                double NoImageTextFontSize = 16;
                Epsitec.Common.Drawing.Font NoImageTextFont = Epsitec.Common.Drawing.Font.GetFont("Arial", "Regular");

                Epsitec.Common.Drawing.Size sz = GetSize(TextIfNoImage, NoImageTextFont, NoImageTextFontSize);

                PointF startpt = new PointF((Width - (float)sz.Width) / 2f, (Height - (float)sz.Height) / 2f);


                Epsitec.Common.Drawing.Path p = new Epsitec.Common.Drawing.Path();

                p.MoveTo(2, 2);
                p.LineTo(2, r.Height-5);
                p.LineTo(r.Width - 5, r.Height - 5);
                p.LineTo(r.Width - 5, 2);
                p.Close();

                AggGr.Rasterizer.AddOutline(p);

                AggGr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, .2, .2, .2);
                AggGr.RenderSolid();                
                DrawOutlinedText(AggGr, TextIfNoImage, NoImageTextFont, NoImageTextFontSize, startpt.X, startpt.Y);


            }
            #endregion

            AggGr.Pixmap.Paint(gr, r);
            mskpth.Dispose();
            maskPhoto.Dispose();
            maskRadio.Dispose();

        }

        protected virtual void DrawTextResult(Epsitec.Common.Drawing.Graphics AggGr)
        {
            Epsitec.Common.Drawing.Font ft = Epsitec.Common.Drawing.Font.GetFont("Arial", "Regular");
            double fontsize = 12;

            string text = ((IAnalyse)this).GetTextResultat();
            DrawOutlinedText(AggGr, text, ft, fontsize, 2, Height - 2);
        }

        private PointF DrawPoint(PointToTake ptT, PointF pt)
        {


            double Ptfontsize = 10;
            Epsitec.Common.Drawing.Font ft;

            ft = Epsitec.Common.Drawing.Font.GetFont("Arial", "Regular");

            Epsitec.Common.Drawing.Point Ept = new Epsitec.Common.Drawing.Point(pt.X, pt.Y);

            Epsitec.Common.Drawing.Path circle = Epsitec.Common.Drawing.Path.CreateCircle(Ept, 3, 3);

            AggGr.Rasterizer.AddSurface(circle);
            AggGr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(.5, 1, 1, 1);
            AggGr.RenderSolid();

            AggGr.Rasterizer.AddOutline(circle);
            AggGr.SolidRenderer.Color = new Epsitec.Common.Drawing.Color(1, 0, 0, 0);
            AggGr.RenderSolid();


            if (_DrawPointName) DrawOutlinedText(AggGr, ptT.PtName, ft, Ptfontsize, pt.X + 5, pt.Y + 10);
            return pt;
        }

        protected override void OnPaint(PaintEventArgs e)
        {





            if (!DesignMode)
            {

                PaintOn(e.Graphics, new Rectangle(0, 0, Bounds.Width, Bounds.Height), false);
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.Gray, new Rectangle(0, 0, Bounds.Width, Bounds.Height));

                e.Graphics.DrawString("Design Mode", new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.Black, new PointF(10, 10));
            }
            base.OnPaint(e);
        }



        Point DownAt;
        Point MoveAt;

        PointF OffsetOnMseDwn;
        float AngleOnMouseDown;
        float ZoomOnMouseDown;
        float BrightnessOnMseDown;
        float ContrastOnMseDown;
        PointToTake HittedOn = null;


        public Point TransformPtScreenToPhoto(Point pt)
        {
            double px = pt.X;
            double py = Height - pt.Y;

            Epsitec.Common.Drawing.Transform trc = new Epsitec.Common.Drawing.Transform(RadioTransform);
            trc.MultiplyByPostfix(PhotoTransform);


            trc.TransformInverse(ref px, ref py);
            return new Point((int)px, (int)(py));
        }

        public Point TransformPtScreenToRadio(Point pt)
        {
            double px = pt.X;
            double py = Height - pt.Y;
            RadioTransform.TransformInverse(ref px, ref py);
            return new Point((int)px, (int)(py));
        }


        public PointF[] TransformPtPhotoToScreen(PointF[] pts)
        {
            PointF[] res = new PointF[pts.Length];
            for (int i = 0; i < pts.Length; i++)
            {
                double px = pts[i].X;
                double py = pts[i].Y;

                Epsitec.Common.Drawing.Transform trc = new Epsitec.Common.Drawing.Transform(RadioTransform);
                trc.MultiplyByPostfix(PhotoTransform);

                trc.TransformDirect(ref px, ref py);
                res[i] = new PointF((float)px, (float)py);
            }

            return res;
        }

        public PointF[] TransformPtRadioToScreen(PointF[] pts)
        {
            PointF[] res = new PointF[pts.Length];
            for (int i = 0; i < pts.Length; i++)
            {
                double px = pts[i].X;
                double py = pts[i].Y;

                RadioTransform.TransformDirect(ref px, ref py);
                res[i] = new PointF((float)px, (float)py);
            }

            return res;
        }

        public PointF TransformPtPhotoToScreen(PointF pt)
        {
            double px = pt.X;
            double py = pt.Y;

            Epsitec.Common.Drawing.Transform trc = new Epsitec.Common.Drawing.Transform(RadioTransform);
            trc.MultiplyByPostfix(PhotoTransform);

            trc.TransformDirect(ref px, ref py);
            return new PointF((float)px, (float)py);
        }

        public PointF TransformPtRadioToScreen(PointF pt)
        {
            double px = pt.X;
            double py = pt.Y;
            RadioTransform.TransformDirect(ref px, ref py);
            return new PointF((float)px, (float)py);
        }

        Keys keyDown = Keys.None;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            keyDown = e.KeyCode;
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.H)
            {
                _showHelp = !_showHelp;
                Invalidate();
            }

            if (e.KeyCode == Keys.N)
            {
                DrawPointName = !DrawPointName;
                Invalidate();
            }

            if ((RadioImage != null) && (PhotoImage != null))
            {
                if (e.KeyCode == Keys.S)
                {
                    StartSynchro();
                    Invalidate();
                }
                if (e.KeyCode == Keys.Add)
                {
                    Transparence = Transparence + .2;
                    Invalidate();
                }

                if (e.KeyCode == Keys.Subtract)
                {
                    Transparence = Transparence - .2;
                    Invalidate();
                }
            }

        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            keyDown = Keys.None;
            base.OnKeyUp(e);
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            DownAt = new Point(e.X, e.Y);

            if (!ReadOnly) HittedOn = HitTest(DownAt);

            if (keyDown == Keys.L) ActivateLoupe();



            OffsetOnMseDwn = OffsetRadio;
            AngleOnMouseDown = AngleDeRotationRadio;
            ZoomOnMouseDown = zoomRadio;
            BrightnessOnMseDown = Brightness;
            ContrastOnMseDown = Contraste;
            Focus();

        }


        

        protected override void OnMouseUp(MouseEventArgs e)
        {

            Point UpAt = new Point(e.X, e.Y);



            LoupeIsOn = false;

            if (loupebmp != null)
            {
                loupebmp.Dispose();
                loupebmp = null;
                Invalidate();
            }

            if ((currentmode == ImageCtrlAgg.ModeSaisie.Saisie) && ((keyDown == Keys.None) || (keyDown == Keys.L)) && (HittedOn == null))
            {


                _ListOfPoints[CurrentPointIdx].Pt = TransformPtScreenToRadio(UpAt);
                
                if (this is IAnalyse)
                    ((IAnalyse)this).ReinitRotationAuto();

                if (OnSaisie != null) OnSaisie(_ListOfPoints[CurrentPointIdx], new EventArgs());
                CurrentPointIdx++;




                HelpImage = null;

                if (CurrentPointIdx >= _ListOfPoints.Count)
                {
                    EndSaisie();
                }
                else
                {
                    string CurrentHelpImage = HelpFolder + "\\" + _ListOfPoints[CurrentPointIdx].PtName + ".jpg";
                    if (File.Exists(CurrentHelpImage))
                    {
                        HelpImage = Epsitec.Common.Drawing.Bitmap.FromFile(CurrentHelpImage).BitmapImage;
                    }
                }

                Invalidate();
            }

            if ((currentmode == ImageCtrlAgg.ModeSaisie.Synchro) && ((keyDown == Keys.None) || (keyDown == Keys.L)))
            {

                if (CurrentPointSynchroIdx < 2)
                {
                    PointSynchro[CurrentPointSynchroIdx].Pt = TransformPtScreenToRadio(UpAt);

                }
                else
                {
                    PointSynchro[CurrentPointSynchroIdx].Pt = TransformPtScreenToPhoto(UpAt);

                }

                if (CurrentPointSynchroIdx < 1)
                    Transparence = 1;
                else
                    Transparence = 0;

                if (CurrentPointSynchroIdx == 1)
                {
                    zoomAuto(true);
                    Center(true);
                }

                CurrentPointSynchroIdx++;

                if (CurrentPointSynchroIdx >= PointSynchro.Length)
                {
                    CurrentPointSynchroIdx = -1;
                    SaisieSyncroStarted = false;
                    currentmode = SaisieStarted ? ModeSaisie.Saisie : ModeSaisie.None;
                    Synchronized = true;
                    SynchroImg();

                    PointSynchro[0].Pt = new Point(0, 0);
                    PointSynchro[1].Pt = new Point(0, 0);
                    PointSynchro[2].Pt = new Point(0, 0);
                    PointSynchro[3].Pt = new Point(0, 0);

                    Transparence = 0.5f;
                }

                Invalidate();
            }


            if (((keyDown == Keys.None) || (keyDown == Keys.L)) && (HittedOn != null))
            {
                if (this is IAnalyse) ((IAnalyse)this).ReinitRotationAuto();

                if (OnSaisie != null) OnSaisie(HittedOn, new EventArgs());
            }

            if ((this is IAnalyse) && (currentmode == ImageCtrlAgg.ModeSaisie.None))
                ((IAnalyse)this).Recalculate();


            if (HittedOn != null)
                HittedOn = null;


            base.OnMouseUp(e);
        }

        public void EndSaisie()
        {
            CurrentPointIdx = -1;
            currentmode = ModeSaisie.None;
            SaisieStarted = false;
            if (this is IAnalyse)
                ((IAnalyse)this).Recalculate();

            if (OnEndSaisie != null)
            {
                OnEndSaisie(this, new EventArgs());
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta < 0)
                zoomRadio /= 1.2f;
            else
                zoomRadio *= 1.2f;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {

            MoveAt = new Point(e.X, e.Y);


            if (HittedOn != null)
            {

                Point pts = TransformPtScreenToRadio(new Point(e.X, e.Y));
                HittedOn.Pt = pts;

                if ((currentmode == ImageCtrlAgg.ModeSaisie.None) && (OnEndSaisie != null)) OnEndSaisie(this, new EventArgs());

                if ((this is IAnalyse) && (currentmode == ImageCtrlAgg.ModeSaisie.None))
                    ((IAnalyse)this).Recalculate();

                Invalidate();
                base.OnMouseDown(e);
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (LoupeIsOn)
                {
                    Point pts = TransformPtScreenToRadio(new Point(e.X, e.Y));
                    Epsitec.Common.Drawing.Bitmap OriginalImage = RadioImage;
                    if (OriginalImage == null) OriginalImage = PhotoImage;


                    loupebmp = OriginalImage.GetContrastedDetail(pts.X, OriginalImage.PixelHeight - pts.Y, LOUPESIZE);
                    Invalidate();
                }else


                if (keyDown == Keys.R)
                {
                    AngleDeRotationRadio = AngleOnMouseDown + ((e.X - DownAt.X) / 4);
                    Invalidate();
                }else

                // if ((keyDown == Keys.M) || ((keyDown == Keys.None) && (!SaisieStarted) && (HittedOn == null)))
                if (keyDown == Keys.M)
                {
                    OffsetRadio = new PointF(OffsetOnMseDwn.X + (e.X - DownAt.X), OffsetOnMseDwn.Y + (e.Y - DownAt.Y));

                    Invalidate();
                }else

                if (keyDown == Keys.Z)
                {
                    zoomRadio = ZoomOnMouseDown + (((float)e.X - DownAt.X) / 100f);
                    Invalidate();
                }else

                if (keyDown == Keys.B)
                {
                    Brightness = BrightnessOnMseDown + (((float)e.X - DownAt.X) / 100f);
                    Invalidate();
                }else

                if (keyDown == Keys.C)
                {
                    Contraste = ContrastOnMseDown + (((float)e.X - DownAt.X) / 100f);
                    Invalidate();
                }else
                {
                    if (AllowOffset)
                    {

                        OffsetRadio = new PointF(
                            OffsetOnMseDwn.X + ((float)e.X - DownAt.X),
                            OffsetOnMseDwn.Y + ((float)e.Y - DownAt.Y)
                            );
                    }
                }
            }




        }


        private void ImageCtrlAgg_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                try
                {
                    loadRadio(files[0]);
                    zoomAuto();
                    Center();
                    Invalidate();
                    return;
                }
                catch (System.Exception)
                { }

            }

            if (e.Data.GetDataPresent(DataFormats.Bitmap, false) == true)
            {
                PhotoImage = (Epsitec.Common.Drawing.Bitmap)Epsitec.Common.Drawing.Bitmap.FromNativeBitmap((Bitmap)e.Data.GetData(DataFormats.Bitmap));
                zoomAuto();
                Center();
                Invalidate();
                return;
            }
        }

        private void ImageCtrlAgg_DragEnter(object sender, DragEventArgs e)
        {
            if ((e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                || e.Data.GetDataPresent(DataFormats.Tiff)
                || e.Data.GetDataPresent(DataFormats.Bitmap)
                )
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        #endregion


    }



}
