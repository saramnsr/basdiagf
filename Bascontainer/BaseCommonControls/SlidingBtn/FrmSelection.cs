using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseCommonControls
{
    public partial class FrmSelection : Form
    {


       

        private TreeNode _value;
        public TreeNode value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public FrmSelection(TreeNode tn)
        {
            InitializeComponent();

            ucTreeListBtn1.LoadFromNode(tn);
            ucTreeListBtn1.ClickNode += new TreeNodeMouseClickEventHandler(ucTreeListBtn1_ClickNode);

                        
        }

        void ucTreeListBtn1_ClickNode(object sender, TreeNodeMouseClickEventArgs e)
        {
            value = e.Node;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FrmSelection_Deactivate(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FrmSelection_Shown(object sender, EventArgs e)
        {
            Focus();
        }
    }
}
