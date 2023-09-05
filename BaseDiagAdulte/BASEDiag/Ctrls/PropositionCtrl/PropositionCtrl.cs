using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BASEDiag_BL;
using BASEDiag_BO;

namespace BASEDiag.Ctrls
{
    public partial class PropositionCtrl : UserControl
    {
        public AbstractPropositoonRenderer Renderer;

        private Patient _CurrentPatient;
        public Patient CurrentPatient
        {
            get
            {
                return _CurrentPatient;
            }
            set
            {
                _CurrentPatient = value;
                Invalidate();
            }
        }

        private bool needToBeRebuilt = true;

        public event EventHandler OnSelectionChange;

        private object _SelectedObject = null;
        public object SelectedObject
        {
            get
            {
                return _SelectedObject;
            }
            set
            {
                _SelectedObject = value;
            }
        }


        public Proposition SelectedProposition
        {
            get
            {
                if (_Propositioncells.Count == 0) return null;

                foreach (BASEDiag.Ctrls.PropositionCell pc in _Propositioncells)
                {
                    if (pc.State == BASEDiag.Ctrls.PropositionCell.Status.Selected)
                        return pc.proposition;
                }
                return _Propositioncells[0].proposition;
            }

        }




        public void DecalerSemestres(int semestres)
        {
            foreach (Proposition p in CurrentPatient.propositions)
                foreach (Traitement t in p.traitements)
                {
                   
                    for (int i = 0; i < t.NumSemestres.Count; i++)
                        t.NumSemestres[i] += semestres;

                    t.PartSecu = TraitementMgmt.checkPartSecu(t, CurrentPatient);

                }
        }

        public void RemoveSelection()
        {
            if (SelectedObject is PropositionCell)
            {
                CurrentPatient.propositions.Remove(((PropositionCell)SelectedObject).proposition);
                _Propositioncells.Remove((PropositionCell)SelectedObject);

            }

            if (SelectedObject is TraitementCell)
            {
                foreach (BASEDiag.Ctrls.PropositionCell pc in _Propositioncells)
                {
                    if (pc.State == BASEDiag.Ctrls.PropositionCell.Status.Selected)
                    {
                        pc.proposition.traitements.Remove(((TraitementCell)SelectedObject).traitement);
                        pc.traitementscell.Remove((TraitementCell)SelectedObject);
                        if (pc.proposition.traitements.Count == 0)
                        {
                            CurrentPatient.propositions.Remove(pc.proposition);
                            _Propositioncells.Remove(pc);
                            break;
                        }
                    }
                }


            }

            needToBeRebuilt = true;
            Invalidate();
        }

        public Traitement SelectedTraitement
        {
            get
            {
                foreach (BASEDiag.Ctrls.PropositionCell pc in _Propositioncells)
                {
                    foreach (TraitementCell tc in pc.traitementscell)
                        if (tc.State == TraitementCell.Status.Selected) return tc.traitement;
                }
                return null;
            }

        }

        private List<PropositionCell> _Propositioncells = new List<PropositionCell>();






        public PropositionCtrl()
        {
            InitializeComponent();

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            Renderer = new PropositoonRenderer();

        }



        public void RemoveProposition(Proposition proposition)
        {
            foreach (Traitement t in proposition.traitements)
                proposition.traitements.Remove(t);

            CurrentPatient.propositions.Remove(proposition);
            needToBeRebuilt = true;
            Invalidate();
        }


        public void BuildNInvalidate()
        {
            needToBeRebuilt = true;
            Invalidate();
        }

        public void AddProposition(Proposition proposition)
        {
            CurrentPatient.propositions.Add(proposition);
            needToBeRebuilt = true;
            Invalidate();
        }

        public object Hittest(Point pt)
        {
            foreach (PropositionCell pc in _Propositioncells)
            {
                if (pc.Bound.Contains(pt))
                    return pc;

                foreach (TraitementCell tc in pc.traitementscell)
                    if (tc.Bound.Contains(pt))
                        return tc;
            }
            return null;
        }


        public void Rebuild(Graphics g)
        {

            Size sz = Renderer.MeasureProposition(g);
            int PropositionWidth = sz.Width;
            int PropositionHeight = sz.Height;

            sz = Renderer.MeasureTraitement(g);
            int TraitementWidth = sz.Width;
            int TraitementHeight = sz.Height;

            int currentX = 0;
            int currentY = 0;
            _Propositioncells.Clear();
            if (CurrentPatient == null) return;
            foreach (Proposition p in CurrentPatient.propositions)
            {
                currentX = 0;
                Rectangle rect = new Rectangle(currentX, currentY, PropositionWidth, PropositionHeight);

                PropositionCell pc = new PropositionCell();
                pc.proposition = p;
                pc.Bound = rect;
                currentX += PropositionWidth;
                foreach (Traitement t in p.traitements)
                {
                    TraitementCell tc = new TraitementCell();
                    tc.traitement = t;
                    rect = new Rectangle(currentX, currentY, TraitementWidth, TraitementHeight);
                    tc.Bound = rect;
                    currentY += TraitementHeight;
                    pc.traitementscell.Add(tc);
                }
                if (p.traitements.Count == 0)
                    currentY += TraitementHeight;

                _Propositioncells.Add(pc);
            }
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            object obj = Hittest(new Point(e.X, e.Y));

            if ((obj != null) && (SelectedObject != obj))
            {
                foreach (PropositionCell pc in _Propositioncells)
                {
                    pc.State = PropositionCell.Status.None;
                    foreach (TraitementCell tc in pc.traitementscell)
                        tc.State = TraitementCell.Status.None;
                }
            }

            if ((obj is PropositionCell) && (SelectedObject != obj))
            {


                ((PropositionCell)obj).State = PropositionCell.Status.Selected;
                if (OnSelectionChange != null) OnSelectionChange(this, new EventArgs());
                SelectedObject = ((PropositionCell)obj);
                Invalidate();
            }

            if ((obj is TraitementCell) && (SelectedObject != obj))
            {
                foreach (PropositionCell pc in _Propositioncells)
                {
                    foreach (TraitementCell tc in pc.traitementscell)
                    {
                        if (obj == tc)
                        {
                            pc.State = PropositionCell.Status.Selected;
                            tc.State = TraitementCell.Status.Selected;
                            if (OnSelectionChange != null) OnSelectionChange(this, new EventArgs());
                            SelectedObject = tc;
                        }
                    }
                }

                Invalidate();
            }

            base.OnMouseDown(e);
        }


        protected override void OnPaint(PaintEventArgs e)
        {

            if (needToBeRebuilt)
            {
                Rebuild(e.Graphics);
                needToBeRebuilt = false;
            }

            foreach (PropositionCell pc in _Propositioncells)
            {
                Renderer.DrawProposition(e.Graphics, pc);
                foreach (TraitementCell tc in pc.traitementscell)
                {
                    Renderer.DrawTraitement(e.Graphics, tc);
                }
            }

        }
    }

    public class TraitementCell
    {

        public enum Status
        {
            Selected,
            None
        }


        private Status _State = Status.None;
        public Status State
        {
            get
            {
                return _State;
            }
            set
            {
                _State = value;
            }
        }

        private Traitement _traitement;
        public Traitement traitement
        {
            get
            {
                return _traitement;
            }
            set
            {
                _traitement = value;
            }
        }

        private Rectangle _Bound;
        public Rectangle Bound
        {
            get
            {
                return _Bound;
            }
            set
            {
                _Bound = value;
            }
        }
    }

    public class PropositionCell
    {

        public enum Status
        {
            Selected,
            None
        }


        private Status _State = Status.None;
        public Status State
        {
            get
            {
                return _State;
            }
            set
            {
                _State = value;
            }
        }

        private List<TraitementCell> _traitements = new List<TraitementCell>();
        public List<TraitementCell> traitementscell
        {
            get
            {
                return _traitements;
            }
            set
            {
                _traitements = value;
            }
        }

        private Proposition _proposition;
        public Proposition proposition
        {
            get
            {
                return _proposition;
            }
            set
            {
                _proposition = value;
            }
        }

        private Rectangle _Bound;
        public Rectangle Bound
        {
            get
            {
                return _Bound;
            }
            set
            {
                _Bound = value;
            }
        }
    }
}
