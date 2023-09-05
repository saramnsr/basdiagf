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

namespace BASEDiag.Ctrls.PropositionCtrlV2
{
    public partial class PropositionCtrlV2 : UserControl
    {

        int SEMESTRECELLHEIGHT = 100;
        int SEMESTRECELLWIDTH = 100;
        int PROPOSITIONHEIGHT = 130;
        int TRAITEMENTHEIGHT = 25;

        public event EventHandler OnSelectionChange;


        public SemestreCell SelectedSemestre
        {
            get
            {
                foreach (PropositionRow row in Rows)
                    foreach (SemestreCell sc in row.Semestres)
                        if (sc.State == SemestreCell.Status.Selected)
                            return sc;

                return null;
            }
            
        }

        public PropositionRow SelectedProposition
        {
            get
            {
                foreach (PropositionRow row in Rows)
                {
                    if (row.State == PropositionRow.Status.Selected)
                        return row;
                }
                return null;
            }
            
        }

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
            }
        }


        private List<PropositionRow> _Rows = new List<PropositionRow>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),Browsable(false)]
        public List<PropositionRow> Rows
        {
            get
            {
                return _Rows;
            }
            set
            {
                _Rows = value;
            }
        }

        private AbstractPropCtrlV2Renderer _renderer;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public AbstractPropCtrlV2Renderer renderer
        {
            get
            {
                return _renderer;
            }
            set
            {
                _renderer = value;
            }
        }

        public PropositionCtrlV2()
        {
            InitializeComponent();

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            renderer = new StandardPropCtrlV2Renderer();
        }

        public void BuildSemestres()
        {
            foreach (PropositionRow pr in Rows)
            {
                pr.Semestres.Clear();
                for (int i = 0; i < 10; i++)
                {
                    SemestreCell sc = new SemestreCell();
                    sc.Index = i+1;

                    int tidx = 0;
                    foreach (Traitement t in pr.proposition.traitements)
                    {
                        TraitementCell tr = new TraitementCell();
                        tr.traitement = t;
                        tr.Index = tidx + 1;
                        pr.traitementcells.Add(tr);

                        foreach (Semestre s in t.semestres)
                            if (s.NumSemestre == (i + 1))
                                sc.Semestres.Add(s);

                        tidx++;
                    }
                    pr.Semestres.Add(sc);
                }
            }
        }

        public void Recalculate()
        {
            int currentY = 0;
            BuildSemestres();

            foreach (PropositionRow pr in Rows)
            {
                int currentX =0;
                foreach (TraitementCell tc in pr.traitementcells)
                {
                    int y = currentY + (PROPOSITIONHEIGHT - SEMESTRECELLHEIGHT);

                    int minsem = TraitementMgmt.GetSemestreMin(tc.traitement)-1;
                    currentX = (minsem*SEMESTRECELLWIDTH);
                    int w = tc.traitement.semestres.Count*SEMESTRECELLWIDTH;
                    tc.Bounds = new Rectangle(currentX, y, w, TRAITEMENTHEIGHT);
                    
                }

                pr.Bounds = new Rectangle(0, currentY, Width, PROPOSITIONHEIGHT);
                
                int currentx = 0;
                foreach (SemestreCell sc in pr.Semestres)
                {
                    int y = currentY + (PROPOSITIONHEIGHT - SEMESTRECELLHEIGHT) + TRAITEMENTHEIGHT;
                    sc.Bounds = new Rectangle(currentx, y, SEMESTRECELLWIDTH, SEMESTRECELLHEIGHT - TRAITEMENTHEIGHT);
                    currentx += SEMESTRECELLWIDTH;
                }
                currentY += PROPOSITIONHEIGHT;

            }
        }


        public void BuildNInvalidate()
        {
            Recalculate();
            Invalidate();
        }

        public void AddProposition(Proposition pr)
        {
            PropositionRow row = new PropositionRow();
            row.proposition = pr;
            Rows.Add(row);

            BuildNInvalidate();
        }


        public object Hittest(Point pt)
        {
            foreach (PropositionRow row in Rows)
            {
                foreach (SemestreCell sc in row.Semestres)
                    if (sc.Bounds.Contains(pt))
                        return sc;

                if (row.Bounds.Contains(pt))
                    return row;

                
            }
            return null;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            AffectSelection(new Point(e.X, e.Y));


                
            
            base.OnMouseDown(e);
        }

        public void AffectSelection(Point pt)
        {
            object obj = Hittest(pt);

            if (obj != null)
            {
                foreach (PropositionRow row in Rows)
                {
                    row.State = PropositionRow.Status.None;
                    foreach (SemestreCell sc in row.Semestres)
                    {
                        sc.State = SemestreCell.Status.None;

                        if (obj == sc)
                        {
                            sc.State = SemestreCell.Status.Selected;
                            row.State = PropositionRow.Status.Selected;
                            if (OnSelectionChange != null) OnSelectionChange(sc, new EventArgs());
                        }
                    }
                    if (obj == row)
                    {
                        row.State = PropositionRow.Status.Selected;
                        if (OnSelectionChange != null) OnSelectionChange(row, new EventArgs());
                    }
                }

                Invalidate();

            }
        }

        public void RemoveProposition(Proposition pr)
        {
            foreach (PropositionRow row in Rows)
            {
                if (row.proposition == pr)
                {
                    Rows.Remove(row);
                    BuildNInvalidate();
                    return;
                }
            }
        }

        public void RemoveSelection()
        {


            foreach (PropositionRow row in Rows)
            {
                if (row.State == PropositionRow.Status.Selected)
                {
                    Rows.Remove(row);
                    BuildNInvalidate();
                    return;
                }
            }
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (PropositionRow row in Rows)
            {
                renderer.DrawProposition(e.Graphics,row);

                foreach (TraitementCell tc in row.traitementcells)
                    renderer.DrawTraitement(e.Graphics, tc, row);

                foreach (SemestreCell c in row.Semestres)
                {
                    renderer.DrawSemestreCell(e.Graphics, c, row);
                }
            }

            base.OnPaint(e);
        }
    }



    [Serializable]
    public class PropositionRow
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

        private Rectangle _Bounds;
        public Rectangle Bounds
        {
            get
            {
                return _Bounds;
            }
            set
            {
                _Bounds = value;
            }
        }


        private List<TraitementCell> _traitementcells = new List<TraitementCell>();
        public List<TraitementCell> traitementcells
        {
            get
            {
                return _traitementcells;
            }
            set
            {
                _traitementcells = value;
            }
        }

        private List<SemestreCell> _Semestres= new List<SemestreCell>();
        public List<SemestreCell> Semestres
        {
            get
            {
                return _Semestres;
            }
            set
            {
                _Semestres = value;
            }
        }
    }

    [Serializable]
    public class SemestreCell
    {

        public enum Status
        {
            Selected,
            None
        }


        private List<Semestre> _Semestres = new List<Semestre>();
        /// <summary>
        /// Normallement il ne devrait y avoir qu'un seul semestre par cellule
        /// </summary>
        public List<Semestre> Semestres
        {
            get
            {
                return _Semestres;
            }
            set
            {
                _Semestres = value;
            }
        }

        private int _Index;
        public int Index
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
            }
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

        private Rectangle _Bounds;
        public Rectangle Bounds
        {
            get
            {
                return _Bounds;
            }
            set
            {
                _Bounds = value;
            }
        }

    }

    [Serializable]
    public class TraitementCell
    {

        public enum Status
        {
            Selected,
            None
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

        private int _Index;
        public int Index
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
            }
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

        private Rectangle _Bounds;
        public Rectangle Bounds
        {
            get
            {
                return _Bounds;
            }
            set
            {
                _Bounds = value;
            }
        }

    }


    
}
