using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

namespace BaseCommonControls
{
    public abstract class AbstractRenderer
    {

        public ImageList imgList
        {
            get;
            set;
        }


        public abstract void DrawHeader(Graphics p_graphics, RectangleF p_Bound, TreeNode p_currentNode, bool p_IsHovered, bool p_IsDown);
        public abstract void DrawButton(Graphics p_graphics, RectangleF p_Bound,TreeNode tn, BASButton p_Button, bool p_IsSelected, bool p_IsHovered, bool p_IsDown);
        public abstract SizeF MeasureButton(Graphics p_graphics, BASButton p_Button);


    }
}
