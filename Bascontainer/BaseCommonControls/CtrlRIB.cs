using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BL;

namespace BaseCommonControls
{
    public partial class CtrlRIB : UserControl
    {


        private string _RIB;
        public string RIB
        {
            get
            {
                return CodeBanque + CodeGuichet + NumCompte + CleRIB;

            }
        }

        public string CleRIB
        {
            get
            {
                return mTxtCle.Text;
            }  
            set
            {
                mTxtCle.Text = value;
            }  
        }

        public string NumCompte
        {
            get
            {
                return mTxtNumCpt.Text;
            }
            set
            {
                mTxtNumCpt.Text = value;
            }  
        }

        public string CodeGuichet
        {
            get
            {
                return mTxtGuichet.Text;
            }
            set
            {
                mTxtGuichet.Text = value;
            }  
        }

        public string CodeBanque
        {
            get
            {
                return mTxtNumBque.Text;
            }
            set
            {
                mTxtNumBque.Text = value;
            }  
        }

        public bool IsValid
        {
            get
            {
                string rib = CodeBanque + CodeGuichet + NumCompte + CleRIB;
                return BanqueMgmt.IsValidRib(rib);
            }
        }

        public CtrlRIB()
        {
            InitializeComponent();
        }

        private void checkvalidity()
        {
            Color validcolor = Color.LawnGreen;

            if (!IsValid) validcolor = Color.LightSalmon;

            mTxtNumBque.BackColor = validcolor;
            mTxtGuichet.BackColor = validcolor;
            mTxtNumCpt.BackColor = validcolor;
            mTxtCle.BackColor = validcolor;

        }

        private void mTxtNumBque_KeyDown(object sender, KeyEventArgs e)
        {
            
            
        }

        private void mTxtGuichet_KeyDown(object sender, KeyEventArgs e)
        {


          
        }

        private void mTxtNumCpt_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void mTxtCle_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void mTxtNumBque_KeyUp(object sender, KeyEventArgs e)
        {
            if (mTxtNumBque.MaskCompleted) { checkvalidity(); mTxtGuichet.Focus(); mTxtGuichet.SelectAll(); }
        }

        private void mTxtGuichet_KeyUp(object sender, KeyEventArgs e)
        {
            if (mTxtGuichet.MaskCompleted) { checkvalidity(); mTxtNumCpt.Focus(); mTxtNumCpt.SelectAll(); }
        }

        private void mTxtNumCpt_KeyUp(object sender, KeyEventArgs e)
        {
            if (mTxtNumCpt.MaskCompleted) { checkvalidity(); mTxtCle.Focus(); mTxtCle.SelectAll(); }

        }

        private void mTxtCle_KeyUp(object sender, KeyEventArgs e)
        {
            checkvalidity();
        }
    }
}
