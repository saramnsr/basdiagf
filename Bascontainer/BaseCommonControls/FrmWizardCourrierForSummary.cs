﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using BasCommon_BO;
using System.IO;

namespace BaseCommonControls
{
    public partial class FrmWizardCourrierForSummary : Form
    {


        private string _folder;
        public string folder
        {
            get
            {
                return _folder;
            }
            set
            {
                _folder = value;
            }
        }

        private basePatient _patient;
        public basePatient patient
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
        
       
        private string _SelectedFile;
        public string FileName
        {
            get
            {
                return _SelectedFile;
            }
            set
            {
                _SelectedFile = value;
            }
        }


        public FrmWizardCourrierForSummary(string folder)
        {
            _folder = folder;
            InitializeComponent();
        }

        

        private void FrmWizardCourrier_Load(object sender, EventArgs e)
        {
            
            BuildDossierTemplate(folder, null);
            tvTemplate.RecalculEmplacementButtons(true);
            RefreshColorButtons();
                
            
        }

        private void RefreshColorButtons()
        {
            foreach (Ctrls.BO.trButton btn in tvTemplate.ButtonList)
            {
                if (((TreeNode)btn.Tag).Tag is DirectoryInfo)
                    btn.Color = Color.Gray;

                if (((TreeNode)btn.Tag).Tag is FileInfo)
                    btn.Color = Color.LightGray;
            }
        }

        void tvTemplate_OnChangeLevel(object sender, EventArgs e)
        {
            RefreshColorButtons();
        }

        void tvTemplate_OnSelected(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (((TreeNode)sender).Tag is FileInfo)
                {
                    FileName = ((FileInfo)((TreeNode)sender).Tag).FullName;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (SystemException)
            {

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void BuildDossierTemplate(string folder, TreeNode nd)
        {
            if (nd == null) nd = tvTemplate.Root;
            if (Directory.Exists(folder))
            {

                string[] ss = Directory.GetDirectories(folder);


                Array.Sort(ss);

                foreach (string s in ss)
                {
                    DirectoryInfo nfo = new DirectoryInfo(s);
                    string txt = nfo.Name;
                    string[] sss = txt.Split('.');

                    txt = sss[sss.Length - 1];

                    TreeNode n = nd.Nodes.Add(txt);
                    n.Tag = nfo;
                    BuildDossierTemplate(s, n);
                }

                List<string> lst = Directory.GetFiles(folder, "*.bvm").ToList();
                lst.Sort();

                foreach (string s in lst)
                {
                    FileInfo nfo = new FileInfo(s);

                    string txt = nfo.Name.Replace(".bvm", "");
                    string[] sss = txt.Split('.');

                    txt = sss[sss.Length - 1];


                    TreeNode n = nd.Nodes.Add(txt);
                    n.Tag = nfo;
                }
            }
        }

        

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

      
    }
}
