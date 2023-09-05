namespace BASEDiagAdulte
{
    partial class FrmDuree
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
            this.pnlStep1 = new System.Windows.Forms.Panel();
            this.rb1PhasePediatrie = new System.Windows.Forms.RadioButton();
            this.rb2Phases = new System.Windows.Forms.RadioButton();
            this.rb1Phase = new System.Windows.Forms.RadioButton();
            this.pnl1Phase = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rb1Semestre = new System.Windows.Forms.RadioButton();
            this.rb3Semestres = new System.Windows.Forms.RadioButton();
            this.rb2Semestres = new System.Windows.Forms.RadioButton();
            this.rb6Semestres = new System.Windows.Forms.RadioButton();
            this.rb4Semestres = new System.Windows.Forms.RadioButton();
            this.rb5Semestres = new System.Windows.Forms.RadioButton();
            this.pnl2Phase1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.pnl2Phases2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton11 = new System.Windows.Forms.RadioButton();
            this.radioButton12 = new System.Windows.Forms.RadioButton();
            this.pnlStep1.SuspendLayout();
            this.pnl1Phase.SuspendLayout();
            this.pnl2Phase1.SuspendLayout();
            this.pnl2Phases2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlStep1
            // 
            this.pnlStep1.BackColor = System.Drawing.Color.White;
            this.pnlStep1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStep1.Controls.Add(this.rb1PhasePediatrie);
            this.pnlStep1.Controls.Add(this.rb2Phases);
            this.pnlStep1.Controls.Add(this.rb1Phase);
            this.pnlStep1.Location = new System.Drawing.Point(12, 12);
            this.pnlStep1.Name = "pnlStep1";
            this.pnlStep1.Size = new System.Drawing.Size(436, 84);
            this.pnlStep1.TabIndex = 0;
            // 
            // rb1PhasePediatrie
            // 
            this.rb1PhasePediatrie.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb1PhasePediatrie.AutoSize = true;
            this.rb1PhasePediatrie.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rb1PhasePediatrie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb1PhasePediatrie.Image = global::BASEDiagAdulte.Properties.Resources._1PhasePediatrie;
            this.rb1PhasePediatrie.Location = new System.Drawing.Point(251, 3);
            this.rb1PhasePediatrie.Name = "rb1PhasePediatrie";
            this.rb1PhasePediatrie.Size = new System.Drawing.Size(73, 73);
            this.rb1PhasePediatrie.TabIndex = 2;
            this.rb1PhasePediatrie.TabStop = true;
            this.rb1PhasePediatrie.UseVisualStyleBackColor = true;
            this.rb1PhasePediatrie.Paint += new System.Windows.Forms.PaintEventHandler(this.rb2Semestres_Paint);
            this.rb1PhasePediatrie.CheckedChanged += new System.EventHandler(this.rb1Phase_CheckedChanged);
            // 
            // rb2Phases
            // 
            this.rb2Phases.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb2Phases.AutoSize = true;
            this.rb2Phases.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rb2Phases.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb2Phases.Image = global::BASEDiagAdulte.Properties.Resources._2Phases;
            this.rb2Phases.Location = new System.Drawing.Point(172, 3);
            this.rb2Phases.Name = "rb2Phases";
            this.rb2Phases.Size = new System.Drawing.Size(73, 73);
            this.rb2Phases.TabIndex = 1;
            this.rb2Phases.TabStop = true;
            this.rb2Phases.UseVisualStyleBackColor = true;
            this.rb2Phases.Paint += new System.Windows.Forms.PaintEventHandler(this.rb2Semestres_Paint);
            this.rb2Phases.CheckedChanged += new System.EventHandler(this.rb1Phase_CheckedChanged);
            // 
            // rb1Phase
            // 
            this.rb1Phase.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb1Phase.AutoSize = true;
            this.rb1Phase.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rb1Phase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb1Phase.Image = global::BASEDiagAdulte.Properties.Resources._1Phase;
            this.rb1Phase.Location = new System.Drawing.Point(93, 3);
            this.rb1Phase.Name = "rb1Phase";
            this.rb1Phase.Size = new System.Drawing.Size(73, 73);
            this.rb1Phase.TabIndex = 0;
            this.rb1Phase.TabStop = true;
            this.rb1Phase.UseVisualStyleBackColor = true;
            this.rb1Phase.Paint += new System.Windows.Forms.PaintEventHandler(this.rb2Semestres_Paint);
            this.rb1Phase.CheckedChanged += new System.EventHandler(this.rb1Phase_CheckedChanged);
            // 
            // pnl1Phase
            // 
            this.pnl1Phase.BackColor = System.Drawing.Color.White;
            this.pnl1Phase.Controls.Add(this.label1);
            this.pnl1Phase.Controls.Add(this.rb1Semestre);
            this.pnl1Phase.Controls.Add(this.rb3Semestres);
            this.pnl1Phase.Controls.Add(this.rb2Semestres);
            this.pnl1Phase.Controls.Add(this.rb6Semestres);
            this.pnl1Phase.Controls.Add(this.rb4Semestres);
            this.pnl1Phase.Controls.Add(this.rb5Semestres);
            this.pnl1Phase.Location = new System.Drawing.Point(12, 102);
            this.pnl1Phase.Name = "pnl1Phase";
            this.pnl1Phase.Size = new System.Drawing.Size(436, 205);
            this.pnl1Phase.TabIndex = 1;
            this.pnl1Phase.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "Durée pour la phase Orthodontique";
            // 
            // rb1Semestre
            // 
            this.rb1Semestre.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb1Semestre.AutoSize = true;
            this.rb1Semestre.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rb1Semestre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb1Semestre.Image = global::BASEDiagAdulte.Properties.Resources._1Semestre;
            this.rb1Semestre.Location = new System.Drawing.Point(252, 114);
            this.rb1Semestre.Name = "rb1Semestre";
            this.rb1Semestre.Size = new System.Drawing.Size(73, 73);
            this.rb1Semestre.TabIndex = 5;
            this.rb1Semestre.TabStop = true;
            this.rb1Semestre.Tag = "1";
            this.rb1Semestre.UseVisualStyleBackColor = true;
            this.rb1Semestre.Paint += new System.Windows.Forms.PaintEventHandler(this.rb2Semestres_Paint);
            this.rb1Semestre.CheckedChanged += new System.EventHandler(this.rb5Semestres_CheckedChanged);
            // 
            // rb3Semestres
            // 
            this.rb3Semestres.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb3Semestres.AutoSize = true;
            this.rb3Semestres.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rb3Semestres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb3Semestres.Image = global::BASEDiagAdulte.Properties.Resources._3_Semestres;
            this.rb3Semestres.Location = new System.Drawing.Point(173, 114);
            this.rb3Semestres.Name = "rb3Semestres";
            this.rb3Semestres.Size = new System.Drawing.Size(73, 73);
            this.rb3Semestres.TabIndex = 4;
            this.rb3Semestres.TabStop = true;
            this.rb3Semestres.Tag = "3";
            this.rb3Semestres.UseVisualStyleBackColor = true;
            this.rb3Semestres.Paint += new System.Windows.Forms.PaintEventHandler(this.rb2Semestres_Paint);
            this.rb3Semestres.CheckedChanged += new System.EventHandler(this.rb5Semestres_CheckedChanged);
            // 
            // rb2Semestres
            // 
            this.rb2Semestres.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb2Semestres.AutoSize = true;
            this.rb2Semestres.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rb2Semestres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb2Semestres.Image = global::BASEDiagAdulte.Properties.Resources._2Semestres;
            this.rb2Semestres.Location = new System.Drawing.Point(94, 114);
            this.rb2Semestres.Name = "rb2Semestres";
            this.rb2Semestres.Size = new System.Drawing.Size(73, 73);
            this.rb2Semestres.TabIndex = 3;
            this.rb2Semestres.TabStop = true;
            this.rb2Semestres.Tag = "2";
            this.rb2Semestres.UseVisualStyleBackColor = true;
            this.rb2Semestres.Paint += new System.Windows.Forms.PaintEventHandler(this.rb2Semestres_Paint);
            this.rb2Semestres.CheckedChanged += new System.EventHandler(this.rb5Semestres_CheckedChanged);
            // 
            // rb6Semestres
            // 
            this.rb6Semestres.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb6Semestres.AutoSize = true;
            this.rb6Semestres.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rb6Semestres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb6Semestres.Image = global::BASEDiagAdulte.Properties.Resources._6Semestres;
            this.rb6Semestres.Location = new System.Drawing.Point(252, 35);
            this.rb6Semestres.Name = "rb6Semestres";
            this.rb6Semestres.Size = new System.Drawing.Size(73, 73);
            this.rb6Semestres.TabIndex = 2;
            this.rb6Semestres.TabStop = true;
            this.rb6Semestres.Tag = "6";
            this.rb6Semestres.UseVisualStyleBackColor = true;
            this.rb6Semestres.Paint += new System.Windows.Forms.PaintEventHandler(this.rb2Semestres_Paint);
            this.rb6Semestres.CheckedChanged += new System.EventHandler(this.rb5Semestres_CheckedChanged);
            // 
            // rb4Semestres
            // 
            this.rb4Semestres.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb4Semestres.AutoSize = true;
            this.rb4Semestres.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rb4Semestres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb4Semestres.Image = global::BASEDiagAdulte.Properties.Resources._4Semestres;
            this.rb4Semestres.Location = new System.Drawing.Point(173, 35);
            this.rb4Semestres.Name = "rb4Semestres";
            this.rb4Semestres.Size = new System.Drawing.Size(73, 73);
            this.rb4Semestres.TabIndex = 1;
            this.rb4Semestres.TabStop = true;
            this.rb4Semestres.Tag = "4";
            this.rb4Semestres.UseVisualStyleBackColor = true;
            this.rb4Semestres.Paint += new System.Windows.Forms.PaintEventHandler(this.rb2Semestres_Paint);
            this.rb4Semestres.CheckedChanged += new System.EventHandler(this.rb5Semestres_CheckedChanged);
            // 
            // rb5Semestres
            // 
            this.rb5Semestres.Appearance = System.Windows.Forms.Appearance.Button;
            this.rb5Semestres.AutoSize = true;
            this.rb5Semestres.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rb5Semestres.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rb5Semestres.Image = global::BASEDiagAdulte.Properties.Resources._5_Semestres;
            this.rb5Semestres.Location = new System.Drawing.Point(94, 35);
            this.rb5Semestres.Name = "rb5Semestres";
            this.rb5Semestres.Size = new System.Drawing.Size(73, 73);
            this.rb5Semestres.TabIndex = 0;
            this.rb5Semestres.TabStop = true;
            this.rb5Semestres.Tag = "5";
            this.rb5Semestres.UseVisualStyleBackColor = true;
            this.rb5Semestres.Paint += new System.Windows.Forms.PaintEventHandler(this.rb2Semestres_Paint);
            this.rb5Semestres.CheckedChanged += new System.EventHandler(this.rb5Semestres_CheckedChanged);
            // 
            // pnl2Phase1
            // 
            this.pnl2Phase1.BackColor = System.Drawing.Color.White;
            this.pnl2Phase1.Controls.Add(this.label2);
            this.pnl2Phase1.Controls.Add(this.radioButton1);
            this.pnl2Phase1.Controls.Add(this.radioButton2);
            this.pnl2Phase1.Controls.Add(this.radioButton3);
            this.pnl2Phase1.Location = new System.Drawing.Point(12, 102);
            this.pnl2Phase1.Name = "pnl2Phase1";
            this.pnl2Phase1.Size = new System.Drawing.Size(436, 205);
            this.pnl2Phase1.TabIndex = 2;
            this.pnl2Phase1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(276, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = "Durée pour la 1ere phase (Orthopédique)";
            // 
            // radioButton1
            // 
            this.radioButton1.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton1.AutoSize = true;
            this.radioButton1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.radioButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton1.Image = global::BASEDiagAdulte.Properties.Resources._1Semestre;
            this.radioButton1.Location = new System.Drawing.Point(94, 77);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(73, 73);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.TabStop = true;
            this.radioButton1.Tag = "1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Paint += new System.Windows.Forms.PaintEventHandler(this.rb2Semestres_Paint);
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton2.AutoSize = true;
            this.radioButton2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.radioButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton2.Image = global::BASEDiagAdulte.Properties.Resources._3_Semestres;
            this.radioButton2.Location = new System.Drawing.Point(248, 77);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(73, 73);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Tag = "3";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Paint += new System.Windows.Forms.PaintEventHandler(this.rb2Semestres_Paint);
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton3.AutoSize = true;
            this.radioButton3.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.radioButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton3.Image = global::BASEDiagAdulte.Properties.Resources._2Semestres;
            this.radioButton3.Location = new System.Drawing.Point(171, 77);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(73, 73);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.TabStop = true;
            this.radioButton3.Tag = "2";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Paint += new System.Windows.Forms.PaintEventHandler(this.rb2Semestres_Paint);
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // pnl2Phases2
            // 
            this.pnl2Phases2.BackColor = System.Drawing.Color.White;
            this.pnl2Phases2.Controls.Add(this.label3);
            this.pnl2Phases2.Controls.Add(this.radioButton7);
            this.pnl2Phases2.Controls.Add(this.radioButton8);
            this.pnl2Phases2.Controls.Add(this.radioButton9);
            this.pnl2Phases2.Controls.Add(this.radioButton11);
            this.pnl2Phases2.Controls.Add(this.radioButton12);
            this.pnl2Phases2.Location = new System.Drawing.Point(12, 102);
            this.pnl2Phases2.Name = "pnl2Phases2";
            this.pnl2Phases2.Size = new System.Drawing.Size(436, 205);
            this.pnl2Phases2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(272, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "Durée pour la 2eme phase (Orthodontie)";
            // 
            // radioButton7
            // 
            this.radioButton7.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton7.AutoSize = true;
            this.radioButton7.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.radioButton7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton7.Image = global::BASEDiagAdulte.Properties.Resources._1Semestre;
            this.radioButton7.Location = new System.Drawing.Point(335, 74);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(73, 73);
            this.radioButton7.TabIndex = 5;
            this.radioButton7.TabStop = true;
            this.radioButton7.Tag = "1";
            this.radioButton7.UseVisualStyleBackColor = true;
            this.radioButton7.Paint += new System.Windows.Forms.PaintEventHandler(this.rb2Semestres_Paint);
            this.radioButton7.CheckedChanged += new System.EventHandler(this.radioButton8_CheckedChanged);
            // 
            // radioButton8
            // 
            this.radioButton8.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton8.AutoSize = true;
            this.radioButton8.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.radioButton8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton8.Image = global::BASEDiagAdulte.Properties.Resources._3_Semestres;
            this.radioButton8.Location = new System.Drawing.Point(27, 74);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(73, 73);
            this.radioButton8.TabIndex = 4;
            this.radioButton8.TabStop = true;
            this.radioButton8.Tag = "3";
            this.radioButton8.UseVisualStyleBackColor = true;
            this.radioButton8.Paint += new System.Windows.Forms.PaintEventHandler(this.rb2Semestres_Paint);
            this.radioButton8.CheckedChanged += new System.EventHandler(this.radioButton8_CheckedChanged);
            // 
            // radioButton9
            // 
            this.radioButton9.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton9.AutoSize = true;
            this.radioButton9.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.radioButton9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton9.Image = global::BASEDiagAdulte.Properties.Resources._2Semestres;
            this.radioButton9.Location = new System.Drawing.Point(185, 74);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(73, 73);
            this.radioButton9.TabIndex = 3;
            this.radioButton9.TabStop = true;
            this.radioButton9.Tag = "2";
            this.radioButton9.UseVisualStyleBackColor = true;
            this.radioButton9.Paint += new System.Windows.Forms.PaintEventHandler(this.rb2Semestres_Paint);
            this.radioButton9.CheckedChanged += new System.EventHandler(this.radioButton8_CheckedChanged);
            // 
            // radioButton11
            // 
            this.radioButton11.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton11.AutoSize = true;
            this.radioButton11.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.radioButton11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton11.Image = global::BASEDiagAdulte.Properties.Resources._4Semestres;
            this.radioButton11.Location = new System.Drawing.Point(106, 74);
            this.radioButton11.Name = "radioButton11";
            this.radioButton11.Size = new System.Drawing.Size(73, 73);
            this.radioButton11.TabIndex = 1;
            this.radioButton11.TabStop = true;
            this.radioButton11.Tag = "4";
            this.radioButton11.UseVisualStyleBackColor = true;
            this.radioButton11.Paint += new System.Windows.Forms.PaintEventHandler(this.rb2Semestres_Paint);
            this.radioButton11.CheckedChanged += new System.EventHandler(this.radioButton8_CheckedChanged);
            // 
            // radioButton12
            // 
            this.radioButton12.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton12.AutoSize = true;
            this.radioButton12.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.radioButton12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButton12.Image = global::BASEDiagAdulte.Properties.Resources._5_Semestres;
            this.radioButton12.Location = new System.Drawing.Point(258, 74);
            this.radioButton12.Name = "radioButton12";
            this.radioButton12.Size = new System.Drawing.Size(73, 73);
            this.radioButton12.TabIndex = 0;
            this.radioButton12.TabStop = true;
            this.radioButton12.Tag = "5";
            this.radioButton12.UseVisualStyleBackColor = true;
            this.radioButton12.Paint += new System.Windows.Forms.PaintEventHandler(this.rb2Semestres_Paint);
            this.radioButton12.CheckedChanged += new System.EventHandler(this.radioButton8_CheckedChanged);
            // 
            // FrmDuree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(461, 315);
            this.Controls.Add(this.pnl2Phases2);
            this.Controls.Add(this.pnl2Phase1);
            this.Controls.Add(this.pnl1Phase);
            this.Controls.Add(this.pnlStep1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmDuree";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Durée du traitement : ";
            this.TopMost = true;
            this.pnlStep1.ResumeLayout(false);
            this.pnlStep1.PerformLayout();
            this.pnl1Phase.ResumeLayout(false);
            this.pnl1Phase.PerformLayout();
            this.pnl2Phase1.ResumeLayout(false);
            this.pnl2Phase1.PerformLayout();
            this.pnl2Phases2.ResumeLayout(false);
            this.pnl2Phases2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlStep1;
        private System.Windows.Forms.RadioButton rb1PhasePediatrie;
        private System.Windows.Forms.RadioButton rb2Phases;
        private System.Windows.Forms.RadioButton rb1Phase;
        private System.Windows.Forms.Panel pnl1Phase;
        private System.Windows.Forms.RadioButton rb1Semestre;
        private System.Windows.Forms.RadioButton rb3Semestres;
        private System.Windows.Forms.RadioButton rb2Semestres;
        private System.Windows.Forms.RadioButton rb6Semestres;
        private System.Windows.Forms.RadioButton rb4Semestres;
        private System.Windows.Forms.RadioButton rb5Semestres;
        private System.Windows.Forms.Panel pnl2Phase1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Panel pnl2Phases2;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.RadioButton radioButton11;
        private System.Windows.Forms.RadioButton radioButton12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

    }
}