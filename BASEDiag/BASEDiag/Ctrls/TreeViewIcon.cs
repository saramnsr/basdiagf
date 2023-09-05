using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag.Ctrls.BO;

namespace BASEDiag.Ctrls
{
    public partial class TreeViewIcon : UserControl
    {
        public List<trButton> ButtonList = new List<trButton>();

        public event PaintEventHandler ButtonPaint;
        public event EventHandler OnChangeLevel;
        public event EventHandler OnSelected;

        AbstractTreeViewRenderer renderer = new StandardTreeViewRenderer();

        int SPACEBETWEENBUTTONSWIDTH = 5;
        int SPACEBETWEENBUTTONSHEIGHT = 5;
        int HEADERHEIGHT = 30;

        int TotalHeight;


        
        private int m_ButtonHeight = 50;
        public int ButtonHeight
        {
            set { m_ButtonHeight = value; RecalculEmplacementButtons(false); }
            get { return m_ButtonHeight; }
        }

        private int m_ButtonWidth = 50;
        public int ButtonWidth
        {
            set { m_ButtonWidth = value; RecalculEmplacementButtons(false); }
            get { return m_ButtonWidth; }
        }

        public TreeNode Root = new TreeNode();
        private TreeNode CurrentNode;

       #region ctor
        public TreeViewIcon()
        {
            InitializeComponent();
            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);

            CurrentNode = Root;
            RecalculEmplacementButtons(true);
            VerticalScrollBar.Value = VerticalScrollBar.Minimum;
            VerticalScrollBar.Maximum = TotalHeight + (SPACEBETWEENBUTTONSHEIGHT * 5);
        }
        #endregion

        #region methods


        public TreeNode GetNodeAt(Point p_point)
        {
            foreach (trButton b in ButtonList)
            {
                if ((b.Bound.X < p_point.X) && (b.Bound.Y < p_point.Y)
                    && (b.Bound.X + b.Bound.Width > p_point.X) && (b.Bound.Y + b.Bound.Height > p_point.Y))
                    return ((TreeNode)b.Tag);
            }
            return null;
        }



        public void ForceRefresh()
        {
            RecalculEmplacementButtons(true);
        }

        public void RecalculEmplacementButtons(bool reinitbutton)
        {
            if (reinitbutton)
            {
                ButtonList.Clear();
                foreach (TreeNode n in CurrentNode.Nodes)
                {
                    trButton btn = new trButton();
                    btn.Tag = n;
                    btn.Text = n.Text;
                    ButtonList.Add(btn);
                }
            }
            int x = 0;
            int y = HEADERHEIGHT+SPACEBETWEENBUTTONSHEIGHT - VerticalScrollBar.Value;
            foreach (trButton b in ButtonList)
            {
                x += SPACEBETWEENBUTTONSWIDTH;

                if (x + m_ButtonWidth > this.Width)
                {
                    x = SPACEBETWEENBUTTONSWIDTH;
                    y += SPACEBETWEENBUTTONSHEIGHT;
                    y += m_ButtonHeight;
                }

                b.Bound = new Rectangle(x, y, m_ButtonWidth, m_ButtonHeight);
                x += m_ButtonWidth;
            }

            y += m_ButtonHeight + SPACEBETWEENBUTTONSHEIGHT;
            TotalHeight = Math.Abs(y - this.Height);
            
            Invalidate();
        }

        #endregion

        private void VerticalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            RecalculEmplacementButtons(false);
        }

        Rectangle HeaderRect;
        private void DrawHeader(PaintEventArgs e)
        {
            HeaderRect = e.ClipRectangle;
            HeaderRect.X = 5;
            HeaderRect.Y = 5;
            HeaderRect.Height = HEADERHEIGHT - 10;
            HeaderRect.Width = e.ClipRectangle.Width - VerticalScrollBar.Width - 10;
            renderer.DrawHeader(e.Graphics, HeaderRect, CurrentNode);
        }

        private void TreeViewIcon_Paint(object sender, PaintEventArgs e)
        {
            foreach (trButton b in ButtonList)
            {
                
                if (ButtonPaint != null) 
                    ButtonPaint(b.Tag, new PaintEventArgs(e.Graphics, b.Bound));
                else
                    b.Draw(renderer, e.Graphics, false);
            }

            DrawHeader(e);

        }


        private trButton ButtonAt(Point p_PtForButton)
        {
            foreach (trButton b in ButtonList)
            {
                if ((b.Bound.Left < p_PtForButton.X) && (b.Bound.Right > p_PtForButton.X) &&
                    (b.Bound.Top < p_PtForButton.Y) && (b.Bound.Bottom > p_PtForButton.Y))
                {
                    return b;
                }
            }
            return null;
        }

        private bool HitAtHeader(Point p_Pt)
        {
            foreach (trButton b in ButtonList)
            {
                if ((HeaderRect.Left < p_Pt.X) && (HeaderRect.Right > p_Pt.X) &&
                    (HeaderRect.Top < p_Pt.Y) && (HeaderRect.Bottom > p_Pt.Y))
                {
                    return true;
                }
            }
            return false;
        }

       

        private TableStripPanel ToolStripCtrl = new TableStripPanel();

        private void TreeViewIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void TreeViewIcon_MouseDown(object sender, MouseEventArgs e)
        {
            trButton b = ButtonAt(new Point(e.X, e.Y));
            if (b != null)
            {
                if (((TreeNode)b.Tag).Nodes.Count > 0)
                {
                    CurrentNode = ((TreeNode)b.Tag);
                    RecalculEmplacementButtons(true);
                    if (OnChangeLevel != null) OnChangeLevel(CurrentNode, new EventArgs());
                }
                if (OnSelected != null) OnSelected(b.Tag, new EventArgs());


            }
            else
            {
                if (HitAtHeader(new Point(e.X, e.Y)))
                {
                    if (CurrentNode.Parent != null)
                    {
                        CurrentNode = CurrentNode.Parent;
                        RecalculEmplacementButtons(true);
                        if (OnChangeLevel != null) OnChangeLevel(CurrentNode, new EventArgs());
                    }
                }
            }
            Invalidate();
        }

        
       
        
    }
}
