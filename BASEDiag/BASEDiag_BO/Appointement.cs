using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace BASEDiag_BO
{
    [Serializable]
    public class Appointment
    {
        public Appointment()
        {
            color = Color.White;
            m_BorderColor = Color.Blue;
            m_Title = "New Appointment";
        }


        public double Duree
        {
            get
            {
                return EndDate.Subtract(StartDate).TotalMinutes;
            }
        }

        private DateTime m_StartDate;

        public DateTime StartDate
        {
            get
            {
                return m_StartDate;
            }
            set
            {
                m_StartDate = value;
                OnStartDateChanged();

            }
        }
        protected virtual void OnStartDateChanged()
        {
        }

        private Resource m_Resource;
        public Resource Resource
        {
            get
            {
                return m_Resource;
            }
            set
            {
                m_Resource = value;
                OnResourceChanged();
            }
        }

        private DateTime m_EndDate;

        public DateTime EndDate
        {
            get
            {
                return m_EndDate;
            }
            set
            {
                m_EndDate = value;
                OnEndDateChanged();
            }
        }

        protected virtual void OnEndDateChanged()
        {
        }

        protected virtual void OnResourceChanged()
        {
        }


        private bool m_Locked = false;

        [System.ComponentModel.DefaultValue(false)]
        public bool Locked
        {
            get { return m_Locked; }
            set
            {
                m_Locked = value;
                OnLockedChanged();
            }
        }

        protected virtual void OnLockedChanged()
        {
        }

        private Color color = Color.White;

        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        private Color textColor = Color.Black;

        public Color TextColor
        {
            get { return textColor; }
            set { textColor = value; }
        }

        private Color m_BorderColor = Color.Blue;

        public Color BorderColor
        {
            get
            {
                return m_BorderColor;
            }
            set
            {
                m_BorderColor = value;
            }
        }

        private string m_Title = "";

        [System.ComponentModel.DefaultValue("")]
        public string Title
        {
            get
            {
                return m_Title;
            }
            set
            {
                m_Title = value;
                OnTitleChanged();
            }
        }
        protected virtual void OnTitleChanged()
        {
        }

    }

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

            }
        }
        
    }

}
