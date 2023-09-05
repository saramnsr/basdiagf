using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BASEDiagAdulte.Ctrls
{
    public partial class SuggestBox : UserControl
    {
        [Browsable(true)]
        public event EventHandler OnYesClick;

        [Browsable(true)]
        public event EventHandler OnFound;

        private String _FormattedText = "Souhaitiez-vous parler de %s ?";
        [Browsable(true)]
        public String FormattedText
        {
            get
            {
                return _FormattedText;
            }
            set
            {
                _FormattedText = value;
            }
        }

        private float _currentDistance;
        public float currentDistance
        {
            get
            {
                return _currentDistance;
            }
            set
            {
                _currentDistance = value;
            }
        }


        private float _MinDistanceForSuggest = float.MaxValue;
        public float MinDistanceForSuggest
        {
            get
            {
                return _MinDistanceForSuggest;
            }
            set
            {
                _MinDistanceForSuggest = value;
            }
        }

        public String LabelText
        {
            get
            {
                return lblSuggest.Text;
            }
            set
            {
                lblSuggest.Text = value;
            }
        }

        private List<object> _SuggestionList = null;
        [Browsable(false)]
        public List<object> SuggestionList
        {
            get
            {
                return _SuggestionList;
            }
            set
            {
                _SuggestionList = value;
            }
        }

        [Browsable(false)]
        public object value
        {
            get
            {
                return Suggestedobj;
            }
            
        }

        string LastSuggestion = "";

        public SuggestBox()
        {
            InitializeComponent();
        }

        object Suggestedobj = null;


        delegate void SetSuggestionDelegate(object corres);
        void SetFindSuggestion(object corres)
        {
            if (InvokeRequired)
                Invoke(new SetSuggestionDelegate(SetFindSuggestion), corres);
            else
            {
                if (MinDistanceForSuggest > currentDistance)
                {
                    Suggestedobj = corres;
                    lblSuggest.Text = FormattedText.Replace("%s", corres.ToString());
                    this.Visible = true;
                    if (OnFound != null)
                        OnFound(this, new EventArgs());
                }
            }
        }

        void FindSuggestion(object state)
        {
            float min = float.MaxValue;
            object selectedmin = null;

            string nomcorres = ((string)state).ToUpper().Trim();

           
            if (nomcorres.Length < 4) return;

            lock (this)
            {
                foreach (object s in _SuggestionList)
                {
                    System.Reflection.PropertyInfo[] nfos = s.GetType().GetProperties(
                                            System.Reflection.BindingFlags.Instance |
                                            System.Reflection.BindingFlags.NonPublic |
                                            System.Reflection.BindingFlags.Public);

                    foreach (System.Reflection.PropertyInfo nfo in nfos)
                    {
                        if (nfo.PropertyType == typeof(System.String))
                        {
                            string val = (string)nfo.GetValue(s, null);
                            if (val.Length < 3) continue;
                            string name = val.ToUpper().Trim();
                            if (name == nomcorres)
                            {
                                min = 0;
                                selectedmin = s;
                                currentDistance = 0;
                            }
                            else
                                if (name != "")
                                {

                                    float f = SoundsLike.LevenshteinDistance(name, nomcorres);
                                    float sim = SoundsLike.SimilarText(name, nomcorres);
                                    f += (100 - sim) / 100;
                                    //f += SoundsLike.SoundEx(s.Nom, nomcorres);

                                    if (min > f)
                                    {
                                        min = f;
                                        selectedmin = s;
                                        currentDistance = f;
                                    }

                                }
                        }
                    }
                }
            }
            if (selectedmin != null) SetFindSuggestion(selectedmin);
        }

        public void Suggest(string Text)
        {
            LastSuggestion = Text;
            if (_SuggestionList == null) return;
            ThreadPool.QueueUserWorkItem(new WaitCallback(FindSuggestion), Text);
        }

        private void BtnChooseSuggestedCorres_Click(object sender, EventArgs e)
        {
            if (OnYesClick!=null)
                OnYesClick(this, new EventArgs());
            Visible = false;
        }

        private void BtnNextSuggestedCorres_Click(object sender, EventArgs e)
        {
            _SuggestionList.Remove(Suggestedobj);
            Suggest(LastSuggestion);
        }
    
    }
}
