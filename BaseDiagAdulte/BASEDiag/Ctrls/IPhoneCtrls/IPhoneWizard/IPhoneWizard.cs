using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;

namespace BASEPractice.Ctrls.IPhoneCtrls.IPhoneWizard
{
    public partial class IPhoneWizard : UserControl
    {

        Rectangle questionBounds;

        List<IPhoneWizardQuestions> AskedQuestions = new List<IPhoneWizardQuestions>();
        

        private int _FooterHeight = 35;
        public int FooterHeight
        {
            get
            {
                return _FooterHeight;
            }
            set
            {
                _FooterHeight = value;
            }
        }

        public event EventHandler OnFinished;
        


        

        

        private Point MouseIsDownAt;
        private bool MouseIsDown = false;
        private bool IsSlideOn = false;

        private Point _SlideOffSet = new Point(0, 0);
        private Point SlideOffSetWhenMouseIsDown = new Point(0, 0);
        public Point SlideOffSet
        {
            get
            {
                return _SlideOffSet;
            }
            set
            {
                _SlideOffSet = value;
            }
        }


        private IphoneWizardQuestionBtn _NextButton = new IphoneWizardQuestionBtn();
        public IphoneWizardQuestionBtn NextButton
        {
            get
            {
                return _NextButton;
            }
            set
            {
                _NextButton = value;
            }
        }

        private IphoneWizardQuestionBtn _PreviousButton = new IphoneWizardQuestionBtn();
        public IphoneWizardQuestionBtn PreviousButton
        {
            get
            {
                return _PreviousButton;
            }
            set
            {
                _PreviousButton = value;
            }
        }


       

        

        private int _ButtonsHeight = 60;
        public int ButtonsHeight
        {
            get
            {
                return _ButtonsHeight;
            }
            set
            {
                _ButtonsHeight = value;
            }
        }

        private int _ButtonsLocation = 65;
        public int ButtonsLocation
        {
            get
            {
                return _ButtonsLocation;
            }
            set
            {
                _ButtonsLocation = value;
            }
        }

        private bool needToRecalculate = true;

        private Font _BtnFont = new Font("Segoe UI", 12, FontStyle.Regular);
        public Font BtnFont
        {
            get
            {
                return _BtnFont;
            }
            set
            {
                _BtnFont = value;
            }
        }

        private Point _QuestionLocation = new Point(0,0);
        public Point QuestionLocation
        {
            get
            {
                return _QuestionLocation;
            }
            set
            {
                _QuestionLocation = value;
            }
        }


        private List<IphoneWizardQuestionBtn> _Btns = new List<IphoneWizardQuestionBtn>();
        private List<IphoneWizardQuestionBtn> Btns
        {
            get
            {
                return _Btns;
            }
            set
            {
                _Btns = value;
            }
        }

       
        private IQuestionnaireFactory _Questionnaire;
        public IQuestionnaireFactory Questionnaire
        {
            get
            {
                return _Questionnaire;
            }
            set
            {
                _Questionnaire = value;
            }
        }


        public object Value
        {
            get
            {
                return Questionnaire.Resultat;
            }
            
        }
        

        public IPhoneWizard()
        {
            InitializeComponent();

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            NextButton.text = "Suivant";
            NextButton.font = BtnFont;

            PreviousButton.text = "Précedent";
            PreviousButton.font = BtnFont;

        }

        



        private void RecalculateBtnPropositions(IPhoneWizardQuestions question, Graphics g)
        {
            if (question.PropositionsList.Count == 0) return;


            int idx = 0;
            Btns.Clear();
            foreach (object p in question.PropositionsList)
            {
                IphoneWizardQuestionBtn btn = new IphoneWizardQuestionBtn();
                btn.font = BtnFont;
                btn.text = p.ToString();
                btn.Value = p;
                btn.Index = idx;
                btn.Selected = question.Answers.Contains(p);
                Btns.Add(btn);

                idx++;
            }


            if (question.OnNeedToCalculateAnswersPosition(Btns, new Rectangle(0, ButtonsLocation, Bounds.Width, Bounds.Height - (ButtonsHeight + 10 + 50))))
            {




                int MaxWidth = ButtonsHeight;
                int IntervallesY = 10;

                /*
                foreach (object p in question.PropositionsList)
                {
                    string s = p.ToString();
                    SizeF sz = g.MeasureString(p.ToString(), BtnFont);
                    if (sz.Width > MaxWidth) MaxWidth = (int)Math.Ceiling(sz.Width);
                }
                */
                int Nblines = 1;
                int BtnPerLine = 1;
                int IntervallesX = 5;
                float currentX;
                float currentY;

                if (question.slidemode == IPhoneWizardQuestions.SlideMode.None)
                {
                    BtnPerLine = question.PropositionsList.Count;
                    if (BtnPerLine <= 0) BtnPerLine = 1;
                    IntervallesX = (int)((Width - (BtnPerLine * MaxWidth)) / BtnPerLine);

                    while ((IntervallesX < 0) && (BtnPerLine > 1))
                    {
                        BtnPerLine--;
                        IntervallesX = (int)((Width - (BtnPerLine * MaxWidth)) / BtnPerLine);
                    }
                    Nblines = (int)Math.Ceiling(question.PropositionsList.Count / (float)BtnPerLine);




                    currentX = (IntervallesX / 2);
                    currentY = ButtonsLocation;
                }
                else
                {
                    if (question.slidemode == IPhoneWizardQuestions.SlideMode.Vertical)
                        currentX = (Width - MaxWidth) / 2;
                    else
                        currentX = SlideOffSet.X;
                    currentY = ButtonsLocation + SlideOffSet.Y;
                }

                int xcounter = 0;
                foreach (IphoneWizardQuestionBtn btn in Btns)
                {
                    RectangleF r = new RectangleF(currentX, currentY, MaxWidth, ButtonsHeight);
                    btn.Bounds = r;
                    currentX += btn.Bounds.Width + IntervallesX;
                    xcounter++;
                    idx++;
                    if (((xcounter >= BtnPerLine) && (question.slidemode == IPhoneWizardQuestions.SlideMode.None)) || (question.slidemode == IPhoneWizardQuestions.SlideMode.Vertical))
                    {
                        xcounter = 0;


                        switch (question.slidemode)
                        {
                            case IPhoneWizardQuestions.SlideMode.None: currentX = (IntervallesX / 2); break;
                            case IPhoneWizardQuestions.SlideMode.Vertical: currentX = (Width - MaxWidth) / 2; break;
                            case IPhoneWizardQuestions.SlideMode.Horizontal: currentX = SlideOffSet.X; break;
                        }


                        currentY += ButtonsHeight + IntervallesY;
                    }
                }

            }
           
            SizeF nxtsz = g.MeasureString(NextButton.text, NextButton.font);
            SizeF prvsz = g.MeasureString(PreviousButton.text, PreviousButton.font);

            int mxw = (int)Math.Max(nxtsz.Width, prvsz.Width);


            Rectangle NextBtnR = new Rectangle(Width - mxw - 5, Height - FooterHeight + 2, mxw + 2, FooterHeight - 4);
            NextButton.Bounds = NextBtnR;
            nxtsz = g.MeasureString(PreviousButton.text, PreviousButton.font);


            NextBtnR = new Rectangle(NextBtnR.Left - mxw - 5, Height - FooterHeight - 2, mxw + 2, FooterHeight-4);
            PreviousButton.Bounds = NextBtnR;


        }

        protected override void OnResize(EventArgs e)
        {
            
            needToRecalculate = true;
            Invalidate();
            base.OnResize(e);
        }


        private void DrawFooter(Graphics g, IPhoneWizardQuestions question)
        {

            if (question.selectmode == IPhoneWizardQuestions.MultiselectMode.Multi)
            {
                Font ft = new Font("Segoe UI", 12, FontStyle.Regular);

                Rectangle p_rectangle = new Rectangle(0, Height - FooterHeight, Width, FooterHeight);

                /*
                RectangleF topPart = new RectangleF(p_rectangle.Left, p_rectangle.Top, p_rectangle.Width, (int)(p_rectangle.Height / 2));
                RectangleF lowPart = new RectangleF(p_rectangle.Left, p_rectangle.Top + (int)(p_rectangle.Height / 2), p_rectangle.Width, (int)(p_rectangle.Height / 2));

                using (LinearGradientBrush aGB = new LinearGradientBrush(topPart, Color.FromArgb(200, 200, 200), Color.FromArgb(190, 190, 190), LinearGradientMode.Vertical))
                    g.FillRectangle(aGB, topPart);

                using (LinearGradientBrush aGB = new LinearGradientBrush(lowPart, Color.FromArgb(180, 180, 180), Color.FromArgb(170, 170, 170), LinearGradientMode.Vertical))
                    g.FillRectangle(aGB, lowPart);
                */
                using (LinearGradientBrush aGB = new LinearGradientBrush(p_rectangle, Color.FromArgb(240, 240, 240), Color.FromArgb(190, 190, 190), LinearGradientMode.Vertical))
                    g.FillRectangle(aGB, p_rectangle);

                NextButton.Draw(g);
                //PreviousButton.Draw(g);
            }
        }


        private void DrawQuestion(Graphics g, IPhoneWizardQuestions question)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            Font ft = new Font("Segoe UI",12,FontStyle.Regular);

            questionBounds = new Rectangle(0, 0, Width, 50);


            RectangleF topPart = new RectangleF(questionBounds.Left, questionBounds.Top, questionBounds.Width, (int)(questionBounds.Height / 2));
            RectangleF lowPart = new RectangleF(questionBounds.Left, questionBounds.Top + (int)(questionBounds.Height / 2), questionBounds.Width, (int)(questionBounds.Height / 2));

            using (LinearGradientBrush aGB = new LinearGradientBrush(topPart, Color.FromArgb(200, 200, 200), Color.FromArgb(190, 190, 190), LinearGradientMode.Vertical))
                g.FillRectangle(aGB, topPart);

            using (LinearGradientBrush aGB = new LinearGradientBrush(lowPart, Color.FromArgb(180, 180, 180), Color.FromArgb(170, 170, 170), LinearGradientMode.Vertical))
                g.FillRectangle(aGB, lowPart);


            g.DrawString(question.Question, ft, Brushes.Black, questionBounds, sf);

        }

        private void DrawPropositions(Graphics g, IPhoneWizardQuestions question)
        {

            if (needToRecalculate)
            {
                RecalculateBtnPropositions(question, g);
                needToRecalculate = false;
            }

            foreach (IphoneWizardQuestionBtn btn in Btns)
                btn.Draw(g);
            
                
        }

        public void Reset()
        {

            foreach (IPhoneWizardQuestions question in AskedQuestions)
            {
                if (question!=null)
                    question.Clear();
            }

            AskedQuestions.Clear();

        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            MouseIsDown = false;

            if (Questionnaire == null) return;

            object hittedOn = Hittest(new Point(e.X,e.Y));

            if (hittedOn == NextButton)
            {
                if ((Questionnaire.Currentquestion.Answers.Count > 0))
                {
                    GoToNextQuestion();

                    SlideOffSet = new Point(0, 0);
                    needToRecalculate = true;
                    Invalidate();
                }
            }
            else
            {

                if ((hittedOn is IphoneWizardQuestionBtn) && (!IsSlideOn))
                {
                    if (Questionnaire.Currentquestion.selectmode == IPhoneWizardQuestions.MultiselectMode.Multi)
                    {
                        ((IphoneWizardQuestionBtn)hittedOn).Selected = !((IphoneWizardQuestionBtn)hittedOn).Selected;
                        if (((IphoneWizardQuestionBtn)hittedOn).Selected)
                        {
                            Questionnaire.Currentquestion.Answers.Add(((IphoneWizardQuestionBtn)hittedOn).Value);
                            Questionnaire.Currentquestion.AnswersIdx.Add(((IphoneWizardQuestionBtn)hittedOn).Index);
                        }
                        else
                        {
                            Questionnaire.Currentquestion.Answers.Remove(((IphoneWizardQuestionBtn)hittedOn).Value);
                            Questionnaire.Currentquestion.AnswersIdx.Remove(((IphoneWizardQuestionBtn)hittedOn).Index);

                        }
                        
                    }
                    else
                    {
                        Questionnaire.Currentquestion.Answers.Add(((IphoneWizardQuestionBtn)hittedOn).Value);
                        Questionnaire.Currentquestion.AnswersIdx.Add(((IphoneWizardQuestionBtn)hittedOn).Index);

                        GoToNextQuestion();

                        SlideOffSet = new Point(0, 0);
                        needToRecalculate = true;
                    }
                   

                    Invalidate();
                }
            }

            if (Questionnaire.Currentquestion == null)
            {
                Questionnaire.ProcessAllQuestionaire();
                if (OnFinished != null) OnFinished(this, new EventArgs());
            }
            else
            {
                Reset();
            }

            base.OnMouseUp(e);

        }

        private void GoToNextQuestion()
        {
            Questionnaire.Currentquestion.QuestionAnswersProcessing();
            Questionnaire.Currentquestion = Questionnaire.Currentquestion.NextQuestion();
            AskedQuestions.Add(Questionnaire.Currentquestion);
            while ((Questionnaire.Currentquestion!=null) && (Questionnaire.Currentquestion.PropositionsList.Count == 0))
            {
                Questionnaire.Currentquestion = Questionnaire.Currentquestion.NextQuestion();
                AskedQuestions.Add(Questionnaire.Currentquestion);

            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if ((MouseIsDown) && (Questionnaire.Currentquestion.slidemode != IPhoneWizardQuestions.SlideMode.None))
            {
                IsSlideOn = ((Math.Abs(MouseIsDownAt.X - e.X) > 5) || (Math.Abs(MouseIsDownAt.Y - e.Y) > 5));
                if (Questionnaire.Currentquestion.slidemode == IPhoneWizardQuestions.SlideMode.Horizontal) SlideOffSet = new Point(SlideOffSetWhenMouseIsDown.X - (MouseIsDownAt.X - e.X), SlideOffSetWhenMouseIsDown.Y);
                if (Questionnaire.Currentquestion.slidemode == IPhoneWizardQuestions.SlideMode.Vertical) SlideOffSet = new Point(SlideOffSetWhenMouseIsDown.X, SlideOffSetWhenMouseIsDown.Y - (MouseIsDownAt.Y - e.Y));
                needToRecalculate = true;
                Invalidate();
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {

            if (HittedOnQuestionBounds(new Point(e.X, e.Y)))
            {
                Questionnaire.Currentquestion = Questionnaire.GetFirstQuestion();
            }
            
            if (Questionnaire.Currentquestion.slidemode != IPhoneWizardQuestions.SlideMode.None)
            {
                MouseIsDown = true;
                IsSlideOn = false;
                MouseIsDownAt = new Point(e.X, e.Y);
                SlideOffSetWhenMouseIsDown = new Point(SlideOffSet.X, SlideOffSet.Y);
            }
            

            base.OnMouseDown(e);
        }

        private bool HittedOnQuestionBounds(Point pt)
        {
            return questionBounds.Contains(pt);
        }

        private object Hittest(Point pt)
        {
            foreach (IphoneWizardQuestionBtn btn in Btns)
            {
                if (btn.Bounds.Contains(pt)) return btn;
            }
            if (NextButton.Bounds.Contains(pt)) return NextButton;
            if (PreviousButton.Bounds.Contains(pt)) return PreviousButton;

            return null;
        }
        
        public void Start()
        {
            Questionnaire.Currentquestion = Questionnaire.GetFirstQuestion();
            AskedQuestions.Add(Questionnaire.Currentquestion);
            needToRecalculate = true;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            e.Graphics.FillRectangle(new SolidBrush(BackColor), new Rectangle(0, 0, Width, Height));

            if ((Questionnaire != null) && (Questionnaire.Currentquestion != null))
            {

                DrawPropositions(e.Graphics, Questionnaire.Currentquestion);
                DrawQuestion(e.Graphics, Questionnaire.Currentquestion);
                DrawFooter(e.Graphics, Questionnaire.Currentquestion);
            }


            base.OnPaint(e);
        }

        private void IPhoneWizard_Load(object sender, EventArgs e)
        {

        }
    }
}
