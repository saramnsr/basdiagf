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
        public static DataTable getTraitements()
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
                //MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    //famille_Traitement
                    string selectQuery = "select traitements.*,rh_base_familletraitement.*  from traitements";
                    selectQuery += " left join rh_base_familletraitement on rh_base_familletraitement.id= traitements.id_famille_Traitement";
                    selectQuery += " order by rh_base_familletraitement.ORDRE, traitements.ORDRE";


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

        public static DataTable getTraitement(int id_traitement)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
              //  MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    //famille_Traitement
                    string selectQuery = "select traitements.*,rh_base_familletraitement.*  from traitements";
                    selectQuery += " left join rh_base_familletraitement on rh_base_familletraitement.id= traitements.id_famille_Traitement";
                    selectQuery += " WHERE traitements.id_traitement = @id_traitement ";
                    selectQuery += " order by id_famille_Traitement,Traitement_libelle";



                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@id_traitement", id_traitement);
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
        public static DataTable GetTraitementAutrePersonne(CommTraitement  com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
           // MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = "select id_traitement, ";
                selectQuery += "        ID_CORRESPONDANT, ";
                selectQuery += "        personne.per_nom , ";
                selectQuery += "        personne.per_prenom  ";
                selectQuery += " from base_traitement_autrepers";
                selectQuery += " inner join personne on personne.ID_PERSONNE=ID_CORRESPONDANT";

                selectQuery += " where id_traitement = @id_traitement";

                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@id_traitement", com.Id);

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

        public static DataTable getActesSupTraitements(int idComtraitement,string  TYPE_ACTE_SUPP  = "")
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
               // MySqlTransaction transaction = connection.BeginTransaction();
                try
                {


                    string selectQuery = "select tc.ID_ACTE,a.ACTE_LIBELLE, a.ACTE_COULEUR, a.ACTE_DURESTD, a.PRIX_ACTE, tc.PRIX_TRAITEMENT, tc.QTE,a.ACTE_BASE_REMBOURSEMENT, a.ACTE_REMBOURSEMENT, a.ACTE_DEPASSEMENT, a.ACTE_CODE_TRANSPOSOTION, a.TARIF ";
                    selectQuery +=" FROM traitement_comm_actes tc ";
                    selectQuery += " left JOIN actes a on a.ID_ACTE = tc.ID_ACTE";
                    selectQuery += " where tc.id = @id";
                    selectQuery += " and tc.TYPE_ACTE_SUPP = '" + TYPE_ACTE_SUPP + "'";



                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@id", idComtraitement);
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

        public static void SaveActesSuppTraitement(CommTraitement  comm, string TYPE_ACTE_SUP = "")
        {
            if (comm.ActesSupp == null) return;
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                selectQuery = "delete from traitement_comm_actes where ID = @id";
               
                selectQuery += " AND TYPE_ACTE_SUPP='" + TYPE_ACTE_SUP + "'";
                



                command.Parameters.AddWithValue("@id", comm.Id);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteNonQuery();



                selectQuery = "insert into traitement_comm_actes (id, ";
                selectQuery += "                               ID_ACTE, TYPE_ACTE_SUPP,PRIX_TRAITEMENT, QTE)";
                selectQuery += " values (@id, ";
                selectQuery += "         @ID_ACTE,";
                selectQuery += "         @TYPE_ACTE_SUPP,";
                selectQuery += "         @PRIX_TRAITEMENT,@QTE)";

                command.CommandText = selectQuery;
                //command.Parameters.AddWithValue("@id_comm", comm.Id);
                //command.Parameters.AddWithValue("@ID_ACTE", comm.IdActe );

                //command.ExecuteNonQuery();


                List<CommActesTraitement> TmpActesSupp = new List<CommActesTraitement>();
                if (TYPE_ACTE_SUP == "R")
                {
                    TmpActesSupp =  comm.Radios;
                }
                else if (TYPE_ACTE_SUP == "P")
                {
                    TmpActesSupp = comm.photos;
                }
                else
                {
                    TmpActesSupp = comm.ActesSupp;
                }
                foreach (CommActesTraitement cr in TmpActesSupp)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", comm.Id );
                    command.Parameters.AddWithValue("@ID_ACTE", cr.IdActe);
                    command.Parameters.AddWithValue("@TYPE_ACTE_SUPP", TYPE_ACTE_SUP);
             


                   command.Parameters.AddWithValue("@PRIX_TRAITEMENT", cr.prix_traitement );
                   command.Parameters.AddWithValue("@QTE", cr.Qte);
                    

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
        public static void setTraitementAutrePersonnes(CommTraitement comm)
        {

            if (comm.AutrePersonnes == null) return;

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                selectQuery = "delete from base_traitement_AUTREPERS where ID_TRAITEMENT = @id";

                command.Parameters.AddWithValue("@id", comm.Id);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteNonQuery();



                selectQuery = "insert into base_traitement_AUTREPERS (ID_TRAITEMENT, ";
                selectQuery += "                            ID_CORRESPONDANT)";
                selectQuery += " values (@id, ";
                selectQuery += "         @ID_CORRESPONDANT)";





                command.CommandText = selectQuery;

                foreach (CommAutrePersonne cr in comm.AutrePersonnes)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", comm.Id);
                    command.Parameters.AddWithValue("@ID_CORRESPONDANT", cr.IdCorrespondant);

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

        public static void UpdateActeTraitement(CommTraitement comm)
        {



            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "update traitement_comments";
                selectQuery += " set ID_ACTE = @ID_ACTE ";
                selectQuery += " , ID_PRATICIEN = @ID_PRATICIEN ";
                selectQuery += " , ID_ASSISTANTE = @ID_ASSISTANTE ";
                selectQuery += " , ID_SECRETAIRE = @ID_SECRETAIRE ";
                selectQuery += " , PRIX_TRAITEMENT = @PRIX_TRAITEMENT ";
                selectQuery += " , QTE = @QTE ";
                selectQuery += " where (id = @id)";




                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;


                command.Parameters.AddWithValue("@id", comm.Id);
                command.Parameters.AddWithValue("@id_acte", comm.Acte == null ? DBNull.Value : (object)comm.Acte.id_acte);

                command.Parameters.AddWithValue("@id_praticien", comm.praticien == null ? DBNull.Value : (object)comm.praticien.Id);
                command.Parameters.AddWithValue("@ID_ASSISTANTE", comm.Assistante == null ? DBNull.Value : (object)comm.Assistante.Id);
                command.Parameters.AddWithValue("@ID_SECRETAIRE", comm.Secretaire == null ? DBNull.Value : (object)comm.Secretaire.Id);

                command.Parameters.AddWithValue("@PRIX_TRAITEMENT", (object)comm.Acte .prix_traitement );
                command.Parameters.AddWithValue("@QTE", comm.Acte.quantite == null ? "1" : (object)comm.Acte.quantite);

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
        public static DataTable getActesTraitements(int id_traitement)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
              //  MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    //famille_Traitement

                   // string selectQuery = "select * FROM traitement_comments TC where tc.ID_TRAITEMENT = @id_traitement ORDER by NBJOURS";

                    string selectQuery = "select tc.ID, tc.ID_TRAITEMENT, tc.id_acte, tc.ID_PRATICIEN, tc.ID_ASSISTANTE, tc.ID_SECRETAIRE, tc.DATE_COMM,  ";
                    selectQuery += " tc.HYGIENE,  a.PRIX_ACTE, tc.NBJOURS, tc.PRIX_TRAITEMENT, tc.QTE ";
                    selectQuery += " FROM traitement_comments tc  " ;
                    selectQuery += " LEFT JOIN actes a on a.ID_ACTE = tc.ID_ACTE " ;
                    selectQuery += " where tc.ID_TRAITEMENT = @id_traitement ORDER by NBJOURS";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection);
                    command.Parameters.AddWithValue("@id_traitement", id_traitement);
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

        public static DataTable GetCommTraitementMateriels(CommTraitement com)
        {
            if (connection == null) getConnection();

            if (connection.State == ConnectionState.Closed) connection.Open();
         //   MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                //string selectQuery = "select id_comm,         id_baseproduit,         m.MATERIEL_LIBELLE, ";
                //selectQuery += " bcm.QTE,m.ID_MATERIEL,m.SHORTLIB,m.MATERIEL_COULEUR ";
                //selectQuery += " from base_traitement_MAT BTM ";
                //selectQuery += " left join MATERIELS M on m.ID_MATERIEL  =  bcm.ID_MATERIEL ";
                //selectQuery += " where id = @ID";

                string selectQuery = "select id,  m.MATERIEL_LIBELLE,  ";
                selectQuery += " BTM.QTE,m.ID_MATERIEL,m.SHORTLIB,m.MATERIEL_COULEUR, m.PRIX_MATERIEL, BTM.PRIX_TRAITEMENT, BTM.QTE, m.ID_FAMILLE_MATERIEL  ";
                selectQuery += " from base_traitement_mat BTM  ";
                selectQuery += " left join materiels m on m.ID_MATERIEL  =  BTM.ID_MATERIEL  ";
                selectQuery += " where id = @ID";
                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                command.Parameters.AddWithValue("@ID", com.Id);

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

        public static void setTraitementPrix(CommTraitement   comm)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                MySqlCommand command;
                command = new MySqlCommand(selectQuery, connection, transaction);


                selectQuery = "update traitement_comments set   PRIX_TRAITEMENT =@PRIX_TRAITEMENT,QTE = @QTE where ID = @id";
     
                command.Parameters.AddWithValue("@id", comm.Id);
                command.Parameters.AddWithValue("@PRIX_TRAITEMENT", comm.Acte.prix_traitement);
                command.Parameters.AddWithValue("@QTE", comm.Acte.quantite);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteNonQuery();
                foreach (CommActesTraitement act in comm.ActesSupp)
                {
                    command = new MySqlCommand(selectQuery, connection, transaction);
                    selectQuery = "update traitement_comm_actes set   PRIX_TRAITEMENT =@PRIX_TRAITEMENT, QTE=@QTE where  Id_ACTE= @Id_ACTE and ID = @id   and TYPE_ACTE_SUPP = ''";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", comm.Id);
                    command.Parameters.AddWithValue("@PRIX_TRAITEMENT", act.prix_traitement );
                    command.Parameters.AddWithValue("@QTE", act.Qte );
                    command.Parameters.AddWithValue("@Id_ACTE", act.IdActe );

                    command.CommandText = selectQuery;
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();

                }
                foreach (CommActesTraitement act in comm.Radios)
                {
                    command = new MySqlCommand(selectQuery, connection, transaction);
                    selectQuery = "update traitement_comm_actes set   PRIX_TRAITEMENT =@PRIX_TRAITEMENT, QTE=@QTE where  Id_ACTE= @Id_ACTE and ID = @id   and TYPE_ACTE_SUPP = 'R'";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", comm.Id);
                    command.Parameters.AddWithValue("@PRIX_TRAITEMENT", act.prix_traitement );
                    command.Parameters.AddWithValue("@QTE", act.Qte);
                    command.Parameters.AddWithValue("@Id_ACTE", act.IdActe);

                    command.CommandText = selectQuery;
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();

                }
                foreach (CommActesTraitement act in comm.photos)
                {
                    command = new MySqlCommand(selectQuery, connection, transaction);
                    selectQuery = "update traitement_comm_actes set   PRIX_TRAITEMENT =@PRIX_TRAITEMENT, QTE=@QTE  where  Id_ACTE= @Id_ACTE and ID = @id   and TYPE_ACTE_SUPP = 'P'";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", comm.Id);
                    command.Parameters.AddWithValue("@PRIX_TRAITEMENT", act.prix_traitement );
                    command.Parameters.AddWithValue("@QTE", act.Qte);
                    command.Parameters.AddWithValue("@Id_ACTE", act.IdActe);

                    command.CommandText = selectQuery;
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();

                }
                foreach (CommMaterielTraitement    act in comm.Materiels)
                {
                    selectQuery = "update base_traitement_mat set   PRIX_TRAITEMENT =@PRIX_TRAITEMENT, QTE=@QTE where  ID_MATERIEL= @ID_MATERIEL and ID = @id  ";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", comm.Id);
                    command.Parameters.AddWithValue("@PRIX_TRAITEMENT", act.prix_traitement );
                    command.Parameters.AddWithValue("@QTE", act.Qte);
                    command.Parameters.AddWithValue("@ID_MATERIEL", act.idMateriel );
                  

                    command.CommandText = selectQuery;
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();

                }

/*
                selectQuery = "insert into base_traitement_MAT (id, ";
                selectQuery += "                            qte, ";
                selectQuery += "                            id_materiel) ";
                selectQuery += " values (@id, ";
                selectQuery += "         @qte, ";
                selectQuery += "         @id_materiel) ";



                command.CommandText = selectQuery;

                foreach (CommMateriel cr in comm.Materiels)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", comm.Id);
                    command.Parameters.AddWithValue("@qte", cr.Qte);
                    command.Parameters.AddWithValue("@id_materiel", cr.idMateriel);


                    command.ExecuteNonQuery();
                }

                */
                command.Transaction.Commit();



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
        public static void setTraitementMateriels(CommTraitement comm)
        {

            if (comm.Materiels == null) return;

            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {

                string selectQuery = "";
                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);


                selectQuery = "delete from base_traitement_mat where ID = @id";

                command.Parameters.AddWithValue("@id", comm.Id);
                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                object obj = command.ExecuteNonQuery();



                selectQuery = "insert into base_traitement_mat (id, ";
                selectQuery += "                            qte, ";
                selectQuery += "                            id_materiel, ";
                selectQuery += "                            PRIX_TRAITEMENT) ";
                selectQuery += " values (@id, ";
                 selectQuery += "         @qte, ";
                 selectQuery += "         @id_materiel, ";
                selectQuery += "         @PRIX_TRAITEMENT) ";
           


                command.CommandText = selectQuery;

                foreach (CommMaterielTraitement cr in comm.Materiels)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", comm.Id);
                    command.Parameters.AddWithValue("@qte", cr.Qte);
                    command.Parameters.AddWithValue("@id_materiel", cr.idMateriel);
                   
                    command.Parameters.AddWithValue("@PRIX_TRAITEMENT", cr.prix_traitement );
               

                    command.ExecuteNonQuery();
                }


                command.Transaction.Commit();



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

        public static DataTable getFamillesTraitements()
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
               // MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    //famille_Traitement
                    string selectQuery = "select * from rh_base_familletraitement order by ordre";

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

        public static void AddFamille(FamillesTraitements p_famille)
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
                    string selectQuery = "select MAX(ID)+1 as NEWID from rh_base_familletraitement";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.CommandType = CommandType.Text;
                    object res = command.ExecuteScalar();
                    if (res is DBNull)
                        p_famille.Id = 1;
                    else
                        p_famille.Id = Convert.ToInt32(command.ExecuteScalar());

                    selectQuery = "select MAX(ordre)+1 as NEWID from rh_base_familletraitement";
                    MySqlCommand commandOrdre = new MySqlCommand(selectQuery, connection, transaction);
                    commandOrdre.CommandType = CommandType.Text;

                    object resOrdre = commandOrdre.ExecuteScalar();
                    if (resOrdre is DBNull)
                        p_famille.ordre = 1;
                    else
                        p_famille.ordre = Convert.ToInt32(commandOrdre.ExecuteScalar());



                    selectQuery = "insert into rh_base_familletraitement (id, nom, couleur, parent, ordre) values (@id, @nom, @couleur, @parent, @ordre)";

                    command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", p_famille.Id);
                    command.Parameters.AddWithValue("@nom", p_famille.libelle);
                    command.Parameters.AddWithValue("@couleur", p_famille.couleur.ToArgb());
                    command.Parameters.AddWithValue("@parent", p_famille.ParentFamillesTraitementId);
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


        public static void UpdateTitreDevis(string p_titre, int p_id)
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


                    string selectQuery = "update rh_base_familletraitement set TITRE_DEVIS = @TITRE_DEVIS where id=@id";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", p_id );
                    command.Parameters.AddWithValue("@TITRE_DEVIS", p_titre );
                   

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

        public static void UpdateFamille(FamillesTraitements p_famille)
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


                    string selectQuery = "update rh_base_familletraitement set nom = @nom, couleur = @couleur, parent = @parent, ordre = @ordre where id=@id";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", p_famille.Id);
                    command.Parameters.AddWithValue("@nom", p_famille.libelle);
                    command.Parameters.AddWithValue("@couleur", p_famille.couleur.ToArgb());
                    command.Parameters.AddWithValue("@parent", p_famille.ParentFamillesTraitementId);
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
        public static void ReorderFamille(FamillesTraitements p_famille, int NewPos)
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


                    string selectQuery = "update rh_base_familletraitement  set ordre = ordre+1 where ordre>=@order and parent = @parent";
                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@order", NewPos);
                    command.Parameters.AddWithValue("@parent", p_famille.ParentFamillesTraitementId);
                    command.ExecuteNonQuery();

                    selectQuery = "update rh_base_familletraitement  set ordre = @order where Id=@Id";
                    command.CommandText = selectQuery;
                    command.Parameters.AddWithValue("@Id", p_famille.Id);
                    command.ExecuteNonQuery();

                    selectQuery = "SET GENERATOR rh_base_familletraitement_ORDER TO 0";
                    command.CommandText = selectQuery;
                    command.ExecuteNonQuery();

                    selectQuery = "update rh_base_familletraitement  set ordre = GEN_ID(rh_base_familletraitement_ORDER, 1 )  where parent = @parent order by ordre";
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

        public static void DelFamille(FamillesTraitements p_famille)
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


                    string selectQuery = "delete from rh_base_familletraitement where id=@id";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                    command.Parameters.AddWithValue("@id", p_famille.Id);
                    command.ExecuteNonQuery();


                    selectQuery = "update traitements set ID_FAMILLE_Traitement = null where (ID_FAMILLE_Traitement = @id)";
                    command.CommandText = selectQuery;
                    command.ExecuteNonQuery();

                    selectQuery = "update rh_base_familletraitement set Parent = null where (Parent = @id)";
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

        public static void UpdateFamilyTraitement(NewTraitement p_Traitement, FamillesTraitements p_familleTraitement)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection(); if (connection == null) return;

                if (connection.State == ConnectionState.Closed) connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "update traitements set ID_FAMILLE_Traitement = @ID_FAMILLE_Traitement where (ID_Traitement = @ID_Traitement)";

                    MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@ID_FAMILLE_Traitement", p_familleTraitement.Id);
                    command.Parameters.AddWithValue("@ID_Traitement", p_Traitement.id_Traitement );
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

        public static void AddActeTraitement(int id_traitement, CommTraitement p_com)
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
                string selectQuery = "select MAX(id)+1 as NEWID from traitement_comments";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object res = command.ExecuteScalar();
                p_com.Id = id_traitement;
                if (res is DBNull)
                    p_com.Id  = 1;
                else
                    p_com.Id = Convert.ToInt32(command.ExecuteScalar());

                selectQuery = @"insert into traitement_comments (id, 
                                            id_traitement, 
                                            id_acte,
                                            ID_PRATICIEN ,
                                            ID_ASSISTANTE ,
                                            ID_SECRETAIRE ,
                                            DATE_COMM,
                                            PRIX_ACTE , NBJOURS, PRIX_TRAITEMENT, QTE  )
                        values (@id, 
                                @id_traitement, 
                                @id_acte,
                                @ID_PRATICIEN ,
                                @ID_ASSISTANTE ,
                                @ID_SECRETAIRE ,
                                @DATE_COMM,
                                @PRIX_ACTE,
                                @NBJOURS,@PRIX_TRAITEMENT, @QTE)";

                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id", p_com.Id );
                command.Parameters.AddWithValue("@id_traitement", id_traitement);
                command.Parameters.AddWithValue("@id_acte", p_com.Acte .id_acte );

                command.Parameters.AddWithValue("@ID_PRATICIEN", p_com.IdPraticien );
                command.Parameters.AddWithValue("@ID_ASSISTANTE", p_com.IdAssistante );
                command.Parameters.AddWithValue("@ID_SECRETAIRE", p_com.IdSecretaire);
                command.Parameters.AddWithValue("@DATE_COMM", p_com.DatePrevisionnnelle );
                command.Parameters.AddWithValue("@PRIX_ACTE", p_com.Acte .prix_acte );
                command.Parameters.AddWithValue("@NBJOURS", (p_com.NbMois * 30) + p_com.NbJours);

                command.Parameters.AddWithValue("@PRIX_TRAITEMENT", p_com.Acte.prix_traitement );
                command.Parameters.AddWithValue("@QTE", p_com.Acte .quantite );

         
          

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

        public static void DeleteActeTraitement(CommTraitement comm)
        {



            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {



                string selectQuery = "delete from  traitement_comm_actes";
                selectQuery += " where (ID = @id)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@id", comm.Id);
                command.ExecuteNonQuery();


                selectQuery = "delete from  base_traitement_mat";
                selectQuery += " where (ID = @id)";

                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();



                selectQuery = "delete from  traitement_comments";
                selectQuery += " where (ID = @id)";

                command.CommandText = selectQuery;
                command.CommandType = CommandType.Text;
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

        public static void AddTraitement(NewTraitement p_Traitement)
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
                string selectQuery = "select MAX(ID_Traitement)+1 as NEWID from traitements";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;
                object res = command.ExecuteScalar();
                if (res is DBNull)
                    p_Traitement.id_Traitement = 1;
                else
                    p_Traitement.id_Traitement = Convert.ToInt32(command.ExecuteScalar());

                selectQuery = @"insert into traitements (id_Traitement, 
                                            Traitement_libelle, 
                                            Traitement_COULEUR,
                                            ID_FAMILLE_Traitement, 
                                            SHORTLIB,
                                            TYPE,
                                            CONTENTION,
                                            commentaire   
                                         )
                        values (@id_Traitement, 
                                @Traitement_libelle, 
                                @Traitement_couleur, 
                                @idfamTraitement, 
                                @Traitement_nom_court,@TYPE,@CONTENTION,@traitement_commentaire)";

                command = new MySqlCommand(selectQuery, connection, transaction);
                command.Parameters.AddWithValue("@id_Traitement", p_Traitement.id_Traitement);
                command.Parameters.AddWithValue("@Traitement_libelle", p_Traitement.Traitement_libelle);
                command.Parameters.AddWithValue("@Traitement_nom_court", p_Traitement.Traitement_shortlib);
                command.Parameters.AddWithValue("@CONTENTION", p_Traitement.contention);
                command.Parameters.AddWithValue("@Traitement_couleur", System.Drawing.ColorTranslator.ToWin32(p_Traitement.Traitement_couleur));
                command.Parameters.AddWithValue("@TYPE", p_Traitement.TypeScenario);
                command.Parameters.AddWithValue("@Traitement_commentaire", p_Traitement.Traitement_commentaire);


                if (p_Traitement.famille_Traitement == null)
                    command.Parameters.AddWithValue("@idfamTraitement", -1);
                else
                    command.Parameters.AddWithValue("@idfamTraitement", p_Traitement.famille_Traitement.Id);
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

        public static void UpdateTraitement(NewTraitement p_Traitement)
        {
            if (connection == null) getConnection(); if (connection == null) return;

            if (connection.State == ConnectionState.Closed) connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();
            try
            {
                string selectQuery = @" update traitements
                                    set Traitement_LIBELLE = @Traitement_LIBELLE,
                                        SHORTLIB = @Traitement_NOMCOURT,
                                        Traitement_COULEUR = @Traitement_COULEUR,
                                        ID_FAMILLE_Traitement = @ID_FAMILLE_Traitement,
                                            MONTANTSCENARIO = @MONTANTSCENARIO,
                                            TYPE=@TYPE,
                                            CONTENTION=@CONTENTION ,
                                            commentaire=@Traitement_commentaire
                                    where (ID_Traitement = @ID_Traitement)";

                MySqlCommand command = new MySqlCommand(selectQuery, connection, transaction);
                command.CommandType = CommandType.Text;



                command.Parameters.AddWithValue("@ID_Traitement", p_Traitement.id_Traitement);
                command.Parameters.AddWithValue("@Traitement_LIBELLE", p_Traitement.Traitement_libelle);
                command.Parameters.AddWithValue("@Traitement_NOMCOURT", p_Traitement.Traitement_shortlib);
                command.Parameters.AddWithValue("@MONTANTSCENARIO", p_Traitement.Montant_Scenario);
                command.Parameters.AddWithValue("@CONTENTION", p_Traitement.contention);
                command.Parameters.AddWithValue("@Traitement_COULEUR", System.Drawing.ColorTranslator.ToWin32(p_Traitement.Traitement_couleur));
                command.Parameters.AddWithValue("@Traitement_commentaire", p_Traitement.Traitement_commentaire);

                if ((System.Configuration.ConfigurationManager.AppSettings["PaysCabinet" + prefix] == "FR"))
                {
                    command.Parameters.AddWithValue("@TYPE", p_Traitement.TypeScenario);
                }
                else
                    command.Parameters.AddWithValue("@TYPE", -1);
                if (p_Traitement.famille_Traitement == null)
                    command.Parameters.AddWithValue("@ID_FAMILLE_Traitement", -1);
                else
                    command.Parameters.AddWithValue("@ID_FAMILLE_Traitement", p_Traitement.famille_Traitement.Id);


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

        public static void DeleteTraitement(NewTraitement p_Traitement)
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
                //suppression des matériaux
                string selectquery = "delete from base_traitement_mat  WHERE base_traitement_mat.ID in ";
                selectquery += "(SELECT BC.ID FROM traitement_comments BC WHERE BC.ID_TRAITEMENT = @id)";
                MySqlCommand commandt = new MySqlCommand(selectquery, connection, transaction);
                commandt.Parameters.AddWithValue("@id", p_Traitement.id_Traitement);
                commandt.ExecuteNonQuery();

                //Suppression des autres personne
                selectquery = "delete from base_traitement_autrepers WHERE base_traitement_autrepers.ID_TRAITEMENT in ";
                selectquery += "(SELECT BC.ID FROM traitement_comments BC WHERE BC.ID_TRAITEMENT = @id)";

                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();

                //Suppression des Actes (Supp, Radio et Photos)
                selectquery = "delete from traitement_comm_actes WHERE traitement_comm_actes.ID  in ";
                selectquery += "(SELECT BC.ID FROM traitement_comments BC WHERE BC.ID_TRAITEMENT = @id)";


                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();



                //Suppression des Comments
                selectquery = "delete from traitement_comments WHERE traitement_comments.ID_TRAITEMENT  = @id";

                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();



                 selectquery = "delete from traitements where ID_Traitement=@id";
                commandt.CommandText = selectquery;

                commandt.ExecuteNonQuery();


             



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

        public static Boolean SearchNameTraitement(string s_Traitement)
        {

            lock (lockobj)
            {
                if (connection == null) getConnection();

                if (connection.State == ConnectionState.Closed) connection.Open();
              //  MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string selectQuery = "select count(*) from traitements";
                    selectQuery += " where Traitement_LIBELLE=@Traitement_LIBELLE";
                    selectQuery += " Group by Traitement_LIBELLE";
                    MySqlCommand command = new MySqlCommand(selectQuery, connection);

                    command.Parameters.AddWithValue("@Traitement_LIBELLE", s_Traitement);
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

    }
}
