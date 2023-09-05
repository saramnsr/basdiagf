using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using BASEDiagAdulte.Ctrls.BO;

namespace BASEDiagAdulte.Ctrls
{
    public abstract class AbstractTreeViewRenderer
    {
        public abstract void DrawHeader(Graphics p_graphics, Rectangle p_Bound, TreeNode p_currentNode);
        public abstract void DrawButton(Graphics p_graphics, Rectangle p_Bound, trButton p_Button, bool p_IsSelected);

        
    }
}
