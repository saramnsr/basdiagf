using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

namespace BaseCommonControls
{
    [Serializable]
    public class BASButton
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

        private RectangleF m_Bound;
        public RectangleF Bound
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

        private Color m_color = Color.LightGray;
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

        public void Draw(AbstractRenderer p_renderer, Graphics p_gr, bool p_IsSelected, bool p_IsHover, bool p_IsDown)
        {
            p_renderer.DrawButton(p_gr, m_Bound,((TreeNode)this.Tag), this, p_IsSelected, p_IsHover, p_IsDown);
        }

        public SizeF MeasureItem(AbstractRenderer p_renderer, Graphics p_gr)
        {
            return p_renderer.MeasureButton(p_gr, this);

        }


    }
}
