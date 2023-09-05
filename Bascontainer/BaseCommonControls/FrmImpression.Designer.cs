namespace BaseCommonControls
{
    partial class FrmImpression
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.DataTableTraitement2BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSetTK = new BaseCommonControls.DataSetTK();
            this.DataTableEcheanceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataTableTraitementBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.DataTableTraitement2BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetTK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTableEcheanceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTableTraitementBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // DataTableTraitement2BindingSource
            // 
            this.DataTableTraitement2BindingSource.DataSource = this.DataSetTK;
            this.DataTableTraitement2BindingSource.Position = 0;
            // 
            // DataSetTK
            // 
            this.DataSetTK.DataSetName = "DataSetTK";
            this.DataSetTK.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // DataTableEcheanceBindingSource
            // 
            this.DataTableEcheanceBindingSource.DataMember = "DataTableEcheance";
            this.DataTableEcheanceBindingSource.DataSource = this.DataSetTK;
            // 
            // DataTableTraitementBindingSource
            // 
            this.DataTableTraitementBindingSource.DataMember = "DataTableTraitement";
            this.DataTableTraitementBindingSource.DataSource = this.DataSetTK;
            // 
            // reportViewer1
            // 
            this.reportViewer1.AccessibleDescription = "";
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            reportDataSource1.Name = "DataTableTraitement";
            reportDataSource1.Value = this.DataTableTraitement2BindingSource;
            reportDataSource2.Name = "DataTableEcheance";
            reportDataSource2.Value = this.DataTableEcheanceBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "BaseCommonControls.ReportTK.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.reportViewer1.ShowBackButton = false;
            this.reportViewer1.ShowDocumentMapButton = false;
            this.reportViewer1.ShowFindControls = false;
            this.reportViewer1.ShowParameterPrompts = false;
            this.reportViewer1.ShowStopButton = false;
            this.reportViewer1.Size = new System.Drawing.Size(892, 318);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.UseWaitCursor = true;
            this.reportViewer1.Print += new Microsoft.Reporting.WinForms.ReportPrintEventHandler(this.reportViewer1_Print);
            this.reportViewer1.PrintingBegin += new Microsoft.Reporting.WinForms.ReportPrintEventHandler(this.reportViewer1_PrintingBegin);
            // 
            // FrmImpression
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 318);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmImpression";
            this.Text = "Devis";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmImpression_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataTableTraitement2BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetTK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTableEcheanceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataTableTraitementBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource DataTableTraitementBindingSource;
        private DataSetTK DataSetTK;
        private System.Windows.Forms.BindingSource DataTableTraitement2BindingSource;
        private System.Windows.Forms.BindingSource DataTableEcheanceBindingSource;
    }
}