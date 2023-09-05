using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlsLibrary
{
    public partial class TreeViewIconCbx : UserControl
    {


        public event EventHandler SelectedIndexChanged;

        public int SelectedIndex
        {
            get
            {
                return SelectFrm.lstBxChoices.SelectedIndex;
            }
            set
            {
                if ((value < SelectFrm.lstBxChoices.Items.Count) && (value >= 0))
                    SelectFrm.lstBxChoices.SelectedIndex = value;
            }
            
        }

        public Object SelectedItem
        {
            get
            {
                return SelectFrm.lstBxChoices.SelectedItem;
            }

            set
            {
                SelectFrm.lstBxChoices.SelectedItem = value;
            }
            
        }

        public ListBox.ObjectCollection Items
        {
            get
            {
                return SelectFrm.lstBxChoices.Items;
            }
            
        }

        private Font _Font;
        public Font ft
        {
            get
            {
                return _Font;
            }
            set
            {
                _Font = value;
                SelectFrm.lstBxChoices.Font = _Font;
            }
        }

        private int _VisibleItems = 5;
        public int VisibleItems 
        {
            get
            {
                return _VisibleItems;
            }
            set
            {
                _VisibleItems = value;
                SelectFrm.NbVisibleItems = value;
            }
        }

        private SelectionFrm SelectFrm;
        

        public TreeViewIconCbx()
        {
            InitializeComponent();

            SelectFrm = new SelectionFrm(VisibleItems);
            SelectFrm.Visible = false;
            

            ft = new Font("Garamond", 12, FontStyle.Regular);

            SelectFrm.Selected += new EventHandler(SelectFrm_Selected);

            
        }

        void SelectFrm_Selected(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null) SelectedIndexChanged(this, new EventArgs());
            Invalidate();
        }


        protected override void OnClick(EventArgs e)
        {
            ShowSelector();
            base.OnClick(e);
        }

        private void ShowSelector()
        {
            SelectFrm.lstBxChoices.ItemHeight = this.Height;



            SelectFrm.Height = (SelectFrm.lstBxChoices.ItemHeight * (VisibleItems + 2)) + 3;



            SelectFrm.Top = this.PointToScreen(new Point(0, 0)).Y - (SelectFrm.lstBxChoices.ItemHeight * ((int)(VisibleItems / 2) + 1));
            SelectFrm.Left = this.PointToScreen(new Point(0, 0)).X;
           // SelectFrm.Height = SelectFrm.lstBxChoices.ItemHeight * VISIBLEITEMS + (SelectFrm.lstBxChoices.ItemHeight / 2);
            SelectFrm.Width = this.Width;


            SelectFrm.lstBxChoices.Top = SelectFrm.lstBxChoices.ItemHeight;
            SelectFrm.lstBxChoices.Height = SelectFrm.lstBxChoices.ItemHeight * VisibleItems + (SelectFrm.lstBxChoices.ItemHeight / 2);


            SelectFrm.btnHaut.Top = 0;
            SelectFrm.btnHaut.Height = SelectFrm.lstBxChoices.ItemHeight;

            SelectFrm.btnBas.Top = (SelectFrm.lstBxChoices.ItemHeight * (VisibleItems + 1));
            SelectFrm.btnBas.Height = SelectFrm.lstBxChoices.ItemHeight;

            if (SelectFrm.Top < 0) SelectFrm.Top = 0;
            if (SelectFrm.Left < 0) SelectFrm.Left = 0;
            if (SelectFrm.Bottom > Screen.GetBounds(this).Bottom) SelectFrm.Top -= (SelectFrm.Bottom - Screen.GetBounds(this).Bottom);
            if (SelectFrm.Right > Screen.GetBounds(this).Right) SelectFrm.Left -= (SelectFrm.Right - Screen.GetBounds(this).Right);
            

            SelectFrm.Show();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Enabled)
            {

                e.Graphics.FillRectangle(Brushes.White, new RectangleF(0, 0, Width, Height));
                e.Graphics.DrawRectangle(Pens.Gray, new Rectangle(0, 0, Width - 1, Height - 1));

                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                string s = SelectFrm.lstBxChoices.SelectedItem == null ? "" : SelectFrm.lstBxChoices.SelectedItem.ToString();
                e.Graphics.DrawString(s, ft, new SolidBrush(ForeColor), new RectangleF(0, 0, Width, Height), sf);
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.WhiteSmoke, new RectangleF(0, 0, Width, Height));
                e.Graphics.DrawRectangle(Pens.LightGray, new Rectangle(0, 0, Width - 1, Height - 1));

                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                string s = SelectFrm.lstBxChoices.SelectedItem == null ? "" : SelectFrm.lstBxChoices.SelectedItem.ToString();
                e.Graphics.DrawString(s, ft, new SolidBrush(Color.LightGray), new RectangleF(0, 0, Width, Height), sf);

            }
        }

        private void TreeViewIconCbx_Load(object sender, EventArgs e)
        {

        }
    }
}
