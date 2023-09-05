using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BO;
using BASEDiag_BL;
using BasCommon_BL;
using BasCommon_BO;

namespace BASEDiag.Ctrls
{
    public partial class BarrePatient : UserControl
    {

        private basePatient _CurrentPatient;
        public basePatient patient
        {
            get
            {
                return _CurrentPatient;
            }
            set
            {
                _CurrentPatient = value;
                Invalidate();
                
            }
        }

       

        public BarrePatient()
        {
            InitializeComponent();
            
        }

        private void BarrePatient_Paint(object sender, PaintEventArgs e)
        {
            if (_CurrentPatient == null) return;
            Font ft = new Font("Garamond", 12, FontStyle.Regular);


            int y;
            int m;
            int d;
            _CurrentPatient.AgeToDate(DateTime.Now, out y, out m, out d);

            string PatientName = _CurrentPatient.ToString() +" "+y.ToString()+" ans et "+m.ToString()+" mois";
            SizeF PatientNameSize = e.Graphics.MeasureString(PatientName, ft);


            string CorrespondantNames = "";

            foreach (LienCorrespondant lc in _CurrentPatient.Correspondants)
            {
                if (lc.IsProfessionnel)
                    CorrespondantNames += lc.ToString() + "(" + lc.Lien + ")";
            }
            CorrespondantNames = CorrespondantNames != "" ? "Correspondants : " + CorrespondantNames : "";

            SizeF CorrespondantNamesSize = e.Graphics.MeasureString(CorrespondantNames, ft);

            string RecoNames = "";

            foreach (LienCorrespondant lc in _CurrentPatient.RecoBy)
            {
                RecoNames += lc.ToString();
            }
            RecoNames = RecoNames != "" ? "Recommandé par : " + RecoNames : "";

            SizeF RecoNamesSize = e.Graphics.MeasureString(RecoNames, ft);



            string firstline = "";
            string secondline = "";

            if (PatientNameSize.Width + CorrespondantNamesSize.Width + RecoNamesSize.Width < this.Width)
            {
                firstline = PatientName;
                firstline += RecoNames != "" ? "," + RecoNames : "";
                firstline += CorrespondantNames != "" ? "," + CorrespondantNames : "";
                secondline = "";
            }else
                if (PatientNameSize.Width + CorrespondantNamesSize.Width < this.Width)
                {
                    firstline = PatientName;
                    firstline += RecoNames != "" ? "," + RecoNames : "";
                    secondline = CorrespondantNames;
                }else
                    {
                        firstline = PatientName;
                        secondline += RecoNames != "" ? "," + RecoNames : "";
                        secondline += CorrespondantNames != "" ? "," + CorrespondantNames : "";
                        
                    }
            
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            e.Graphics.DrawString(firstline + "\n" + secondline, ft, Brushes.Black, this.Bounds, sf);






            
            
            
        }

        private void iPhoneListBox1_Load(object sender, EventArgs e)
        {
        }
    }
}
