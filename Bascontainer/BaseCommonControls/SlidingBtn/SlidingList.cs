using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseCommonControls
{
    public partial class SlidingList : UserControl
    {


        private bool _MultiSelectMode = false;
        public bool MultiSelectMode
        {
            get
            {
                return _MultiSelectMode;
            }
            set
            {
                _MultiSelectMode = value;
            }
        }

        protected bool _WrapMode = false;
        public bool WrapMode
        {
            get
            {
                return _WrapMode;
            }
            set
            {
                _WrapMode = value;
            }
        }

        public event MouseEventHandler OnLongClickEvent;
        public event EventHandler OnSelectionChange;

        Timer BigClickTimer = new Timer();

        
        private int _NbLigne = 1;
        [DefaultValue(1)]
        public int NbLigne
        {
            get
            {
                return _NbLigne;
            }
            set
            {
                _NbLigne = value;
            }
        }
        

        private List<TreeNode> _SelectedItems = new List<TreeNode>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<TreeNode> SelectedItems
        {
            get
            {
                return _SelectedItems;
            }
            set
            {
                _SelectedItems = value;
                if (OnSelectionChange != null)
                    OnSelectionChange(this,new EventArgs());
            }
        }

        private ImageList _imagelist = null;
        public ImageList imagelist
        {
            get
            {
                return _imagelist;
            }
            set
            {
                _imagelist = value;

            }
        }

        private SizeF _ButtonSize = new SizeF(80,80);
        public SizeF ButtonSize
        {
            get
            {
                return _ButtonSize;
            }
            set
            {
                _ButtonSize = value;
            }
        }

        /*
        private bool _AutoSizeButton = true;
        [DefaultValue(true)]
        public bool AutoSizeButton
        {
            get
            {
                return _AutoSizeButton;
            }
            set
            {
                _AutoSizeButton = value;
            }
        }
        */
        public event TreeNodeMouseClickEventHandler ClickNode;
        public event TreeNodeMouseHoverEventHandler HoverNode;

        const int HEADERWIDTH = 25;

        int LeftMargin = HEADERWIDTH + 5;
        int RightMargin = 5;
        int TopMargin = 5;
        int BottomMargin = 5;

        AbstractRenderer renderer = new StandardRenderer();
        Rectangle HeaderRect;

        PointF Offset = new PointF(0, 0);
     




        private BASButton m_HoveredNode = null;
       

        private TreeNode _m_CurrentNode = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TreeNode m_CurrentNode
        {
            get
            {
                return _m_CurrentNode == null ? Root : _m_CurrentNode;
            }
            
        }

        private TreeNode _Root = new TreeNode();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TreeNode Root
        {
            get
            {
                return _Root;
            }            
        }


        public List<BASButton> lstbuttons = new List<BASButton>();




        private BASButton ButtonAt(Point p_PtForButton)
        {
            foreach (BASButton b in lstbuttons)
            {
                if (b.Bound.Contains(p_PtForButton))
                    return b;
                
            }
            return null;
        }

        private bool HitAtHeader(Point p_Pt)
        {
            if (HeaderRect.Contains(p_Pt))
                return true;
            else
                return false;
        }


        public void AddToSelectionByTag(object obj)
        {
            foreach (TreeNode tn in m_CurrentNode.Nodes)
                if ((tn.Tag!=null)&&( tn.Tag.Equals(obj)))
                    if (!SelectedItems.Contains(tn)) SelectedItems.Add(tn);
        }

        public void SelectByTag(object obj)
        {

            if (MultiSelectMode)
            {
                AddToSelectionByTag(obj);
            }
            else
            {
                SelectedItems.Clear();
                AddToSelectionByTag(obj);
            }
        }

        protected void Click(MouseEventArgs e)
        {
            if (HitAtHeader(new Point(e.X, e.Y)))
            {
                if (m_CurrentNode.Parent != null)
                {
                    _m_CurrentNode = m_CurrentNode.Parent;
                    SelectedItems.Clear();
                    RecalculateBtns();
                    
                }
            }
            else
            {
                BASButton b = ButtonAt(new Point(e.X, e.Y));
                if ((b != null) && (((TreeNode)b.Tag).FirstNode != null))
                {
                    Offset = new PointF(0, 0);
                    _m_CurrentNode = ((TreeNode)b.Tag);
                    SelectedItems.Clear();

                    RecalculateBtns();

                }
                if ((b != null) && (((TreeNode)b.Tag).FirstNode == null))
                {

                    if (MultiSelectMode)
                    {
                        if (!SelectedItems.Contains(((TreeNode)b.Tag)))
                            SelectedItems.Add(((TreeNode)b.Tag));
                        else
                            SelectedItems.Remove(((TreeNode)b.Tag));
                    }
                    else
                    {

                        SelectedItems.Clear();
                        SelectedItems.Add(((TreeNode)b.Tag));
                    }
                    if (OnSelectionChange != null)
                        OnSelectionChange(this, new EventArgs());

                    if (ClickNode!=null)
                        ClickNode(this, new TreeNodeMouseClickEventArgs(((TreeNode)b.Tag), e.Button, e.Clicks, e.X, e.Y));
                }

                
            }
            
            Invalidate();
            base.OnMouseClick(e);

        }




        public SlidingList()
        {
            InitializeComponent();

            BigClickTimer.Interval = 1000;
            BigClickTimer.Tick += new EventHandler(BigClickTimer_Tick);

            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);

        }

        void BigClickTimer_Tick(object sender, EventArgs e)
        {

            BASButton btn = ButtonAt(new Point(longclickeventarg.X, longclickeventarg.Y));

            if (MultiSelectMode)
            {
                if (!SelectedItems.Contains((TreeNode)btn.Tag))
                    SelectedItems.Add((TreeNode)btn.Tag);
                else
                    SelectedItems.Remove((TreeNode)btn.Tag);
            }
            else
            {
                SelectedItems.Clear();
                if (btn != null)
                    SelectedItems.Add((TreeNode)btn.Tag);
            }
            BigClickTimer.Stop();

            if ((Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
            {
                if (OnLongClickEvent != null)
                    OnLongClickEvent(this, longclickeventarg);
            } 
            
        }



        


        private void RecalculateBtns()
        {
            lstbuttons.Clear();


            foreach (TreeNode tn in m_CurrentNode.Nodes)
            {
                BASButton b = new BASButton();
                b.Text = tn.Text;
                b.Tag = tn;
                b.Color = tn.BackColor;
                lstbuttons.Add(b);
            }



        }



        protected override void OnResize(EventArgs e)
        {
            Invalidate();
        }

        float maxheight = 0;
        float maxwidth = 0;


        SizeF btnsz = new SizeF(80,80);
        private void RecalculatePos(Graphics g)
        {
            
            HeaderRect.X = 0;
            HeaderRect.Y = 0;
            HeaderRect.Height = Height;
            HeaderRect.Width = HEADERWIDTH;

            btnsz = ButtonSize;

          
            float y = TopMargin;
            float x = LeftMargin;
            int counter = 1;
            foreach (BASButton btn in lstbuttons)
            {
                //SizeF sz = btn.MeasureItem(g);
                
                btn.Bound = new RectangleF(x + Offset.X, y + Offset.Y, btnsz.Width, btnsz.Height);


                x += btnsz.Width + RightMargin;

                if (WrapMode)
                {
                    if (x + btnsz.Width > Width) { x = LeftMargin; y += btnsz.Height + TopMargin; ;}
                }
                else
                {

                }


               

                counter++;
            }

            foreach (BASButton btn in lstbuttons)
            {
                if (btn.Bound.Right - Offset.X > maxwidth) maxwidth = btn.Bound.Right - Offset.X;
                if (btn.Bound.Bottom - Offset.Y > maxheight) maxheight = btn.Bound.Bottom - Offset.Y;
            }
 
        }


        PointF DownAt;
        PointF OffsetWhenDown;
        BASButton DownAtBtn = null;

        protected override void OnMouseUp(MouseEventArgs e)
        {
            BigClickTimer.Stop();
            if ((Math.Abs(DownAt.X - e.X) < 5) && (Math.Abs(DownAt.Y - e.Y) < 5))
            {
                Click(e);
            }
            DownAtBtn = null;

        }
        MouseEventArgs longclickeventarg;
        protected override void OnMouseDown(MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {

                DownAtBtn = ButtonAt(new Point(e.X, e.Y));

                if (DownAtBtn != null)
                {
                    longclickeventarg = e;
                    BigClickTimer.Start();
                }
                DownAt = new PointF(e.X, e.Y);
                OffsetWhenDown = new PointF(Offset.X,Offset.Y);

                Invalidate();

                base.OnMouseDown(e);
            }
        }

        
        private void DrawHeader(PaintEventArgs e)
        {
            
            renderer.DrawHeader(e.Graphics, HeaderRect, m_CurrentNode,false,false);


        }


        protected override void OnMouseMove(MouseEventArgs e)
        {

            BASButton bb = ButtonAt(new Point( e.X, e.Y));
            if ((bb!=null) && ((m_HoveredNode==null)||(bb != m_HoveredNode)))
            {
                m_HoveredNode = bb;
                if (HoverNode!=null)
                    HoverNode(this, new TreeNodeMouseHoverEventArgs(((TreeNode)m_HoveredNode.Tag)));
                Invalidate();
            }
            if (e.Button == MouseButtons.Left)
            {
                BigClickTimer.Enabled = false;
                float x = OffsetWhenDown.X + (e.X - DownAt.X);
                float y = OffsetWhenDown.Y + (e.Y - DownAt.Y);

                //float minx = Width - maxX - RightMargin - btnsz.Width;
                //float miny = Height - maxY - BottomMargin - btnsz.Height;

                if (x < (Width - maxwidth))
                    x = (Width - maxwidth);
                if (y < (Height - maxheight))
                    y = (Height - maxheight);
               
                
                if (x > 0) x = 0;
                if (y > 0) y = 0;
                

                Offset = new PointF(x,y);

                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            RecalculatePos(e.Graphics);

            foreach (BASButton btn in lstbuttons)
                btn.Draw(renderer, e.Graphics, SelectedItems.Contains(((TreeNode)btn.Tag)), btn == m_HoveredNode, btn == DownAtBtn);

            DrawHeader(e);

            base.OnPaint(e);


        }

        public void LoadFromFamilyValueList(List<FamilyValue> lst)
        {
           
           List<string> familles = new List<string>();
            foreach (FamilyValue fv in lst)
            {
                familles.Add(fv.Familly);
            }

            Root.Nodes.Clear();
            TreeTools.BuildTreeFrom(Root, familles, FamilyValue.famillyseparator,true);
            

            foreach (FamilyValue fv in lst)
            {
                TreeNode t = TreeTools.GetNode(Root, fv.Familly, FamilyValue.famillyseparator);
                TreeNode tnvue = new TreeNode(fv.Value);
                tnvue.Tag = fv.Tag;
                tnvue.BackColor = fv.color;
                t.Nodes.Add(tnvue);
            }


            RecalculateBtns();
            Invalidate();
        }

        public void LoadFromNode(TreeNode tn)
        {


            _Root = tn;


            RecalculateBtns();
            Invalidate();
        }


        private void ucTreeListBtn_Load(object sender, EventArgs e)
        {
            renderer.imgList = imagelist;
            Invalidate();
        }

        public IEnumerable<ListViewItem> CheckedItems { get; set; }
    }

    public class FamilyValue
    {

        private Color _color;
        public Color color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        private object _Tag;
        public object Tag
        {
            get
            {
                return _Tag;
            }
            set
            {
                _Tag = value;
            }
        }


        private string _Organisation;
        public string Organisation
        {
            get
            {
                return _Organisation;
            }
            set
            {
                _Organisation =value;
            }
        }

        public FamilyValue(string famille, string value, object Tag,Color color)
        {
            this.Familly = famille;
            this.Value = value;
            this.Tag = Tag;
            this.color = color;
        }

        public FamilyValue(string famille, string value, object Tag)
        {
            this.Familly = famille;
            this.Value = value;
            this.Tag = Tag;
            this.color = Color.Gray;
        }

        public FamilyValue(string Organisation)
        {
            // TODO: Complete member initialization
            this.Organisation = Organisation;
        }

        private static char _famillyseparator = '/';
        public static char famillyseparator
        {
            get
            {
                return _famillyseparator;
            }
            set
            {
                _famillyseparator = value;
            }
        }

        private string _Value;
        public string Value
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

        private string _Familly;
        private string p;
        public string Familly
        {
            get
            {
                return _Familly;
            }
            set
            {
                _Familly = value;
            }
        }
    }


    internal class NodeSorter : IComparer<TreeNode>
    {
        public int Compare(TreeNode x, TreeNode y)
        {
            TreeNode tx = x;
            TreeNode ty = y;


            return tx.Text.CompareTo(ty.Text);
        }
    }

    internal static class TreeTools
    {
        public static TreeNode GetNode(TreeNode nde, string txt, char separator)
        {

            if (txt == "") return nde;
            TreeNode result = null;

            if (nde.Text == "")
            {
                foreach (TreeNode subt in nde.Nodes)
                {
                    TreeNode res = GetNode(subt, txt, separator);
                    if (res != null)
                        return res;
                }
                return result == null ? nde : result;
            }

            string[] ss = txt.Split(separator);
            if (ss[nde.Level - 1] == nde.Text)
            {
                if (ss.Length > nde.Level)
                {
                    foreach (TreeNode subt in nde.Nodes)
                    {
                        TreeNode res = GetNode(subt, txt, separator);
                        if (res != null)
                            return res;
                    }
                }
                else
                    return nde;
            }

            return result;
        }

        public static void BuildFromTree(TreeNode nde, ref string txt, char separator)
        {
            if (nde.Text == "") return;
            if (txt != "") txt = separator + txt;
            txt = nde.Text + txt;
            if (nde.Parent != null)
                BuildFromTree(nde.Parent, ref txt, separator);
        }

        
        public static void BuildTreeFrom(TreeNode nde, List<string> categories, char separator,bool sortnode)
        {
            foreach (string s in categories)
            {
                if (s == "") continue;
                string[] ss = s.Split(separator);
                if (ss.Length <= nde.Level) continue;
                string cat = ss[nde.Level];


                string rebuilt = "";
                BuildFromTree(nde, ref rebuilt, separator);

                bool alreadyadded = false;
                foreach (TreeNode t in nde.Nodes)
                    if (t.Text == cat)
                    {
                        alreadyadded = true;
                        break;
                    }

                if (!alreadyadded)
                {
                    if (s.StartsWith(rebuilt))
                    {
                        TreeNode subnde = nde.Nodes.Add(cat);
                        BuildTreeFrom(subnde, categories, separator, sortnode);
                    }

                }
            }

            if (sortnode)
            {
                List<TreeNode> lst = new List<TreeNode>();
                foreach (TreeNode tn in nde.Nodes)
                    lst.Add(tn);

                lst.Sort(new NodeSorter());

                nde.Nodes.Clear();
                foreach (TreeNode tn in lst)
                    nde.Nodes.Add(tn);
            }

        }

    }
}
