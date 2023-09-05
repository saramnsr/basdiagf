using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using BasCommon_BO;
using MySql.Data.MySqlClient;
namespace BasCommon_DAL
{
    public static partial class DAC
    {
        public static DataTable getActesGroupement()
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
             //   MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    
                    //famille_Acte
                    string selectQuery = "   SELECT ac.*, a.ID, a.IDACTE, a.IDPARENT, a.PRIX_TRAITEMENT, a.QTE"
                                            + " FROM grpactes a"
                                            + " left join actes ac on a.IDACTE = ac.ID_ACTE";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);
                  //  transaction.Commit();

                    DataTable dt = ds.Tables[0];


                    return dt;

                }
                catch (System.SystemException ex)
                {
                    throw ex;
                }
                finally
                {
                   connection.Close();

                }
            }
        }
        public static DataTable getActesGroupementByIdParent(int idParent)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
               // MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    //famille_Acte
                    string selectQuery = "   SELECT ac.*, a.ID, a.IDACTE, a.IDPARENT, a.PRIX_TRAITEMENT, a.QTE"
                                            + " FROM grpactes a"
                                            + " left join actes ac on a.IDACTE = ac.ID_ACTE"
                                            + " where a.IDPARENT = @idParent or a.IDACTE = @idParent";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@idParent", idParent);
                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);
                  //  transaction.Commit();

                    DataTable dt = ds.Tables[0];


                    return dt;

                }
                catch (System.SystemException ex)
                {
                    throw ex;
                }
                finally
                {
                   connection.Close();


                }
            }
        }
        public static DataTable getActes()
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
               // MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    //famille_Acte
                    string selectQuery = "select tps_ass,tps_prat,id_famille_acte,id_acte, acte_libelle, acte_durestd, acte_couleur,";
                    selectQuery += "  nb_fautbloc, code_planing, temps_chrono,mailsubject, mailbody, mailattachement,prix_acte, SHORTLIB, ";
                    selectQuery += "  ACTE_LIBELLE_ESTIMATION, ACTE_LIBELLE_FACTURE, NOMENCLATURE, COEFFICIENT, COTATION, Id_FAUTEUIL,JOURS, PRATICIEN,";
                    selectQuery += "  actes.ordre, actes.NOMBRE_POINTS, actes.QUANTITE,actes.HEURE_DEBUT, actes.HEURE_FIN,actes.ACTE_BASE_REMBOURSEMENT,actes.ACTE_REMBOURSEMENT,actes.ACTE_DEPASSEMENT,actes.ACTE_CODE_TRANSPOSOTION,actes.TARIF";
                    selectQuery += " from actes";
                    selectQuery += " left join rh_base_familleacte on rh_base_familleacte.id= actes.id_famille_acte";
                    selectQuery += " order by rh_base_familleacte.ordre, actes.ordre";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);
                   // transaction.Commit();

                    DataTable dt = ds.Tables[0];


                    return dt;

                }
                catch (System.SystemException ex)
                {
                    throw ex;
                }
                finally
                {
                   connection.Close();

                }
            }
        }

        public static DataTable getFamillesActes()
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                //MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    //famille_Acte
                    string selectQuery = "select id, nom, Parent, couleur, ordre from rh_base_familleacte order by parent,ordre";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);
                   // transaction.Commit();

                    DataTable dt = ds.Tables[0];

                    return dt;

                }
                catch (System.SystemException ex)
                {
                    throw ex;
                }
                finally
                {
                   connection.Close();

                }
            }
        }
        public static DataTable getFamillesActesGrp()
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
               // MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    //famille_Acte
                    string selectQuery = "select id, nom, Parent, couleur, ordre from rh_base_familleactegrp order by parent,ordre";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    DataSet ds = new DataSet();
                    MySqlDataAdapter adapt = new MySqlDataAdapter(command);
                    adapt.Fill(ds);
                 //   transaction.Commit();

                    DataTable dt = ds.Tables[0];

                    return dt;

                }
                catch (System.SystemException ex)
                {
                    throw ex;
                }
                finally
                {
                   connection.Close();

                }
            }
        }
        public static void AddFamille(FamillesActe p_famille)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                }
                catch (System.Exception e)
                {
                    throw e;
                }

                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string selectQuery = "select MAX(ID)+1 as NEWID from rh_base_familleacte";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandType = CommandType.Text;
                    object res = command.ExecuteScalar();
                    if (res is DBNull)
                        p_famille.Id = 1;
                    else
                        p_famille.Id = Convert.ToInt32(command.ExecuteScalar());

                    selectQuery = "insert into rh_base_familleacte (id, nom, couleur, parent, ordre) values (@id, @nom, @couleur, @parent, @ordre)";

                    command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", p_famille.Id);
                    command.Parameters.AddWithValue("@nom", p_famille.libelle);
                    command.Parameters.AddWithValue("@couleur", p_famille.couleur.ToArgb());
                    command.Parameters.AddWithValue("@parent", p_famille.ParentFamillesActeId);
                    command.Parameters.AddWithValue("@ordre", p_famille.ordre);

                    command.ExecuteNonQuery();
                    transaction.Commit();

                }
                catch (System.SystemException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                   connection.Close();

                }
            }
        }

        public static void UpdateFamille(FamillesActe p_famille)
        {

            lock (lockobj)
            {
                if (p_famille.Id < 0) return;

                if (connection == null) getConnection(); if (connection == null) return;

                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                }
                catch (System.Exception e)
                {
                    throw e;
                }

                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {


                    string selectQuery = "update rh_base_familleacte set nom = @nom, couleur = @couleur, parent = @parent, ordre = @ordre where id=@id";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", p_famille.Id);
                    command.Parameters.AddWithValue("@nom", p_famille.libelle);
                    command.Parameters.AddWithValue("@couleur", p_famille.couleur.ToArgb());
                    command.Parameters.AddWithValue("@parent", p_famille.ParentFamillesActeId);
                    command.Parameters.AddWithValue("@ordre", p_famille.ordre);

                    command.ExecuteNonQuery();
                    transaction.Commit();

                }
                catch (System.SystemException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                   connection.Close();

                }
            }
        }
        public static void UpdateFamilleGrp(FamillesActe p_famille)
        {

            lock (lockobj)
            {
                if (p_famille.Id < 0) return;

                if (connection == null) getConnection(); if (connection == null) return;

                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                }
                catch (System.Exception e)
                {
                    throw e;
                }

                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {


                    string selectQuery = "update rh_base_familleactegrp set nom = @nom, couleur = @couleur, parent = @parent, ordre = @ordre where id=@id";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", p_famille.Id);
                    command.Parameters.AddWithValue("@nom", p_famille.libelle);
                    command.Parameters.AddWithValue("@couleur", p_famille.couleur.ToArgb());
                    command.Parameters.AddWithValue("@parent", p_famille.ParentFamillesActeId);
                    command.Parameters.AddWithValue("@ordre", p_famille.ordre);

                    command.ExecuteNonQuery();
                    transaction.Commit();

                }
                catch (System.SystemException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                   connection.Close();

                }
            }
        }

        public static void ReorderFamille(FamillesActe p_famille, int NewPos)
        {

            lock (lockobj)
            {
                if (p_famille.Id < 0) return;

                if (NewPos > p_famille.ordre) NewPos++;

                if (connection == null) getConnection(); if (connection == null) return;

                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                }
                catch (System.Exception e)
                {
                    throw e;
                }

                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {


                    string selectQuery = "update rh_base_familleacte  set ordre = ordre+1 where ordre>=@order and parent = @parent";
                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@order", NewPos);
                    command.Parameters.AddWithValue("@parent", p_famille.ParentFamillesActeId);
                    command.ExecuteNonQuery();

                    selectQuery = "update rh_base_familleacte  set ordre = @order where Id=@Id";
                    command.CommandText = selectQuery;
                    command.Parameters.AddWithValue("@Id", p_famille.Id);
                    command.ExecuteNonQuery();

                    //selectQuery = "SET GENERATOR rh_base_familleactes_order TO 0";
                    //command.CommandText = selectQuery;
                    //command.ExecuteNonQuery();

                    //selectQuery = "update rh_base_familleacte  set ordre = GEN_ID(rh_base_familleactes_order, 1 )  where parent = @parent order by ordre";
                    //command.CommandText = selectQuery;
                    //command.ExecuteNonQuery();


                    transaction.Commit();

                }
                catch (System.SystemException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                   connection.Close();

                }
            }
        }
        public static void DelFamilleGroupement(FamillesActe p_famille)
        {
            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                }
                catch (System.Exception e)
                {
                    throw e;
                }

                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {


                    string selectQuery = "delete from rh_base_familleactegrp where id=@id";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", p_famille.Id);
                    command.ExecuteNonQuery();


                    selectQuery = "delete from  grpactes where idparent = @id";
                    command.CommandText = selectQuery;
                    command.ExecuteNonQuery();
                    transaction.Commit();

                }
                catch (System.SystemException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                   connection.Close();

                }
            }
        }
        public static void DelFamille(FamillesActe p_famille)
        {
            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                }
                catch (System.Exception e)
                {
                    throw e;
                }

                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {


                    string selectQuery = "delete from rh_base_familleacte where id=@id";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", p_famille.Id);
                    command.ExecuteNonQuery();


                    selectQuery = "update actes set id_famille_acte = null where (id_famille_acte = @id)";
                    command.CommandText = selectQuery;
                    command.ExecuteNonQuery();

                    selectQuery = "update rh_base_familleacte set Parent = null where (Parent = @id)";
                    command.CommandText = selectQuery;
                    command.ExecuteNonQuery();


                    transaction.Commit();

                }
                catch (System.SystemException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                   connection.Close();

                }
            }
        }

        public static void UpdateFamilyActe(Acte p_acte, FamillesActe p_familleActe)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "update actes set id_famille_acte = @id_famille_acte where (id_acte = @id_acte)";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id_famille_acte", p_familleActe.Id);
                    command.Parameters.AddWithValue("@id_acte", p_acte.id_acte);
                    command.ExecuteNonQuery();

                    transaction.Commit();

                }
                catch (System.SystemException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
                finally
                {
                   connection.Close();
                }
            }
        }

        public static void AddActeGroupement(List<ActeGroupement> lst)
        {
            if (lst.Count == 0) return;
           // DeleteActeGroupement(lst[0].idParent);
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {
                int id =-1;
                string selectQuery = "select MAX(ID)+1 as NEWID from grpactes";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object res = command.ExecuteScalar();
                if (res is DBNull)
                    id = 1;
                else
                    id = Convert.ToInt32(command.ExecuteScalar());

                selectQuery = @"insert into grpactes (ID, 
                                           IDACTE, 
                                           IDPARENT, 
                                           PRIX_TRAITEMENT, 
                                           QTE )
                        values (@ID, 
                                @IDACTE, 
                                @IDPARENT, 
                                @PRIX_TRAITEMENT, 
                                @QTE )";

                foreach (ActeGroupement p_acte in lst)
                {
                    command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@ID", id++);
                    command.Parameters.AddWithValue("@IDACTE", p_acte.id_acte);
                    command.Parameters.AddWithValue("@PRIX_TRAITEMENT", p_acte.prixTraitement);
                    command.Parameters.AddWithValue("@IDPARENT", p_acte.idParent);
                    command.Parameters.AddWithValue("@QTE", p_acte.qte);
                    command.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
             
               connection.Close();
            }

        }

        public static void AddActe(Acte p_acte)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
            }
            catch (System.Exception e)
            {
                throw e;
            }

            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {
                string selectQuery = "select MAX(ID_ACTE)+1 as NEWID from actes";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object res = command.ExecuteScalar();
                if (res is DBNull)
                    p_acte.id_acte = 1;
                else
                    p_acte.id_acte = Convert.ToInt32(command.ExecuteScalar());

                selectQuery = @"insert into actes (id_acte, 
                                           acte_libelle, 
                                           acte_durestd, 
                                           acte_couleur, 
                                           nb_fautbloc, 
                                           id_famille_acte, 
                                           tps_prat, 
                                           tps_ass, 
                                           mailsubject, 
                                           mailbody, 
                                           mailattachement,
                                            ACTE_LIBELLE_ESTIMATION,
                                            ACTE_LIBELLE_FACTURE,
                                            NOMENCLATURE,
                                            COEFFICIENT,
                                            COTATION,
                                            SHORTLIB,
                                            PRIX_ACTE,
                                            Id_FAUTEUIL,
                                            NOMBRE_POINTS,
                                            QUANTITE,JOURS, PRATICIEN, HEURE_DEBUT, HEURE_FIN,ACTE_BASE_REMBOURSEMENT,ACTE_REMBOURSEMENT,ACTE_DEPASSEMENT,ACTE_CODE_TRANSPOSOTION,TARIF)
                        values (@id_acte, 
                                @acte_libelle, 
                                @acte_durestd, 
                                @acte_couleur, 
                                @nb_fautbloc, 
                                @idfamacte, 
                                @tps_prat, 
                                @tps_ass, 
                                @mailsubject, 
                                @mailbody, 
                                @mailattachement,
                                @ACTE_LIBELLE_ESTIMATION,
                                @ACTE_LIBELLE_FACTURE,
                                @NOMENCLATURE, 
                                @COEFFICIENT,
                                @COTATION,
                                @SHORTLIB,
                                @PRIX_ACTE,
                                @Id_FAUTEUIL,
                                @NOMBRE_POINTS,
                                @QUANTITE,@JOURS, @PRATICIEN,@HEURE_DEBUT, @HEURE_FIN,@ACTE_BASE_REMBOURSEMENT,@ACTE_REMBOURSEMENT,@ACTE_DEPASSEMENT,@ACTE_CODE_TRANSPOSOTION,@TARIF)";

                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_acte", p_acte.id_acte);
                command.Parameters.AddWithValue("@acte_libelle", p_acte.acte_libelle);
                command.Parameters.AddWithValue("@acte_durestd", p_acte.acte_durestd);
                command.Parameters.AddWithValue("@acte_couleur", System.Drawing.ColorTranslator.ToWin32(p_acte.acte_couleur));
                command.Parameters.AddWithValue("@nb_fautbloc", p_acte.nb_fautbloc);
                command.Parameters.AddWithValue("@tps_prat", p_acte.tps_prat);
                command.Parameters.AddWithValue("@tps_ass", p_acte.tps_ass);
                command.Parameters.AddWithValue("@mailsubject", p_acte.MailConfirmationSubject);
                command.Parameters.AddWithValue("@mailbody", p_acte.MailConfirmationRDVBody);
                command.Parameters.AddWithValue("@mailattachement", p_acte.MailConfirmationAttachments);
                command.Parameters.AddWithValue("@ACTE_LIBELLE_ESTIMATION", p_acte.acte_libelle_estimation);
                command.Parameters.AddWithValue("@ACTE_LIBELLE_FACTURE", p_acte.acte_libelle_facture);
                command.Parameters.AddWithValue("@NOMENCLATURE", p_acte.nomenclature);
                command.Parameters.AddWithValue("@COEFFICIENT", p_acte.coefficient);
                command.Parameters.AddWithValue("@COTATION", p_acte.cotation);
                command.Parameters.AddWithValue("@SHORTLIB", p_acte.nom_raccourci);
                command.Parameters.AddWithValue("@PRIX_ACTE", p_acte.prix_acte);
                command.Parameters.AddWithValue("@Id_FAUTEUIL", p_acte.emplacement);
                command.Parameters.AddWithValue("@NOMBRE_POINTS", Convert.ToDouble(p_acte.nombre_points));
                command.Parameters.AddWithValue("@QUANTITE", p_acte.quantite);
                command.Parameters.AddWithValue("@JOURS", p_acte.jour);
                command.Parameters.AddWithValue("@PRATICIEN", p_acte.praticien);
                command.Parameters.AddWithValue("@HEURE_DEBUT", p_acte.heure_debut );
                command.Parameters.AddWithValue("@HEURE_FIN", p_acte.heure_fin);
                command.Parameters.AddWithValue("@ACTE_BASE_REMBOURSEMENT", p_acte.BaseRemboursement);
                command.Parameters.AddWithValue("@ACTE_REMBOURSEMENT", p_acte.Remboursement);
                command.Parameters.AddWithValue("@ACTE_DEPASSEMENT", p_acte.Depassement);
                command.Parameters.AddWithValue("@ACTE_CODE_TRANSPOSOTION", p_acte.CodeTransposition);
                command.Parameters.AddWithValue("@TARIF", p_acte.Tarif);
                if (p_acte.famille_Acte == null)
                    command.Parameters.AddWithValue("@idfamacte", -1);
                else
                    command.Parameters.AddWithValue("@idfamacte", p_acte.famille_Acte.Id);

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
               
               connection.Close();
            }

        }

        public static void UpdateActe(Acte p_acte)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //string selectQuery = "update actes set acte_libelle = @acte_libelle, acte_dureestd = @acte_duree, acte_couleur = @acte_couleur, id_famille_acte = @id_famille_acte where (id_acte = @id_acte)";
                string selectQuery = @" update actes
                                    set acte_libelle = @acte_libelle,
                                        acte_durestd = @acte_duree,
                                        acte_couleur = @acte_couleur,
                                        nb_fautbloc = @nb_fautbloc,
                                        id_famille_acte = @id_famille_acte,
                                        tps_prat = @tps_prat,
                                        tps_ass = @tps_ass,
                                        mailsubject = @mailsubject,
                                        mailbody = @mailbody,
                                        SHORTLIB = @SHORTLIB,
                                        prix_acte = @prix_acte,
                                        mailattachement = @mailattachement,
                                        ACTE_LIBELLE_ESTIMATION= @ACTE_LIBELLE_ESTIMATION,
                                        ACTE_LIBELLE_FACTURE = @ACTE_LIBELLE_FACTURE,
                                        NOMENCLATURE= @NOMENCLATURE, 
                                        COEFFICIENT= @COEFFICIENT,
                                        COTATION = @COTATION,
                                        Id_FAUTEUIL = @Id_FAUTEUIL,
                                        NOMBRE_POINTS = @NOMBRE_POINTS,
                                        QUANTITE = @QUANTITE,
                                        JOURS=@JOURS,
                                        
                                        PRATICIEN = @PRATICIEN,
                                        HEURE_DEBUT = @HEURE_DEBUT,
                                        HEURE_FIN = @HEURE_FIN,
                                        ACTE_BASE_REMBOURSEMENT = @ACTE_BASE_REMBOURSEMENT,
                                        ACTE_REMBOURSEMENT = @ACTE_REMBOURSEMENT,
                                        ACTE_DEPASSEMENT = @ACTE_DEPASSEMENT,
                                        ACTE_CODE_TRANSPOSOTION = @ACTE_CODE_TRANSPOSOTION,
                                         TARIF=@TARIF
                                    where (id_acte = @id_acte)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                

                
                command.Parameters.AddWithValue("@id_acte", p_acte.id_acte);
                command.Parameters.AddWithValue("@acte_libelle", p_acte.acte_libelle);
                command.Parameters.AddWithValue("@acte_duree", p_acte.acte_durestd);
                command.Parameters.AddWithValue("@nb_fautbloc", p_acte.nb_fautbloc);
                command.Parameters.AddWithValue("@acte_couleur", System.Drawing.ColorTranslator.ToWin32(p_acte.acte_couleur));
                command.Parameters.AddWithValue("@tps_prat", p_acte.tps_prat);
                command.Parameters.AddWithValue("@tps_ass", p_acte.tps_ass);
                command.Parameters.AddWithValue("@mailsubject", p_acte.MailConfirmationSubject);
                command.Parameters.AddWithValue("@mailbody", p_acte.MailConfirmationRDVBody);
                command.Parameters.AddWithValue("@prix_acte", p_acte.prix_acte);
                command.Parameters.AddWithValue("@ACTE_LIBELLE_ESTIMATION", p_acte.acte_libelle_estimation);
                command.Parameters.AddWithValue("@ACTE_LIBELLE_FACTURE",p_acte.acte_libelle_facture );
                command.Parameters.AddWithValue("@NOMENCLATURE", p_acte.nomenclature );
                command.Parameters.AddWithValue("@COEFFICIENT",p_acte.coefficient);
                command.Parameters.AddWithValue("@COTATION",p_acte.cotation );
                command.Parameters.AddWithValue("@SHORTLIB", p_acte.nom_raccourci);
                command.Parameters.AddWithValue("@Id_FAUTEUIL", p_acte.emplacement );
                command.Parameters.AddWithValue("@NOMBRE_POINTS", Convert.ToDouble(p_acte.nombre_points.Replace(".",",")) );
                command.Parameters.AddWithValue("@QUANTITE", p_acte.quantite);
               
                

                command.Parameters.AddWithValue("@mailattachement", p_acte.MailConfirmationAttachments);
                command.Parameters.AddWithValue("@JOURS", p_acte.jour);
                command.Parameters.AddWithValue("@PRATICIEN", p_acte.praticien);
                command.Parameters.AddWithValue("@HEURE_DEBUT", p_acte.heure_debut );
                command.Parameters.AddWithValue("@HEURE_FIN", p_acte.heure_fin );
                command.Parameters.AddWithValue("@ACTE_BASE_REMBOURSEMENT", p_acte.BaseRemboursement);
                command.Parameters.AddWithValue("@ACTE_REMBOURSEMENT", p_acte.Remboursement);
                command.Parameters.AddWithValue("@ACTE_DEPASSEMENT", p_acte.Depassement);
                command.Parameters.AddWithValue("@ACTE_CODE_TRANSPOSOTION", p_acte.CodeTransposition);
                command.Parameters.AddWithValue("@TARIF", p_acte.Tarif);
                if (p_acte.famille_Acte == null)
                    command.Parameters.AddWithValue("@id_famille_acte", -1);
                else
                    command.Parameters.AddWithValue("@id_famille_acte", p_acte.famille_Acte.Id);


                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
               connection.Close();

            }

        }
        public static void DeleteActeGroupement(int idParent)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();

            }
            catch (System.Exception e)
            {
                throw e;
            }
            MySqlTransaction transaction = connection.BeginTransaction();


            try
            {
                string selectQuery = "delete from grpactes where IDPARENT=@id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", idParent);
                command.ExecuteNonQuery();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Commit();
               connection.Close();

            }
        }
        public static void DeleteActe(Acte p_acte)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            try
            {
                if (connection.State == ConnectionState.Closed) connection.Open();
            }
            catch (System.Exception e)
            {
                throw e;
            }

            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {
                string selectQuery = "delete from actes where id_acte=@id";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", p_acte.id_acte);
                command.ExecuteNonQuery();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Commit();
               connection.Close();

            }
        }

        public static Boolean SearchNameActe(string s_Acte)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
              //  MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "select count(*) from actes";
                    selectQuery += " where acte_libelle=@acte_libelle";
                    selectQuery += " Group by acte_libelle";
                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    command.Parameters.AddWithValue("@acte_libelle", s_Acte);
                    //command.ExecuteNonQuery();
                    int nb_acte = Convert.ToInt32(command.ExecuteScalar());
                    if (nb_acte > 0)
                    {
                        return false;
                    }
                    else
                        return true;

                }
                catch (System.SystemException ex)
                {
                   // transaction.Rollback();
                    throw ex;
                }
                finally
                {
                   connection.Close();
                }
            }
        }

        public static Boolean VerifRdv(Acte p_acte)
        {
            lock (lockobj)
            {
                if (connection == null) getConnection();

                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                }
                catch (System.Exception e)
                {
                    throw e;
                }

                //MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string selectQuery = "select count(*) from rendez_vous where id_acte=@id";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@id", p_acte.id_acte);
                    command.CommandType = CommandType.Text;

                    int ret = Convert.ToInt32(command.ExecuteScalar());
                    if (ret > 0)
                        return false;
                    else
                        return true;
                }
                catch (System.SystemException ex)
                {
                  //  transaction.Rollback();
                    throw ex;
                }
                finally
                {
                   connection.Close();
                }
            }
        }
    }
}
