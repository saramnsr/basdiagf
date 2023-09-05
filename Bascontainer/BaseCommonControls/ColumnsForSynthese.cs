
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
//using BASEPractice_BO;
using BasCommon_BO.ElementsEnBouche.BO;
using BasCommon_BO;
using BasCommon_BL;
using System.Text.RegularExpressions;
using BaseCommonControls;


namespace BaseCommonControls
{
    #region Assistante Praticien
    public class TheBigCtrlDataGridViewPersonneCell :
     DataGridViewTextBoxCell
    {



        protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
        {
            TheBigCtrlDataGridViewPersonneColumn parent = (TheBigCtrlDataGridViewPersonneColumn)this.OwningColumn;

            CommTraitement comclinique = (CommTraitement)this.OwningRow.Tag;


            if (value is Utilisateur)
            {

                Utilisateur user = (Utilisateur)value;
                cellStyle.ForeColor = (comclinique.DatePrevisionnnelle > DateTime.Now) ? Color.OrangeRed : Color.LightCoral;

                //if (parent.infocomplementaire != null)
                //{
                //    if (comclinique.patient == null)
                //        comclinique.patient = baseMgmtPatient.GetPatient(comclinique.IdPatient);

                //    if (comclinique.patient.infoscomplementaire == null)
                //        comclinique.patient.infoscomplementaire = baseMgmtPatient.getinfocomplementaire(comclinique.IdPatient);

                //    if ((comclinique.patient.infoscomplementaire.PraticienResponsable == user) ||
                //        (comclinique.patient.infoscomplementaire.AssistanteResponsable == user))
                //        cellStyle.ForeColor = (comclinique.DatePrevisionnnelle > DateTime.Now) ? Color.Green : Color.LightGreen;

                //}

                formattedValue = user.Initial;
            }

            base.Paint(graphics, clipBounds,
               cellBounds, rowIndex, cellState,
               value, formattedValue, errorText,
               cellStyle, advancedBorderStyle,
               paintParts);
        }

    }

    public class TheBigCtrlDataGridViewPersonneColumn :
      DataGridViewColumn
    {
        private InfoPatientComplementaire _infocomplementaire;
        public InfoPatientComplementaire infocomplementaire
        {
            get
            {
                return _infocomplementaire;
            }
            set
            {
                _infocomplementaire = value;
            }
        }

        public TheBigCtrlDataGridViewPersonneColumn()
        {
            this.CellTemplate = new TheBigCtrlDataGridViewPersonneCell();
            this.ReadOnly = true;
        }


    }
    #endregion

    #region Autre personne

    public class TheBigCtrlDataGridViewAutrePersonneCell :
     DataGridViewTextBoxCell
    {



        protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
        {
            TheBigCtrlDataGridViewAutrePersonneColumn parent = (TheBigCtrlDataGridViewAutrePersonneColumn)this.OwningColumn;

            if (value is List<CommAutrePersonne>)
            {

                List<CommAutrePersonne> users = (List<CommAutrePersonne>)value;

                formattedValue = users.Count == 0 ? "" : users.Count.ToString();
                ToolTipText = "";

                foreach (CommAutrePersonne cap in users)
                {
                    if (ToolTipText != "") ToolTipText += "\n";

                    ToolTipText += cap.ToString();
                }
            }

            base.Paint(graphics, clipBounds,
               cellBounds, rowIndex, cellState,
               value, formattedValue, errorText,
               cellStyle, advancedBorderStyle,
               paintParts);
        }

    }

    public class TheBigCtrlDataGridViewAutrePersonneColumn :
      DataGridViewColumn
    {


        public TheBigCtrlDataGridViewAutrePersonneColumn()
        {
            this.CellTemplate = new TheBigCtrlDataGridViewAutrePersonneCell();
            this.ReadOnly = true;
        }


    }
    #endregion
    
    #region dents

    public class TheBigCtrlDataGridViewDentsCell :
     DataGridViewTextBoxCell
    {
      protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
        {
            TheBigCtrlDataGridViewDentsColumn parent = (TheBigCtrlDataGridViewDentsColumn)this.OwningColumn;

            if (value != null)
            {

                ToolTipText = value.ToString();
            }

            base.Paint(graphics, clipBounds,
               cellBounds, rowIndex, cellState,
               value, formattedValue, errorText,
               cellStyle, advancedBorderStyle,
               paintParts);
        }

    }

    public class TheBigCtrlDataGridViewDentsColumn :
      DataGridViewColumn
    {
        public TheBigCtrlDataGridViewDentsColumn()
        {
            this.CellTemplate = new TheBigCtrlDataGridViewDentsCell();
            this.ReadOnly = true;
        }


    }
    #endregion
  
    #region actes
    public class TheBigCtrlDataGridViewActeCell :
     DataGridViewTextBoxCell
    {



        protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
        {
            CommTraitement comclinique = (CommTraitement)this.OwningRow.Tag;

            if (comclinique == null) return;

            formattedValue = null;

            string txt = "";
            if (comclinique.Appointement == null)
            {
                if (value is Acte)
                    txt = ((Acte)value).acte_durestd.ToString();
            }
            else
                txt = comclinique.Appointement.acte.acte_durestd.ToString();

            base.Paint(graphics, clipBounds,
              cellBounds, rowIndex, cellState,
              value, formattedValue, errorText,
              cellStyle, advancedBorderStyle,
              paintParts);



            Color ClEnSalle = Color.FromArgb(255, 200, 255, 200);
            Color ClFauteuil = Color.FromArgb(255, 38, 255, 38);
            Color ClSecretariat = Color.FromArgb(255, 18, 255, 18);
            Color ClSortie = Color.FromArgb(50, 100, 100, 100);



            TheBigCtrlDataGridViewActeColumn parent = (TheBigCtrlDataGridViewActeColumn)this.OwningColumn;




            if ((comclinique.Appointement == null) && (!(value is Acte))) return;

            Color markcolor = Color.WhiteSmoke;
            Color appcolor = comclinique.Appointement == null ? ((Acte)value).acte_couleur : comclinique.Appointement.Color;


            Color txtcolor = Color.Black;
            FontStyle fs = FontStyle.Regular;

            Pen borderpen = new Pen(Color.Black, 1);

            if (comclinique.etat == CommTraitement.EtatCommentaire.Termine)
            {
                appcolor = Color.FromArgb(50, appcolor);
                txtcolor = Color.Gray;
                borderpen.Color = Color.Gray;
            }


            if (comclinique.Appointement != null)
            {

               

                if (((RHAppointment)comclinique.Appointement).Status == RHAppointment.EnumStatus.Absent)
                    fs = FontStyle.Strikeout;


               
            }


            Rectangle newRect = new Rectangle(cellBounds.X + 1,
            cellBounds.Y + 1, cellBounds.Width - 4,
            cellBounds.Height - 4);

            graphics.FillRectangle(new SolidBrush(appcolor), newRect);

            graphics.DrawRectangle(borderpen, newRect);


            Font ft1 = new Font(cellStyle.Font.FontFamily.Name, 11f, fs, GraphicsUnit.Pixel);



            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            graphics.DrawString(txt, ft1, new SolidBrush(txtcolor), cellBounds, sf);

            
        }

    }

    public class TheBigCtrlDataGridViewActeColumn :
      DataGridViewColumn
    {


        public TheBigCtrlDataGridViewActeColumn()
        {
            this.CellTemplate = new TheBigCtrlDataGridViewActeCell();
            this.ReadOnly = true;
        }


    }






    #endregion
    ///

    #region DateGrid



    public class TheBigCtrlDataGridViewDateCellGrid :
     DataGridViewTextBoxCell
    {


        private static int GetDaysInMonth(int year, int month)
        {
            // this is also available from Calendar class,
            // but just as easy to do ourselves

            if (month < 1 || month > 12)
            {
                throw new ArgumentException("month value must be from 1-12");
            }

            // 1 2 3 4 5 6 7 8 9 10 11 12
            int[] days = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            if (((year / 400 * 400) == year) ||
            (((year / 4 * 4) == year) && (year % 100 != 0)))
            {
                days[2] = 29;
            }

            return days[month];
        }

        private static void DateDiff(DateTime d1, DateTime d2,
        out int years, out int months, out int days)
        {
            // compute & return the difference of two dates,
            // returning years, months & days
            // d1 should be the larger (newest) of the two dates
            //
            //
            // y m d
            // 3/10/2005 <-- 3/10/2005 0 0 0
            // 3/10/2005 <-- 3/09/2005 0 0 1
            // 3/10/2005 <-- 3/01/2005 0 0 9
            // 3/10/2005 <-- 2/28/2005 0 0 10
            // 3/10/2005 <-- 2/11/2005 0 0 27
            // 3/10/2005 <-- 2/10/2005 0 1 0
            // 3/10/2005 <-- 2/09/2005 0 1 1
            // 3/10/2005 <-- 7/20/1969 35 7 21

            // we want d1 to be the larger (newest) date
            // flip if we need to

            if (d1 < d2)
            {
                DateTime d3 = d2;
                d2 = d1;
                d1 = d3;
            }

            // compute difference in total months
            months = 12 * (d1.Year - d2.Year) + (d1.Month - d2.Month);

            // based upon the 'days',
            // adjust months & compute actual days difference
            if (d1.Day < d2.Day)
            {
                months--;
                days = GetDaysInMonth(d2.Year, d2.Month) - d2.Day + d1.Day;
            }
            else
            {
                days = d1.Day - d2.Day;
            }

            // compute years & actual months
            years = months / 12;
            months -= years * 12;

            //Debug.WriteLine(string.Format("{0} <-- {1} {2,2} {3,2} {4,2}",d1.ToShortDateString(),d2.ToShortDateString(),year s,months,days));
        }



        protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
        {
            TheBigCtrlDataGridViewDateColumnGrid parent = (TheBigCtrlDataGridViewDateColumnGrid)this.OwningColumn;



            CommTraitement comclinique = (CommTraitement)this.OwningRow.Tag;

            if (comclinique == null) return;

            string txt = "";
            string ToolTipTxt = txt;

            if (comclinique.Appointement != null)
            {
                //   if (((RHAppointment)comclinique.Appointement).Status == RHAppointment.EnumStatus.Absent)
                //       cellStyle.Font = new Font(cellStyle.Font, FontStyle.Strikeout);

                txt = comclinique.Appointement.StartDate.ToShortDateString();
                ToolTipTxt = txt;
            }
            else
                if ((comclinique.etat == CommTraitement.EtatCommentaire.Termine) || (comclinique.DatePrevisionnnelle == null))
                {
                    if (comclinique.date != null)
                    {
                        txt = comclinique.date.Value.ToShortDateString();
                        ToolTipTxt = txt;
                    }
                    else
                    {
                        txt = "???";
                        ToolTipTxt = "";
                    }
                }
                else
                {

                    
                    //if (comclinique.NbMois != 0 || comclinique.NbJours != 0)
                    //{
                    if ((comclinique.NbMois * 30) > comclinique.NbJours)
                    comclinique.NbJours = comclinique.NbMois * 30 + comclinique.NbJours;
                    txt = (comclinique.NbJours).ToString() + "j";
                    //}
                    //else
                    //{
                    //    nbjours = (int)(comclinique.DatePrevisionnnelle.Value - DateTime.Now).TotalDays;
                    //    txt = nbjours.ToString() + "j";
                    //    bool isnegatif = nbjours < 0;

                    //    if (txt != "")
                    //    {
                    //        if (isnegatif)
                    //        {
                    //            txt = "-" + txt;
                    //            cellStyle.ForeColor = Color.Red;
                    //        }
                    //        else
                    //            txt = "+" + txt;

                    //    }
                    //    else
                    //    {
                    //        txt = "Aujourd'hui";
                    //        ToolTipTxt = txt;

                    //    }
                    //}

                }


            formattedValue = txt;
            ToolTipText = ToolTipTxt;



            base.Paint(graphics, clipBounds,
               cellBounds, rowIndex, cellState,
               value, formattedValue, errorText,
               cellStyle, advancedBorderStyle,
               paintParts);

        }
    }

    public class TheBigCtrlDataGridViewDateColumnGrid :
      DataGridViewColumn
    {


        public TheBigCtrlDataGridViewDateColumnGrid()
        {
            this.CellTemplate = new TheBigCtrlDataGridViewDateCellGrid();
            this.ReadOnly = true;
        }


    }

    #endregion
    
    ////
    #region Date



    public class TheBigCtrlDataGridViewDateCell :
     DataGridViewTextBoxCell
    {


        private static int GetDaysInMonth(int year, int month)
        {
            // this is also available from Calendar class,
            // but just as easy to do ourselves

            if (month < 1 || month > 12)
            {
                throw new ArgumentException("month value must be from 1-12");
            }

            // 1 2 3 4 5 6 7 8 9 10 11 12
            int[] days = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            if (((year / 400 * 400) == year) ||
            (((year / 4 * 4) == year) && (year % 100 != 0)))
            {
                days[2] = 29;
            }

            return days[month];
        }

        private static void DateDiff(DateTime d1, DateTime d2,
        out int years, out int months, out int days)
        {
            // compute & return the difference of two dates,
            // returning years, months & days
            // d1 should be the larger (newest) of the two dates
            //
            //
            // y m d
            // 3/10/2005 <-- 3/10/2005 0 0 0
            // 3/10/2005 <-- 3/09/2005 0 0 1
            // 3/10/2005 <-- 3/01/2005 0 0 9
            // 3/10/2005 <-- 2/28/2005 0 0 10
            // 3/10/2005 <-- 2/11/2005 0 0 27
            // 3/10/2005 <-- 2/10/2005 0 1 0
            // 3/10/2005 <-- 2/09/2005 0 1 1
            // 3/10/2005 <-- 7/20/1969 35 7 21

            // we want d1 to be the larger (newest) date
            // flip if we need to

            if (d1 < d2)
            {
                DateTime d3 = d2;
                d2 = d1;
                d1 = d3;
            }

            // compute difference in total months
            months = 12 * (d1.Year - d2.Year) + (d1.Month - d2.Month);

            // based upon the 'days',
            // adjust months & compute actual days difference
            if (d1.Day < d2.Day)
            {
                months--;
                days = GetDaysInMonth(d2.Year, d2.Month) - d2.Day + d1.Day;
            }
            else
            {
                days = d1.Day - d2.Day;
            }

            // compute years & actual months
            years = months / 12;
            months -= years * 12;

            //Debug.WriteLine(string.Format("{0} <-- {1} {2,2} {3,2} {4,2}",d1.ToShortDateString(),d2.ToShortDateString(),year s,months,days));
        }



        protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
        {
            TheBigCtrlDataGridViewDateColumn parent = (TheBigCtrlDataGridViewDateColumn)this.OwningColumn;



            CommClinique comclinique = (CommClinique)this.OwningRow.Tag;

            if (comclinique == null) return;

            string txt = "";
            string ToolTipTxt = txt;

            if (comclinique.Appointement != null)
            {
                //   if (((RHAppointment)comclinique.Appointement).Status == RHAppointment.EnumStatus.Absent)
                //       cellStyle.Font = new Font(cellStyle.Font, FontStyle.Strikeout);

                txt = comclinique.Appointement.StartDate.ToShortDateString();
                ToolTipTxt = txt;
            }
            else
                if (comclinique.etat == CommClinique.EtatCommentaire.Termine)
                {
                    if (comclinique.date != null)
                    {
                        txt = comclinique.date.Value.ToShortDateString();
                        ToolTipTxt = txt;
                    }
                    else
                    {
                        txt = "???";
                        ToolTipTxt = "";
                    }
                }
                else
                {

                    int nbjours = 0;
                    int nbmonth = 0;
                    int nbyear = 0;
                    bool isnegatif = nbjours < 0;

                    if (DateTime.Now > comclinique.DatePrevisionnnelle)
                    {
                        DateDiff(DateTime.Now, comclinique.DatePrevisionnnelle.Value, out nbyear, out nbmonth, out nbjours);
                        isnegatif = true;
                    }
                    else
                    {
                        DateDiff(comclinique.DatePrevisionnnelle.Value, DateTime.Now, out nbyear, out nbmonth, out nbjours);
                        isnegatif = false;
                    }


                    #region calculate string


                    txt = "";
                    ToolTipTxt = txt;
                    if (nbyear > 0)
                    {
                        txt += nbyear.ToString() + "A";
                        ToolTipTxt += nbyear.ToString() + " An(s)";
                    }

                    if (nbmonth > 0)
                    {
                        txt += txt == "" ? nbmonth.ToString() + "M" : "," + nbmonth.ToString() + "M";
                        ToolTipTxt += ToolTipTxt == "" ? nbmonth.ToString() + " Mois" : ", " + nbmonth.ToString() + " Mois";
                    }
                    if (nbjours > 0)
                    {
                        txt += txt == "" ? nbjours.ToString() + "J" : " et " + nbjours.ToString() + "J";
                        ToolTipTxt += ToolTipTxt == "" ? nbjours.ToString() + " Jour(s)" : " et " + nbjours.ToString() + " Jour(s)";
                    }
                    #endregion

                    if (txt != "")
                    {
                        if (isnegatif)
                        {
                            txt = "-" + txt;
                            cellStyle.ForeColor = Color.Red;
                        }
                        else
                            txt = "+" + txt;

                    }
                    else
                    {
                        txt = "Aujourd'hui";
                        ToolTipTxt = txt;

                    }
                }




            formattedValue = txt;
            ToolTipText = ToolTipTxt;



            base.Paint(graphics, clipBounds,
               cellBounds, rowIndex, cellState,
               value, formattedValue, errorText,
               cellStyle, advancedBorderStyle,
               paintParts);

        }
    }

    public class TheBigCtrlDataGridViewDateColumn :
      DataGridViewColumn
    {


        public TheBigCtrlDataGridViewDateColumn()
        {
            this.CellTemplate = new TheBigCtrlDataGridViewDateCell();
            this.ReadOnly = true;
        }


    }

    #endregion
    
    #region Fait
    public class TheBigCtrlDataGridViewFaitCell :
     DataGridViewTextBoxCell
    {



        protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
        {



            if ((value != null) && (value != ""))
            {
                string[] ss = ((string)value.ToString()).Split('\n');

                List<string> lst = new List<string>();

                foreach (string s in ss)
                    if (s.Trim() != "") lst.Add(s);

                formattedValue = lst.Count > 1 ? "[...]" + lst[lst.Count - 1] : ((string)value.ToString());

                ToolTipText = ((string)value);
            }
            else
            {
                CommClinique comclinique = (CommClinique)this.OwningRow.Tag;

                if (comclinique == null) return;

                if (comclinique.Acte == null)
                    formattedValue = "";
                else
                    formattedValue = comclinique.Acte.acte_libelle;

                ToolTipText = ((string)formattedValue);

                if (comclinique.Appointement != null)
                {
                    //  if (((RHAppointment)comclinique.Appointement).Status == RHAppointment.EnumStatus.Absent)
                    //      cellStyle.Font = new Font(cellStyle.Font, FontStyle.Strikeout);

                    formattedValue = ((RHAppointment)comclinique.Appointement).acte.acte_libelle;
                }
            }

            base.Paint(graphics, clipBounds,
              cellBounds, rowIndex, cellState,
              value, formattedValue, errorText,
              cellStyle, advancedBorderStyle,
              paintParts);
        }

    }

    public class TheBigCtrlDataGridViewFaitColumn :
      DataGridViewColumn
    {


        public TheBigCtrlDataGridViewFaitColumn()
        {
            this.CellTemplate = new TheBigCtrlDataGridViewFaitCell();
            this.ReadOnly = true;
        }


    }

    #endregion

    #region actesSupp
    //Cell Supp
    public class TheBigCtrlDataGridViewActeSuppCell :
   DataGridViewTextBoxCell
    {



        protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
        {


            TheBigCtrlDataGridViewActeSuppColumn parent = (TheBigCtrlDataGridViewActeSuppColumn)this.OwningColumn;
            CommTraitement rowcom = ((CommTraitement)this.OwningRow.Tag);
            Color appcolor;
            formattedValue = "";
            ToolTipText = "";
            //
            CommTraitement comclinique = (CommTraitement)this.OwningRow.Tag;

            if (comclinique == null) return;
            cellStyle.BackColor = Color.FromArgb(Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("230", @"\d+").Value), Int32.Parse(Regex.Match("230", @"\d+").Value), Int32.Parse(Regex.Match("230", @"\d+").Value));
            cellStyle.SelectionBackColor = Color.FromArgb(Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("51", @"\d+").Value), Int32.Parse(Regex.Match("153", @"\d+").Value), Int32.Parse(Regex.Match("255", @"\d+").Value));
            cellStyle.SelectionForeColor = Color.FromArgb(Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("255", @"\d+").Value));
            cellStyle.BackColor = Color.White;
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, null, "", "", cellStyle, advancedBorderStyle, paintParts);
            if (comclinique == null) return;

            formattedValue = null;

            string txt = "";
            if (comclinique.Appointement == null)
            {
                if (value is Acte)
                    txt = ((Acte)value).acte_durestd.ToString();
            }
            else
            {
                txt = (comclinique.Appointement.EndDate - comclinique.Appointement.StartDate).TotalMinutes.ToString();
            }

            //

            if (value is List<CommActesTraitement>)
            {
                int CtrComActes = 0;
                foreach (CommActesTraitement d in ((List<CommActesTraitement>)value))
                {
                    // if ((string)formattedValue != "" && (string)formattedValue != null) formattedValue += ";";
                    formattedValue = (d.acte_durestd.ToString() == null) ? "" : d.acte_durestd.ToString();


                    //formattedValue += d.CodePlanning.ToString();
                    if ((string)ToolTipText != "") ToolTipText += "\n";
                    ToolTipText += (d.LibActe == null) ? "" : d.LibActe.Trim();

                    cellStyle.ForeColor = d.acte_couleur;

                    // base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);


                    appcolor = d.acte_couleur;

                    Color markcolor = Color.White;

                    Color txtcolor = Color.Black;
                    FontStyle fs = FontStyle.Regular;

                    Pen borderpen = new Pen(Color.Black, 1);
                    Rectangle newRect = new Rectangle(cellBounds.X + CtrComActes,
                   cellBounds.Y + 1, 20,
                   cellBounds.Height - 4);





                    graphics.FillRectangle(new SolidBrush(appcolor), newRect);

                    graphics.DrawRectangle(borderpen, newRect);


                    Font ft1 = new Font(cellStyle.Font.FontFamily.Name, 11f, fs, GraphicsUnit.Pixel);



                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Far;
                    sf.LineAlignment = StringAlignment.Far;
                    graphics.DrawString(formattedValue.ToString(), ft1, new SolidBrush(txtcolor), cellBounds.X + 1 + CtrComActes, cellBounds.Y + 2);
                    CtrComActes = CtrComActes + 26;

                }


            }




        }

    }
    public class TheBigCtrlDataGridViewActeSuppColumn :
    DataGridViewColumn
    {


        public TheBigCtrlDataGridViewActeSuppColumn()
        {
            this.CellTemplate = new TheBigCtrlDataGridViewActeSuppCell();
            this.ReadOnly = true;
        }


    }
    #endregion

    #region grpt actes

    public class TheBigCtrlDataGridViewActeSuppCell1 :
DataGridViewTextBoxCell
    {



        protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
        {


            TheBigCtrlDataGridViewActeSuppColumn1 parent = (TheBigCtrlDataGridViewActeSuppColumn1)this.OwningColumn;
            CommTraitement rowcom = ((CommTraitement)this.OwningRow.Tag);
            Color appcolor;
            formattedValue = "";
            ToolTipText = "";
            //
            CommTraitement comclinique = (CommTraitement)this.OwningRow.Tag;

            if (comclinique == null) return;
            cellStyle.BackColor = Color.FromArgb(Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("230", @"\d+").Value), Int32.Parse(Regex.Match("230", @"\d+").Value), Int32.Parse(Regex.Match("230", @"\d+").Value));
            cellStyle.SelectionBackColor = Color.FromArgb(Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("51", @"\d+").Value), Int32.Parse(Regex.Match("153", @"\d+").Value), Int32.Parse(Regex.Match("255", @"\d+").Value));
            cellStyle.SelectionForeColor = Color.FromArgb(Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("255", @"\d+").Value));
            cellStyle.BackColor = Color.White;
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, null, "", "", cellStyle, advancedBorderStyle, paintParts);
            if (comclinique == null) return;

            formattedValue = null;

            string txt = "";
            if (comclinique.Appointement == null)
            {
                if (value is Acte)
                    txt = ((Acte)value).acte_durestd.ToString();
            }
            else
            {
                txt = (comclinique.Appointement.EndDate - comclinique.Appointement.StartDate).TotalMinutes.ToString();
            }

            //

            if (value is List<CommActesTraitement>)
            {
                int CtrComActes = 0;
                foreach (CommActesTraitement d in ((List<CommActesTraitement>)value))
                {
                    // if ((string)formattedValue != "" && (string)formattedValue != null) formattedValue += ";";
                    formattedValue = (d.acte_durestd.ToString() == null) ? "" : d.acte_durestd.ToString();


                    //formattedValue += d.CodePlanning.ToString();
                    if ((string)ToolTipText != "") ToolTipText += "\n";
                    ToolTipText += (d.LibActe == null) ? "" : d.LibActe.Trim();

                    cellStyle.ForeColor = d.acte_couleur;

                    // base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);


                    appcolor = d.acte_couleur;

                    Color markcolor = Color.White;

                    Color txtcolor = Color.Black;
                    FontStyle fs = FontStyle.Regular;

                    Pen borderpen = new Pen(Color.Black, 1);
                    Rectangle newRect = new Rectangle(cellBounds.X + CtrComActes,
                   cellBounds.Y + 1, 20,
                   cellBounds.Height - 4);





                    graphics.FillRectangle(new SolidBrush(appcolor), newRect);

                    graphics.DrawRectangle(borderpen, newRect);


                    Font ft1 = new Font(cellStyle.Font.FontFamily.Name, 11f, fs, GraphicsUnit.Pixel);



                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Far;
                    sf.LineAlignment = StringAlignment.Far;
                    graphics.DrawString(formattedValue.ToString(), ft1, new SolidBrush(txtcolor), cellBounds.X + 1 + CtrComActes, cellBounds.Y + 2);
                    CtrComActes = CtrComActes + 26;

                }


            }




        }

    }
    public class TheBigCtrlDataGridViewActeSuppColumn1 :
    DataGridViewColumn
    {


        public TheBigCtrlDataGridViewActeSuppColumn1()
        {
            this.CellTemplate = new TheBigCtrlDataGridViewActeSuppCell1();
            this.ReadOnly = true;
        }


    }

    #endregion

    #region Materiel
    public class TheBigCtrlDataGridViewMaterielCell :
     DataGridViewTextBoxCell
    {



        protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
        {
            TheBigCtrlDataGridViewMaterielColumn parent = (TheBigCtrlDataGridViewMaterielColumn)this.OwningColumn;
            CommTraitement rowcom = ((CommTraitement)this.OwningRow.Tag);
            if (rowcom == null) return;
            formattedValue = "";
            ToolTipText = "";

            if (value is List<CommMaterielTraitement>)
            {

                foreach (CommMaterielTraitement d in ((List<CommMaterielTraitement>)value))
                {
                    if ((string)formattedValue != "") formattedValue += ";";
                    formattedValue += d.ShortLib;
                    if ((string)ToolTipText != "") ToolTipText += "\n";
                    ToolTipText += d.Libelle;


                }

            }

            base.Paint(graphics, clipBounds,
              cellBounds, rowIndex, cellState,
              value, formattedValue, errorText,
              cellStyle, advancedBorderStyle,
              paintParts);
        }

    }

    public class TheBigCtrlDataGridViewMaterielColumn :
      DataGridViewColumn
    {


        public TheBigCtrlDataGridViewMaterielColumn()
        {
            this.CellTemplate = new TheBigCtrlDataGridViewMaterielCell();
            this.ReadOnly = true;
        }


    }

    #endregion

    #region Photo
    public class TheBigCtrlDataGridViewPhotoCell :
     DataGridViewTextBoxCell
    {



        protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
        {

           

            TheBigCtrlDataGridViewPhotoColumn parent = (TheBigCtrlDataGridViewPhotoColumn)this.OwningColumn;
            CommTraitement rowcom = ((CommTraitement)this.OwningRow.Tag);
            Color appcolor;
            formattedValue = "";
            ToolTipText = "";
            //
            CommTraitement comclinique = (CommTraitement)this.OwningRow.Tag;
            if (comclinique == null) return;
            cellStyle.BackColor = Color.FromArgb(Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("230", @"\d+").Value), Int32.Parse(Regex.Match("230", @"\d+").Value), Int32.Parse(Regex.Match("230", @"\d+").Value));
            cellStyle.SelectionBackColor = Color.FromArgb(Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("51", @"\d+").Value), Int32.Parse(Regex.Match("153", @"\d+").Value), Int32.Parse(Regex.Match("255", @"\d+").Value));
            cellStyle.SelectionForeColor = Color.FromArgb(Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("255", @"\d+").Value));
            cellStyle.BackColor = Color.White;
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, null, "", "", cellStyle, advancedBorderStyle, paintParts);
            if (comclinique == null) return;

            formattedValue = null;

            string txt = "";
            if (comclinique.Appointement == null)
            {
                if (value is Acte)
                    txt = ((Acte)value).acte_durestd.ToString();
            }
            else
            {
                txt = (comclinique.Appointement.EndDate - comclinique.Appointement.StartDate).TotalMinutes.ToString();
            }

            //

            if (value is List<CommActesTraitement>)
            {
                int CtrComPhotos = 0;
                foreach (CommActesTraitement d in ((List<CommActesTraitement>)value))
                {
                    // if ((string)formattedValue != "" && (string)formattedValue != null) formattedValue += ";";
                    formattedValue = (d.acte_durestd.ToString() == null) ? "" : d.acte_durestd.ToString();


                    //formattedValue += d.CodePlanning.ToString();
                    if ((string)ToolTipText != "") ToolTipText += "\n";
                    ToolTipText += (d.LibActe == null) ? "" : d.LibActe.Trim();

                    cellStyle.ForeColor = d.acte_couleur;

                    // base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);


                    appcolor = d.acte_couleur;

                    Color markcolor = Color.WhiteSmoke;

                    Color txtcolor = Color.Black;
                    FontStyle fs = FontStyle.Regular;

                    Pen borderpen = new Pen(Color.Black, 1);
                    Rectangle newRect = new Rectangle(cellBounds.X + CtrComPhotos,
                   cellBounds.Y + 1, 20,
                   cellBounds.Height - 4);





                    graphics.FillRectangle(new SolidBrush(appcolor), newRect);

                    graphics.DrawRectangle(borderpen, newRect);


                    Font ft1 = new Font(cellStyle.Font.FontFamily.Name, 11f, fs, GraphicsUnit.Pixel);



                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Far;
                    sf.LineAlignment = StringAlignment.Far;
                    graphics.DrawString(formattedValue.ToString(), ft1, new SolidBrush(txtcolor), cellBounds.X + 1 + CtrComPhotos, cellBounds.Y + 2);
                    CtrComPhotos = CtrComPhotos + 26;

                }


            }




        }

    }

    public class TheBigCtrlDataGridViewPhotoColumn :
      DataGridViewColumn
    {


        public TheBigCtrlDataGridViewPhotoColumn()
        {
            this.CellTemplate = new TheBigCtrlDataGridViewPhotoCell();
            this.ReadOnly = true;
        }


    }

    #endregion

    #region Radio
    public class TheBigCtrlDataGridViewRadioCell :
     DataGridViewTextBoxCell
    {



        protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
        {


            TheBigCtrlDataGridViewRadioColumn parent = (TheBigCtrlDataGridViewRadioColumn)this.OwningColumn;
            CommTraitement rowcom = ((CommTraitement)this.OwningRow.Tag);

          
            Color appcolor;
            formattedValue = "";
            ToolTipText = "";
            //
            CommTraitement comclinique = (CommTraitement)this.OwningRow.Tag;
            if (comclinique == null) return;
            cellStyle.BackColor = Color.FromArgb(Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("230", @"\d+").Value), Int32.Parse(Regex.Match("230", @"\d+").Value), Int32.Parse(Regex.Match("230", @"\d+").Value));
            cellStyle.SelectionBackColor = Color.FromArgb(Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("51", @"\d+").Value), Int32.Parse(Regex.Match("153", @"\d+").Value), Int32.Parse(Regex.Match("255", @"\d+").Value));
            cellStyle.SelectionForeColor = Color.FromArgb(Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("255", @"\d+").Value), Int32.Parse(Regex.Match("255", @"\d+").Value));
            cellStyle.BackColor = Color.White;
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, null, "", "", cellStyle, advancedBorderStyle, paintParts);
            if (comclinique == null) return;

            formattedValue = null;

            string txt = "";
            if (comclinique.Appointement == null)
            {
                if (value is Acte)
                    txt = ((Acte)value).acte_durestd.ToString();
            }
            else
            {
                txt = (comclinique.Appointement.EndDate - comclinique.Appointement.StartDate).TotalMinutes.ToString();
            }

            //

            if (value is List<CommActesTraitement>)
            {
                int CtrComRadios = 0;
                foreach (CommActesTraitement d in ((List<CommActesTraitement>)value))
                {
                    // if ((string)formattedValue != "" && (string)formattedValue != null) formattedValue += ";";
                    formattedValue = (d.acte_durestd.ToString() == null) ? "" : d.acte_durestd.ToString();


                    //formattedValue += d.CodePlanning.ToString();
                    if ((string)ToolTipText != "") ToolTipText += "\n";
                    ToolTipText += (d.LibActe == null) ? "" : d.LibActe.Trim();

                    cellStyle.ForeColor = d.acte_couleur;

                    // base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);


                    appcolor = d.acte_couleur;

                    Color markcolor = Color.WhiteSmoke;

                    Color txtcolor = Color.Black;
                    FontStyle fs = FontStyle.Regular;

                    Pen borderpen = new Pen(Color.Black, 1);
                    Rectangle newRect = new Rectangle(cellBounds.X + CtrComRadios,
                   cellBounds.Y + 1, 20,
                   cellBounds.Height - 4);





                    graphics.FillRectangle(new SolidBrush(appcolor), newRect);

                    graphics.DrawRectangle(borderpen, newRect);


                    Font ft1 = new Font(cellStyle.Font.FontFamily.Name, 11f, fs, GraphicsUnit.Pixel);



                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Far;
                    sf.LineAlignment = StringAlignment.Far;
                    graphics.DrawString(formattedValue.ToString(), ft1, new SolidBrush(txtcolor), cellBounds.X + 1 + CtrComRadios, cellBounds.Y + 2);
                    CtrComRadios = CtrComRadios + 26;

                }


            }




        }

    }

    public class TheBigCtrlDataGridViewRadioColumn :
      DataGridViewColumn
    {


        public TheBigCtrlDataGridViewRadioColumn()
        {
            this.CellTemplate = new TheBigCtrlDataGridViewRadioCell();
            this.ReadOnly = true;
        }


    }

    #endregion

    #region TIM
    public class TheBigCtrlDataGridViewTIMCell :
     DataGridViewTextBoxCell
    {



        protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
        {

            base.Paint(graphics, clipBounds,
               cellBounds, rowIndex, cellState,
               value, formattedValue, errorText,
               cellStyle, advancedBorderStyle,
               paintParts);
        }

    }

    public class TheBigCtrlDataGridViewTIMColumn :
      DataGridViewColumn
    {


        public TheBigCtrlDataGridViewTIMColumn()
        {
            this.CellTemplate = new TheBigCtrlDataGridViewTIMCell();
            this.ReadOnly = true;
        }


    }

    #endregion

    #region Accessoires

    public class TheBigCtrlDataGridViewAccessoiresBasCell :
     DataGridViewTextBoxCell
    {



        protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
        {
            TheBigCtrlDataGridViewAccessoiresBasColumn parent = (TheBigCtrlDataGridViewAccessoiresBasColumn)this.OwningColumn;

            CommClinique rowcom = ((CommClinique)this.OwningRow.Tag);


            int nbAppareilsInstalledHaut = 0;
            int nbAppareilsDesinstalledHaut = 0;

            if (value is List<IElementDent>)
            {

                List<IElementDent> lst = (List<IElementDent>)value;
                foreach (IElementDent app in lst)
                {
                    if (app.Bas && (
                        (app.IdCommDebut == rowcom.Id) ||
                        ((rowcom.date != null) && (app.DateInstallation.Value.Date == rowcom.date.Value.Date))))
                    {
                        nbAppareilsInstalledHaut++;
                    }

                    if (app.Bas && (
                        (app.IdCommFin == rowcom.Id) ||
                        ((rowcom.date != null) && (app.Datesuppression != null) && (app.Datesuppression.Value == rowcom.date.Value.Date))))
                    {
                        nbAppareilsDesinstalledHaut++;
                    }
                }

                string s = nbAppareilsInstalledHaut > 0 ? " +" + nbAppareilsInstalledHaut.ToString() : "";
                s += nbAppareilsDesinstalledHaut > 0 ? " -" + nbAppareilsDesinstalledHaut.ToString() : "";


                formattedValue = s;
            }
            base.Paint(graphics, clipBounds,
               cellBounds, rowIndex, cellState,
               value, formattedValue, errorText,
               cellStyle, advancedBorderStyle,
               paintParts);
        }

    }

    public class TheBigCtrlDataGridViewAccessoiresBasColumn :
      DataGridViewColumn
    {


        public TheBigCtrlDataGridViewAccessoiresBasColumn()
        {
            this.CellTemplate = new TheBigCtrlDataGridViewAccessoiresBasCell();
            this.ReadOnly = true;
        }


    }

    public class TheBigCtrlDataGridViewAccessoiresHautCell :
     DataGridViewTextBoxCell
    {



        protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
        {
            TheBigCtrlDataGridViewAccessoiresHautColumn parent = (TheBigCtrlDataGridViewAccessoiresHautColumn)this.OwningColumn;
            CommClinique rowcom = ((CommClinique)this.OwningRow.Tag);


            int nbAppareilsInstalledHaut = 0;
            int nbAppareilsDesinstalledHaut = 0;

            if (value is List<IElementDent>)
            {

                List<IElementDent> lst = (List<IElementDent>)value;
                foreach (IElementDent app in lst)
                {
                    if (app.Haut && (
                        (app.IdCommDebut == rowcom.Id) ||
                        ((rowcom.date != null) && (app.DateInstallation.Value.Date == rowcom.date.Value.Date))))
                    {
                        nbAppareilsInstalledHaut++;
                    }

                    if (app.Haut && (
                        (app.IdCommFin == rowcom.Id) ||
                        ((rowcom.date != null) && (app.Datesuppression != null) && (app.Datesuppression.Value == rowcom.date.Value.Date))))
                    {
                        nbAppareilsDesinstalledHaut++;
                    }
                }

                string s = nbAppareilsInstalledHaut > 0 ? " +" + nbAppareilsInstalledHaut.ToString() : "";
                s += nbAppareilsDesinstalledHaut > 0 ? " -" + nbAppareilsDesinstalledHaut.ToString() : "";


                formattedValue = s;
            }

            base.Paint(graphics, clipBounds,
               cellBounds, rowIndex, cellState,
               value, formattedValue, errorText,
               cellStyle, advancedBorderStyle,
               paintParts);
        }

    }

    public class TheBigCtrlDataGridViewAccessoiresHautColumn :
      DataGridViewColumn
    {


        public TheBigCtrlDataGridViewAccessoiresHautColumn()
        {
            this.CellTemplate = new TheBigCtrlDataGridViewAccessoiresHautCell();
            this.ReadOnly = true;
        }


    }


    #endregion


    #region Appareils

    public class TheBigCtrlDataGridViewAppareilBasCell :
     DataGridViewTextBoxCell
    {



        protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
        {
            TheBigCtrlDataGridViewAppareilBasColumn parent = (TheBigCtrlDataGridViewAppareilBasColumn)this.OwningColumn;

            CommClinique rowcom = ((CommClinique)this.OwningRow.Tag);


            int nbAppareilsInstalledHaut = 0;
            int nbAppareilsDesinstalledHaut = 0;

            if (value is List<IElementAppareil>)
            {

                List<IElementAppareil> lst = (List<IElementAppareil>)value;
                foreach (IElementAppareil app in lst)
                {
                    if (app.Bas && (
                        (app.IdCommDebut == rowcom.Id) ||
                        ((rowcom.date != null) && (app.DateInstallation != null) && (app.DateInstallation.Value.Date == rowcom.date.Value.Date))))
                    {
                        nbAppareilsInstalledHaut++;
                    }

                    if (app.Bas && (
                        (app.IdCommFin == rowcom.Id) ||
                        ((rowcom.date != null) && (app.Datesuppression != null) && (app.Datesuppression.Value == rowcom.date.Value.Date))))
                    {
                        nbAppareilsDesinstalledHaut++;
                    }
                }

                string s = nbAppareilsInstalledHaut > 0 ? " +" + nbAppareilsInstalledHaut.ToString() : "";
                s += nbAppareilsDesinstalledHaut > 0 ? " -" + nbAppareilsDesinstalledHaut.ToString() : "";


                formattedValue = s;
            }
            base.Paint(graphics, clipBounds,
               cellBounds, rowIndex, cellState,
               value, formattedValue, errorText,
               cellStyle, advancedBorderStyle,
               paintParts);
        }

    }

    public class TheBigCtrlDataGridViewAppareilBasColumn :
      DataGridViewColumn
    {


        public TheBigCtrlDataGridViewAppareilBasColumn()
        {
            this.CellTemplate = new TheBigCtrlDataGridViewAppareilBasCell();
            this.ReadOnly = true;
        }


    }

    public class TheBigCtrlDataGridViewAppareilHautCell :
     DataGridViewTextBoxCell
    {



        protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
        {
            TheBigCtrlDataGridViewAppareilHautColumn parent = (TheBigCtrlDataGridViewAppareilHautColumn)this.OwningColumn;
            CommClinique rowcom = ((CommClinique)this.OwningRow.Tag);


            int nbAppareilsInstalledHaut = 0;
            int nbAppareilsDesinstalledHaut = 0;

            if (value is List<IElementAppareil>)
            {

                List<IElementAppareil> lst = (List<IElementAppareil>)value;
                foreach (IElementAppareil app in lst)
                {
                    if (app.Haut && (
                        (app.IdCommDebut == rowcom.Id) ||
                        ((rowcom.date != null) && (app.DateInstallation != null) && (app.DateInstallation.Value.Date == rowcom.date.Value.Date))))
                    {
                        nbAppareilsInstalledHaut++;
                    }

                    if (app.Haut && (
                        (app.IdCommFin == rowcom.Id) ||
                        ((rowcom.date != null) && (app.Datesuppression != null) && (app.Datesuppression.Value == rowcom.date.Value.Date))))
                    {
                        nbAppareilsDesinstalledHaut++;
                    }
                }

                string s = nbAppareilsInstalledHaut > 0 ? " +" + nbAppareilsInstalledHaut.ToString() : "";
                s += nbAppareilsDesinstalledHaut > 0 ? " -" + nbAppareilsDesinstalledHaut.ToString() : "";


                formattedValue = s;
            }

            base.Paint(graphics, clipBounds,
               cellBounds, rowIndex, cellState,
               value, formattedValue, errorText,
               cellStyle, advancedBorderStyle,
               paintParts);
        }

    }

    public class TheBigCtrlDataGridViewAppareilHautColumn :
      DataGridViewColumn
    {


        public TheBigCtrlDataGridViewAppareilHautColumn()
        {
            this.CellTemplate = new TheBigCtrlDataGridViewAppareilHautCell();
            this.ReadOnly = true;
        }


    }

    #endregion

    #region Extraction
    public class TheBigCtrlDataGridViewExtractionCell :
     DataGridViewTextBoxCell
    {



        protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
        {
            TheBigCtrlDataGridViewExtractionColumn parent = (TheBigCtrlDataGridViewExtractionColumn)this.OwningColumn;
            CommClinique rowcom = ((CommClinique)this.OwningRow.Tag);

            formattedValue = "";
            ToolTipText = "";
            if (value is List<CommDentAExtraire>)
            {

                foreach (CommDentAExtraire d in ((List<CommDentAExtraire>)value))
                {
                    if ((string)formattedValue != "") formattedValue += ";";
                    formattedValue += d.dents;

                    if ((string)ToolTipText != "") ToolTipText += ";";
                    ToolTipText += d.dents;
                }

            }

            base.Paint(graphics, clipBounds,
              cellBounds, rowIndex, cellState,
              value, formattedValue, errorText,
              cellStyle, advancedBorderStyle,
              paintParts);
        }

    }

    public class TheBigCtrlDataGridViewExtractionColumn :
      DataGridViewColumn
    {


        public TheBigCtrlDataGridViewExtractionColumn()
        {
            this.CellTemplate = new TheBigCtrlDataGridViewExtractionCell();
            this.ReadOnly = true;
        }


    }

    #endregion

    #region Hygiene
    public class TheBigCtrlDataGridViewHygieneCell :
     DataGridViewTextBoxCell
    {



        protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates cellState,
      object value, object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
        {
            formattedValue = "";

            base.Paint(graphics, clipBounds,
              cellBounds, rowIndex, cellState,
              value, formattedValue, errorText,
              cellStyle, advancedBorderStyle,
              paintParts);

            if (value != null)
            {
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;


                TheBigCtrlDataGridViewHygieneColumn parent = (TheBigCtrlDataGridViewHygieneColumn)this.OwningColumn;
                CommClinique rowcom = ((CommClinique)this.OwningRow.Tag);

                Bitmap img = null;

                switch ((int)value)
                {
                    case -3: img = global::BaseCommonControls.Properties .Resources.moinsmoinsmoins ; ToolTipText = "---";
                         break;
                    case -2: img = global::BaseCommonControls.Properties.Resources.moinsmoins; ToolTipText = "--";
                        break;
                    case -1: img = global::BaseCommonControls.Properties.Resources.moins; ToolTipText = "-";
                        break;
                    case 1: img = global::BaseCommonControls.Properties.Resources.plus; ToolTipText = "+";
                        break;
                    case 2: img = global::BaseCommonControls.Properties.Resources.plusplus; ToolTipText = "++";
                        break;
                    case 3: img = global::BaseCommonControls.Properties.Resources.plusplusplus; ToolTipText = "+++";
                        break;
                }



                if (img != null)
                {
                    RectangleF pt = new RectangleF(cellBounds.Location.X + (cellBounds.Width - img.Width) / 2,
                                            cellBounds.Location.Y + (cellBounds.Height - img.Height) / 2,
                                            16,
                                            16);
                    graphics.DrawImage(img, pt);
                }
            }

        }

    }

    public class TheBigCtrlDataGridViewHygieneColumn :
      DataGridViewColumn
    {


        public TheBigCtrlDataGridViewHygieneColumn()
        {
            this.CellTemplate = new TheBigCtrlDataGridViewHygieneCell();
            this.ReadOnly = true;
        }


    }

    #endregion
}
