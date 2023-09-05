using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BL;
using BasCommon_BO;
using System.Security.Cryptography;

namespace BaseCommonControls
{
    public partial class FrmAccessREquest : Form
    {

        private AccessObject _Value;
        public AccessObject Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }
        public FrmAccessREquest()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            

            if (txtbxPassword.Text != "")
            {
                string[] ss = txtbxPassword.Text.Split(':');
                Value = AccessMgmt.GetAccessObj(ss[0]);

                if (ss.Length > 1)
                {
                    if (ss[1] == "CNOff")
                        MgmtEncaissement.includeCN = true;
                    if (ss[1] == "CNOn")
                        MgmtEncaissement.includeCN = false;
                }
                

                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            else
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                Close();
            }
        }

        private void txtbxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmAccessREquest_Shown(object sender, EventArgs e)
        {
            txtbxPassword.Focus();
        }

        private void FrmAccessREquest_Load(object sender, EventArgs e)
        {

        }


        private static void CheckUserIsNull()
        {
            if (UtilisateursMgt.CurrentUtilisateur == null)
            {
                AskForUser();
            }
        }

        public static void AskForUser()
        {
            BaseCommonControls.FrmAccessREquest frm = new BaseCommonControls.FrmAccessREquest();
            frm.ShowDialog();
            if (frm.Value != null) UtilisateursMgt.CurrentUtilisateur = frm.Value;
            else ClearUser();
        }

        private static void ClearUser()
        {
            UtilisateursMgt.CurrentUtilisateur = null;
        }

        public static bool Check_BasPract_ListControles()
        {
            CheckUserIsNull();

            if (UtilisateursMgt.CurrentUtilisateur == null) return false;
            else return UtilisateursMgt.CurrentUtilisateur.BasPract_ListControles;
        }

        public static bool Check_BasPract_ListFinancieres()
        {
            CheckUserIsNull();

            if (UtilisateursMgt.CurrentUtilisateur == null) return false;
            else return UtilisateursMgt.CurrentUtilisateur.BasPract_ListFinancieres;
        }

        public static bool Check_Bas_Stat_AllowFinances()
        {
            CheckUserIsNull();

            if (UtilisateursMgt.CurrentUtilisateur == null) return false;
            else return UtilisateursMgt.CurrentUtilisateur.Bas_Stat_AllowFinances;
        }

        public static bool Check_BasPract_Comptabilite()
        {
            CheckUserIsNull();

            if (UtilisateursMgt.CurrentUtilisateur == null) return false;
            else return UtilisateursMgt.CurrentUtilisateur.BasPract_Comptabilite;
        }



        public static bool Check_CanDeleteEncaissement()
        {
            CheckUserIsNull();

            if (UtilisateursMgt.CurrentUtilisateur == null) return false;
            else return UtilisateursMgt.CurrentUtilisateur.CanDeleteEncaissement;
        }

        public static bool Check_CanDeleteActe()
        {
            CheckUserIsNull();

            if (UtilisateursMgt.CurrentUtilisateur == null) return false;
            else return UtilisateursMgt.CurrentUtilisateur.CanDeleteActe;
        }

        public static bool Check_BasPract_HistoriqueFinances()
        {
            CheckUserIsNull();

            if (UtilisateursMgt.CurrentUtilisateur == null) return false;
            else return UtilisateursMgt.CurrentUtilisateur.BasPract_HistoriqueFinances;
        }

        public static bool CheckUser()
        {
            CheckUserIsNull();

            if (UtilisateursMgt.CurrentUtilisateur == null) return false;
            else return true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UtilisateursMgt.ClearCurrentUser();
            Value = null;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        #region Cryptage et decryptage// Methode de Cryptagepublic 
        static string cryptPassword(string password)
        {string hash = "f0xle@rn";byte[] data = UTF8Encoding.UTF8.GetBytes(password);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                Byte[] Keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripleDesc = new TripleDESCryptoServiceProvider() 
                { Key = Keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {ICryptoTransform transform = tripleDesc.CreateEncryptor();
                    byte[] resultat = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(resultat, 0, resultat.Length);}}}
        //Methode de Decryptage
        public static string decryptPassword(string cryptedPassword){
            string hash = "f0xle@rn";byte[] data = Convert.FromBase64String(cryptedPassword);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider()){
                Byte[] Keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripleDesc = new TripleDESCryptoServiceProvider() {
                    Key = Keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {ICryptoTransform transform = tripleDesc.CreateDecryptor();
                    byte[] resultat = transform.TransformFinalBlock(data, 0, data.Length);
                    return UTF8Encoding.UTF8.GetString(resultat);}}}
        #endregion
        // cette methode permet de verifier l'existance du user dont le mot de passe est recuperé depuis la clé USB
        public void ConnectFromUSB(string cryptedPassword) {
            if (cryptedPassword == null) return;
            string decryptedPassword = decryptPassword(cryptedPassword);
            string[] ss = decryptedPassword.Split(':');
            Value = AccessMgmt.GetAccessObj(ss[0]);
            if (ss.Length > 1){if (ss[1] == "CNOff")
                MgmtEncaissement.includeCN = true;
                if (ss[1] == "CNOn")MgmtEncaissement.includeCN = false;
            }
            if(Value!=null)
                UtilisateursMgt.CurrentUtilisateur = this.Value;
        }
    }
}
