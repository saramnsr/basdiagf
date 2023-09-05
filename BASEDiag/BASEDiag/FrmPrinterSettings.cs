using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BO;

namespace BASEDiag
{
    public partial class FrmPrinterSettings : Form
    {
        public FrmPrinterSettings()
        {
            InitializeComponent();
        }

        private void FrmPrinterSettings_Load(object sender, EventArgs e)
        {

            RefreshValues();
        }

        private void RefreshValues()
        {
            listView1.Items.Clear();
            foreach (BasePrinterSetting s in BASEDiag_BL.PrinterSettingsMgmt.printsettings)
            {
                ListViewItem itm = new ListViewItem();
                itm.Text = s.Libelle;
                itm.Tag = s;
                itm.SubItems.Add(s.Descriptif);
                listView1.Items.Add(itm);
            }
        }

        

        private void btnOk_Click(object sender, EventArgs e)
        {
            BASEDiag_BL.PrinterSettingsMgmt.SavePrintSettings();
            Close();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FrmPrinterSetting frm = new FrmPrinterSetting();
            frm.printersetting = (BasePrinterSetting)(listView1.SelectedItems[0].Tag);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                RefreshValues();
            }
        }

       
    }
}
