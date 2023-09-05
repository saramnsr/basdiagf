using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using BaseCommonControls.Ctrls;

namespace BaseCommonControls.Ctrls.BO
{
    public class trButton
    {
        public object Tag;

        private string m_Text;
        public string Text
        {
            get
            {
                return m_Text;
            }
            set
            {
                m_Text = value;
            }
        }

        private Font ft = new Font("Segoe UI", 8, FontStyle.Regular);

        private Rectangle m_Bound;
        public Rectangle Bound
        {
            get
            {
                return m_Bound;
            }
            set
            {
                m_Bound = value;
            }
        }

        private Color m_color = Color.LightBlue;
        public Color Color
        {
            get
            {
                return m_color;
            }
            set
            {
                m_color = value;
            }
        }

        public void Draw(AbstractTreeViewRenderer p_renderer, Graphics p_gr, bool p_IsSelected, bool p_IsHover)
        {
            p_renderer.DrawButton(p_gr, m_Bound, this, p_IsSelected);
        }

        
    }
}
