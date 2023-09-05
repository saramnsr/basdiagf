using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace CS1504.BO
{
    public class BarCode
    {
        public override string ToString()
        {
            return Date.ToString("dd MMMM yyyy à HH:mm") + " : " + CodeBarValue.ToString();
        }

        public StringBuilder CodeBarValue = new StringBuilder(63);
        public StringBuilder CodeType = new StringBuilder(63);
        public StringBuilder TimeStamp = new StringBuilder(32);

        public DateTime Date
        {
            get
            {
                try
                {
                    CultureInfo nfo = new CultureInfo("en-US");
                    return DateTime.Parse(TimeStamp.ToString(), nfo);
                }
                catch (System.Exception) { return DateTime.MinValue; }
            }

        }


    }
}
