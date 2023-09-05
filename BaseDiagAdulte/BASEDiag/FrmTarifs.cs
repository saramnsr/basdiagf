using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BO;
using BASEDiag_BL;

namespace BASEDiag
{
    public partial class FrmTarifs : Form
    {


        private Patient _patient;
        public Patient patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
            }
        }



        public FrmTarifs()
        {
            InitializeComponent();
        }

        private void FrmTarifs_Load(object sender, EventArgs e)
        {

        }
    }
}
