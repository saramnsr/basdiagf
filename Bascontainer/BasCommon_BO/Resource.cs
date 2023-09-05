using System;
using System.Collections.Generic;
using System.Text;

namespace BasCommon_BO
{
    public class Resource
    {
        private string m_Name;
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            { 
                m_Name = value;
                OnNameChanged();

            }
        }
        protected virtual void OnNameChanged()
        {
        }

        private bool m_Visible = true;

        [System.ComponentModel.DefaultValue(true)]
        public bool Visible
        {
            get
            {
                return m_Visible;
            }
            set
            {
                m_Visible = value;
                OnVisibleChanged();

            }
        }
        protected virtual void OnVisibleChanged()
        {
        }

    }
}
