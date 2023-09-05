using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEPractice.Ctrls.IPhoneCtrls.IPhoneWizard
{
    
    public delegate void NextQuestionEventHandler(object sender, NextQuestionEventArgs e);
    public delegate void ProcessQuestionAnswerEventHandler(object sender, ProcessQuestionAnswerEventArgs e);
    public delegate void NeedToCalculateAnswersPositionEventHandler(object sender, NeedToCalculateAnswersPositionEventArgs e);
    

    


    public class NextQuestionEventArgs : EventArgs
    {
        private IPhoneWizardQuestions _NextQuestion;
        public IPhoneWizardQuestions NextQuestion
        {
            get
            {
                return _NextQuestion;
            }
            set
            {
                _NextQuestion = value;
            }
        }


    }

    public class ProcessQuestionAnswerEventArgs : EventArgs
    {
        
    }

    public class NeedToCalculateAnswersPositionEventArgs : EventArgs
    {
        private List<IphoneWizardQuestionBtn> _buttons;
        public List<IphoneWizardQuestionBtn> buttons
        {
            get
            {
                return _buttons;
            }
            set
            {
                _buttons = value;
            }
        }

        private System.Drawing.Rectangle _DrawingArea;
        public System.Drawing.Rectangle DrawingArea
        {
            get
            {
                return _DrawingArea;
            }
            set
            {
                _DrawingArea = value;
            }
        }

        public NeedToCalculateAnswersPositionEventArgs(List<IphoneWizardQuestionBtn> lstBtn, System.Drawing.Rectangle DrawingRectangle)
        {
            _buttons = lstBtn;
            DrawingArea = DrawingRectangle;
        }
    }
    

       [Serializable]
    public class IPhoneWizardQuestions
    {

           public event NeedToCalculateAnswersPositionEventHandler NeedToCalculateAnswersPosition;
           public event ProcessQuestionAnswerEventHandler ProcessQuestionAnswer;
           public event NextQuestionEventHandler RequestNextQuestion;


           public enum SlideMode
           {
               None,
               Horizontal,
               Vertical
           }


           public enum MultiselectMode
           {
               Single,
               Multi
           }

           private SlideMode _slidemode = SlideMode.None;
           public SlideMode slidemode
           {
               get
               {
                   return _slidemode;
               }
               set
               {
                   _slidemode = value;
               }
           }

           private MultiselectMode _selectmode = MultiselectMode.Single;
           public MultiselectMode selectmode
           {
               get
               {
                   return _selectmode;
               }
               set
               {
                   _selectmode = value;
               }
           }

           private List<object> _PropositionsList = new List<object>();
        public List<object> PropositionsList
        {
            get
            {
                return _PropositionsList;
            }
            set
            {
                _PropositionsList = value;
            }
        }


        public void Clear()
        {
            Answers.Clear();
            AnswersIdx.Clear();
        }

        private List<object> _Answers = new List<object>();
        public List<object> Answers
        {
            get
            {
                return _Answers;
            }
            set
            {
                _Answers = value;
            }
        }

        private List<int> _AnswersIdx = new List<int>();
        public List<int> AnswersIdx
        {
            get
            {
                return _AnswersIdx;
            }
            set
            {
                _AnswersIdx = value;
            }
        }


        private String _Question;
        public String Question
        {
            get
            {
                return _Question;
            }
            set
            {
                _Question = value;
            }
        }

        internal void QuestionAnswersProcessing()
        {            
            if (ProcessQuestionAnswer!=null) ProcessQuestionAnswer(this,new ProcessQuestionAnswerEventArgs());
        }

        internal bool OnNeedToCalculateAnswersPosition(List<IphoneWizardQuestionBtn> lstbtns, System.Drawing.Rectangle DrawingArea)
        {
            if (NeedToCalculateAnswersPosition != null)
            {
                NeedToCalculateAnswersPosition(this, new NeedToCalculateAnswersPositionEventArgs(lstbtns,DrawingArea));
                return false;
            }
            else
                return true;
        }

        internal IPhoneWizardQuestions NextQuestion()
        {
            NextQuestionEventArgs args = new NextQuestionEventArgs();
            if (RequestNextQuestion != null) RequestNextQuestion(this, args);
            return args.NextQuestion;
        }

        public void BuildPropositionsFromEnum(Type value)
        {
            PropositionsList.Clear();
            if (!value.IsEnum) return;
            string[] vals = Enum.GetNames(value);
            foreach (string s in vals)
                PropositionsList.Add(Enum.Parse(value,s));
        }

        public void BuildPropositionsFromList(IList lst )
        {
            PropositionsList.Clear();

            for (int i=0;i<lst.Count;i++)
                PropositionsList.Add(lst[i]);
        }

        public void BuildPropositionsFromString(string values)
        {
            PropositionsList.Clear();
            string[] vals = values.Split(new char[]{';','\n'});
            foreach (string s in vals)
                PropositionsList.Add(s.Trim());
        }

    }
}
