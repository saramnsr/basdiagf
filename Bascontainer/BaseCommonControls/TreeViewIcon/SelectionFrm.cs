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
    public partial class SelectionFrm : Form
    {

        private int _NbVisibleItems;
        public int NbVisibleItems
        {
            get
            {
                return _NbVisibleItems;
            }
            set
            {
                _NbVisibleItems = value;
            }
        }


        public Object Selection
        {
            get
            {
                return lstBxChoices.SelectedItem;
            }
            
        }

        public event EventHandler Selected;
        public event EventHandler Canceled;

        public SelectionFrm(int nbVisibleItems)
        {
            NbVisibleItems = nbVisibleItems;
            InitializeComponent();

            lstBxChoices.HorizontalScrollbar = false;

        }

        private void SelectionFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void lstBxChoices_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void SelectionFrm_Leave(object sender, EventArgs e)
        {
            
        }

        private void SelectionFrm_Deactivate(object sender, EventArgs e)
        {
            if (Canceled != null) Canceled(this, new EventArgs());
            Close();
        }

        private void lstBxChoices_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            if (e.Index > -1)
            {
                string s = lstBxChoices.Items[e.Index].ToString();
                e.Graphics.DrawString(s, e.Font, new SolidBrush(e.ForeColor), e.Bounds, sf);
            }
        }

        private void lstBxChoices_Click(object sender, EventArgs e)
        {
            if (Selected != null) Selected(this, new EventArgs());
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lstBxChoices.SelectedIndex - NbVisibleItems > 0)
                lstBxChoices.SelectedIndex -= NbVisibleItems;
            else
                lstBxChoices.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lstBxChoices.SelectedIndex + NbVisibleItems < lstBxChoices.Items.Count)
                lstBxChoices.SelectedIndex += NbVisibleItems;
            else
                lstBxChoices.SelectedIndex = lstBxChoices.Items.Count-1;
        }

        private void SelectionFrm_Load(object sender, EventArgs e)
        {
            
        }

        private void lstBxChoices_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
