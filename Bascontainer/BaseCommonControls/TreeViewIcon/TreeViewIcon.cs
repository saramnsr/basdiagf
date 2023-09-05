using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaseCommonControls.Ctrls.BO;
using System.Text.RegularExpressions;
using System.Configuration;




namespace BaseCommonControls.Ctrls
{
    public partial class TreeViewIcon : UserControl
    {

        #region properties

        public event EventHandler OnLongClickEvent;


        Timer BigClickTimer = new Timer();

        public List<trButton> ButtonList = new List<trButton>();

        public PaintEventHandler ButtonPaint;
        public PaintEventHandler HeaderPaint;
        public event EventHandler OnChangeLevel;
        public event EventHandler OnSelected;
        public event EventHandler OnRemove;
        public Boolean ChangeItems;
        AbstractTreeViewRenderer renderer = new StandardTreeViewRenderer();

        int SPACEBETWEENBUTTONSWIDTH = 9;
        int SPACEBETWEENBUTTONSHEIGHT = 9;
        int HEADERHEIGHT = 40;

        int TotalHeight;
        private ListBoxItem selectedListBox;

        private trButton m_DragingBefore;
        public TreeNode DragingBefore
        {
            get { return ((TreeNode)m_DragingBefore.Tag); }
        }

        private TreeNode m_HoveredNode;
        public TreeNode HoveredNode
        {
            get { return m_HoveredNode; }
        }

        private TreeNode m_SelectedNode;
        public TreeNode SelectedNode
        {
            set { m_SelectedNode = value; }
            get { return m_SelectedNode; }
        }

        public TreeNode CurrentNode
        {
            get { return m_CurrentNode; }
            set { m_CurrentNode = value; }
        }


        private int m_ButtonHeight = 75;
        public int ButtonHeight
        {
            set { m_ButtonHeight = value; RecalculEmplacementButtons(true); }
            get { return m_ButtonHeight; }
        }
        int WidthTreeView = 0;
        private int m_ButtonWidth = 75;
        public int ButtonWidth
        {
            set { m_ButtonWidth = value; RecalculEmplacementButtons(true); }
            get { return m_ButtonWidth; }
        }

        public TreeNode Root = new TreeNode();
        private TreeNode m_CurrentNode;


        public string m_ChoixFamille = "";
        public string ChoixFamille
        {
            set { m_ChoixFamille = value; }
            get { return m_ChoixFamille; }
        }
        public Boolean m_isCreated = false;
        public Boolean isCreated
        {
            set { m_isCreated = value; }
            get { return m_isCreated; }
        }
        public Boolean  m_MultiChoiceVisibilite = false;
        public Boolean  MultiChoiceVisibilite
        {
            set { m_MultiChoiceVisibilite = value;  }
            get { return m_MultiChoiceVisibilite; }
        }
        private double _totaleActeSupp;
        public double totaleActeSupp
        {
            set { _totaleActeSupp = value; }
            get { return _totaleActeSupp; }
        }

        #endregion

        #region ctor
        public TreeViewIcon()
        {
            InitializeComponent();
            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);

            m_CurrentNode = Root;
          
            RecalculEmplacementButtons(true);

            ChangeItems = false;
          
            this.AllowDrop = true;
          
            BigClickTimer.Interval = 1500;
            BigClickTimer.Tick += new EventHandler(BigClickTimer_Tick);

        }

        void BigClickTimer_Tick(object sender, EventArgs e)
        {
            BigClickTimer.Stop();

            if ((Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
            {
                if (OnLongClickEvent != null)
                    OnLongClickEvent(this, new EventArgs());
            }


        }
        #endregion

        #region methods


        private trButton ButtonAt(Point p_PtForButton)
        {
            foreach (trButton b in ButtonList)
            {
                if ((b.Bound.Left < p_PtForButton.X) && (b.Bound.Right > p_PtForButton.X) &&
                    (b.Bound.Top < p_PtForButton.Y) && (b.Bound.Bottom > p_PtForButton.Y))
                {
                    return b;
                }
            }
            return null;
        }

        private bool HitAtHeader(Point p_Pt)
        {

            if ((HeaderRect.Left < p_Pt.X) && (HeaderRect.Right > p_Pt.X) &&
                (HeaderRect.Top < p_Pt.Y) && (HeaderRect.Bottom > p_Pt.Y))
            {
                return true;
            }
            else return false;
        }


        public void Clear()
        {
            m_CurrentNode = Root;
            Root.Nodes.Clear();
        }

        public TreeNode GetNodeAt(Point p_point)
        {
            foreach (trButton b in ButtonList)
            {
                if ((b.Bound.X < p_point.X) && (b.Bound.Y < p_point.Y)
                    && (b.Bound.X + b.Bound.Width > p_point.X) && (b.Bound.Y + b.Bound.Height > p_point.Y))
                    return ((TreeNode)b.Tag);
            }
            return null;
        }

       
        public Boolean   Alimenter_listBox(String Text, String Info, Boolean CtrlAjout)
        {
            if (listBox1.Items.Count == Int32.Parse(ConfigurationManager.AppSettings["NbreActeSupp"]) && CtrlAjout)
            {
                return false;
            }
            else
            {

                Boolean redondant = false;
                listBox1.ItemHeight = 30;

                foreach (var listBoxItem in listBox1.Items)
                {
                    ListBoxItem item = (ListBoxItem)listBoxItem;
                    string[] words = item.Tag.ToString().Split('$');
                    string[] wordsinfo = Info.Split('$');

                    if (words[0] == wordsinfo[0])
                    {
                        words[10] = (Convert.ToInt32(wordsinfo[10]) + Convert.ToInt32(words[10])).ToString(); 
                        redondant = true;
                        item.Tag = string.Join("$", words);
                        item.Text = Text + " (" + words[10] + ")";
                        listBox1.Refresh();
                        
                    }
               
                }
                if (!redondant)
                {

                    ListBoxItem itemMC = new ListBoxItem();

                   string[] ActeInfo = Info.Split('$');
                   
                    itemMC.Text = Text + " (" + ActeInfo [10] + ")";
                    itemMC.Tag = Info;
                    itemMC.Name = Text;

                    listBox1.Items.Add(itemMC);
                    ListBoxItem itemMC2 = new ListBoxItem();


                    itemMC2.Text = "";
                    itemMC2.Tag = "";

                    


                    listBox1.DrawMode = DrawMode.OwnerDrawVariable;

                    listBox1.MeasureItem += new MeasureItemEventHandler(listBox1_MeasureItem);
                    listBox1.DrawItem += new DrawItemEventHandler(listBox1_DrawItem);
                }
                totaleActeSupp = 0;
                foreach (var listBoxItem in listBox1.Items)
                {
                    ListBoxItem item = (ListBoxItem)listBoxItem;
                    string[] words = item.Tag.ToString().Split('$');
                    if (Convert.ToBoolean(words[words.Length - 1]) == false)
                    {

                        totaleActeSupp += Convert.ToDouble(words[9]) * Convert.ToInt32(words[10]);
                    }

                }
                return true;
            }
      
        }
        private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            //e.ItemHeight = 10;
              
            if (listBox1.Items[e.Index].ToString() != "")
            {
                e.ItemHeight = 30;
            }
            //else
            //{
            //    e.ItemHeight = 10;
            //}
          
        }
        void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                if (listBox1.Items[e.Index].ToString() != "")
                {

                    e = new DrawItemEventArgs(e.Graphics,
                                              e.Font,
                                              e.Bounds,
                                              e.Index,
                                              e.State ^ DrawItemState.Selected,
                                              e.ForeColor,
                                              Color.WhiteSmoke);



                    // Draw the background of the ListBox control for each item.
                    e.DrawBackground();

                    ListBoxItem item = (ListBoxItem)listBox1.Items[e.Index];

                    string[] words = item.Tag.ToString().Split('$');
                    String a = words[3];

                   var numbers = a.Split(',').ToList();

               
                   
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(Int32.Parse(Regex.Match(words[3], @"\d+").Value), Int32.Parse(Regex.Match(words[4], @"\d+").Value), Int32.Parse(Regex.Match(words[5], @"\d+").Value), Int32.Parse(Regex.Match(words[6], @"\d+").Value))), e.Bounds);
                        Font font;
                        if (Convert.ToBoolean(words[words.Length -1]) == true)
                        {
                            font = new Font("garamond", 12, FontStyle.Strikeout);
                        }
                        else
                            font = e.Font;
                    // Draw the current item text
                    SizeF size = e.Graphics.MeasureString(item.ToString(), font);
                    
                    e.Graphics.DrawString(listBox1.Items[e.Index].ToString(),font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width / 2 - size.Width / 2), e.Bounds.Top + (e.Bounds.Height / 2 - size.Height / 2));

                    // If the ListBox has focus, draw a focus rectangle around the selected item.
                    e.DrawFocusRectangle();
                }
            }
        }

        public void RecalculEmplacementButtons(bool needUpdate)
        {
            listBox1.Height = this.Height - 50 ;

            ButtonList.Clear();
            if (m_CurrentNode == null) return;
            foreach (TreeNode n in m_CurrentNode.Nodes)
            {
                trButton btn = new trButton();
                btn.Tag = n;
                btn.Text = n.Text;
                ButtonList.Add(btn);
            }

            int x = 0;
            int y = HEADERHEIGHT + SPACEBETWEENBUTTONSHEIGHT - VerticalScrollBar.Value;
            if (MultiChoiceVisibilite)
            {
            }
            else
            {

            }
            foreach (trButton b in ButtonList)
            {
                x += SPACEBETWEENBUTTONSWIDTH;

//                if (x + m_ButtonWidth > this.Width)
                
                if (MultiChoiceVisibilite)
                    WidthTreeView = this.Width - listBox1.Width - 30;
                else
                    WidthTreeView = this.Width;

                 if (x + m_ButtonWidth > WidthTreeView)
                {
                    x = SPACEBETWEENBUTTONSWIDTH;
                    y += SPACEBETWEENBUTTONSHEIGHT;
                    y += m_ButtonHeight;
                }
                
              

                b.Bound = new Rectangle(x, y, m_ButtonWidth, m_ButtonHeight);
                x += m_ButtonWidth;
            }

            y += m_ButtonHeight + SPACEBETWEENBUTTONSHEIGHT;
            TotalHeight = Math.Abs(y - this.Height);


            if (needUpdate)
            {
                VerticalScrollBar.Value = VerticalScrollBar.Minimum;
                VerticalScrollBar.Maximum = TotalHeight + (SPACEBETWEENBUTTONSHEIGHT * 5);
                VerticalScrollBar.Visible = y > this.Height;
            }

            listBox1.Location = new Point(WidthTreeView - 10, HEADERHEIGHT + 5);

            Invalidate();


        }

        #endregion

        #region events
        private void VerticalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            RecalculEmplacementButtons(false);
        }

        Rectangle HeaderRect;
        private void DrawHeader(PaintEventArgs e)
        {
            HeaderRect = Bounds;
            HeaderRect.X = 5;
            HeaderRect.Y = 5;
            HeaderRect.Height = HEADERHEIGHT - 10;
            HeaderRect.Width = e.ClipRectangle.Width - VerticalScrollBar.Width - 10;

            if (HeaderPaint != null)
                HeaderPaint(this, new PaintEventArgs(e.Graphics, HeaderRect));
            else
                renderer.DrawHeader(e.Graphics, HeaderRect, m_CurrentNode);


        }

        //private void TreeViewIcon_Paint(object sender, PaintEventArgs e)
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (trButton b in ButtonList)
            {

                if (ButtonPaint != null)
                     ButtonPaint(b.Tag, new PaintEventArgs(e.Graphics, b.Bound));
                else
                    b.Draw(renderer, e.Graphics, ((TreeNode)b.Tag) == m_SelectedNode, ((TreeNode)b.Tag) == m_HoveredNode);

                if (b == m_DragingBefore) e.Graphics.DrawLine(new Pen(Brushes.Black, 2), new Point(b.Bound.X - 4, b.Bound.Y), new Point(b.Bound.X - 4, b.Bound.Y + b.Bound.Height));
            }

            DrawHeader(e);



        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
           
            if (HitAtHeader(new Point(e.X, e.Y)))
            {
                if (m_CurrentNode.Parent != null)
                {
                   // If(m_CurrentNode .Parent .Level == 0 &&  
                    if (m_CurrentNode.Parent .Level == 0 && m_ChoixFamille !="") return;
                    m_CurrentNode = m_CurrentNode.Parent;
                    m_SelectedNode = null;

                }
            }
            else
            {
                trButton b = ButtonAt(new Point(e.X, e.Y));
                if ((b != null) && (((TreeNode)b.Tag).FirstNode != null))
                {
                    m_CurrentNode = ((TreeNode)b.Tag);
                    m_SelectedNode = null;

                }
            }
            RecalculEmplacementButtons(true);
            base.OnMouseClick(e);

        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
            {
                TreeNode nde = GetNodeAt(DragDropPoint);
                if (nde == null) return;
                if (((Math.Abs(DragDropPoint.X - pt.X) > 5) || (Math.Abs(DragDropPoint.Y - pt.Y) > 5)))
                {
                    BigClickTimer.Enabled = false;
                    if (nde.Tag!=null)
                        DoDragDrop(nde.Tag, DragDropEffects.Copy);
                }
            }







            base.OnMouseMove(e);
        }


        private Point DragDropPoint;
        protected override void OnMouseDown(MouseEventArgs e)
        {


            BigClickTimer.Start();

            trButton b = ButtonAt(new Point(e.X, e.Y));
            if (b != null)
            {
                DragDropPoint = new Point(e.X, e.Y);
                m_SelectedNode = ((TreeNode)b.Tag);
                if (OnSelected != null) OnSelected(b.Tag, new EventArgs());
            }
            else
                m_SelectedNode = null;

            Invalidate();


            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            BigClickTimer.Stop();
            base.OnMouseUp(e);
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            if (m_HoveredNode != null)
            {

            }

            if (m_DragingBefore != null)
            {

                this.Root.Nodes.Remove(m_SelectedNode);
                this.Root.Nodes.Insert(((TreeNode)m_DragingBefore.Tag).Index, m_SelectedNode);

                RecalculEmplacementButtons(true);

                this.Invalidate();

            }

            base.OnDragDrop(drgevent);
        }


        protected override void OnDragOver(DragEventArgs e)
        {

            e.Effect = DragDropEffects.All;

            DragDropPoint = this.PointToClient(new Point(e.X, e.Y));
            Point DragDropPointNext = this.PointToClient(new Point(e.X + SPACEBETWEENBUTTONSWIDTH, e.Y));

            trButton b = ButtonAt(DragDropPoint);

            if (b != null)
            {
                if (DragDropPoint.X - b.Bound.X > SPACEBETWEENBUTTONSWIDTH)
                {
                    m_HoveredNode = ((TreeNode)b.Tag);
                    m_DragingBefore = null;
                }
            }
            else
            {
                b = ButtonAt(DragDropPointNext);

                if ((b != null) && (DragDropPoint.X - b.Bound.X < SPACEBETWEENBUTTONSWIDTH))
                {
                    m_HoveredNode = null;
                    m_DragingBefore = b;
                }
            }






            Invalidate();



            base.OnDragOver(e);
        }



        #endregion

        private void TreeViewIcon_Load(object sender, EventArgs e)
        {
            if (MultiChoiceVisibilite)
            {
                listBox1.Visible = true;
                listBox1.Width = 173;
            }
            else
            {
                listBox1.Visible = false;
                listBox1.Width = 0;
            }


            //BasCommon_BO.CommClinique cclinique = new BasCommon_BO.CommClinique();
            //string test = cclinique.ActesSupp.Count.ToString();
            //MessageBox.Show(test);

        
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        
            ListBoxItem item = (ListBoxItem)listBox1.SelectedItem;
            if (item == null) return;
            string[] words = item.Tag.ToString().Split('$');
               

              //MessageBox.Show(listBoxChoice.SelectedIndex.ToString ());
            if (listBox1.SelectedIndex > -1 )
              {
                  if (listBox1.Items[listBox1.SelectedIndex].ToString() != "")
                  {
                      if (words .Length >12)
                      if (Int32.Parse(words[11]) > 0 && Int32.Parse(words[12]) > 0)
                      {
                          MessageBox.Show("Suppression impossible, Acte déjà encaissée");
                          return;
                      }
                      if (this.isCreated)
                      {
                          if (Convert.ToBoolean(words[words.Length - 1]))
                          {
                              if (MessageBox.Show("Vous voulez activer cet acte ?", "Activer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                  words[words.Length - 1] = "False";
                              selectedListBox.Tag = string.Join("$", words);
                              listBox1.Items.RemoveAt(listBox1.Items.IndexOf(selectedListBox));
                              totaleActeSupp += Convert.ToDouble(words[9]) * Convert.ToDouble(words[10]);
                              Alimenter_listBox(words[1], selectedListBox.Tag.ToString(), false);

                          }
                          else
                          {
                              if (MessageBox.Show("Vous voulez désactiver cet acte ?", "Désactiver", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                  words[words.Length - 1] = "True";
                              selectedListBox.Tag = string.Join("$", words);
                              totaleActeSupp -= Convert.ToDouble(words[9]) * Convert.ToDouble(words[10]);
                              listBox1.Items.RemoveAt(listBox1.Items.IndexOf(selectedListBox));
                              Alimenter_listBox(words[1], selectedListBox.Tag.ToString(), false);



                          }
                      }
                      else
                      {
                          if (MessageBox.Show("Vous voulez supprimer cet acte ?", "Désactiver", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                          listBox1.Items.RemoveAt(listBox1.SelectedIndex);

                      }
                      totaleActeSupp = 0;
                      foreach (var listBoxItem in listBox1.Items)
                      {
                          ListBoxItem items = (ListBoxItem)listBoxItem;
                          string[] word = items.Tag.ToString().Split('$');
                          if(!Convert.ToBoolean(word[word.Length - 1]))
                              totaleActeSupp += Convert.ToDouble(word[9]) * Convert.ToDouble(word[10]);

                      }
                      ChangeItems = true;
                  }

                  if (OnRemove != null)
                      OnRemove(this, new EventArgs());
              }
              
          
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            selectedListBox = new ListBoxItem();
      
           int index = listBox1.IndexFromPoint(e.X, e.Y);
           try
           {
               selectedListBox = (ListBoxItem)listBox1.Items[index];
           }
           catch(Exception ex)
           {
               selectedListBox = null;
           }
            
           // cm.MenuItems.Add("Supprimer");
           
         //   listBox1.ContextMenuStrip = cm;
        //    listBox1.SelectedIndex = -1;
        }

        private void listBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //BigClickTimer.Start();

            //Point pt = new Point(e.X, e.Y);
         

            //Invalidate();


            //base.OnMouseDown(e);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
      

        }

        private void ajouterToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  listBox1.SelectedIndex = listBox1.IndexFromPoint(e.X, e.Y);
        }

        private void ajouterToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (selectedListBox == null) return;
            string[] words = selectedListBox.Tag.ToString().Split('$');
            words[words.Length - 1] = "false"; 
            selectedListBox.Tag = string.Join("$", words);
            listBox1.Items.RemoveAt(listBox1.Items.IndexOf(selectedListBox));
            totaleActeSupp += Convert.ToDouble(words[9]) * Convert.ToDouble(words[10]);
            Alimenter_listBox(words[1], selectedListBox.Tag.ToString(),false);
            ChangeItems = true;
        }

        private void supprimerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (selectedListBox == null) return;
            string[] words = selectedListBox.Tag.ToString().Split('$');
            words[words.Length -1] = "true";
            selectedListBox.Tag = string.Join("$",words);
            totaleActeSupp -= Convert.ToDouble(words[9]) * Convert.ToDouble(words[10]);
            listBox1.Items.RemoveAt(listBox1.Items.IndexOf(selectedListBox));
            Alimenter_listBox(words[1], selectedListBox.Tag.ToString(),false);
            ChangeItems = true;
        }

        private void contextMenuStrip1_Opening_1(object sender, CancelEventArgs e)
        {

        }

    }

    public class ListBoxItem : Object
    {
        public virtual string Text { get; set; }
        public virtual object Tag { get; set; }
        public virtual object Object { get; set; }
        public virtual string Name { get; set; }

        /// <summary>
        /// Class Constructor
        /// </summary>
        public ListBoxItem()
        {
            this.Text = string.Empty;
            this.Tag = null;
            this.Name = string.Empty;
            this.Object = null;
            
        }

        /// <summary>
        /// Overloaded Class Constructor
        /// </summary>
        /// <param name="Text">Object Text</param>
        /// <param name="Name">Object Name</param>
        /// <param name="Tag">Object Tag</param>
        /// <param name="Object">Object</param>
        public ListBoxItem(string Text, string Name, object Tag, object Object)
        {
            this.Text = Text;
            this.Tag = Tag;
            this.Name = Name;
            this.Object = Object;
        }

        /// <summary>
        /// Overloaded Class Constructor
        /// </summary>
        /// <param name="Object">Object</param>
        public ListBoxItem(object Object)
        {
            this.Text = Object.ToString();
            this.Name = Object.ToString();
            this.Object = Object;
        }

        /// <summary>
        /// Overridden ToString() Method
        /// </summary>
        /// <returns>Object Text</returns>
        public override string ToString()
        {
            return this.Text;
        }
    }
}
