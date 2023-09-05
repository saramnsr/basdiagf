using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseCommonControls
{
    public class NoScrollBarListBox : ListBox
    {

        private const int WS_VSCROLL = 0x00200000;
        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                System.Windows.Forms.CreateParams parms = base.CreateParams;
                if (!this.verticalScrollbar)
                    parms.Style &= ~WS_VSCROLL;
                return parms;
            }
        }
        
        private bool verticalScrollbar;
        public virtual bool VerticalScrollbar
        {
            get { return this.verticalScrollbar; }
            set
            {
                if (this.verticalScrollbar != value)
                {
                    this.verticalScrollbar = value;
                    this.RecreateHandle();
                }
            }
        }
    }
}
