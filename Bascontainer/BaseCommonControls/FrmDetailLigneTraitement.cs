using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BasCommon_BO;

namespace BaseCommonControls
{
    public partial class FrmDetailLigneTraitement : Form
    {
        private List<CommTraitement > _Trait;
        public List<CommTraitement> Trait
        {
            get
            {
                return _Trait;
            }
            set
            {
                _Trait = value;
            }
        }

        private CommTraitement   _Com;
        public CommTraitement Com
        {
            get
            {
                return _Com;
            }
            set
            {
                _Com = value;
            }
        }

        private Boolean  _DevisTraitement;
       private Boolean  _visualisation  = false;
       private Boolean _PrixQte = false;
       public Boolean ChangeItems;
       private double _MntDocteur,_MontantScenario, _MontantDevis;
       private NewTraitement.typeScenario typeS = new NewTraitement.typeScenario();
       public FrmDetailLigneTraitement(List<CommTraitement> Trait, CommTraitement com, Boolean visualisation = false, Boolean PrixQte = true, double MntDocteur = 0, double montantDevis = 0, double montantscenario = 0, NewTraitement.typeScenario type=NewTraitement.typeScenario.Prothése,Boolean Verif=false)
        {
           
            
            this.Trait = Trait ;
            this.Com = com;
            typeS = type;
             InitializeComponent();
            
             _visualisation = visualisation;
             _PrixQte = PrixQte;
             if (montantDevis == 0)
                 pnlDevis.Visible = false;
             if (_visualisation)            
                 btnClose.Visible = false;
                 ChangeItems = false;
                 _MntDocteur = MntDocteur;
                 _MontantScenario = montantscenario;
                 _MontantDevis = montantDevis;
                 if (MntDocteur > 0 || _visualisation)
                     dgvactepropose.Columns[colBtn.Name].Visible = false;
                 //   if (!_DevisTraitement)
                 //  {
                 // pnlDevis.Visible = false;
                 // lblEcheances.Visible = false;
                 //   }
                 if (Verif && (System.Configuration.ConfigurationManager.AppSettings["PaysCabinet" + BasCommon_DAL.DAC.prefix] == "FR") && type != NewTraitement.typeScenario.Prothése_CMUC)
                 {

                     colQte.Visible = false;
                     RemiseQte.Visible = false;
                     colBtn.Visible = false;
                     partPatient.Visible = true;
                     partMutuelle.Visible = true;
                     colRembSS.Visible = true;
                     pnlDevis.Visible = false;
                     btnClose.Visible = true;
                 }
             
        }
        private void InitDisplay()
        {
            
            if (Com == null) return;

            InitDisplayActes();

            BuildPnlDevis();



        }
        List<ActePGPropose> apgs = new List<ActePGPropose>();
        private void InitDisplayActes()
        {
           
            double totaldevis = 0;
            double totalDevisAventRemise = 0;
            ActePGPropose Tmpapg;
            if (apgs == null || apgs.Count  == 0)
            {
                Tmpapg = new ActePGPropose();
                Tmpapg.Libelle = Com.Acte.acte_libelle;
          
                Tmpapg.MontantAvantRemise = Com.Acte.prix_acte;
                Tmpapg.Montant = Com.Acte.prix_traitement;
                Tmpapg.Qte =Convert.ToInt32 ( Com.Acte.quantite);
                Tmpapg.Id = Com.Acte.id_acte;
                Tmpapg.IdTemplateActePG = 1;
                Tmpapg.RembMutuelle = Com.RembMutuelle;
                Tmpapg.partPatient = Com.partPatient;
                 Tmpapg.BaseRemboursement = Com.Acte.BaseRemboursement;
                 Tmpapg.Remboursement = Com.Acte.Remboursement;
                 Tmpapg.Depassement = Com.Acte.Depassement;
                 Tmpapg.CodeTransposition = Com.Acte.CodeTransposition;
                 Tmpapg.Tarif = Com.Acte.Tarif;
                
                apgs.Add(Tmpapg);

                foreach (CommActesTraitement a in Com.ActesSupp)
                {

                    Tmpapg = new ActePGPropose();
                    Tmpapg.Libelle = a.LibActe;
                    Tmpapg.MontantAvantRemise = a.prix_acte;
                    Tmpapg.Montant = a.prix_traitement ;
                    Tmpapg.Qte = a.Qte;
                    Tmpapg.IdTemplateActePG = 2;
                    Tmpapg.Id = a.IdActe;
                    Tmpapg.RembMutuelle = a.RembMutuelle;
                    Tmpapg.partPatient = a.partPatient;
                    Tmpapg.BaseRemboursement = a.BaseRemboursement;
                    Tmpapg.Remboursement = a.Remboursement;
                    Tmpapg.Depassement = a.Depassement;
                    Tmpapg.CodeTransposition = a.CodeTransposition;
                    apgs.Add(Tmpapg);
                   
                }
                foreach (CommActesTraitement a in Com.Radios)
                {
                 
                    Tmpapg = new ActePGPropose();
                    Tmpapg.Libelle = a.LibActe;
                    Tmpapg.MontantAvantRemise = a.prix_acte;
                    Tmpapg.Montant = a.prix_traitement;
                    Tmpapg.Qte = a.Qte;
                    Tmpapg.Id = a.IdActe;
                    Tmpapg.IdTemplateActePG = 3;
                    Tmpapg.RembMutuelle = a.RembMutuelle;
                    Tmpapg.partPatient = a.partPatient;
                    Tmpapg.Depassement = a.Depassement;
                    Tmpapg.CodeTransposition = a.CodeTransposition;
                    Tmpapg.BaseRemboursement = a.BaseRemboursement;
                    Tmpapg.Remboursement = a.Remboursement;
                    apgs.Add(Tmpapg);
                
                }
                foreach (CommActesTraitement a in Com.photos)
                {
                   
                    Tmpapg = new ActePGPropose();
                    Tmpapg.Libelle = a.LibActe;
                    Tmpapg.MontantAvantRemise = a.prix_acte;
                    Tmpapg.Montant = a.prix_traitement;
                    Tmpapg.Qte = a.Qte;
                    Tmpapg.Id = a.IdActe;
                    Tmpapg.IdTemplateActePG = 4;
                    Tmpapg.RembMutuelle = a.RembMutuelle;
                    Tmpapg.partPatient = a.partPatient;
                    Tmpapg.Depassement = a.Depassement;
                    Tmpapg.CodeTransposition = a.CodeTransposition;
                    Tmpapg.BaseRemboursement = a.BaseRemboursement;
                    Tmpapg.Remboursement = a.Remboursement;
                    apgs.Add(Tmpapg);
                }
                foreach (CommMaterielTraitement a in Com.Materiels)
                {
                 
                    Tmpapg = new ActePGPropose();
                    Tmpapg.Libelle = a.Libelle;
                    Tmpapg.MontantAvantRemise = a.prix_materiel;
                    Tmpapg.Montant = a.prix_traitement ;
                    Tmpapg.Id = a.idMateriel ;
                    Tmpapg.Qte = a.Qte;
                    Tmpapg.IdTemplateActePG = 5;
                    apgs.Add(Tmpapg);
                }
        
            }

         


            dgvactepropose.Rows.Clear();
            foreach (ActePGPropose act in apgs)
            {
        
                if (act.IdTemplateActePG==5)
                {
                    FamillesMateriels Fmat = BasCommon_BL.MaterielsMgmt.GetFamilleMaterielByIdMateriel(act.Id);
                    if (Fmat.libelle.ToLower().IndexOf("laboratoire") >= 0 || Fmat.libelle.ToLower().IndexOf("stérilisation") >= 0 || Fmat.libelle.ToLower().IndexOf("achats") >= 0 )
                    {
                        
                        totalDevisAventRemise += act.MontantAvantRemise * act.Qte;
                        totaldevis += act.Montant * act.Qte;
                    }

                }
                else
                   
                    {
                        totalDevisAventRemise += act.MontantAvantRemise * act.Qte;
                        totaldevis += act.Montant * act.Qte;
                    }
                    //if (act.template == null)
                //    act.template = TemplateApctePGMgmt.getTemplatesActeGestion(act.IdTemplateActePG);
             
              //  act.partPatient = act.MontantAvantRemise - act.RembMutuelle - act.Remboursement;

                object[] obj = new object[]{
                    act.Libelle  + " (" + act.Qte  + ")",
                    act.Qte,
                    act.MontantAvantRemise,
                    act.Montant, 
                    act.Remboursement == 0 ? "" : Convert.ToString( act.Remboursement.ToString("C2")) ,
                    act.RembMutuelle == 0 ? "" : Convert.ToString( act.RembMutuelle.ToString("C2")) ,
                    act.partPatient == 0 ? "" : Convert.ToString( act.partPatient.ToString("C2"))
                };

                int idx = dgvactepropose.Rows.Add(obj);
                dgvactepropose.Rows[idx].Tag = act;

            }
           // _MontantAvantRemise  =  _MontantDevis


            //lblTotal.Text = (_MontantDevis - (totalDevisAventRemise - totaldevis)).ToString("C2");
            //lblTotalAvantRemise.Text = _MontantDevis.ToString("C2");
            lblTotal.Text = totaldevis.ToString("C2");
            

           // lblTotalAvantRemise.Text = _MontantScenario.ToString("C2");

             
            
            if (lblTotal.Text == _MntDocteur.ToString("C2"))
                MontantDocteur.ForeColor = Color.Green;
            else
                MontantDocteur.ForeColor = Color.Red;
           // dgvactepropose.Columns.Remove(ColDel);

            //if (ReadOnly)
            //{
            //    dgvactepropose.Columns.Remove(colBtn);
            //    dgvactepropose.Columns.Remove(ColDel);
            //    btnRistourneGlobal.Visible = false;
            //}

            //BuildPnlDevis();
            BuildPnlDevis();

        }
        //private void BuildPnlDevis()
        //{

        //    double total = 0;
        //    double totalavantremise = 0;
        //    foreach (ActePGPropose acte in devis.actesHorstraitement)
        //    {
        //        total += acte.Montant;
        //        totalavantremise += acte.MontantAvantRemise;

        //    }

        //    lblTotal.Text = total.ToString("C2");
        //    lblTotalAvantRemiseSurActes.Text = totalavantremise.ToString("C2");
        //    lblTotalAvantRemiseSurActes.Visible = totalavantremise != total;


        //    if ((devis.MontantAvantRemise != null) && (devis.Montant != null) && (devis.MontantAvantRemise != devis.Montant))
        //    {
        //        lblTotalAvantRemise.Text = total.ToString("C2");
        //        lblTotalAvantRemise.Visible = true;
        //        lblTotal.Text = devis.Montant.Value.ToString("C2");
        //    }
        //    else
        //        lblTotalAvantRemise.Visible = false;

        //    InitDisplayEcheances();

        //}
        private void FrmDetailLigneTraitement_Load(object sender, EventArgs e)
        {
            InitDisplay();
        
        }
        private void InitDisplayEcheances()
        {

            string SummaryEcheances = BasCommon_BL.EcheancesMgmt.GetSummary(Com.echeancestemp.Cast<BaseTempEcheanceDefinition>().ToList());
            lblEcheances.Text = SummaryEcheances;
        }


        private void dgvactepropose_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(_visualisation))
            {
                if (dgvactepropose.Columns[e.ColumnIndex] == colBtn)
                {
                    ActePGPropose a = (ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag;

                    FrmRistourne frm = new FrmRistourne(a);
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        ///
                        double avantremise = a.MontantAvantRemise;
                        double newval = frm.Value;
                        _MontantDevis = _MontantDevis - (a.MontantAvantRemise - newval);
                        a.Montant = newval;
                        ChangeItems = true;


                        InitDisplayActes();
                        if (_DevisTraitement)
                        {
                            if (((MessageBox.Show("Attention, les échéances vont être supprimé due à la modification du montant \nSouhaitez-vous continuer ?", "Effacer les echeances", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)))
                            {


                                /////
                                if (a.IdTemplateActePG == 1)
                                {
                                    Com.Acte.prix_traitement = a.Montant;
                                }
                                if (a.IdTemplateActePG == 2)
                                {
                                    foreach (CommActesTraitement c in Com.ActesSupp)
                                    {
                                        if (c.IdActe == a.Id)
                                        {

                                            c.prix_traitement = a.Montant;
                                        }
                                    }
                                }

                                if (a.IdTemplateActePG == 3)
                                {
                                    foreach (CommActesTraitement c in Com.Radios)
                                    {
                                        if (c.IdActe == a.Id)
                                        {

                                            c.prix_traitement = a.Montant;
                                        }
                                    }
                                }
                                if (a.IdTemplateActePG == 5)
                                {
                                    foreach (CommMaterielTraitement c in Com.Materiels)
                                    {
                                        if (c.idMateriel == a.Id)
                                        {

                                            c.prix_traitement = a.Montant;


                                        }
                                    }
                                }
                                if (a.IdTemplateActePG == 4)
                                {
                                    foreach (CommActesTraitement c in Com.photos)
                                    {
                                        if (c.IdActe == a.Id)
                                        {

                                            c.prix_traitement = a.Montant;

                                        }
                                    }
                                }




                                //BaseTempEcheanceDefinition
                                DateTime TmpDate = new DateTime();
                                TmpDate = Com.echeancestemp[0].DAteEcheance;
                                Com.echeancestemp.Clear();
                                ActePG ap = new ActePG();
                                //
                                TempEcheanceDefinition ted = new TempEcheanceDefinition();
                                ted.DAteEcheance = TmpDate;
                                ted.Montant = Com.MontantLigne;
                                ted.Libelle = "Ligne : " + Com.Acte.acte_libelle;
                                ted.acte = ap;
                                ted.acte.Libelle = Com.Acte.acte_libelle;
                                //ted.acte = com.acte;
                                ted.AlreadyPayed = false;
                                ted.payeur = Echeance.typepayeur.patient;

                                Com.echeancestemp.Add(ted);

                                //




                                /////
                                BuildPnlDevis();
                            }
                        }

                    }

                }
                if (dgvactepropose.Columns[e.ColumnIndex] == RemiseQte)
                {
                    ActePGPropose a = (ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag;

                    FrmString frm = new FrmString("Quantité", "Quantité", a.Qte.ToString());
                    //FrmRistourne frm = new FrmRistourne(a);
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        ///
                        double avantremise = a.MontantAvantRemise;
                        string newval = frm.Value;

                        a.QteModifiee = Convert.ToInt32(newval);
                        ChangeItems = true;


                        InitDisplayActes();
                        if (_DevisTraitement)
                        {
                            if (((MessageBox.Show("Attention, les échéances vont être supprimé due à la modification du montant \nSouhaitez-vous continuer ?", "Effacer les echeances", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)))
                            {


                                /////
                                if (a.IdTemplateActePG == 1)
                                {
                                    Com.Acte.prix_traitement = a.Montant;
                                }
                                if (a.IdTemplateActePG == 2)
                                {
                                    foreach (CommActesTraitement c in Com.ActesSupp)
                                    {
                                        if (c.IdActe == a.Id)
                                        {

                                            c.prix_traitement = a.Montant;
                                        }
                                    }
                                }

                                if (a.IdTemplateActePG == 3)
                                {
                                    foreach (CommActesTraitement c in Com.Radios)
                                    {
                                        if (c.IdActe == a.Id)
                                        {

                                            c.prix_traitement = a.Montant;
                                        }
                                    }
                                }
                                if (a.IdTemplateActePG == 5)
                                {
                                    foreach (CommMaterielTraitement c in Com.Materiels)
                                    {
                                        if (c.idMateriel == a.Id)
                                        {

                                            c.prix_traitement = a.Montant;


                                        }
                                    }
                                }
                                if (a.IdTemplateActePG == 4)
                                {
                                    foreach (CommActesTraitement c in Com.photos)
                                    {
                                        if (c.IdActe == a.Id)
                                        {

                                            c.prix_traitement = a.Montant;

                                        }
                                    }
                                }




                                //BaseTempEcheanceDefinition
                                DateTime TmpDate = new DateTime();
                                TmpDate = Com.echeancestemp[0].DAteEcheance;
                                Com.echeancestemp.Clear();
                                ActePG ap = new ActePG();
                                //
                                TempEcheanceDefinition ted = new TempEcheanceDefinition();
                                ted.DAteEcheance = TmpDate;
                                ted.Montant = Com.MontantLigne;
                                ted.Libelle = "Ligne : " + Com.Acte.acte_libelle;
                                ted.acte = ap;
                                ted.acte.Libelle = Com.Acte.acte_libelle;
                                //ted.acte = com.acte;
                                ted.AlreadyPayed = false;
                                ted.payeur = Echeance.typepayeur.patient;

                                Com.echeancestemp.Add(ted);

                                //




                                /////
                                BuildPnlDevis();
                            }
                        }

                    }

                }
            }
                //// nadheeeeeeeeeeeeeeeeeeeeeeem
            if (dgvactepropose.Columns[e.ColumnIndex] == partMutuelle || dgvactepropose.Columns[e.ColumnIndex] == partPatient)
                  {
                      if (e.RowIndex == -1) return;
                      Boolean breakpoint = false;
                      ActePGPropose a = (ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag;
                      if (a.IdTemplateActePG == 5) return;
                      double montant = Math.Round(a.Montant,2) - Math.Round(a.Remboursement,2);
                      FrmRepartition frm = new FrmRepartition(montant,a.partPatient,a.RembMutuelle,typeS);
                      if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                      {
                          foreach (CommTraitement cc in Trait)
                          {
                             
                              if (cc.Id == Com.Id && !breakpoint)
                              {
                                  ActePG ap = new ActePG();
                                  foreach (ActePGPropose ac in apgs)
                                  {
                                     
                                      if (a.IdTemplateActePG == 1 && !breakpoint)
                                      {
                                          if (ac.Id == a.Id)
                                          {
                                            //  cc.RembMutuelle = frm.montantMutuelle;
                                              a.RembMutuelle = frm.montantMutuelle;
                                            //  cc.partPatient = frm.montantPatient;
                                              a.partPatient = frm.montantPatient;
                             
                                              breakpoint = true;
                                          }
                                      }
                                         
                                      else
                                          if (a.IdTemplateActePG == 2 && !breakpoint)
                                          {
                                              foreach (CommActesTraitement c in cc.ActesSupp)
                                              {
                                                  if (c.IdActe == a.Id)
                                                  {
                                                   //   c.RembMutuelle = frm.montantMutuelle;
                                                      a.RembMutuelle = frm.montantMutuelle;
                                                   //   c.partPatient = frm.montantPatient;
                                                      a.partPatient = frm.montantPatient;
                                                      breakpoint = true;
                                     

                                                  }
                                              }
                                          }
                                          else
                                              if (a.IdTemplateActePG == 4 && !breakpoint)
                                              {
                                                  foreach (CommActesTraitement c in cc.photos)
                                                  {
                                                      if (c.IdActe == a.Id)
                                                      {
                                                     //     c.RembMutuelle = frm.montantMutuelle;
                                                          a.RembMutuelle = frm.montantMutuelle;
                                                      //    c.partPatient = frm.montantPatient;
                                                          a.partPatient = frm.montantPatient;
                                                          breakpoint = true;
                                                  

                                                      }
                                                  }
                                              }
                                              else
                                                  if (a.IdTemplateActePG == 3 && !breakpoint)
                                                  {
                                                      foreach (CommActesTraitement c in cc.Radios)
                                                      {
                                                          if (c.IdActe == a.Id)
                                                          {
                                                           //   c.RembMutuelle = frm.montantMutuelle;
                                                              a.RembMutuelle = frm.montantMutuelle;
                                                          //    c.partPatient = frm.montantPatient;
                                                              a.partPatient = frm.montantPatient;
                                                              breakpoint = true;
                                                             
                                                          }
                                                      }
                                                  }
                                 
                                      break;
                                  }
                                 
                              }
                              if (breakpoint)
                                  break;
                          }
                         
                                

                      }
                      InitDisplay();
                  }

                 
                  
        
            }

        private void dgvactepropose_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex != colTarif.Index) return;

            Font ftstriked = new System.Drawing.Font("Garamond", 12, FontStyle.Strikeout);
            Font ft = new System.Drawing.Font("Garamond", 12, FontStyle.Regular);

            ActePGPropose apgp = (ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag;

            e.PaintBackground(e.CellBounds, true);
            if (Math.Round ( apgp.Montant,2)  != Math.Round( apgp.MontantAvantRemise,2))
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(apgp.MontantAvantRemise.ToString("C2"), ftstriked, new SolidBrush(e.CellStyle.ForeColor), e.CellBounds, sf);


                sf.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(apgp.Montant.ToString("C2"), ft, new SolidBrush(e.CellStyle.ForeColor), e.CellBounds, sf);
            }
            else
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(apgp.MontantAvantRemise.ToString("C2"), ft, new SolidBrush(e.CellStyle.ForeColor), e.CellBounds, sf);
            }

            e.Handled = true;
        }

        private void dgvactepropose_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (_PrixQte)
            {
                if (colQte.Index == e.ColumnIndex)
                {
                    int oldqte = ((ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag).Qte;
                    int qte = Convert.ToInt32(dgvactepropose.Rows[e.RowIndex].Cells[colQte.Index].Value);


                    ((ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag).MontantAvantRemise = (((ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag).MontantAvantRemise / oldqte) * qte;
                    ((ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag).Montant = ((ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag).MontantAvantRemise;
                    ((ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag).Qte = qte;

                    //devis.Montant = null;
                    //devis.MontantAvantRemise = null;

                    dgvactepropose.Rows[e.RowIndex].Cells[colTarif.Index].Value = ((ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag).Montant;

                    //if ((devis.echeancestemp == null) || (devis.echeancestemp.Count < 2) ||
                    //   ((devis.echeancestemp.Count > 1) &&
                    //   (MessageBox.Show("Attention, les échéances vont être supprimé due à la modification du montant du devis\nSouhaitez-vous continuer ?", "Effacer les echeances", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)))
                    //{

                    //    devis.echeancestemp.Clear();
                    //    devis.echeancestemp.Add(MgmtDevis.CreateEcheanceDevisALaCarte(devis));

                    //}
                    BuildPnlDevis();

                }
            }
            else
            {
                if (colQte.Index == e.ColumnIndex)
                {
                    ((ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag).Qte = Convert.ToInt32(dgvactepropose.Rows[e.RowIndex].Cells[colQte.Index].Value);
                }
            }
        }


        private void dgvactepropose_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvactepropose_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (colQte.Index == e.ColumnIndex )
            {
                if (int.Parse(dgvactepropose.Rows[e.RowIndex].Cells[colQte.Index].Value.ToString()).GetType().Equals(typeof(int)))
                {
                    ((ActePGPropose)dgvactepropose.Rows[e.RowIndex].Tag).Qte = Convert.ToInt32(dgvactepropose.Rows[e.RowIndex].Cells[colQte.Index].Value);
                    InitDisplayActes();
                }
                else
                    MessageBox.Show("Valeur erronée!");
            }
        }

        private void dgvactepropose_RowValidated(object sender, DataGridViewCellEventArgs e)
        {

        }
        private bool Build()
        {


            return true;

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (Build())
            {
                foreach (CommTraitement  cc in Trait)
                {
                    if (cc.Id == Com.Id )
                    {
                        foreach (ActePGPropose a in apgs)
                        {
                            if (a.IdTemplateActePG == 1)
                            {
                                     cc.RembMutuelle = a.RembMutuelle;
                                      cc.partPatient = a.partPatient;
                                    cc.Acte.prix_traitement = a.Montant;
                                    cc.Acte.quantite = a.Qte.ToString ();
                            }
                            if (a.IdTemplateActePG == 2)
                            {
                                foreach (CommActesTraitement c in cc.ActesSupp)
                                {
                                    if (c.IdActe == a.Id)
                                    {
                                        cc.RembMutuelle = a.RembMutuelle;
                                        cc.partPatient = a.partPatient;
                                            c.prix_traitement = a.Montant;
                                            c.Qte = a.Qte;
                                   }
                                }
                            }
                            if (a.IdTemplateActePG == 3)
                            {
                                foreach (CommActesTraitement c in cc.Radios)
                                {
                                    if (c.IdActe == a.Id)
                                    {
                                        cc.RembMutuelle = a.RembMutuelle;
                                        cc.partPatient = a.partPatient;
                                            c.prix_traitement = a.Montant;
                                            c.Qte = a.Qte;
                                   }
                                }
                            }
                            if (a.IdTemplateActePG == 5)
                            {
                                foreach (CommMaterielTraitement  c in cc.Materiels)
                                {
                                    if (c.idMateriel  == a.Id)
                                    {
                                 
                                            c.prix_traitement = a.Montant;
                                            c.Qte = a.Qte;

                                    }
                                }
                            }
                            if (a.IdTemplateActePG == 4)
                            {
                                foreach (CommActesTraitement c in cc.photos)
                                {
                                    if (c.IdActe == a.Id)
                                    {
                                        cc.RembMutuelle = a.RembMutuelle;
                                        cc.partPatient = a.partPatient;
                                            c.prix_traitement = a.Montant;
                                            c.Qte = a.Qte;
                                    }
                                }
                            }
                        }

                    }
                }
             
                DialogResult = System.Windows.Forms.DialogResult.OK;
                
                Close();
            }
        }

        private void btnRistourneGlobal_Click(object sender, EventArgs e)
        {
            double total = 0;
            List<TempEcheanceDefinition> EcheancesDevis = new List<TempEcheanceDefinition>();

             
            FrmRistourne frm = new FrmRistourne(Com );
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (( (MessageBox.Show("Attention, les échéances vont être supprimé due à la modification du montant \nSouhaitez-vous continuer ?", "Effacer les echeances", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)))
                {
                    double avantremise = Com.MontantLigneAvantRemise;
                    double newval = frm.Value;

                    Com.MontantLigne   = newval;

                    DateTime TmpDate = new DateTime();
                    TmpDate = Com.echeancestemp[0].DAteEcheance;
                    Com.echeancestemp.Clear();
                    ActePG ap = new ActePG();
                    //
                    TempEcheanceDefinition ted = new TempEcheanceDefinition();
                    ted.DAteEcheance = TmpDate;
                    ted.Montant = Com.MontantLigne;
                    ted.Libelle = "Ligne : " + Com.Acte.acte_libelle;
                    ted.acte = ap;
                    ted.acte.Libelle = Com.Acte.acte_libelle;
                    //ted.acte = com.acte;
                    ted.AlreadyPayed = false;
                    ted.payeur = Echeance.typepayeur.patient;

                    Com.echeancestemp.Add(ted);

                    


                    BuildPnlDevis();
                }
            }


        }


        private void BuildPnlDevis()
        {

            //lblTotalAvantRemise.Text = Com.MontantLigneAvantRemise.ToString("C2");
        //    lblTotalAvantRemise.Text = _MontantAvantRemise.ToString("C2");

            //lblTotal.Text = Com.MontantLigne.ToString("C2");
        //    lblTotal.Text = _MontantDevis.ToString("C2");

        //    lblTotalAvantRemise.Visible = lblTotalAvantRemise.Text != lblTotal.Text;
           

            InitDisplayEcheances();
            if (_MntDocteur > 0)
            {
                MontantRemise.Text = (_MontantDevis     - _MntDocteur).ToString("C2");
                MontantDocteur.Visible = true;
                MontantRemise.Visible = false;
                label4.Visible = false;
                MontantDocteur.Text = _MntDocteur.ToString("C2");
            }
            else
            {
                MontantDocteur.Visible = false;
                MontantRemise.Visible = false;
                label4.Visible = false;
                label3.Visible = false;
            }
        }
        private void lblEcheances_Click(object sender, EventArgs e)
        {
            /*
            if (Com.echeancestemp == null)
                Com.echeancestemp = MgmtDevis.get_EcheancesDevisALaCarte(Com);



            FrmFinancement frm = new FrmFinancement(devis, CurrentPatient, devis.echeancestemp.Cast<BaseTempEcheanceDefinition>().ToList());
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                devis.echeancestemp.Clear();
                BasCommon_BL.MgmtDevis.DeleteEcheanceDevisALaCarte(devis);
                foreach (BaseTempEcheanceDefinition ted in frm.Montants)
                {
                    if (ted.acte != null)
                    {
                        EcheanceDevisALaCarte edc = EcheanceDevisALaCarte.FromBaseTempEcheanceDefinition(ted);
                        edc.devis = devis;
                        devis.echeancestemp.Add(edc);
                        BasCommon_BL.MgmtDevis.AddEcheanceDevisALaCarte(edc);
                    }
                }

                InitDisplayEcheances();
            }
              */
        }

        private void MontantDocteur_Click(object sender, EventArgs e)
        {

        }

     
       
       
    }
}
