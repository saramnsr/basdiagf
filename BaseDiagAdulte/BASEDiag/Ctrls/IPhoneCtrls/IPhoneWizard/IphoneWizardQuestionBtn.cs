using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BASEPractice.Ctrls.IPhoneCtrls.IPhoneWizard
{
    public class IphoneWizardQuestionBtn
    {



        private AbstractIPhoneBtnRenderer _Renderer = new BASEPractice.Ctrls.IPhoneCtrls.IPhoneWizard.IphoneKeyBoardBtnRenderer();
        public AbstractIPhoneBtnRenderer Renderer
        {
            get
            {
                return _Renderer;
            }
            set
            {
                _Renderer = value;
            }
        }

        private Color _ForeColor = Color.Black;
        public Color ForeColor
        {
            get
            {
                return _ForeColor;
            }
            set
            {
                _ForeColor = value;
            }
        }

        private AbstractIPhoneBtnRenderer.TextLayout _TextAlign = AbstractIPhoneBtnRenderer.TextLayout.Center;
        public AbstractIPhoneBtnRenderer.TextLayout TextAlign
        {
            get
            {
                return _TextAlign;
            }
            set
            {
                _TextAlign = value;
            }
        }

        private Image _icon = null;
        public Image icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
            }
        }

        private Color _couleur = Color.LightGray;
        public Color couleur
        {
            get
            {
                return _couleur;
            }
            set
            {
                _couleur = value;
            }
        }

        private bool _Selected;
        public bool Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                _Selected = value;
            }
        }

        private int _Index;
        public int Index
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
            }
        }

        private Font _font = new Font("Arial", 12, FontStyle.Regular);
        public Font font
        {
            get
            {
                return _font;
            }
            set
            {
                _font = value;
            }
        }

        private object _Value;
        public object Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        private string _text;
        public string text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        private RectangleF _Bounds;
        public RectangleF Bounds
        {
            get
            {
                return _Bounds;
            }
            set
            {
                _Bounds = value;
            }
        }


        public void Draw(Graphics g)
        {

            if (Renderer == null)
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                if (!Selected)
                    g.DrawRectangle(Pens.Black, Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);
                else
                    g.DrawRectangle(Pens.Red, Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height);


                g.DrawString(text, font, Brushes.Black, Bounds, sf);
            }
            else
            {
                Renderer.DrawBtn(g, Bounds, this);
            }

        }
    
    }
}
