using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Windows.Forms;
using BasCommon_BO;
using System.Drawing;

namespace BaseCommonControls.documentsToPrint
{
    public class ListControlPrelevement : PrintDocument
    {
        DataGridView _dgv;
        List<GroupedPrelevement> lstGpPrel = new List<GroupedPrelevement>();
        double total = 0;

        Font ftTitle = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
        Font ft = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point);
        DataGridViewPrinter prntr;

        public ListControlPrelevement(List<GroupedPrelevement> lst)
        {
            lstGpPrel = lst;
            _dgv = InitDgv();
            DataGridViewPrinter dgvPrint = new DataGridViewPrinter(_dgv, this, true, false, "", ft, Color.Black, true);
            prntr = dgvPrint;
        }

        protected override void OnQueryPageSettings(QueryPageSettingsEventArgs e)
        {

            base.OnQueryPageSettings(e);
            e.PageSettings.Margins = new Margins(0, 0, 0, 0);

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
            string Title = "Liste de contrôle des prélèvements";
            e.Graphics.DrawString(Title, ftbold, Brushes.Black, titleRect, sf);
            int y = titleRect.Bottom;

            e.HasMorePages = DrawLignes(ref y, e);
            y += 20;
            DrawTotal(ref y, e);
            y += 20;

        }

        private DataGridView InitDgv()
        {

            Font Headerft = new Font("Arial", 11, FontStyle.Bold, GraphicsUnit.Point);

            DataGridViewCellStyle dgvcstyle = new DataGridViewCellStyle();
            dgvcstyle.Font = new Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Point);
            dgvcstyle.WrapMode = DataGridViewTriState.False;
            dgvcstyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            DataGridView dgv = new DataGridView();
            dgv.Width = 600;
            dgv.ColumnHeadersDefaultCellStyle.Font = Headerft;
            dgv.DefaultCellStyle = dgvcstyle;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;

            DataGridViewColumn dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Patient";
            dgvcol.Width = 116;
            dgv.Columns.Add(dgvcol);

            dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Traitement";
            dgvcol.Width = 496;
            dgv.Columns.Add(dgvcol);

            dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Total";
            dgvcol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvcol.DefaultCellStyle.Format = "C2";
            dgvcol.Width = 100;
            dgv.Columns.Add(dgvcol);

            dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Montant par prélèvement";
            dgvcol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvcol.DefaultCellStyle.Format = "C2";
            dgvcol.Width = 100;
            dgv.Columns.Add(dgvcol);

            dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Date";
            dgvcol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvcol.Width = 100;
            dgvcol.DefaultCellStyle.Format = "d";
            dgv.Columns.Add(dgvcol);

            dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Nb prélèvements";
            dgvcol.Width = 60;
            dgv.Columns.Add(dgvcol);

            dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Jours prélèvements";
            dgvcol.Width = 80;
            dgv.Columns.Add(dgvcol);


            foreach (GroupedPrelevement gp in lstGpPrel)
            {
                object[] o = new object[]{
                    gp.Patient,
                    gp.traitement,
                    gp.TotalDue,
                    gp.MontantParPrelevement,
                    gp.DatePremierPrelevement,
                    gp.Montants.Count,
                    gp.EcheanceDay
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

        void DrawTotal(ref int y, PrintPageEventArgs e)
        {

            Font ft = new Font("Garamond", 12, FontStyle.Regular, GraphicsUnit.Point);


            int TEXTHEIGHT = e.PageBounds.Height - 20;
            int TEXTWIDTH = e.PageBounds.Width / 3;


            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;

            string Text = "Montant total : " + total.ToString("C2");


            Rectangle Rect = new Rectangle((e.PageBounds.Width - TEXTWIDTH) / 2, y, TEXTWIDTH, TEXTHEIGHT);

            e.Graphics.DrawString(Text, ft, Brushes.Black, Rect, sf);

            y = Rect.Bottom;
        }
    }
}
