using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BASEPractice.Ctrls.IPhoneCtrls.IPhoneWizard
{

    public abstract class QuestionnaireFactory : IQuestionnaireFactory
    {
        #region IQuestionnaireFactory Members

        IPhoneWizardQuestions _Currentquestion;

        public IPhoneWizardQuestions Currentquestion
        {
            get
            {
                return _Currentquestion;
            }
            set
            {
                _Currentquestion = value;
            }
        }

        public virtual void ProcessAllQuestionaire()
        {
            
        }

        public virtual IPhoneWizardQuestions GetPreviousQuestion() { return null; }
        public virtual IPhoneWizardQuestions GetFirstQuestion() { return null; }

        

        private object _Resultat;
        public object Resultat
        {
            get
            {
                return _Resultat;
            }
            set
            {
                _Resultat = value;
            }
        }

        #endregion
    }


    public interface IQuestionnaireFactory
    {


        IPhoneWizardQuestions Currentquestion { get; set; }
        void ProcessAllQuestionaire();
        IPhoneWizardQuestions GetPreviousQuestion();
        IPhoneWizardQuestions GetFirstQuestion();
        
        object Resultat
        {
            get;
            set;
        }
    }
}
