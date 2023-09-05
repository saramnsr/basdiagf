using BasCommon_BL;
using BasCommon_BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BASEDiagAdulte
{
    public partial class Browser : Form
    {
        private basePatient _CurrentPatient;
        public basePatient CurrentPatient
        {
            get
            {
                return _CurrentPatient;
            }
            set
            {
                _CurrentPatient = value;
            }
        }
        bool init = true;
        String Url = string.Empty;
        public Browser(basePatient pat)
        {
            InitializeComponent();
            Url = "https://smilers.biotech-dental.com/fr/login";
            CurrentPatient = pat;
            myBrowser();


        }

        private void Browser_Load(object sender, EventArgs e)
        {
            toolStripButton1.Enabled = false;
            toolStripButton2.Enabled = false;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            myBrowser();
        }

        private void myBrowser()
        {
            //if (toolStripComboBox1.Text != "")
            //    Url = toolStripComboBox1.Text;
            webBrowser1.ScriptErrorsSuppressed = true;

            webBrowser1.Navigate(Url);
            webBrowser1.ProgressChanged += new WebBrowserProgressChangedEventHandler(webpage_ProgressChanged);
            webBrowser1.DocumentTitleChanged += new EventHandler(webpage_DocumentTitleChanged);
            webBrowser1.StatusTextChanged += new EventHandler(webpage_StatusTextChanged);
            webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(webpage_Navigated);
            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webpage_DocumentCompleted);

        }

        private void webpage_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string username = System.Configuration.ConfigurationManager.AppSettings["SmilersLogin"];
            string password = System.Configuration.ConfigurationManager.AppSettings["SmilersPassword"];

            if (webBrowser1.Url.ToString() == "https://smilers.biotech-dental.com/fr/login")
            {
                this.webBrowser1.Document.All["_username"].SetAttribute("value", username);
                this.webBrowser1.Document.All["_password"].SetAttribute("value", password);
                //this.Web.Document.All["ckeckbox"].SetAttribute("checked", "checked");
                HtmlElementCollection elc = this.webBrowser1.Document.GetElementsByTagName("input");
                foreach (HtmlElement el in elc)
                {
                    if (el.GetAttribute("type").Equals("submit"))
                    {
                        el.InvokeMember("Click");
                    }
                }
            }
            else
            {
                if (webBrowser1.Url.ToString().Contains("/DS") && (CurrentPatient.infoSmilers == null || CurrentPatient.infoSmilers.numdossier == null || CurrentPatient.infoSmilers.numdossier == ""))
                {
                    if (MessageBox.Show("Vous voulez enregistrer le numéro de dossier patient", "Enregistrer numéro dossier patient", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (CurrentPatient.infoSmilers == null) CurrentPatient.infoSmilers = new InfoSmilers();

                        CurrentPatient.infoSmilers.idPatient = CurrentPatient.Id;
                        CurrentPatient.infoSmilers.numdossier = webBrowser1.Url.Segments[webBrowser1.Url.Segments.Length - 1];
                        SmilersMgmt.insertSmiler(CurrentPatient.infoSmilers);
                        init = true;
                    }
                }
                else
                    if (init || CurrentPatient.infoSmilers != null)
                    {

                        if (CurrentPatient.infoSmilers != null)
                        {
                            if (CurrentPatient.infoSmilers.numdossier != "" && Url != "https://smilers.biotech-dental.com/fr/treatments/" + CurrentPatient.infoSmilers.numdossier)
                            {
                                Url = "https://smilers.biotech-dental.com/fr/treatments/" + CurrentPatient.infoSmilers.numdossier;
                                myBrowser();
                            }
                            else
                            {
                                if (CurrentPatient.infoSmilers.orderid >= 0 && (CurrentPatient.infoSmilers.numdossier == null || CurrentPatient.infoSmilers.numdossier == "") && Url != "https://smilers.biotech-dental.com/fr/treatment/create-treatment/" + CurrentPatient.infoSmilers.orderid)
                                {
                                    Url = "https://smilers.biotech-dental.com/fr/treatment/create-treatment/" + CurrentPatient.infoSmilers.orderid;
                                    myBrowser();
                                }
                            }
                        }
                        else
                        {

                            HtmlElementCollection elc = this.webBrowser1.Document.GetElementsByTagName("input");
                            foreach (HtmlElement el in elc)
                            {
                                if (el.GetAttribute("type").Equals("search"))
                                {
                                    el.SetAttribute("value", CurrentPatient.Nom);
                                    //HtmlElement btnElement =  this.webBrowser1.Document.GetElementById("search-btn");
                                    //btnElement.InvokeMember("click");
                                    Url = "https://smilers.biotech-dental.com/fr/pract/index?page=1&input=" + CurrentPatient.Nom + "&sortField=orderName&sortType=asc&product=&treatmentTypeFilter%5B%5D=traitements-etudes-encours&treatmentTypeFilter%5B%5D=traitements-encours&treatmentTypeFilter%5B%5D=traitements-archives";
                                    myBrowser();
                                }
                            }
                        }
                        init = false;

                    }


            }

            if (webBrowser1.CanGoBack) toolStripButton1.Enabled = true;
            else toolStripButton1.Enabled = false;

            if (webBrowser1.CanGoForward) toolStripButton2.Enabled = true;
            else toolStripButton2.Enabled = false;
            toolStripStatusLabel1.Text = "Done";
        }

        private void webpage_DocumentTitleChanged(object sender, EventArgs e)
        {
            this.Text = webBrowser1.DocumentTitle.ToString();
        }
        private void webpage_StatusTextChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = webBrowser1.StatusText;
        }

        private void webpage_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Maximum = (int)e.MaximumProgress;
            toolStripProgressBar1.Value = ((int)e.CurrentProgress < 0 || (int)e.MaximumProgress < (int)e.CurrentProgress) ? (int)e.MaximumProgress : (int)e.CurrentProgress;
        }

        private void webpage_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            toolStripComboBox1.Text = webBrowser1.Url.ToString();

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintPreviewDialog();
        }
    }
}

