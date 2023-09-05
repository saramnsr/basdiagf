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
    public class BordereauDeRemise : PrintDocument
    {

        public enum TypeBordereau
        {
            Especes,
            Cheques,
            Auto
        }

        DataGridView dgv;



        private TypeBordereau _tpe = TypeBordereau.Cheques;
        public TypeBordereau tpe
        {
            get
            {
                return _tpe;
            }
            set
            {
                _tpe = value;
            }
        }

        private BordereauFinance _bordereau;
        public BordereauFinance Currentbordereau
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

        public BordereauDeRemise(BordereauFinance bordereau)
        {
            Currentbordereau = bordereau;
            dgv = InitDgv();


            
        }

      
        protected override void OnQueryPageSettings(QueryPageSettingsEventArgs e)
        {

            base.OnQueryPageSettings(e);
            e.PageSettings.Margins = new Margins(0, 0, 0, 0);
            dgv.Width = e.PageSettings.Bounds.Width;

        }

        void DrawNomBanque(ref int y, PrintPageEventArgs e)
        {

            Font ft = new Font("Garamond",12,FontStyle.Regular,GraphicsUnit.Point);
            

            int TEXTHEIGHT = 70;
            int TEXTWIDTH = e.PageBounds.Width / 2;


            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            string Text = "Nom de la banque : "+Currentbordereau.BanqueDeRemise.Libelle;


            Rectangle Rect = new Rectangle( (e.PageBounds.Width - TEXTWIDTH)/2,y, TEXTWIDTH, TEXTHEIGHT);

            e.Graphics.DrawRectangle(Pens.Black, Rect);
            e.Graphics.DrawString(Text,ft,Brushes.Black,Rect,sf);

            y = Rect.Bottom;
        }

        void DrawTitulaire(ref int y, PrintPageEventArgs e)
        {

            Font ft = new Font("Garamond", 12, FontStyle.Regular, GraphicsUnit.Point);


            int TEXTHEIGHT = 70;
            int TEXTWIDTH = e.PageBounds.Width / 2;


            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            string Text = "Titulaire du compte : " +Currentbordereau.BanqueDeRemise.Titulaire;


            Rectangle Rect = new Rectangle( (e.PageBounds.Width - TEXTWIDTH) / 2,y, TEXTWIDTH, TEXTHEIGHT);

            e.Graphics.DrawRectangle(Pens.Black, Rect);
            e.Graphics.DrawString(Text, ft, Brushes.Black, Rect, sf);

            y = Rect.Bottom;
        }

        void DrawDateRemise(ref int y, PrintPageEventArgs e)
        {

            Font ft = new Font("Garamond", 12, FontStyle.Regular, GraphicsUnit.Point);


            int TEXTHEIGHT = 70;
            int TEXTWIDTH = e.PageBounds.Width / 2;


            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            string Text = "Date remise : " + (Currentbordereau.DateRemise==null?"":Currentbordereau.DateRemise.Value.ToLongDateString());


            Rectangle Rect = new Rectangle( (e.PageBounds.Width - TEXTWIDTH) / 2,y, TEXTWIDTH, TEXTHEIGHT);

            e.Graphics.DrawRectangle(Pens.Black, Rect);
            e.Graphics.DrawString(Text, ft, Brushes.Black, Rect, sf);

            y = Rect.Bottom;
        }

        void DrawNumBordereau(ref int y, PrintPageEventArgs e)
        {

            Font ft = new Font("Garamond", 12, FontStyle.Regular, GraphicsUnit.Point);


            int TEXTHEIGHT = 70;
            int TEXTWIDTH = e.PageBounds.Width / 2;


            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            //string Text = "Numéro de bordereau : " + Currentbordereau.NumBordereau;
           // if (Currentbordereau.NumBordereauBancaire!="")
            string Text = "N° de bordereau bancaire : " + Currentbordereau.NumBordereauBancaire;


            Rectangle Rect = new Rectangle( (e.PageBounds.Width - TEXTWIDTH) / 2,y, TEXTWIDTH, TEXTHEIGHT);

            e.Graphics.DrawRectangle(Pens.Black, Rect);
            e.Graphics.DrawString(Text, ft, Brushes.Black, Rect, sf);

            y = Rect.Bottom;
        }

        void DrawRIB(ref int y, PrintPageEventArgs e)
        {

            Font ft = new Font("Garamond", 12, FontStyle.Regular, GraphicsUnit.Point);


            int TEXTHEIGHT = 20;
            int TEXTWIDTH = 20;


            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            int x = (e.PageBounds.Width-520)/2;
            //Etablissement
            for (int i = 0; i < 5; i++)
            {
                string txt = Currentbordereau.BanqueDeRemise.NumA[i].ToString();
                Rectangle rect = new Rectangle(x,y,TEXTWIDTH,TEXTHEIGHT);
                e.Graphics.DrawRectangle(Pens.Black, rect);
                e.Graphics.DrawString(txt, ft, Brushes.Black, rect, sf);
                x += TEXTWIDTH;
            }
            x += TEXTWIDTH;
            //Guichet
            for (int i = 0; i < 5; i++)
            {
                string txt = Currentbordereau.BanqueDeRemise.NumGui[i].ToString();
                Rectangle rect = new Rectangle(x, y, TEXTWIDTH, TEXTHEIGHT);
                e.Graphics.DrawRectangle(Pens.Black, rect);
                e.Graphics.DrawString(txt, ft, Brushes.Black, rect, sf);
                x += TEXTWIDTH;

            }
            x += TEXTWIDTH;
            //Num Compte
            for (int i = 0; i < 11; i++)
            {
                string txt = Currentbordereau.BanqueDeRemise.NumCPT[i].ToString();
                Rectangle rect = new Rectangle(x, y, TEXTWIDTH, TEXTHEIGHT);
                e.Graphics.DrawRectangle(Pens.Black, rect);
                e.Graphics.DrawString(txt, ft, Brushes.Black, rect, sf);
                x += TEXTWIDTH;

            }
            x += TEXTWIDTH;
            //Cle
            string cle = Currentbordereau.BanqueDeRemise.NumCle;
                
            for (int i = 0; i < 2; i++)
            {
                string txt = cle[i].ToString();
                Rectangle rect = new Rectangle(x, y, TEXTWIDTH, TEXTHEIGHT);
                e.Graphics.DrawRectangle(Pens.Black, rect);
                e.Graphics.DrawString(txt, ft, Brushes.Black, rect, sf);
                x += TEXTWIDTH;

            }



            y += TEXTHEIGHT;
        }
        DataGridViewPrinter prntr = null;
        bool DrawCheques(ref int y, PrintPageEventArgs e)
        {
            bool mustcontinue = false;
            if (Currentbordereau.NbCheques == 0) return false;


            Font ftTitle = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Point);
            Font ft = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point);

            


            if (prntr==null)
                prntr = new DataGridViewPrinter(dgv,(PrintDocument)this,true,false,"",ft,Color.Black,false);
            prntr.TopMargin = y;
            prntr.BottomMargin = 20;
            mustcontinue = prntr.DrawDataGridView(e.Graphics);

            y = (int)prntr.currenty;

            return mustcontinue;
        }


        void DrawEspeces(ref int y, PrintPageEventArgs e)
        {

            if ((Currentbordereau.Nbbillets5 == 0)&&
                (Currentbordereau.Nbbillets10 == 0) &&
                (Currentbordereau.Nbbillets20 == 0) &&
                (Currentbordereau.Nbbillets50 == 0) &&
                (Currentbordereau.Nbbillets100 == 0) &&
                (Currentbordereau.Nbbillets200 == 0) &&
                (Currentbordereau.Nbbillets500 == 0)) return;


            Font ft = new Font("Garamond", 12, FontStyle.Regular, GraphicsUnit.Point);

            StringFormat sf = new StringFormat();

            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            int SQUAREWIDTH = 75;
            int SQUAREHEIGHT = 35;
            
            int pos = (e.PageBounds.Width - (SQUAREWIDTH * 8)) / 2;
            
            string[,] data = new string[,]{{"Type de billets","5","10","20","50","100","200","500"},
                                           {"Nombre de billets",Currentbordereau.Nbbillets5.ToString(),Currentbordereau.Nbbillets10.ToString(),Currentbordereau.Nbbillets20.ToString(),Currentbordereau.Nbbillets50.ToString(),Currentbordereau.Nbbillets100.ToString(),Currentbordereau.Nbbillets200.ToString(),Currentbordereau.Nbbillets500.ToString()}};

            int maxy = y;
            for (int xr=0;xr<8;xr++)
                for (int yr = 0; yr < 2; yr++)
                {
                    Rectangle currentRectangle = new Rectangle(pos + (SQUAREWIDTH * xr), y + (SQUAREHEIGHT * yr), SQUAREWIDTH, SQUAREHEIGHT);

                    e.Graphics.DrawRectangle(Pens.Black, currentRectangle);
                    e.Graphics.DrawString(data[yr,xr], ft, Brushes.Black, currentRectangle, sf);

                    maxy = Math.Max(currentRectangle.Bottom, maxy);
                }

            y = maxy;

            string s = "";

            foreach (PaiementReel pr in Currentbordereau.paiements)
            {
                if (s != "") s += "\n";
                s += pr.ToString();
            }

            SizeF sz = e.Graphics.MeasureString(s, ft);
            RectangleF r = new RectangleF(pos + (SQUAREWIDTH), y, sz.Width, sz.Height);

            e.Graphics.DrawString(s, ft, Brushes.Black, r);
            y = (int)(y + sz.Height + 2);
        }

        private DataGridView InitDgv()
        {

            Font Headerft = new Font("Arial", 8, FontStyle.Bold, GraphicsUnit.Point);
           
            DataGridViewCellStyle dgvcstyle = new DataGridViewCellStyle();
            dgvcstyle.Font = new Font("Arial", 8, FontStyle.Regular, GraphicsUnit.Point);
            dgvcstyle.WrapMode = DataGridViewTriState.False;
            dgvcstyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            DataGridView dgv = new DataGridView();
            dgv.ColumnHeadersDefaultCellStyle.Font = Headerft;
            dgv.DefaultCellStyle = dgvcstyle;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AllowUserToAddRows = false;
            dgv.RowHeadersVisible = false;

            DataGridViewColumn dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Nom du payeur";
            dgvcol.Width = 200;
            dgv.Columns.Add(dgvcol);

            dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Nom du patient";
            dgvcol.Width = 200;
            dgv.Columns.Add(dgvcol);

            dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Numero du cheque";
            dgvcol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvcol.Width = 35;
            dgv.Columns.Add(dgvcol);

            dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Montant";
            dgvcol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvcol.Width = 80;
            dgv.Columns.Add(dgvcol);
            
            dgvcol = new DataGridViewTextBoxColumn();
            dgvcol.HeaderText = "Banque";
            dgvcol.Width = 200;
            dgv.Columns.Add(dgvcol);
            
            

            


            foreach (PaiementReel pr in Currentbordereau.paiements)
            {
                object[] o = new object[]{
                    pr.payeur,
                    pr.Patients,
                    pr.NumCheque,
                    pr.Montant.ToString("C2"),
                    pr.BanqueEmetrice==null?"":pr.BanqueEmetrice.Libelle,
                    
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


            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            string Title = "";
            string SubTitle = "";

            if (tpe == TypeBordereau.Cheques)
            {
                Title = "BORDEREAU DE REMISE DE CHEQUE";
                SubTitle = "Tamponner chaque chèque au dos avec le tampon du cabinet";
            }
            if (tpe == TypeBordereau.Especes)
            {
                Title = "BORDEREAU DE REMISE DES ESPECES";
                SubTitle = "";
            }

            int y=0;
            Rectangle titleRect = new Rectangle(0,y,e.PageBounds.Width,TITLEHEIGHT);
            y = titleRect.Bottom;
            Rectangle SubTitleRect = new Rectangle(0,y,e.PageBounds.Width,SUBTITLEHEIGHT);
            y = SubTitleRect.Bottom;

            e.Graphics.DrawString(Title, ftbold, Brushes.Black, titleRect, sf);
            e.Graphics.DrawString(SubTitle, ft, Brushes.Black, SubTitleRect, sf);

            DrawNomBanque(ref y,e);
            y += 10;
            DrawTitulaire(ref y,e);
            y += 10;
            DrawDateRemise(ref y, e);
            y += 10;
            DrawNumBordereau(ref y, e);
            y += 10;
            DrawRIB(ref y, e);
            y += 10;

            string Text = "";
            if ((tpe == TypeBordereau.Cheques) ||
                ((tpe == TypeBordereau.Auto) && (Currentbordereau.NbCheques > 0)))
            {
                e.HasMorePages = DrawCheques(ref y, e);
                Text += "Nombre de cheques : " + Currentbordereau.NbCheques.ToString();
            
            }
            else
            {
               DrawEspeces(ref y, e);
            }

            y += 20;

            Text += "      TOTAL de la remise : " + Currentbordereau.Montant.ToString("C2");
            Rectangle footerRect = new Rectangle(0, y, e.PageBounds.Width, 25);

            e.Graphics.DrawString(Text, ft, Brushes.Black, footerRect, sf);


        }

      
    }
}
