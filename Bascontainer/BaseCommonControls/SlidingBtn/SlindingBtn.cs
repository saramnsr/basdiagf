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
    public partial class SlindingBtn : Button
    {

        public event MouseEventHandler OnLongClickEvent;
        public event EventHandler OnValueChanged;


        public event CancelEventHandler OnDropDown;


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

        protected int _WindowWidth = 400;
        public int WindowWidth
        {
            get
            {
                return _WindowWidth;
            }
            set
            {
                _WindowWidth = value;
            }
        }


        protected int _WindowHeight = -1;
        public int WindowHeight
        {
            get
            {
                return _WindowHeight;
            }
            set
            {
                _WindowHeight = value;
            }
        }


        private TreeNode _SelectedNode;
        public TreeNode SelectedNode
        {
            get
            {
                return _SelectedNode;
            }
            set
            {
                _SelectedNode = value;
                if (value != null)
                {
                    this.Text = value.Text;
                    if (ImageList != null)
                    {
                        this.ImageIndex = value.ImageIndex;
                        TextAlign = ContentAlignment.BottomCenter;
                        ImageAlign = ContentAlignment.MiddleCenter;
                    }
                    else
                    {
                        TextAlign = ContentAlignment.MiddleCenter;
                    }

                    if (OnValueChanged != null)
                        OnValueChanged(this, new EventArgs());
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedTag
        {
            get
            {
                return _SelectedNode == null ? null : _SelectedNode.Tag;
            }
            set
            {

                SelectedNode = FindNodeByTag(Root.Nodes, value);
            }
        }

        private TreeNode FindNodeByTag(TreeNodeCollection tnc, object obj)
        {
            foreach (TreeNode tn in tnc)
                if ((tn.Tag!=null) &&(tn.Tag.Equals(obj)))
                    return tn;
                else
                    if (tn.Nodes.Count > 0)
                    {
                        TreeNode tn2 = FindNodeByTag(tn.Nodes, obj);
                        if (tn2 != null) return tn2; 
                    }

            return null;
        }

        private TreeNode _Root = new TreeNode();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TreeNode Root
        {
            get
            {
                return _Root;
            }
            set
            {
                Root = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TreeNodeCollection Nodes
        {
            get
            {
                return _Root.Nodes;
            }
            
        }

        public SlindingBtn()
        {
            InitializeComponent();
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
                t.Nodes.Add(tnvue);
            }


            Invalidate();
        }

        public void LoadFromNode(TreeNode tn)
        {
            _Root = tn;
        }

        public void AddItem(TreeNode tn)
        {

            _Root.Nodes.Add(tn);            

        }
        

        protected override void OnClick(EventArgs e)
        {

            CancelEventArgs ec = new CancelEventArgs();
            ec.Cancel = false;
            if (OnDropDown != null)
                OnDropDown(this, ec);


            if (ec.Cancel) return;


            FrmSelection frm = new FrmSelection(Root);

            frm.ucTreeListBtn1.imagelist = ImageList;

            frm.ucTreeListBtn1.ButtonSize = new SizeF(80,67);

            frm.ucTreeListBtn1.WrapMode = WrapMode;

            frm.ucTreeListBtn1.OnLongClickEvent += OnLongClickEvent;

            Point pt = this.PointToScreen(new Point(0, 0));


            Rectangle screenrect = Screen.GetWorkingArea(this);

            int h = WindowHeight < 1 ? (int)frm.ucTreeListBtn1.ButtonSize.Height+5 : WindowHeight;

            int x = pt.X - ((WindowWidth - Width) / 2);
            if (x < screenrect.Left) x = screenrect.Left;
            if (x + WindowWidth > screenrect.Right) x = screenrect.Right - WindowWidth;
            int y = pt.Y - ((h - Height) / 2);
            if (y + h > screenrect.Bottom) x = screenrect.Bottom - h;
            
            frm.Bounds = new Rectangle(x, y, WindowWidth, h);
            frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
            frm.Owner = this.FindForm();
            frm.Show(); 
            
            base.OnClick(e);
        }

        void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (((FrmSelection)sender).value != null)
            {
                 SelectedNode = ((FrmSelection)sender).value;
            }
        }
    }
}
