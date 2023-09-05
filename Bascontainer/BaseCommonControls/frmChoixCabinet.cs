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

namespace BaseCommonControls
{
    public partial class frmChoixCabinet : Form
    {

        private int _cabinet;
        public int cabinet
        {
            get
            {
                return _cabinet;
            }
            set
            {
                _cabinet = value;
            }
        }
        public frmChoixCabinet()
        {
            InitializeComponent();
            int pointX = 108;
            int pointY = 54;
            foreach (Cabinet cab in CabinetMgmt.Cabinet)
            {
                
                Button button = new Button();
                button.Location = new System.Drawing.Point(pointX, pointY);
                button.Margin = new System.Windows.Forms.Padding(6);
                button.Name = "button" + cab.Id;
                button.Size = new System.Drawing.Size(145, 108);
                button.TabIndex = 0;
                button.Text = cab.nomCabinet;
                button.UseVisualStyleBackColor = true;
                button.Click += delegate{
                    cabinet = cab.Id;
                    UtilisateursMgt.SetCurrentUCabOnRegistry(cab.Id.ToString());
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                                };
                pointX = pointX + 157;
                this.Controls.Add(button);
            }

        }

    
    }
}
