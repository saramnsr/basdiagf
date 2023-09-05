using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using BasCommon_BO;
using BasCommon_BL;
using System.Windows.Forms;

namespace BaseCommonControls.documentsToPrint
{
    public class ListControleCB : PrintDocument
    {

     

        DataGridView dgv;


        private BordereauFinance _bordereau;
        public BordereauFinance bordereau
        {
            get
            {
                return _bordereau;
            }   
            set
            {
                _bordereau = value;
            }
        }

        public ListControleCB(BordereauFinance bordereau)
        {
            this.bordereau = bordereau;
            dgv = InitDgv();
        }

      
        protected override void OnQueryPageSettings(QueryPageSettingsEventArgs e)
        {

            base.OnQueryPageSettings(e);
            e.PageSettings.Margins = new Margins(0, 0, 0, 0);
            dgv.Width = e.PageSettings.Bounds.Width;

        }


        float ConvertmmToPxl(float En_mm, PrinterResolution res)
        {
            float Inch = 25.4f;
            return (res.X / Inch) * En_mm;

        }


        void DrawEmplacementAgraffe(ref int y, PrintPageEventArgs e)
        {

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            


            Font ft = new Font("Garamond", 10, FontStyle.Regular, GraphicsUnit.Point);

            //float w = ConvertmmToPxl(60.0f, e.PageSettings.PrinterResolution);
            //float h = ConvertmmToPxl(85.0f, e.PageSettings.PrinterResolution);

            float w = 227;
            float h = 321;

            RectangleF emplacement2emeTickets = new RectangleF(0, y, w, h);
            RectangleF emplacement3emeTickets = new RectangleF(e.PageBounds.Width - w, y, w, h);

            e.Graphics.DrawRectangle(Pens.Black,emplacement2emeTickets.X,emplacement2emeTickets.Y,emplacement2emeTickets.Width,emplacement2emeTickets.Height);                       
            e.Graphics.DrawRectangle(Pens.Black, emplacement3emeTickets.X, emplacement2emeTickets.Y, emplacement2emeTickets.Width, emplacement2emeTickets.Height);

            PointF txtpt = new PointF(emplacement2emeTickets.X,emplacement2emeTickets.Y-15);
            e.Graphics.DrawString("TICKETS RECAPITULATIF", ft, Brushes.Black, txtpt);

            txtpt = new PointF(emplacement3emeTickets.X, emplacement3emeTickets.Y - 15);
            e.Graphics.DrawString("TICKETS RECEPTIONNES", ft, Brushes.Black, txtpt);


            e.Graphics.DrawString("A agrafer ici", ft, Brushes.Black, emplacement2emeTickets,sf);

            e.Graphics.DrawString("Sur chaque ticket doit figurer le nom du patient", ft, Brushes.Black, emplacement3emeTickets,sf);
            y = (int)Math.Max(emplacement2emeTickets.Bottom,emplacement3emeTickets.Bottom);
        }

        void DrawTotalTicket(ref int y, PrintPageEventArgs e)
        {

            Font ft = new Font("Garamond", 12, FontStyle.Regular, GraphicsUnit.Point);


            int TEXTHEIGHT = 70;
            int TEXTWIDTH = e.PageBounds.Width / 3;


            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;


            string Text = "Ticket récapitulatif correspondant au montant total de  : " + bordereau.Montant.ToString("C2");


            Rectangle Rect = new Rectangle((e.PageBounds.Width - TEXTWIDTH) / 2, y, TEXTWIDTH, TEXTHEIGHT);

            e.Graphics.DrawString(Text, ft, Brushes.Black, Rect, sf);

            y = Rect.Bottom;
        }


        void DrawNbTicket(ref int y, PrintPageEventArgs e)
        {

            Font ft = new Font("Garamond", 12, FontStyle.Regular, GraphicsUnit.Point);


            int TEXTHEIGHT = 70;
            int TEXTWIDTH = e.PageBounds.Width / 3;


            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            string Text = "Le nombre de tickets est de : " + bordereau.paiements.Count.ToString();


            Rectangle Rect = new Rectangle((e.PageBounds.Width - TEXTWIDTH) / 2, y, TEXTWIDTH, TEXTHEIGHT);

            e.Graphics.DrawString(Text, ft, Brushes.Black, Rect, sf);

            y = Rect.Bottom;
        }

        void DrawNumControl(ref int y, PrintPageEventArgs e)
        {

            Font ft = new Font("Garamond", 12, FontStyle.Regular, GraphicsUnit.Point);


            int TEXTHEIGHT = 70;
            int TEXTWIDTH = e.PageBounds.Width / 3;


            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            //string Text = "Numéro de bordereau : " + bordereau.NumBordereau;
            string Text = "Numéro bancaire : " + bordereau.NumBordereauBancaire;


            Rectangle Rect = new Rectangle( (e.PageBounds.Width - TEXTWIDTH) / 2,y, TEXTWIDTH, TEXTHEIGHT);

            e.Graphics.DrawString(Text, ft, Brushes.Black, Rect, sf);

            y = Rect.Bottom;
        }


        DataGridViewPrinter prntr = null;
        bool DrawLignes(ref int y, PrintPageEventArgs e)
        {

            

            Font ftTitle = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
            Font ft = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point);




            if (prntr==null)
                prntr = new DataGridViewPrinter(dgv, (PrintDocument)this, true, false, "", ft, Color.Black, false);
            prntr.TopMargin = y;
            prntr.BottomMargin = 20;
            bool mustcontinue = prntr.DrawDataGridView(e.Graphics);
            y = (int)prntr.currenty;

            return mustcontinue;
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
            dgvcol.HeaderText = "Patient";
            dgvcol.Width = 200;
            dgv.Columns.Add(dgvcol);
            
            dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Payeur";
            dgvcol.Width = 200;
            dgv.Columns.Add(dgvcol);
            
            dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Date";
            dgvcol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvcol.Width = 70;
            dgvcol.DefaultCellStyle.Format = "d";
            dgv.Columns.Add(dgvcol);

            dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Montant";
            dgvcol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvcol.DefaultCellStyle.Format = "C2";
            dgvcol.Width = 80;
            dgv.Columns.Add(dgvcol);

            dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Banque";
            dgvcol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvcol.Width = 100;
            dgv.Columns.Add(dgvcol);

            dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Entité";
            dgvcol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvcol.Width = 100;
            dgv.Columns.Add(dgvcol);


            foreach (PaiementReel pr in bordereau.paiements)
            {
                object[] o = new object[]{
                    pr.Patients,
                    pr.payeur,
                    pr.DateEncaissement,
                    pr.Montant.ToString("C2"),
                    pr.BanqueDeRemise.Libelle,
                    pr.EntiteJuridique.Nom
                    
                };

                dgv.Rows.Add(o);

            }


            return dgv;
        }


        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            Font ft = new Font("Garamond", 12, FontStyle.Regular, GraphicsUnit.Point);
            Font ftbold = new Font("Garamond", 16, FontStyle.Bold, GraphicsUnit.Point);
            
            int TITLEHEIGHT = 50;
            int SUBTITLEHEIGHT = 50;
            int DESCHEIGHT = 150;


            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            StringFormat sfdesc = new StringFormat();
            sfdesc.Alignment = StringAlignment.Near;
            sfdesc.LineAlignment = StringAlignment.Center;
            string Title = "";
            string SubTitle = "";
            string SubDesc = "Cette liste permet de contrôler :\n-	Que tous les 2e tickets ont bien été enregistrés dans Bas Practice\n-	Que toutes les lignes dans Bas Practice ont un 2e ticket\n-	Que les informations du 3e ticket ne comportent pas d’erreurs (que la télétransmission s’est bien effectuée)";

            
                Title = "Liste de contrôle des 2eme et 3eme tickets";
                SubTitle = "";
           

            int y=0;
            Rectangle titleRect = new Rectangle(0,y,e.PageBounds.Width,TITLEHEIGHT);
            y = titleRect.Bottom;
            Rectangle SubTitleRect = new Rectangle(0,y,e.PageBounds.Width,SUBTITLEHEIGHT);
            y = SubTitleRect.Bottom;
            Rectangle DescRect = new Rectangle(0, y, e.PageBounds.Width, DESCHEIGHT);
            y = DescRect.Bottom;

            e.Graphics.DrawString(Title, ftbold, Brushes.Black, titleRect, sf);
            e.Graphics.DrawString(SubTitle, ft, Brushes.Black, SubTitleRect, sf);
            e.Graphics.DrawString(SubDesc, ft, Brushes.Black, DescRect, sfdesc);


            int yemplticket = y;
            DrawEmplacementAgraffe(ref y, e);
            //y = e.PageBounds.Height / 2;

            y += 20;

            e.HasMorePages = DrawLignes(ref y,e);
            y += 20;

            string Text = "";

            Text += "      Montant TOTAL : " + bordereau.Montant.ToString("C2");
            Rectangle footerRect = new Rectangle(0, y, e.PageBounds.Width, 25);

            e.Graphics.DrawString(Text, ft, Brushes.Black, footerRect, sf);


            y = yemplticket;
            DrawNumControl(ref y, e);
            y += 20;
            DrawNbTicket(ref y, e);
            y += 20;
            DrawTotalTicket(ref y, e);
            y += 20;
            

        }

      
    }
}
