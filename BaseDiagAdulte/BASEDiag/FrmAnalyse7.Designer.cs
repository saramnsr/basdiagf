namespace BASEDiagAdulte
{
    partial class FrmAnalyse7
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
            System.Windows.Forms.ImageList Bigimgs;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAnalyse7));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("test2", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("test1", 0);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbLevreSupPro = new System.Windows.Forms.RadioButton();
            this.rbLevreSupRetro = new System.Windows.Forms.RadioButton();
            this.rbLevreSupNormo = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbLevreInfPro = new System.Windows.Forms.RadioButton();
            this.rbLevreInfRetro = new System.Windows.Forms.RadioButton();
            this.rbLevreInfNormo = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbMentonPro = new System.Windows.Forms.RadioButton();
            this.rbMentonRetro = new System.Windows.Forms.RadioButton();
            this.rbMentonNormo = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbIncisiveSupPro = new System.Windows.Forms.RadioButton();
            this.rbIncisiveSupRetro = new System.Windows.Forms.RadioButton();
            this.rbIncisiveSupNormo = new System.Windows.Forms.RadioButton();
            this.lvPPT = new System.Windows.Forms.ListView();
            this.SmallImg = new System.Windows.Forms.ImageList(this.components);
            this.lblTitre = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lstBxDiag = new System.Windows.Forms.ListBox();
            this.lstBxObjectifs = new System.Windows.Forms.ListBox();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnRisque = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.BtnPrevious = new System.Windows.Forms.Button();
            this.BTnNext = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ImgProfilSourire = new BASEDiagAdulte.Ctrls.Analyse62();
            this.barrePatient1 = new BASEDiagAdulte.Ctrls.BarrePatient();
            Bigimgs = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Bigimgs
            // 
            Bigimgs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Bigimgs.ImageStream")));
            Bigimgs.TransparentColor = System.Drawing.Color.Transparent;
            Bigimgs.Images.SetKeyName(0, "mediumPPT.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbLevreSupPro);
            this.groupBox1.Controls.Add(this.rbLevreSupRetro);
            this.groupBox1.Controls.Add(this.rbLevreSupNormo);
            this.groupBox1.Location = new System.Drawing.Point(643, 474);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 85);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lèvre Supérieure";
            // 
            // rbLevreSupPro
            // 
            this.rbLevreSupPro.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbLevreSupPro.AutoSize = true;
            this.rbLevreSupPro.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbLevreSupPro.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbLevreSupPro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbLevreSupPro.Image = global::BASEDiagAdulte.Properties.Resources.Pro;
            this.rbLevreSupPro.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbLevreSupPro.Location = new System.Drawing.Point(187, 13);
            this.rbLevreSupPro.Name = "rbLevreSupPro";
            this.rbLevreSupPro.Size = new System.Drawing.Size(73, 73);
            this.rbLevreSupPro.TabIndex = 19;
            this.rbLevreSupPro.TabStop = true;
            this.rbLevreSupPro.UseVisualStyleBackColor = true;
            this.rbLevreSupPro.Click += new System.EventHandler(this.rbLevreInfRetro_CheckedChanged);
            this.rbLevreSupPro.Paint += new System.Windows.Forms.PaintEventHandler(this.rbIncisiveSupPro_Paint);
            // 
            // rbLevreSupRetro
            // 
            this.rbLevreSupRetro.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbLevreSupRetro.AutoSize = true;
            this.rbLevreSupRetro.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbLevreSupRetro.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbLevreSupRetro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbLevreSupRetro.Image = global::BASEDiagAdulte.Properties.Resources.Retro;
            this.rbLevreSupRetro.Location = new System.Drawing.Point(6, 13);
            this.rbLevreSupRetro.Name = "rbLevreSupRetro";
            this.rbLevreSupRetro.Size = new System.Drawing.Size(73, 73);
            this.rbLevreSupRetro.TabIndex = 18;
            this.rbLevreSupRetro.TabStop = true;
            this.rbLevreSupRetro.UseVisualStyleBackColor = true;
            this.rbLevreSupRetro.Click += new System.EventHandler(this.rbLevreInfRetro_CheckedChanged);
            this.rbLevreSupRetro.Paint += new System.Windows.Forms.PaintEventHandler(this.rbIncisiveSupPro_Paint);
            // 
            // rbLevreSupNormo
            // 
            this.rbLevreSupNormo.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbLevreSupNormo.AutoSize = true;
            this.rbLevreSupNormo.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbLevreSupNormo.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbLevreSupNormo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbLevreSupNormo.Image = global::BASEDiagAdulte.Properties.Resources.Normo;
            this.rbLevreSupNormo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbLevreSupNormo.Location = new System.Drawing.Point(100, 13);
            this.rbLevreSupNormo.Name = "rbLevreSupNormo";
            this.rbLevreSupNormo.Size = new System.Drawing.Size(73, 73);
            this.rbLevreSupNormo.TabIndex = 17;
            this.rbLevreSupNormo.TabStop = true;
            this.rbLevreSupNormo.UseVisualStyleBackColor = true;
            this.rbLevreSupNormo.Click += new System.EventHandler(this.rbLevreInfRetro_CheckedChanged);
            this.rbLevreSupNormo.Paint += new System.Windows.Forms.PaintEventHandler(this.rbIncisiveSupPro_Paint);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.rbLevreInfPro);
            this.groupBox2.Controls.Add(this.rbLevreInfRetro);
            this.groupBox2.Controls.Add(this.rbLevreInfNormo);
            this.groupBox2.Location = new System.Drawing.Point(643, 292);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(326, 85);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lèvre Inférieure";
            // 
            // rbLevreInfPro
            // 
            this.rbLevreInfPro.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbLevreInfPro.AutoSize = true;
            this.rbLevreInfPro.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbLevreInfPro.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbLevreInfPro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbLevreInfPro.Image = global::BASEDiagAdulte.Properties.Resources.Pro;
            this.rbLevreInfPro.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbLevreInfPro.Location = new System.Drawing.Point(187, 13);
            this.rbLevreInfPro.Name = "rbLevreInfPro";
            this.rbLevreInfPro.Size = new System.Drawing.Size(73, 73);
            this.rbLevreInfPro.TabIndex = 19;
            this.rbLevreInfPro.TabStop = true;
            this.rbLevreInfPro.UseVisualStyleBackColor = true;
            this.rbLevreInfPro.Click += new System.EventHandler(this.rbLevreInfRetro_CheckedChanged);
            this.rbLevreInfPro.Paint += new System.Windows.Forms.PaintEventHandler(this.rbIncisiveSupPro_Paint);
            // 
            // rbLevreInfRetro
            // 
            this.rbLevreInfRetro.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbLevreInfRetro.AutoSize = true;
            this.rbLevreInfRetro.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbLevreInfRetro.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbLevreInfRetro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbLevreInfRetro.Image = global::BASEDiagAdulte.Properties.Resources.Retro;
            this.rbLevreInfRetro.Location = new System.Drawing.Point(6, 13);
            this.rbLevreInfRetro.Name = "rbLevreInfRetro";
            this.rbLevreInfRetro.Size = new System.Drawing.Size(73, 73);
            this.rbLevreInfRetro.TabIndex = 18;
            this.rbLevreInfRetro.TabStop = true;
            this.rbLevreInfRetro.UseVisualStyleBackColor = true;
            this.rbLevreInfRetro.CheckedChanged += new System.EventHandler(this.rbLevreInfRetro_CheckedChanged);
            this.rbLevreInfRetro.Click += new System.EventHandler(this.rbLevreInfRetro_CheckedChanged);
            this.rbLevreInfRetro.Paint += new System.Windows.Forms.PaintEventHandler(this.rbIncisiveSupPro_Paint);
            // 
            // rbLevreInfNormo
            // 
            this.rbLevreInfNormo.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbLevreInfNormo.AutoSize = true;
            this.rbLevreInfNormo.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbLevreInfNormo.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbLevreInfNormo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbLevreInfNormo.Image = global::BASEDiagAdulte.Properties.Resources.Normo;
            this.rbLevreInfNormo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbLevreInfNormo.Location = new System.Drawing.Point(100, 13);
            this.rbLevreInfNormo.Name = "rbLevreInfNormo";
            this.rbLevreInfNormo.Size = new System.Drawing.Size(73, 73);
            this.rbLevreInfNormo.TabIndex = 17;
            this.rbLevreInfNormo.TabStop = true;
            this.rbLevreInfNormo.UseVisualStyleBackColor = true;
            this.rbLevreInfNormo.Click += new System.EventHandler(this.rbLevreInfRetro_CheckedChanged);
            this.rbLevreInfNormo.Paint += new System.Windows.Forms.PaintEventHandler(this.rbIncisiveSupPro_Paint);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.rbMentonPro);
            this.groupBox3.Controls.Add(this.rbMentonRetro);
            this.groupBox3.Controls.Add(this.rbMentonNormo);
            this.groupBox3.Location = new System.Drawing.Point(643, 383);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(326, 85);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Menton";
            // 
            // rbMentonPro
            // 
            this.rbMentonPro.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbMentonPro.AutoSize = true;
            this.rbMentonPro.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbMentonPro.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbMentonPro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbMentonPro.Image = global::BASEDiagAdulte.Properties.Resources.Pro;
            this.rbMentonPro.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbMentonPro.Location = new System.Drawing.Point(187, 13);
            this.rbMentonPro.Name = "rbMentonPro";
            this.rbMentonPro.Size = new System.Drawing.Size(73, 73);
            this.rbMentonPro.TabIndex = 19;
            this.rbMentonPro.TabStop = true;
            this.rbMentonPro.UseVisualStyleBackColor = true;
            this.rbMentonPro.Click += new System.EventHandler(this.rbLevreInfRetro_CheckedChanged);
            this.rbMentonPro.Paint += new System.Windows.Forms.PaintEventHandler(this.rbIncisiveSupPro_Paint);
            // 
            // rbMentonRetro
            // 
            this.rbMentonRetro.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbMentonRetro.AutoSize = true;
            this.rbMentonRetro.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbMentonRetro.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbMentonRetro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbMentonRetro.Image = global::BASEDiagAdulte.Properties.Resources.Retro;
            this.rbMentonRetro.Location = new System.Drawing.Point(6, 13);
            this.rbMentonRetro.Name = "rbMentonRetro";
            this.rbMentonRetro.Size = new System.Drawing.Size(73, 73);
            this.rbMentonRetro.TabIndex = 18;
            this.rbMentonRetro.TabStop = true;
            this.rbMentonRetro.UseVisualStyleBackColor = true;
            this.rbMentonRetro.Click += new System.EventHandler(this.rbLevreInfRetro_CheckedChanged);
            this.rbMentonRetro.Paint += new System.Windows.Forms.PaintEventHandler(this.rbIncisiveSupPro_Paint);
            // 
            // rbMentonNormo
            // 
            this.rbMentonNormo.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbMentonNormo.AutoSize = true;
            this.rbMentonNormo.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbMentonNormo.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbMentonNormo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbMentonNormo.Image = global::BASEDiagAdulte.Properties.Resources.Normo;
            this.rbMentonNormo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbMentonNormo.Location = new System.Drawing.Point(100, 13);
            this.rbMentonNormo.Name = "rbMentonNormo";
            this.rbMentonNormo.Size = new System.Drawing.Size(73, 73);
            this.rbMentonNormo.TabIndex = 17;
            this.rbMentonNormo.TabStop = true;
            this.rbMentonNormo.UseVisualStyleBackColor = true;
            this.rbMentonNormo.Click += new System.EventHandler(this.rbLevreInfRetro_CheckedChanged);
            this.rbMentonNormo.Paint += new System.Windows.Forms.PaintEventHandler(this.rbIncisiveSupPro_Paint);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.rbIncisiveSupPro);
            this.groupBox4.Controls.Add(this.rbIncisiveSupRetro);
            this.groupBox4.Controls.Add(this.rbIncisiveSupNormo);
            this.groupBox4.Location = new System.Drawing.Point(643, 565);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(326, 85);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Incisive Supérieure";
            // 
            // rbIncisiveSupPro
            // 
            this.rbIncisiveSupPro.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbIncisiveSupPro.AutoSize = true;
            this.rbIncisiveSupPro.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbIncisiveSupPro.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbIncisiveSupPro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbIncisiveSupPro.Image = global::BASEDiagAdulte.Properties.Resources.Pro;
            this.rbIncisiveSupPro.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbIncisiveSupPro.Location = new System.Drawing.Point(187, 13);
            this.rbIncisiveSupPro.Name = "rbIncisiveSupPro";
            this.rbIncisiveSupPro.Size = new System.Drawing.Size(73, 73);
            this.rbIncisiveSupPro.TabIndex = 19;
            this.rbIncisiveSupPro.TabStop = true;
            this.rbIncisiveSupPro.UseVisualStyleBackColor = true;
            this.rbIncisiveSupPro.Click += new System.EventHandler(this.rbLevreInfRetro_CheckedChanged);
            this.rbIncisiveSupPro.Paint += new System.Windows.Forms.PaintEventHandler(this.rbIncisiveSupPro_Paint);
            // 
            // rbIncisiveSupRetro
            // 
            this.rbIncisiveSupRetro.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbIncisiveSupRetro.AutoSize = true;
            this.rbIncisiveSupRetro.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbIncisiveSupRetro.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbIncisiveSupRetro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbIncisiveSupRetro.Image = global::BASEDiagAdulte.Properties.Resources.Retro;
            this.rbIncisiveSupRetro.Location = new System.Drawing.Point(6, 13);
            this.rbIncisiveSupRetro.Name = "rbIncisiveSupRetro";
            this.rbIncisiveSupRetro.Size = new System.Drawing.Size(73, 73);
            this.rbIncisiveSupRetro.TabIndex = 18;
            this.rbIncisiveSupRetro.TabStop = true;
            this.rbIncisiveSupRetro.UseVisualStyleBackColor = true;
            this.rbIncisiveSupRetro.Click += new System.EventHandler(this.rbLevreInfRetro_CheckedChanged);
            this.rbIncisiveSupRetro.Paint += new System.Windows.Forms.PaintEventHandler(this.rbIncisiveSupPro_Paint);
            // 
            // rbIncisiveSupNormo
            // 
            this.rbIncisiveSupNormo.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbIncisiveSupNormo.AutoSize = true;
            this.rbIncisiveSupNormo.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rbIncisiveSupNormo.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbIncisiveSupNormo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbIncisiveSupNormo.Image = global::BASEDiagAdulte.Properties.Resources.Normo;
            this.rbIncisiveSupNormo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbIncisiveSupNormo.Location = new System.Drawing.Point(100, 13);
            this.rbIncisiveSupNormo.Name = "rbIncisiveSupNormo";
            this.rbIncisiveSupNormo.Size = new System.Drawing.Size(73, 73);
            this.rbIncisiveSupNormo.TabIndex = 17;
            this.rbIncisiveSupNormo.TabStop = true;
            this.rbIncisiveSupNormo.UseVisualStyleBackColor = true;
            this.rbIncisiveSupNormo.Click += new System.EventHandler(this.rbLevreInfRetro_CheckedChanged);
            this.rbIncisiveSupNormo.Paint += new System.Windows.Forms.PaintEventHandler(this.rbIncisiveSupPro_Paint);
            // 
            // lvPPT
            // 
            this.lvPPT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPPT.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.lvPPT.LargeImageList = Bigimgs;
            this.lvPPT.Location = new System.Drawing.Point(56, 603);
            this.lvPPT.Name = "lvPPT";
            this.lvPPT.Size = new System.Drawing.Size(413, 51);
            this.lvPPT.SmallImageList = this.SmallImg;
            this.lvPPT.TabIndex = 27;
            this.lvPPT.UseCompatibleStateImageBehavior = false;
            this.lvPPT.View = System.Windows.Forms.View.SmallIcon;
            this.lvPPT.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvPPT_MouseDoubleClick);
            // 
            // SmallImg
            // 
            this.SmallImg.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("SmallImg.ImageStream")));
            this.SmallImg.TransparentColor = System.Drawing.Color.Transparent;
            this.SmallImg.Images.SetKeyName(0, "VerySmallPPT.png");
            // 
            // lblTitre
            // 
            this.lblTitre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitre.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.Location = new System.Drawing.Point(-3, 67);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(640, 23);
            this.lblTitre.TabIndex = 29;
            this.lblTitre.Text = "Profil";
            this.lblTitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lstBxDiag, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lstBxObjectifs, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(643, 67);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(326, 219);
            this.tableLayoutPanel1.TabIndex = 31;
            // 
            // lstBxDiag
            // 
            this.lstBxDiag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxDiag.FormattingEnabled = true;
            this.lstBxDiag.Location = new System.Drawing.Point(3, 3);
            this.lstBxDiag.Name = "lstBxDiag";
            this.lstBxDiag.Size = new System.Drawing.Size(320, 103);
            this.lstBxDiag.TabIndex = 18;
            this.lstBxDiag.SelectedIndexChanged += new System.EventHandler(this.lstBxDiag_SelectedIndexChanged);
            // 
            // lstBxObjectifs
            // 
            this.lstBxObjectifs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstBxObjectifs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstBxObjectifs.FormattingEnabled = true;
            this.lstBxObjectifs.Location = new System.Drawing.Point(3, 112);
            this.lstBxObjectifs.Name = "lstBxObjectifs";
            this.lstBxObjectifs.Size = new System.Drawing.Size(320, 104);
            this.lstBxObjectifs.TabIndex = 19;
            this.lstBxObjectifs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstBxObjectifs_MouseClick);
            this.lstBxObjectifs.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstBxObjectifs_DrawItem);
            // 
            // btnclose
            // 
            this.btnclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Image = global::BASEDiagAdulte.Properties.Resources.retour1;
            this.btnclose.Location = new System.Drawing.Point(919, 7);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(50, 50);
            this.btnclose.TabIndex = 23;
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnRisque
            // 
            this.btnRisque.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRisque.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRisque.Image = global::BASEDiagAdulte.Properties.Resources.Risques;
            this.btnRisque.Location = new System.Drawing.Point(475, 603);
            this.btnRisque.Name = "btnRisque";
            this.btnRisque.Size = new System.Drawing.Size(50, 50);
            this.btnRisque.TabIndex = 30;
            this.btnRisque.UseVisualStyleBackColor = true;
            this.btnRisque.Click += new System.EventHandler(this.btnRisque_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::BASEDiagAdulte.Properties.Resources.changeecran;
            this.button1.Location = new System.Drawing.Point(863, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 50);
            this.button1.TabIndex = 28;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BtnPrevious
            // 
            this.BtnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPrevious.ForeColor = System.Drawing.Color.Black;
            this.BtnPrevious.Image = global::BASEDiagAdulte.Properties.Resources.Previous;
            this.BtnPrevious.Location = new System.Drawing.Point(0, 603);
            this.BtnPrevious.Name = "BtnPrevious";
            this.BtnPrevious.Size = new System.Drawing.Size(50, 54);
            this.BtnPrevious.TabIndex = 26;
            this.BtnPrevious.UseVisualStyleBackColor = true;
            this.BtnPrevious.Click += new System.EventHandler(this.BtnPrevious_Click);
            // 
            // BTnNext
            // 
            this.BTnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTnNext.ForeColor = System.Drawing.Color.Black;
            this.BTnNext.Image = global::BASEDiagAdulte.Properties.Resources.Next;
            this.BTnNext.Location = new System.Drawing.Point(587, 603);
            this.BTnNext.Name = "BTnNext";
            this.BTnNext.Size = new System.Drawing.Size(50, 50);
            this.BTnNext.TabIndex = 25;
            this.BTnNext.UseVisualStyleBackColor = true;
            this.BTnNext.Click += new System.EventHandler(this.BTnNext_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Image = global::BASEDiagAdulte.Properties.Resources.Imprimer;
            this.btnPrint.Location = new System.Drawing.Point(531, 603);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(50, 50);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-2, 588);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 12);
            this.label1.TabIndex = 35;
            this.label1.Text = "Appuyer sur \'H\' pour voir l\'aide";
            // 
            // ImgProfilSourire
            // 
            this.ImgProfilSourire.AllowDrop = true;
            this.ImgProfilSourire.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ImgProfilSourire.AngleDeRotationPhoto = 0F;
            this.ImgProfilSourire.AngleDeRotationRadio = 0F;
            this.ImgProfilSourire.Brightness = 0F;
            this.ImgProfilSourire.Contraste = 1F;
            this.ImgProfilSourire.DrawPointName = false;
            this.ImgProfilSourire.file = null;
            this.ImgProfilSourire.HelpFolder = ".\\";
            this.ImgProfilSourire.HelpImage = null;
            this.ImgProfilSourire.Location = new System.Drawing.Point(0, 93);
            this.ImgProfilSourire.Name = "ImgProfilSourire";
            this.ImgProfilSourire.PhotoImage = null;
            this.ImgProfilSourire.RadioImage = null;
            this.ImgProfilSourire.ReadOnly = false;
            this.ImgProfilSourire.RotationPointInPhoto = new System.Drawing.Point(0, 0);
            this.ImgProfilSourire.RotationPointInRadio = new System.Drawing.Point(0, 0);
            this.ImgProfilSourire.Size = new System.Drawing.Size(637, 504);
            this.ImgProfilSourire.Synchronized = false;
            this.ImgProfilSourire.TabIndex = 1;
            this.ImgProfilSourire.TextIfNoImage = "Pas d\'image";
            this.ImgProfilSourire.Transparence = 0.99D;
            this.ImgProfilSourire.zoomPhoto = 1F;
            this.ImgProfilSourire.zoomRadio = 1F;
            this.ImgProfilSourire.OnRadioChanged += new System.EventHandler(this.ImgProfilSourire_OnRadioChanged);
            // 
            // barrePatient1
            // 
            this.barrePatient1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.barrePatient1.BackColor = System.Drawing.Color.White;
            this.barrePatient1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barrePatient1.Location = new System.Drawing.Point(0, 7);
            this.barrePatient1.Name = "barrePatient1";
            this.barrePatient1.patient = null;
            this.barrePatient1.Size = new System.Drawing.Size(857, 54);
            this.barrePatient1.TabIndex = 14;
            // 
            // FrmAnalyse6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(969, 666);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnRisque);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lvPPT);
            this.Controls.Add(this.ImgProfilSourire);
            this.Controls.Add(this.BtnPrevious);
            this.Controls.Add(this.BTnNext);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.barrePatient1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAnalyse6";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Analyse du Profil";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAnalyse6_FormClosing);
            this.Load += new System.EventHandler(this.FrmAnalyse6_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BASEDiagAdulte.Ctrls.BarrePatient barrePatient1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbLevreSupPro;
        private System.Windows.Forms.RadioButton rbLevreSupRetro;
        private System.Windows.Forms.RadioButton rbLevreSupNormo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbLevreInfPro;
        private System.Windows.Forms.RadioButton rbLevreInfRetro;
        private System.Windows.Forms.RadioButton rbLevreInfNormo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbMentonPro;
        private System.Windows.Forms.RadioButton rbMentonRetro;
        private System.Windows.Forms.RadioButton rbMentonNormo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbIncisiveSupPro;
        private System.Windows.Forms.RadioButton rbIncisiveSupRetro;
        private System.Windows.Forms.RadioButton rbIncisiveSupNormo;
        private BASEDiagAdulte.Ctrls.Analyse62 ImgProfilSourire;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button BtnPrevious;
        private System.Windows.Forms.Button BTnNext;
        private System.Windows.Forms.ListView lvPPT;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Button btnRisque;
        private System.Windows.Forms.ImageList SmallImg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox lstBxDiag;
        private System.Windows.Forms.ListBox lstBxObjectifs;
        private System.Windows.Forms.Label label1;
    }
}