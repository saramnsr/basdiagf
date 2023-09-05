using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using BasCommon_BO;
using System.Windows.Forms;
using System.Drawing;

namespace BaseCommonControls.documentsToPrint
{
    public class ListControlVirement : PrintDocument
    {
        DataGridView _dgv;
        List<Virement> lstVir = new List<Virement>();
        //double total = 0;

        Font ftTitle = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
        Font ft = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point);
        DataGridViewPrinter prntr;

        public ListControlVirement(List<Virement> lst)
        {
            lstVir = lst;
            _dgv = InitDgv();
            DataGridViewPrinter dgvPrint = new DataGridViewPrinter(_dgv, this, true, false, "", ft, Color.Black, true);
            prntr = dgvPrint;
        }

        protected override void OnQueryPageSettings(QueryPageSettingsEventArgs e)
        {

            base.OnQueryPageSettings(e);
            e.PageSettings.Margins = new Margins(0, 0, 0, 0);
            _dgv.Width = e.PageSettings.Bounds.Width;

        }

        float ConvertmmToPxl(float En_mm, PrinterResolution res)
        {
            float Inch = 25.4f;
            return (res.X / Inch) * En_mm;

        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            Font ftbold = new Font("Garamond", 16, FontStyle.Bold, GraphicsUnit.Point);

            int TITLEHEIGHT = 50;

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            Rectangle titleRect = new Rectangle(0, 0, e.PageBounds.Width, TITLEHEIGHT);
            string Title = "Liste de contrôle des virements";
            e.Graphics.DrawString(Title, ftbold, Brushes.Black, titleRect, sf);
            int y = titleRect.Bottom;

            e.HasMorePages = DrawLignes(ref y, e);


        }

        private DataGridView InitDgv()
        {

            Font Headerft = new Font("Arial", 11, FontStyle.Bold, GraphicsUnit.Point);

            DataGridViewCellStyle dgvcstyle = new DataGridViewCellStyle();
            dgvcstyle.Font = new Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Point);
            dgvcstyle.WrapMode = DataGridViewTriState.False;
            dgvcstyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            DataGridView dgv = new DataGridView();
            dgv.ColumnHeadersDefaultCellStyle.Font = Headerft;
            dgv.DefaultCellStyle = dgvcstyle;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;

            DataGridViewColumn dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Montant";
            dgvcol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvcol.DefaultCellStyle.Format = "C2";
            dgvcol.Width = 80;
            dgv.Columns.Add(dgvcol);

            dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Date";
            dgvcol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvcol.Width = 70;
            dgvcol.DefaultCellStyle.Format = "d";
            dgv.Columns.Add(dgvcol);

            dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Patient";
            dgvcol.Width = 200;
            dgv.Columns.Add(dgvcol);

            dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Banque de remise";
            dgvcol.Width = 200;
            dgv.Columns.Add(dgvcol);

            dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Entité";
            dgvcol.Width = 200;
            dgv.Columns.Add(dgvcol);


            foreach (Virement v in lstVir)
            {
                object[] o = new object[]{
                    v.echeance.Montant.ToString("C2"),
                    v.echeance.DateEcheance,
                    v.echeance.patient.ToString(),
                    v.comptecabinet,
                    v.Entite
                };

                dgv.Rows.Add(o);
            }

            return dgv;
        }

        bool DrawLignes(ref int y, PrintPageEventArgs e)
        {

            prntr.TopMargin = y;
            prntr.BottomMargin = y;
            bool hasMorePages = prntr.DrawDataGridView(e.Graphics);

            y = (int)prntr.currenty;

            return hasMorePages;

        }


    }
}
